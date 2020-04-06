using HarmonyLib;
using System;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace BannerlordTweaks.Patches
{
	[HarmonyPatch(typeof(DefaultCharacterDevelopmentModel), "CalculateLearningLimit")]
	[HarmonyPatch(new Type[] { typeof(Hero), typeof(SkillObject), typeof(StatExplainer) })]
	public class LearningLimitPatch1
    {
        static bool Prefix(DefaultCharacterDevelopmentModel __instance, Hero hero, SkillObject skill, StatExplainer explainer, ref TextObject ____skillFocusText, ref int __result)
        {
				ExplainedNumber explainedNumber = new ExplainedNumber(0f, explainer, null);
				int attributeValue = hero.GetAttributeValue(skill.CharacterAttributeEnum);
				int focus = hero.HeroDeveloper.GetFocus(skill);
				explainedNumber.Add((float)((attributeValue - 1) * Settings.Instance.HeroSkillLearningAttributeBonus), skill.CharacterAttribute.Name);
				explainedNumber.Add((float)(focus * Settings.Instance.HeroSkillLearningFocusPointBonus), ____skillFocusText);
				explainedNumber.LimitMin(Settings.Instance.HeroSkillLearningMinimum);
				__result = (int)explainedNumber.ResultNumber;
				return false;
		}
		static bool Prepare()
		{
			return Settings.Instance.HeroSkillLearningEnabled;
		}
	}
}