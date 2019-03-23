using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFRP_NPC_Creator
{
    public class Dwarf : Character
    {
        public static string[] SpeciesSkills = {
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
        };

        public Dwarf()
        {
            species = Species.Dwarf;
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
            initialCharacteristics[Characteristics.WS] =    30 + rand.Next(1, 11) + rand.Next(1, 11);
            initialCharacteristics[Characteristics.BS] =    20 + rand.Next(1, 11) + rand.Next(1, 11);
            initialCharacteristics[Characteristics.S] =     20 + rand.Next(1, 11) + rand.Next(1, 11);
            initialCharacteristics[Characteristics.T] =     30 + rand.Next(1, 11) + rand.Next(1, 11);
            initialCharacteristics[Characteristics.I] =     20 + rand.Next(1, 11) + rand.Next(1, 11);
            initialCharacteristics[Characteristics.Agi] =   10 + rand.Next(1, 11) + rand.Next(1, 11);
            initialCharacteristics[Characteristics.Dex] =   30 + rand.Next(1, 11) + rand.Next(1, 11);
            initialCharacteristics[Characteristics.Int] =   20 + rand.Next(1, 11) + rand.Next(1, 11);
            initialCharacteristics[Characteristics.WP] =    40 + rand.Next(1, 11) + rand.Next(1, 11);
            initialCharacteristics[Characteristics.Fel] =   10 + rand.Next(1, 11) + rand.Next(1, 11);

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
