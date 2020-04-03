using HarmonyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;

namespace BannerlordTweaks
{
    [HarmonyPatch(typeof(CraftingCampaignBehavior), "GetMaxHeroCraftingStamina")]
    public class GetMaxHeroCraftingStaminaPatch
    {
        static bool Prefix(CraftingCampaignBehavior __instance, ref int __result)
        {
            __result = 400;
            return false;
        }
    }

    [HarmonyPatch(typeof(CraftingCampaignBehavior), "HourlyTick")]
    public class HourlyTickPatch
    {
        static bool Prefix(CraftingCampaignBehavior __instance)
        {
            try
            {
                IDictionary records = (IDictionary)(typeof(CraftingCampaignBehavior).GetField("_heroCraftingRecords", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(__instance));
                foreach (Hero hero in records.Keys)
                {
                    int curCraftingStamina = __instance.GetHeroCraftingStamina(hero);

                    if (curCraftingStamina < 400 && hero.PartyBelongedTo?.CurrentSettlement != null)
                    {
                        __instance.SetHeroCraftingStamina(hero, Math.Min(400, curCraftingStamina + 10));
                    }
                    //MessageBox.Show($"Hero: {hero.Name}\n\nCrafting Stamina: {__instance.GetHeroCraftingStamina(hero)}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An exception happened during the HourlyTick patch:\n\n{ex.Message}\n\n{ex.InnerException?.Message}", "Exception");
            }
            return false;
        }
    }
}
