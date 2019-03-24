using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WFRP_NPC_Creator;

namespace WFRP_NPC_Creator
{
    public class TreeViewModel : BaseViewModel
    {
        public ObservableCollection<TreeItemViewModel> Items { get; set; } = new ObservableCollection<TreeItemViewModel>();
        public TreeItemViewModel SelectedItem { get; set; }

        public TreeViewModel()
        {
            TreeItemViewModel ClassItem, CareerItem, TierItem;
            foreach (CareerClass cClass in Career.ClassList)
            {
                ClassItem = new TreeItemViewModel(cClass.ClassName);
                foreach (CareerPath cPath in cClass.CareerPaths)
                {
                    CareerItem = new TreeItemViewModel(cPath.PathName);
                    foreach (Career career in cPath.Tiers)
                    {
                        TierItem = new TreeItemViewModel(career.Name, true);
                        CareerItem.SubItems.Add(TierItem);
                    }
                    ClassItem.SubItems.Add(CareerItem);
                }
                Items.Add(ClassItem);
            }
        }
    }

    public class TreeItemViewModel : BaseViewModel
    {
        public DragCommand DragItemCommand { get; private set; }
        DependencyObject obj;
        public string Name { get; set; }
        public ObservableCollection<TreeItemViewModel> SubItems { get; set; } = new ObservableCollection<TreeItemViewModel>();

        public bool Selectable { get; set; }

        public TreeItemViewModel(string name, bool selectable = false)
        {
            Name = name;
            Selectable = selectable;
            //DragItemCommand = new DragCommand(BeginDrag);
        }

        public void BeginDrag()
        {
        }

    }
}
