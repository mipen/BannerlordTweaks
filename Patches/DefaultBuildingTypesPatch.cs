using System;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Localization;

namespace BannerlordTweaks.Patches
{
    [HarmonyPatch(typeof(DefaultBuildingTypes), "InitializeAll")]
    public class DefaultBuildingTypesPatch
    {
        static void Postfix(BuildingType ____buildingCastleTrainingFields, BuildingType ____buildingCastleGranary, BuildingType ____buildingCastleGardens,
            BuildingType ____buildingCastleMilitiaBarracks, BuildingType ____buildingSettlementTrainingFields, BuildingType ____buildingSettlementGranary,
            BuildingType ____buildingSettlementOrchard, BuildingType ____buildingSettlementMilitiaBarracks)
        {
            //Castle
            #region Training Fields
            if (Settings.Instance.CastleTrainingFieldsBonusEnabled)
            {
                ____buildingCastleTrainingFields?.Initialize(new TextObject("{=BkTiRPT4}Training Fields"),
                    new TextObject("{=otWlERkc}A field for military drills that increase the daily experience gain of all garrisoned units."),
                    new int[3] { 39, 52, 65 }, BuildingLocation.Castle,
                    new Tuple<BuildingEffectEnum, int, int, int>[]
                    {
                        new Tuple<BuildingEffectEnum, int, int, int>(
                            BuildingEffectEnum.Experience,
                            Settings.Instance.CastleTrainingFieldsXpAmountLevel1,
                            Settings.Instance.CastleTrainingFieldsXpAmountLevel2,
                            Settings.Instance.CastleTrainingFieldsXpAmountLevel3
                        )
                    });
            }
            #endregion
            #region Granary
            if (Settings.Instance.CastleGranaryBonusEnabled)
            {
                ____buildingCastleGranary?.Initialize(new TextObject("{=PstO2f5I}Granary"),
                    new TextObject("{=iazij7fO}Keeps stockpiles of food so that the settlement has more food supply. Increases the local food supply."),
                    new int[3] { 39, 65, 91 }, BuildingLocation.Castle,
                    new Tuple<BuildingEffectEnum, int, int, int>[]
                    {
                        new Tuple<BuildingEffectEnum, int, int, int>(
                            BuildingEffectEnum.Foodstock,
                            Settings.Instance.CastleGranaryStorageAmountLevel1,
                            Settings.Instance.CastleGranaryStorageAmountLevel2,
                            Settings.Instance.CastleGranaryStorageAmountLevel3
                        )
                    });
            }
            #endregion
            #region Gardens
            if (Settings.Instance.CastleGardensBonusEnabled)
            {
                ____buildingCastleGardens?.Initialize(new TextObject("{=yT6XN4Mr}Gardens"),
                    new TextObject("{=ZCLVOXgM}Castles contained fruit trees, bakeries, chicken coups to be used in emergencies. While it is not enough for a full contingency of troops any small amount of fresh foods are a big help while in the sieges."),
                    new int[3] { 25, 39, 52 }, BuildingLocation.Castle,
                    new Tuple<BuildingEffectEnum, int, int, int>[] {
                        new Tuple<BuildingEffectEnum, int, int, int>(
                            BuildingEffectEnum.FoodProduction,
                            Settings.Instance.CastleGardensFoodProductionAmountLevel1,
                            Settings.Instance.CastleGardensFoodProductionAmountLevel2,
                            Settings.Instance.CastleGardensFoodProductionAmountLevel3
                        )
                    });
            }
            #endregion
            #region Militia Barracks
            if (Settings.Instance.CastleMilitiaBarracksBonusEnabled)
            {
                ____buildingCastleMilitiaBarracks?.Initialize(new TextObject("{=l91xAgmU}Militia Barracks"),
                    new TextObject("{=YRrx8bAK}Provides battle training for citizens and recruit them into militia, each level increases daily militia recruitment."),
                    new int[3] { 46, 59, 72 }, BuildingLocation.Castle,
                    new Tuple<BuildingEffectEnum, int, int, int>[] {
                        new Tuple<BuildingEffectEnum, int, int, int>(
                            BuildingEffectEnum.Militia,
                            Settings.Instance.CastleMilitiaBarracksAmountLevel1,
                            Settings.Instance.CastleMilitiaBarracksAmountLevel2,
                            Settings.Instance.CastleMilitiaBarracksAmountLevel3
                        )
                    });
            }
            #endregion

            //Town
            #region Training Fields
            if (Settings.Instance.TownTrainingFieldsBonusEnabled)
            {
                ____buildingSettlementTrainingFields?.Initialize(new TextObject("{=BkTiRPT4}Training Fields"),
                    new TextObject("{=otWlERkc}A field for military drills that increase the daily experience gain of all garrisoned units."),
                    new int[3] { 600, 800, 1000 }, BuildingLocation.Settlement,
                    new Tuple<BuildingEffectEnum, int, int, int>[]
                    {
                        new Tuple<BuildingEffectEnum, int, int, int>(
                        BuildingEffectEnum.Experience,
                        Settings.Instance.TownTrainingFieldsXpAmountLevel1,
                        Settings.Instance.TownTrainingFieldsXpAmountLevel2,
                        Settings.Instance.TownTrainingFieldsXpAmountLevel3)
                    });
            }
            #endregion
            #region Granary
            if (Settings.Instance.TownGranaryBonusEnabled)
            {
                ____buildingCastleGranary?.Initialize(new TextObject("{=PstO2f5I}Granary"),
                    new TextObject("{=aK23T43P}Keeps stockpiles of food so that the settlement has more food supply. Each level increases the local food supply."),
                    new int[3] { 500, 700, 1000 }, BuildingLocation.Settlement,
                    new Tuple<BuildingEffectEnum, int, int, int>[]
                    {
                        new Tuple<BuildingEffectEnum,int,int,int>(
                            BuildingEffectEnum.Foodstock,
                            Settings.Instance.TownGranaryStorageAmountLevel1,
                            Settings.Instance.TownGranaryStorageAmountLevel2,
                            Settings.Instance.TownGranaryStorageAmountLevel3)
                    });
            }
            #endregion
            #region Orchards
            if (Settings.Instance.TownOrchardsBonusEnabled)
            {
                ____buildingSettlementOrchard?.Initialize(new TextObject("{=AkbiPIij}Orchards"),
                    new TextObject("{=ZCLVOXgM}Fruit trees and vegetable gardens outside the walls provide food as long as there is no siege."),
                    new int[3] { 800, 1300, 1800 }, BuildingLocation.Settlement,
                    new Tuple<BuildingEffectEnum, int, int, int>[]
                    {
                    new Tuple<BuildingEffectEnum,int,int,int>(
                        BuildingEffectEnum.FoodProduction,
                        Settings.Instance.TownOrchardsFoodProductionAmountLevel1,
                        Settings.Instance.TownOrchardsFoodProductionAmountLevel2,
                        Settings.Instance.TownOrchardsFoodProductionAmountLevel3)
                    });
            }
            #endregion
            #region Militia Barracks
            if (Settings.Instance.TownMilitiaBarracksBonusEnabled)
            {
                ____buildingSettlementMilitiaBarracks?.Initialize(new TextObject("{=l91xAgmU}Militia Barracks"),
                    new TextObject("{=RliyRJKl}Provides battle training for citizens and recruit them into militia. Increases daily militia recruitment."),
                    new int[3] { 600, 800, 1500 }, BuildingLocation.Settlement,
                    new Tuple<BuildingEffectEnum, int, int, int>[]
                    {
                        new Tuple<BuildingEffectEnum, int, int, int>(
                            BuildingEffectEnum.Militia,
                            Settings.Instance.TownMilitiaBarracksAmountLevel1,
                            Settings.Instance.TownMilitiaBarracksAmountLevel2,
                            Settings.Instance.TownMilitiaBarracksAmountLevel3)
                    });
            }
            #endregion
        }

        static bool Prepare()
        {
            return Settings.Instance.CastleGranaryBonusEnabled || Settings.Instance.CastleGardensBonusEnabled ||
                Settings.Instance.CastleTrainingFieldsBonusEnabled || Settings.Instance.CastleMilitiaBarracksBonusEnabled ||
                Settings.Instance.TownGranaryBonusEnabled || Settings.Instance.TownOrchardsBonusEnabled ||
                Settings.Instance.TownTrainingFieldsBonusEnabled || Settings.Instance.TownMilitiaBarracksBonusEnabled;
        }
    }
}
