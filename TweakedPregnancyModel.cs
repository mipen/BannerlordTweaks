using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;

namespace BannerlordTweaks
{
    /// <summary>
    /// Allows for tweaking of the pregnancy Model
    /// NOTE: we set the probabilities for disabling to -1 since TW does a smaller-equals check on a range of [0,1)
    /// </summary>
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
