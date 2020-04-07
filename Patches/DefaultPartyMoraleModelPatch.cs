using HarmonyLib;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Party;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace BannerlordTweaks.Patches
{
    [HarmonyPatch(typeof(DefaultPartyMoraleModel), "GetPartySizeMoraleEffect")]
    public class GetPartySizeMoraleEffectPatch
    {
        static bool Prefix(GetPartySizeMoraleEffectPatch __instance, MobileParty party, ref ExplainedNumber result, TextObject ____partySizeMoraleText)
        {
            int party_size = party.Party.NumberOfAllMembers;

            foreach (TroopRosterElement troopRosterElement in party.MemberRoster)
            {
                if (troopRosterElement.Character == MBObjectManager.Instance.GetObject<CharacterObject>("borrowed_troop") ||
                    troopRosterElement.Character == MBObjectManager.Instance.GetObject<CharacterObject>("veteran_borrowed_troop"))
                {
                    party_size -= troopRosterElement.Number;
                }
            }
            
            int num = party_size - party.Party.PartySizeLimit;
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
}
