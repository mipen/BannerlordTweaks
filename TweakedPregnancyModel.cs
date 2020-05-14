using System;
using System.Linq;
using Helpers;
using TaleWorlds.CampaignSystem;
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

        public override float CharacterFertilityProbability => Settings.Instance.CharacterFertilityProbabilityTweakEnabled
            ? Settings.Instance.CharacterFertilityProbability
            : base.CharacterFertilityProbability;

        public override float GetDailyChanceOfPregnancyForHero(Hero hero)
        {
            if (hero == null) throw new ArgumentNullException(nameof(hero));

            if (!Settings.Instance.DailyChancePregnancyTweakEnabled)
                return base.GetDailyChanceOfPregnancyForHero(hero);

            float num = 0.0f;

            if (!Settings.Instance.PlayerCharacterFertileEnabled && HeroIsMainOrSpouseOfMain(hero))
            {
                return num;
            }

            if (Settings.Instance.MaxChildrenTweakEnabled && hero.Children != null && hero.Children.Any() && hero.Children.Count >= Settings.Instance.MaxChildren)
            {
                return num;
            }

            if (hero.Spouse != null && hero.IsFertile && IsHeroAgeSuitableForPregnancy(hero))
            {
                ExplainedNumber bonuses = new ExplainedNumber(1f, null);
                PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Medicine.PerfectHealth, hero.Clan.Leader.CharacterObject, ref bonuses);
                num = (float)((6.5 - ((double)hero.Age - Settings.Instance.MinPregnancyAge) * 0.23) * 0.02) * bonuses.ResultNumber;
            }

            if (hero.Children == null || !hero.Children.Any())
                num *= 3f;
            else if (hero.Children.Count > 1)
                num *= 2f;

            return num;
        }

        private bool IsHeroAgeSuitableForPregnancy(Hero hero)
        {
            if (!hero.IsFemale)
                return true;

            return (double)hero.Age >= Settings.Instance.MinPregnancyAge && (double)hero.Age <= Settings.Instance.MaxPregnancyAge;
        }

        private bool HeroIsMainOrSpouseOfMain(Hero hero)
        {
            if (hero == Hero.MainHero)
                return true;

            if (hero.Spouse == Hero.MainHero)
                return true;

            return false;
        }
    }
}
