using System;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Localization;

namespace BannerlordTweaks.Patches
{
    [HarmonyPatch(typeof(DefaultBuildingTypes), "InitializeAll")]
    public class DefaultBuildingTypesPatch
    {
        static void Postfix(BuildingType ____buildingCastleTrainingFields, BuildingType ____buildingCastleGranary, BuildingType ____buildingCastleGardens, BuildingType ____buildingCastleMilitiaBarracks)
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
                    new int[3] {39, 65, 91}, BuildingLocation.Castle,
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
                    new int[3] { 25, 39, 52}, BuildingLocation.Castle, 
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

            //Towns

        }

        static bool Prepare()
        {
            return Settings.Instance.CastleGranaryBonusEnabled || Settings.Instance.CastleGardensBonusEnabled ||
                Settings.Instance.CastleTrainingFieldsBonusEnabled || Settings.Instance.CastleMilitiaBarracksBonusEnabled;
        }
    }
}
