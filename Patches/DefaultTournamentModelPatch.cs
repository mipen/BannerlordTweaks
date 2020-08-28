using HarmonyLib;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;

namespace BannerlordTweaks.Patches
{
    [HarmonyPatch(typeof(DefaultTournamentModel), "GetRenownReward")]
    public class DefaultTournamentModelPatch
    {
        static bool Prefix(ref int __result)
        {
            __result = BannerlordTweaksSettings.Instance.TournamentRenownAmount;
            return false;
        }

        static bool Prepare()
        {
            return BannerlordTweaksSettings.Instance.TournamentRenownIncreaseEnabled;
        }
    }
}
