using System.Collections.Generic;
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

        public IEnumerable<string> GetConfigErrors()
        {
            if (MaxAge <= BecomeOldAge)
                yield return "\'Max Age\' must be greater than \'Become Old \'Age\'.";
            if (BecomeOldAge <= HeroComesOfAge)
                yield return "\'Become Old Age\' must be greater than \'Hero Comes Of Age\'.";
            if (HeroComesOfAge <= BecomeTeenagerAge)
                yield return "\'Hero Comes Of Age\' must be greater than \'Become Teenager Age\'.";
            if (BecomeTeenagerAge <= BecomeChildAge)
                yield return "\'Become Teenager Age\' must be greater than \'Become Child Age\'";
            if (BecomeChildAge <= BecomeInfantAge)
                yield return "\'Become Child Age\' must be greater than \'Become Infant Age\'";
        }
    }
}
