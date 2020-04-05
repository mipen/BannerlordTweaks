using HarmonyLib;
using SandBox.TournamentMissions.Missions;

namespace BannerlordTweaks.Patches
{
    [HarmonyPatch(typeof(TournamentBehavior), "OnPlayerWinTournament")]
    public class TournamentBehaviourPatch
    {
        static bool Prefix(TournamentBehavior __instance)
        {
            typeof(TournamentBehavior).GetProperty("OverallExpectedDenars").SetValue(__instance, __instance.OverallExpectedDenars + Settings.Instance.TournamentGoldRewardAmount);
            return true;
        }

        static bool Prepare()
        {
            return Settings.Instance.TournamentGoldRewardEnabled;
        }
    }
}
