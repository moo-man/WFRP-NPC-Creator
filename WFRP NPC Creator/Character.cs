using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFRP_NPC_Creator
{
    public abstract class Character
    {
        protected Dictionary<Characteristics, int> initialCharacteristics { get; private set; } = new Dictionary<Characteristics, int>();
        protected Species species;
        protected int movement;
        protected int wounds;

        public List<CareerAdvancement> Careers { get; private set; } = new List<CareerAdvancement>();
        public List<Skill> Skills { get; private set; } = new List<Skill>();
        public List<Talent> Talents { get; private set; } = new List<Talent>();

        public static Random rand = new Random();
        public Character()
        {
            for (Characteristics i = 0; i < (Characteristics)10; i++)
                initialCharacteristics.Add(i, 0);

            RollCharacteristics();
            AdvanceSpeciesSkills();
            AddSpeciesTalents();
        }

        public void PrintToConsole()
        {
            Console.WriteLine("WS\tBS\tS\tT\tI\tAgi\tDex\tInt\tWP\tFel\n");
            for (Characteristics i = 0; i < (Characteristics)10; i++)
            {
                Console.Write(CharacteristicValue(i) + "\t");
            }
            Console.WriteLine("\n______________________________________");
            foreach (Skill sk in GetAllSkills())
            {
                if (sk.IsRelevant())
                    System.Diagnostics.Debug.Write(SkillNameAndValue(sk) + " ");
            }

            System.Diagnostics.Debug.Write('\n');
            foreach (Talent talent in GetAllTalents())
            {
                if (talent.IsRelevant())
                    System.Diagnostics.Debug.Write(talent.TalentNameAndValue());
            }
            Console.WriteLine();
        }

        public int CharacteristicValue(Characteristics ch)
        {
            return CharacteristicValues()[ch];
        }

        public Dictionary<Characteristics, int> CharacteristicValues()
        {
            Dictionary<Characteristics, int> values = new Dictionary<Characteristics, int>();
            Dictionary<Characteristics, int> advances = TotalCharacteristicAdvances();
            for (Characteristics i = 0; i < (Characteristics)10; i++)
            {
                values[i] = initialCharacteristics[i];
                values[i] += advances[i];
            }

            return values;
        }

        public Dictionary<Characteristics, int> TotalCharacteristicAdvances()
        {
            Dictionary<Characteristics, int> chAdvances = new Dictionary<Characteristics, int>();
            for (Characteristics i = 0; i < (Characteristics)10; i++)
            {
                chAdvances[i] = 0;
                foreach (CareerAdvancement career in Careers)
                    chAdvances[i] += career.CharacteristicAdvances[i];
            }
            return chAdvances;
        }

        public int TotalCharacteristicAdvances(Characteristics ch)
        {
            return TotalCharacteristicAdvances()[ch];
        }


        public int TotalSkillAdvances(Skill skill)
        {
            return TotalSkillAdvances(skill.Name);

        }
        public int TotalSkillAdvances(string skillName)
        {
            int totalValue = 0;
            Skill characterSkill = Skills.Find(sk => sk.Name == skillName);

            if (characterSkill != null)
                totalValue += characterSkill.Advances;

            foreach (CareerAdvancement career in Careers)
            {
                Skill skillAdvanced = career.SkillsAdvanced.Find(sk => sk.Name == skillName);
                if (skillAdvanced != null)
                    totalValue += skillAdvanced.Advances;
            }

            return totalValue;
        }

        public List<Skill> GetAllSkills()
        {
            List<Skill> SkillList = Skills;
            foreach (CareerAdvancement career in Careers)
            {
                SkillList = SkillList.Concat(career.SkillsAdvanced).ToList();
            }
            SkillList = SkillList.Distinct(new SkillEqualityComparer()).ToList();
                
            SkillList.Sort(delegate (Skill a, Skill b)
            {
                return a.Name.CompareTo(b.Name);
            });
            return SkillList;
        }

        public List<Talent> GetAllTalents()
        {

        }

        public int SkillValue(Skill skill)
        {
            return SkillValue(skill.Name);
        }

        public int SkillValue(string skillName)
        {   
            return CharacteristicValue(Skill.CharacteristicLookUp(skillName)) + TotalSkillAdvances(skillName);
        }

        public string SkillNameAndValue(Skill skill)
        {
            return skill.Name + " " + SkillValue(skill);
        }

        protected virtual void AddSkill(string skillName, int advances)
        {
            if (advances == 0)
                return;

            Skill acquiredSkill = Skills.Find(x => x.Name == skillName);

            if (acquiredSkill == null)
            {
                acquiredSkill = new Skill(skillName, advances);
                Skills.Add(acquiredSkill);
            }
            else
            {
                acquiredSkill.Advance(advances);
            }

        }

        protected virtual void AddTalent(string talentName)
        {
            Talent talentAcquired = Talents.Find(x => x.Name == talentName);

            if (talentAcquired != null)
                talentAcquired.Advance();
            else
            {
                Talents.Add(new Talent(talentName));
                Tuple<Characteristics, int> bonus;
                Talent.TalentBonus.TryGetValue(talentName, out bonus);

                if (bonus != null)
                    initialCharacteristics[bonus.Item1] += bonus.Item2;
            }
        }

        protected virtual void RemoveTalent(string talentName)
        {
            Talent talentToRemove = Talents.Find(x => x.Name == talentName);

            if (talentToRemove != null)
            {
                Talents.Remove(talentToRemove);
                Tuple<Characteristics, int> bonus;
                Talent.TalentBonus.TryGetValue(talentName, out bonus);

                if (bonus != null)
                    initialCharacteristics[bonus.Item1] -= bonus.Item2;
            }
            else return;

        }

        public virtual void AddCareer(string name, AdvanceLevel advancement = AdvanceLevel.None)
        {

            Careers.Add(new CareerAdvancement(this, Career.List.Find(c => c.Name == name), advancement));
         /*   Career careerToAdd = Career.List.Find(x => x.Name == name);
            Random advanceGen = new Random();
            int advances;
            switch (advancement)
            {
                case AdvanceLevel.Beginner:
                    foreach (Characteristics characteristic in careerToAdd.CareerCharacteristics)
                    {
                        advances = advanceGen.Next(0, 4);
                        if (characteristicAdvances[characteristic] < advances)
                            characteristicAdvances[characteristic] += advances; // difference in advances?( to not go over)
                    }

                    foreach (string skill in careerToAdd.CareerSkills)
                    {
                        int skillAdvances = advanceGen.Next(0, 6);
                        Skill currentSkill = skills.Find(sk => sk.Name == skill);

                        if (currentSkill == null)
                        {
                            if (skillAdvances > 0)
                                skills.Add(new Skill(skill, skillAdvances));
                        }
                        else if (currentSkill.Advances < skillAdvances)
                            currentSkill.Advance(skillAdvances);
                    }

                    int numTalents = advanceGen.Next(0, 2);

                    for (int i = 0; i < numTalents; i++)
                    {
                        //TODO: max
                        AddTalent(careerToAdd.CareerTalents[advanceGen.Next(0, careerToAdd.CareerTalents.Length)]);
                    }
                    break;

                case AdvanceLevel.Experienced:
                    foreach (Characteristics characteristic in careerToAdd.CareerCharacteristics)
                    {
                        advances = advanceGen.Next(3, 6);
                        if (characteristicAdvances[characteristic] < advances)
                            characteristicAdvances[characteristic] += advances; // difference in advances?( to not go over)
                    }

                    foreach (string skill in careerToAdd.CareerSkills)
                    {
                        int skillAdvances = advanceGen.Next(3, 6);
                        Skill currentSkill = skills.Find(sk => sk.Name == skill);

                        if (currentSkill == null)
                        {
                            if (skillAdvances > 0)
                                skills.Add(new Skill(skill, skillAdvances));
                        }
                        else if (currentSkill.Advances < skillAdvances)
                            currentSkill.Advance(skillAdvances);
                    }

                    numTalents = advanceGen.Next(0, 5);

                    for (int i = 0; i < numTalents; i++)
                    {
                        //TODO: max
                        AddTalent(careerToAdd.CareerTalents[advanceGen.Next(0, careerToAdd.CareerTalents.Length)]);
                    }
                    break;

                case AdvanceLevel.Complete:
                    foreach (Characteristics characteristic in careerToAdd.CareerCharacteristics)
                    {
                        if (characteristicAdvances[characteristic] < 5)
                            characteristicAdvances[characteristic] = 5; // difference in advances?( to not go over)
                    }

                    foreach (string skill in careerToAdd.CareerSkills)
                    {
                        Skill currentSkill = skills.Find(sk => sk.Name == skill);

                        if (currentSkill == null)
                            skills.Add(new Skill(skill, 5));
                        else if (currentSkill.Advances < 5)
                            currentSkill.Advance(5-currentSkill.Advances);
                    }

                    numTalents = advanceGen.Next(1, 5);

                    for (int i = 0; i < numTalents; i++)
                    {
                        //TODO: max
                        AddTalent(careerToAdd.CareerTalents[advanceGen.Next(0, careerToAdd.CareerTalents.Length)]);
                    }
                    break;
            }
            */
        }


        protected abstract void RollCharacteristics();
        protected abstract void AdvanceSpeciesSkills();
        protected abstract void AddSpeciesTalents();

    }

    public class Human : Character
    {
        public Human()
        {
            species = Species.Human;
            SpeciesStats.Movement.TryGetValue(species, out movement);
;
        }
        protected override void RollCharacteristics()
        {
            for (Characteristics i = 0; i < (Characteristics)10; i++)
            {
                //initialCharacteristics[i] = 30;
                initialCharacteristics[i] = 20 + rand.Next(1, 11) + rand.Next(1, 11);
            }
        }


        protected override void AdvanceSpeciesSkills()
        {

            // Only consider relevant ones: Cool, Melee (Basic), Ranged (Bow) (Reimplementation needed if all skills are considerede)
            // Randomly decide 5, 3, or 0

            // 0 = 0, 1 = 3, 2 = 5
            int adv = rand.Next(0, 3);

            // Cool
            switch (adv)
            {
                case 1:
                    AddSkill("Cool", 3);
                    break;
                case 2:
                    AddSkill("Cool", 5);
                    break;
            }

            adv = rand.Next(0, 3);
            // Cool
            switch (adv)
            {
                case 1:
                    AddSkill("Melee (Basic)", 3);
                    break;
                case 2:
                    AddSkill("Melee (Basic)", 5);
                    break;
            }

            adv = rand.Next(0, 3);
            // Cool
            switch (adv)
            {
                case 1:
                    AddSkill("Ranged (Bow)", 3);
                    break;
                case 2:
                    AddSkill("Ranged (Bow)", 5);
                    break;
            }
        }

        protected override void AddSpeciesTalents()
        {
            List<string[]> speciesTalentList = SpeciesStats.SpeciesTalents[Species.Human];

            for (int i = 0; i < speciesTalentList.Count - 1; i++)
            {
                AddTalent(speciesTalentList[i][rand.Next(0, speciesTalentList[i].Length)]);
            }

            int randomTalentCount = Int32.Parse(speciesTalentList[speciesTalentList.Count - 1][0]);

            for (int i = 0; i < randomTalentCount; i++)
                AddTalent(Talent.RollRandomTalent());
        }
    } 
}
