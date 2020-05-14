using HarmonyLib;
using ModLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Encyclopedia;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace BannerlordTweaks
{
    public class SubModule : MBSubModuleBase
    {
        public static readonly string ModuleFolderName = "zzBannerlordTweaks";

        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            try
            {
                FileDatabase.Initialise(ModuleFolderName);

                var harmony = new Harmony("mod.bannerlord.mipen");
                harmony.PatchAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error Initialising Bannerlord Tweaks:\n\n{ex.ToStringFull()}");
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
                if (Settings.Instance.TroopBattleExperienceMultiplierEnabled || Settings.Instance.ArenaHeroExperienceMultiplierEnabled || Settings.Instance.TournamentHeroExperienceMultiplierEnabled)
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
                if (Settings.Instance.NoStillbirthsTweakEnabled || Settings.Instance.NoMaternalMortalityTweakEnabled ||
                        Settings.Instance.PregnancyDurationTweakEnabled || Settings.Instance.FemaleOffspringProbabilityTweakEnabled ||
                        Settings.Instance.TwinsProbabilityTweakEnabled)
                    gameStarter.AddModel(new TweakedPregnancyModel());
                if (Settings.Instance.AgeTweaksEnabled)
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
                if (Settings.Instance.AttributeFocusPointTweakEnabled)
                    gameStarter.AddModel(new TweakedCharacterDevelopmentModel());
            }
        }

        public override bool DoLoading(Game game)
        {
            if (Campaign.Current != null)
            {
                if (Settings.Instance.PrisonerImprisonmentTweakEnabled)
                    PrisonerImprisonmentTweak.Apply(Campaign.Current);
                if (Settings.Instance.DailyTroopExperienceTweakEnabled)
                    DailyTroopExperienceTweak.Apply(Campaign.Current);
            }
            return base.DoLoading(game);
        }
    }
}
