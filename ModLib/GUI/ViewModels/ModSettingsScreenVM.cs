using System.Linq;
using System.Windows.Forms;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Engine.Screens;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace ModLib.GUI.ViewModels
{
    public class ModSettingsScreenVM : ViewModel
    {
        private string _titleLabel;
        private string _cancelButtonText;
        private string _doneButtonText;
        private ModSettingsVM _selectedMod;
        private MBBindingList<ModSettingsVM> _modSettingsList = new MBBindingList<ModSettingsVM>();
        private string _hintText;

        [DataSourceProperty]
        public string TitleLabel
        {
            get => _titleLabel;
            set
            {
                _titleLabel = value;
                OnPropertyChanged();
            }
        }
        [DataSourceProperty]
        public bool ChangesMade
        {
            get
            {
                return ModSettingsList.Any((x) => x.URS.ChangesMade());
            }
        }
        [DataSourceProperty]
        public string DoneButtonText
        {
            get => _doneButtonText;
            set
            {
                _doneButtonText = value;
                OnPropertyChanged();
            }
        }
        [DataSourceProperty]
        public string CancelButtonText
        {
            get => _cancelButtonText; set
            {
                _cancelButtonText = value;
                OnPropertyChanged();
            }
        }
        [DataSourceProperty]
        public MBBindingList<ModSettingsVM> ModSettingsList
        {
            get => _modSettingsList;
            set
            {
                if (!(_modSettingsList == value))
                {
                    _modSettingsList = value;
                    OnPropertyChanged();
                }
            }
        }
        [DataSourceProperty]
        public ModSettingsVM SelectedMod
        {
            get => _selectedMod;
            set
            {
                if (_selectedMod != value)
                {
                    _selectedMod = value;
                    OnPropertyChanged();
                    OnPropertyChanged("SelectedModName");
                    OnPropertyChanged("SomethingSelected");
                }
            }
        }
        [DataSourceProperty]
        public string SelectedModName => SelectedMod == null ? "Mod Name Goes Here" : SelectedMod.ModName;
        [DataSourceProperty]
        public bool SomethingSelected => SelectedMod != null;
        [DataSourceProperty]
        public string HintText
        {
            get => _hintText;
            set
            {
                if (_hintText != value)
                {
                    _hintText = value;
                    OnPropertyChanged();
                    OnPropertyChanged("IsHintVisible");
                }
            }
        }
        public bool IsHintVisible => !string.IsNullOrWhiteSpace(HintText);

        public ModSettingsScreenVM()
        {
            RefreshValues();
        }

        public override void RefreshValues()
        {
            base.RefreshValues();
            TitleLabel = "Mod Options";
            DoneButtonText = new TextObject("{=WiNRdfsm}Done", null).ToString();
            CancelButtonText = new TextObject("{=3CpNUnVl}Cancel", null).ToString();

            ModSettingsList.Clear();
            foreach (var msvm in SettingsDatabase.ModSettingsVMs)
            {
                msvm.AddSelectCommand(ExecuteSelect);
                ModSettingsList.Add(msvm);
                msvm.SetParent(this);
                msvm.RefreshValues();
            }
            OnPropertyChanged("SelectedMod");
        }

        public bool ExecuteCancel()
        {
            //Revert any changes
            ScreenManager.PopScreen();
            foreach (var msvm in ModSettingsList)
            {
                msvm.URS.UndoAll();
                msvm.URS.ClearStack();
            }
            AssignParent(true);
            ExecuteSelect(null);
            return true;
        }

        private void ExecuteDone()
        {
            //Save the changes to file.
            if (ModSettingsList.Any((x) => x.URS.ChangesMade()))
            {
                InformationManager.ShowInquiry(new InquiryData("Game Needs to Restart",
                                "The game needs to be restarted to apply mods settings changes. Do you want to close the game now?",
                                true, true, "Yes", "No",
                                () =>
                                {
                                    ModSettingsList.Where((x) => x.URS.ChangesMade())
                                    .Do((x) => SettingsDatabase.SaveSettings(x.SettingsInstance))
                                    .Do((x) => x.URS.ClearStack());

                                    Utilities.QuitGame();
                                }, () => { }));
            }
            else
                ScreenManager.PopScreen();
        }

        public void AssignParent(bool remove = false)
        {
            foreach (var msvm in ModSettingsList)
                msvm.SetParent(remove ? null : this);
        }

        public void ExecuteSelect(ModSettingsVM msvm)
        {
            if (SelectedMod != msvm)
            {
                if (SelectedMod != null)
                    SelectedMod.IsSelected = false;

                SelectedMod = msvm;

                if (SelectedMod != null)
                {
                    SelectedMod.IsSelected = true;
                    //TODO:: Update the settings view
                }
            }
        }
    }
}
