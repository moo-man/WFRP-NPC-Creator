using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFRP_NPC_Creator
{
    public class Halfling : Character
    {

        public Halfling()
        {
            species = Species.Halfling;
            SpeciesStats.Movement.TryGetValue(species, out BaseMovement);
            RollCharacteristics();
            AdvanceSpeciesSkills();
            AddSpeciesTalents();
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

        public override void TakeAverageCharacteristics()
        {
            initialCharacteristics[Characteristics.WS] =    10 + 10 ;
            initialCharacteristics[Characteristics.BS] =    30 + 10 ;
            initialCharacteristics[Characteristics.S] =     10 + 10 ;
            initialCharacteristics[Characteristics.T] =     20 + 10 ;
            initialCharacteristics[Characteristics.I] =     20 + 10 ;
            initialCharacteristics[Characteristics.Agi] =   20 + 10 ;
            initialCharacteristics[Characteristics.Dex] =   30 + 10 ;
            initialCharacteristics[Characteristics.Int] =   20 + 10 ;
            initialCharacteristics[Characteristics.WP] =    30 + 10 ;
            initialCharacteristics[Characteristics.Fel] =   30 + 10 ;
        }
    }
}
