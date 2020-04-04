using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
