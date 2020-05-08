using ModLib.Definitions.Attributes;
using ModLib.Definitions;
using System.Xml.Serialization;

namespace BannerlordTweaks
{
    public class Settings : SettingsBase
    {
        public const string InstanceID = "BannerlordTweaksSettings";
        public override string ModName => "Bannerlord Tweaks";
        public override string ModuleFolderName => SubModule.ModuleFolderName;

        [XmlElement]
        public override string ID { get; set; } = InstanceID;

        public static Settings Instance
        {
            get
            {
                return (Settings)SettingsDatabase.GetSettings<Settings>();
            }
        }

        //[SettingProperty("","")
        //[SettingPropertyGroup("")

        #region Miscellaneous
        [XmlElement]
        [SettingProperty("Disable Quest Troops Affecting Morale", "When enabled, quest troops such as \"Borrowed Troop\" in your party are ignored when party morale is calculated.")]
        public bool QuestCharactersIgnorePartySize { get; set; } = false;
        [XmlElement]
        [SettingProperty("Show Number of Days of Food", "Changes the number showing how much food you have to instead show how many days' worth of food you have. (Bottom right of campaign map UI).")]
        public bool ShowFoodDaysRemaining { get; set; } = false;

        #endregion

        #region Crafting stamina Settings
        [XmlElement]
        [SettingProperty("Crafting Stamina Tweaks", "Enables tweaks which affect crafting stamina.")]
        [SettingPropertyGroup("Crafting Stamina Tweaks", true)]
        public bool CraftingStaminaTweakEnabled { get; set; } = true;
        [XmlElement]
        [SettingProperty("Max Crafting Stamina", 100, 1000, 10, 10000, "Native value is 400. Sets the maximum crafting stamina value.")]
        [SettingPropertyGroup("Crafting Stamina Tweaks")]
        public int MaxCraftingStamina { get; set; } = 400;
        [XmlElement]
        [SettingProperty("Crafting Stamina Gain", 0, 100, 1, 1000, "Native value is 5. You gain this amount of crafting stamina per hour.")]
        [SettingPropertyGroup("Crafting Stamina Tweaks")]
        public int CraftingStaminaGainAmount { get; set; } = 10;
        [XmlElement]
        [SettingProperty("Ignore Crafting Stamina", "Native value is false. This disables crafting stamina completely. You will still lose crafting stamina when you craft, but you will still be able to craft when you hit zero.")]
        [SettingPropertyGroup("Crafting Stamina Tweaks")]
        public bool IgnoreCraftingStamina { get; set; } = false;
        [XmlElement]
        [SettingProperty("Crafting Stamina Gain Outside Settlement Multiplier", 0f, 1f, "Native value is 0.0. In native, you do not gain crafting stamina if you are not resting inside a settlement.")]
        [SettingPropertyGroup("Crafting Stamina Tweaks")]
        public float CraftingStaminaGainOutsideSettlementMultiplier { get; set; } = 1f;
        #endregion

        #region Smelting patches
        [XmlElement]
        [SettingProperty("Hide Locked Weapons in Smelting Menu", "Native value is false. Prevent weapons that you have locked in your inventory from showing up in the smelting list to prevent accidental smelting.")]
        [SettingPropertyGroup("Smelting Tweaks")]
        public bool PreventSmeltingLockedItems { get; set; } = true;

        [XmlElement]
        [SettingProperty("Enable Unlocking Parts From Smelted Weapons", "Native value is false. Unlock the parts that a weapon is made of when you smelt it.")]
        [SettingPropertyGroup("Smelting Tweaks")]
        public bool AutoLearnSmeltedParts { get; set; } = true;
        #endregion

        #region Battle reward patches
        [XmlElement]
        [SettingProperty("Battle Reward Tweaks", "Applies the set multiplier to renown and influence gain from battles (applies to the player only).")]
        [SettingPropertyGroup("Battle Reward Tweaks", true)]
        public bool BattleRewardTweaksEnabled { get; set; } = true;
        [XmlElement]
        [SettingProperty("Battle Renown Multiplier", 1f, 5f, 1f, 20f, "Native value is 1.0. The amount of renown you receive from a battle is multiplied by this value.")]
        [SettingPropertyGroup("Battle Reward Tweaks")]
        public float BattleRenownMultiplier { get; set; } = 2f;
        [XmlElement]
        [SettingProperty("Battle Influence Multiplier", 0.1f, 5f, "Native value is 1.0. The amount of influence you receive from a battle is multiplied by this value.")]
        [SettingPropertyGroup("Battle Reward Tweaks")]
        public float BattleInfluenceMultiplier { get; set; } = 1f;
        [XmlElement]
        [SettingProperty("Apply To AI", "Applies the same multipliers to AI parties.")]
        [SettingPropertyGroup("Battle Reward Tweaks")]
        public bool BattleRewardApplyToAI { get; set; } = true;
        #endregion

        #region Party size patches
        [XmlElement]
        [SettingProperty("Enable Party Size Bonus", "Applies a bonues to your party size based on your leadership and steward skills.")]
        [SettingPropertyGroup("Party Size Bonus", true)]
        public bool PartySizeTweakEnabled { get; set; } = true;
        [XmlElement]
        [SettingProperty("Enable Leadership Bonus", "Applies a bonus equal to the set percentage of your leadership skill to your party size.")]
        [SettingPropertyGroup("Party Size Bonus")]
        public bool LeadershipPartySizeBonusEnabled { get; set; } = true;
        [XmlElement]
        [SettingProperty("Leadership Percentage Bonus", 0f, 1f, 0f, 10f, "Applies a bonus equal to the set percentage of your leadership skill to your party size.")]
        [SettingPropertyGroup("Party Size Bonus")]
        public float LeadershipPartySizeBonus { get; set; } = 0.3f;
        [XmlElement]
        [SettingProperty("Enable Steward Bonus", "Applies a bonus equal to the set percentage of your steward skill to your party size.")]
        [SettingPropertyGroup("Party Size Bonus")]
        public bool StewardPartySizeBonusEnabled { get; set; } = true;
        [XmlElement]
        [SettingProperty("Steward Percentage Bonus", 0f, 1f, 0f, 10f, "Applies a bonus equal to the set percentage of your leadership skill to your party size.")]
        [SettingPropertyGroup("Party Size Bonus")]
        public float StewardPartySizeBonus { get; set; } = 0.3f;
        #endregion

        # region Tournament patches
        [XmlElement]
        [SettingProperty("Enable Tournament Renown Tweak", "Sets the amount of renown awarded when you win a tournament.")]
        [SettingPropertyGroup("Tournament Tweaks/Renown Reward Tweak", true)]
        public bool TournamentRenownIncreaseEnabled { get; set; } = true;
        [XmlElement]
        [SettingProperty("Tournament Renown Reward", 1, 20, 1, 10000, "Native value is 3. Increases the amount of renown awarded when you win a tournament.")]
        [SettingPropertyGroup("Tournament Tweaks/Renown Reward Tweak")]
        public int TournamentRenownAmount { get; set; } = 8;
        [XmlElement]
        [SettingProperty("Enable Tournament Gold Reward Tweak", "Adds the set amount of gold to the rewards when you win a tournament.")]
        [SettingPropertyGroup("Tournament Tweaks/Gold Reward Tweak", true)]
        public bool TournamentGoldRewardEnabled { get; set; } = true;
        [XmlElement]
        [SettingProperty("Tournament Gold Reward", 150, 1000, 0, 10000, "Native value is 0. Adds the set amount of gold to the rewards when you win a tournament.")]
        [SettingPropertyGroup("Tournament Tweaks/Gold Reward Tweak")]
        public int TournamentGoldRewardAmount { get; set; } = 500;
        [XmlElement]
        [SettingProperty("Enable Tournament Max Bet Tweak", "Sets the maximum amount of gold that you can bet per round in tournaments.")]
        [SettingPropertyGroup("Tournament Tweaks/Maximum Bet Amount Tweak", true)]
        public bool TournamentMaxBetAmountTweakEnabled { get; set; } = true;
        [XmlElement]
        [SettingProperty("Tournament Maximum Bet Amount", 150, 2000, 1, 10000, "Native value is 150. Sets the maximum amount of gold that you can bet per round in tournaments.")]
        [SettingPropertyGroup("Tournament Tweaks/Maximum Bet Amount Tweak")]
        public int TournamentMaxBetAmount { get; set; } = 500;

        [XmlElement]
        [SettingProperty("Enable Tournament Hero Experience Multiplier Override", "Overrides the native multiplier value for experience gain in tournaments for hero characters.")]
        [SettingPropertyGroup("Tournament Tweaks/Tournament Hero Experience Multiplier", true)]
        public bool TournamentHeroExperienceMultiplierEnabled { get; set; } = false;
        [XmlElement]
        [SettingProperty("Tournament Hero Experience Multiplier", 0.25f, 1f, "Native value is 0.25. Sets the multiplier applied to experience gained in tournaments by hero characters.")]
        [SettingPropertyGroup("Tournament Tweaks/Tournament Hero Experience Multiplier")]
        public float TournamentHeroExperienceMultiplier { get; set; } = 0.25f;
        [XmlElement]
        [SettingProperty("Enable Arena Hero Experience Multiplier Override", "Overrides the native multiplier value for experience gain in arena fights for hero characters.")]
        [SettingPropertyGroup("Tournament Tweaks/Arena Hero Experience Multiplier", true)]
        public bool ArenaHeroExperienceMultiplierEnabled { get; set; } = false;
        [XmlElement]
        [SettingProperty("Arena Hero Experience Multiplier", 0.07f, 1f, "Native value is 0.06. Overrides the native multiplier for experience gain in arena fights for hero characters.")]
        [SettingPropertyGroup("Tournament Tweaks/Arena Hero Experience Multiplier")]
        public float ArenaHeroExperienceMultiplier { get; set; } = 0.07f;
        #endregion

        #region Hero skill multiplier patch
        [XmlElement]
        [SettingProperty("Enable Hero Skill Experience Multiplier", "Applies a multiplier to the amount of experience recieved for skills. Affects the player only.")]
        [SettingPropertyGroup("Hero Skill Experience Multiplier", true)]
        public bool HeroSkillExperienceMultiplierEnabled { get; set; } = false;
        // [XmlElement]
        // [SettingProperty("Enable Flat Experience Multiplier Override", "If enabled, overrides the mod's experience curve multiplier calculation and replaces it with the override multiplier. This means that experience will be multiplied by the same value, independant of the skill level.")]
        // [SettingPropertyGroup("Hero Skill Experience Tweak")]
        //public bool HeroSkillExperienceOverrideMultiplierEnabled { get; set; } = false;
        [XmlElement]
        [SettingProperty("Hero Skill Experience Multiplier", 1f, 5f, "Applies a multiplier to the amount of experience recieved for skills. Affects the player only.")]
        [SettingPropertyGroup("Hero Skill Experience Multiplier")]
        public float HeroSkillExperienceMultiplier { get; set; } = 1f;
        #endregion

        #region Hideout battle tweaks
        [XmlElement]
        [SettingProperty("Enable Hideout Battle Troop Limit Tweak", "Changes the maximum troop limit to the set value inside hideout battles.")]
        [SettingPropertyGroup("Hideout Battle Tweaks", true)]
        public bool HideoutBattleTroopLimitTweakEnabled { get; set; } = true;
        [XmlElement]
        [SettingProperty("Hideout Battle Troop Limit", 5, 90, "Native value is 9 or 10. Changes the maximum troop limit to the set value inside hideout battles. Cannot be higher than 90 because it causes bugs.")]
        [SettingPropertyGroup("Hideout Battle Tweaks")]
        public int HideoutBattleTroopLimit { get; set; } = 90;
        [XmlElement]
        [SettingProperty("Continue Hideout Battle On Player Death", "Native value is false. If enabled, you will not automatically lose the hideout battle if you die. Your troops will charge and the boss duel will be disabled.")]
        [SettingPropertyGroup("Hideout Battle Tweaks")]
        public bool ContinueHideoutBattleOnPlayerDeath { get; set; } = false;
        [XmlElement]
        [SettingProperty("Continue Battle On Losing Duel", "Native value is false. If enabled, you will lose the battle if you lose the boss duel. If disabled, your troops will rush to avenge you and finish everyone off.")]
        [SettingPropertyGroup("Hideout Battle Tweaks")]
        public bool ContinueHideoutBattleOnPlayerLoseDuel { get; set; } = true;
        #endregion

        #region Troop experience multiplier
        [XmlElement]
        [SettingProperty("Enable Troop Battle Experience Multiplier", "Multiplies the amount of experience that ALL troops receive during battles (Note: Only troops, not heroes).")]
        [SettingPropertyGroup("Troop Battle Experience Tweaks", true)]
        public bool TroopBattleExperienceMultiplierEnabled { get; set; } = false;
        [XmlElement]
        [SettingProperty("Troop Battle Experience Modifier", 1f, 6f, 0f, 100f, "Native value is 1.0. Multiplies the amount of experience that ALL troops receive during fought battles (Note: Only troops, not heroes. Does not apply to simulated battles.).")]
        [SettingPropertyGroup("Troop Battle Experience Tweaks")]
        public float TroopBattleExperienceMultiplier { get; set; } = 1.0f;
        [XmlElement]
        [SettingProperty("Enable Troop Battle Simulation Experience Multiplier", "Provides a multiplier to experience gained from simulated battles. This is applied to all fights (including NPC fights) on the campaign map.")]
        [SettingPropertyGroup("Troop Battle Experience Tweaks")]
        public bool TroopBattleSimulationExperienceMultiplierEnabled { get; set; } = false;
        [XmlElement]
        [SettingProperty("Troop Battle Simulation Experience Multiplier", 0.5f, 8f, 0f, 100f, "Native value is 1.0. Provides a multiplier to experience gained from simulated battles. This is applied to all simulated fights on the campaign map.")]
        [SettingPropertyGroup("Troop Battle Experience Tweaks")]
        public float TroopBattleSimulationExperienceMultiplier { get; set; } = 1.0f;
        #endregion

        #region Workshop tweaks
        [XmlElement]
        [SettingProperty("Enable Max Workshop Limit Tweak", "Sets the base maximum number of workshops you can have and the limit increase gained per clan tier.")]
        [SettingPropertyGroup("Workshop Tweaks")]
        public bool MaxWorkshopCountTweakEnabled { get; set; } = true;
        [XmlElement]
        [SettingProperty("Base Workshop Limit", 0, 10, 0, 100, "Native value is 1. Sets the base maximum number of workshops you can have.")]
        [SettingPropertyGroup("Workshop Tweaks")]
        public int BaseWorkshopCount { get; set; } = 2;
        [XmlElement]
        [SettingProperty("Bonus Workshops Per Clan Tier", 0, 3, 0, 20, "Sets the base maximum number of workshops you can have and the limit increase gained per clan tier.")]
        [SettingPropertyGroup("Workshop Tweaks")]
        public int BonusWorkshopsPerClanTier { get; set; } = 1;
        [XmlElement]
        [SettingProperty("Enable Workshop Cost Tweak", "Sets the base value used to calculate the cost of workshops. Reduce to reduce cost of workshops.")]
        [SettingPropertyGroup("Workshop Tweaks")]
        public bool WorkshopBuyingCostTweakEnabled { get; set; } = false;
        [XmlElement]
        [SettingProperty("Workshop Base Cost", 0, 15000, "Native value is 10,000. Sets the base value used to calculate the cost of workshops. Reduce to reduce cost of workshops.")]
        [SettingPropertyGroup("Workshop Tweaks")]
        public int WorkshopBaseCost { get; set; } = 10000;
        #endregion

        #region Companion limit tweak
        [XmlElement]
        [SettingProperty("Enable Companion Limit Tweak", "Sets the base companion limit and the bonus gained per clan tier.")]
        [SettingPropertyGroup("Companion Limit Tweak", true)]
        public bool CompanionLimitTweakEnabled { get; set; } = false;
        [XmlElement]
        [SettingProperty("Base Companion Limit", 1, 10, 1, 100, "Native value is 3. Sets the base companion limit.")]
        [SettingPropertyGroup("Companion Limit Tweak")]
        public int CompanionBaseLimit { get; set; } = 3;
        [XmlElement]
        [SettingProperty("Companion Limit Bonus Per Clan Tier", 0, 5, 0, 50, "Native value is 1. Sets the bonus to companion limit per clan tier. This value is multiplied by your clan tier.")]
        [SettingPropertyGroup("Companion Limit Tweak")]
        public int CompanionLimitBonusPerClanTier { get; set; } = 3;
        #endregion

        #region Settlement militia bonus tweak
        [XmlElement]
        [SettingProperty("Enable Settlement Militia Bonus", "Grants a bonus to militia growth in towns and castles.")]
        [SettingPropertyGroup("Settlement Militia Bonus", true)]
        public bool SettlementMilitiaBonusEnabled { get; set; } = false;
        [XmlElement]
        [SettingProperty("Castle Militia Growth Bonus", 0f, 5f, 0f, 100f, "Native value is 0. Grants a bonus to militia growth in castles.")]
        [SettingPropertyGroup("Settlement Militia Bonus")]
        public float CastleMilitiaBonus { get; set; } = 1.25f;
        [XmlElement]
        [SettingProperty("Town Militia Growth Bonus", 0f, 5f, 0f, 100f, "Native value is 0. Grants a bonus to militia growth in towns.")]
        [SettingPropertyGroup("Settlement Militia Bonus")]
        public float TownMilitiaBonus { get; set; } = 2.5f;

        [XmlElement]
        [SettingProperty("Enable Militia Elite Spawn Bonus", "Adds a bonus to the chance that militia spawning in towns and castles are elite.")]
        [SettingPropertyGroup("Settlement Militia Bonus")]
        public bool SettlementMilitiaEliteSpawnRateBonusEnabled { get; set; } = true;
        [XmlElement]
        [SettingProperty("Elite Melee Militia Spawn Bonus", 0f, 1f, "Native value is 0. Adds a bonus to the change the militia spawning in towns and castles are elite melee troops.")]
        [SettingPropertyGroup("Settlement Militia Bonus")]
        public float SettlementEliteMeleeSpawnRateBonus { get; set; } = 0.15f;
        [XmlElement]
        [SettingProperty("Elite Ranged Militia Spawn Bonus", 0f, 1f, "Native value is 0. Adds a bonus to the change the militia spawning in towns and castles are elite ranged troops.")]
        [SettingPropertyGroup("Settlement Militia Bonus")]
        public float SettlementEliteRangedSpawnRateBonus { get; set; } = 0.1f;
        #endregion

        #region Settlement food bonus
        [XmlElement]
        [SettingProperty("Enabled Settlement Food Bonus Tweaks", "Enables tweaks which provide bonuses to food production in towns and castles.")]
        [SettingPropertyGroup("Settlement Food Bonus", true)]
        public bool SettlementFoodBonusEnabled { get; set; } = true;
        [XmlElement]
        [SettingProperty("Castle Food Bonus", 0f, 10f, 0f, 100f, "Native value is 0. Provides a bonus to food production in castles.")]
        [SettingPropertyGroup("Settlement Food Bonus")]
        public float CastleFoodBonus { get; set; } = 2f;
        [XmlElement]
        [SettingProperty("Town Food Bonus", 0f, 10f, 0f, 100f, "Native value is 0. Provides a bonus to food production in towns.")]
        [SettingPropertyGroup("Settlement Food Bonus")]
        public float TownFoodBonus { get; set; } = 4f;
        [XmlElement]
        [SettingProperty("Enable Food Loss From Prosperity Tweak", "Allows you to adjust the loss to food production received from settlement prosperity.")]
        [SettingPropertyGroup("Settlement Food Bonus")]
        public bool SettlementProsperityFoodMalusTweakEnabled { get; set; } = true;
        [XmlElement]
        [SettingProperty("Prosperity Food Loss Divisor", 50f, 400f, 50f, 10000f, "Native value is 50. The prosperity of the settlement is divided by this value to calculate the loss. Increasing this value will decrease the amount of food lost.")]
        [SettingPropertyGroup("Settlement Food Bonus")]
        public float SettlementProsperityFoodMalusDivisor { get; set; } = 100;
        #endregion

        #region Castle buildings bonuses
        [XmlElement]
        [SettingProperty("Enable Castle Training Fields Tweak", "Changes the amount of experience the training fields provides for each level.")]
        [SettingPropertyGroup("Castle Training Fields Tweak", true)]
        public bool CastleTrainingFieldsBonusEnabled { get; set; } = true;
        [XmlElement]
        [SettingProperty("Castle Training Fields Level 1 Experience", 1, 150, 1, 1000, "Native value is 1. Changes the amount of experience the training fields provides at level 1.")]
        [SettingPropertyGroup("Castle Training Fields Tweak")]
        public int CastleTrainingFieldsXpAmountLevel1 { get; set; } = 30;
        [XmlElement]
        [SettingProperty("Castle Training Fields Level 2 Experience", 2, 200, 2, 1000, "Native value is 2. Changes the amount of experience the training fields provides at level 2.")]
        [SettingPropertyGroup("Castle Training Fields Tweak")]
        public int CastleTrainingFieldsXpAmountLevel2 { get; set; } = 70;
        [XmlElement]
        [SettingProperty("Castle Training Fields Level 3 Experience", 3, 250, 3, 1000, "Native value is 3. Changes the amount of experience the training fields provides at level 3.")]
        [SettingPropertyGroup("Castle Training Fields Tweak")]
        public int CastleTrainingFieldsXpAmountLevel3 { get; set; } = 150;

        [XmlElement]
        [SettingProperty("Enable Castle Granary Tweak", "Changes the amount of food storage the castle granary provides per level.")]
        [SettingPropertyGroup("Castle Granary Tweak", true)]
        public bool CastleGranaryBonusEnabled { get; set; } = true;
        [XmlElement]
        [SettingProperty("Castle Granary Food Storage Level 1", 10, 90, 10, 1000, "Native value is 10. Changes the amount of food storage the castle granary provides at level 1.")]
        [SettingPropertyGroup("Castle Granary Tweak")]
        public int CastleGranaryStorageAmountLevel1 { get; set; } = 30;
        [XmlElement]
        [SettingProperty("Castle Granary Food Storage Level 2", 20, 180, 20, 1000, "Native value is 20. Changes the amount of food storage the castle granary provides at level 2.")]
        [SettingPropertyGroup("Castle Granary Tweak")]
        public int CastleGranaryStorageAmountLevel2 { get; set; } = 45;
        [XmlElement]
        [SettingProperty("Castle Granary Food Storage Level 3", 30, 270, 30, 1000, "Native value is 30. Changes the amount of food storage the castle granary provides at level 3.")]
        [SettingPropertyGroup("Castle Granary Tweak")]
        public int CastleGranaryStorageAmountLevel3 { get; set; } = 60;

        [XmlElement]
        [SettingProperty("Enable Castle Gardens Tweak", "Changes the amount of food the castle gardens produce per level.")]
        [SettingPropertyGroup("Castle Gardens Tweak", true)]
        public bool CastleGardensBonusEnabled { get; set; } = true;
        [XmlElement]
        [SettingProperty("Castle Garden Food Production Level 1", 1, 10, 1, 1000, "Native value is 1. Changes the amount of food the castle gardens produce at level 1.")]
        [SettingPropertyGroup("Castle Gardens Tweak")]
        public int CastleGardensFoodProductionAmountLevel1 { get; set; } = 3;
        [XmlElement]
        [SettingProperty("Castle Garden Food Production Level 2", 2, 20, 2, 1000, "Native value is 2. Changes the amount of food the castle gardens produce at level 2.")]
        [SettingPropertyGroup("Castle Gardens Tweak")]
        public int CastleGardensFoodProductionAmountLevel2 { get; set; } = 6;
        [XmlElement]
        [SettingProperty("Castle Garden Food Production Level 3", 3, 30, 3, 1000, "Native value is 3. Changes the amount of food the castle gardens produce at level 3.")]
        [SettingPropertyGroup("Castle Gardens Tweak")]
        public int CastleGardensFoodProductionAmountLevel3 { get; set; } = 9;

        [XmlElement]
        [SettingProperty("Enable Castle Militia Barracks Tweak", "Changes the militia production that the castle militia barracks provides per level.")]
        [SettingPropertyGroup("Castle Militia Barracks Tweak", true)]
        public bool CastleMilitiaBarracksBonusEnabled { get; set; } = true;
        [XmlElement]
        [SettingProperty("Castle Militia Barracks Production Level 1", 1, 10, 1, 1000, "Native value is 1. Changes the militia production that the castle militia barracks provides at level 1.")]
        [SettingPropertyGroup("Castle Militia Barracks Tweak")]
        public int CastleMilitiaBarracksAmountLevel1 { get; set; } = 2;
        [XmlElement]
        [SettingProperty("Castle Militia Barracks Production Level 2", 1, 14, 1, 1000, "Native value is 2. Changes the militia production that the castle militia barracks provides at level 2.")]
        [SettingPropertyGroup("Castle Militia Barracks Tweak")]
        public int CastleMilitiaBarracksAmountLevel2 { get; set; } = 4;
        [XmlElement]
        [SettingProperty("Castle Militia Barracks Production Level 3", 1, 16, 1, 1000, "Native value is 4. Changes the militia production that the castle militia barracks provides at level 3.")]
        [SettingPropertyGroup("Castle Militia Barracks Tweak")]
        public int CastleMilitiaBarracksAmountLevel3 { get; set; } = 8;
        #endregion

        #region Siege Changes
        [XmlElement]
        [SettingProperty("Enable Siege Construction Progress Tweak", "Adds a multiplier to the construction progress per day for sieges.")]
        [SettingPropertyGroup("Siege Constructon Progress Tweak", true)]
        public bool SiegeConstructionProgressPerDayMultiplierEnabled { get; set; } = true;
        [XmlElement]
        [SettingProperty("Siege Construction Progress Per Day Multiplier", 0.5f, 1.5f, "Native value is 1.0. Adds a multiplier to the construction progress per day for sieges. A smaller number results in longer siege times.")]
        [SettingPropertyGroup("Siege Constructon Progress Tweak")]
        public float SiegeConstructionProgressPerDayMultiplier { get; set; } = 0.8f;

        [XmlElement]
        [SettingProperty("Enable Siege Casualties Tweaks", "Changes the values used to calculate casualties during the siege stage on the campaign map.")]
        [SettingPropertyGroup("Siege Casualties Tweaks", true)]
        public bool SiegeCasualtiesTweakEnabled { get; set; } = true;
        [XmlElement]
        [SettingProperty("Siege Collateral Damage Casualties", 1, 3, "Native value is 2.0. Changes the value used to calculate collateral casualties during the campaign map siege stage.")]
        [SettingPropertyGroup("Siege Casualties Tweaks")]
        public int SiegeCollateralDamageCasualties { get; set; } = 1;
        [XmlElement]
        [SettingProperty("Siege Destruction Casualties", 3, 7, "Native value is 5.0. Changes the value used to calculate desctruction casualties during the campaign map siege stage.")]
        [SettingPropertyGroup("Siege Casualties Tweaks")]
        public int SiegeDestructionCasualties { get; set; } = 4;
        #endregion

        #region Clan parties tweak
        [XmlElement]
        [SettingProperty("Enable Clan Parties Tweak", "Changes the base number of parties you can field and the bonus gained per clan tier.")]
        [SettingPropertyGroup("Clan Parties Tweak", true)]
        public bool ClanPartiesLimitTweakEnabled { get; set; } = false;
        [XmlElement]
        [SettingProperty("Base Clan Parties Limit", 1, 10, 1, 100, "Native value is 1. This is the base number of parties you can field.")]
        [SettingPropertyGroup("Clan Parties Tweak")]
        public int BaseClanPartiesLimit { get; set; } = 2;
        [XmlElement]
        [SettingProperty("Clan Parties Bonus Per Clan Tier", 0.0f, 3f, 0f, 100f, "Native has a calculation for this: 1 party for under tier 3, 2 parties for under tier 5, 3 parties for over tier 5. This setting is multiplied by your clan tier. A value of 0.5 will equate to 1 extra party per 2 clan tiers, which eqautes to the same as native.")]
        [SettingPropertyGroup("Clan Parties Tweak")]
        public float ClanPartiesBonusPerClanTier { get; set; } = 0.5f;
        #endregion

        #region Pregnancy tweak
        [XmlElement]
        [SettingProperty("Disable Stillbirths", "Disables the chance of children dying when born.")]
        [SettingPropertyGroup("Pregnancy Tweaks")]
        public bool NoStillbirthsTweakEnabled { get; set; } = false;
        [XmlElement]
        [SettingProperty("Disable Maternal Mortality", "Disables the chance of mothers dying when giving birth.")]
        [SettingPropertyGroup("Pregnancy Tweaks")]
        public bool NoMaternalMortalityTweakEnabled { get; set; } = false;
        [XmlElement]
        [SettingProperty("Enable Pregnancy Duration Tweak", "Allows for adjusting the duration for a pregnancy.")]
        [SettingPropertyGroup("Pregnancy Tweaks/Pregnancy Duration Tweak", true)]
        public bool PregnancyDurationTweakEnabled { get; set; } = false;
        [XmlElement]
        [SettingProperty("Pregnancy Duration", 1, 96, "Native value is 36 days.")]
        [SettingPropertyGroup("Pregnancy Tweaks/Pregnancy Duration Tweak")]
        public int PregnancyDuration { get; set; } = 36;
        [XmlElement]
        [SettingProperty("Enable Gender Ratio Tweak", "Allows for adjusting the gender ratio of births.")]
        [SettingPropertyGroup("Pregnancy Tweaks/Female Offspring Probability Tweak", true)]
        public bool FemaleOffspringProbabilityTweakEnabled { get; set; } = false;
        [XmlElement]
        [SettingProperty("Probability for female children", -1.0f, 1.0f, "Native value is 0.51. Set to -1 to disable female births.")]
        [SettingPropertyGroup("Pregnancy Tweaks/Female Offspring Probability Tweak")]
        public float FemaleOffspringProbability { get; set; } = 0.51f;
        [XmlElement]
        [SettingProperty("Enable Twins Probability Tweak", "Allows for adjusting the chance of giving birth to twins.")]
        [SettingPropertyGroup("Pregnancy Tweaks/Twins Probability Tweak", true)]
        public bool TwinsProbabilityTweakEnabled { get; set; } = false;
        [XmlElement]
        [SettingProperty("Probability to deliver twins", -1.0f, 1.0f, "Native value is 0.03. Determines the chance of giving birth to twins.")]
        [SettingPropertyGroup("Pregnancy Tweaks/Twins Probability Tweak")]
        public float TwinsProbability { get; set; } = 0.03f;
        [XmlElement]
        [SettingProperty("Enable Character Fertility Probability Tweak", "Allows for adjusting for the probability to get pregnant, this will apply to everyone.")]
        [SettingPropertyGroup("Pregnancy Tweaks/Fertility Probability Tweak", true)]
        public bool CharacterFertilityProbabilityTweakEnabled { get; set; } = false;
        [XmlElement]
        [SettingProperty("Character Fertility Probability", 0f, 1.0f, "Native value is 0.95. Determines the chance of getting pregnant")]
        [SettingPropertyGroup("Pregnancy Tweaks/Fertility Probability Tweak")]
        public float CharacterFertilityProbability { get; set; } = 0.95f;
        [XmlElement]
        [SettingProperty("Enable Daily Chance Pregnancy Tweak", "Enabling this will completely override the daily pregnancy check. All settings below will be applied!")]
        [SettingPropertyGroup("Pregnancy Tweaks/Daily Chance Pregnancy Tweak", true)]
        public bool DailyChancePregnancyTweakEnabled { get; set; } = false;
        [XmlElement]
        [SettingProperty("Enable Player Character Fertility", "Is the player character (you) fertile? Native: true")]
        [SettingPropertyGroup("Pregnancy Tweaks/Daily Chance Pregnancy Tweak")]
        public bool PlayerCharacterFertileEnabled { get; set; } = true;
        [XmlElement]
        [SettingProperty("Min Pregnancy Age", 0, 999, "Minimum Age the Hero can get pregnant. Native: 18")]
        [SettingPropertyGroup("Pregnancy Tweaks/Daily Chance Pregnancy Tweak")]
        public int MinPregnancyAge { get; set; } = 18;
        [XmlElement]
        [SettingProperty("Max Pregnancy Age", 0, 999, "Maximum Age the Hero can get pregnant. Native: 45")]
        [SettingPropertyGroup("Pregnancy Tweaks/Daily Chance Pregnancy Tweak")]
        public int MaxPregnancyAge { get; set; } = 45;
        [XmlElement]
        [SettingProperty("Enable Max Children Tweak", "Allows to set the maximum number of children that you can get")]
        [SettingPropertyGroup("Pregnancy Tweaks/Daily Chance Pregnancy Tweak")]
        public bool MaxChildrenTweakEnabled { get; set; } = false;
        [XmlElement]
        [SettingProperty("Max Children", 0, 999, "Maximum number of children anyone can have. Default: 5")]
        [SettingPropertyGroup("Pregnancy Tweaks/Daily Chance Pregnancy Tweak")]
        public int MaxChildren { get; set; } = 5;
        #endregion

        #region Age tweak
        [XmlElement]
        [SettingProperty("Enable Age Tweaks", "Enables the tweaking of character age behaviour")]
        [SettingPropertyGroup("Age Tweaks", true)]
        public bool AgeTweaksEnabled { get; set; } = true;
        [XmlElement]
        [SettingProperty("Become Infant Age", 0, 125, "Native: 3")]
        [SettingPropertyGroup("Age Tweaks")]
        public int BecomeInfantAge { get; set; } = 3;
        [XmlElement]
        [SettingProperty("Become Child Age", 0, 125, "Native: 6")]
        [SettingPropertyGroup("Age Tweaks")]
        public int BecomeChildAge { get; set; } = 6;
        [XmlElement]
        [SettingProperty("Become Teenager Age", 0, 125, "Native: 14")]
        [SettingPropertyGroup("Age Tweaks")]
        public int BecomeTeenagerAge { get; set; } = 14;
        [XmlElement]
        [SettingProperty("Hero Comes Of Age", 0, 100, "Native: 18")]
        [SettingPropertyGroup("Age Tweaks")]
        public int HeroComesOfAge { get; set; } = 18;
        [XmlElement]
        [SettingProperty("Become Old Age", 0, 125, "Native: 47")]
        [SettingPropertyGroup("Age Tweaks")]
        public int BecomeOldAge { get; set; } = 47;
        [XmlElement]
        [SettingProperty("Max Age", 0, 125, "Native: 125")]
        [SettingPropertyGroup("Age Tweaks")]
        public int MaxAge { get; set; } = 125;
        #endregion

        #region Attribute Focus Point Tweaks
        [XmlElement]
        [SettingProperty("Enable Attribute-Focus Point Tweaks", "Changes the values used to calculate how many Attribute and Focus points player gain.")]
        [SettingPropertyGroup("Attribute-Focus Points Tweaks", true)]
        public bool AttributeFocusPointTweakEnabled { get; set; } = true;

        [XmlElement]
        [SettingProperty("Levels To Gain For Attribute Points", 1, 5, "Native value is 4. Howmany levels do you have to gain to be able to receive attribute points")]
        [SettingPropertyGroup("Attribute-Focus Points Tweaks")]
        public int AttributePointRequiredLevel { get; set; } = 4;

        [XmlElement]
        [SettingProperty("Focus Point Per Level", 1, 5, "Native value is 1. This is the amount of focus points earned per level.")]
        [SettingPropertyGroup("Attribute-Focus Points Tweaks")]
        public int FocusPointsPerLevel { get; set; } = 1;
        #endregion

        #region Caravan Patches 
        [XmlElement]
        [SettingProperty("Enable Player Caravan Party Size Tweak", "Applies a configured value to your caravan party size")]
        [SettingPropertyGroup("Caravan Tweaks/Player Caravan Party Size Tweak", true)]
        public bool PlayerCaravanPartySizeTweakEnabled { get; set; } = true;
        [XmlElement]
        [SettingProperty("Player Caravan Party Size", 30, 100, "Native: 30")]
        [SettingPropertyGroup("Caravan Tweaks/Player Caravan Party Size Tweak")]
        public int PlayerCaravanPartySize { get; set; } = 30;
        #endregion
    }
}
