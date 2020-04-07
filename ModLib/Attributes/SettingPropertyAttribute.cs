using System;

namespace ModLib.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SettingPropertyAttribute : Attribute
    {
        public string DisplayName { get; private set; }
        public float MinValue { get; private set; }
        public float MaxValue { get; private set; }
        public string Tooltip { get; private set; }
        public string Group { get; private set; }

        public SettingPropertyAttribute(string displayName, float minValue, float maxValue, string tooltip = "", string group = "")
        {
            DisplayName = displayName;
            MinValue = minValue;
            MaxValue = maxValue;
            Tooltip = tooltip;
            Group = group;
        }

        public SettingPropertyAttribute(string displayName, string tooltip = "", string group = "") : this(displayName, 0f, 0f, tooltip, group)
        {

        }
    }
}
