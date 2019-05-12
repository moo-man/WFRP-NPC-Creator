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

        /*  CareerRowViewModel currentSelection;
          public CareerRowViewModel CurrentSelection {
              get
              {
                  System.Diagnostics.Debug.Print(currentSelection.ToString());
                  return currentSelection; }
              set {
                  System.Diagnostics.Debug.Print(currentSelection.ToString());
                  currentSelection = value;
              }
          }*/

        public int CurrentSelection { get; private set; }

        public delegate void RowChangedEventHandler(object source, EventArgs e);

        public event RowChangedEventHandler CareerChanged;
        public event RowChangedEventHandler SpeciesChanged;


        public ObservableCollection<SpeciesRowViewModel> SpeciesRow { get; set; } = new ObservableCollection<SpeciesRowViewModel>();

        public RemoveCommand RemoveCareer { get; protected set; }

        public DGSelectionChangedCommand ChangeSelection { get; protected set; }


        public DataGridViewModel()
        {
            SpeciesRow.Add(new SpeciesRowViewModel());
            SpeciesRow[0].RowChanged += SpeciesEdit;

            RemoveCareer = new RemoveCommand(RemoveRow);

            ChangeSelection = new DGSelectionChangedCommand(SelectedRowChange);

            messageService = new MsgBoxService();
        }

        public void AddRow(string careerName)
        {
            try
            {
                string[] lowerTiers = Career.GetLowerTiers(careerName);
                List<string> lowerTiersToAdd = new List<string>();

                if (lowerTiers.Length > 0)
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
                    if (lowerTiersToAdd.Count > 0)
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

            catch (Exception e)
            {
                messageService.ShowNotification(e.Message);
            }
        }

        private void RemoveRow(int rowID)
        {
            int rowIndex = -1;
            for (int i = 0; i < CareerRows.Count; i++)
                if (CareerRows[i].RowID == rowID)
                {
                    rowIndex = i;
                    break;
                }
            
            if (rowIndex == -1)
                return;

            CareerChanged(CareerRows[rowIndex], new CareerChangedEventArgs
            {
                careerIndex = rowID,
                change = RowAction.Delete
            });
            CareerRows.RemoveAt(rowID);

            System.Diagnostics.Debug.WriteLine("REMOVE {0}\n", rowID);
        }

        private void SelectedRowChange(int n)
        {
            CurrentSelection = n;
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