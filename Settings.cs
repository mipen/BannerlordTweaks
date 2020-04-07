using ModLib;
using ModLib.Interfaces;
using System;
using System.Xml.Serialization;

namespace BannerlordTweaks
{
    public class Settings : SettingsBase, ILoadable
    {
        private const string instanceID = "BannerlordTweaksSettings";
        private static Settings _instance = null;
        public override string ModName => "Bannerlord Tweaks";


        [XmlElement("ID")]
        public string ID { get; set; } = instanceID;
        [XmlElement]
        public bool DebugMode { get; set; } = true;

        public static Settings Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Loader.Get<Settings>(instanceID);
                    if (_instance == null)
                        throw new Exception("Unable to find Bannerlord Tweaks settings in Loader");
                }

                return _instance;
            }
        }

        #region Crafting stamina Settings
        [XmlElement]
        public bool CraftingStaminaTweakEnabled { get; set; } = true;
        [XmlElement]
        public int MaxCraftingStamina { get; set; } = 400;
        [XmlElement]
        public int CraftingStaminaGainAmount { get; set; } = 10;
        [XmlElement]
        public bool IgnoreCraftingStamina { get; set; } = false;
        [XmlElement]
        public float CraftingStaminaGainOutsideSettlementMultiplier { get; set; } = 1f;
        [XmlElement]
        public bool PreventSmeltingLockedItems { get; set; } = true;
        #endregion

        #region Battle reward patches
        [XmlElement]
        public bool BattleRenownMultiplierEnabled { get; set; } = true;
        [XmlElement]
        public float BattleRenownMultiplier { get; set; } = 2f;
        #endregion

        #region Party size patches
        [XmlElement]
        public bool LeadershipPartySizeBonusEnabled { get; set; } = true;
        [XmlElement]
        public float LeadershipPartySizeBonus { get; set; } = 0.3f;
        [XmlElement]
        public bool StewardPartySizeBonusEnabled { get; set; } = true;
        [XmlElement]
        public float StewardPartySizeBonus { get; set; } = 0.3f;
        #endregion

        # region Tournament patches
        [XmlElement]
        public bool TournamentRenownIncreaseEnabled { get; set; } = true;
        [XmlElement]
        public int TournamentRenownAmount { get; set; } = 8;
        [XmlElement]
        public bool TournamentGoldRewardEnabled { get; set; } = true;
        [XmlElement]
        public int TournamentGoldRewardAmount { get; set; } = 500;
        [XmlElement]
        public bool TournamentExperienceEnabled { get; set; } = false;
        [XmlElement]
        public bool ArenaExperienceEnabled { get; set; } = false;
        [XmlElement]
        public bool TournamentMaxBetAmountTweakEnabled { get; set; } = true;
        [XmlElement]
        public int TournamentMaxBetAmount { get; set; } = 500;
        #endregion

        #region Hero skill multiplier patch
        [XmlElement]
        public bool HeroSkillExperienceMultiplierEnabled { get; set; } = true;
        [XmlElement]
        public float HeroSkillExperienceOverrideMultiplier { get; set; } = -1;
        #endregion

        #region Hideout battle tweaks
        [XmlElement]
        public bool HideoutBattleTroopLimitTweakEnabled { get; set; } = true;
        [XmlElement]
        public int HideoutBattleTroopLimit { get; set; } = 9999;
        [XmlElement]
        public bool LoseHideoutBattleOnPlayerDeath { get; set; } = false;
        [XmlElement]
        public bool LoseHideoutBattleOnPlayerLoseDuel { get; set; } = false;
        #endregion

        #region Troop experience multiplier
        [XmlElement]
        public bool TroopBattleExperienceMultiplierEnabled { get; set; } = true;
        [XmlElement]
        public float TroopBattleExperienceMultiplier { get; set; } = 3.0f;
        [XmlElement]
        public bool TroopBattleSimulationExperienceMultiplierEnabled { get; set; } = false;
        [XmlElement]
        public float TroopBattleSimulationExperienceMultiplier { get; set; } = 1.0f;
        #endregion

        #region Workshop tweaks
        [XmlElement]
        public bool MaxWorkshopCountTweakEnabled { get; set; } = true;
        [XmlElement]
        public int BaseWorkshopCount { get; set; } = 2;
        [XmlElement]
        public int BonusWorkshopsPerClanTier { get; set; } = 1;
        [XmlElement]
        public bool WorkshopBuyingCostTweakEnabled { get; set; } = false;
        [XmlElement]
        public int WorkshopBaseCost { get; set; } = 10000;
        #endregion

        #region Companion limit tweak
        [XmlElement]
        public bool CompanionLimitTweakEnabled { get; set; } = false;
        [XmlElement]
        public int CompanionLimitBonusPerClanTier { get; set; } = 3;
        [XmlElement]
        public int CompanionBaseLimit { get; set; } = 3;
        #endregion

        #region Kingdom leave relation loss tweak
        [XmlElement]
        public bool LeaveKingdomRelationLossTweakEnabled { get; set; } = false;
        [XmlElement]
        public int LeaveKingdomRelationLossLeaderAmount { get; set; } = 20;
        [XmlElement]
        public int LeaveKingdomRelationLossVassalAmount { get; set; } = 5;
        #endregion

        #region Settlement militia bonus tweak
        [XmlElement]
        public bool SettlementMilitiaBonusEnabled { get; set; } = false;
        [XmlElement]
        public float CastleMilitiaBonus { get; set; } = 1.25f;
        [XmlElement]
        public float TownMilitiaBonus { get; set; } = 2.5f;

        [XmlElement]
        public bool SettlementMilitiaEliteSpawnRateBonusEnabled { get; set; } = true;
        [XmlElement]
        public float SettlementEliteMeleeSpawnRateBonus { get; set; } = 0.15f;
        [XmlElement]
        public float SettlementEliteRangedSpawnRateBonus { get; set; } = 0.1f;
        #endregion

        #region Settlement food bonus
        [XmlElement]
        public bool SettlementFoodBonusEnabled { get; set; } = true;
        [XmlElement]
        public float CastleFoodBonus { get; set; } = 2f;
        [XmlElement]
        public float TownFoodBonus { get; set; } = 4f;
        [XmlElement]
        public bool SettlementProsperityFoodMalusTweakEnabled { get; set; } = true;
        [XmlElement]
        public float SettlementProsperityFoodMalusDivisor { get; set; } = 100;
        #endregion

        #region Castle buildings bonuses
        [XmlElement]
        public bool CastleTrainingFieldsBonusEnabled { get; set; } = true;
        [XmlElement]
        public int CastleTrainingFieldsXpAmountLevel1 { get; set; } = 30;
        [XmlElement]
        public int CastleTrainingFieldsXpAmountLevel2 { get; set; } = 70;
        [XmlElement]
        public int CastleTrainingFieldsXpAmountLevel3 { get; set; } = 150;

        [XmlElement]
        public bool CastleGranaryBonusEnabled { get; set; } = true;
        [XmlElement]
        public int CastleGranaryStorageAmountLevel1 { get; set; } = 30;
        [XmlElement]
        public int CastleGranaryStorageAmountLevel2 { get; set; } = 45;
        [XmlElement]
        public int CastleGranaryStorageAmountLevel3 { get; set; } = 60;

        [XmlElement]
        public bool CastleGardensBonusEnabled { get; set; } = true;
        [XmlElement]
        public int CastleGardensFoodProductionAmountLevel1 { get; set; } = 3;
        [XmlElement]
        public int CastleGardensFoodProductionAmountLevel2 { get; set; } = 6;
        [XmlElement]
        public int CastleGardensFoodProductionAmountLevel3 { get; set; } = 9;

        [XmlElement]
        public bool CastleMilitiaBarracksBonusEnabled { get; set; } = true;
        [XmlElement]
        public int CastleMilitiaBarracksAmountLevel1 { get; set; } = 3;
        [XmlElement]
        public int CastleMilitiaBarracksAmountLevel2 { get; set; } = 6;
        [XmlElement]
        public int CastleMilitiaBarracksAmountLevel3 { get; set; } = 9;
        #endregion

        #region Siege Changes
        [XmlElement]
        public bool SiegeConstructionProgressPerDayMultiplierEnabled { get; set; } = true;
        [XmlElement]
        public float SiegeConstructionProgressPerDayMultiplier { get; set; } = 0.8f;

        [XmlElement]
        public bool SiegeCasualtiesTweakEnabled { get; set; } = true;
        [XmlElement]
        public float SiegeCollateralDamageCasualties { get; set; } = 1.75f;
        [XmlElement]
        public float SiegeDestructionCasualties { get; set; } = 4.5f;
        #endregion

        #region Clan parties tweak
        [XmlElement]
        public bool ClanPartiesLimitTweakEnabled { get; set; } = true;
        [XmlElement]
        public int BaseClanPartiesLimit { get; set; } = 0;
        [XmlElement]
        public float ClanPartiesBonusPerClanTier { get; set; } = 0.5f;
        #endregion
    }
}
