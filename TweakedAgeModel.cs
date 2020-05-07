using TaleWorlds.CampaignSystem.SandBox.GameComponents;

namespace BannerlordTweaks
{
    public class TweakedAgeModel : DefaultAgeModel
    {
        public override int BecomeInfantAge => Settings.Instance.AgeTweaksEnabled
            ? Settings.Instance.BecomeInfantAge
            : base.BecomeInfantAge;

        public override int BecomeChildAge => Settings.Instance.AgeTweaksEnabled
            ? Settings.Instance.BecomeChildAge
            : base.BecomeChildAge;

        public override int BecomeTeenagerAge => Settings.Instance.AgeTweaksEnabled
            ? Settings.Instance.BecomeTeenagerAge
            : base.BecomeTeenagerAge;
        
        public override int HeroComesOfAge => Settings.Instance.AgeTweaksEnabled
            ? Settings.Instance.HeroComesOfAge
            : base.HeroComesOfAge; 
        
        public override int BecomeOldAge => Settings.Instance.AgeTweaksEnabled
            ? Settings.Instance.BecomeOldAge
            : base.BecomeOldAge;
        
        public override int MaxAge => Settings.Instance.AgeTweaksEnabled
            ? Settings.Instance.MaxAge
            : base.MaxAge;
    }
}
