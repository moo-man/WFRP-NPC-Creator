using System;
using System.Collections.Generic;
using System.Linq;

namespace WFRP_NPC_Creator
{
    public class Human : Character
    {

        public Human()
        {
            species = Species.Human;
            SpeciesStats.Movement.TryGetValue(species, out movement);
            RollCharacteristics();
            AdvanceSpeciesSkills();
            AddSpeciesTalents();
            
        }
        public override void RollCharacteristics()
        {
            for (Characteristics i = 0; i < (Characteristics)10; i++)
            {
                //initialCharacteristics[i] = 30;
                initialCharacteristics[i] = 20 + rand.Next(1, 11) + rand.Next(1, 11);
            }
        }
    } 
}
