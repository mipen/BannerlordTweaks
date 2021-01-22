﻿using HarmonyLib;
using SandBox.Source.Missions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Reflection;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace BannerlordTweaks.Patches
{
    [HarmonyPatch(typeof(HideoutMissionController), "IsSideDepleted")]
    public class IsSideDepletedPatch
    {
        public static bool Notified { get; set; } = false;
        public static bool Yelled { get; set; } = false;
        public static bool Dueled { get; set; } = false;

        static void Postfix(HideoutMissionController __instance, ref bool __result, BattleSideEnum side, ref int ____hideoutMissionState, Team ____enemyTeam)
        {
            //Only do something if the side was reported as being depleted and it is the attacker side
            if (__result && side == BattleSideEnum.Attacker)
            {
                try
                {
                    if (HasTroopsRemaining(__instance, side))
                    {
                        if (PlayerIsDead())
                        {
                            //If the player died during the boss fight
                            if (____hideoutMissionState == 5 || ____hideoutMissionState == 6)
                            {
                                if (BannerlordTweaksSettings.Instance.ContinueHideoutBattleOnPlayerLoseDuel)
                                {
                                    if (!Notified)
                                    {
                                        //Tell troops to charge on both sides
                                        SetTeamsHostile(__instance, ____enemyTeam);
                                        FreeAgentsToMove(__instance);
                                        TryAlarmAgents(__instance);
                                        MakeAgentsYell(__instance);
                                        TrySetFormationsCharge(__instance, BattleSideEnum.Attacker);
                                        TrySetFormationsCharge(__instance, BattleSideEnum.Defender);
                                        InformationManager.DisplayMessage(new InformationMessage("You have lost the duel! Your men are avenging your defeat!"));
                                        Notified = true;
                                        Dueled = true;
                                    }

                                    if (____hideoutMissionState != 6)
                                        ____hideoutMissionState = 6;

                                    __result = false;
                                }
                            }
                            else
                            {
                                //The player died during the initial battle phase
                                if (BannerlordTweaksSettings.Instance.ContinueHideoutBattleOnPlayerDeath && !Dueled)
                                {
                                    if (!Notified)
                                    {
                                        //The player is dead, but has troops remaining. We need to tell all remaining troops to charge, then report side is not depleted.
                                        TrySetFormationsCharge(__instance, BattleSideEnum.Attacker);
                                        MakeAgentsYell(__instance, BattleSideEnum.Attacker);
                                        InformationManager.DisplayMessage(new InformationMessage("You have fallen in the attack. Your troops are charging to avenge you!"));
                                        Notified = true;
                                    }

                                    //Disable the boss fight
                                    if (____hideoutMissionState != 1 && ____hideoutMissionState != 6)
                                        ____hideoutMissionState = 1;

                                    __result = false;
                                }
                            }
                        }
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message + "\n" + exception.StackTrace + "\n" + exception.InnerException);
                }
            }
        }

        static bool Prepare()
        {
            //Patch if it is set to not lose on player death
            return BannerlordTweaksSettings.Instance.ContinueHideoutBattleOnPlayerDeath || BannerlordTweaksSettings.Instance.ContinueHideoutBattleOnPlayerLoseDuel;
        }

        private static bool HasTroopsRemaining(HideoutMissionController controller, BattleSideEnum side)
        {
            IList missionSides = (IList)typeof(HideoutMissionController).GetField("_missionSides", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(controller);
            var mSide = missionSides[(int)side];
            int numTroops = (int)typeof(HideoutMissionController).GetNestedType("MissionSide", BindingFlags.NonPublic)
                .GetProperty("NumberOfActiveTroops", BindingFlags.Public | BindingFlags.Instance).GetValue(mSide);
            return numTroops > 0;
        }

        private static bool PlayerIsDead()
        {
            return Agent.Main == null || !Agent.Main.IsActive();
        }

        private static void TrySetFormationsCharge(HideoutMissionController controller, BattleSideEnum side)
        {
            List<Team> teams = (from t in controller.Mission.Teams
                                where t.Side == side
                                select t).ToList();
            if (teams != null && teams.Count > 0)
            {
                foreach (var team in teams)
                {
                    foreach (var formation in team.Formations)
                    {
                        if (formation.MovementOrder.OrderType != OrderType.Charge)
                            formation.MovementOrder = MovementOrder.MovementOrderCharge;
                    }
                }
            }
        }

        private static void TryAlarmAgents(HideoutMissionController controller)
        {
            foreach (var agent in controller.Mission.Agents)
            {
                if (agent.IsAIControlled && !agent.IsAlarmed())
                {
                    agent.SetWatchState(AgentAIStateFlagComponent.WatchState.Alarmed);
                }
            }
        }

        private static void MakeAgentsYell(HideoutMissionController controller, BattleSideEnum side)
        {
            foreach (var agent in controller.Mission.Agents)
            {
                if (agent.IsActive() && agent.Team.Side == side)
                    agent.SetWantsToYell();
            }
        }

        private static void MakeAgentsYell(HideoutMissionController controller)
        {
            MakeAgentsYell(controller, BattleSideEnum.Attacker);
            MakeAgentsYell(controller, BattleSideEnum.Defender);
        }

        private static void SetTeamsHostile(HideoutMissionController controller, Team enemyTeam)
        {
            Team passivePlayerTeam = controller.Mission.Teams.Where((x) => x.Side == BattleSideEnum.None && x.Banner == controller.Mission.PlayerTeam.Banner).FirstOrDefault();
            Team passiveEnemyTeam = controller.Mission.Teams.Where((x) => x.Side == BattleSideEnum.None && x.Banner == enemyTeam.Banner).FirstOrDefault();

            if (passivePlayerTeam != null)
            {
                List<Agent> list = new List<Agent>(passivePlayerTeam.ActiveAgents);
                foreach (var agent in list)
                {
                    agent.SetTeam(controller.Mission.Teams.Attacker, true);
                }
            }
            if (passiveEnemyTeam != null)
            {
                List<Agent> list = new List<Agent>(passiveEnemyTeam.ActiveAgents);
                foreach (var agent in list)
                {
                    agent.SetTeam(controller.Mission.Teams.Defender, true);
                }
            }
            controller.Mission.Teams.Attacker.SetIsEnemyOf(controller.Mission.Teams.Defender, true);
        }

        private static void FreeAgentsToMove(HideoutMissionController controller)
        {
            foreach (var agent in controller.Mission.Agents)
            {
                if (agent.IsActive())
                    agent.DisableScriptedMovement();
            }
        }
    }

    [HarmonyPatch(typeof(HideoutMissionController), "InitializeMission")]
    public class InitializeMissionPatch
    {
        static void Postfix()
        {
            IsSideDepletedPatch.Notified = false;
            IsSideDepletedPatch.Yelled = false;
            IsSideDepletedPatch.Dueled = false;
        }

        static bool Prepare()
        {
            //Patch if it is set to not lose on player death
            return BannerlordTweaksSettings.Instance.ContinueHideoutBattleOnPlayerDeath || BannerlordTweaksSettings.Instance.ContinueHideoutBattleOnPlayerLoseDuel;
        }
    }
}
