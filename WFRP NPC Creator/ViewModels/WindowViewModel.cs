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

        public delegate void AdvanceChangedEventHandler(object sender, AdvanceChangedEventArgs e);

        protected virtual void OnAdvanceChanged(AdvanceChangedEventArgs e)
        {
        }


        public WindowViewModel()
        {
            NPC = new Human();
        }

        public void AddGridRow(string careerName)
        {
            DataGrid.AddRow(careerName);
        }
    }

    public class AdvanceChangedEventArgs : EventArgs
    {
        public AdvanceLevel NewAdvanceLevel { get; set; }
        public int dgRow { get; set; }

    }
}
