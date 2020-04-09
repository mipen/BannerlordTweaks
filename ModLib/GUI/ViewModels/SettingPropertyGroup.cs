using ModLib.Attributes;
using TaleWorlds.Library;

namespace ModLib.GUI.ViewModels
{
    public class SettingPropertyGroup : ViewModel
    {
        public const string DefaultGroupName = "Misc";
        public SettingProperty GroupToggleSettingProperty { get; private set; } = null;
        public SettingPropertyGroupAttribute Attribute { get; private set; }
        public UndoRedoStack URS { get; private set; }
        public ModSettingsScreenVM Parent { get; private set; }
        public string HintText
        {
            get
            {
                if (GroupToggleSettingProperty != null && !string.IsNullOrWhiteSpace(GroupToggleSettingProperty.HintText))
                {
                    return $"{GroupToggleSettingProperty.HintText}";
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
                string addition = GroupToggle ? "" : "(Disabled)";
                return $"{Attribute.GroupName} {addition}";
            }
        }
        [DataSourceProperty]
        public MBBindingList<SettingProperty> SettingProperties { get; } = new MBBindingList<SettingProperty>();
        [DataSourceProperty]
        public bool GroupToggle
        {
            get
            {
                if (GroupToggleSettingProperty != null)
                    return GroupToggleSettingProperty.BoolValue;
                else
                    return true;
            }
            set
            {
                if (GroupToggleSettingProperty != null && GroupToggleSettingProperty.BoolValue != value)
                {
                    GroupToggleSettingProperty.BoolValue = value;
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
        public bool HasGroupToggle => GroupToggleSettingProperty != null;

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
                GroupToggleSettingProperty = sp;
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
