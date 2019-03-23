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
            { Species.Human,  4 },
            {Species.Dwarf, 3 }
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
            }}
        };

    }
}
