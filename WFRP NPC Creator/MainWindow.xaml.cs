using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml;

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
            TraitReader.read = true;
            this.DataContext = new WindowViewModel();

            //  string text;
            //  using (StreamReader sr = new StreamReader("fgdb.json"))
            //  {
            //      text = sr.ReadToEnd();
            //  }
            //  dynamic json = Newtonsoft.Json.JsonConvert.DeserializeObject(text);
            //
            //  foreach (dynamic table in json["tables"])
            //  {
            //      dynamic test = table["0"]["description"];
            //      System.Diagnostics.Debug.WriteLine(((object)test).ToString());
            //  }

            /*using (StreamWriter sw = new StreamWriter("fg.json"))
            {
                
            }*/


            //string careerJson = Newtonsoft.Json.JsonConvert.SerializeObject(Career.ClassList);
            //string talentJson = Newtonsoft.Json.JsonConvert.SerializeObject(Talent.TalentList);
            //string traitJson = Newtonsoft.Json.JsonConvert.SerializeObject(Trait.TraitList);

            //    StreamWriter sw = new StreamWriter("trait.json");
            //sw.Write(traitJson);
            //  sw.Close();


            //Testing code
            /*  foreach (Career career in Career.GetCareerList())
              {
                  foreach (string talent in career.CareerTalents)
                      if (Talent.TalentList.Find(t => t.Name == Talent.GenericName(talent)) == null)
                          System.Diagnostics.Debug.WriteLine(talent + " not found");
              }

              int talentCount;
              foreach (TalentInfo talent in Talent.TalentList)
              {
                  talentCount = 0;
                  foreach (Career career in Career.GetCareerList())
                  {
                      if (career.CareerTalents.Contains(talent.Name))
                          talentCount++;
                  }
                  if (talentCount == 1)
                      System.Diagnostics.Debug.WriteLine(talent.Name);
              }*/
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

        private void MenuItem_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
