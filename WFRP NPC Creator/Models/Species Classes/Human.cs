using System;
using System.Collections.Generic;
using System.Linq;

namespace WFRP_NPC_Creator
{
    public class Human : Character
    {
        public static string[] SpeciesSkills = {
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
            "Ranged (Bow)" };

        public Human()
        {
            species = Species.Human;
            SpeciesStats.Movement.TryGetValue(species, out movement);
;
        }
        public override void RollCharacteristics()
        {
            for (Characteristics i = 0; i < (Characteristics)10; i++)
            {
                //initialCharacteristics[i] = 30;
                initialCharacteristics[i] = 20 + rand.Next(1, 11) + rand.Next(1, 11);
            }
        }


        public  override void AdvanceSpeciesSkills()
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
