using System.Collections.ObjectModel;

namespace WFRP_NPC_Creator
{
    public class RichTextViewModel : BaseViewModel
    {
        public ObservableCollection<int> TableValues { get; set; } = new ObservableCollection<int>();

        public ObservableCollection<string> SkillsString { get; set; } = new ObservableCollection<string>(new string[1]);

        public ObservableCollection<string> TalentsString { get; set; } = new ObservableCollection<string>(new string[1]);


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

        public void UpdateSkills(string newString)
        {
            SkillsString[0] = newString;
        }

        public void UpdateTalents(string newString)
        {
            TalentsString[0] = newString;
        }
    }
}