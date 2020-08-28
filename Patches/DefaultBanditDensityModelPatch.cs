using HarmonyLib;
using System;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Map;

// Replaces DefaultTroopCoundLimitModelPatch as the method was removed in 1.4.3.
namespace BannerlordTweaks.Patches
{
    [HarmonyPatch(typeof(DefaultBanditDensityModel), "get_PlayerMaximumTroopCountForHideoutMission")]
    public class DefaultBanditDensityModelPatch
    {
        static bool Prefix(ref int __result)
        {
            __result = Math.Min(BannerlordTweaksSettings.Instance.HideoutBattleTroopLimit, 90);
            return false;
        }

        static bool Prepare()
        {
            return BannerlordTweaksSettings.Instance.HideoutBattleTroopLimitTweakEnabled;
        }
    }
}
