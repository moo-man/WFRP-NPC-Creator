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
        public Character NPC;

        public WindowViewModel()
        {
            DataGrid.CareerChanged += CareerChange;
            NPC = new Human();
        }

        public void AddGridRow(string careerName)
        {
            DataGrid.AddRow(careerName);
            NPC.AddCareer(careerName);
        }

        public void CareerChange(object source, CareerChangedEventArgs e)
        {
            NPC.ChangeCareerAdvancement(e.careerIndex, e.advLevel);
            NPC.PrintToConsole(true);
        }
    }


    public class RowChangeEventArgs : EventArgs
    {
        public RowAction ChangeType { get; set; }
        public AdvanceLevel AdvLevel { get; set; }
        public int RowNum { get; set; }
    }
}
