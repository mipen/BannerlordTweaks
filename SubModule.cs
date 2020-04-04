using BannerlordTweaks.Lib;
using HarmonyLib;
using SandBox;
using SandBox.TournamentMissions.Missions;
using System;
using System.Windows.Forms;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace BannerlordTweaks
{
    public class SubModule : MBSubModuleBase
    {
        public static readonly string ModuleName = "BannerlordTweaks";

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
                MessageBox.Show($"Error patching:\n{ex.Message} \n\n{ex.InnerException?.Message}");
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
            gameStarter.AddModel(new TweakedCombatXpModel());
            gameStarter.AddModel(new TweakedWorkshopModel());
            gameStarter.AddModel(new TweakedClanTierModel());
            gameStarter.AddModel(new TweakedSettlementMilitiaModel());
            gameStarter.AddModel(new TweakedSettlementFoodModel());
        }
    }
}
