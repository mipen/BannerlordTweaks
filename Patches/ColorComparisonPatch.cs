using HarmonyLib;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Library;

namespace BannerlordTweaks.Patches
{
    [HarmonyPatch(typeof(ItemMenuVM), "GetColorFromComparison")]
    public class ColorComparisonPatch
    {
        //Replace the entire method
        static bool Prefix(ItemMenuVM __instance, int result, bool isCompared, ref Color __result)
        {
            //if (MobileParty.MainParty != null && (MobileParty.MainParty.HasPerk(DefaultPerks.Trade.WholeSeller) || MobileParty.MainParty.HasPerk(DefaultPerks.Trade.Appraiser)))
            //{
            //	return Colors.Black;
            //}

            // Code above is from original method, the intention was probably to turn on red/green colours on the purchase price of any goods in your inventory as the Perk says "Profits are marked"
            // But this function is also used for equipment stat comparisons, so this is the wrong place to enable those perks.
            if (result != -1)
            {
                if (result != 1)
                {
                    __result = Colors.Black;
                    return false;
                }
                if (!isCompared)
                {
                    __result = UIColors.PositiveIndicator;
                    return false;
                }
                __result = UIColors.NegativeIndicator;
                return false;
            }
            else
            {
                if (!isCompared)
                {
                    __result = UIColors.NegativeIndicator;
                    return false;
                }
                __result = UIColors.PositiveIndicator;
                return false;
            }
        }
    }
}
