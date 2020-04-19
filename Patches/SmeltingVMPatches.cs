using HarmonyLib;
using ModLib;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ViewModelCollection.Craft.Smelting;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace BannerlordTweaks.Patches
{
    /*
     * Prevent locked items from showing up in the smelting list to stop accidental smelting
     */
    [HarmonyPatch(typeof(SmeltingVM), "RefreshList")]
    public class RefreshListPatch
    {
        private static void Postfix(SmeltingVM __instance, ItemRoster ____playerItemRoster)
        {
            // This appears to be how the game works out if an item is locked
            // From TaleWorlds.CampaignSystem.ViewModelCollection.SPInventoryVM.InitializeInventory()
            IEnumerable<EquipmentElement> locks = Campaign.Current.GetCampaignBehavior<TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors.IInventoryLockTracker>().GetLocks();
            EquipmentElement[] locked_items = locks?.ToArray<EquipmentElement>();

            bool isLocked(EquipmentElement test_item)
            {
                return locked_items != null && locked_items.Any(delegate (EquipmentElement x)
                {
                    ItemObject lock_item = x.Item;
                    if (lock_item.StringId == test_item.Item.StringId)
                    {
                        ItemModifier itemModifier = x.ItemModifier;
                        string a = itemModifier?.StringId;
                        ItemModifier itemModifier2 = test_item.ItemModifier;
                        return a == (itemModifier2?.StringId);
                    }
                    return false;
                });
            }
            MBBindingList<SmeltingItemVM> filteredList = new MBBindingList<SmeltingItemVM>();

            foreach (SmeltingItemVM sItem in __instance.SmeltableItemList)
            {
                if (!____playerItemRoster.Any(rItem =>
                    sItem.Item == rItem.EquipmentElement.Item && isLocked(rItem.EquipmentElement)
                ))
                {
                    filteredList.Add(sItem);
                }
            }

            __instance.SmeltableItemList = filteredList;

            if (__instance.SmeltableItemList.Count == 0)
            {
                __instance.CurrentSelectedItem = null;
            }
        }

        static bool Prepare()
        {
            return Settings.Instance.PreventSmeltingLockedItems;
        }
    }

    [HarmonyPatch(typeof(SmeltingVM), "RefreshList")]
    [HarmonyPriority(Priority.VeryLow)]
    public class RefreshListRenamePatch
    {
        private static void Postfix(SmeltingVM __instance, ItemRoster ____playerItemRoster)
        {
            foreach (SmeltingItemVM item in __instance.SmeltableItemList)
            {
                int count = SmeltingHelper.GetNewPartsFromSmelting(item.Item).Count();
                if (count > 0)
                {
                    string parts = count == 1 ? "part" : "parts";
                    item.Name = $"{item.Name} ({count} new {parts})";
                }
            }
        }

        static bool Prepare()
        {
            return Settings.Instance.AutoLearnSmeltedParts;
        }
    }
}
