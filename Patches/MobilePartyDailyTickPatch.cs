using HarmonyLib;
using TaleWorlds.CampaignSystem;

namespace BannerlordTweaks.Patches
{
    [HarmonyPatch(typeof(MobileParty), "DailyTick")]
    public class MobilePartyDailyTickPatch
    {
        static bool Prefix(MobileParty __instance)
        {
            if (__instance.IsActive && __instance.HasPerk(DefaultPerks.Leadership.CombatTips))
            {
                for(int i = 0; i < __instance.MemberRoster.Count; i++)
                {
                    TroopRosterElement troopElement = __instance.MemberRoster.GetElementCopyAtIndex(i);
                    int singleTroopPerksXp = Campaign.Current.Models.PartyTrainingModel.GetTroopPerksXp(DefaultPerks.Leadership.CombatTips);
                    //We remove one troop as this will be applied once later on running of actual DailyTick.
                    int totalTroopXp = singleTroopPerksXp * (troopElement.Number - 1);
                    __instance.Party.MemberRoster.AddXpToTroopAtIndex(totalTroopXp, i);

                }
            }
            else if (__instance.IsActive && __instance.HasPerk(DefaultPerks.Leadership.RaiseTheMeek))
            {
                for (int i = 0; i < __instance.MemberRoster.Count; i++)
                {
                    TroopRosterElement troopElement2 = __instance.MemberRoster.GetElementCopyAtIndex(i);
                    if (troopElement2.Character.Tier < 4)
                    {
                        int singleTroopPerksXp2 = Campaign.Current.Models.PartyTrainingModel.GetTroopPerksXp(DefaultPerks.Leadership.RaiseTheMeek);
                        //We remove one troop as this will be applied once later on running of actual DailyTick.
                        int totalTroopXp2 = singleTroopPerksXp2 * (troopElement2.Number - 1);
                        __instance.Party.MemberRoster.AddXpToTroopAtIndex(totalTroopXp2, i);
                    }

                }
            }
            return true;
        }

        static bool Prepare()
        {
            return Settings.Instance.TroopPerkXpMultipliedByStackEnabled;
        }
    }
}
