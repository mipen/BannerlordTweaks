using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;

namespace BannerlordTweaks
{
    public class TweakedDefaultAgeModel : DefaultAgeModel
    {
        public override int BecomeInfantAge => Settings.Instance.DefaultAgeTweakEnabled
            ? Settings.Instance.BecomeInfantAge
            : base.BecomeInfantAge;

        public override int BecomeChildAge => Settings.Instance.DefaultAgeTweakEnabled
            ? Settings.Instance.BecomeChildAge
            : base.BecomeChildAge;

        public override int BecomeTeenagerAge => Settings.Instance.DefaultAgeTweakEnabled
            ? Settings.Instance.BecomeTeenagerAge
            : base.BecomeTeenagerAge;
        
        public override int HeroComesOfAge => Settings.Instance.DefaultAgeTweakEnabled
            ? Settings.Instance.HeroComesOfAge
            : base.HeroComesOfAge; 
        
        public override int BecomeOldAge => Settings.Instance.DefaultAgeTweakEnabled
            ? Settings.Instance.BecomeOldAge
            : base.BecomeOldAge;
        
        public override int MaxAge => Settings.Instance.DefaultAgeTweakEnabled
            ? Settings.Instance.MaxAge
            : base.MaxAge;
    }
}
