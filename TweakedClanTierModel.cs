using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;

namespace BannerlordTweaks
{
    public class TweakedClanTierModel : DefaultClanTierModel
    {
        // 1.5.5 - This method was changed to private. New method is "GetCompanionLimit"
        /* public override int GetCompanionLimitForTier(int clanTier)
        {
            if (BannerlordTweaksSettings.Instance.CompanionLimitTweakEnabled)
                return BannerlordTweaksSettings.Instance.CompanionBaseLimit + clanTier * BannerlordTweaksSettings.Instance.CompanionLimitBonusPerClanTier;
            else
                return base.GetCompanionLimitForTier(clanTier);
        }
        */

        public override int GetCompanionLimit(Clan clan)
        {
            if (BannerlordTweaksSettings.Instance.CompanionLimitTweakEnabled)
            {
                int clanTier = clan.Tier;
                
                // From the now-private GetCompanionLimitFromTier()
                int companionLimitFromTier = clanTier + 3;

                ExplainedNumber explainedNumber = new ExplainedNumber((float)companionLimitFromTier, null);
                if (clan.Leader.GetPerkValue(DefaultPerks.Leadership.WePledgeOurSwords))
                {
                    explainedNumber.Add(DefaultPerks.Leadership.WePledgeOurSwords.PrimaryBonus, null, null);
                }
                return BannerlordTweaksSettings.Instance.CompanionBaseLimit + (int)explainedNumber.ResultNumber * BannerlordTweaksSettings.Instance.CompanionLimitBonusPerClanTier;
            }
            else
                return base.GetCompanionLimit(clan);
        }

        public override int GetPartyLimitForTier(Clan clan, int clanTierToCheck)
        {
            if (BannerlordTweaksSettings.Instance.ClanPartiesLimitTweakEnabled && clan == Clan.PlayerClan)
                return BannerlordTweaksSettings.Instance.BaseClanPartiesLimit + (int)Math.Floor(clanTierToCheck * BannerlordTweaksSettings.Instance.ClanPartiesBonusPerClanTier);
            else
                return base.GetPartyLimitForTier(clan, clanTierToCheck);
        }
    }
}
