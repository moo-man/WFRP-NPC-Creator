using System;
using System.Collections.Generic;
using System.Linq;
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

        public Talent(string name)
        {
            Name = name;
            Advances = 1;
        }

        public void Advance()
        {
            Advances++;
        }

        public string TalentNameAndValue()
        {
            return Advances > 1 ? Name + " " + Advances : Name;
        }

    }
}
