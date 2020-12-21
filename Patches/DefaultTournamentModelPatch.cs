using HarmonyLib;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;

namespace BannerlordTweaks.Patches
{
    [HarmonyPatch(typeof(DefaultTournamentModel), "GetRenownReward")]
    public class DefaultTournamentModelPatch
    {
        static bool Prefix(ref int __result)
        {
            if (BannerlordTweaksSettings.Instance is not null)
            {
                __result = BannerlordTweaksSettings.Instance.TournamentRenownAmount;
                return false;
            }
            return true;
        }

        static bool Prepare() => BannerlordTweaksSettings.Instance is { } settings && settings.TournamentRenownIncreaseEnabled;
    }
}
