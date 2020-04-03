using HarmonyLib;
using SandBox.TournamentMissions.Missions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BannerlordTweaks
{
    [HarmonyPatch(typeof(TournamentBehavior), "OnPlayerWinMatch")]
    public class TournamentBehaviourPatch
    {
        static void Postfix(TournamentBehavior __instance)
        {
            typeof(TournamentBehavior).GetProperty("OverallExpectedDenars").SetValue(__instance, __instance.OverallExpectedDenars + 100);
        }
    }
}
