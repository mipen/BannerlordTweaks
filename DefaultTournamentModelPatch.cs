using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;

namespace BannerlordTweaks
{
    [HarmonyPatch(typeof(DefaultTournamentModel), "GetRenownReward")]
    public class DefaultTournamentModelPatch
    {
        static bool Prefix(Hero winner, ref int __result)
        {
            __result = 8;
            return false;
        }
    }
}
