using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFRP_NPC_Creator
{
    public class DataRowViewModel : BaseViewModel
    {
        public SelectionChangedCommand AdvanceLevelChanged { get; private set; }

        public RerollCommand RerollClicked { get; private set; }

        public delegate void RowChangedEventHandler(object source, RowChangeEventArgs e);

        public event RowChangedEventHandler RowChanged;

        public string Name { get; private set; }

        private int RowID;
        public AdvanceLevel ComboBoxSelection { get; set; } = AdvanceLevel.None;

        public DataRowViewModel(Career career, int rowID)
        {
            Name = career.Name;
            AdvanceLevelChanged = new SelectionChangedCommand(ComboBoxChanged);
            RerollClicked = new RerollCommand(Reroll);
            RowID = rowID;
        }

        protected void Reroll(RowAction change)
        {
            OnRowChanged(change);
        }

        protected void ComboBoxChanged()
        {
            OnRowChanged(RowAction.AdvanceChange);
        }

        protected virtual void OnRowChanged(RowAction change)
        {
            if (RowChanged != null)
                RowChanged(this, new RowChangeEventArgs { ChangeType = change, AdvLevel = ComboBoxSelection, RowNum = RowID});
        }
    }
}
