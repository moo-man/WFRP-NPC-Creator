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
                new string[] {"Read/Write"}
            }},
            {Species.Welf, new List<string[]>
            {
                new string[] {"Acute Sense (Sight)"},
                new string[] {"Hardy", "Second Sight"},
                new string[] {"Night Vision"},
                new string[] {"Read/Write", "Very Resilient"},
                new string[] {"Rover"}
            }}
        };

    }
}
