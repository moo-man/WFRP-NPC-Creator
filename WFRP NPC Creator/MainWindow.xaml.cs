using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WFRP_NPC_Creator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            CareerJsonReader.read = true;
            TalentReader.read = true;

            this.DataContext = new WindowViewModel();

            Character Test = new Human();

            Test.AddCareer("Servant", AdvanceLevel.Complete);
            Test.AddCareer("Advisor", AdvanceLevel.Complete);
            Test.AddCareer("Wizard's Apprentice", AdvanceLevel.Complete);
            Test.AddCareer("Wizard", AdvanceLevel.Complete);
            Test.AddCareer("Master Wizard", AdvanceLevel.Beyond);


            Test.PrintToConsole(true);


        }

        private void item_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock item = (TextBlock)sender;
            DragDrop.DoDragDrop(item, item.Text, DragDropEffects.Move);
        }

        private void DataGrid_Drop(object sender, DragEventArgs e)
        {
            (this.DataContext as WindowViewModel).AddGridRow(e.Data.GetData(DataFormats.Text).ToString());
        }
    }
}
