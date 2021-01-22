﻿using HarmonyLib;
using System;
using System.Collections;
using System.Reflection;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;
using TaleWorlds.Core;

namespace BannerlordTweaks.Patches
{
    [HarmonyPatch(typeof(CraftingCampaignBehavior), "DoSmelting")]
    public class DoSmeltingPatch
    {
        private static MethodInfo? openPartMethodInfo;

        static void Postfix(CraftingCampaignBehavior __instance, EquipmentElement equipmentElement)
        {
            ItemObject item = equipmentElement.Item;
            if (item == null) return;
            if (__instance == null) throw new ArgumentNullException(nameof(__instance), $"Tried to run postfix for {nameof(CraftingCampaignBehavior)}.DoSmelting but the instance was null.");
            if (openPartMethodInfo == null) GetMethodInfo();
            foreach (CraftingPiece piece in SmeltingHelper.GetNewPartsFromSmelting(item))
            {
                if (piece != null && piece.Name != null && openPartMethodInfo != null)
                    openPartMethodInfo.Invoke(__instance, new object[] { piece });
            }
        }

        static bool Prepare()
        {
            if (BannerlordTweaksSettings.Instance is { } settings)
            {
                if (settings.AutoLearnSmeltedParts)
                    GetMethodInfo();
                return settings.AutoLearnSmeltedParts;
            }
            else return false;
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

            //__result = BannerlordTweaksSettings.Instance.MaxCraftingStamina;
            __result = BannerlordTweaksSettings.Instance is { } settings ? settings.MaxCraftingStamina : 100;
            return false;
        }

        static bool Prepare() => BannerlordTweaksSettings.Instance is { } settings && settings.CraftingStaminaTweakEnabled;
    }

    [HarmonyPatch(typeof(CraftingCampaignBehavior), "HourlyTick")]
    public class HourlyTickPatch
    {
        private static FieldInfo? recordsInfo;

        static bool Prefix(CraftingCampaignBehavior __instance)
        {
            if (recordsInfo == null)
                GetRecordsInfo();
            //Get the list of hero records
            if (recordsInfo == null || __instance == null) throw new ArgumentNullException(nameof(__instance), $"Tried to run postfix for {nameof(CraftingCampaignBehavior)}.HourlyTickPatch but the recordsInfo or __instance was null.");

            IDictionary records = (IDictionary)recordsInfo.GetValue(__instance);

            foreach (Hero hero in records.Keys)
            {
                int curCraftingStamina = __instance.GetHeroCraftingStamina(hero);

                if (BannerlordTweaksSettings.Instance is not null && curCraftingStamina < BannerlordTweaksSettings.Instance.MaxCraftingStamina)
                {
                    int staminaGainAmount = BannerlordTweaksSettings.Instance.CraftingStaminaGainAmount;

                    if (BannerlordTweaksSettings.Instance.CraftingStaminaGainOutsideSettlementMultiplier < 1 && hero.PartyBelongedTo?.CurrentSettlement == null)
                        staminaGainAmount = (int)Math.Ceiling(staminaGainAmount * BannerlordTweaksSettings.Instance.CraftingStaminaGainOutsideSettlementMultiplier);

                    int diff = BannerlordTweaksSettings.Instance.MaxCraftingStamina - curCraftingStamina;
                    if (diff < staminaGainAmount)
                        staminaGainAmount = diff;

                    __instance.SetHeroCraftingStamina(hero, Math.Min(BannerlordTweaksSettings.Instance.MaxCraftingStamina, curCraftingStamina + staminaGainAmount));
                }
            }
            return false;
        }

        static bool Prepare()
        {
            if (BannerlordTweaksSettings.Instance is { } settings)
            {
                if (settings.CraftingStaminaTweakEnabled)
                    GetRecordsInfo();
                return settings.CraftingStaminaTweakEnabled;
            }
            else return false;
        }
 
        private static void GetRecordsInfo()
        {
            recordsInfo = typeof(CraftingCampaignBehavior).GetField("_heroCraftingRecords", BindingFlags.Instance | BindingFlags.NonPublic);
        }
    }
}
