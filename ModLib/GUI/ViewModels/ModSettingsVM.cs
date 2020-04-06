using ModLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Engine.Screens;
using TaleWorlds.Library;

namespace ModLib.GUI.ViewModels
{
    public class ModSettingsVM : ViewModel
    {
        private bool _isSelected;

        public ISettings SettingsInstance { get; private set; }
        [DataSourceProperty]
        public string ModName { get; set; }
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

        public ModSettingsVM(string modName, ISettings settingsInstance)
        {
            ModName = modName;
            SettingsInstance = settingsInstance;
            RefreshValues();
        }

        public override void RefreshValues()
        {
            base.RefreshValues();
            OnPropertyChanged("IsSelected");
            OnPropertyChanged("ModName");
        }

        private void ExecuteSelected()
        {
            IsSelected = !IsSelected;
        }
    }
}
