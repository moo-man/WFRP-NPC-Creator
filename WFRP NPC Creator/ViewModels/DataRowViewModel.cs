using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFRP_NPC_Creator
{
    public abstract class DataRowViewModel : BaseViewModel
    {
        
        public SelectionChangedCommand SelectionChanged { get; protected set; }

        public RerollCommand RerollClicked { get; protected set; }

        public delegate void RowChangedEventHandler(object source, RowChangeEventArgs e);

        public event RowChangedEventHandler RowChanged;
        
        protected void Reroll(RowAction change)
        {
            OnRowChanged(CreateEventArgs(change));
        }

        protected void ComboBoxChanged()
        {
            OnRowChanged(CreateEventArgs(RowAction.SelectionChange));
        }

        protected abstract RowChangeEventArgs CreateEventArgs(RowAction rowAction);


        protected virtual void OnRowChanged(RowChangeEventArgs args)
        {
            if (RowChanged != null)
            {
                RowChanged(this, args);
            }
        }
    }


    public class CareerRowViewModel : DataRowViewModel
    {
        public string Name { get; private set; }

        private int RowID;

        public AdvanceLevel AdvanceSelection { get; set; } = AdvanceLevel.None;


        public CareerRowViewModel(Career career, int rowID)
        {
            Name = career.Name;
            RerollClicked = new RerollCommand(Reroll);
            RowID = rowID;
            SelectionChanged = new SelectionChangedCommand(ComboBoxChanged);
        }

        protected override RowChangeEventArgs CreateEventArgs(RowAction rowAction)
        {
            return new RowChangeEventArgs
            {
                ChangeType = rowAction,
                AdvLevel = AdvanceSelection,
                RowNum = RowID,
                rType = RowType.Career
            };
        }
    }

    public class SpeciesRowViewModel : DataRowViewModel
    {
        public string Name { get; set; }

        public Species SpeciesSelection { get; set; } = Species.Human;

        public SpeciesRowViewModel()
        {
            SpeciesSelection = Species.Human;
            RerollClicked = new RerollCommand(Reroll);
            SelectionChanged = new SelectionChangedCommand(ComboBoxChanged);

        }

        protected override RowChangeEventArgs CreateEventArgs(RowAction rowAction)
        {
            return new RowChangeEventArgs
            {
                ChangeType = rowAction,
                rType = RowType.Species,
                SelectedSpecies = SpeciesSelection
            };
        }
    }
}
