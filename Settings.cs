using MCM.Abstractions.Attributes;
using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Settings.Base.Global;

namespace BannerlordTweaks
{
    public class BannerlordTweaksSettings : AttributeGlobalSettings<BannerlordTweaksSettings>
    {
        public override string Id => "BannerlordTweaksSettings";
        public override string DisplayName => "Bannerlord Tweaks Settings";
        public override string FolderName => "BannerlordTweaksSettings";
        public override string FormatType => "json2";

        #region Miscellaneous

        [SettingPropertyBool("Disable Quest Troops Affecting Morale", Order = 1, RequireRestart = false, HintText = "When enabled, quest troops such as \"Borrowed Troop\" in your party are ignored when party morale is calculated.")]
        public bool QuestCharactersIgnorePartySize { get; set; } = false;

        [SettingPropertyBool("Show Number of Days of Food", Order = 2, RequireRestart = false, HintText = "Changes the number showing how much food you have to instead show how many days' worth of food you have. (Bottom right of campaign map UI).")]
        public bool ShowFoodDaysRemaining { get; set; } = false;

        [SettingPropertyBool("Enable Remote Companion Skill Management", Order = 3, RequireRestart = false, HintText = "Allows you to manage your companions' skills when they are not in your party.")]
        public bool RemoteCompanionSkillManagementEnabled { get; set; } = true;

        [SettingPropertyBool("Enable Auto-Extenson of 'Stop the Conspiracy' Quest Timer", Order = 4, RequireRestart = false, HintText = "Automatically extends the timer of the 'Stop the Conspiracy' quest as TW hasn't finished it yet.")]
        public bool TweakedConspiracyQuestTimerEnabled { get; set; } = true;

        #endregion

        #region Campaign Tweaks - Difficulty Settings

        [SettingPropertyBool("Enable Difficulty Tweaks", Order = 1, RequireRestart = false, IsToggle = true, HintText = "Allows you to change the difficulty settings. These override the options in the game's settings menu."), SettingPropertyGroup("Campaign Tweaks/Difficulty Tweaks")]
        public bool DifficultyTweaksEnabled { get; set; } = false;

        [SettingPropertyBool("Enable Damage to Player Tweak", Order = 1, RequireRestart = false, IsToggle = true, HintText = "Allows you to change the multiplier for damage the player receives."), SettingPropertyGroup("Campaign Tweaks/Difficulty Tweaks/Damage to Player Tweak")]
        public bool DamageToPlayerTweakEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("Damage to Player Tweak Multiplier", 0.1f, 5.0f, HintText = "Native values: Very Easy: 0.3, Easy: 0.67, Realistic: 1. This value is used to calculate the damage player receives."), SettingPropertyGroup("Campaign Tweaks/Difficulty Tweaks/Damage to Player Tweak")]
        public float DamageToPlayerMultiplier { get; set; } = 1.0f;

        [SettingPropertyBool("Enable Damage to Friends Tweak", Order = 1, RequireRestart = false, IsToggle = true, HintText = "Allows you to change the damage the player's friends receive."), SettingPropertyGroup("Campaign Tweaks/Difficulty Tweaks/Damage to Friends Tweak")]
        public bool DamageToFriendsTweakEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("Damage to Friends Tweak Multiplier", 0.1f, 5.0f, HintText = "Native values: Very Easy: 0.3, Easy: 0.67, Realistic: 1. This value is used to calculate the damage the player's friends receive."), SettingPropertyGroup("Campaign Tweaks/Difficulty Tweaks/Damage to Friends Tweak")]
        public float DamageToFriendsMultiplier { get; set; } = 1.0f;

        [SettingPropertyBool("Enable Damage to Player's Troops Tweak", Order = 1, RequireRestart = false, IsToggle = true, HintText = "Allows you to change the multiplier for damage the player's troops receive."), SettingPropertyGroup("Campaign Tweaks/Difficulty Tweaks/Damage to Player's Troops Tweak")]
        public bool DamageToTroopsTweakEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("Damage to Troops Tweak Multiplier", 0.1f, 5.0f, HintText = "Native values: Very Easy: 0.3, Easy: 0.67, Realistic: 1. This value is used to calculate the damage to the player's troops."), SettingPropertyGroup("Campaign Tweaks/Difficulty Tweaks/Damage to Player's Troops Tweak")]
        public float DamageToTroopsMultiplier { get; set; } = 1.0f;

        [SettingPropertyBool("Enable Combat AI Difficulty Tweak", Order = 1, RequireRestart = false, IsToggle = true, HintText = "Allows you to change the AI combat difficulty."), SettingPropertyGroup("Campaign Tweaks/Difficulty Tweaks/Combat AI Difficulty Tweak")]
        public bool CombatAIDifficultyTweakEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("Combat AI Difficulty Tweak Multiplier", 0.1f, 1.0f, HintText = "Native values: Very Easy: 0.1, Easy: 0.32, Realistic: 0.96. This value is used to calculate AI combat difficulty."), SettingPropertyGroup("Campaign Tweaks/Difficulty Tweaks/Combat AI Difficulty Tweak")]
        public float CombatAIDifficultyMultiplier { get; set; } = 0.96f;

        [SettingPropertyBool("Enable Player Map Movement Speed Tweak", Order = 1, RequireRestart = false, IsToggle = true, HintText = "Allows you to change the bonus map movement speed multiplier the player receives."), SettingPropertyGroup("Campaign Tweaks/Difficulty Tweaks/Player Map Movement Speed Bonus Tweak")]
        public bool PlayerMapMovementSpeedBonusTweakEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("Player Map Movement Tweak Multiplier", 0.0f, 2.0f, HintText = "Native values: Very Easy: 0.1, Easy: 0.05, Realistic: 0. This value is used to calculate player's map movement speed."), SettingPropertyGroup("Campaign Tweaks/Difficulty Tweaks/Player Map Movement Speed Bonus Tweak")]
        public float PlayerMapMovementSpeedBonusMultiplier { get; set; } = 0.0f;

        #endregion

        #region Campaign Tweaks - Battle Size Tweak

        [SettingPropertyBool("Battle Size Tweak", Order = 1, IsToggle = true, HintText = "Allows you to set the battle size limit outside of native values. WARNING: Setting this above 1000 can cause performance degradation and crashes."), SettingPropertyGroup("Campaign Tweaks/Battle Size Tweak")]
        public bool BattleSizeTweakEnabled { get; set; } = false;

        [SettingPropertyInteger("Battle Size Limit", 2, 1500, HintText = "Sets the limit for number of troops on a battlefield. WARNING: Setting this above 1000 can cause performance degradation and crashes."), SettingPropertyGroup("Campaign Tweaks/Battle Size Tweak")]
        public int BattleSize { get; set; } = 1000;

        #endregion

        #region Battle Tweaks - Battle Reward Tweaks

        [SettingPropertyBool("Battle Reward Tweaks", Order = 1, RequireRestart = false, IsToggle = true, HintText = "Applies the set multiplier to renown and influence gain from battles (applies to the player only)."), SettingPropertyGroup("Battle Tweaks/Battle Reward Tweaks")]
        public bool BattleRewardTweaksEnabled { get; set; } = true;

        [SettingPropertyFloatingInteger("Battle Renown Multiplier", 1f, 5f, HintText = "Native value is 1.0. The amount of renown you receive from a battle is multiplied by this value."), SettingPropertyGroup("Battle Tweaks/Battle Reward Tweaks")]
        public float BattleRenownMultiplier { get; set; } = 2f;

        [SettingPropertyFloatingInteger("Battle Influence Multiplier", 0.1f, 5f, HintText = "Native value is 1.0. The amount of influence you receive from a battle is multiplied by this value."), SettingPropertyGroup("Battle Tweaks/Battle Reward Tweaks")]
        public float BattleInfluenceMultiplier { get; set; } = 1f;

        [SettingPropertyBool("Aso Apply To AI", Order = 1, RequireRestart = false, HintText = "Applies the renown and influence multipliers to AI parties."), SettingPropertyGroup("Battle Tweaks/Battle Reward Tweaks")]
        public bool BattleRewardApplyToAI { get; set; } = true;

        #endregion

        #region Battle Tweaks - Hideout Tweaks

        [SettingPropertyBool("Enable Hideout Battle Behavior", Order = 1, RequireRestart = false, IsToggle = true, HintText = "Changes game behavior inside hideout battles."), SettingPropertyGroup("Battle Tweaks/Hideout Tweaks")]
        public bool HideoutBattleTroopLimitTweakEnabled { get; set; } = true;

        [SettingPropertyInteger("Battle Tweaks/Hideout Battle Troop Limit", 5, 90, HintText = "Native value is 9 or 10. Changes the maximum troop limit to the set value inside hideout battles. Cannot be higher than 90 because it causes bugs."), SettingPropertyGroup("Battle Tweaks/Hideout Tweaks")]
        public int HideoutBattleTroopLimit { get; set; } = 90;

        [SettingPropertyBool("Continue Hideout Battle On Player Death", Order = 1, RequireRestart = false, HintText = "Native value is false. If enabled, you will not automatically lose the hideout battle if you die. Your troops will charge and the boss duel will be disabled."), SettingPropertyGroup("Battle Tweaks/Hideout Tweaks")]
        public bool ContinueHideoutBattleOnPlayerDeath { get; set; } = false;

        [SettingPropertyBool("Continue Battle On Losing Duel", Order = 1, RequireRestart = false, HintText = "Native value is false. If enabled, you will lose the battle if you lose the boss duel. If disabled, your troops will rush to avenge you and finish everyone off."), SettingPropertyGroup("Battle Tweaks/Hideout Tweaks")]
        public bool ContinueHideoutBattleOnPlayerLoseDuel { get; set; } = true;

        #endregion

        #region Battle Tweaks - Siege Tweaks

        [SettingPropertyBool("Enable Siege Tweaks", Order = 1, RequireRestart = false, IsToggle = true, HintText = "Enable Siege Tweaks."), SettingPropertyGroup("Battle Tweaks/Siege Tweaks")]
        public bool SiegeTweaksEnabled { get; set; } = false;


        [SettingPropertyBool("Enable Siege Construction Progress Tweak", Order = 1, RequireRestart = false, IsToggle = true, HintText = "Adds a multiplier to the construction progress per day for sieges."), SettingPropertyGroup("Battle Tweaks/Siege Tweaks/Construction Progress Tweak")]
        public bool SiegeConstructionProgressPerDayMultiplierEnabled { get; set; } = true;

        [SettingPropertyFloatingInteger("Siege Construction Progress Per Day Multiplier", 0.5f, 10.0f, HintText = "Native value is 1.0. Adds a multiplier to the construction progress per day for sieges. A smaller number results in longer siege times."), SettingPropertyGroup("Battle Tweaks/Siege Tweaks/Construction Progress Tweak")]
        public float SiegeConstructionProgressPerDayMultiplier { get; set; } = 0.8f;


        [SettingPropertyBool("Enable Siege Casualties Tweaks", Order = 2, RequireRestart = false, IsToggle = true, HintText = "Changes the values used to calculate casualties during the siege stage on the campaign map."), SettingPropertyGroup("Battle Tweaks/Siege Tweaks/Casualties Tweaks")]
        public bool SiegeCasualtiesTweakEnabled { get; set; } = true;

        [SettingPropertyInteger("Siege Collateral Damage Casualties", 1, 3, HintText = "Native value is 2.0. Changes the value used to calculate collateral casualties during the campaign map siege stage."), SettingPropertyGroup("Battle Tweaks/Siege Tweaks/Casualties Tweaks")]
        public int SiegeCollateralDamageCasualties { get; set; } = 1;

        [SettingPropertyInteger("Siege Destruction Casualties", 3, 7, HintText = "Native value is 5.0. Changes the value used to calculate desctruction casualties during the campaign map siege stage."), SettingPropertyGroup("Battle Tweaks/Siege Tweaks/Casualties Tweaks")]
        public int SiegeDestructionCasualties { get; set; } = 4;

        #endregion

        #region Battle Tweaks - Troop Battle Experience Tweaks

        [SettingPropertyBool("Enable Troop Battle Experience Multiplier", Order = 1, RequireRestart = false, IsToggle = true, HintText = "Multiplies the amount of experience that ALL troops receive during battles (Note: Only troops, not heroes)."), SettingPropertyGroup("Battle Tweaks/Troop Battle Experience Tweaks")]
        public bool TroopBattleExperienceMultiplierEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("Troop Battle Experience Modifier", 1f, 6f, HintText = "Native value is 1.0. Multiplies the amount of experience that ALL troops receive during fought battles (Note: Only troops, not heroes. Does not apply to simulated battles.)."), SettingPropertyGroup("Battle Tweaks/Troop Battle Experience Tweaks")]
        public float TroopBattleExperienceMultiplier { get; set; } = 1.0f;

        [SettingPropertyBool("Enable Troop Battle Simulation Experience Multiplier", Order = 1, RequireRestart = false, IsToggle = true, HintText = "Provides a multiplier to experience gained from simulated battles. This is applied to all fights (including NPC fights) on the campaign map."), SettingPropertyGroup("Battle Tweaks/Troop Battle Experience Tweaks/Simulation Experience Tweak")]
        public bool TroopBattleSimulationExperienceMultiplierEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("Troop Battle Simulation Experience Multiplier", 0.5f, 8f, HintText = "Native value is 1.0. Provides a multiplier to experience gained from simulated battles. This is applied to all simulated fights on the campaign map."), SettingPropertyGroup("Battle Tweaks/Troop Battle Experience Tweaks/Simulation Experience Tweak")]
        public float TroopBattleSimulationExperienceMultiplier { get; set; } = 1.0f;

        #endregion

        #region Battle Tweaks - Weapon Cut Through Tweaks

        [SettingPropertyBool("All Two-Handed Weapons Cut Through", Order = 1, RequireRestart = false, HintText = "Allows all two-handed weapon types to cut through and hit multiple people."), SettingPropertyGroup("Battle Tweaks/Weapon Cut Through Tweaks")]
        public bool TwoHandedWeaponsSliceThroughEnabled { get; set; } = false;

        [SettingPropertyBool("All One-Handed Weapons Cut Through", Order = 1, RequireRestart = false, HintText = "Allows all single-handed weapon types to cut through and hit multiple people."), SettingPropertyGroup("Battle Tweaks/Weapon Cut Through Tweaks")]
        public bool SingleHandedWeaponsSliceThroughEnabled { get; set; } = false;

        #endregion

        #region Character Tweaks - Age Tweaks

        [SettingPropertyBool("Enable Age Tweaks", RequireRestart = false, IsToggle = true, HintText = "Enables the tweaking of character age behaviour."), SettingPropertyGroup("Character Tweaks/Age Tweaks")]
        public bool AgeTweaksEnabled { get; set; } = false;

        [SettingPropertyInteger("Become Infant Age", 0, 125, HintText = "Native: 3. Must be less than Become Child Age.", Order = 1), SettingPropertyGroup("Character Tweaks/Age Tweaks")]
        public int BecomeInfantAge { get; set; } = 3;

        [SettingPropertyInteger("Become Child Age", 0, 125, HintText = "Native: 6. Must be less than Become Teenager Age.", Order = 1), SettingPropertyGroup("Character Tweaks/Age Tweaks")]
        public int BecomeChildAge { get; set; } = 6;

        [SettingPropertyInteger("Become Teenager Age", 0, 125, HintText = "Native: 14. Must be less than Hero Comes Of Age.", Order = 2), SettingPropertyGroup("Character Tweaks/Age Tweaks")]
        public int BecomeTeenagerAge { get; set; } = 14;

        [SettingPropertyInteger("Hero Comes Of Age", 0, 100, HintText = "Native: 18. Must be less than Become Old Age.", Order = 3), SettingPropertyGroup("Character Tweaks/Age Tweaks")]
        public int HeroComesOfAge { get; set; } = 18;

        [SettingPropertyInteger("Become Old Age", 0, 125, HintText = "Native: 47. Must be less than Max Age.", Order = 4), SettingPropertyGroup("Character Tweaks/Age Tweaks")]
        public int BecomeOldAge { get; set; } = 47;

        [SettingPropertyInteger("Max Age", 0, 125, HintText = "Native: 125.", Order = 5), SettingPropertyGroup("Character Tweaks/Age Tweaks")]
        public int MaxAge { get; set; } = 125;

        #endregion

        #region Character Tweaks - Attribute Focus Point Tweaks

        [SettingPropertyBool("Enable Attribute-Focus Point Tweaks", Order = 1, RequireRestart = false, IsToggle = true, HintText = "Changes the values used to calculate how many Attribute and Focus points Heroes gain."), SettingPropertyGroup("Character Tweaks/Attribute-Focus Points Tweaks")]
        public bool AttributeFocusPointTweakEnabled { get; set; } = false;


        [SettingPropertyInteger("Levels To Gain For Attribute Points", 1, 5, HintText = "Native value is 4. How many levels you need to gain to receive an attribute point."), SettingPropertyGroup("Character Tweaks/Attribute-Focus Points Tweaks")]
        public int AttributePointRequiredLevel { get; set; } = 4;


        [SettingPropertyInteger("Focus Point Per Level", 1, 5, HintText = "Native value is 1. This is the amount of focus points earned per level."), SettingPropertyGroup("Character Tweaks/Attribute-Focus Points Tweaks")]
        public int FocusPointsPerLevel { get; set; } = 1;

        #endregion

        #region Character Tweaks - Hero Skill Multiplier Tweaks

        [SettingPropertyBool("Enable Hero Skill Experience Multiplier", Order = 1, RequireRestart = false, HintText = "Applies a multiplier to the amount of experience recieved for skills. Affects the player only. 1.0 = No Bonus. 1.1 = 10%, etc."), SettingPropertyGroup("Character Tweaks/Hero Skill Experience Multiplier")]
        public bool HeroSkillExperienceMultiplierEnabled { get; set; } = false;

        [SettingPropertyBool("Enable Companion Skill Experience Multiplier", Order = 1, RequireRestart = false, HintText = "Applies a multiplier to the amount of experience recieved for skills. Affects Compaions only. 1.0 = No Bonus. 1.1 = 10%, etc."), SettingPropertyGroup("Character Tweaks/Hero Skill Experience Multiplier")]
        public bool CompanionSkillExperienceMultiplierEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("Hero Skill Experience Multiplier", 1f, 5f, RequireRestart = false, HintText = "Applies a multiplier to the amount of experience recieved for skills. Affects the player only."), SettingPropertyGroup("Character Tweaks/Hero Skill Experience Multiplier")]
        public float HeroSkillExperienceMultiplier { get; set; } = 1f;

        [SettingPropertyFloatingInteger("Companion Skill Experience Multiplier", 1f, 20f, RequireRestart = false, HintText = "Applies a multiplier to the amount of experience recieved for skills. Affects the Companion only."), SettingPropertyGroup("Character Tweaks/Hero Skill Experience Multiplier")]
        public float CompanionSkillExperienceMultiplier { get; set; } = 1f;

        #endregion

        #region Character Tweaks - Pregnancy Tweaks

        [SettingPropertyBool("Disable Stillbirths", Order = 1, RequireRestart = false, HintText = "Disables the chance of children dying when born."), SettingPropertyGroup("Character Tweaks/Pregnancy Tweaks")]
        public bool NoStillbirthsTweakEnabled { get; set; } = false;

        [SettingPropertyBool("Disable Maternal Mortality", Order = 1, RequireRestart = false, HintText = "Disables the chance of mothers dying when giving birth."), SettingPropertyGroup("Character Tweaks/Pregnancy Tweaks")]
        public bool NoMaternalMortalityTweakEnabled { get; set; } = false;

        [SettingPropertyBool("Enable Pregnancy Duration Tweak", Order = 1, RequireRestart = false, IsToggle = true, HintText = "Allows for adjusting the duration for a pregnancy."), SettingPropertyGroup("Character Tweaks/Pregnancy Tweaks/Pregnancy Duration Tweak")]
        public bool PregnancyDurationTweakEnabled { get; set; } = false;

        [SettingPropertyInteger("Pregnancy Duration", 1, 96, HintText = "Native value is 36 days."), SettingPropertyGroup("Character Tweaks/Pregnancy Tweaks/Pregnancy Duration Tweak")]
        public int PregnancyDuration { get; set; } = 36;

        [SettingPropertyBool("Enable Gender Ratio Tweak", Order = 1, RequireRestart = false, IsToggle = true, HintText = "Allows for adjusting the gender ratio of births."), SettingPropertyGroup("Character Tweaks/Pregnancy Tweaks/Female Offspring Probability Tweak")]
        public bool FemaleOffspringProbabilityTweakEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("Probability for female children", -1.0f, 1.0f, HintText = "Native value is 0.51. Set to -1 to disable female births."), SettingPropertyGroup("Character Tweaks/Pregnancy Tweaks/Female Offspring Probability Tweak")]
        public float FemaleOffspringProbability { get; set; } = 0.51f;

        [SettingPropertyBool("Enable Twins Probability Tweak", Order = 1, RequireRestart = false, IsToggle = true, HintText = "Allows for adjusting the chance of giving birth to twins."), SettingPropertyGroup("Character Tweaks/Pregnancy Tweaks/Twins Probability Tweak")]
        public bool TwinsProbabilityTweakEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("Probability to deliver twins", -1.0f, 1.0f, HintText = "Native value is 0.03. Determines the chance of giving birth to twins."), SettingPropertyGroup("Character Tweaks/Pregnancy Tweaks/Twins Probability Tweak")]
        public float TwinsProbability { get; set; } = 0.03f;

        [SettingPropertyBool("Enable Pregnancy Chance Tweaks", Order = 1, RequireRestart = false, IsToggle = true, HintText = "Enabling this will completely override the daily pregnancy check. All settings below will be applied!"), SettingPropertyGroup("Character Tweaks/Pregnancy Tweaks/Pregnancy Chance Tweaks")]
        public bool DailyChancePregnancyTweakEnabled { get; set; } = false;

        [SettingPropertyBool("Player is Fertile", Order = 1, RequireRestart = false, HintText = "Native: true. If set to false, the player will not be able to have children."), SettingPropertyGroup("Character Tweaks/Pregnancy Tweaks/Pregnancy Chance Tweaks")]
        public bool PlayerCharacterFertileEnabled { get; set; } = true;

        [SettingPropertyInteger("Min Pregnancy Age", 0, 999, HintText = "Native: 18. Minimum age that someone can get pregnant.", RequireRestart = false), SettingPropertyGroup("Character Tweaks/Pregnancy Tweaks/Pregnancy Chance Tweaks")]
        public int MinPregnancyAge { get; set; } = 18;

        [SettingPropertyInteger("Max Pregnancy Age", 0, 999, HintText = "Native: 45. Maximum age that someone can get pregnant.", RequireRestart = false), SettingPropertyGroup("Character Tweaks/Pregnancy Tweaks/Pregnancy Chance Tweaks")]
        public int MaxPregnancyAge { get; set; } = 45;

        [SettingPropertyBool("Enable Clan Fertility Bonus", Order = 1, RequireRestart = false, IsToggle = true, HintText = "Enable a bonus to your clan members to become pregnant."), SettingPropertyGroup("Character Tweaks/Pregnancy Tweaks/Pregnancy Chance Tweaks/Clan Fertility Bonus")]
        public bool ClanFertilityBonusEnabled { get; set; } = true;

        [SettingPropertyFloatingInteger("Clan Fertility Bonus", 1f, 5f, RequireRestart = false, HintText = "Adds a % bonus to your clan members to become pregnant. 1 = No Bonus, 2 = 2x chance, 5 = 5x chance. Note: May not do much after ~6-8 kids due to the base pregnancy calculations."), SettingPropertyGroup("Character Tweaks/Pregnancy Tweaks/Pregnancy Chance Tweaks/Clan Fertility Bonus")]
        public float ClanFertilityBonus { get; set; } = 1.1f;

        [SettingPropertyBool("Enable Max Children Tweak", Order = 1, RequireRestart = false, IsToggle = true, HintText = "Native: false. Sets the maximum number of children that someone can have."), SettingPropertyGroup("Character Tweaks/Pregnancy Tweaks/Pregnancy Chance Tweaks/Max Children Tweak")]
        public bool MaxChildrenTweakEnabled { get; set; } = false;

        [SettingPropertyInteger("Max Children", 0, 999, HintText = "Default: 5. Maximum number of children that someone can have.", RequireRestart = false), SettingPropertyGroup("Character Tweaks/Pregnancy Tweaks/Pregnancy Chance Tweaks/Max Children Tweak")]
        public int MaxChildren { get; set; } = 5;

        #endregion

        #region Clan Tweaks - Companion Limit Tweak

        [SettingPropertyBool("Enable Companion Limit Tweak", Order = 1, RequireRestart = false, IsToggle = true, HintText = "Sets the base companion limit and the bonus gained per clan tier."), SettingPropertyGroup("Clan Tweaks/Companion Limit Tweak")]
        public bool CompanionLimitTweakEnabled { get; set; } = false;

        [SettingPropertyInteger("Base Companion Limit", 1, 20, HintText = "Native value is 3. Sets the base companion limit."), SettingPropertyGroup("Clan Tweaks/Companion Limit Tweak")]
        public int CompanionBaseLimit { get; set; } = 3;

        [SettingPropertyInteger("Companion Limit Bonus Per Clan Tier", 0, 10, HintText = "Native value is 1. Sets the bonus to companion limit per clan tier. This value is multiplied by your clan tier. Note that Leadership 'We Pledge Our Swords' perk will also increae this number by 1."), SettingPropertyGroup("Clan Tweaks/Companion Limit Tweak")]
        public int CompanionLimitBonusPerClanTier { get; set; } = 3;

        [SettingPropertyBool("Enable Unlimited Wanderers Patch", Order = 1, RequireRestart = false, HintText = "Removes the soft cap on the maximum number of potential companions who can spawn. Native limits the # of wanderers to ~25. This will remove that limit. Note: Requires a new campaign to take effect, as the cap is set when a new game is generated. Credit to Bleinz for his UnlimitedWanderers mod."), SettingPropertyGroup("Clan Tweaks/Companion Limit Tweak")]
        public bool UnlimitedWanderersPatch { get; set; } = false;


        #endregion

        #region Clan Tweaks - Clan Parties Tweaks

        [SettingPropertyBool("Enable Clan Parties Tweak", Order = 1, RequireRestart = false, IsToggle = true, HintText = "Changes the base number of parties you can field and the bonus gained per clan tier."), SettingPropertyGroup("Clan Tweaks/Clan Parties Tweak")]
        public bool ClanPartiesLimitTweakEnabled { get; set; } = false;

        [SettingPropertyInteger("Base Clan Parties Limit", 1, 10, HintText = "Native value is 1. This is the base number of parties you can field."), SettingPropertyGroup("Clan Tweaks/Clan Parties Tweak")]
        public int BaseClanPartiesLimit { get; set; } = 1;

        [SettingPropertyFloatingInteger("Clan Parties Bonus Per Clan Tier", 0.0f, 6f, HintText = "Native has a calculation for this: 1 party for under tier 3, 2 parties for under tier 5, 3 parties for over tier 5. This setting is multiplied by your clan tier. A value of 0.5 will equate to 1 extra party per 2 clan tiers, which eqautes to the same as native."), SettingPropertyGroup("Clan Tweaks/Clan Parties Tweak")]
        public float ClanPartiesBonusPerClanTier { get; set; } = 0.5f;

        [SettingPropertyBool("Enable AI Clan Parties Tweak", Order = 1, RequireRestart = false, IsToggle = true, HintText = "Changes the base number of parties AI Lords can field."), SettingPropertyGroup("Clan Tweaks/Clan Parties Tweak/AI Lord Parties Tweak")]
        public bool AIClanPartiesLimitTweakEnabled { get; set; } = false;

        [SettingPropertyBool("Also Adjust Minor Factions", Order = 1, RequireRestart = false, HintText = "Changes the base number of parties AI Minor Factiopn Lords can field. [Native is 1-4, depending on Clan tier.]"), SettingPropertyGroup("Clan Tweaks/Clan Parties Tweak/AI Lord Parties Tweak")]
        public bool AIMinorClanPartiesLimitTweakEnabled { get; set; } = false;

        [SettingPropertyInteger("Add to AI Clan Parties Limit", 1, 10, Order = 1, RequireRestart = false, HintText = "This adds to the the base number of parties AI Lords can field. [Native is 1 for Tier 3 and below, 2 at T4, 3 at T5 and up.] Minor Factions are not included unless the option below is also enabled."), SettingPropertyGroup("Clan Tweaks/Clan Parties Tweak/AI Lord Parties Tweak")]
        public int BaseAIClanPartiesLimit { get; set; } = 0;

        [SettingPropertyBool("Adjust Custom Spawn Parties", Order = 2, RequireRestart = false, IsToggle = true, HintText = "Changes the base number of parties Custom Spawn lords can field. [Native is 1-4, depending on Clan tier.]"), SettingPropertyGroup("Clan Tweaks/Clan Parties Tweak/Custom Spawn Parties Tweak")]
        public bool AICustomSpawnPartiesLimitTweakEnabled { get; set; } = false;

        [SettingPropertyInteger("Add to Custom Spawn Parties Limit", 1, 10, Order = 1, RequireRestart = false, HintText = "This adds to the the base number of parties Custom Lords can field. [Native is 1 for Tier 3 and below, 2 at T4, 3 at T5 and up.] Minor Factions are not included unless the option below is also enabled."), SettingPropertyGroup("Clan Tweaks/Clan Parties Tweak/Custom Spawn Parties Tweak")]
        public int BaseAICustomSpawnPartiesLimit { get; set; } = 0;

        #endregion

        #region Crafting Tweaks - Crafting Stamina Tweaks
        [SettingPropertyBool("Crafting Stamina Tweaks", Order = 1, RequireRestart = false, IsToggle = true, HintText = "Enables tweaks which affect crafting stamina."), SettingPropertyGroup("Crafting Tweaks/Crafting Stamina Tweaks")]
        public bool CraftingStaminaTweakEnabled { get; set; } = true;

        [SettingPropertyInteger("Max Crafting Stamina", 100, 1000, HintText = "Native value is 400. Sets the maximum crafting stamina value."), SettingPropertyGroup("Crafting Tweaks/Crafting Stamina Tweaks")]
        public int MaxCraftingStamina { get; set; } = 400;

        [SettingPropertyInteger("Crafting Stamina Gain", 0, 100, HintText = "Native value is 5. You gain this amount of crafting stamina per hour."), SettingPropertyGroup("Crafting Tweaks/Crafting Stamina Tweaks")]
        public int CraftingStaminaGainAmount { get; set; } = 10;

        [SettingPropertyBool("Ignore Crafting Stamina", Order = 1, RequireRestart = false, HintText = "Native value is false. This disables crafting stamina completely. You will still lose crafting stamina when you craft, but you will still be able to craft when you hit zero."), SettingPropertyGroup("Crafting Tweaks/Crafting Stamina Tweaks")]
        public bool IgnoreCraftingStamina { get; set; } = false;

        [SettingPropertyFloatingInteger("Crafting Stamina Gain Outside Settlement Multiplier", 0f, 1f, HintText = "Native value is 0.0. In native, you do not gain crafting stamina if you are not resting inside a settlement."), SettingPropertyGroup("Crafting Tweaks/Crafting Stamina Tweaks")]
        public float CraftingStaminaGainOutsideSettlementMultiplier { get; set; } = 1f;

        #endregion

        #region Crafting Tweaks - Smelting Tweaks

        [SettingPropertyBool("Hide Locked Weapons in Smelting Menu", Order = 1, RequireRestart = false, IsToggle = true, HintText = "Native value is false. Prevent weapons that you have locked in your inventory from showing up in the smelting list to prevent accidental smelting."), SettingPropertyGroup("Crafting Tweaks/Smelting Tweaks")]
        public bool PreventSmeltingLockedItems { get; set; } = false;


        [SettingPropertyBool("Enable Unlocking Parts From Smelted Weapons", Order = 1, RequireRestart = false, HintText = "Native value is false. Unlock the parts that a weapon is made of when you smelt it."), SettingPropertyGroup("Crafting Tweaks/Smelting Tweaks")]
        public bool AutoLearnSmeltedParts { get; set; } = true;

        #endregion

        #region Kingdom Tweaks - Lord Bartering Tweaks

        [SettingPropertyBool("Bartering Tweaks", Order = 1, RequireRestart = false, IsToggle = true, HintText = "Enables tweaks which affect bartering (Marriage, Factions Joining You, etc.)"), SettingPropertyGroup("Kingdom Tweaks/Lord Bartering Tweaks")]
        public bool BarterablesTweaksEnabled { get; set; } = true;

        [SettingPropertyInteger("Faction Joining Barter Adjustment", 1, 200, Order = 1, RequireRestart = false, HintText = "Adjust the % cost of swaying a fatcion to join your kingdom. Native value is 100% (no change). 50 = 50% reduction in cost. 150 = 50% increase in cost, etc."), SettingPropertyGroup("Kingdom Tweaks/Lord Bartering Tweaks")]
        public int BarterablesJoinKingdomAsClanAdjustment { get; set; } = 100;

        [SettingPropertyBool("Use Alternate Formula for Faction Joining Barter", Order = 1, RequireRestart = false, HintText = "Applies alternate formula for calculating cost of swaying a faction to join your kingdom, with more emphasis on relationsip. [The higher your relationship to the lord, the cheaper the barter]."), SettingPropertyGroup("Kingdom Tweaks/Lord Bartering Tweaks")]
        public bool BarterablesJoinKingdomAsClanAltFormulaEnabled { get; set; } = false;


        #endregion

        #region Party Tweaks - Army Tweaks

        /*
        [SettingPropertyBool("All-Clan Armies Lose No Coehsion", Order = 1, RequireRestart = false, HintText = "Armies composed of only clan parties lose no coehsion."), SettingPropertyGroup("Party Tweaks/Army Tweaks")]
        public bool ClanArmyLosesNoCohesionEnabled { get; set; } = false;
        */

        #endregion

        #region Party Tweaks - Caravan Tweaks

        [SettingPropertyBool("Enable Player Caravan Party Size Tweak", Order = 1, RequireRestart = false, IsToggle = true, HintText = "Applies a configured value to your caravan party size"), SettingPropertyGroup("Party Tweaks/Caravan Party Size Tweak")]
        public bool PlayerCaravanPartySizeTweakEnabled { get; set; } = false;

        [SettingPropertyInteger("Player Caravan Party Size", 30, 100, HintText = "Native: 30. Be aware that bigger parties are also slower parties."), SettingPropertyGroup("Party Tweaks/Caravan Party Size Tweak")]
        public int PlayerCaravanPartySize { get; set; } = 30;

        #endregion

        #region Party Tweaks - Party Size Tweaks

        [SettingPropertyBool("Enable Party Size Bonus", Order = 1, RequireRestart = false, IsToggle = true, HintText = "Applies a bonues to your party size based on your leadership and steward skills."), SettingPropertyGroup("Party Tweaks/Party Size Bonus")]
        public bool PartySizeTweakEnabled { get; set; } = true;

        [SettingPropertyBool("Enable Leadership Bonus", Order = 1, RequireRestart = false, HintText = "Applies a bonus equal to the set percentage of your leadership skill to your party size."), SettingPropertyGroup("Party Tweaks/Party Size Bonus")]
        public bool LeadershipPartySizeBonusEnabled { get; set; } = true;

        [SettingPropertyFloatingInteger("Leadership Percentage Bonus", 0f, 1f, HintText = "Applies a bonus equal to the set percentage of your leadership skill to your party size."), SettingPropertyGroup("Party Tweaks/Party Size Bonus")]
        public float LeadershipPartySizeBonus { get; set; } = 0.3f;

        [SettingPropertyBool("Enable Steward Bonus", Order = 1, RequireRestart = false, HintText = "Applies a bonus equal to the set percentage of your steward skill to your party size."), SettingPropertyGroup("Party Tweaks/Party Size Bonus")]
        public bool StewardPartySizeBonusEnabled { get; set; } = true;

        [SettingPropertyFloatingInteger("Steward Percentage Bonus", 0f, 1f, HintText = "Applies a bonus equal to the set percentage of your leadership skill to your party size."), SettingPropertyGroup("Party Tweaks/Party Size Bonus")]
        public float StewardPartySizeBonus { get; set; } = 0.3f;

        #endregion

        #region Party Tweaks - Party Wage Tweaks

        [SettingPropertyBool("Enable Wage Tweaks", Order = 1, RequireRestart = false, IsToggle = true, HintText = "Allows you to reduce/increase wages for various groups. [experimental]"), SettingPropertyGroup("Party Tweaks/Wage Tweaks")]
        public bool PartyWageTweaksEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("Party Wage Adjustment", .05f, 5f, HintText = "Adjusts party wages to a % of native value. Minimum 5% (.05), Maximum 500% (5.00) [Native 100% (1.00)]", Order = 1, RequireRestart = false), SettingPropertyGroup("Party Tweaks/Wage Tweaks")]
        public float PartyWagePercent { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Garrison Wage Adjustment", .05f, 5f, HintText = "Adjusts garrison wages to a % of native value. Minimum 5% (.05), Maximum 500% (5.00) [Native 100% (1.00)]", Order = 2, RequireRestart = false), SettingPropertyGroup("Party Tweaks/Wage Tweaks")]
        public float GarrisonWagePercent { get; set; } = 1.0f;

        [SettingPropertyBool("Also Apply Wage Tweaks to Your Faction", Order = 1, RequireRestart = false, HintText = "Applies the wage adjust to your clan/faction parties as well"), SettingPropertyGroup("Party Tweaks/Wage Tweaks")]
        public bool ApplyWageTweakToFaction { get; set; } = false;

        #endregion

        #region Party Tweaks - Troop Daily Experience Tweak

        [SettingPropertyBool("Enable Daily Troop Experience Gain", Order = 1, RequireRestart = false, IsToggle = true, HintText = "Gives troops in a party an amount of experience each day based upon the leader's Leadership skill. By default only applies to the player."), SettingPropertyGroup("Party Tweaks/Troop Daily ExperienceTweak")]
        public bool DailyTroopExperienceTweakEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("Percentage of Leadership", 0.01f, 5f, RequireRestart = false, HintText = "The percentage of the leader's Leadership skill to be given as experience to their troops."), SettingPropertyGroup("Party Tweaks/Troop Daily ExperienceTweak")]
        public float LeadershipPercentageForDailyExperienceGain { get; set; } = 0.5f;

        [SettingPropertyBool("Apply to Player's Clan Members", Order = 1, RequireRestart = false, HintText = "Applies the daily troop experience gain to members of the player's clan also."), SettingPropertyGroup("Party Tweaks/Troop Daily ExperienceTweak")]
        public bool DailyTroopExperienceApplyToPlayerClanMembers { get; set; } = false;

        [SettingPropertyBool("Apply to all NPC Lords", Order = 1, RequireRestart = false, HintText = "Applies the daily troop experience gain to all NPC lords."), SettingPropertyGroup("Party Tweaks/Troop Daily ExperienceTweak")]
        public bool DailyTroopExperienceApplyToAllNPC { get; set; } = false;

        [SettingPropertyBool("Display Message", Order = 1, RequireRestart = false, HintText = "Displays a message showing the amount of experience granted."), SettingPropertyGroup("Party Tweaks/Troop Daily ExperienceTweak")]
        public bool DisplayMessageDailyExperienceGain { get; set; } = false;

        [SettingPropertyInteger("Required Leadership Level", 1, 200, RequireRestart = false, HintText = "The Leadership level required to start giving experience to troops."), SettingPropertyGroup("Party Tweaks/Troop Daily ExperienceTweak")]
        public int DailyTroopExperienceRequiredLeadershipLevel { get; set; } = 30;

        #endregion

        #region Prisoner Tweaks

        [SettingPropertyBool("Enable Imprisonment Period Tweak", Order = 1, RequireRestart = false, IsToggle = true, HintText = "Adds a minimum amount of time before lords can attempt to escape imprisonment."), SettingPropertyGroup("Prisoner Tweaks")]
        public bool PrisonerImprisonmentTweakEnabled { get; set; } = false;

        [SettingPropertyBool("Player Prisoners Only", Order = 1, RequireRestart = false, HintText = "Whether the tweak should be applied only to prisoners held by the player."), SettingPropertyGroup("Prisoner Tweaks")]
        public bool PrisonerImprisonmentPlayerOnly { get; set; } = true;

        [SettingPropertyInteger("Minimum Days of Imprisonment", 0, 180, RequireRestart = false, HintText = "The minimum number of days a lord will remain imprisoned before they can attempt to escape."), SettingPropertyGroup("Prisoner Tweaks")]
        public int MinimumDaysOfImprisonment { get; set; } = 10;

        [SettingPropertyBool("Enable Missing Prisoner Hero Fix", Order = 2, HintText = "Will attempt to detect and release prisoner Heroes who may be bugged and do not respawn. Will trigger 3 days after the Minimum Days of Imprisonment setting."), SettingPropertyGroup("Prisoner Tweaks")]
        public bool EnableMissingHeroFix { get; set; } = true;

        [SettingPropertyBool("Enable Prisoner Size Bonus", Order = 1, RequireRestart = false, IsToggle = true, HintText = "Enables a flat % bonus to your party's maximum prisoner size."), SettingPropertyGroup("Prisoner Tweaks/Prisoner Size Tweak")]
        public bool PrisonerSizeTweakEnabled { get; set; } = false;

        [SettingPropertyInteger("Prisoner Size Bonus", 0, 500, Order = 1, RequireRestart = false, HintText = "Adds a flat % bonues to your party's maximum prisoner size. 10% = 10, +50% = 50, etc. [Native is 0]"), SettingPropertyGroup("Prisoner Tweaks/Prisoner Size Tweak")]
        public float PrisonerSizeTweakPercent { get; set; } = 0;

        #endregion

        #region Settlement Tweaks - Disable Troop Donations

        [SettingPropertyBool("Disable Troop Donations", Order = 1, RequireRestart = false, IsToggle = true, HintText = "Disables your clan (and optionally kingdom) parties from donating troops to owned settlements."), SettingPropertyGroup("Settlement Tweaks/Disable Troop Donation Tweak")]
        public bool DisableTroopDonationPatchEnabled { get; set; } = false;

        [SettingPropertyBool("Disable Troop Donations - Apply to Faction", Order = 1, RequireRestart = false, HintText = "Extends disabling of troop donations to owned settlements to your entire kingdom."), SettingPropertyGroup("Settlement Tweaks/Disable Troop Donation Tweak")]
        public bool DisableTroopDonationFactionWideEnabled { get; set; } = false;

        #endregion

        #region Settlement Tweaks - Settlement Buildings Tweaks - Castle Buildings Tweaks

        [SettingPropertyBool("Enable Castle Training Fields Tweak", Order = 1, RequireRestart = false, IsToggle = true, HintText = "Changes the amount of experience the training fields provides for each level."), SettingPropertyGroup("Settlement Tweaks/Buildings Tweaks/Castle Buildings Tweaks/Castle Training Fields Tweak")]
        public bool CastleTrainingFieldsBonusEnabled { get; set; } = true;

        [SettingPropertyInteger("Castle Training Fields Level 1 Experience", 1, 100, HintText = "Native value is 1. Changes the amount of experience the training fields provides at level 1."), SettingPropertyGroup("Settlement Tweaks/Buildings Tweaks/Castle Buildings Tweaks/Castle Training Fields Tweak")]
        public int CastleTrainingFieldsXpAmountLevel1 { get; set; } = 1;

        [SettingPropertyInteger("Castle Training Fields Level 2 Experience", 2, 200, HintText = "Native value is 2. Changes the amount of experience the training fields provides at level 2."), SettingPropertyGroup("Settlement Tweaks/Buildings Tweaks/Castle Buildings Tweaks/Castle Training Fields Tweak")]
        public int CastleTrainingFieldsXpAmountLevel2 { get; set; } = 2;

        [SettingPropertyInteger("Castle Training Fields Level 3 Experience", 3, 300, HintText = "Native value is 3. Changes the amount of experience the training fields provides at level 3."), SettingPropertyGroup("Settlement Tweaks/Buildings Tweaks/Castle Buildings Tweaks/Castle Training Fields Tweak")]
        public int CastleTrainingFieldsXpAmountLevel3 { get; set; } = 3;


        [SettingPropertyBool("Enable Castle Granary Tweak", Order = 1, RequireRestart = false, IsToggle = true, HintText = "Changes the amount of food storage the castle granary provides per level."), SettingPropertyGroup("Settlement Tweaks/Buildings Tweaks/Castle Buildings Tweaks/Castle Granary Tweak")]
        public bool CastleGranaryBonusEnabled { get; set; } = true;

        [SettingPropertyInteger("Castle Granary Food Storage Level 1", 100, 1000, HintText = "Native value is 100. Changes the amount of food storage the castle granary provides at level 1."), SettingPropertyGroup("Settlement Tweaks/Buildings Tweaks/Castle Buildings Tweaks/Castle Granary Tweak")]
        public int CastleGranaryStorageAmountLevel1 { get; set; } = 100;

        [SettingPropertyInteger("Castle Granary Food Storage Level 2", 200, 2000, HintText = "Native value is 200. Changes the amount of food storage the castle granary provides at level 2."), SettingPropertyGroup("Settlement Tweaks/Buildings Tweaks/Castle Buildings Tweaks/Castle Granary Tweak")]
        public int CastleGranaryStorageAmountLevel2 { get; set; } = 200;

        [SettingPropertyInteger("Castle Granary Food Storage Level 3", 300, 3000, HintText = "Native value is 300. Changes the amount of food storage the castle granary provides at level 3."), SettingPropertyGroup("Settlement Tweaks/Buildings Tweaks/Castle Buildings Tweaks/Castle Granary Tweak")]
        public int CastleGranaryStorageAmountLevel3 { get; set; } = 300;


        [SettingPropertyBool("Enable Castle Gardens Tweak", Order = 1, RequireRestart = false, IsToggle = true, HintText = "Changes the amount of food the castle gardens produce per level."), SettingPropertyGroup("Settlement Tweaks/Buildings Tweaks/Castle Buildings Tweaks/Castle Gardens Tweak")]
        public bool CastleGardensBonusEnabled { get; set; } = true;

        [SettingPropertyInteger("Castle Garden Food Production Level 1", 5, 50, HintText = "Native value is 5. Changes the amount of food the castle gardens produce at level 1."), SettingPropertyGroup("Settlement Tweaks/Buildings Tweaks/Castle Buildings Tweaks/Castle Gardens Tweak")]
        public int CastleGardensFoodProductionAmountLevel1 { get; set; } = 5;

        [SettingPropertyInteger("Castle Garden Food Production Level 2", 10, 100, HintText = "Native value is 10. Changes the amount of food the castle gardens produce at level 2."), SettingPropertyGroup("Settlement Tweaks/Buildings Tweaks/Castle Buildings Tweaks/Castle Gardens Tweak")]
        public int CastleGardensFoodProductionAmountLevel2 { get; set; } = 10;

        [SettingPropertyInteger("Castle Garden Food Production Level 3", 15, 150, HintText = "Native value is 15. Changes the amount of food the castle gardens produce at level 3."), SettingPropertyGroup("Settlement Tweaks/Buildings Tweaks/Castle Buildings Tweaks/Castle Gardens Tweak")]
        public int CastleGardensFoodProductionAmountLevel3 { get; set; } = 15;


        [SettingPropertyBool("Enable Castle Militia Barracks Tweak", Order = 1, RequireRestart = false, IsToggle = true, HintText = "Changes the militia production that the castle militia barracks provides per level."), SettingPropertyGroup("Settlement Tweaks/Buildings Tweaks/Castle Buildings Tweaks/Castle Militia Barracks Tweak")]
        public bool CastleMilitiaBarracksBonusEnabled { get; set; } = true;

        [SettingPropertyInteger("Castle Militia Barracks Production Level 1", 1, 10, HintText = "Native value is 1. Changes the militia production that the castle militia barracks provides at level 1."), SettingPropertyGroup("Settlement Tweaks/Buildings Tweaks/Castle Buildings Tweaks/Castle Militia Barracks Tweak")]
        public int CastleMilitiaBarracksAmountLevel1 { get; set; } = 1;

        [SettingPropertyInteger("Castle Militia Barracks Production Level 2", 2, 20, HintText = "Native value is 2. Changes the militia production that the castle militia barracks provides at level 2."), SettingPropertyGroup("Settlement Tweaks/Buildings Tweaks/Castle Buildings Tweaks/Castle Militia Barracks Tweak")]
        public int CastleMilitiaBarracksAmountLevel2 { get; set; } = 2;

        [SettingPropertyInteger("Castle Militia Barracks Production Level 3", 3, 30, HintText = "Native value is 3. Changes the militia production that the castle militia barracks provides at level 3."), SettingPropertyGroup("Settlement Tweaks/Buildings Tweaks/Castle Buildings Tweaks/Castle Militia Barracks Tweak")]
        public int CastleMilitiaBarracksAmountLevel3 { get; set; } = 3;

        #endregion

        #region Settlement Tweaks - Settlement Buildings Tweaks - Town Buildings Tweaks

        [SettingPropertyBool("Enable Town Training Fields Tweak", Order = 1, RequireRestart = false, IsToggle = true, HintText = "Changes the amount of experience the training fields provides for each level."), SettingPropertyGroup("Settlement Tweaks/Buildings Tweaks/Town Buildings Tweaks/Town Training Fields Tweak")]
        public bool TownTrainingFieldsBonusEnabled { get; set; } = true;

        [SettingPropertyInteger("Town Training Fields Level 1 Experience", 30, 300, HintText = "Native value is 30. Changes the amount of experience the training fields provides at level 1."), SettingPropertyGroup("Settlement Tweaks/Buildings Tweaks/Town Buildings Tweaks/Town Training Fields Tweak")]
        public int TownTrainingFieldsXpAmountLevel1 { get; set; } = 30;

        [SettingPropertyInteger("Town Training Fields Level 2 Experience", 60, 600, HintText = "Native value is 60. Changes the amount of experience the training fields provides at level 2."), SettingPropertyGroup("Settlement Tweaks/Buildings Tweaks/Town Buildings Tweaks/Town Training Fields Tweak")]
        public int TownTrainingFieldsXpAmountLevel2 { get; set; } = 60;

        [SettingPropertyInteger("Town Training Fields Level 3 Experience", 100, 1000, HintText = "Native value is 100. Changes the amount of experience the training fields provides at level 3."), SettingPropertyGroup("Settlement Tweaks/Buildings Tweaks/Town Buildings Tweaks/Town Training Fields Tweak")]
        public int TownTrainingFieldsXpAmountLevel3 { get; set; } = 100;


        [SettingPropertyBool("Enable Town Granary Tweak", Order = 1, RequireRestart = false, IsToggle = true, HintText = "Changes the amount of food storage the town granary provides per level."), SettingPropertyGroup("Settlement Tweaks/Buildings Tweaks/Town Buildings Tweaks/Town Granary Tweak")]
        public bool TownGranaryBonusEnabled { get; set; } = true;

        [SettingPropertyInteger("Town Granary Food Storage Level 1", 200, 2000, HintText = "Native value is 200. Changes the amount of food storage the town granary provides at level 1."), SettingPropertyGroup("Settlement Tweaks/Buildings Tweaks/Town Buildings Tweaks/Town Granary Tweak")]
        public int TownGranaryStorageAmountLevel1 { get; set; } = 200;

        [SettingPropertyInteger("Town Granary Food Storage Level 2", 400, 4000, HintText = "Native value is 400. Changes the amount of food storage the town granary provides at level 2."), SettingPropertyGroup("Settlement Tweaks/Buildings Tweaks/Town Buildings Tweaks/Town Granary Tweak")]
        public int TownGranaryStorageAmountLevel2 { get; set; } = 400;

        [SettingPropertyInteger("Town Granary Food Storage Level 3", 600, 6000, HintText = "Native value is 600. Changes the amount of food storage the town granary provides at level 3."), SettingPropertyGroup("Settlement Tweaks/Buildings Tweaks/Town Buildings Tweaks/Town Granary Tweak")]
        public int TownGranaryStorageAmountLevel3 { get; set; } = 600;


        [SettingPropertyBool("Enable Town Orchards Tweak", Order = 1, RequireRestart = false, IsToggle = true, HintText = "Changes the amount of food the town orchards produce per level."), SettingPropertyGroup("Settlement Tweaks/Buildings Tweaks/Town Buildings Tweaks/Town Orchards Tweak")]
        public bool TownOrchardsBonusEnabled { get; set; } = true;

        [SettingPropertyInteger("Town Orchard Food Production Level 1", 10, 100, HintText = "Native value is 10. Changes the amount of food the town orchards produce at level 1."), SettingPropertyGroup("Settlement Tweaks/Buildings Tweaks/Town Buildings Tweaks/Town Orchards Tweak")]
        public int TownOrchardsFoodProductionAmountLevel1 { get; set; } = 10;

        [SettingPropertyInteger("Town Orchard Food Production Level 2", 20, 200, HintText = "Native value is 20. Changes the amount of food the town orchards produce at level 2."), SettingPropertyGroup("Settlement Tweaks/Buildings Tweaks/Town Buildings Tweaks/Town Orchards Tweak")]
        public int TownOrchardsFoodProductionAmountLevel2 { get; set; } = 20;

        [SettingPropertyInteger("Town Orchard Food Production Level 3", 30, 300, HintText = "Native value is 30. Changes the amount of food the town orchards produce at level 3."), SettingPropertyGroup("Settlement Tweaks/Buildings Tweaks/Town Buildings Tweaks/Town Orchards Tweak")]
        public int TownOrchardsFoodProductionAmountLevel3 { get; set; } = 30;


        [SettingPropertyBool("Enable Town Militia Barracks Tweak", Order = 1, RequireRestart = false, IsToggle = true, HintText = "Changes the militia production that the town militia barracks provides per level."), SettingPropertyGroup("Settlement Tweaks/Buildings Tweaks/Town Buildings Tweaks/Town Militia Barracks Tweak")]
        public bool TownMilitiaBarracksBonusEnabled { get; set; } = true;

        [SettingPropertyFloatingInteger("Town Militia Barracks Production Level 1", .5f, 15, HintText = "Native value is .5. Changes the militia production that the town militia barracks provides at level 1."), SettingPropertyGroup("Settlement Tweaks/Buildings Tweaks/Town Buildings Tweaks/Town Militia Barracks Tweak")]
        public float TownMilitiaBarracksAmountLevel1 { get; set; } = 0.5f;

        [SettingPropertyFloatingInteger("Town Militia Barracks Production Level 2", 1f, 20f, HintText = "Native value is 1. Changes the militia production that the town militia barracks provides at level 2."), SettingPropertyGroup("Settlement Tweaks/Buildings Tweaks/Town Buildings Tweaks/Town Militia Barracks Tweak")]
        public float TownMilitiaBarracksAmountLevel2 { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Town Militia Barracks Production Level 3", 1.5f, 30f, HintText = "Native value is 1.5. Changes the militia production that the town militia barracks provides at level 3."), SettingPropertyGroup("Settlement Tweaks/Buildings Tweaks/Town Buildings Tweaks/Town Militia Barracks Tweak")]
        public float TownMilitiaBarracksAmountLevel3 { get; set; } = 1.5f;

        #endregion

        #region Settlement Tweaks - Settlement Food Bonus

        [SettingPropertyBool("Enabled Settlement Food Bonus Tweaks", Order = 1, RequireRestart = false, IsToggle = true, HintText = "Enables tweaks which provide bonuses to food production in towns and castles."), SettingPropertyGroup("Settlement Tweaks/Settlement Food Bonus")]
        public bool SettlementFoodBonusEnabled { get; set; } = true;

        [SettingPropertyFloatingInteger("Castle Food Bonus", 0f, 10f, HintText = "Native value is 0. Provides a bonus to food production in castles."), SettingPropertyGroup("Settlement Tweaks/Settlement Food Bonus")]
        public float CastleFoodBonus { get; set; } = 2f;

        [SettingPropertyFloatingInteger("Town Food Bonus", 0f, 10f, HintText = "Native value is 0. Provides a bonus to food production in towns."), SettingPropertyGroup("Settlement Tweaks/Settlement Food Bonus")]
        public float TownFoodBonus { get; set; } = 4f;

        [SettingPropertyBool("Enable Food Loss From Prosperity Tweak", Order = 1, RequireRestart = false, IsToggle = true, HintText = "Allows you to adjust the loss to food production received from settlement prosperity."), SettingPropertyGroup("Settlement Tweaks/Settlement Food Bonus/Food Loss from Prosperity Tweak")]
        public bool SettlementProsperityFoodMalusTweakEnabled { get; set; } = true;

        [SettingPropertyFloatingInteger("Prosperity Food Loss Divisor", 50f, 400f, HintText = "Native value is 50. The prosperity of the settlement is divided by this value to calculate the loss. Increasing this value will decrease the amount of food lost."), SettingPropertyGroup("Settlement Tweaks/Settlement Food Bonus/Food Loss from Prosperity Tweak")]
        public float SettlementProsperityFoodMalusDivisor { get; set; } = 100;

        #endregion

        #region Settlement Tweaks - Settlement Militia Bonus Tweaks

        [SettingPropertyBool("Enable Settlement Militia Bonus", Order = 1, RequireRestart = false, IsToggle = true, HintText = "Grants a bonus to militia growth in towns and castles."), SettingPropertyGroup("Settlement Tweaks/Settlement Militia Bonus")]
        public bool SettlementMilitiaBonusEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("Castle Militia Growth Bonus", 0f, 5f, HintText = "Native value is 0. Grants a bonus to militia growth in castles."), SettingPropertyGroup("Settlement Tweaks/Settlement Militia Bonus")]
        public float CastleMilitiaBonus { get; set; } = 1.25f;

        [SettingPropertyFloatingInteger("Town Militia Growth Bonus", 0f, 5f, HintText = "Native value is 0. Grants a bonus to militia growth in towns."), SettingPropertyGroup("Settlement Tweaks/Settlement Militia Bonus")]
        public float TownMilitiaBonus { get; set; } = 2.5f;

        [SettingPropertyBool("Enable Militia Elite Spawn Bonus", Order = 1, RequireRestart = false, IsToggle = true, HintText = "Adds a bonus to the chance that militia spawning in towns and castles are elite."), SettingPropertyGroup("Settlement Tweaks/Settlement Militia Bonus/Elite Militia Bonus")]
        public bool SettlementMilitiaEliteSpawnRateBonusEnabled { get; set; } = true;

        [SettingPropertyFloatingInteger("Elite Melee Militia Spawn Bonus", 0f, 1f, HintText = "Native value is 0. Adds a bonus to the change the militia spawning in towns and castles are elite melee troops."), SettingPropertyGroup("Settlement Tweaks/Settlement Militia Bonus/Elite Militia Bonus")]
        public float SettlementEliteMeleeSpawnRateBonus { get; set; } = 0.15f;

        [SettingPropertyFloatingInteger("Elite Ranged Militia Spawn Bonus", 0f, 1f, HintText = "Native value is 0. Adds a bonus to the change the militia spawning in towns and castles are elite ranged troops."), SettingPropertyGroup("Settlement Tweaks/Settlement Militia Bonus/Elite Militia Bonus")]
        public float SettlementEliteRangedSpawnRateBonus { get; set; } = 0.1f;

        #endregion

        #region Settlement Tweaks - Tournament Tweaks

        [SettingPropertyBool("Enable Tournament Renown Tweak", Order = 1, RequireRestart = false, IsToggle = true, HintText = "Sets the amount of renown awarded when you win a tournament."), SettingPropertyGroup("Settlement Tweaks/Tournament Tweaks/Renown Reward Tweak")]
        public bool TournamentRenownIncreaseEnabled { get; set; } = true;

        [SettingPropertyInteger("Tournament Renown Reward", 1, 50, HintText = "Native value is 3. Increases the amount of renown awarded when you win a tournament."), SettingPropertyGroup("Settlement Tweaks/Tournament Tweaks/Renown Reward Tweak")]
        public int TournamentRenownAmount { get; set; } = 3;

        [SettingPropertyBool("Enable Tournament Gold Reward Tweak", Order = 1, RequireRestart = false, IsToggle = true, HintText = "Adds the set amount of gold to the rewards when you win a tournament."), SettingPropertyGroup("Settlement Tweaks/Tournament Tweaks/Gold Reward Tweak")]
        public bool TournamentGoldRewardEnabled { get; set; } = true;

        [SettingPropertyInteger("Tournament Gold Reward", 0, 2000, HintText = "Native value is 0. Adds the set amount of gold to the rewards when you win a tournament."), SettingPropertyGroup("Settlement Tweaks/Tournament Tweaks/Gold Reward Tweak")]
        public int TournamentGoldRewardAmount { get; set; } = 500;

        [SettingPropertyBool("Enable Tournament Max Bet Tweak", Order = 1, RequireRestart = false, IsToggle = true, HintText = "Sets the maximum amount of gold that you can bet per round in tournaments."), SettingPropertyGroup("Settlement Tweaks/Tournament Tweaks/Maximum Bet Amount Tweak")]
        public bool TournamentMaxBetAmountTweakEnabled { get; set; } = true;

        [SettingPropertyInteger("Tournament Maximum Bet Amount", 0, 4000, HintText = "Native value is 150. Sets the maximum amount of gold that you can bet per round in tournaments."), SettingPropertyGroup("Settlement Tweaks/Tournament Tweaks/Maximum Bet Amount Tweak")]
        public int TournamentMaxBetAmount { get; set; } = 500;


        [SettingPropertyBool("Enable Tournament Hero Experience Multiplier Override", Order = 1, RequireRestart = false, IsToggle = true, HintText = "Overrides the native multiplier value for experience gain in tournaments for hero characters."), SettingPropertyGroup("Settlement Tweaks/Tournament Tweaks/Tournament Hero Experience Multiplier")]
        public bool TournamentHeroExperienceMultiplierEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("Tournament Hero Experience Multiplier", 0.25f, 2f, HintText = "Native value is 0.25. Sets the multiplier applied to experience gained in tournaments by hero characters. .25 = 1/4 normal experience, 1 = full real-world experience, 2 = 2x real-world experience."), SettingPropertyGroup("Settlement Tweaks/Tournament Tweaks/Tournament Hero Experience Multiplier")]
        public float TournamentHeroExperienceMultiplier { get; set; } = 0.25f;

        [SettingPropertyBool("Enable Arena Hero Experience Multiplier Override", Order = 1, RequireRestart = false, IsToggle = true, HintText = "Overrides the native multiplier value for experience gain in arena fights for hero characters. .25 = 1/4 normal experience, 1 = full real-world experience, 2 = 2x real-world experience."), SettingPropertyGroup("Settlement Tweaks/Tournament Tweaks/Arena Hero Experience Multiplier")]
        public bool ArenaHeroExperienceMultiplierEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("Arena Hero Experience Multiplier", 0.06f, 2f, HintText = "Native value is 0.06. Overrides the native multiplier for experience gain in arena fights for hero characters."), SettingPropertyGroup("Settlement Tweaks/Tournament Tweaks/Arena Hero Experience Multiplier")]
        public float ArenaHeroExperienceMultiplier { get; set; } = 0.06f;


        [SettingPropertyBool("Enabled Minimum Betting Odds Tweak", Order = 1, RequireRestart = false, IsToggle = true, HintText = "Allows you to set the minimum betting odds in tournaments."), SettingPropertyGroup("Settlement Tweaks/Tournament Tweaks/Minimum Betting Odds")]
        public bool MinimumBettingOddsTweakEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("Minimum Betting Odds", 1.1f, 10f, HintText = "Native: 1.1. The minimum odds for tournament bets."), SettingPropertyGroup("Settlement Tweaks/Tournament Tweaks/Minimum Betting Odds")]
        public float MinimumBettingOdds { get; set; } = 1.1f;

        #endregion

        #region Settlement Tweaks - Workshop Tweaks

        [SettingPropertyBool("Enable Max Workshop Limit Tweak", Order = 1, RequireRestart = false, IsToggle = true, HintText = "Sets the base maximum number of workshops you can have and the limit increase gained per clan tier."), SettingPropertyGroup("Settlement Tweaks/Workshop Tweaks/Workshop Limit Tweak")]
        public bool MaxWorkshopCountTweakEnabled { get; set; } = true;

        [SettingPropertyInteger("Base Workshop Limit", 0, 20, HintText = "Native value is 1. Sets the base maximum number of workshops you can have."), SettingPropertyGroup("Settlement Tweaks/Workshop Tweaks/Workshop Limit Tweak")]
        public int BaseWorkshopCount { get; set; } = 2;

        [SettingPropertyInteger("Bonus Workshops Per Clan Tier", 0, 5, HintText = "Sets the base maximum number of workshops you can have and the limit increase gained per clan tier."), SettingPropertyGroup("Settlement Tweaks/Workshop Tweaks/Workshop Limit Tweak")]
        public int BonusWorkshopsPerClanTier { get; set; } = 1;

        [SettingPropertyBool("Enable Workshop Cost Tweak", Order = 1, RequireRestart = false, IsToggle = true, HintText = "Sets the base value used to calculate the cost of workshops. Reduce to reduce cost of workshops."), SettingPropertyGroup("Settlement Tweaks/Workshop Tweaks/Workshop Cost Tweak")]
        public bool WorkshopBuyingCostTweakEnabled { get; set; } = false;

        [SettingPropertyInteger("Workshop Base Cost", 0, 15000, HintText = "Native value is 10,000. Sets the base value used to calculate the cost of workshops. Reduce to reduce cost of workshops."), SettingPropertyGroup("Settlement Tweaks/Workshop Tweaks/Workshop Cost Tweak")]
        public int WorkshopBaseCost { get; set; } = 10000;

        #endregion

    }
}
