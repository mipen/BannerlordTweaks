using BannerlordTweaks.Lib;
using HarmonyLib;
using System;
using System.Windows.Forms;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Party;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace BannerlordTweaks.Patches
{
    static public class QuestPartySizeHelper
    {
        static public int GetPartySize(MobileParty party)
        {
            int party_size = party.Party.NumberOfAllMembers;

            foreach (TroopRosterElement troopRosterElement in party.MemberRoster)
            {
                if (
                    (troopRosterElement.Character.Culture.Villager != null &&
                    troopRosterElement.Character == MBObjectManager.Instance.GetObject<CharacterObject>(troopRosterElement.Character.Culture.Villager.StringId)) ||
                    troopRosterElement.Character == MBObjectManager.Instance.GetObject<CharacterObject>("borrowed_troop") ||
                    troopRosterElement.Character == MBObjectManager.Instance.GetObject<CharacterObject>("veteran_borrowed_troop"))
                {
                    party_size -= troopRosterElement.Number;
                }
            }

            return party_size;
        }
    }
    [HarmonyPatch(typeof(DefaultPartyMoraleModel), "GetPartySizeMoraleEffect")]
    public class GetPartySizeMoraleEffectPatch
    {
        static bool Prefix(MobileParty party, ref ExplainedNumber result, TextObject ____partySizeMoraleText)
        {
            int num = QuestPartySizeHelper.GetPartySize(party) - party.Party.PartySizeLimit;
            if (num > 0)
            {
                result.Add(-1f * (float)Math.Sqrt((double)num), ____partySizeMoraleText);
            }
            return false;
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
            int partySizeLimit = mobileParty.Party.PartySizeLimit;
            __result = MBRandom.RoundRandomized(((float)QuestPartySizeHelper.GetPartySize(mobileParty) - mobileParty.PaymentRatio * (float)partySizeLimit) * 0.2f);
            return false;
        }

        static bool Prepare()
        {
            return Settings.Instance.QuestCharactersIgnorePartySize;
        }
    }
}
