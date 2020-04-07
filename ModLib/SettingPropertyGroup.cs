using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Library;

namespace ModLib
{
    public class SettingPropertyGroup : ViewModel
    {
        public const string DefaultGroupName = "Ungrouped";

        [DataSourceProperty]
        public string GroupName { get; private set; }
        [DataSourceProperty]
        public MBBindingList<SettingProperty> SettingProperties { get; } = new MBBindingList<SettingProperty>();

        public SettingPropertyGroup(string groupName)
        {
            GroupName = groupName;
        }

        public void Add(SettingProperty sp)
        {
            SettingProperties.Add(sp);
        }
    }
}
