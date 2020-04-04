using HarmonyLib;
using SandBox.TournamentMissions.Missions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
