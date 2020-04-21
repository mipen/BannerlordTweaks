using HarmonyLib;
using ModLib;
using System;
using System.Collections;
using System.Reflection;
using System.Windows.Forms;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;
using TaleWorlds.Core;

namespace BannerlordTweaks.Patches
{
    [HarmonyPatch(typeof(CraftingCampaignBehavior), "DoSmelting")]
    public class DoSmeltingPatch
    {
        private static MethodInfo openPartMethodInfo;

        static void Postfix(CraftingCampaignBehavior __instance, ItemObject item)
        {
            if (item == null) return;
            if (__instance == null) throw new ArgumentNullException(nameof(__instance), $"Tried to run postfix for {nameof(CraftingCampaignBehavior)}.DoSmelting but the instance was null.");
            if (openPartMethodInfo == null) GetMethodInfo();
            foreach (CraftingPiece piece in SmeltingHelper.GetNewPartsFromSmelting(item))
            {
                if (piece != null && piece.Name != null)
                    openPartMethodInfo.Invoke(__instance, new object[] { piece });
            }
        }

        static bool Prepare()
        {
            if (Settings.Instance.AutoLearnSmeltedParts)
                GetMethodInfo();
            return Settings.Instance.AutoLearnSmeltedParts;
        }

        private static void GetMethodInfo()
        {
            openPartMethodInfo = typeof(CraftingCampaignBehavior).GetMethod("OpenPart", BindingFlags.NonPublic | BindingFlags.Instance);
        }
    }

    [HarmonyPatch(typeof(CraftingCampaignBehavior), "GetMaxHeroCraftingStamina")]
    public class GetMaxHeroCraftingStaminaPatch
    {
        static bool Prefix(CraftingCampaignBehavior __instance, ref int __result)
        {
            __result = Settings.Instance.MaxCraftingStamina;
            return false;
        }

        static bool Prepare()
        {
            return Settings.Instance.CraftingStaminaTweakEnabled;
        }
    }

    [HarmonyPatch(typeof(CraftingCampaignBehavior), "HourlyTick")]
    public class HourlyTickPatch
    {
        private static FieldInfo recordsInfo;

        static bool Prefix(CraftingCampaignBehavior __instance)
        {
            if (recordsInfo == null)
                GetRecordsInfo();
            //Get the list of hero records
            IDictionary records = (IDictionary)recordsInfo.GetValue(__instance);

            foreach (Hero hero in records.Keys)
            {
                int curCraftingStamina = __instance.GetHeroCraftingStamina(hero);

                if (curCraftingStamina < Settings.Instance.MaxCraftingStamina)
                {
                    int staminaGainAmount = Settings.Instance.CraftingStaminaGainAmount;

                    if (Settings.Instance.CraftingStaminaGainOutsideSettlementMultiplier < 1 && hero.PartyBelongedTo?.CurrentSettlement == null)
                        staminaGainAmount = (int)Math.Ceiling(staminaGainAmount * Settings.Instance.CraftingStaminaGainOutsideSettlementMultiplier);

                    int diff = Settings.Instance.MaxCraftingStamina - curCraftingStamina;
                    if (diff < staminaGainAmount)
                        staminaGainAmount = diff;

                    __instance.SetHeroCraftingStamina(hero, Math.Min(Settings.Instance.MaxCraftingStamina, curCraftingStamina + staminaGainAmount));
                }
            }
            return false;
        }

        static bool Prepare()
        {
            if (Settings.Instance.CraftingStaminaTweakEnabled)
                GetRecordsInfo();
            return Settings.Instance.CraftingStaminaTweakEnabled;
        }

        private static void GetRecordsInfo()
        {
            recordsInfo = typeof(CraftingCampaignBehavior).GetField("_heroCraftingRecords", BindingFlags.Instance | BindingFlags.NonPublic);
        }
    }
}
