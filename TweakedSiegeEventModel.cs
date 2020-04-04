using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;
using TaleWorlds.Core;

namespace BannerlordTweaks
{
    public class TweakedSiegeEventModel : DefaultSiegeEventModel
    {
        public override float GetConstructionProgressPerHour(SiegeEngineType type, SiegeEvent siegeEvent, ISiegeEventSide side, StatExplainer explanation = null)
        {
            if (Settings.Instance.SiegeConstructionProgressPerDayMultiplierEnabled)
                return base.GetConstructionProgressPerHour(type, siegeEvent, side, explanation) * Settings.Instance.SiegeConstructionProgressPerDayMultiplier;
            else
                return base.GetConstructionProgressPerHour(type, siegeEvent, side, explanation);
        }

        public override float GetColleteralDamageCasualties(SiegeEngineType siegeEngineType)
        {
            if (Settings.Instance.SiegeCasualtiesTweakEnabled)
                return base.GetColleteralDamageCasualties(siegeEngineType) * Settings.Instance.SiegeCollateralDamageCasualties;
            else
                return base.GetColleteralDamageCasualties(siegeEngineType);
        }

        public override float GetDestructionCasualties(SiegeEngineType destroyedSiegeEngine)
        {
            if (Settings.Instance.SiegeCasualtiesTweakEnabled)
                return base.GetDestructionCasualties(destroyedSiegeEngine) * Settings.Instance.SiegeDestructionCasualties;
            else
                return base.GetDestructionCasualties(destroyedSiegeEngine);
        }
    }
}
