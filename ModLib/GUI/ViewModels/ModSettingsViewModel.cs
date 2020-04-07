using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaleWorlds.Engine.Screens;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace ModLib.GUI.ViewModels
{
    public class ModSettingsViewModel : ViewModel
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

        public ModSettingsViewModel()
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
            }
            OnPropertyChanged("SelectedMod");

            //DEBUG
            //ChangesMade = true;
            //for (int i = 1; i < 21; i++)
            //{
            //    ModSettingsList.Add(new ModSettingsVM($"mod {i}", null, (x) => { ExecuteSelect(x); }));
            //}
            //ExecuteSelect(ModSettingsList[0]);
            //OnPropertyChanged("SelectedModName");
        }

        private void ExecuteCancel()
        {
            //TODO:: Revert any changes
            ScreenManager.PopScreen();
        }

        private void OnScroll()
        {
            MessageBox.Show("hello");
        }

        private void ExecuteDone()
        {
            //TODO:: Save the changes to file.
            ScreenManager.PopScreen();
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
