using HarmonyLib;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Map;
using TaleWorlds.CampaignSystem;

namespace BannerlordTweaks.Patches
{
    [HarmonyPatch(typeof(DefaultPartyTrainingModel), "GetTroopPerksXp")]
    public class TroopPerksXpPatch
    {
        static bool Prefix(PerkObject perk, ref int __result)
        {
            if (perk == DefaultPerks.Leadership.CombatTips)
            {
                __result = Settings.Instance.TroopPerkCombatTipsAmount;
            }
            else if (perk == DefaultPerks.Leadership.RaiseTheMeek)
            {
                __result = Settings.Instance.TroopPerkRaiseTheMeekAmount;
            }
            return false;
        }

        static bool Prepare()
        {
            return Settings.Instance.TroopPerkXpAmountTweakEnabled;
        }
    }
}
