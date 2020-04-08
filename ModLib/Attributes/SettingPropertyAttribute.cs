using System;

namespace ModLib.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SettingPropertyAttribute : Attribute
    {
        public string DisplayName { get; private set; }
        public float MinValue { get; private set; }
        public float MaxValue { get; private set; }
        public string HintText { get; private set; }

        public SettingPropertyAttribute(string displayName, float minValue, float maxValue, string hintText = "")
        {
            DisplayName = displayName;
            MinValue = minValue;
            MaxValue = maxValue;
            HintText = hintText;
        }

        public SettingPropertyAttribute(string displayName, string tooltip = "") : this(displayName, 0f, 0f, tooltip)
        {

        }
    }
}
