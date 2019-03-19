using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WFRP_NPC_Creator
{
    public class DataGridViewModel : BaseViewModel
    {
        public ObservableCollection<DataRowViewModel> Rows { get; set; } = new ObservableCollection<DataRowViewModel>();
        public List<int> RowIDs { get; set; } = new List<int>();
        private int id = 0;


        public delegate void CareerChangedEventHandler(object source, CareerChangedEventArgs e);

        public event CareerChangedEventHandler CareerChanged;

        public DataGridViewModel()
        {

        }

        public void AddRow(string careerName)
        {
            DataRowViewModel newDGRow = new DataRowViewModel(Career.GetCareerList().Find(c => c.Name == careerName), id);

            newDGRow.RowChanged += CareerEdit;

            Rows.Add(newDGRow);
            RowIDs.Add(id);
            id++;
        }

        public void CareerEdit(object source, RowChangeEventArgs e)
        {
            OnCareerChanged(source as DataRowViewModel, e);
        }

        protected virtual void OnCareerChanged(DataRowViewModel row, RowChangeEventArgs rowChange)
        {
            if (CareerChanged != null)
                CareerChanged(this, new CareerChangedEventArgs { careerIndex = Rows.IndexOf(row), advLevel = rowChange.AdvLevel, change = rowChange.ChangeType });
        }
        
        public List<Tuple<string, AdvanceLevel>> GetRowData()
        {
            List<Tuple<string, AdvanceLevel>> data = new List<Tuple<string, AdvanceLevel>>();
            foreach (DataRowViewModel row in Rows)
                data.Add(new Tuple<string, AdvanceLevel>(row.Name, row.ComboBoxSelection));

            return data;
        }
    }

    public class CareerChangedEventArgs : EventArgs
    {
        public int careerIndex;
        public RowAction change;
        public AdvanceLevel advLevel;
    }
}