using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace BannerlordTweaks
{
    public class SubModule : MBSubModuleBase
    {
        public static readonly string ModuleFolderName = "BannerlordTweaks";
        private static Harmony? harmony = null;

        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            if (harmony == null)
            {
                try
                {
                    harmony = new Harmony("mod.bannerlord.tweaks");
                    harmony.PatchAll();

                    if (BannerlordTweaksSettings.Instance.BattleSizeTweakEnabled)
                        BannerlordConfig.BattleSize = BannerlordTweaksSettings.Instance.BattleSize;
                    DebugHelpers.ColorOrangeMessage("Bannerlord Tweaks Loaded");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error Initialising Bannerlord Tweaks:\n\n{ex.ToStringFull()}");
                }
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
                if (BannerlordTweaksSettings.Instance.TroopBattleExperienceMultiplierEnabled || BannerlordTweaksSettings.Instance.ArenaHeroExperienceMultiplierEnabled || BannerlordTweaksSettings.Instance.TournamentHeroExperienceMultiplierEnabled)
                    gameStarter.AddModel(new TweakedCombatXpModel());
                if (BannerlordTweaksSettings.Instance.MaxWorkshopCountTweakEnabled || BannerlordTweaksSettings.Instance.WorkshopBuyingCostTweakEnabled)
                    gameStarter.AddModel(new TweakedWorkshopModel());
                if (BannerlordTweaksSettings.Instance.CompanionLimitTweakEnabled || BannerlordTweaksSettings.Instance.ClanPartiesLimitTweakEnabled)
                    gameStarter.AddModel(new TweakedClanTierModel());
                if (BannerlordTweaksSettings.Instance.SettlementMilitiaBonusEnabled)
                    gameStarter.AddModel(new TweakedSettlementMilitiaModel());
                if (BannerlordTweaksSettings.Instance.SettlementFoodBonusEnabled)
                    gameStarter.AddModel(new TweakedSettlementFoodModel());
                if (BannerlordTweaksSettings.Instance.SiegeCasualtiesTweakEnabled || BannerlordTweaksSettings.Instance.SiegeConstructionProgressPerDayMultiplierEnabled)
                    gameStarter.AddModel(new TweakedSiegeEventModel());
                if (BannerlordTweaksSettings.Instance.NoStillbirthsTweakEnabled || BannerlordTweaksSettings.Instance.NoMaternalMortalityTweakEnabled ||
                        BannerlordTweaksSettings.Instance.PregnancyDurationTweakEnabled || BannerlordTweaksSettings.Instance.FemaleOffspringProbabilityTweakEnabled ||
                        BannerlordTweaksSettings.Instance.TwinsProbabilityTweakEnabled)
                    gameStarter.AddModel(new TweakedPregnancyModel());
                if (BannerlordTweaksSettings.Instance.AgeTweaksEnabled)
                {
                    TweakedAgeModel model = new TweakedAgeModel();
                    List<string> configErrors = model.GetConfigErrors().ToList();
                    if (configErrors.Any())
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine("There is a configuration error in the \'Age\' tweaks from Bannerlord Tweaks.");
                        sb.AppendLine("Please check the below errors and fix the age settings in the settings menu:");
                        sb.AppendLine();
                        foreach (var e in configErrors)
                            sb.AppendLine(e);
                        sb.AppendLine();
                        sb.AppendLine("The age tweaks will not be applied until these errors have been resolved.");
                        sb.Append("Note that this is only a warning message and not a crash.");

                        MessageBox.Show(sb.ToString(), "Configuration Error in Bannerlord Tweaks");
                    }
                    else
                        gameStarter.AddModel(new TweakedAgeModel());
                }
                if (BannerlordTweaksSettings.Instance.AttributeFocusPointTweakEnabled)
                    gameStarter.AddModel(new TweakedCharacterDevelopmentModel());
                if (BannerlordTweaksSettings.Instance.DifficultyTweaksEnabled)
                    gameStarter.AddModel(new TweakedDifficultyModel());
            }
        }

        public override bool DoLoading(Game game)
        {
            if (Campaign.Current != null)
            {
                if (BannerlordTweaksSettings.Instance.PrisonerImprisonmentTweakEnabled)
                    PrisonerImprisonmentTweak.Apply(Campaign.Current);
                if (BannerlordTweaksSettings.Instance.DailyTroopExperienceTweakEnabled)
                    DailyTroopExperienceTweak.Apply(Campaign.Current);
                if (BannerlordTweaksSettings.Instance.TweakedConspiracyQuestTimerEnabled)
                    ConspiracyQuestTimerTweak.Apply(Campaign.Current);
            }
            return base.DoLoading(game);
        }

        public override void OnMissionBehaviourInitialize(Mission mission)
        {
            if (mission == null) return;
            base.OnMissionBehaviourInitialize(mission);
        }

        public override void OnGameInitializationFinished(Game game)
        {
            base.OnGameInitializationFinished(game);
            if (Campaign.Current != null && BannerlordTweaksSettings.Instance.EnableMissingHeroFix)
            {
                try
                {
                    CampaignEvents.DailyTickEvent.AddNonSerializedListener(this, delegate
                    {
                        PrisonerImprisonmentTweak.DailyTick();
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error Initialising Missing Hero Fix:\n\n{ex.ToStringFull()}");
                }
            }
        }
    }
}

