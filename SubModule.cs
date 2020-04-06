﻿using ModLib;
using HarmonyLib;
using SandBox;
using SandBox.TournamentMissions.Missions;
using System;
using System.Reflection;
using System.Windows.Forms;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using System.Linq;

namespace BannerlordTweaks
{
    public class SubModule : MBSubModuleBase
    {
        public static readonly string ModuleName = "zzBannerlordTweaks";

        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
            try
            {
                Loader.Initialise(ModuleName);

                var harmony = new Harmony("mod.bannerlord.mipen");
                harmony.PatchAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error Initialising Bannerlord Tweaks:\n\n{ex.ToStringFull()}");
            }
        }

        public override void OnMissionBehaviourInitialize(Mission mission)
        {
            base.OnMissionBehaviourInitialize(mission);

            if (Settings.Instance.TournamentExperienceEnabled && !mission.HasMissionBehaviour<TournamentExperienceMissionLogic>() &&
                (mission.HasMissionBehaviour<TournamentFightMissionController>() || mission.HasMissionBehaviour<TournamentArcheryMissionController>() ||
                mission.HasMissionBehaviour<TournamentArcheryMissionController>() || mission.HasMissionBehaviour<TownHorseRaceMissionController>()))
            {
                mission.AddMissionBehaviour(new TournamentExperienceMissionLogic());
            }

            if (Settings.Instance.ArenaExperienceEnabled && !mission.HasMissionBehaviour<TournamentExperienceMissionLogic>() &&
                mission.HasMissionBehaviour<ArenaPracticeFightMissionController>())
            {
                mission.AddMissionBehaviour(new TournamentExperienceMissionLogic());
            }
        }

        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            base.OnGameStart(game, gameStarterObject);

            AddModels(gameStarterObject as CampaignGameStarter);
        }

        private void AddModels(CampaignGameStarter gameStarter)
        {
            if (gameStarter != null)
            {
                if (Settings.Instance.TroopBattleExperienceMultiplierEnabled)
                    gameStarter.AddModel(new TweakedCombatXpModel());
                if (Settings.Instance.MaxWorkshopCountTweakEnabled || Settings.Instance.WorkshopBuyingCostTweakEnabled)
                    gameStarter.AddModel(new TweakedWorkshopModel());
                if (Settings.Instance.CompanionLimitTweakEnabled || Settings.Instance.ClanPartiesLimitTweakEnabled)
                    gameStarter.AddModel(new TweakedClanTierModel());
                if (Settings.Instance.SettlementMilitiaBonusEnabled)
                    gameStarter.AddModel(new TweakedSettlementMilitiaModel());
                if (Settings.Instance.SettlementFoodBonusEnabled)
                    gameStarter.AddModel(new TweakedSettlementFoodModel());
                if (Settings.Instance.SiegeCasualtiesTweakEnabled || Settings.Instance.SiegeConstructionProgressPerDayMultiplierEnabled)
                    gameStarter.AddModel(new TweakedSiegeEventModel());
            }
        }
    }
}
