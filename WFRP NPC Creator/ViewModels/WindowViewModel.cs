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
            NPC = new Human();
            UpdateTable();
        }

        public void AddGridRow(string careerName)
        {
            DataGrid.AddRow(careerName);
            NPC.AddCareer(careerName);
            UpdateTable();
        }

        public void CareerChange(object source, CareerChangedEventArgs e)
        {
            switch (e.change)
            {
                case RowAction.AdvanceChange:
                    NPC.ChangeCareerAdvancement(e.careerIndex, e.advLevel);
                    break;
                case RowAction.RerollCharacteristic:
                    NPC.RerollCareerCharacteristics(e.careerIndex);
                    break;
                case RowAction.RerollSkill:
                    NPC.RerollCareerSkills(e.careerIndex);
                    break;
                case RowAction.RerollTalent:
                    NPC.RerollCareerTalents(e.careerIndex);
                    break;
            }
            UpdateTable();
        }


        private void UpdateTable()
        {
            int[] tableArray = new int[12];
            for (Characteristics i = 0; i < (Characteristics)10; i++)
                tableArray[(int)i+1] = NPC.CharacteristicValue(i);

            RichText.UpdateTableValues(tableArray);
        }
    }


    public class RowChangeEventArgs : EventArgs
    {
        public RowAction ChangeType { get; set; }
        public AdvanceLevel AdvLevel { get; set; }
        public int RowNum { get; set; }
    }
}
