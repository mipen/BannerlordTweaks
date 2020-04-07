using ModLib.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ModLib
{
    public abstract class SettingsBase
    {
        public abstract string ModName { get; }

        public List<SettingPropertyGroup> GetSettingPropertyGroups()
        {
            var list = (from p in GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                        let attr = p.GetCustomAttribute<SettingPropertyAttribute>(true)
                        where attr != null
                        select new SettingProperty(attr, p, this)).ToList();

            List<SettingPropertyGroup> groups = new List<SettingPropertyGroup>();

            foreach (var settingProp in list)
            {
                CheckIsValid(settingProp);
                GetGroupFor(settingProp, groups).Add(settingProp);
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
            string groupName = !string.IsNullOrWhiteSpace(sp.Attribute.Group) ? sp.Attribute.Group : SettingPropertyGroup.DefaultGroupName;
            SettingPropertyGroup group = groupsList.Where((x) => x.GroupName == groupName).FirstOrDefault();
            if (group == null)
            {
                group = new SettingPropertyGroup(groupName);
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
                throw new Exception($"Property {prop.Property.Name} in {GetType().FullName} must have a getter.");
            if (!prop.Property.CanWrite)
                throw new Exception($"Property {prop.Property.Name} in {GetType().FullName} must have a setter.");
        }
    }
}
