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
            Human test1 = new Human();
            Human test2 = new Human();
            Human test3 = new Human();

            test1.AddCareer("Squire", AdvanceLevel.Complete);
            test1.AddCareer("Knight", AdvanceLevel.Complete);
            test1.AddCareer("First Knight", AdvanceLevel.Complete);
            test1.AddCareer("Knight of the Inner Circle", AdvanceLevel.Complete);
            test1.PrintToConsole();

        }
    }
}
