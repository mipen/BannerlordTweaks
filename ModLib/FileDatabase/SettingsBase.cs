using ModLib.Attributes;
using ModLib.GUI.ViewModels;
using ModLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ModLib
{
    public abstract class SettingsBase : ISerialisableFile, ISubFolder
    {
        public abstract string ID { get; set; }
        public abstract string ModuleFolderName { get; }
        public abstract string ModName { get; }
        public virtual string SubFolder => "";

        public List<SettingPropertyGroup> GetSettingPropertyGroups()
        {
            var groups = new List<SettingPropertyGroup>();

            var propList = (from p in GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                            let propAttr = p.GetCustomAttribute<SettingPropertyAttribute>(true)
                            let groupAttr = p.GetCustomAttribute<SettingPropertyGroupAttribute>(true)
                            where propAttr != null
                            let groupAttrToAdd = groupAttr == null ? SettingPropertyGroupAttribute.Default : groupAttr
                            select new SettingProperty(propAttr, groupAttrToAdd, p, this)).ToList();


            foreach (var settingProp in propList)
            {
                CheckIsValid(settingProp);
                SettingPropertyGroup group = GetGroupFor(settingProp, groups);
                group.Add(settingProp);
            }

            SettingPropertyGroup noneGroup = GetGroupFor(groups, SettingPropertyGroup.DefaultGroupName);
            if (noneGroup != null && groups.Count > 1)
                groups.Remove(noneGroup);
            else
                noneGroup = null;

            groups.Sort((x, y) => x.GroupName.CompareTo(y.GroupName));
            if (noneGroup != null)
                groups.Add(noneGroup);

            return groups;
        }

        private SettingPropertyGroup GetGroupFor(SettingProperty sp, List<SettingPropertyGroup> groupsList)
        {
            if (sp.GroupAttribute == null)
                throw new Exception($"SettingProperty {sp.Name} has null GroupAttribute");

            SettingPropertyGroup group = groupsList.Where((x) => x.GroupName == sp.GroupAttribute.GroupName).FirstOrDefault();
            if (group == null)
            {
                group = new SettingPropertyGroup(sp.GroupAttribute);
                groupsList.Add(group);
            }
            return group;
        }

        private SettingPropertyGroup GetGroupFor(List<SettingPropertyGroup> groupsList, string groupName)
        {
            return groupsList.Where((x) => x.GroupName == groupName).FirstOrDefault();
        }

        private void CheckIsValid(SettingProperty prop)
        {
            if (!prop.Property.CanRead)
                throw new Exception($"Property {prop.Property.Name} in {prop.SettingsInstance.GetType().FullName} must have a getter.");
            if (!prop.Property.CanWrite)
                throw new Exception($"Property {prop.Property.Name} in {prop.SettingsInstance.GetType().FullName} must have a setter.");
            if (prop.SettingType == SettingType.Int || prop.SettingType == SettingType.Float)
            {
                if (prop.MinValue == prop.MaxValue)
                    throw new Exception($"Property {prop.Property.Name} in {prop.SettingsInstance.GetType().FullName} is a numeric type but the MinValue and MaxValue are the same.");
            }
        }
    }
}
