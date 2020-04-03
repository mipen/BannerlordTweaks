using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaleWorlds.MountAndBlade;

namespace BannerlordTweaks
{
    public class SubModule : MBSubModuleBase
    {
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
            try
            {
                var harmony = new Harmony("mod.bannerlord.mipen");
                harmony.PatchAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error patching:\n{ex.Message} \n\n{ex.InnerException?.Message}");
            }
        }
    }
}
