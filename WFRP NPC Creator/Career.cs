using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Reflection;

namespace WFRP_NPC_Creator
{
    public class Career
    {
        public static List<Career> List = new List<Career>();
        public string Name { get; private set; }
        public int Tier { get; private set; }
        public Characteristics[] CareerCharacteristics { get; private set; }
        public string[] CareerSkills { get; private set; }
        public string[] CareerTalents { get; private set; }
        public string[] CareerTrappings { get; private set; }

        // priority skills to focus
        public string[] FocusSkills;

        public Career(string name, int tier, Characteristics[] availableChar, string[] skills, string[] talents, string[] trappings)
        {
            Name = name;
            Tier = tier;
            CareerCharacteristics = availableChar;
            CareerSkills = skills;
            CareerTalents = talents;
            CareerTrappings = trappings;
            if (!CareerFocusLookup.ShouldFocus.TryGetValue(Name, out FocusSkills))
                FocusSkills = new string[0];
        }
    }

    public class CareerAdvancement
    {
        public  Dictionary<Characteristics, int> CharacteristicAdvances { get; private set; } = new Dictionary<Characteristics, int>();
        public List<Skill> SkillsAdvanced { get; private set; } = new List<Skill>();
        public List<Talent> TalentsAdvanced { get; private set; } = new List<Talent>();
        public Career CareerTemplate { get; private set; }
        public Character Owner { get; private set; }

        public CareerAdvancement(Character owner, Career careerToAdd, AdvanceLevel advancement = AdvanceLevel.None, bool focus = true)
        {
            Owner = owner;
            CareerTemplate = careerToAdd;
            Random advanceGen = new Random();
            int advances = 0;
            for (Characteristics i = 0; i < (Characteristics)10; i++)
                CharacteristicAdvances.Add(i, 0);

            int skillsLearned = 0;
            double percentToLearn = 0.125;
            List<string> LearnedList = new List<string>();
            int advanceMin, advanceMax;

            switch (advancement)
            {
                case AdvanceLevel.Beginner:
                    advanceMin = AdvanceConstraints.EXPERIENCED_MIN;
                    advanceMax = AdvanceConstraints.EXPERIENCED_MAX_EX;
                    break;       

                case AdvanceLevel.Experienced:
                    advanceMin = AdvanceConstraints.EXPERIENCED_MIN;
                    advanceMax = AdvanceConstraints.EXPERIENCED_MAX_EX;
                    break;

                case AdvanceLevel.Complete:
                    advanceMin = 5;
                    advanceMax = 6;
                    break;

                default:
                    advanceMin = 0;
                    advanceMax = 1;
                    break;
                 }

            foreach (Characteristics ch in careerToAdd.CareerCharacteristics)
            {
                advances = advanceGen.Next(advanceMin, advanceMax) * CareerTemplate.Tier;
                if (Owner.TotalCharacteristicAdvances(ch) < advances)
                    CharacteristicAdvances[ch] += advances - Owner.TotalCharacteristicAdvances(ch);
            }


            if (focus)
            {
                foreach (string focusedSkill in CareerTemplate.FocusSkills)
                {
                    advances = advanceGen.Next(advanceMin, advanceMax) * CareerTemplate.Tier;
                    int currentAdvances = Owner.TotalSkillAdvances(focusedSkill);
                    if (currentAdvances < advances && advances != 0)
                    {
                        SkillsAdvanced.Add(new Skill(focusedSkill, advances - currentAdvances));
                        skillsLearned++;
                        percentToLearn += 0.125;
                        LearnedList.Add(focusedSkill);
                    }
                }
            }
            string[] SkillPool = CareerTemplate.CareerSkills.OrderBy(sk => advanceGen.Next()).ToArray(); // Randomly order skill pool
            while (skillsLearned != 8)
            {
                for (int i = 0; i < SkillPool.Length && skillsLearned != 8; i++)
                {
                    string possibleSkill = SkillPool[i];

                    // If we have not already learned the skill -> learn it by chance or learn it if we are focusing on relevant skills and it qualifies
                    if (!LearnedList.Contains(possibleSkill) && 
                        (advanceGen.NextDouble() < percentToLearn || (focus && CareerTemplate.FocusSkills.Contains(possibleSkill)))) // Learn by chance or if focusing and skill should be focused
                    {
                        advances = advanceGen.Next(advanceMin, advanceMax) * CareerTemplate.Tier;
                        int currentAdvances = Owner.TotalSkillAdvances(possibleSkill);
                        if (currentAdvances < advances && advances != 0)
                        {
                            SkillsAdvanced.Add(new Skill(possibleSkill, advances - currentAdvances));
                            skillsLearned++;
                            percentToLearn += 0.125;
                            LearnedList.Add(possibleSkill);
                        }
                    }
                }
            }
        }

    }

    public static class CareerJsonReader
    {
        public static bool read = false;
        static dynamic ClassList;
        static List<dynamic> AllCareers;
        static List<dynamic> AllCareerTiers;
        static CareerJsonReader()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            StreamReader sr = new StreamReader(assembly.GetManifestResourceStream("WFRP_NPC_Creator.careers.json"));

            ClassList = JsonConvert.DeserializeObject(sr.ReadToEnd());
            sr.Close();

            AllCareers = new List<dynamic>();
            AllCareerTiers = new List<dynamic>();

            foreach (dynamic CareerClass in ClassList.Classes)
            {
                foreach (dynamic career in CareerClass.First)
                {
                    AllCareers.Add(career);
                    int tierCounter = 1;
                    List<string> skillList = new List<string>();
                    List<string> trappings = new List<string>();
                    foreach (dynamic tier in career.Value["Tiers"])
                        try
                        {
                            string name = tier["Name"];
                            AllCareerTiers.Add(tier);

                            List<string> talentList = new List<string>();
                            Characteristics c  = Characteristics.WS;
                            Characteristics[] characteristicAvailable = new Characteristics[tierCounter + 2];
                            int counter = 0;
                            foreach (dynamic characteristic in career.Value["Attributes"])
                            {
                                if (characteristic.Value != 0 && characteristic.Value <= tierCounter)
                                {
                                    characteristicAvailable[counter] = c;
                                    counter++;
                                }
                                c++;
                            }

                            foreach (dynamic skill in tier["Skills"])
                            {
                                skillList.Add(skill.Value);
                            }

                            foreach (dynamic talent in tier["Talents"])
                            {
                                talentList.Add(talent.Value);
                            }

                            foreach (dynamic trapping in tier["Trappings"])
                            {
                                trappings.Add(trapping.Value);
                            }

                            Career.List.Add(new Career(name, tierCounter, characteristicAvailable, skillList.ToArray(), talentList.ToArray(), trappings.ToArray()));

                            tierCounter++;
                        }
                        catch (Exception e)
                        {

                        }
                }
            }
        }
    }
}
