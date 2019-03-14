using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFRP_NPC_Creator
{
    public class CareerTreeViewModel : BaseViewModel
    {
        public ObservableCollection<CareerItemViewModel> Items { get; set; } = new ObservableCollection<CareerItemViewModel>();

        public CareerTreeViewModel()
        {
            CareerItemViewModel ClassItem, CareerItem, TierItem;
            foreach (CareerClass cClass in Career.ClassList)
            {
                ClassItem = new CareerItemViewModel(cClass.ClassName);
                foreach (CareerPath cPath in cClass.CareerPaths)
                {
                    CareerItem = new CareerItemViewModel(cPath.PathName);
                    foreach (Career career in cPath.Tiers)
                    {
                        TierItem = new CareerItemViewModel(career.Name);
                        CareerItem.SubItems.Add(TierItem);
                    }
                    ClassItem.SubItems.Add(CareerItem);
                }
                Items.Add(ClassItem);
            }
        }
    }

    public class CareerItemViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public ObservableCollection<CareerItemViewModel> SubItems { get; set; } = new ObservableCollection<CareerItemViewModel>();

        public CareerItemViewModel(string name)
        {
            Name = name;
        }


    }
}
