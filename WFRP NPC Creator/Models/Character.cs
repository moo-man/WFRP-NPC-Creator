using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFRP_NPC_Creator
{
    public abstract class Character
    {
        //protected static string[] SpeciesSkills;
        protected Dictionary<Characteristics, int> initialCharacteristics { get; private set; } = new Dictionary<Characteristics, int>();
        protected Species species;
        protected int movement;
        protected int wounds;

        public string Name { get; set; }

        public List<CareerAdvancement> Careers { get; protected set; } = new List<CareerAdvancement>();
        public List<Skill> Skills { get; protected set; } = new List<Skill>();
        public List<Talent> Talents { get; protected set; } = new List<Talent>();

        public static Random rand = new Random();
        public Character()
        {
            for (Characteristics i = 0; i < (Characteristics)10; i++)
                initialCharacteristics.Add(i, 0);

        }

        public void ChangeCareerAdvancement(int careerIndex, AdvanceLevel newAdvLevel)
        {
            Careers[careerIndex].ChangeAdvancement(newAdvLevel);
        }

        public void RerollCareerCharacteristics(int careerIndex)
        {
            Careers[careerIndex].RerollCharacteristics();
        }
        public void RerollCareerSkills(int careerIndex)
        {
            Careers[careerIndex].RerollSkills();
        }
        public void RerollCareerTalents(int careerIndex)
        {
            Careers[careerIndex].RerollTalents();
        }

        public void PrintToConsole(bool onlyRelevant)
        {
            Console.WriteLine("WS\tBS\tS\tT\tI\tAgi\tDex\tInt\tWP\tFel\n");
            for (Characteristics i = 0; i < (Characteristics)10; i++)
            {
                Console.Write(CharacteristicValue(i) + "\t");
            }
            Console.WriteLine("\n______________________________________");
            foreach (string sk in GetAllSkills())
            {
                if (!(onlyRelevant && !Skill.IsRelevant(sk))) // Print always if relevancy doesn't matter, but only print if relevant and relevancy does matter
                    System.Diagnostics.Debug.Write(SkillNameAndValue(sk) + ", ");


            }

            System.Diagnostics.Debug.Write('\n');
            foreach (string talent in GetAllTalents())
            {
                if (!(onlyRelevant && !Talent.IsRelevant(talent))) // Print always if relevancy doesn't matter, but only print if relevant and relevancy does matter
                    System.Diagnostics.Debug.Write(TalentNameAndAdvances(talent) + ", ");


            }
            Console.WriteLine();
        }



        public string SkillsString(bool onlyRelevant)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string sk in GetAllSkills())
            {
                if (!(onlyRelevant && !Skill.IsRelevant(sk))) // Print always if relevancy doesn't matter, but only print if relevant and relevancy does matter
                    sb.Append(SkillNameAndValue(sk) + ", ");
            }
            return sb.Length > 0 ? sb.ToString().Remove(sb.Length - 2, 2) : sb.ToString();

        }


        public string TalentsString(bool onlyRelevant)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string talent in GetAllTalents())
            {
                if (!(onlyRelevant && !Talent.IsRelevant(talent))) // Print always if relevancy doesn't matter, but only print if relevant and relevancy does matter
                    sb.Append(TalentNameAndAdvances(talent) + ", ");
            }

            return sb.Length > 0 ? sb.ToString().Remove(sb.Length - 2, 2) : sb.ToString();

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

            foreach (string t in GetAllTalents())
            {
                if (Talent.TalentBonus.Keys.Contains(t))
                    values[Talent.TalentBonus[t].Item1] += Talent.TalentBonus[t].Item2;
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

        #region Skills
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

        /// <summary>
        /// Gets the advancement as if the input career was the latest career
        /// This is necessary to accurately reroll earlier careers 
        /// </summary>
        /// <param name="career"></param>
        /// <returns></returns>
        public int TotalSkillAdvancesToCareer(string skillName, CareerAdvancement career)
        {
            int totalValue = 0;
            Skill characterSkill = Skills.Find(sk => sk.Name == skillName);

            if (characterSkill != null)
                totalValue += characterSkill.Advances;

            foreach (CareerAdvancement beforeCareer in Careers)
            {
                if (beforeCareer == career)
                    return totalValue;
                Skill skillAdvanced = career.SkillsAdvanced.Find(sk => sk.Name == skillName);
                if (skillAdvanced != null)
                    totalValue += skillAdvanced.Advances;
            }

            return totalValue;
        }

        /// <summary>
        /// Similar to TotalSkillAdvancesToCareer(), this is used when rerolling characteristic advances of earlier careers
        /// This adds all the characteristic advances of the input characteristic, starting at the first career, and ending before the input career
        /// </summary>
        /// <param name="c"></param>
        /// <param name="career"></param>
        /// <returns></returns>
        public int TotalCharacteristicAdvancesToCareer(Characteristics c, CareerAdvancement career)
        {
            int cAdvances = 0;

            foreach (CareerAdvancement beforeCareer in Careers)
            {
                if (beforeCareer == career)
                    return cAdvances;
                cAdvances += beforeCareer.CharacteristicAdvances[c];
            }

            return cAdvances;
        }

        public string[] GetAllSkills()
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
            return SkillList.Select(sk => sk.Name).ToArray();
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
            return SkillNameAndValue(skill.Name);
        }

        public string SkillNameAndValue(string skillName)
        {
            return skillName + " " + SkillValue(skillName);
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
        #endregion


        #region Talents
        public string[] GetAllTalents()
        {
            List<Talent> TalentList = Talents;
            foreach (CareerAdvancement career in Careers)
            {
                TalentList = TalentList.Concat(career.TalentsAdvanced).ToList();
            }
            TalentList = TalentList.Distinct(new TalentEqualityComparer()).ToList();

            TalentList.Sort(delegate (Talent a, Talent b)
            {
                return a.Name.CompareTo(b.Name);
            });
            return TalentList.Select(t => t.Name).ToArray();
        }

        public int TotalTalentAdvances(Talent t)
        {
            return TotalTalentAdvances(t.Name);
        }

        public int TotalTalentAdvances(string tName)
        {
            int adv = 0;
            Talent charTalent = Talents.Find(t => t.Name == tName);
            if (charTalent != null)
                adv = charTalent.Advances;

            foreach (CareerAdvancement career in Careers)
            {
                Talent careerTalent = career.TalentsAdvanced.Find(t => t.Name == tName);
                if (careerTalent != null)
                    adv += careerTalent.Advances;
            }

            return adv;
        }

        public string TalentNameAndAdvances(string t)
        {
            try
            {
                int adv = TotalTalentAdvances(t);
                return adv > 1 ? t + " " + TotalTalentAdvances(t) : t;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return "";
            }
        }

        protected virtual void AddTalent(string talentName)
        {
            Talent talentAcquired = Talents.Find(x => x.Name == talentName);

            if (talentAcquired != null)
                talentAcquired.Advance();
            else
                Talents.Add(new Talent(talentName, this));
        }
        #endregion


        public virtual void AddCareer(string name, AdvanceLevel advancement = AdvanceLevel.None)
        {
            Careers.Add(new CareerAdvancement(this, Career.GetCareerList().Find(c => c.Name == name), advancement));
        }


        public bool Validate()
        {
            int advCount;

            foreach (CareerAdvancement career in Careers)
            {
                advCount = 0;
                foreach (string sk in career.CareerTemplate.CareerSkills)
                {
                    if (TotalSkillAdvances(sk) >= 5 * career.CareerTemplate.Tier)
                        advCount++;
                }
                if (advCount < 8)
                {
                    System.Diagnostics.Debug.Print(career.CareerTemplate.Name + " Invalid");
                    return false;
                }
            }
            return true;

        }

        public abstract void RollCharacteristics();
        public virtual void AdvanceSpeciesSkills()
        {
            Skills = new List<Skill>();
            string[] skillListRandom = SpeciesStats.SpeciesSkills[species].OrderBy(x => rand.Next()).ToArray();
            int advanceNum = 0;
            for (int i = 0; i < 5; i++)
            {
                if (i < 3)
                    advanceNum = 5;
                else
                    advanceNum = 3;

                AddSkill(skillListRandom[i], advanceNum);
            }
        }

        public virtual void AddSpeciesTalents()
        {
            Talents = new List<Talent>();
            List<string[]> speciesTalentList = SpeciesStats.SpeciesTalents[species];

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
