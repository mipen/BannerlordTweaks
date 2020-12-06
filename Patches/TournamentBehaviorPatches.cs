using HarmonyLib;
using SandBox.TournamentMissions.Missions;
using System.Reflection;
using TaleWorlds.Library;

namespace BannerlordTweaks.Patches
{
    [HarmonyPatch(typeof(TournamentBehavior), "OnPlayerWinTournament")]
    public class OnPlayerWinTournamentPatch
    {
        static bool Prefix(TournamentBehavior __instance)
        {
            typeof(TournamentBehavior).GetProperty("OverallExpectedDenars").SetValue(__instance, __instance.OverallExpectedDenars + BannerlordTweaksSettings.Instance.TournamentGoldRewardAmount);
            return true;
        }

        static bool Prepare()
        {
            return BannerlordTweaksSettings.Instance.TournamentGoldRewardEnabled;
        }
    }

    [HarmonyPatch(typeof(TournamentBehavior), "CalculateBet")]
    public class CalculateBetPatch
    {
        private static PropertyInfo? betOddInfo = null;

        static void Postfix(TournamentBehavior __instance)
        {
            betOddInfo?.SetValue(__instance, MathF.Max((float)betOddInfo.GetValue(__instance), BannerlordTweaksSettings.Instance.MinimumBettingOdds, 0));
        }

        static bool Prepare()
        {
            if (BannerlordTweaksSettings.Instance.MinimumBettingOddsTweakEnabled)
            {
                betOddInfo = typeof(TournamentBehavior).GetProperty(nameof(TournamentBehavior.BetOdd), BindingFlags.Public | BindingFlags.Instance);
                return true;
            }
            else
                return false;
        }
    }
}
