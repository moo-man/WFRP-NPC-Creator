using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WFRP_NPC_Creator
{
    public class DataGridViewModel : BaseViewModel
    {
        private IMsgBoxService messageService;
        
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

            messageService = new MsgBoxService();
        }

        public void AddRow(string careerName)
        {
            string[] lowerTiers = Career.GetLowerTiers(careerName);
            List<string> lowerTiersToAdd = new List<string>();

            if (lowerTiers.Length > 1)
            {
                foreach (string lowerCareer in lowerTiers)
                {
                    bool contains = false;
                    foreach (CareerRowViewModel row in CareerRows)
                    {
                        if (row.Name == lowerCareer)
                            contains = true;
                    }
                    if (!contains)
                        lowerTiersToAdd.Add(lowerCareer);
                }
                if (lowerTiersToAdd.Count > 1)
                    if (messageService.YesNoBox("Add lower tier careers?", "Add Lower Tiers"))
                    {
                        foreach (string career in lowerTiersToAdd)
                        {
                            CareerRowViewModel lowerRow = new CareerRowViewModel(Career.GetCareerList().Find(c => c.Name == career), id);
                            lowerRow.RowChanged += CareerEdit;
                            lowerRow.AdvanceSelection = AdvanceLevel.Complete;
                            CareerRows.Add(lowerRow);
                            RowIDs.Add(id);
                            id++;

                            lowerRow.ManualUpdate(RowAction.Add);

                        }
                    }
            }

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
            {
                if (rowChange.ChangeType != RowAction.Add)
                    CareerChanged(this, new CareerChangedEventArgs
                    {
                        careerIndex = CareerRows.IndexOf(row),
                        careerName = CareerRows[rowChange.RowNum].Name,
                        advLevel = rowChange.AdvLevel,
                        change = rowChange.ChangeType
                    });
                else // If row was added from ManualUpdate()
                    CareerChanged(this, new CareerChangedEventArgs
                    {
                        careerIndex = -1, // -1 = adding a new career
                        careerName = CareerRows[rowChange.RowNum].Name,
                        advLevel = rowChange.AdvLevel,
                        change = rowChange.ChangeType
                    });
            }
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
        public string careerName;
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