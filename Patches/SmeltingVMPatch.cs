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
     * Prevent locked items from showing up in smelting list to stop accidental smelting
     */
    [HarmonyPatch(typeof(SmeltingVM), "RefreshList")]
    public class RefreshListPatch
    {
        private static void Postfix(SmeltingVM __instance, ItemRoster ____playerItemRoster)
        {
            // This appears to be how the game works out if an item is locked
            // From TaleWorlds.CampaignSystem.ViewModelCollection.SPInventoryVM.InitializeInventory()
            IEnumerable<ItemRosterElement> locks = Campaign.Current.GetCampaignBehavior<TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors.IInventoryLockTracker>().GetLocks();
            ItemRosterElement[] locked_items = (locks != null) ? locks.ToArray<ItemRosterElement>() : null;

            bool isLocked(ItemRosterElement test_item)
            {
                return locked_items != null && locked_items.Count(delegate (ItemRosterElement x)
                {
                    ItemObject lock_item = x.EquipmentElement.Item;
                    if (lock_item.StringId == test_item.EquipmentElement.Item.StringId)
                    {
                        ItemModifier itemModifier = x.EquipmentElement.ItemModifier;
                        string a = (itemModifier != null) ? itemModifier.StringId : null;
                        ItemModifier itemModifier2 = test_item.EquipmentElement.ItemModifier;
                        return a == ((itemModifier2 != null) ? itemModifier2.StringId : null);
                    }
                    return false;
                }) > 0;
            }
            MBBindingList<SmeltingItemVM> filteredList = new MBBindingList<SmeltingItemVM>();

            foreach (SmeltingItemVM sItem in __instance.SmeltableItemList)
            {
                if (!____playerItemRoster.Any(rItem =>
                    sItem.Item == rItem.EquipmentElement.Item && isLocked(rItem)
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
    public class RefreshList_RenamePatch
    {
        private static void Postfix(SmeltingVM __instance, ItemRoster ____playerItemRoster)
        {
            foreach(SmeltingItemVM item in __instance.SmeltableItemList)
            {
                int count = SmeltingHelper.GetNewPartsFromSmelting(item.Item).Count();
                if(count > 0)
                {
                    string parts = count == 1 ? "part" : "parts";
                    item.Name = item.Name + $" ({count} new {parts})";
                }
            }
        }

        static bool Prepare()
        {
            return Settings.Instance.AutoLearnSmeltedParts;
        }
    }
}
