using HarmonyLib;
using System;
using System.Runtime.CompilerServices;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Party;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace BannerlordTweaks.Patches
{
    [HarmonyPatch(typeof(DefaultPartyWageModel), "GetTotalWage")]
    public class DefaultPartyWageModelPatch
    {

        static void Postfix(ref int __result, MobileParty mobileParty)
        {
            try {
                if (BannerlordTweaksSettings.Instance.PartyWageTweaksEnabled && mobileParty != null)
                {
                    if (mobileParty.IsMainParty || (mobileParty.Party.MapFaction == Hero.MainHero.MapFaction && BannerlordTweaksSettings.Instance.ApplyWageTweakToFaction && !mobileParty.IsGarrison))
                    {
                        float num = BannerlordTweaksSettings.Instance.PartyWagePercent;
                        __result = MathF.Round(__result * num);
                    }
                    if (mobileParty.IsGarrison && mobileParty.Party.Owner == Hero.MainHero)
                    {
                        float num2 = BannerlordTweaksSettings.Instance.GarrisonWagePercent;
                        __result = MathF.Round(__result * num2);
                        // Debug
                        // DebugHelpers.DebugMessage("Adjusted garrison " + mobileParty.Name + "by " + num2 + ". Value: " + __result);
                    }
                    /* Debug
                    DebugHelpers.DebugMessage("Relevant data: mobileParty.Party.Owner:" + mobileParty.Party.Owner + "\nmobileParty.Party.MapFaction:" + mobileParty.Party.MapFaction + "\nmobileParty.Party.LeaderHero:" + mobileParty.Party.LeaderHero + "\nmobileParty.Party.Leader" + mobileParty.Party.Leader);
                    */
                }
            }
            catch (Exception ex)
            {
                DebugHelpers.ShowError("An exception occurred whilst trying to apply Wage Patch.", "", ex);
            }
        }

        static bool Prepare()
        {
            return BannerlordTweaksSettings.Instance.PartyWageTweaksEnabled;
        }
    }
}
