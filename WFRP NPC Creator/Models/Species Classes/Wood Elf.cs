using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFRP_NPC_Creator
{
    public class WoodElf : Character
    {

        public WoodElf()
        {
            species = Species.Welf;
            SpeciesStats.Movement.TryGetValue(species, out BaseMovement);
            RollCharacteristics();
            AdvanceSpeciesSkills();
            AddSpeciesTalents();
        }

        public override void RollCharacteristics()
        {
            initialCharacteristics[Characteristics.WS] =    30 + rand.Next(1, 11) + rand.Next(1, 11);
            initialCharacteristics[Characteristics.BS] =    30 + rand.Next(1, 11) + rand.Next(1, 11);
            initialCharacteristics[Characteristics.S] =     20 + rand.Next(1, 11) + rand.Next(1, 11);
            initialCharacteristics[Characteristics.T] =     20 + rand.Next(1, 11) + rand.Next(1, 11);
            initialCharacteristics[Characteristics.I] =     40 + rand.Next(1, 11) + rand.Next(1, 11);
            initialCharacteristics[Characteristics.Agi] =   30 + rand.Next(1, 11) + rand.Next(1, 11);
            initialCharacteristics[Characteristics.Dex] =   30 + rand.Next(1, 11) + rand.Next(1, 11);
            initialCharacteristics[Characteristics.Int] =   30 + rand.Next(1, 11) + rand.Next(1, 11);
            initialCharacteristics[Characteristics.WP] =    30 + rand.Next(1, 11) + rand.Next(1, 11);
            initialCharacteristics[Characteristics.Fel] =   20 + rand.Next(1, 11) + rand.Next(1, 11);

        }

        public override void TakeAverageCharacteristics()
        {
            initialCharacteristics[Characteristics.WS] = 30 + 10;
            initialCharacteristics[Characteristics.BS] = 30 + 10;
            initialCharacteristics[Characteristics.S] = 20 + 10;
            initialCharacteristics[Characteristics.T] = 20 + 10;
            initialCharacteristics[Characteristics.I] = 40 + 10;
            initialCharacteristics[Characteristics.Agi] = 30 + 10;
            initialCharacteristics[Characteristics.Dex] = 30 + 10;
            initialCharacteristics[Characteristics.Int] = 30 + 10;
            initialCharacteristics[Characteristics.WP] = 30 + 10;
            initialCharacteristics[Characteristics.Fel] = 20 + 10;
        }

    }
}
