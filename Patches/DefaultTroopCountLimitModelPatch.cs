using HarmonyLib;
using System;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;

namespace BannerlordTweaks.Patches
{
    [HarmonyPatch(typeof(DefaultTroopCountLimitModel), "GetHideoutBattlePlayerMaxTroopCount")]
    public class DefaultTroopCountLimitModelPatch
    {
        static bool Prefix(ref int __result)
        {
            __result = Math.Min(Settings.Instance.HideoutBattleTroopLimit, 90);
            return false;
        }

        static bool Prepare()
        {
            return Settings.Instance.HideoutBattleTroopLimitTweakEnabled;
        }
    }
}
