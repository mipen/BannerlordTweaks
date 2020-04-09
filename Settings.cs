using ModLib;
using ModLib.Attributes;
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
                    {
                        _instance = new Settings();
                        FileDatabase.SaveToFile(SubModule.ModuleFolderName, _instance);
                    }
                }

                return _instance;
            }
        }

        #region Miscellaneous
        [XmlElement]
        [SettingProperty("Disable Quest Troops Affecting Morale", "When enabled, quest troops such as \"Borrowed Troop\" in your party are ignored when party morale is calculated.")]
        public bool QuestCharactersIgnorePartySize { get; set; } = true;

        #endregion

        #region Crafting stamina Settings
        [XmlElement]
        [SettingProperty("Crafting Stamina Tweaks", "Enables tweaks which affect crafting stamina.")]
        [SettingPropertyGroup("Crafting Stamina Tweaks", true)]
        public bool CraftingStaminaTweakEnabled { get; set; } = true;
        [XmlElement]
        [SettingProperty("Max Crafting Stamina", 100, 1000, "Native value is 400. Sets the maximum crafting stamina value.")]
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
        [SettingProperty("Crafting Stamina Gain Outside Settlement Multiplier", 0f, 1f, "Native value is 0.0. In native, you do not gain crafting stamina if you are not resting inside a settlement.")]
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
        [SettingProperty("Battle Renown Multiplier", 1f, 5f, "Native value is 1.0. The amount of renown you receive from a battle is multiplied by this value.")]
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
        [SettingProperty("Enable Hero Skill Experience Tweak", "Applies a multiplier to the amount of experience received based on the skill level of the skill that the experience has been gained for. The multiplier is calculated using this function: 0.0315769 * skillLevel^1.020743 with a minumum output of 1.")]
        [SettingPropertyGroup("Hero Skill Experience Tweak", true)]
        public bool HeroSkillExperienceMultiplierEnabled { get; set; } = true;
        [XmlElement]
        [SettingProperty("Enable Flat Experience Multiplier Override", "If enabled, overrides the mod's experience curve multiplier calculation and replaces it with the override multiplier. This means that experience will be multiplied by the same value, independant of the skill level.")]
        [SettingPropertyGroup("Hero Skill Experience Tweak")]
        public bool HeroSkillExperienceOverrideMultiplierEnabled { get; set; } = false;
        [XmlElement]
        [SettingProperty("Hero Skill Experience Override Multiplier", 1f, 15f, "Overrides the mod's default experience multiplier with the flat multiplier that you set. All experience you receive will be multiplied by this value instead.")]
        [SettingPropertyGroup("Hero Skill Experience Tweak")]
        public float HeroSkillExperienceOverrideMultiplier { get; set; } = 1f;
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
        public bool TroopBattleExperienceMultiplierEnabled { get; set; } = true;
        [XmlElement]
        [SettingProperty("Troop Battle Experience Modifier", 1f, 6f, "Multiplies the amount of experience that ALL troops receive during fought battles (Note: Only troops, not heroes. Does not apply to simulated battles.).")]
        [SettingPropertyGroup("Troop Battle Experience Tweaks")]
        public float TroopBattleExperienceMultiplier { get; set; } = 3.0f;
        [XmlElement]
        [SettingProperty("Enable Troop Battle Simulation Experience Multiplier", "In native, auto-calculated battles give an 8x multiplier to troop experience. If enabled, this multiplier is applied on top of that bonus. This is applied to ALL NPC fights on the campaign map.")]
        [SettingPropertyGroup("Troop Battle Experience Tweaks")]
        public bool TroopBattleSimulationExperienceMultiplierEnabled { get; set; } = false;
        [XmlElement]
        [SettingProperty("Troop Battle Simulation Experience Multiplier", 0.5f, 3f, "In native, auto-calculated battles give an 8x multiplier to troop experience. If enabled, this multiplier is applied on top of that bonus. This is applied to ALL NPC fights on the campaign map.")]
        [SettingPropertyGroup("Troop Battle Experience Tweaks")]
        public float TroopBattleSimulationExperienceMultiplier { get; set; } = 1.0f;
        #endregion

        #region Workshop tweaks
        [XmlElement]
        [SettingProperty("Enable Max Workshop Limit Tweak", "Sets the base maximum number of workshops you can have and the limit increase gained per clan tier.")]
        [SettingPropertyGroup("Workshop Tweaks")]
        public bool MaxWorkshopCountTweakEnabled { get; set; } = true;
        [XmlElement]
        [SettingProperty("Base Workshop Limit", 0, 10, "Native value is 1. Sets the base maximum number of workshops you can have.")]
        [SettingPropertyGroup("Workshop Tweaks")]
        public int BaseWorkshopCount { get; set; } = 2;
        [XmlElement]
        [SettingProperty("Bonus Workshops Per Clan Tier", 0, 3, "Sets the base maximum number of workshops you can have and the limit increase gained per clan tier.")]
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
        [SettingProperty("Base Companion Limit", 1, 10, "Native value is 3. Sets the base companion limit.")]
        [SettingPropertyGroup("Companion Limit Tweak")]
        public int CompanionBaseLimit { get; set; } = 3;
        [XmlElement]
        [SettingProperty("Companion Limit Bonus Per Clan Tier", 0, 5, "Native value is 1. Sets the bonus to companion limit per clan tier. This value is multiplied by your clan tier.")]
        [SettingPropertyGroup("Companion Limit Tweak")]
        public int CompanionLimitBonusPerClanTier { get; set; } = 3;
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
        [SettingProperty("Enable Settlement Militia Bonus", "Grants a bonus to militia growth in towns and castles.")]
        [SettingPropertyGroup("Settlement Militia Bonus", true)]
        public bool SettlementMilitiaBonusEnabled { get; set; } = false;
        [XmlElement]
        [SettingProperty("Castle Militia Growth Bonus", 0f, 5f, "Native value is 0. Grants a bonus to militia growth in castles.")]
        [SettingPropertyGroup("Settlement Militia Bonus")]
        public float CastleMilitiaBonus { get; set; } = 1.25f;
        [XmlElement]
        [SettingProperty("Town Militia Growth Bonus", 0f, 5f, "Native value is 0. Grants a bonus to militia growth in towns.")]
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
        [SettingProperty("Castle Food Bonus", 0f, 10f, "Native value is 0. Provides a bonus to food production in castles.")]
        [SettingPropertyGroup("Settlement Food Bonus")]
        public float CastleFoodBonus { get; set; } = 2f;
        [XmlElement]
        [SettingProperty("Town Food Bonus", 0f, 10f, "Native value is 0. Provides a bonus to food production in towns.")]
        [SettingPropertyGroup("Settlement Food Bonus")]
        public float TownFoodBonus { get; set; } = 4f;
        [XmlElement]
        [SettingProperty("Enable Prosperity Food Malus Tweak", "Allows you to adjust the malus to food production received from settlement prosperity.")]
        [SettingPropertyGroup("Settlement Food Bonus")]
        public bool SettlementProsperityFoodMalusTweakEnabled { get; set; } = true;
        [XmlElement]
        [SettingProperty("Prosperity Malus Divisor", 50f, 400f, "Native value is 50. The prosperity of the settlement is divided by this value to calculate the malus. Increase this value to decrease the malus.")]
        [SettingPropertyGroup("Settlement Food Bonus")]
        public float SettlementProsperityFoodMalusDivisor { get; set; } = 100;
        #endregion

        #region Castle buildings bonuses
        [XmlElement]
        [SettingProperty("Enable Castle Training Fields Tweak", "Changes the amount of experience the training fields provides for each level.")]
        [SettingPropertyGroup("Castle Training Fields Tweak", true)]
        public bool CastleTrainingFieldsBonusEnabled { get; set; } = true;
        [XmlElement]
        [SettingProperty("Castle Training Fields Level 1 Experience", 1, 150, "Native value is 1. Changes the amount of experience the training fields provides at level 1.")]
        [SettingPropertyGroup("Castle Training Fields Tweak")]
        public int CastleTrainingFieldsXpAmountLevel1 { get; set; } = 30;
        [XmlElement]
        [SettingProperty("Castle Training Fields Level 2 Experience", 2, 200, "Native value is 2. Changes the amount of experience the training fields provides at level 2.")]
        [SettingPropertyGroup("Castle Training Fields Tweak")]
        public int CastleTrainingFieldsXpAmountLevel2 { get; set; } = 70;
        [XmlElement]
        [SettingProperty("Castle Training Fields Level 3 Experience", 3, 250, "Native value is 3. Changes the amount of experience the training fields provides at level 3.")]
        [SettingPropertyGroup("Castle Training Fields Tweak")]
        public int CastleTrainingFieldsXpAmountLevel3 { get; set; } = 150;

        [XmlElement]
        [SettingProperty("Enable Castle Granary Tweak", "Changes the amount of food storage the castle granary provides per level.")]
        [SettingPropertyGroup("Castle Granary Tweak", true)]
        public bool CastleGranaryBonusEnabled { get; set; } = true;
        [XmlElement]
        [SettingProperty("Castle Granary Food Storage Level 1", 10, 90, "Native value is 10. Changes the amount of food storage the castle granary provides at level 1.")]
        [SettingPropertyGroup("Castle Granary Tweak")]
        public int CastleGranaryStorageAmountLevel1 { get; set; } = 30;
        [XmlElement]
        [SettingProperty("Castle Granary Food Storage Level 2", 20, 180, "Native value is 20. Changes the amount of food storage the castle granary provides at level 2.")]
        [SettingPropertyGroup("Castle Granary Tweak")]
        public int CastleGranaryStorageAmountLevel2 { get; set; } = 45;
        [XmlElement]
        [SettingProperty("Castle Granary Food Storage Level 3", 30, 270, "Native value is 30. Changes the amount of food storage the castle granary provides at level 3.")]
        [SettingPropertyGroup("Castle Granary Tweak")]
        public int CastleGranaryStorageAmountLevel3 { get; set; } = 60;

        [XmlElement]
        [SettingProperty("Enable Castle Gardens Tweak", "Changes the amount of food the castle gardens produce per level.")]
        [SettingPropertyGroup("Castle Gardens Tweak", true)]
        public bool CastleGardensBonusEnabled { get; set; } = true;
        [XmlElement]
        [SettingProperty("Castle Garden Food Production Level 1", 1, 10, "Native value is 1. Changes the amount of food the castle gardens produce at level 1.")]
        [SettingPropertyGroup("Castle Gardens Tweak")]
        public int CastleGardensFoodProductionAmountLevel1 { get; set; } = 3;
        [XmlElement]
        [SettingProperty("Castle Garden Food Production Level 2", 2, 20, "Native value is 2. Changes the amount of food the castle gardens produce at level 2.")]
        [SettingPropertyGroup("Castle Gardens Tweak")]
        public int CastleGardensFoodProductionAmountLevel2 { get; set; } = 6;
        [XmlElement]
        [SettingProperty("Castle Garden Food Production Level 3", 3, 30, "Native value is 3. Changes the amount of food the castle gardens produce at level 3.")]
        [SettingPropertyGroup("Castle Gardens Tweak")]
        public int CastleGardensFoodProductionAmountLevel3 { get; set; } = 9;

        [XmlElement]
        [SettingProperty("Enable Castle Militia Barracks Tweak", "Changes the militia production that the castle militia barracks provides per level.")]
        [SettingPropertyGroup("Castle Militia Barracks Tweak", true)]
        public bool CastleMilitiaBarracksBonusEnabled { get; set; } = true;
        [XmlElement]
        [SettingProperty("Castle Militia Barracks Production Level 1", 1, 10, "Native value is 1. Changes the militia production that the castle militia barracks provides at level 1.")]
        [SettingPropertyGroup("Castle Militia Barracks Tweak")]
        public int CastleMilitiaBarracksAmountLevel1 { get; set; } = 2;
        [XmlElement]
        [SettingProperty("Castle Militia Barracks Production Level 2", 1, 14, "Native value is 2. Changes the militia production that the castle militia barracks provides at level 2.")]
        [SettingPropertyGroup("Castle Militia Barracks Tweak")]
        public int CastleMilitiaBarracksAmountLevel2 { get; set; } = 4;
        [XmlElement]
        [SettingProperty("Castle Militia Barracks Production Level 3", 1, 16, "Native value is 4. Changes the militia production that the castle militia barracks provides at level 3.")]
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
        public bool ClanPartiesLimitTweakEnabled { get; set; } = true;
        [XmlElement]
        [SettingProperty("Base Clan Parties Limit", 1, 10, "Native value is 1. This is the base number of parties you can field.")]
        [SettingPropertyGroup("Clan Parties Tweak")]
        public int BaseClanPartiesLimit { get; set; } = 2;
        [XmlElement]
        [SettingProperty("Clan Parties Bonus Per Clan Tier", 0.0f, 3f, "Native has a calculation for this: 1 party for under tier 3, 2 parties for under tier 5, 3 parties for over tier 5. This setting is multiplied by your clan tier. A value of 0.5 will equate to 1 extra party per 2 clan tiers, which eqautes to the same as native.")]
        [SettingPropertyGroup("Clan Parties Tweak")]
        public float ClanPartiesBonusPerClanTier { get; set; } = 0.5f;
        #endregion
    }
}
