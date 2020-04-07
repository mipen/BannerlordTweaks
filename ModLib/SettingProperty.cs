using ModLib.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Library;

namespace ModLib
{
    public class SettingProperty : ViewModel
    {
        public SettingPropertyAttribute Attribute { get; private set; }
        public PropertyInfo Property { get; private set; }
        public SettingsBase SettingsInstance { get; private set; }
        public SettingType SettingType { get; private set; }

        [DataSourceProperty]
        public string Name => Attribute.DisplayName;
        [DataSourceProperty]
        public string Tooltip => Attribute.Tooltip;

        [DataSourceProperty]
        public bool IsIntVisible => SettingType == SettingType.Int;
        [DataSourceProperty]
        public bool IsFloatVisible => SettingType == SettingType.Float;
        [DataSourceProperty]
        public bool IsBoolVisible { get => SettingType == SettingType.Bool; set { } }
        [DataSourceProperty]
        public bool HasTooltip => !string.IsNullOrWhiteSpace(Tooltip);

        [DataSourceProperty]
        public float FloatValue
        {
            get
            {
                if (SettingType == SettingType.Float)
                    return (float)Property.GetValue(SettingsInstance);
                else
                    return 0f;
            }
            set
            {
                if (SettingType == SettingType.Float)
                {
                    Property.SetValue(SettingsInstance, value);
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
                if (SettingType == SettingType.Int)
                    return (int)Property.GetValue(SettingsInstance);
                else
                    return 0;
            }
            set
            {
                if (SettingType == SettingType.Int)
                {
                    Property.SetValue(SettingsInstance, value);
                    OnPropertyChanged();
                    OnPropertyChanged("ValueString");
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
                    Property.SetValue(SettingsInstance, value);
                    OnPropertyChanged();
                }
            }
        }
        [DataSourceProperty]
        public float MaxValue => Attribute.MaxValue;
        [DataSourceProperty]
        public float MinValue => Attribute.MinValue;
        [DataSourceProperty]
        public string ValueString
        {
            get
            {
                if (SettingType == SettingType.Int)
                    return IntValue.ToString();
                else if (SettingType == SettingType.Float)
                    return FloatValue.ToString();
                else
                    return "Shouldn't be here";
            }
        }

        public SettingProperty(SettingPropertyAttribute attribute, PropertyInfo property, SettingsBase instance)
        {
            Attribute = attribute;
            Property = property;
            SettingsInstance = instance;
            SetType();
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
    }
}
