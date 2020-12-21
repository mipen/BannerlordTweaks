using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;

namespace BannerlordTweaks.Patches
{
    
    [HarmonyPatch(typeof(GarrisonTroopsCampaignBehavior), "OnSettlementEntered")]
    class GarrisonTroopsCampaignBehaviorPatch
    {
        static bool Prefix(ref MobileParty mobileParty, Settlement settlement, Hero hero)
        {
            if (hero != null && mobileParty != null && mobileParty.LeaderHero != null && !mobileParty.IsDisbanding && settlement.MapFaction == Hero.MainHero.MapFaction)
            {
                bool extendToFactionOk = ((BannerlordTweaksSettings.Instance is { } settings && settings.DisableTroopDonationFactionWideEnabled) && mobileParty.LeaderHero.MapFaction == Hero.MainHero.MapFaction && mobileParty.LeaderHero.Clan != Clan.PlayerClan);
                bool DonateCheck = (extendToFactionOk == true || mobileParty.LeaderHero.Clan == Clan.PlayerClan);
                if (DonateCheck == true)
                {
                    return false;
                }
             }
            return true;
        }

        static bool Prepare() => BannerlordTweaksSettings.Instance is { } settings && settings.DisableTroopDonationPatchEnabled;
    }
}
