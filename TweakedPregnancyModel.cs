using Helpers;
using System;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;

namespace BannerlordTweaks
{
    public class TweakedPregnancyModel : DefaultPregnancyModel
    {
        public override float StillbirthProbability => BannerlordTweaksSettings.Instance.NoStillbirthsTweakEnabled
           ? -1
           : base.StillbirthProbability;

        public override float MaternalMortalityProbabilityInLabor => BannerlordTweaksSettings.Instance.NoMaternalMortalityTweakEnabled
            ? -1
            : base.MaternalMortalityProbabilityInLabor;

        public override float DeliveringTwinsProbability => BannerlordTweaksSettings.Instance.TwinsProbabilityTweakEnabled
            ? BannerlordTweaksSettings.Instance.TwinsProbability
            : base.DeliveringTwinsProbability;

        public override float DeliveringFemaleOffspringProbability => BannerlordTweaksSettings.Instance.FemaleOffspringProbabilityTweakEnabled
            ? BannerlordTweaksSettings.Instance.FemaleOffspringProbability
            : base.DeliveringFemaleOffspringProbability;

        public override float PregnancyDurationInDays => BannerlordTweaksSettings.Instance.PregnancyDurationTweakEnabled
            ? BannerlordTweaksSettings.Instance.PregnancyDuration
            : base.PregnancyDurationInDays;

        /* Looks like CharacterFertilityProbability was removed in 1.5.2
        public override float CharacterFertilityProbability => BannerlordTweaksSettings.Instance.CharacterFertilityProbabilityTweakEnabled
            ? BannerlordTweaksSettings.Instance.CharacterFertilityProbability
            : base.CharacterFertilityProbability;
        */

        public override float GetDailyChanceOfPregnancyForHero(Hero hero)
        {
            if (hero == null) throw new ArgumentNullException(nameof(hero));

            if (!BannerlordTweaksSettings.Instance.DailyChancePregnancyTweakEnabled)
                return base.GetDailyChanceOfPregnancyForHero(hero);

            float num = 0f;
            if (!BannerlordTweaksSettings.Instance.PlayerCharacterFertileEnabled && HeroIsMainOrSpouseOfMain(hero))
            {
                DebugHelpers.Message("Hero: " + hero.Name + "PlayerCharacterFertileEnabled Check - num = " + num);
                return num;
            }

            if (BannerlordTweaksSettings.Instance.MaxChildrenTweakEnabled && hero.Children != null && hero.Children.Any() && hero.Children.Count >= BannerlordTweaksSettings.Instance.MaxChildren)
            {
                DebugHelpers.Message("Hero: " + hero.Name + "MaxChildrenTweakEnabled Check - num = " + num);
                return num;
            }

            if (hero.Spouse != null && hero.IsFertile && IsHeroAgeSuitableForPregnancy(hero))
            {
                ExplainedNumber bonuses = new ExplainedNumber(1f, null);
                PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Medicine.PerfectHealth, hero.Clan.Leader.CharacterObject, true, ref bonuses);
                //num = (float)((6.5 - ((double)hero.Age - BannerlordTweaksSettings.Instance.MinPregnancyAge) * 0.23) * 0.02) * bonuses.ResultNumber;
                //num = (float)((6.7 - ((double)hero.Age - BannerlordTweaksSettings.Instance.MinPregnancyAge) * 0.2) * 0.02 / ((hero.Children.Count + (1 ^ 2)) * 0.3f)) * bonuses.ResultNumber;
                num = (float)((6.9 - ((double)hero.Age - BannerlordTweaksSettings.Instance.MinPregnancyAge) * 0.2) * 0.02) / ((hero.Children.Count + 1) * 0.2f) * bonuses.ResultNumber;
                //num = (float)((6.9 - ((double)hero.Age - BannerlordTweaksSettings.Instance.MinPregnancyAge) * 0.2) * 0.02);
                //double num2 = hero.Children.Count + 1;
                //num2 *= 0.2;
                //num /= (float)num2;
                //num = (1.2f - (hero.Age - 18f) * 0.04f) / (float)(hero.Children.Count + 1 ^ 2) * 0.2f * bonuses.ResultNumber;
             }

            if (BannerlordTweaksSettings.Instance.ClanFertilityBonusEnabled && hero.Clan == Hero.MainHero.Clan)
                //num *= 1.25f;
                num *= BannerlordTweaksSettings.Instance.ClanFertilityBonus;

            return num;
        }

        private bool IsHeroAgeSuitableForPregnancy(Hero hero)
        {
            if (!hero.IsFemale)
                return true;

            return (double)hero.Age >= BannerlordTweaksSettings.Instance.MinPregnancyAge && (double)hero.Age <= BannerlordTweaksSettings.Instance.MaxPregnancyAge;
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
