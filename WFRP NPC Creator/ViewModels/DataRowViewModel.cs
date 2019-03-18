using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFRP_NPC_Creator
{
    public class DataRowViewModel : BaseViewModel
    {
        private Career careerData;

        public SelectionChangedCommand AdvanceLevelChanged { get; private set; }

        public string Name { get; set; }
        public AdvanceLevel ComboBoxSelection { get; set; } = AdvanceLevel.None;

        public DataRowViewModel(Career career)
        {
            careerData = career;
            Name = careerData.Name;

            AdvanceLevelChanged = new SelectionChangedCommand(ChangeAdvance);
        }

        public void ChangeAdvance()
        {

        }
    }
}
