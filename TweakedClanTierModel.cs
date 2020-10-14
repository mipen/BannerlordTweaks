using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;

namespace BannerlordTweaks
{
    public class TweakedClanTierModel : DefaultClanTierModel
    {
        public override int GetCompanionLimitForTier(int clanTier)
        {
            if (BannerlordTweaksSettings.Instance.CompanionLimitTweakEnabled)
                return BannerlordTweaksSettings.Instance.CompanionBaseLimit + clanTier * BannerlordTweaksSettings.Instance.CompanionLimitBonusPerClanTier;
            else
                return base.GetCompanionLimitForTier(clanTier);
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
