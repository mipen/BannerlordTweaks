using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;
using TaleWorlds.Core;

namespace BannerlordTweaks
{
    public class TweakedSiegeEventModel : DefaultSiegeEventModel
    {
        public override float GetConstructionProgressPerHour(SiegeEngineType type, SiegeEvent siegeEvent, ISiegeEventSide side, StatExplainer explanation = null)
        {
            if (BannerlordTweaksSettings.Instance.SiegeConstructionProgressPerDayMultiplierEnabled)
                return base.GetConstructionProgressPerHour(type, siegeEvent, side, explanation) * BannerlordTweaksSettings.Instance.SiegeConstructionProgressPerDayMultiplier;
            else
                return base.GetConstructionProgressPerHour(type, siegeEvent, side, explanation);
        }

        public override float GetColleteralDamageCasualties(SiegeEngineType siegeEngineType, MobileParty party)
        {
            if (BannerlordTweaksSettings.Instance.SiegeCasualtiesTweakEnabled)
                return BannerlordTweaksSettings.Instance.SiegeCollateralDamageCasualties;
            else
                return base.GetColleteralDamageCasualties(siegeEngineType, party);
        }

        public override float GetDestructionCasualties(SiegeEngineType destroyedSiegeEngine)
        {
            if (BannerlordTweaksSettings.Instance.SiegeCasualtiesTweakEnabled)
                return BannerlordTweaksSettings.Instance.SiegeDestructionCasualties;
            else
                return base.GetDestructionCasualties(destroyedSiegeEngine);
        }
    }
}
