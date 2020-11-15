using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;
using TaleWorlds.Core;

namespace BannerlordTweaks {
    public static class SmeltingHelper {
        public static IEnumerable<CraftingPiece> GetNewPartsFromSmelting(ItemObject item) {
            if (item == null) {
                //throw new ArgumentNullException(nameof(item));
                DebugHelpers.ColorRedMessage("Error in Bannerlord Tweaks SmeltingHelper. Did not find" + item.Name);
            }

            return item.WeaponDesign.UsedPieces.Select(x => x.CraftingPiece).Where(x => x != null && x.IsValid && !Campaign.Current.GetCampaignBehavior<CraftingCampaignBehavior>().IsOpened(x));
        }
    }
}
