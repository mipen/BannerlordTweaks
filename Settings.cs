using ModLib;
using ModLib.Attributes;
using System;
using System.Xml.Serialization;

namespace BannerlordTweaks
{
    public class Settings : SettingsBase
    {
        //[SettingProperty("","")
        //[SettingPropertyGroup("")
        private const string instanceID = "BannerlordTweaksSettings";
        private static Settings _instance = null;
        public override string ModName => "Bannerlord Tweaks";
        public override string ModuleFolderName => ModLibSubModule.ModuleFolderName;

        [XmlElement]
        public override string ID { get; set; } = instanceID;

        public static Settings Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FileDatabase.Get<Settings>(instanceID);
                    if (_instance == null)
                        throw new Exception("Unable to find Bannerlord Tweaks settings in Loader");
                }

                return _instance;
            }
        }

        #region Crafting stamina Settings
        [XmlElement]
        [SettingProperty("Crafting Stamina Tweaks", "Enables tweaks which affect crafting stamina.")]
        [SettingPropertyGroup("Crafting Stamina Tweaks", true)]
        public bool CraftingStaminaTweakEnabled { get; set; } = true;
        [XmlElement]
        [SettingProperty("Max Crafting Stamina", 100, 1000, "Native value is 400.")]
        [SettingPropertyGroup("Crafting Stamina Tweaks")]
        public int MaxCraftingStamina { get; set; } = 400;
        [XmlElement]
        [SettingProperty("Crafting Stamina Gain", 0, 100, "Native value is 5. You gain this amount of crafting stamina per hour.")]
        [SettingPropertyGroup("Crafting Stamina Tweaks")]
        public int CraftingStaminaGainAmount { get; set; } = 10;
        [XmlElement]
        [SettingProperty("Ignore Crafting Stamina", "Native value is false. This disables crafting stamina completely. You will still lose crafting stamina when you craft, but you will still be able to craft when you hit zero.")]
        [SettingPropertyGroup("Crafting Stamina Tweaks")]
        public bool IgnoreCraftingStamina { get; set; } = false;
        [XmlElement]
        [SettingProperty("Crafting Stamina Gain Outside Settlement Multiplier", 0f, 1f, "Native value is 0. In native, you do not gain crafting stamina if you are not resting in a settlement.")]
        [SettingPropertyGroup("Crafting Stamina Tweaks")]
        public float CraftingStaminaGainOutsideSettlementMultiplier { get; set; } = 1f;
        [XmlElement]
        [SettingProperty("Prevent Smelting Locked Items", "Native value is false. Prevent locked items don't show up in smelting list to stop accidental smelting.")]
        [SettingPropertyGroup("Crafting Stamina Tweaks")]
        public bool PreventSmeltingLockedItems { get; set; } = true;
        #endregion

        #region Battle reward patches
        [XmlElement]
        [SettingProperty("Battle Renown Tweak", "Applies the set multiplier to renown gain from battles (applies to the player only).")]
        [SettingPropertyGroup("Battle Renown Tweak", true)]
        public bool BattleRenownMultiplierEnabled { get; set; } = true;
        [XmlElement]
        [SettingProperty("Battle Renown Multiplier", 1f, 5f, "Native value is 1.0")]
        [SettingPropertyGroup("Battle Renown Tweak")]
        public float BattleRenownMultiplier { get; set; } = 2f;
        #endregion

        #region Party size patches
        [XmlElement]
        [SettingProperty("Enable Party Size Bonus", "Applies a bonues to your party size based on your leadership and steward skills.")]
        [SettingPropertyGroup("Party Size Bonus", true)]
        public bool PartySizeTweakEnabled { get; set; } = true;
        [XmlElement]
        [SettingProperty("Enable Leaderhip Bonus", "Applies a bonus equal to the set percentage of your leadership skill to your party size.")]
        [SettingPropertyGroup("Party Size Bonus")]
        public bool LeadershipPartySizeBonusEnabled { get; set; } = true;
        [XmlElement]
        [SettingProperty("Leadership Percentage Bonus", 0f, 1f, "Applies a bonus equal to the set percentage of your leadership skill to your party size.")]
        [SettingPropertyGroup("Party Size Bonus")]
        public float LeadershipPartySizeBonus { get; set; } = 0.3f;
        [XmlElement]
        [SettingProperty("Enable Steward Bonus", "Applies a bonus equal to the set percentage of your steward skill to your party size.")]
        [SettingPropertyGroup("Party Size Bonus")]
        public bool StewardPartySizeBonusEnabled { get; set; } = true;
        [XmlElement]
        [SettingProperty("Steward Percentage Bonus", 0f, 1f, "Applies a bonus equal to the set percentage of your leadership skill to your party size.")]
        [SettingPropertyGroup("Party Size Bonus")]
        public float StewardPartySizeBonus { get; set; } = 0.3f;
        #endregion

        # region Tournament patches
        [XmlElement]
        [SettingProperty("Enable Tournament Renown Tweak", "Sets the amount of renown awarded when you win a tournament.")]
        [SettingPropertyGroup("Tournament Tweaks")]
        public bool TournamentRenownIncreaseEnabled { get; set; } = true;
        [XmlElement]
        [SettingProperty("Tournament Renown Reward", 1, 20, "Native value is 3. Increases the amount of renown awarded when you win a tournament.")]
        [SettingPropertyGroup("Tournament Tweaks")]
        public int TournamentRenownAmount { get; set; } = 8;
        [XmlElement]
        [SettingProperty("Enable Tournament Gold Reward Tweak", "Adds the set amount of gold to the rewards when you win a tournament.")]
        [SettingPropertyGroup("Tournament Tweaks")]
        public bool TournamentGoldRewardEnabled { get; set; } = true;
        [XmlElement]
        [SettingProperty("Tournament Gold Reward", 150, 1000, "Native value is 0. Adds the set amount of gold to the rewards when you win a tournament.")]
        [SettingPropertyGroup("Tournament Tweaks")]
        public int TournamentGoldRewardAmount { get; set; } = 500;
        [XmlElement]
        [SettingProperty("Enable Tournament Max Bet Tweak", "Sets the maximum amount of gold that you can bet per round in tournaments.")]
        [SettingPropertyGroup("Tournament Tweaks")]
        public bool TournamentMaxBetAmountTweakEnabled { get; set; } = true;
        [XmlElement]
        [SettingProperty("Tournament Maximum Bet Amount", 150, 2000, "Native value is 150. Sets the maximum amount of gold that you can bet per round in tournaments.")]
        [SettingPropertyGroup("Tournament Tweaks")]
        public int TournamentMaxBetAmount { get; set; } = 500;

        [XmlElement]
        public bool TournamentExperienceEnabled { get; set; } = false;
        [XmlElement]
        public bool ArenaExperienceEnabled { get; set; } = false;
        #endregion

        #region Hero skill multiplier patch
        [XmlElement]
        [SettingProperty("Enable Hero Skill Experience Tweak", "Applies a multiplier to the amount of experience received based on the skill level of the skill that the experience has been gained for.")]
        [SettingPropertyGroup("Hero Skill Experience Tweak", true)]
        public bool HeroSkillExperienceMultiplierEnabled { get; set; } = true;
        [XmlElement]
        [SettingProperty("Hero Experience Multiplier", -1f, 15f, "Overrides the mod's default experience multiplier with the flat multiplier that you set. Set to -1 to disable.")]
        [SettingPropertyGroup("Hero Skill Experience Override Multiplier")]
        public float HeroSkillExperienceOverrideMultiplier { get; set; } = -1;
        #endregion

        #region Hideout battle tweaks
        [XmlElement]
        [SettingProperty("Enable Hideout Battle Trool Limit Tweak", "Changes the maximum troop limit to the set value inside hideout battles.")]
        [SettingPropertyGroup("Hideout Battle Tweaks")]
        public bool HideoutBattleTroopLimitTweakEnabled { get; set; } = true;
        [XmlElement]
        [SettingProperty("Hideout Battle Troop Limit", 5, 90, "Native value is 9 or 10. Changes the maximum troop limit to the set value inside hideout battles.")]
        [SettingPropertyGroup("Hideout Battle Tweaks")]
        public int HideoutBattleTroopLimit { get; set; } = 90;
        [XmlElement]
        [SettingProperty("Continue Hideout Battle On Death", "If enabled, you will not automatically lose the hideout battle if you die. Your troops will charge and the boss duel will be disabled.")]
        [SettingPropertyGroup("Hideout Battle Tweaks")]
        public bool LoseHideoutBattleOnPlayerDeath { get; set; } = false;
        [XmlElement]
        [SettingProperty("Continue Battle On Losing Duel", "If enabled, you will not lose the battle if you lose the boss duel. Your troops will rush to avenge you and finish everyone off.")]
        [SettingPropertyGroup("Hideout Battle Tweaks")]
        public bool LoseHideoutBattleOnPlayerLoseDuel { get; set; } = false;
        #endregion

        #region Troop experience multiplier
        [XmlElement]
        [SettingProperty("Enable Troop Battle Experience Multiplier", "Multiplies the amount of experience that ALL troops receive during battles (Note: Only troops, not heroes).")]
        [SettingPropertyGroup("Troop Battle Experience Tweaks", true)]
        public bool TroopBattleExperienceMultiplierEnabled { get; set; } = true;
        [XmlElement]
        [SettingProperty("Troop Battle Experience Modifier", "Multiplies the amount of experience that ALL troops receive during fought battles (Note: Only troops, not heroes. Does not apply to simulated battles.).")]
        [SettingPropertyGroup("Troop Battle Experience Tweaks")]
        public float TroopBattleExperienceMultiplier { get; set; } = 3.0f;
        [XmlElement]
        [SettingProperty("Enable Troop Battle Simulation Experience Multiplier", "In native, auto-calculated battles give an 8x multiplier to troop experience.")]
        [SettingPropertyGroup("Troop Battle Experience Tweaks")]
        public bool TroopBattleSimulationExperienceMultiplierEnabled { get; set; } = false;
        [XmlElement]
        [SettingProperty("", "")]
        [SettingPropertyGroup("Troop Battle Experience Tweaks")]
        public float TroopBattleSimulationExperienceMultiplier { get; set; } = 1.0f;
        #endregion

        #region Workshop tweaks
        [XmlElement]
        [SettingProperty("", "")]
        [SettingPropertyGroup("")]
        public bool MaxWorkshopCountTweakEnabled { get; set; } = true;
        [XmlElement]
        [SettingProperty("", "")]
        [SettingPropertyGroup("")]
        public int BaseWorkshopCount { get; set; } = 2;
        [XmlElement]
        [SettingProperty("", "")]
        [SettingPropertyGroup("")]
        public int BonusWorkshopsPerClanTier { get; set; } = 1;
        [XmlElement]
        [SettingProperty("", "")]
        [SettingPropertyGroup("")]
        public bool WorkshopBuyingCostTweakEnabled { get; set; } = false;
        [XmlElement]
        [SettingProperty("", "")]
        [SettingPropertyGroup("")]
        public int WorkshopBaseCost { get; set; } = 10000;
        #endregion

        #region Companion limit tweak
        [XmlElement]
        [SettingProperty("", "")]
        [SettingPropertyGroup("")]
        public bool CompanionLimitTweakEnabled { get; set; } = false;
        [XmlElement]
        [SettingProperty("", "")]
        [SettingPropertyGroup("")]
        public int CompanionLimitBonusPerClanTier { get; set; } = 3;
        [XmlElement]
        [SettingProperty("", "")]
        [SettingPropertyGroup("")]
        public int CompanionBaseLimit { get; set; } = 3;
        #endregion

        #region Kingdom leave relation loss tweak
        [XmlElement]
        [SettingProperty("", "")]
        [SettingPropertyGroup("")]
        public bool LeaveKingdomRelationLossTweakEnabled { get; set; } = false;
        [XmlElement]
        [SettingProperty("", "")]
        [SettingPropertyGroup("")]
        public int LeaveKingdomRelationLossLeaderAmount { get; set; } = 20;
        [XmlElement]
        [SettingProperty("", "")]
        [SettingPropertyGroup("")]
        public int LeaveKingdomRelationLossVassalAmount { get; set; } = 5;
        #endregion

        #region Settlement militia bonus tweak
        [XmlElement]
        [SettingProperty("", "")]
        [SettingPropertyGroup("")]
        public bool SettlementMilitiaBonusEnabled { get; set; } = false;
        [XmlElement]
        [SettingProperty("", "")]
        [SettingPropertyGroup("")]
        public float CastleMilitiaBonus { get; set; } = 1.25f;
        [XmlElement]
        [SettingProperty("", "")]
        [SettingPropertyGroup("")]
        public float TownMilitiaBonus { get; set; } = 2.5f;

        [XmlElement]
        [SettingProperty("", "")]
        [SettingPropertyGroup("")]
        public bool SettlementMilitiaEliteSpawnRateBonusEnabled { get; set; } = true;
        [XmlElement]
        [SettingProperty("", "")]
        [SettingPropertyGroup("")]
        public float SettlementEliteMeleeSpawnRateBonus { get; set; } = 0.15f;
        [XmlElement]
        [SettingProperty("", "")]
        [SettingPropertyGroup("")]
        public float SettlementEliteRangedSpawnRateBonus { get; set; } = 0.1f;
        #endregion

        #region Settlement food bonus
        [XmlElement]
        [SettingProperty("", "")]
        [SettingPropertyGroup("")]
        public bool SettlementFoodBonusEnabled { get; set; } = true;
        [XmlElement]
        [SettingProperty("", "")]
        [SettingPropertyGroup("")]
        public float CastleFoodBonus { get; set; } = 2f;
        [XmlElement]
        [SettingProperty("", "")]
        [SettingPropertyGroup("")]
        public float TownFoodBonus { get; set; } = 4f;
        [XmlElement]
        [SettingProperty("", "")]
        [SettingPropertyGroup("")]
        public bool SettlementProsperityFoodMalusTweakEnabled { get; set; } = true;
        [XmlElement]
        [SettingProperty("", "")]
        [SettingPropertyGroup("")]
        public float SettlementProsperityFoodMalusDivisor { get; set; } = 100;
        #endregion

        #region Castle buildings bonuses
        [XmlElement]
        [SettingProperty("", "")]
        [SettingPropertyGroup("")]
        public bool CastleTrainingFieldsBonusEnabled { get; set; } = true;
        [XmlElement]
        [SettingProperty("", "")]
        [SettingPropertyGroup("")]
        public int CastleTrainingFieldsXpAmountLevel1 { get; set; } = 30;
        [XmlElement]
        [SettingProperty("", "")]
        [SettingPropertyGroup("")]
        public int CastleTrainingFieldsXpAmountLevel2 { get; set; } = 70;
        [XmlElement]
        [SettingProperty("", "")]
        [SettingPropertyGroup("")]
        public int CastleTrainingFieldsXpAmountLevel3 { get; set; } = 150;

        [XmlElement]
        [SettingProperty("", "")]
        [SettingPropertyGroup("")]
        public bool CastleGranaryBonusEnabled { get; set; } = true;
        [XmlElement]
        [SettingProperty("", "")]
        [SettingPropertyGroup("")]
        public int CastleGranaryStorageAmountLevel1 { get; set; } = 30;
        [XmlElement]
        [SettingProperty("", "")]
        [SettingPropertyGroup("")]
        public int CastleGranaryStorageAmountLevel2 { get; set; } = 45;
        [XmlElement]
        [SettingProperty("", "")]
        [SettingPropertyGroup("")]
        public int CastleGranaryStorageAmountLevel3 { get; set; } = 60;

        [XmlElement]
        [SettingProperty("", "")]
        [SettingPropertyGroup("")]
        public bool CastleGardensBonusEnabled { get; set; } = true;
        [XmlElement]
        [SettingProperty("", "")]
        [SettingPropertyGroup("")]
        public int CastleGardensFoodProductionAmountLevel1 { get; set; } = 3;
        [XmlElement]
        [SettingProperty("", "")]
        [SettingPropertyGroup("")]
        public int CastleGardensFoodProductionAmountLevel2 { get; set; } = 6;
        [XmlElement]
        [SettingProperty("", "")]
        [SettingPropertyGroup("")]
        public int CastleGardensFoodProductionAmountLevel3 { get; set; } = 9;

        [XmlElement]
        [SettingProperty("", "")]
        [SettingPropertyGroup("")]
        public bool CastleMilitiaBarracksBonusEnabled { get; set; } = true;
        [XmlElement]
        [SettingProperty("", "")]
        [SettingPropertyGroup("")]
        public int CastleMilitiaBarracksAmountLevel1 { get; set; } = 3;
        [XmlElement]
        [SettingProperty("", "")]
        [SettingPropertyGroup("")]
        public int CastleMilitiaBarracksAmountLevel2 { get; set; } = 6;
        [XmlElement]
        [SettingProperty("", "")]
        [SettingPropertyGroup("")]
        public int CastleMilitiaBarracksAmountLevel3 { get; set; } = 9;
        #endregion

        #region Siege Changes
        [XmlElement]
        [SettingProperty("", "")]
        [SettingPropertyGroup("")]
        public bool SiegeConstructionProgressPerDayMultiplierEnabled { get; set; } = true;
        [XmlElement]
        [SettingProperty("", "")]
        [SettingPropertyGroup("")]
        public float SiegeConstructionProgressPerDayMultiplier { get; set; } = 0.8f;

        [XmlElement]
        [SettingProperty("", "")]
        [SettingPropertyGroup("")]
        public bool SiegeCasualtiesTweakEnabled { get; set; } = true;
        [XmlElement]
        [SettingProperty("", "")]
        [SettingPropertyGroup("")]
        public float SiegeCollateralDamageCasualties { get; set; } = 1.75f;
        [XmlElement]
        [SettingProperty("", "")]
        [SettingPropertyGroup("")]
        public float SiegeDestructionCasualties { get; set; } = 4.5f;
        #endregion

        #region Clan parties tweak
        [XmlElement]
        [SettingProperty("", "")]
        [SettingPropertyGroup("")]
        public bool ClanPartiesLimitTweakEnabled { get; set; } = true;
        [XmlElement]
        [SettingProperty("", "")]
        [SettingPropertyGroup("")]
        public int BaseClanPartiesLimit { get; set; } = 0;
        [XmlElement]
        [SettingProperty("", "")]
        [SettingPropertyGroup("")]
        public float ClanPartiesBonusPerClanTier { get; set; } = 0.5f;
        #endregion
    }
}
