using HarmonyLib;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Party;
using TaleWorlds.Core;
using TaleWorlds.Localization;
using TaleWorlds.ObjectSystem;

namespace BannerlordTweaks.Patches
{
    static public class QuestPartySizeHelper
    {
        static public int GetPartySize(MobileParty party)
        {
            if (party == null) throw new ArgumentNullException(nameof(party));
            int partySize = party.Party.NumberOfAllMembers;

            if (party.MemberRoster != null)
            {
                foreach (TroopRosterElement troopRosterElement in party.MemberRoster)
                {
                    if (troopRosterElement.Character != null && troopRosterElement.Character.Culture != null && troopRosterElement.Character.Culture.Villager != null &&
                        (troopRosterElement.Character == MBObjectManager.Instance.GetObject<CharacterObject>(troopRosterElement.Character.Culture.Villager.StringId)) ||
                        troopRosterElement.Character == MBObjectManager.Instance.GetObject<CharacterObject>("borrowed_troop") ||
                        troopRosterElement.Character == MBObjectManager.Instance.GetObject<CharacterObject>("veteran_borrowed_troop"))
                    {
                        partySize -= troopRosterElement.Number;
                    }
                }
            }

            return partySize;
        }
    }
    [HarmonyPatch(typeof(DefaultPartyMoraleModel), "GetPartySizeMoraleEffect")]
    public class GetPartySizeMoraleEffectPatch
    {
        static bool Prefix(MobileParty party, ref ExplainedNumber result, TextObject ____partySizeMoraleText)
        {
            if (party != null && party.Party != null && party.Party.LeaderHero != null && party.Party.LeaderHero == Hero.MainHero)
            {
                int num = QuestPartySizeHelper.GetPartySize(party) - party.Party.PartySizeLimit;
                if (num > 0)
                {
                    result.Add(-1f * (float)Math.Sqrt((double)num), ____partySizeMoraleText);
                }
                return false;
            }
            return true;
        }

        static bool Prepare()
        {
            return Settings.Instance.QuestCharactersIgnorePartySize;
        }
    }

    [HarmonyPatch(typeof(DefaultPartyMoraleModel), "NumberOfDesertersDueToPaymentRatio")]
    public class NumberOfDesertersDueToPaymentRatioPatch
    {
        static bool Prefix(MobileParty mobileParty, ref int __result)
        {
            if (mobileParty != null && mobileParty.Party != null && mobileParty.Party.LeaderHero != null && mobileParty.Party.LeaderHero == Hero.MainHero)
            {
                int partySizeLimit = mobileParty.Party.PartySizeLimit;
                __result = MBRandom.RoundRandomized(((float)QuestPartySizeHelper.GetPartySize(mobileParty) - mobileParty.PaymentRatio * (float)partySizeLimit) * 0.2f);
                return false;
            }
            else
                return true;
        }

        static bool Prepare()
        {
            return Settings.Instance.QuestCharactersIgnorePartySize;
        }
    }
}
