using HarmonyLib;
using ModLib;
using System;
using System.Windows.Forms;
using TaleWorlds.CampaignSystem;
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
                if (Settings.Instance.AttributeFocusPointTweakEnabled)
                    gameStarter.AddModel(new TweakedDefaultCharacterDevelopmentModel());
            }
        }
    }
}
