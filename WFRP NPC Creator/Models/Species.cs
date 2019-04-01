using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFRP_NPC_Creator
{
    static class SpeciesStats
    {
        public static readonly Dictionary<Species, int> Movement = new Dictionary<Species, int>()
        {
            {Species.Human, 4},
            {Species.Dwarf, 3},
            {Species.Halfling, 3},
            {Species.Helf, 5},
            {Species.Welf, 5}
        };

        public static readonly Dictionary<Species, string[]> SpeciesSkills = new Dictionary<Species, string[]>()
        {
            {Species.Human, new string[]
            {
                "Animal Care",
                "Charm",
                "Cool",
                "Evaluate",
                "Gossip",
                "Haggle",
                "Language (Bretonnian)",
                "Language (Wastelander)",
                "Leadership",
                "Lore (Reikland)",
                "Melee (Basic)",
                "Ranged (Bow)"
            }},

            { Species.Dwarf, new string[]
            {
                "Consume Alcohol",
                "Cool",
                "Endurance",
                "Entertain (Storytelling)",
                "Evaluate",
                "Intimidate",
                "Language (Khazalid)",
                "Lore (Dwarfs)",
                "Lore (Geology)",
                "Lore (Metallurgy)",
                "Melee (Basic)",
                "Trade (any one)"
            }},
            { Species.Halfling, new string[]
            {
                "Charm",
                "Consume Alcohol",
                "Dodge",
                "Gamble",
                "Haggle",
                "Intuition",
                "Language (Mootish)",
                "Lore (Reikland)",
                "Lore (Perception)",
                "Sleight of Hand",
                "Stealth (Any)",
                "Trade (Cook)"
            }},
            { Species.Helf, new string[]
            {
                "Cool",
                "Entertain (Sing)",
                "Evaluate",
                "Language (Eltharin)",
                "Leadership",
                "Melee (Basic)",
                "Navigation",
                "Perception",
                "Play (any one)",
                "Ranged (Bow)",
                "Sail",
                "Swim"
            }},
            { Species.Welf, new string[]
            {
                "Athletics",
                "Climb",
                "Endurance",
                "Entertain (Sing)",
                "Intimidate",
                "Language (Eltharin)",
                "Melee (Basic)",
                "Outdoor Survival",
                "Perception",
                "Ranged (Bow)",
                "Stealth (Rural)",
                "Track"
            }}
        };



        public static readonly Dictionary<Species, List<string[]>> SpeciesTalents = new Dictionary<Species, List<string[]>>()
        {
            {Species.Human, new List<string[]>
            {
                new string[] {"Doomed"},
                new string[] {"Savvy", "Suave"},
                new string[] {"3"}
            }},

            {Species.Dwarf, new List<string[]>
            {
                new string[] {"Magic Resistance"},
                new string[] {"Night Vision"},
                new string[] {"Read/Write", "Relentless"},
                new string[] {"Resolute", "Strong-minded"},
                new string[] {"Sturdy"},
                new string[] {"0"}
            }},
            { Species.Halfling, new List<string[]>
            {
                new string[] {"Acute Sense (Taste)"},
                new string[] {"Night Vision"},
                new string[] {"Resistance (Chaos)"},
                new string[] {"Small"},
                new string[] {"2"}
            }},
            { Species.Helf, new List<string[]>
            {
                new string[] {"Acute Sense (Sight)"},
                new string[] {"Coolheaded", "Savvy"},
                new string[] {"Night Vision"},
                new string[] {"Second Sight", "Sixth Sense"},
                new string[] {"Read/Write"},
                new string[] {"0"}

            }},
            {Species.Welf, new List<string[]>
            {
                new string[] {"Acute Sense (Sight)"},
                new string[] {"Hardy", "Second Sight"},
                new string[] {"Night Vision"},
                new string[] {"Read/Write", "Very Resilient"},
                new string[] {"Rover"},
                new string[] {"0"}
            }}
        };

    }
}
