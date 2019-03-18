using System.Collections.ObjectModel;

namespace WFRP_NPC_Creator
{
    public class DataGridViewModel : BaseViewModel
    {
        public ObservableCollection<DataRowViewModel> Rows { get; set; } = new ObservableCollection<DataRowViewModel>();

        public DataGridViewModel()
        {
            
        }

        public void AddRow(string careerName)
        {
            Rows.Add(new DataRowViewModel(Career.GetCareerList().Find(c => c.Name == careerName)));
        }
    }
}