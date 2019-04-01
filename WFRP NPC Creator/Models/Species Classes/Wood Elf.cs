using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFRP_NPC_Creator
{
    public class WoodElf : Character
    {
        public static string[] SpeciesSkills = {
            "Athletics",
            "Climb",
            "Endurance",
            "Entertain (Sing)",
            "Intimidate",
            "Language (Eltharin)",
            "Melee (Basic)",
            "Lore (Reikland)",
            "Lore (Perception)",
            "Sleight of Hand",
            "Stealth (Any)",
            "Trade (Cook)"
        };

        public WoodElf()
        {
            species = Species.Halfling;
            SpeciesStats.Movement.TryGetValue(species, out movement);
        }

        public override void AdvanceSpeciesSkills()
        {
            Skills = new List<Skill>();
            string[] skillListRandom = SpeciesSkills.OrderBy(x => rand.Next()).ToArray();
            int advanceNum = 0;
            for (int i = 0; i < 5; i++)
            {
                if (i < 3)
                    advanceNum = 5;
                else
                    advanceNum = 3;

                AddSkill(skillListRandom[i], advanceNum);
            }
        }

        public override void RollCharacteristics()
        {
            initialCharacteristics[Characteristics.WS] =    10 + rand.Next(1, 11) + rand.Next(1, 11);
            initialCharacteristics[Characteristics.BS] =    30 + rand.Next(1, 11) + rand.Next(1, 11);
            initialCharacteristics[Characteristics.S] =     10 + rand.Next(1, 11) + rand.Next(1, 11);
            initialCharacteristics[Characteristics.T] =     20 + rand.Next(1, 11) + rand.Next(1, 11);
            initialCharacteristics[Characteristics.I] =     20 + rand.Next(1, 11) + rand.Next(1, 11);
            initialCharacteristics[Characteristics.Agi] =   20 + rand.Next(1, 11) + rand.Next(1, 11);
            initialCharacteristics[Characteristics.Dex] =   30 + rand.Next(1, 11) + rand.Next(1, 11);
            initialCharacteristics[Characteristics.Int] =   20 + rand.Next(1, 11) + rand.Next(1, 11);
            initialCharacteristics[Characteristics.WP] =    30 + rand.Next(1, 11) + rand.Next(1, 11);
            initialCharacteristics[Characteristics.Fel] =   30 + rand.Next(1, 11) + rand.Next(1, 11);

        }

        public override void AddSpeciesTalents()
        {
            Talents = new List<Talent>();
            List<string[]> speciesTalentList = SpeciesStats.SpeciesTalents[species];

            for (int i = 0; i < speciesTalentList.Count - 1; i++)
            {
                AddTalent(speciesTalentList[i][rand.Next(0, speciesTalentList[i].Length)]);
            }

            int randomTalentCount = Int32.Parse(speciesTalentList[speciesTalentList.Count - 1][0]);

            for (int i = 0; i < randomTalentCount; i++)
                AddTalent(Talent.RollRandomTalent());
        }
    }
}
