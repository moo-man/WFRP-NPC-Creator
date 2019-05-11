namespace WFRP_NPC_Creator
{
    public class SettingsViewModel : BaseViewModel
    {
        public delegate void SettingsChangedEventHandler(object sender, ChangedSetting change);
        public event SettingsChangedEventHandler SettingsChanged;

        private bool onlyRelevantSkillsChecked;
        public bool OnlyRelevantSkillsChecked
        {
            get
            {
                return onlyRelevantSkillsChecked;
            }
            set
            {
                onlyRelevantSkillsChecked = value;
                Settings.ShowOnlyRelevantSkills = value;
                OnPropertyChanged("OnlyRelevantSkillsChecked");
                OnSettingsChanged(ChangedSetting.RelevantSkills);
            }
        }

        private bool onlyRelevantTalentsChecked;
        public bool OnlyRelevantTalentsChecked
        {
            get
            {
                return onlyRelevantTalentsChecked;
            }
            set
            {
                onlyRelevantTalentsChecked = value;
                Settings.ShowOnlyRelevantTalents = value;
                OnPropertyChanged("OnlyRelevantTalentsChecked");
                OnSettingsChanged(ChangedSetting.RelevantTalents);

            }
        }

        private bool averageCharacteristicsChecked;
        public bool AverageCharacteristicsChecked
        {
            get
            {
                return averageCharacteristicsChecked;
            }
            set
            {
                averageCharacteristicsChecked = value;
                Settings.UseAverageSpeciesCharacteristics = value;
                OnPropertyChanged("AverageCharacteristicsChecked");
                OnSettingsChanged(ChangedSetting.AverageCharacteristics);
            }
        }


        private bool alphabeticalClassViewChecked;
        public bool AlphabeticalClassViewChecked
        {
            get
            {
                return alphabeticalClassViewChecked;
            }
            set
            {
                alphabeticalClassViewChecked = value;
                Settings.AlphabeticalClassView = alphabeticalClassViewChecked;
                OnPropertyChanged("AlphabeticalClassViewChecked");
                OnSettingsChanged(ChangedSetting.AlphabeticalClassView);
            }
        }

        public SettingsViewModel()
        {
            OnlyRelevantSkillsChecked = true;
            OnlyRelevantTalentsChecked = true;
            AverageCharacteristicsChecked = false;
            AlphabeticalClassViewChecked = false;
        }

        protected virtual void OnSettingsChanged(ChangedSetting change)
        {
            if (SettingsChanged != null)
                SettingsChanged(this, change);
        }

        public enum ChangedSetting
        {
            RelevantSkills,
            RelevantTalents,
            AverageCharacteristics,
            AlphabeticalClassView
        }
    }
}