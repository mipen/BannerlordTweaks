using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.CampaignSystem.ViewModelCollection.Craft.Smelting;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

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
            // IEnumerable<EquipmentElement> locks = (IEnumerable<EquipmentElement>)Campaign.Current.GetCampaignBehavior<TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors.IInventoryLockTracker>().GetLocks();
            
            // Updated line 24 to Line 25 which seems to be the updated way game works out item locks in v1.4.3 InitializeInventory()
            // EquipmentElement[] locked_items = locks?.ToArray<EquipmentElement>();

            // Update for 1.5.4
            IInventoryLockTracker campaignBehavior = Campaign.Current.GetCampaignBehavior<IInventoryLockTracker>();
            List<string> locks = campaignBehavior.GetLocks().ToList<string>();


            //EquipmentElement[] locked_items = (locks != null) ? locks.ToArray<EquipmentElement>() : null;

            /*
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
            */

            bool isLocked(ItemRosterElement item)
            {
                string text = item.EquipmentElement.Item.StringId;
                if (item.EquipmentElement.ItemModifier != null)
                {
                    text += item.EquipmentElement.ItemModifier.StringId;
                }
                return locks.Contains(text);
            }

            MBBindingList<SmeltingItemVM> filteredList = new MBBindingList<SmeltingItemVM>();

            foreach (SmeltingItemVM sItem in __instance.SmeltableItemList)
            {
                if (!____playerItemRoster.Any(rItem =>
                    // SmeltinItemVM ItemObject (item) was removed in 1.5.3 beta
                    // sItem.Item == rItem.EquipmentElement.Item && isLocked(rItem.EquipmentElement)
                    // sItem.EquipmentElement.Equals(rItem.EquipmentElement) && isLocked(rItem.EquipmentElement)
                    sItem.EquipmentElement.Item == rItem.EquipmentElement.Item && isLocked(rItem)
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
            return BannerlordTweaksSettings.Instance.PreventSmeltingLockedItems;
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
                // SmeltinItemVM ItemObject (item) was removed in 1.5.3 beta
                // int count = SmeltingHelper.GetNewPartsFromSmelting(item.Item).Count();
                int count = item.NumOfItems;
                if (count > 0)
                {
                    string parts = count == 1 ? "part" : "parts";
                    item.Name = $"{item.Name} ({count} new {parts})";
                }
            }
        }

        static bool Prepare()
        {
            return BannerlordTweaksSettings.Instance.AutoLearnSmeltedParts;
        }
    }
}
