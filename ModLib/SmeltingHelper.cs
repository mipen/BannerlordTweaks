using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;
using TaleWorlds.Core;

namespace ModLib
{
    public static class SmeltingHelper
    {
        public static IEnumerable<CraftingPiece> GetNewPartsFromSmelting(ItemObject item)
        {
            return item.WeaponDesign.UsedPieces.Select(
                    x => x.CraftingPiece
                ).Where(
                    x => x != null &&
                    x.IsValid &&
                    !Campaign.Current.GetCampaignBehavior<CraftingCampaignBehavior>().IsOpened(x)
                );
        }
    }
}
