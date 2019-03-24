using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFRP_NPC_Creator
{
    public static class Utility
    {
        public static Dictionary<Species, string> DisplaySpeciesEnum = new Dictionary<Species, string>()
        {
            {Species.Human , "Human" },
            {Species.Dwarf , "Dwarf" },
            {Species.Halfling , "Halfling" },
            {Species.Helf , "High Elf" },
            {Species.Welf , "Wood Elf" }
        };
    }
}
