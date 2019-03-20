using System.Collections.ObjectModel;

namespace WFRP_NPC_Creator
{
    public class RichTextViewModel : BaseViewModel
    {
        public ObservableCollection<int> TableValues { get; set; } = new ObservableCollection<int>();

        public string SkillsString { get; private set; }

        public string TalentsString { get; private set; }

        public RichTextViewModel()
        {
            TableValues = new ObservableCollection<int>(new int[12]);

        }

        public void UpdateTableValues(int[] newTable)
        {

            for (int i = 0; i < 12; i++)
            {
                TableValues[i] = newTable[i];
            }
        }



    }
}