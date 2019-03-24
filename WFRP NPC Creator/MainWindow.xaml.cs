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
        }

        // ************************************************************************************************************************************
        // What lies below does not follow MVVM architecture, but getting drag and drop to work with MVVM was so damn confusing I gave up on it
        private void item_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock item = (TextBlock)sender;

            if ((bool)item.Tag) // If leaf (selectable)
                DragDrop.DoDragDrop(item, item.Text, DragDropEffects.Move);
        }

        private void DataGrid_Drop(object sender, DragEventArgs e)
        {
            (this.DataContext as WindowViewModel).AddGridRow(e.Data.GetData(DataFormats.Text).ToString());
        }
    }
}
