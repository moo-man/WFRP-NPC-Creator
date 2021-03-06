﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFRP_NPC_Creator
{
    public class Dwarf : Character
    {

        public Dwarf(bool average)
        {
            species = Species.Dwarf;
            SpeciesStats.Movement.TryGetValue(species, out BaseMovement);
            if (average)
                TakeAverageCharacteristics();
            else
                RollCharacteristics();
            AdvanceSpeciesSkills();
            AddSpeciesTalents();
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

        public override void TakeAverageCharacteristics()
        {
            initialCharacteristics[Characteristics.WS] = 30 + 10;
            initialCharacteristics[Characteristics.BS] = 20 + 10;
            initialCharacteristics[Characteristics.S] = 20 + 10;
            initialCharacteristics[Characteristics.T] = 30 + 10;
            initialCharacteristics[Characteristics.I] = 20 + 10;
            initialCharacteristics[Characteristics.Agi] = 10 + 10;
            initialCharacteristics[Characteristics.Dex] = 30 + 10;
            initialCharacteristics[Characteristics.Int] = 20 + 10;
            initialCharacteristics[Characteristics.WP] = 40 + 10;
            initialCharacteristics[Characteristics.Fel] = 10 + 10;
        }                                                  
    }
}
