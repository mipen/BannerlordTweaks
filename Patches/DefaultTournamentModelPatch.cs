using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;

namespace BannerlordTweaks.Patches
{
    [HarmonyPatch(typeof(DefaultTournamentModel), "GetRenownReward")]
    public class DefaultTournamentModelPatch
    {
        static bool Prefix(Hero winner, ref int __result)
        {
            __result = Settings.Instance.TournamentRenownAmount;
            return false;
        }

        static bool Prepare()
        {
            return Settings.Instance.TournamentRenownIncreaseEnabled;
        }
    }
}
