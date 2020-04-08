using ModLib.Attributes;
using ModLib.Interfaces;
using System;
using System.Reflection;
using TaleWorlds.Library;

namespace ModLib.GUI.ViewModels
{
    public class SettingProperty : ViewModel
    {
        private float _floatValue = 0f;
        private int _intValue = 0;
        private bool initialising = false;

        public SettingPropertyAttribute SettingAttribute { get; private set; }
        public PropertyInfo Property { get; private set; }
        public SettingPropertyGroupAttribute GroupAttribute { get; private set; }
        public SettingPropertyGroup Group { get; set; }
        public ISerialisableFile SettingsInstance { get; private set; }
        public SettingType SettingType { get; private set; }
        public UndoRedoStack URS { get; private set; }
        public ModSettingsScreenVM Parent { get; private set; }
        public string HintText { get; private set; }

        [DataSourceProperty]
        public string Name => SettingAttribute.DisplayName;

        [DataSourceProperty]
        public bool IsIntVisible => SettingType == SettingType.Int;
        [DataSourceProperty]
        public bool IsFloatVisible => SettingType == SettingType.Float;
        [DataSourceProperty]
        public bool IsBoolVisible { get => SettingType == SettingType.Bool; set { } }
        [DataSourceProperty]
        public bool IsEnabled
        {
            get
            {
                if (Group == null)
                    return true;
                return Group.GlobalToggle;
            }
        }
        [DataSourceProperty]
        public bool IsSettingVisible
        {
            get
            {
                if (Group != null && GroupAttribute != null && GroupAttribute.IsMainToggle)
                    return false;
                else if (!Group.GlobalToggle)
                    return false;
                else
                    return true;
            }
        }

        [DataSourceProperty]
        public float FloatValue
        {
            get
            {
                return _floatValue;
            }
            set
            {
                if (SettingType == SettingType.Float && _floatValue != value)
                {
                    _floatValue = (float)Math.Round((double)value, 2, MidpointRounding.ToEven);
                    OnPropertyChanged();
                    OnPropertyChanged("ValueString");
                }
            }
        }
        [DataSourceProperty]
        public int IntValue
        {
            get
            {
                return _intValue;
            }
            set
            {
                if (SettingType == SettingType.Int)
                {
                    _intValue = value;
                    OnPropertyChanged();
                    OnPropertyChanged("ValueString");
                }
            }
        }
        [DataSourceProperty]
        public float FinalisedFloatValue
        {
            get => 0;
            set
            {
                if ((float)Property.GetValue(SettingsInstance) != value && !initialising)
                {
                    URS.Do(new SetValueAction<float>(new Ref(Property, SettingsInstance), (float)Math.Round((double)value, 2, MidpointRounding.ToEven)));
                }
            }
        }
        [DataSourceProperty]
        public int FinalisedIntValue
        {
            get => 0;
            set
            {
                if ((int)Property.GetValue(SettingsInstance) != value && !initialising)
                {
                    URS.Do(new SetValueAction<int>(new Ref(Property, SettingsInstance), value));
                }
            }
        }
        [DataSourceProperty]
        public bool BoolValue
        {
            get
            {
                if (SettingType == SettingType.Bool)
                    return (bool)Property.GetValue(SettingsInstance);
                else
                    return false;
            }
            set
            {
                if (SettingType == SettingType.Bool)
                {
                    if (BoolValue != value)
                    {
                        URS.Do(new SetValueAction<bool>(new Ref(Property, SettingsInstance), value));
                        //Property.SetValue(SettingsInstance, value);
                        OnPropertyChanged();
                    }
                }
            }
        }
        [DataSourceProperty]
        public float MaxValue => SettingAttribute.MaxValue;
        [DataSourceProperty]
        public float MinValue => SettingAttribute.MinValue;
        [DataSourceProperty]
        public string ValueString
        {
            get
            {
                if (SettingType == SettingType.Int)
                    return IntValue.ToString();
                else if (SettingType == SettingType.Float)
                    return FloatValue.ToString("0.00");
                else
                    return "";
            }
        }

        public SettingProperty(SettingPropertyAttribute settingAttribute, SettingPropertyGroupAttribute groupAttribute, PropertyInfo property, ISerialisableFile instance)
        {
            SettingAttribute = settingAttribute;
            GroupAttribute = groupAttribute;

            Property = property;
            SettingsInstance = instance;

            RefreshValues();
        }

        public override void RefreshValues()
        {
            base.RefreshValues();
            initialising = true;
            SetType();
            if (!string.IsNullOrWhiteSpace(SettingAttribute.HintText))
                HintText = $"{Name}: {SettingAttribute.HintText}";

            if (SettingType == SettingType.Float)
                FloatValue = (float)Property.GetValue(SettingsInstance);
            else if (SettingType == SettingType.Int)
                IntValue = (int)Property.GetValue(SettingsInstance);
            initialising = false;
        }

        private void SetType()
        {
            if (Property.PropertyType == typeof(bool))
                SettingType = SettingType.Bool;
            else if (Property.PropertyType == typeof(int))
                SettingType = SettingType.Int;
            else if (Property.PropertyType == typeof(float))
                SettingType = SettingType.Float;
            else
                throw new Exception($"Property {Property.Name} in {SettingsInstance.GetType().FullName} has an invalid type.\nValid types are {string.Join(",", Enum.GetNames(typeof(SettingType)))}");
        }

        public void AssignUndoRedoStack(UndoRedoStack urs)
        {
            URS = urs;
        }

        public void SetParent(ModSettingsScreenVM parent)
        {
            Parent = parent;
        }

        public void OnHover()
        {
            if (Parent != null)
                Parent.HintText = HintText;
        }

        public void OnHoverEnd()
        {
            if (Parent != null)
                Parent.HintText = "";
        }
    }
}
