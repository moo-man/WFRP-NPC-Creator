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
        public static List<CareerClass> ClassList = new List<CareerClass>();
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

        public static List<Career> GetCareerList()
        {
            List<Career> flatList = new List<Career>();
            foreach (CareerClass cClass in ClassList)
                foreach (CareerPath cPath in cClass.CareerPaths)
                    foreach (Career career in cPath.Tiers)
                        flatList.Add(career);

            return flatList.OrderBy(c => c.Name).ToList();

        }
    }

    public class CareerAdvancement
    {
        public  Dictionary<Characteristics, int> CharacteristicAdvances { get; private set; } = new Dictionary<Characteristics, int>();
        public List<Skill> SkillsAdvanced { get; private set; } = new List<Skill>();
        public List<Talent> TalentsAdvanced { get; private set; } = new List<Talent>();
        public Career CareerTemplate { get; private set; }
        public Character Owner { get; private set; }
        public AdvanceLevel Advancement { get; private set; }

        private Random rand = new Random();

        public CareerAdvancement(Character owner, Career careerToAdd, AdvanceLevel advancement = AdvanceLevel.None, bool skillFocus = true, bool talentFocus = true)
        {
            Owner = owner;
            CareerTemplate = careerToAdd;
            for (Characteristics i = 0; i < (Characteristics)10; i++)
                CharacteristicAdvances.Add(i, 0);

            Advancement = advancement;

            AdvanceCharacteristics();
            AdvanceSkills(skillFocus);
            AdvanceTalents(talentFocus);
        }

        private void AdvanceCharacteristics()
        {
            int advances = 0;
            foreach (Characteristics ch in CareerTemplate.CareerCharacteristics)
            {
                advances = GenerateAdvanceNum(false) * CareerTemplate.Tier;
                if (Owner.TotalCharacteristicAdvances(ch) - CharacteristicAdvances[ch] < advances)
                    CharacteristicAdvances[ch] = advances - (Owner.TotalCharacteristicAdvances(ch) - CharacteristicAdvances[ch]);
            }
        }

        private void AdvanceSkills(bool focus)
        {
            SkillsAdvanced = new List<Skill>();
            int skillsLearned = 0;
            int advances = 0;

            double percentToLearn = 0.125;
            List<string> LearnedList = new List<string>();


            if (focus)
            {
                foreach (string focusedSkill in CareerTemplate.FocusSkills)
                {
                    advances = GenerateAdvanceNum(false) * CareerTemplate.Tier;
                    int currentAdvances = Owner.TotalSkillAdvancesToCareer(focusedSkill, this);
                    if (currentAdvances < advances && advances != 0)
                    {
                        SkillsAdvanced.Add(new Skill(focusedSkill, advances - currentAdvances));
                        skillsLearned++;
                        percentToLearn += 0.125;
                        LearnedList.Add(focusedSkill);
                    }
                }
            }
            string[] SkillPool = CareerTemplate.CareerSkills.OrderBy(sk => rand.Next()).ToArray(); // Randomly order skill pool
            while (skillsLearned != 8)
            {
                for (int i = 0; i < SkillPool.Length && skillsLearned != 8; i++)
                {
                    string possibleSkill = SkillPool[i];

                    // If we have not already learned the skill -> learn it by chance or learn it if we are focusing on relevant skills and it qualifies
                    if (!LearnedList.Contains(possibleSkill) &&
                        (rand.NextDouble() < percentToLearn || (focus && CareerTemplate.FocusSkills.Contains(possibleSkill)))) // Learn by chance or if focusing and skill should be focused
                    {
                        advances = GenerateAdvanceNum(false) * CareerTemplate.Tier;
                        int currentAdvances = Owner.TotalSkillAdvancesToCareer(possibleSkill, this);
                        if (currentAdvances < advances && advances != 0)
                        {
                            SkillsAdvanced.Add(new Skill(possibleSkill, advances - currentAdvances));
                            skillsLearned++;
                            percentToLearn += 0.125;
                            LearnedList.Add(possibleSkill);
                        }
                        else if (currentAdvances >= advances)
                        {   // Prevent infinite loop if skill already learned
                            skillsLearned++;
                            percentToLearn += 0.125;
                        }
                    }
                }
            }
        }

        private void AdvanceTalents(bool focus)
        {
            TalentsAdvanced = new List<Talent>();
            Random advanceGen = new Random();
            int talentsAdvanced = 0;

            int advances = GenerateAdvanceNum(true);

            //int relevantTalents = CareerTemplate.CareerTalents.Where(t => Talent.IsRelevant(t)).Count();

            while (talentsAdvanced < advances)
            {
                Talent talentPick = new Talent(CareerTemplate.CareerTalents[advanceGen.Next(0, CareerTemplate.CareerTalents.Length)], Owner);
                int currentAdvances = Owner.TotalTalentAdvances(talentPick);

                /*// If focused and all relevant talents are taken
                if (focus && talentsAdvanced == relevantTalents)
                    focus = false;*/               

                if (currentAdvances < talentPick.Max())// && !(focus && !talentPick.IsRelevant())) // Don't consider it if we are focusing AND it's not relevant
                {
                    if (TalentsAdvanced.Contains(talentPick))
                        TalentsAdvanced.Find(t => t.Name == talentPick.Name).Advance();
                    else
                        TalentsAdvanced.Add(talentPick);
                    talentsAdvanced++;
                }
            }           
        }


        private int GenerateAdvanceNum(bool talentAdvance)
        {
            int advanceMin, advanceMax;

            if (!talentAdvance)
            {
                switch (Advancement)
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

                    case AdvanceLevel.Beyond:
                        advanceMin = AdvanceConstraints.BEYOND_MIN;
                        advanceMax = AdvanceConstraints.BEYOND_MAX_EX;
                        break;
                    default:
                        advanceMin = 0;
                        advanceMax = 1;
                        break;
                }
            }
            else
            {
                switch (Advancement)
                {
                    case AdvanceLevel.Experienced:
                        advanceMin = 0;
                        advanceMax = 2;
                        break;

                    case AdvanceLevel.Complete:
                        advanceMin = 1;
                        advanceMax = 3;
                        break;

                    case AdvanceLevel.Beyond:
                        advanceMin = 2;
                        advanceMax = 3;
                        break;

                    default:
                        advanceMin = 0;
                        advanceMax = 0;
                        break;
                }
            }

            return rand.Next(advanceMin, advanceMax);
        }

        public void ChangeAdvancement(AdvanceLevel newAdvLevel)
        {
            Advancement = newAdvLevel;
            AdvanceCharacteristics();
            AdvanceSkills(true);
            AdvanceTalents(true);
        }

        public void RerollSkills()
        {
            AdvanceSkills(true);
        }

        public void RerollCharacteristics()
        {
            AdvanceCharacteristics();
        }

        public void RerollTalents()
        {
            AdvanceTalents(true);
        }
    }

    public class CareerClass
    {
        public string ClassName { get; set; }
        public List<CareerPath> CareerPaths { get; set; } = new List<CareerPath>();

        public CareerClass(string name)
        {
            ClassName = name;
        }

        public void AddCareerPath(CareerPath path)
        {
            CareerPaths.Add(path);
        }
    }

    public class CareerPath
    {

        public string PathName { get; set; }
        public List<Career> Tiers { get; set; } = new List<Career>();

        public CareerPath(string pathName)
        {
            PathName = pathName;
        }

        public void AddTier(Career tier)
        {
            Tiers.Add(tier);
        }
    }

    public static class CareerJsonReader
    {
        public static bool read = false;
        static dynamic ClassList;
        static CareerJsonReader()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            StreamReader sr = new StreamReader(assembly.GetManifestResourceStream("WFRP_NPC_Creator.Data.careers.json"));

            ClassList = JsonConvert.DeserializeObject(sr.ReadToEnd());
            sr.Close();


            CareerClass currentClass;
            CareerPath currentPath;
            Career currentTier;

            foreach (dynamic CareerClass in ClassList.Classes)
            {
                currentClass = new CareerClass(CareerClass.Name);
                foreach (dynamic career in CareerClass.First)
                {
                    currentPath = new CareerPath(career.Name);

                    int tierCounter = 1;
                    List<string> skillList = new List<string>();
                    List<string> trappings = new List<string>();
                    foreach (dynamic tier in career.Value["Tiers"])
                        try
                        {
                            string name = tier["Name"];
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
                            currentTier = new Career(name, tierCounter, characteristicAvailable, skillList.ToArray(), talentList.ToArray(), trappings.ToArray());
                            currentPath.AddTier(currentTier);
                            tierCounter++;
                        }
                        catch (Exception e)
                        {

                        }
                    currentClass.AddCareerPath(currentPath);
                }
                Career.ClassList.Add(currentClass);
            }
        }
    }
}
