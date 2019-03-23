using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFRP_NPC_Creator
{
    public class WindowViewModel : BaseViewModel
    {
        public TreeViewModel Tree { get; set; } = new TreeViewModel();
        public DataGridViewModel DataGrid { get; set; } = new DataGridViewModel();

        public RichTextViewModel RichText { get; set; } = new RichTextViewModel();
        public Character NPC;

        public WindowViewModel()
        {
            DataGrid.CareerChanged += CareerChange;
            DataGrid.SpeciesChanged += SpeciesChange;
            NPC = new Human();
            UpdateStatBlock();
        }

        public void AddGridRow(string careerName)
        {
            DataGrid.AddRow(careerName);
            NPC.AddCareer(careerName);
            UpdateStatBlock();
        }

        public void CareerChange(object source, EventArgs e)
        {
            CareerChangedEventArgs careerArgs = (CareerChangedEventArgs)e;
            switch (careerArgs.change)
            {
                case RowAction.SelectionChange:
                    NPC.ChangeCareerAdvancement(careerArgs.careerIndex, careerArgs.advLevel);
                    break;
                case RowAction.RerollCharacteristic:
                    NPC.RerollCareerCharacteristics(careerArgs.careerIndex);
                    break;
                case RowAction.RerollSkill:
                    NPC.RerollCareerSkills(careerArgs.careerIndex);
                    break;
                case RowAction.RerollTalent:
                    NPC.RerollCareerTalents(careerArgs.careerIndex);
                    break;
            }
            UpdateStatBlock();
        }

        public void SpeciesChange(object source, EventArgs e)
        {
            SpeciesChangedEventArgs speciesArgs = (SpeciesChangedEventArgs)e;
            NPC.Name = speciesArgs.Name;
            if (speciesArgs.change == RowAction.SelectionChange)
            {
                Character newNPC;
                switch (speciesArgs.newSpecies)
                {
                    case Species.Human:
                        newNPC = new Human();
                        break;
                     case Species.Dwarf:
                         newNPC = new Dwarf();
                         break;
                    /* case Species.Halfling:
                         newNPC = new Halfling();
                         break;
                     case Species.Welf:
                         newNPC = new WoodElf();
                         break;
                     case Species.Helf:
                         newNPC = new HighElf();
                         break;*/
                    default:
                        return;
                }
                newNPC.Name = NPC.Name;
                foreach (CareerAdvancement careerAdv in NPC.Careers)
                    newNPC.AddCareer(careerAdv.CareerTemplate.Name, careerAdv.Advancement);
                NPC = newNPC;
            }

            else
            {
                switch (speciesArgs.change)
                {
                    case RowAction.RerollCharacteristic:
                        NPC.RollCharacteristics();
                        break;
                    case RowAction.RerollSkill:
                        NPC.AdvanceSpeciesSkills();
                        break;
                    case RowAction.RerollTalent:
                        NPC.AddSpeciesTalents();
                        break;
                    default:
                        break;
                }
            }

            UpdateStatBlock();
        }

        private void UpdateStatBlock()
        {
            int[] tableArray = new int[12];
            for (Characteristics i = 0; i < (Characteristics)10; i++)
                tableArray[(int)i+1] = NPC.CharacteristicValue(i);

            RichText.UpdateTableValues(tableArray);
            RichText.UpdateSkills(NPC.SkillsString(true));
            RichText.UpdateTalents(NPC.TalentsString(true));
            RichText.UpdateName(NPC.Name);
            NPC.Validate();
        }
    }


    public class RowChangeEventArgs : EventArgs
    {
        public RowType rType { get; set; }
        public RowAction ChangeType { get; set; }
        public AdvanceLevel AdvLevel { get; set; }
        public int RowNum { get; set; }
        public Species SelectedSpecies { get; set; }
    }
}
