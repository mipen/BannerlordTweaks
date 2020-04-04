using HarmonyLib;
using Helpers;
using System.Collections.Generic;
using System.Windows.Forms;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.Library;

namespace BannerlordTweaks.Patches
{
    //[HarmonyPatch(typeof(ChangeKingdomAction), "ApplyInternal")]
    //public class ChangeKingdomActionPatch
    //{
        //static bool Prefix(Clan clan, Kingdom kingdom, int detail, int awardMultiplier, bool byRebellion, bool showNotification)
        //{
        //    //MessageBox.Show($"Detail: {detail}");
        //    if (kingdom != null)
        //    {
        //        foreach (Kingdom k in Kingdom.All)
        //        {
        //            if (k == kingdom || !kingdom.IsAtWarWith(k))
        //            {
        //                FactionHelper.FinishAllRelatedHostileActionsOfFactionToFaction(clan, k);
        //                FactionHelper.FinishAllRelatedHostileActionsOfFactionToFaction(k, clan);
        //            }
        //        }
        //        foreach (Clan c in Clan.All)
        //        {
        //            if (c != clan && c.Kingdom == null && !kingdom.IsAtWarWith(c))
        //            {
        //                FactionHelper.FinishAllRelatedHostileActions(clan, c);
        //            }
        //        }
        //    }

        //    var onClanChangedKingdom = typeof(CampaignEventDispatcher).GetMethod("OnClanChangedKingdom", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        //    var onMercenaryClanChangedKingdom = typeof(CampaignEventDispatcher).GetMethod("OnMercenaryClanChangedKingdom", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        //    switch (detail)
        //    {
        //        //ChangeKingdomActionDetail.JoinKingdom
        //        case 1:
        //            {
        //                StatisticsDataLogHelper.AddLog(StatisticsDataLogHelper.LogAction.ChangeKingdomAction, clan, kingdom, kingdom, false);
        //                clan.IsUnderMercenaryService = false;
        //                if (kingdom != null)
        //                {
        //                    clan.ClanLeaveKingdom(!byRebellion);
        //                }
        //                clan.ClanJoinFaction(kingdom);
        //                onClanChangedKingdom.Invoke(CampaignEventDispatcher.Instance, new object[] { clan, kingdom, clan.Kingdom, byRebellion, showNotification });
        //                break;
        //            }
        //        //ChangeKingdomActionDetail.JoinAsMercenary
        //        case 0:
        //            {
        //                StatisticsDataLogHelper.AddLog(StatisticsDataLogHelper.LogAction.ChangeKingdomAction, clan, kingdom, kingdom, false);
        //                if (clan.IsUnderMercenaryService)
        //                {
        //                    clan.ClanLeaveKingdom();
        //                }
        //                clan.MercenaryAwardMultiplier = MathF.Round(awardMultiplier);
        //                clan.IsUnderMercenaryService = true;
        //                clan.ClanJoinFaction(kingdom);
        //                onMercenaryClanChangedKingdom.Invoke(CampaignEventDispatcher.Instance, new object[] { clan, null, kingdom });
        //                break;
        //            }
        //        //ChangeKingdomActionDetail.LeaveKingdom
        //        case 2:
        //        //ChangeKingdomActionDetail.LeaveWithRebellion
        //        case 3:
        //        //ChangeKingdomActionDetail.LeaveAsMercenary
        //        case 4:
        //            {
        //                StatisticsDataLogHelper.AddLog(StatisticsDataLogHelper.LogAction.ChangeKingdomAction, clan, kingdom, kingdom, true);
        //                clan.ClanLeaveKingdom();
        //                //ChangeKingdomActionDetail.LeaveAsMercenary
        //                if (detail == 4)
        //                {
        //                    onMercenaryClanChangedKingdom.Invoke(CampaignEventDispatcher.Instance, new object[] { clan, kingdom, null });
        //                    clan.IsUnderMercenaryService = false;
        //                }
        //                switch (detail)
        //                {
        //                    //ChangeKingdomActionDetail.LeaveWithRebellion
        //                    case 3:
        //                        {
        //                            if (clan == Clan.PlayerClan)
        //                            {
        //                                foreach (Clan clan2 in kingdom.Clans)
        //                                {
        //                                    ChangeRelationAction.ApplyRelationChangeBetweenHeroes(clan.Leader, clan2.Leader, -40);
        //                                }
        //                                DeclareWarAction.Apply(kingdom, clan);
        //                            }
        //                            onClanChangedKingdom.Invoke(CampaignEventDispatcher.Instance, new object[] { clan, kingdom, null, true });
        //                            break;
        //                        }
        //                    //ChangeKingdomActionDetail.LeaveKingdom
        //                    case 2:
        //                        {
        //                            if (clan == Clan.PlayerClan)
        //                            {
        //                                foreach (Clan clan3 in kingdom.Clans)
        //                                {
        //                                    int relationLoss = (clan3 == kingdom.RulingClan) ? Settings.Instance.LeaveKingdomRelationLossLeaderAmount : Settings.Instance.LeaveKingdomRelationLossVassalAmount;
        //                                    ChangeRelationAction.ApplyRelationChangeBetweenHeroes(clan.Leader, clan3.Leader, -relationLoss);
        //                                }
        //                            }
        //                            foreach (Settlement item3 in new List<Settlement>(clan.Settlements))
        //                            {
        //                                ChangeOwnerOfSettlementAction.ApplyByLeaveFaction(kingdom.Leader, item3);
        //                                foreach (Hero item4 in new List<Hero>(item3.HeroesWithoutParty))
        //                                {
        //                                    if (item4.CurrentSettlement != null && item4.Clan == clan)
        //                                    {
        //                                        if (item4.PartyBelongedTo != null)
        //                                        {
        //                                            LeaveSettlementAction.ApplyForParty(item4.PartyBelongedTo);
        //                                            EnterSettlementAction.ApplyForParty(item4.PartyBelongedTo, clan.Leader.HomeSettlement);
        //                                        }
        //                                        else
        //                                        {
        //                                            LeaveSettlementAction.ApplyForCharacterOnly(item4);
        //                                            EnterSettlementAction.ApplyForCharacterOnly(item4, clan.Leader.HomeSettlement);
        //                                        }
        //                                    }
        //                                }
        //                            }
        //                            onClanChangedKingdom.Invoke(CampaignEventDispatcher.Instance, new object[] { clan, kingdom, null, false });
        //                            break;
        //                        }
        //                }
        //                break;
        //            }
        //    }
        //    if (clan == Clan.PlayerClan)
        //    {
        //        Campaign.Current.UpdateDecisions();
        //    }
        //    typeof(ChangeKingdomAction).GetMethod("CheckIfPartyIconIsDirty", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic)
        //        .Invoke(null, new object[] { clan, kingdom });

        //    return false;
        //}

        //static bool Prepare()
        //{
        //    return Settings.Instance.LeaveKingdomRelationLossTweakEnabled;
        //}
    //}
}
