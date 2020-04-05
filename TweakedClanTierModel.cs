using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;

namespace BannerlordTweaks
{
    public class TweakedClanTierModel : DefaultClanTierModel
    {
        public override int GetCompanionLimitForTier(int clanTier)
        {
            if (Settings.Instance.CompanionLimitTweakEnabled)
                return Settings.Instance.CompanionBaseLimit + clanTier * Settings.Instance.CompanionLimitBonusPerClanTier;
            else
                return base.GetCompanionLimitForTier(clanTier);
        }

        public override int GetPartyLimitForTier(Clan clan, int clanTierToCheck)
        {
            if (Settings.Instance.ClanPartiesLimitTweakEnabled && clan == Clan.PlayerClan)
                return Settings.Instance.BaseClanPartiesLimit + (int)Math.Floor(clanTierToCheck * Settings.Instance.ClanPartiesBonusPerClanTier);
            else
                return base.GetPartyLimitForTier(clan, clanTierToCheck);
        }
    }
}
