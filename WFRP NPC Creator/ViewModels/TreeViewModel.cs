using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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
                        TierItem = new TreeItemViewModel(career.Name, true, career.Tier);
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
        public string Name { get; set; }
        public ObservableCollection<TreeItemViewModel> SubItems { get; set; } = new ObservableCollection<TreeItemViewModel>();

        public bool Selectable { get; set; }

        public string IconPath { get; set; }

   
       

        public TreeItemViewModel(string name, bool selectable = false, int tier = 0)
        {
            Name = name;
            Selectable = selectable;

           // Icon. = "pack://application:,,,/WFRP_NPC_Creator;component/Resources/tier1";

            switch (tier)
            {
                case 1:
                    IconPath = "/Resources/tier1.png";
                    break;
                case 2:
                    IconPath = "/Resources/tier2.png";
                    break;
                case 3:
                    IconPath = "/Resources/tier3.png";
                    break;
                case 4:
                    IconPath = "/Resources/tier4.png";
                    break;
                default:
                    IconPath = null;
                    break;
            }

            //DragItemCommand = new DragCommand(BeginDrag);
        }

        public void BeginDrag()
        {
        }

    }
}
