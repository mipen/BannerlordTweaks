using ModLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaleWorlds.Engine.Screens;
using TaleWorlds.Library;

namespace ModLib.GUI.ViewModels
{
    public class ModSettingsVM : ViewModel
    {
        private bool _isSelected;
        private Action<ModSettingsVM> _executeSelect;
        private MBBindingList<SettingPropertyGroup> _settingPropertyGroups;

        public SettingsBase SettingsInstance { get; private set; }
        [DataSourceProperty]
        public string ModName => SettingsInstance.ModName;
        [DataSourceProperty]
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged();
            }
        }
        [DataSourceProperty]
        public MBBindingList<SettingPropertyGroup> SettingPropertyGroups
        {
            get => _settingPropertyGroups;
            set
            {
                if (_settingPropertyGroups != value)
                {
                    _settingPropertyGroups = value;
                    OnPropertyChanged();
                }
            }
        }

        public ModSettingsVM(SettingsBase settingsInstance, Action<ModSettingsVM> executeSelect)
        {
            SettingsInstance = settingsInstance;
            _executeSelect = executeSelect;
            SettingPropertyGroups = new MBBindingList<SettingPropertyGroup>();
            SettingPropertyGroups.AddRange(settingsInstance.GetSettingPropertyGroups());
            RefreshValues();
        }

        public override void RefreshValues()
        {
            base.RefreshValues();
            OnPropertyChanged("IsSelected");
            OnPropertyChanged("ModName");
            OnPropertyChanged("SettingPropertyGroups");
        }

        public void AddSelectCommand(Action<ModSettingsVM> command)
        {
            _executeSelect = command;
        }

        private void ExecuteSelect()
        {
            _executeSelect?.Invoke(this);
        }
    }
}
