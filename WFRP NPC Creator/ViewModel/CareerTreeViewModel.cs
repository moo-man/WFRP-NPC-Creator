using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFRP_NPC_Creator
{
    public class CareerTreeViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<CareerItemViewModel> Items { get; set; } = new ObservableCollection<CareerItemViewModel>();

        public CareerTreeViewModel()
        {
            foreach (Career career in Career.List)
            {
                Items.Add(new CareerItemViewModel(career.Name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
    }

    public class CareerItemViewModel : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public ObservableCollection<CareerItemViewModel> SubItems { get; set; } = new ObservableCollection<CareerItemViewModel>();

        public CareerItemViewModel(string name)
        {
            Name = name;
        }

        public bool IsExpanded
        {
            get
            {
                return SubItems.Count() > 0;
            }
            set
            {
                if (value == true)
                    Expand();
                else
                    ClearChildren();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };


        private void Expand()
        {
            throw new NotImplementedException();
        }

        private void ClearChildren()
        {
            throw new NotImplementedException();
        }
    }
}
