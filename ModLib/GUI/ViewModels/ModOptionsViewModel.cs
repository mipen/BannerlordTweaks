using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Engine.Screens;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace ModLib.GUI.ViewModels
{
    public class ModOptionsViewModel : ViewModel
    {
        private string _titleLabel;
        private bool _changesMade = false;
        private string _cancelButtonText;
        private string _doneButtonText;
        private ModSettingsVM _selectedMod;
        private MBBindingList<ModSettingsVM> _modSettingsList = new MBBindingList<ModSettingsVM>();

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
            get => _changesMade;
            set
            {
                _changesMade = value;
                OnPropertyChanged();
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
                _selectedMod = value;
                OnPropertyChanged();
                OnPropertyChanged("SelectedModName");
            }
        }
        [DataSourceProperty]
        public string SelectedModName => SelectedMod == null ? "Mod Name Goes Here" : SelectedMod.ModName;

        public ModOptionsViewModel()
        {
            RefreshValues();
        }

        public override void RefreshValues()
        {
            base.RefreshValues();
            TitleLabel = "Mod Options";
            DoneButtonText = new TextObject("{=WiNRdfsm}Done", null).ToString();
            CancelButtonText = new TextObject("{=3CpNUnVl}Cancel", null).ToString();

            //DEBUG
            ChangesMade = true;
            for (int i = 1; i < 11; i++)
            {
                ModSettingsList.Add(new ModSettingsVM($"mod {i}", null));
            }
            SelectedMod = ModSettingsList[0];
            OnPropertyChanged("SelectedModName");
        }

        private void ExecuteCancel()
        {
            //TODO:: Revert any changes
            ScreenManager.PopScreen();
        }

        private void ExecuteDone()
        {
            //TODO:: Save the changes to file.
            //ScreenManager.PopScreen();
            if (SelectedMod != null)
            {
                int ind = ModSettingsList.IndexOf(SelectedMod) + 1;
                if (ind >= ModSettingsList.Count)
                    ind = 0;
                SelectedMod = ModSettingsList[ind];

            }
            else
                SelectedMod = ModSettingsList[0];
        }
    }
}
