using HarmonyLib;
using System;
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
            if (BannerlordTweaksSettings.Instance is null) { return; }
            //Castle
            #region Training Fields
            if (BannerlordTweaksSettings.Instance.CastleTrainingFieldsBonusEnabled)
            {
                ____buildingCastleTrainingFields?.Initialize(new TextObject("{=BkTiRPT4}Training Fields"),
                    new TextObject("{=otWlERkc}A field for military drills that increase the daily experience gain of all garrisoned units."),
                    new int[3] { 500, 1000, 1500 }, BuildingLocation.Castle,
                    new Tuple<BuildingEffectEnum, float, float, float>[]
                    {
                        new Tuple<BuildingEffectEnum, float, float, float>(
                            BuildingEffectEnum.Experience,
                            BannerlordTweaksSettings.Instance.CastleTrainingFieldsXpAmountLevel1,
                            BannerlordTweaksSettings.Instance.CastleTrainingFieldsXpAmountLevel2,
                            BannerlordTweaksSettings.Instance.CastleTrainingFieldsXpAmountLevel3
                        )
                    });
            }
            #endregion
            #region Granary
            if (BannerlordTweaksSettings.Instance.CastleGranaryBonusEnabled)
            {
                ____buildingCastleGranary?.Initialize(new TextObject("{=PstO2f5I}Granary"),
                    new TextObject("{=iazij7fO}Keeps stockpiles of food so that the settlement has more food supply. Each level increases the local food supply."),
                    new int[3] { 1000, 1500, 2000 }, BuildingLocation.Castle,
                    new Tuple<BuildingEffectEnum, float, float, float>[]
                    {
                        new Tuple<BuildingEffectEnum, float, float, float>(
                            BuildingEffectEnum.Foodstock,
                            BannerlordTweaksSettings.Instance.CastleGranaryStorageAmountLevel1,
                            BannerlordTweaksSettings.Instance.CastleGranaryStorageAmountLevel2,
                            BannerlordTweaksSettings.Instance.CastleGranaryStorageAmountLevel3
                        )
                    });
            }
            #endregion
            #region Gardens
            if (BannerlordTweaksSettings.Instance.CastleGardensBonusEnabled)
            {
                ____buildingCastleGardens?.Initialize(new TextObject("{=yT6XN4Mr}Gardens"),
                    new TextObject("{=ZCLVOXgM}Fruit trees and vegetable gardens outside the walls provide food as long as there is no siege.", null),
                    new int[] { 500, 750, 1000 }, BuildingLocation.Castle,
                    new Tuple<BuildingEffectEnum, float, float, float>[] 
                    {
                        new Tuple<BuildingEffectEnum, float, float, float>(
                            BuildingEffectEnum.FoodProduction,
                            BannerlordTweaksSettings.Instance.CastleGardensFoodProductionAmountLevel1,
                            BannerlordTweaksSettings.Instance.CastleGardensFoodProductionAmountLevel2,
                            BannerlordTweaksSettings.Instance.CastleGardensFoodProductionAmountLevel3
                        )
                    });
            }
            #endregion
            #region Militia Barracks
            if (BannerlordTweaksSettings.Instance.CastleMilitiaBarracksBonusEnabled)
            {
                ____buildingCastleMilitiaBarracks?.Initialize(new TextObject("{=l91xAgmU}Militia Grounds"),
                    new TextObject("{=YRrx8bAK}Provides battle training for citizens and recruit them into militia, each level increases daily militia recruitment."),
                    new int[3] { 500, 750, 1000 }, BuildingLocation.Castle,
                    new Tuple<BuildingEffectEnum, float, float, float>[] 
                    {
                        new Tuple<BuildingEffectEnum, float, float, float>(
                            BuildingEffectEnum.Militia,
                            BannerlordTweaksSettings.Instance.CastleMilitiaBarracksAmountLevel1,
                            BannerlordTweaksSettings.Instance.CastleMilitiaBarracksAmountLevel2,
                            BannerlordTweaksSettings.Instance.CastleMilitiaBarracksAmountLevel3
                        )
                    });
            }
            #endregion

            //Town
            #region Training Fields
            if (BannerlordTweaksSettings.Instance.TownTrainingFieldsBonusEnabled)
            {
                ____buildingSettlementTrainingFields?.Initialize(new TextObject("{=BkTiRPT4}Training Fields"),
                    new TextObject("{=otWlERkc}A field for military drills that increase the daily experience gain of all garrisoned units."),
                    new int[3] { 2000, 3000, 4000 }, BuildingLocation.Settlement,
                    new Tuple<BuildingEffectEnum, float, float, float>[]
                    {
                        new Tuple<BuildingEffectEnum, float, float, float>(
                        BuildingEffectEnum.Experience,
                        BannerlordTweaksSettings.Instance.TownTrainingFieldsXpAmountLevel1,
                        BannerlordTweaksSettings.Instance.TownTrainingFieldsXpAmountLevel2,
                        BannerlordTweaksSettings.Instance.TownTrainingFieldsXpAmountLevel3)
                    });
            }
            #endregion
            #region Granary
            if (BannerlordTweaksSettings.Instance.TownGranaryBonusEnabled)
            {
                ____buildingSettlementGranary?.Initialize(new TextObject("{=PstO2f5I}Granary"),
                    new TextObject("{=aK23T43P}Keeps stockpiles of food so that the settlement has more food supply. Each level increases the local food supply."),
                    new int[3] { 1000, 1500, 2000 }, BuildingLocation.Settlement,
                    new Tuple<BuildingEffectEnum, float, float, float>[]
                    {
                        new Tuple<BuildingEffectEnum,float,float,float>(
                            BuildingEffectEnum.Foodstock,
                            BannerlordTweaksSettings.Instance.TownGranaryStorageAmountLevel1,
                            BannerlordTweaksSettings.Instance.TownGranaryStorageAmountLevel2,
                            BannerlordTweaksSettings.Instance.TownGranaryStorageAmountLevel3)
                    });
            }
            #endregion
            #region Orchards
            if (BannerlordTweaksSettings.Instance.TownOrchardsBonusEnabled)
            {
                ____buildingSettlementOrchard?.Initialize(new TextObject("{=AkbiPIij}Orchards"),
                    new TextObject("{=ZCLVOXgM}Fruit trees and vegetable gardens outside the walls provide food as long as there is no siege."),
                    new int[3] { 2000, 3000, 4000 }, BuildingLocation.Settlement,
                    new Tuple<BuildingEffectEnum, float, float, float>[]
                    {
                    new Tuple<BuildingEffectEnum,float,float,float>(
                        BuildingEffectEnum.FoodProduction,
                        BannerlordTweaksSettings.Instance.TownOrchardsFoodProductionAmountLevel1,
                        BannerlordTweaksSettings.Instance.TownOrchardsFoodProductionAmountLevel2,
                        BannerlordTweaksSettings.Instance.TownOrchardsFoodProductionAmountLevel3)
                    });
            }
            #endregion
            #region Militia Barracks
            if (BannerlordTweaksSettings.Instance.TownMilitiaBarracksBonusEnabled)
            {
                ____buildingSettlementMilitiaBarracks?.Initialize(new TextObject("{=l91xAgmU}Militia Grounds"),
                    new TextObject("{=RliyRJKl}Provides battle training for citizens and recruit them into militia. Increases daily militia recruitment."),
                    new int[3] { 2000, 3000, 4000 }, BuildingLocation.Settlement,
                    new Tuple<BuildingEffectEnum, float, float, float>[]
                    {
                        new Tuple<BuildingEffectEnum, float, float, float>(
                            BuildingEffectEnum.Militia,
                            BannerlordTweaksSettings.Instance.TownMilitiaBarracksAmountLevel1,
                            BannerlordTweaksSettings.Instance.TownMilitiaBarracksAmountLevel2,
                            BannerlordTweaksSettings.Instance.TownMilitiaBarracksAmountLevel3)
                    });
            }
            #endregion
        }

        static bool Prepare()
        {
            if (BannerlordTweaksSettings.Instance == null) { return false; }
            return BannerlordTweaksSettings.Instance.CastleGranaryBonusEnabled || BannerlordTweaksSettings.Instance.CastleGardensBonusEnabled ||
                BannerlordTweaksSettings.Instance.CastleTrainingFieldsBonusEnabled || BannerlordTweaksSettings.Instance.CastleMilitiaBarracksBonusEnabled ||
                BannerlordTweaksSettings.Instance.TownGranaryBonusEnabled || BannerlordTweaksSettings.Instance.TownOrchardsBonusEnabled ||
                BannerlordTweaksSettings.Instance.TownTrainingFieldsBonusEnabled || BannerlordTweaksSettings.Instance.TownMilitiaBarracksBonusEnabled;
        }
    }
}
