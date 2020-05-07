using TaleWorlds.CampaignSystem.SandBox.GameComponents;

namespace BannerlordTweaks
{
    public class TweakedPregnancyModel : DefaultPregnancyModel
    {
        public override float StillbirthProbability => Settings.Instance.NoStillbirthsTweakEnabled
           ? -1
           : base.StillbirthProbability;

        public override float MaternalMortalityProbabilityInLabor => Settings.Instance.NoMaternalMortalityTweakEnabled
            ? -1
            : base.MaternalMortalityProbabilityInLabor;

        public override float DeliveringTwinsProbability => Settings.Instance.TwinsProbabilityTweakEnabled
            ? Settings.Instance.TwinsProbability
            : base.DeliveringTwinsProbability;

        public override float DeliveringFemaleOffspringProbability => Settings.Instance.FemaleOffspringProbabilityTweakEnabled
            ? Settings.Instance.FemaleOffspringProbability
            : base.DeliveringFemaleOffspringProbability;

        public override float PregnancyDurationInDays => Settings.Instance.PregnancyDurationTweakEnabled
            ? Settings.Instance.PregnancyDuration
            : base.PregnancyDurationInDays;
    }
}
