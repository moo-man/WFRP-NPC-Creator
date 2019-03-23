using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WFRP_NPC_Creator
{
    public class DataGridViewModel : BaseViewModel
    {
        public ObservableCollection<CareerRowViewModel> CareerRows { get; set; } = new ObservableCollection<CareerRowViewModel>();
        public List<int> RowIDs { get; set; } = new List<int>();
        private int id = 0;

        public delegate void RowChangedEventHandler(object source, EventArgs e);

        public event RowChangedEventHandler CareerChanged;
        public event RowChangedEventHandler SpeciesChanged;
        

        public ObservableCollection<SpeciesRowViewModel> SpeciesRow { get; set; } = new ObservableCollection<SpeciesRowViewModel>();


        public DataGridViewModel()
        {
            SpeciesRow.Add(new SpeciesRowViewModel());
            SpeciesRow[0].RowChanged += SpeciesEdit;
        }

        public void AddRow(string careerName)
        {
            CareerRowViewModel newDGRow = new CareerRowViewModel(Career.GetCareerList().Find(c => c.Name == careerName), id);

            newDGRow.RowChanged += CareerEdit;

            CareerRows.Add(newDGRow);
            RowIDs.Add(id);
            id++;
        }


        public void SpeciesEdit(object source, RowChangeEventArgs e)
        {
            OnSpeciesChanged(source as SpeciesRowViewModel, e);
        }


        public void CareerEdit(object source, RowChangeEventArgs e)
        {
            OnCareerChanged(source as CareerRowViewModel, e);
        }

        protected virtual void OnCareerChanged(CareerRowViewModel row, RowChangeEventArgs rowChange)
        {
            if (CareerChanged != null)
                CareerChanged(this, new CareerChangedEventArgs
                {
                    careerIndex = CareerRows.IndexOf(row),
                    advLevel = rowChange.AdvLevel,
                    change = rowChange.ChangeType
                });
        }

        protected virtual void OnSpeciesChanged(SpeciesRowViewModel speciesRowViewModel, RowChangeEventArgs rowChange)
        {
            if (SpeciesChanged != null)
                SpeciesChanged(this, new SpeciesChangedEventArgs
                {
                    Name = speciesRowViewModel.Name,
                    change = rowChange.ChangeType,
                    newSpecies = rowChange.SelectedSpecies
                });
        }
    }

    public class CareerChangedEventArgs : EventArgs
    {
        public int careerIndex;
        public RowAction change;
        public AdvanceLevel advLevel;
    }

    public class SpeciesChangedEventArgs : EventArgs
    {
        public string Name;
        public RowAction change;
        public Species newSpecies;
    }
}