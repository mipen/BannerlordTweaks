using System;
using HarmonyLib;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.CampaignSystem.ViewModelCollection.ArmyManagement;
using TaleWorlds.CampaignSystem.Actions;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Localization;

/* Attempt to add the ability to create armies when player is Merc or not in a kingdom. Based on 
 * an updated version of Splintertx's Mercenary Army mod.
 * 
 * Effort is stalled as the 'CreateArmy()' method is embedded in the Kingdom{} class and you can't 
 * create a party w/out being in a kingdom.
 * 
 * Bug: Disbanding the army is not working. You can click on the party leaders in the army and 
 * remove them from the army, and it will allow you to diband. 
 * 
 * Bug: Once you disband the army by removing the leaders, you cannot recreate an army again.
 * 
 * Idea: Create a 'temp kingdom' (Maybe a minor faction?) and join the player to it. Would have to 
 * set a flag to block some kingdom functions (like manipulating policies, etc.) and set up a way 
 * to assure the kingdom is dissolved when the player leaves it or the party is disbanded.
 * 
 * Or just wait for TW to come up w/ some way to create armies w/out being in a kingdom.
 */

namespace BannerlordTweaks.Patches
{
	[HarmonyPatch(typeof(MapVM), "CanGatherArmyWithReason")]

	public class MapVMPatch
	{
		static void Postfix(ref bool __result, ref string reasonText)
		{
			//DebugHelpers.DebugMessage("CanGatherArmy Triggered");
			if (__result == false)
			{
				__result = true;
				reasonText = "";
			}
		}

		static bool Prepare()
		{
			return BannerlordTweaksSettings.Instance.GatherArmyTweakEnabled;
		}
	}

	// Jiros: Borrowed from Mercenary Army Mod - Credit: Splintert
	[HarmonyPatch(typeof(ArmyManagementVM), MethodType.Constructor, new Type[] { typeof(Action) })]
	public class ArmyManagementVMMod
	{
		static void Postfix(ArmyManagementVM __instance)
		{
			if (!Hero.MainHero.MapFaction.IsKingdomFaction || Clan.PlayerClan.IsUnderMercenaryService)
			{
				for (int x = 0; x < __instance.PartyList.Count; x++)
				{
					if (__instance.PartyList[x].Clan.Id != MobileParty.MainParty.LeaderHero.Clan.Id)
					{
						__instance.PartyList.RemoveAt(x);
						x--;
					}
				}
			}
		}
	}


	[HarmonyPatch(typeof(ArmyManagementVM), "ExecuteDone")]
	public class ExecuteDonePatch
	{
		static bool Prefix(ArmyManagementVM __instance, MBBindingList<ArmyManagementItemVM> ____partiesToRemove, Action ____onClose)
		{
			// Only enable this hack if it is enabled and player is independent
			// THIS IS A PORT OF ArmyManagementVM.ExecuteDone!
			if (!Hero.MainHero.MapFaction.IsKingdomFaction && BannerlordTweaksSettings.Instance.AllowCreateArmyAsMerc)
			{
				int num = __instance.PartiesInCart.Sum((ArmyManagementItemVM P) => P.Cost);
				bool flag = num <= 0 || (float)num <= Hero.MainHero.Clan.Influence;
				if (flag && __instance.NewCohesion > __instance.Cohesion)
				{
					DebugHelpers.DebugMessage("Cohesion low trigger.");
					if (MobileParty.MainParty.Army != null)
					{
						ArmyManagementCalculationModel armyManagementCalculationModel = Campaign.Current.Models.ArmyManagementCalculationModel;
						int num2 = __instance.NewCohesion - __instance.Cohesion;
						int cost = armyManagementCalculationModel.CalculateTotalInfluenceCost(MobileParty.MainParty.Army, (float)num2);
						DebugHelpers.DebugMessage("Cohesion cost =" + cost);
						//MobileParty.MainParty.Army.BoostCohesionWithInfluence((float)num2, cost);
					}
				}
				if (__instance.PartiesInCart.Count > 1 && flag /* && MobileParty.MainParty.MapFaction.IsKingdomFaction*/)
				{
					if (MobileParty.MainParty.Army == null)
					{
						DebugHelpers.DebugMessage("MainParty.Army is null.");
						if ((MobileParty.MainParty.MapFaction as Kingdom) is null)
                        {
							string temp_kingdom = Hero.MainHero.Name.ToString();
							CampaignCheats.CreatePlayerKingdom(new List<string>());
						}
						((Kingdom)MobileParty.MainParty.MapFaction).CreateArmy(Hero.MainHero, Hero.MainHero.HomeSettlement, Army.ArmyTypes.Patrolling);
					}
					foreach (ArmyManagementItemVM armyManagementItemVM in __instance.PartiesInCart)
					{
						if (armyManagementItemVM.Party != MobileParty.MainParty)
						{
							armyManagementItemVM.Party.Army = MobileParty.MainParty.Army;
							SetPartyAiAction.GetActionForEscortingParty(armyManagementItemVM.Party, MobileParty.MainParty);
							armyManagementItemVM.Party.IsJoiningArmy = true;
						}
					}
					DebugHelpers.DebugMessage("Subtracting influence.");
					Hero.MainHero.Clan.Influence -= (float)num;
				}
				if (____partiesToRemove.Count > 0)
				{
					DebugHelpers.DebugMessage("Have parties to remove."); 
					bool flag2 = false;
					foreach (ArmyManagementItemVM armyManagementItemVM2 in ____partiesToRemove)
					{
						if (armyManagementItemVM2.Party == MobileParty.MainParty)
						{
							armyManagementItemVM2.Party.Army = null;
							flag2 = true;
						}
					}
					if (!flag2)
					{
						foreach (ArmyManagementItemVM armyManagementItemVM3 in ____partiesToRemove)
						{
							Army army = MobileParty.MainParty.Army;
							if (army != null && army.Parties.Contains(armyManagementItemVM3.Party))
							{
								armyManagementItemVM3.Party.Army = null;
							}
						}
					}
					____partiesToRemove.Clear();
				}
				if (flag)
				{
					____onClose();
					return false;
				}
				else 
					InformationManager.AddQuickInformation(new TextObject("{=Xmw93W6a}Not Enough Influence", null), 0, null, "");

				return false;
			}
			return false;
		}
	}

}
