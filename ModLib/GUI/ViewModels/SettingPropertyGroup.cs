using ModLib.Attributes;
using ModLib.GUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Library;

namespace ModLib.GUI.ViewModels
{
    public class SettingPropertyGroup : ViewModel
    {
        public const string DefaultGroupName = "Ungrouped";
        public SettingProperty GlobalToggleSettingProperty { get; private set; } = null;
        public SettingPropertyGroupAttribute Attribute { get; private set; }
        public UndoRedoStack URS { get; private set; }
        public ModSettingsScreenVM Parent { get; private set; }
        public string HintText
        {
            get
            {
                if (GlobalToggleSettingProperty != null && !string.IsNullOrWhiteSpace(GlobalToggleSettingProperty.HintText))
                {
                    return $"{GlobalToggleSettingProperty.Name}: {GlobalToggleSettingProperty.HintText}";
                }
                return "";
            }
        }

        public string GroupName => Attribute.GroupName;

        [DataSourceProperty]
        public string GroupNameDisplay
        {
            get
            {
                string addition = GlobalToggle ? "" : "(Disabled)";
                return $"{Attribute.GroupName} {addition}";
            }
        }
        [DataSourceProperty]
        public MBBindingList<SettingProperty> SettingProperties { get; } = new MBBindingList<SettingProperty>();
        [DataSourceProperty]
        public bool GlobalToggle
        {
            get
            {
                if (GlobalToggleSettingProperty != null)
                    return GlobalToggleSettingProperty.BoolValue;
                else
                    return true;
            }
            set
            {
                if (GlobalToggleSettingProperty != null && GlobalToggleSettingProperty.BoolValue != value)
                {
                    GlobalToggleSettingProperty.BoolValue = value;
                    OnPropertyChanged();
                    foreach (var propSetting in SettingProperties)
                    {
                        propSetting.OnPropertyChanged("IsEnabled");
                        propSetting.OnPropertyChanged("IsSettingVisible");
                        OnPropertyChanged("GroupNameDisplay");
                    }
                }
            }
        }
        [DataSourceProperty]
        public bool HasGlobalToggle => GlobalToggleSettingProperty != null;

        public SettingPropertyGroup(SettingPropertyGroupAttribute attribute)
        {
            Attribute = attribute;
        }

        public override void RefreshValues()
        {
            base.RefreshValues();
            foreach (var setting in SettingProperties)
                setting.RefreshValues();

            OnPropertyChanged("GroupNameDisplay");
        }

        public void Add(SettingProperty sp)
        {
            SettingProperties.Add(sp);
            sp.Group = this;

            if (sp.GroupAttribute.IsMainToggle)
            {
                Attribute = sp.GroupAttribute;
                GlobalToggleSettingProperty = sp;
            }
        }

        public void AssignUndoRedoStack(UndoRedoStack urs)
        {
            URS = urs;
            foreach (var settingProp in SettingProperties)
                settingProp.AssignUndoRedoStack(urs);
        }

        public void SetParent(ModSettingsScreenVM parent)
        {
            Parent = parent;
            foreach (var settingProperty in SettingProperties)
                settingProperty.SetParent(Parent);
        }

        private void OnHover()
        {
            if (Parent != null && !string.IsNullOrWhiteSpace(HintText))
                Parent.HintText = HintText;
        }

        private void OnHoverEnd()
        {
            if (Parent != null)
                Parent.HintText = "";
        }
    }
}
