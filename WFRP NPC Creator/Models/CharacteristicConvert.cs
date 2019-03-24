using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFRP_NPC_Creator
{
    public static class CharacteristicValueConverter
    {
        public static Dictionary<string, Characteristics> Convert = new Dictionary<string, Characteristics>
        {
            { "Weapon Skill" , Characteristics.WS },
            { "Ballistic Skill" , Characteristics.BS},
            { "Strength" , Characteristics.S  },
            { "Toughness" , Characteristics.T  },
            { "Initiative" , Characteristics.I },
            { "Agility" , Characteristics.Agi  },
            { "Dexterity" , Characteristics.Dex },
            { "Intelligence" , Characteristics.Int },
            { "Willpower" , Characteristics.WP },
            { "Fellowship" , Characteristics.Fel }
        };
    }
}
