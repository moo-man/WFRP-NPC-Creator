using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WFRP_NPC_Creator
{
    public class Talent
    {
        public static readonly Dictionary<string, Tuple<Characteristics, int>> TalentBonus = new Dictionary<string, Tuple<Characteristics, int>>()
        {
            {"Savvy", new Tuple<Characteristics, int>(Characteristics.Int, 5) },
            {"Suave", new Tuple<Characteristics, int>(Characteristics.Fel, 5) },
            {"Warrior Born", new Tuple<Characteristics, int>(Characteristics.WS, 5) },
            {"Marksman", new Tuple<Characteristics, int>(Characteristics.BS, 5) },
            {"Very Strong", new Tuple<Characteristics, int>(Characteristics.S, 5) },
            {"Sharp", new Tuple<Characteristics, int>(Characteristics.I, 5) },
            {"Lightning Reflexes", new Tuple<Characteristics, int>(Characteristics.Agi, 5) },
            {"Coolheaded", new Tuple<Characteristics, int>(Characteristics.WP, 5) },
            {"Very Resilient", new Tuple<Characteristics, int>(Characteristics.T, 5) },
            {"Nimble Fingered", new Tuple<Characteristics, int>(Characteristics.T, 5) }
        };
        public static List<TalentInfo> TalentList = new List<TalentInfo>();

        public static readonly string[] RandomTalents = new string[] {
            "Acute Sense (any one)",
            "Ambidextrous",
            "Animal Affinity",
            "Artistic",
            "Attractive",
            "Coolheaded",
            "Craftsman (any one)",
            "Flee!",
            "Hardy",
            "Lightning Reflexes",
            "Linguistics",
            "Luck",
            "Marksman",
            "Mimic",
            "Night Vision",
            "Nimble Fingered",
            "Noble Blood",
            "Orientation",
            "Perfect Pitch",
            "Pure Soul",
            "Read/Write",
            "Resistance (any one)",
            "Savvy",
            "Sharp",
            "Sixth Sense",
            "Strong Legs",
            "Sturdy",
            "Suave",
            "Super Numerate",
            "Very Resilient",
            "Very Strong",
            "Warrior Born"
        };
        public static Random randTalent = new Random();

        public static string RollRandomTalent()
        {
            return RandomTalents[randTalent.Next(0, RandomTalents.Length)];
        }

        public string Name { get; private set; }
        public int Advances { get; private set; }
        public Character Owner { get; private set; }
        public TalentInfo Info { get; private set; }

        public Talent(string name, Character owner)
        {
            Name = name;
            Advances = 1;
            Owner = owner;
            Info = TalentList.Find(t => t.Name == GenericName());
        }

        public bool Advance()
        {
            if (Advances >= Max())
                return false;
            else
            {
                Advances++;
                return true;
            }
        }

        public int Max()
        {
            return Info.IntMax(Owner.CharacteristicValues());
        }

        public bool IsRelevant()
        {
            return Info.isRelevant;
        }

        public static bool IsRelevant(string talentName)
        {
            return TalentList.Find(x => x.Name == GenericName(talentName)).isRelevant;
        }

        public static string GenericName(string inputName)
        {
            int parenIndex = inputName.IndexOf('(');

            if (parenIndex == -1)
            {
                return inputName;
            }
            else
            {
                return inputName.Remove(parenIndex - 1);
            }
        }

        public string GenericName()
        {
            return GenericName(Name);
        }

    }

    public class TalentInfo
    {
        public string Name { get; private set; }
        public string Max { get; private set; }
        public string Tests { get; private set; }
        public bool isRelevant { get; private set; }


        public TalentInfo(string name, string max, string tests, bool relevance)
        {
            Name = name;
            Max = max;
            Tests = tests;
            isRelevant = relevance;
        }

        public int IntMax(Dictionary<Characteristics, int> characteristics)
        {
            int intMax;
            if (Int32.TryParse(Max, out intMax))
                return intMax;
            else
            {
                if (Max.Contains("Bonus"))
                {
                    intMax = Convert.ToInt32(Math.Floor(characteristics[CharacteristicValueConverter.Convert[Max.Remove(Max.IndexOf(" Bonus"))]]/10.0));
                }
            }
            return intMax;
        }
    }

    static class TalentReader
    {
        public static bool read = false;
        static TalentReader()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            StreamReader sr = new StreamReader(assembly.GetManifestResourceStream("WFRP_NPC_Creator.Data.TalentData.txt"));
            string[] talentData = sr.ReadToEnd().Split('\n');
            string max, name, tests;
            bool relevance;
            for (int i = 0; i < talentData.Length; i++)
            {
                if (talentData[i].Length > 4 && talentData[i].Substring(0, 4) == "Max:")
                {
                    max = talentData[i].Substring(5).Trim();
                    name = talentData[i - 5].Trim();
                    relevance = talentData[i - 4].Trim() == "1";
                    if (talentData[i+1].Length > 6 && talentData[i + 1].Substring(0, 6) == "Tests:")
                        tests = talentData[i + 1].Substring(7).Trim();
                    else
                        tests = "";
                    Talent.TalentList.Add(new TalentInfo(Talent.GenericName(name), max, tests, relevance));
                }
            }
            sr.Close();

        }
    }
    class TalentEqualityComparer : IEqualityComparer<Talent>
    {
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public bool Equals(Talent x, Talent y)
        {
            return x.Name == y.Name;
        }

        public int GetHashCode(Talent obj)
        {
            return obj.Name.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
