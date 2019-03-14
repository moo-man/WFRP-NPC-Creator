using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFRP_NPC_Creator
{
    static class CareerFocusLookup
    {
        public static Dictionary<string, string[]> ShouldFocus = new Dictionary<string, string[]>()
        {
            {"Apprentice Wizard" , new string[] {"Language (Magick)", "Channeling (Any Color)", "Melee (Basic)"} },
            {"Wizard" , new string[] {"Language (Magick)", "Channeling (Any Color)", "Melee (Basic)", "Cool"} },
            {"Master Wizard" , new string[] {"Language (Magick)", "Channeling (Any Color)", "Ride (Horse)", "Cool"} },
            {"Wizard Lord" , new string[] {"Language (Magick)", "Channeling (Any Color)", "Cool"} },

            {"Physician's Apprentice" , new string[] {"Heal", "Sleight of Hand"} },
            {"Physician" , new string[] {"Heal", "Sleight of Hand"} },
            {"Doktor" , new string[] {"Heal", "Sleight of Hand"} },
            {"Court Physician" , new string[] {"Heal", "Sleight of Hand"} },

            {"Initiate" , new string[] {"Pray", "Endurance", "Intuition"} },
            {"Priest" , new string[] {"Pray", "Endurance", "Heal", "Melee (Basic)", "Intuition"} },
            {"High Priest" , new string[] {"Pray", "Endurance", "Heal", "Melee (Basic)", "Intuition"} },
            {"Lector" , new string[] {"Pray", "Endurance", "Heal", "Melee (Basic)", "Intuition"} },

            {"Squire" , new string[] {"Melee (Cavalry)", "Heal", "Ride (Horse)", "Athletics"} },
            {"Knight" , new string[] {"Melee (Cavalry)", "Heal", "Ride (Horse)", "Athletics", "Melee (Any)"} },
            {"First Knight" , new string[] {"Melee (Cavalry)", "Heal", "Ride (Horse)", "Athletics", "Melee (Any)"} },
            {"Knight of the Inner Circle" , new string[] {"Melee (Cavalry)", "Heal", "Ride (Horse)", "Athletics", "Melee (Any)"} },

            {"Novitiate" , new string[] {"Pray", "Endurance", "Cool", "Melee (Any)", "Dodge"} },
            {"Warrior Priest" , new string[] {"Pray", "Endurance", "Cool", "Melee (Any)", "Dodge"} },
            {"Priest Sergeant" , new string[] {"Pray", "Endurance", "Cool", "Melee (Any)", "Dodge"} },
            {"Priest Captain" , new string[] {"Pray", "Endurance", "Cool", "Melee (Any)", "Dodge"} },

            {"Interrogator", new string[]      {"Heal", "Melee (Brawling)","Perception", "Intimidate", "Intuition"} },
            {"Witch Hunter", new string[] {"Heal", "Melee (Basic)", "Ranged (Any)","Perception", "Intimidate", "Intuition"} },
            {"Inquisitor", new string[]{"Heal", "Melee (Basic)", "Ranged (Any)","Perception", "Intimidate", "Intuition"} },
            {"Witchfinder General", new string[] {"Heal", "Melee (Basic)", "Ranged (Any)","Perception", "Intimidate", "Intuition"} },


        };
    }
}
