using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFRP_NPC_Creator
{
    public static class Settings
    {
        public static bool ShowOnlyRelevantSkills { get; set; }
        public static bool ShowOnlyRelevantTalents { get; set; }
        public static bool UseAverageSpeciesCharacteristics { get; set; }

        static Settings()
        {
            ShowOnlyRelevantSkills = true;
            ShowOnlyRelevantTalents = true;
            UseAverageSpeciesCharacteristics = false;
        }
    }
}
