using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFRP_NPC_Creator
{
    public class Skill
    {
        static readonly Dictionary<string, Characteristics> SkillCharacteristic = new Dictionary<string, Characteristics>()
        {
            { "Animal Care" , Characteristics.Int },
            { "Animal Training" , Characteristics.Int },
            { "Art" , Characteristics.Dex },
            { "Athletics" , Characteristics.Agi },
            { "Bribery" , Characteristics.Fel },
            { "Channeling" , Characteristics.WP },
            { "Charm" , Characteristics.Fel },
            { "Charm Animal" , Characteristics.WP },
            { "Climb" , Characteristics.S },
            { "Consume Alcohol" , Characteristics.T },
            { "Cool" , Characteristics.WP },
            { "Dodge" , Characteristics.Agi },
            { "Drive" , Characteristics.Agi },
            { "Endurance" , Characteristics.T },
            { "Entertain" , Characteristics.Fel },
            { "Evaluate" , Characteristics.Int },
            { "Gamble" , Characteristics.Int },
            { "Gossip" , Characteristics.Fel },
            { "Haggle" , Characteristics.Fel },
            { "Heal" , Characteristics.Int },
            { "Intimidate" , Characteristics.S },
            { "Intuition" , Characteristics.I },
            { "Language" , Characteristics.Int },
            { "Leadership" , Characteristics.Fel },
            { "Lore" , Characteristics.Int },
            { "Melee" , Characteristics.WS },
            { "Navigation" , Characteristics.I },
            { "Outdoor Survival" , Characteristics.Int },
            { "Perception" , Characteristics.I },
            { "Perform" , Characteristics.Agi },
            { "Pick Lock" , Characteristics.Dex },
            { "Play" , Characteristics.Dex },
            { "Pray" , Characteristics.Fel },
            { "Ranged" , Characteristics.BS },
            { "Research" , Characteristics.Int },
            { "Ride" , Characteristics.Agi },
            { "Row" , Characteristics.S },
            { "Sail" , Characteristics.Agi },
            { "Secret Signs" , Characteristics.Int },
            { "Set Trap" , Characteristics.Dex },
            { "Sleight of Hand" , Characteristics.Dex },
            { "Stealth" , Characteristics.Agi },
            { "Swim" , Characteristics.S },
            { "Track" , Characteristics.I },
            { "Trade" , Characteristics.Dex }
        };

        // Relevant to print
        static readonly Dictionary<string, bool> Relevance = new Dictionary<string, bool>()
        {
            { "Animal Care" , false},
            { "Animal Training" , false },
            { "Art" , false },
            { "Athletics" , true },
            { "Bribery" , false },
            { "Channeling" , true },
            { "Charm" , false },
            { "Charm Animal" , false },
            { "Climb" , true },
            { "Consume Alcohol" , false },
            { "Cool" , true },
            { "Dodge" , true },
            { "Drive" , true },
            { "Endurance" , true },
            { "Entertain" , false },
            { "Evaluate" , true },
            { "Gamble" ,  true },
            { "Gossip" , false },
            { "Haggle" , true },
            { "Heal" ,  true },
            { "Intimidate" , true },
            { "Intuition" , true },
            { "Language (Magick)" , true},
            { "Language" , false },
            { "Leadership" , false },
            { "Lore" , false },
            { "Melee" , true },
            { "Navigation" , true },
            { "Outdoor Survival" , true },
            { "Perception" , true },
            { "Perform" , false },
            { "Pick Lock" , true },
            { "Play" , false},
            { "Pray" , true},
            { "Ranged" , true },
            { "Research" , false },
            { "Ride" , true },
            { "Row" , false },
            { "Sail" , false },
            { "Secret Signs" , false },
            { "Set Trap" , false },
            { "Sleight of Hand" , true },
            { "Stealth" , true },
            { "Swim" , true },
            { "Track" , false },
            { "Trade" , false }
        };

        public string Name { get; private set; }

        public int Advances { get; private set; }

        public Characteristics BaseCharacteristic { get; private set; }

        public Skill(string skillName, int skillAdvs)
        {
            Name = skillName;
            Advances = skillAdvs;

            Characteristics baseChar;
            SkillCharacteristic.TryGetValue(GenericName(), out baseChar);
            BaseCharacteristic = baseChar;
        }

        public void Advance(int num)
        {
            Advances += num;
        }


        /// <summary>
        /// Remove all specialization - just the skill name
        /// </summary>
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

        public static Characteristics CharacteristicLookUp(string skillName)
        {
            return SkillCharacteristic[GenericName(skillName)];
        }

        public bool IsRelevant()
        {
            return IsRelevant(Name);
        }

        public static bool IsRelevant(string skillName)
        {
            bool relevant;
            if (Relevance.TryGetValue(skillName, out relevant))
                return relevant;

            else // if specific name did not get value, use genericname
                return Relevance[GenericName(skillName)];
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(string))
                return obj.ToString() == Name;
            else if (obj is Skill)
                return Name == (obj as Skill).Name;
            else
                return base.Equals(obj);
        }
    }

    class SkillEqualityComparer : IEqualityComparer<Skill>
    {
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public bool Equals(Skill x, Skill y)
        {
            return x.Name == y.Name;
        }

        public int GetHashCode(Skill obj)
        {
            return obj.Name.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
