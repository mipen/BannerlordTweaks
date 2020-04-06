using HarmonyLib;
using System;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace BannerlordTweaks.Patches
{
	[HarmonyPatch(typeof(DefaultCharacterDevelopmentModel), "CalculateLearningLimit")]
	[HarmonyPatch(new Type[] { typeof(int), typeof(int), typeof(TextObject), typeof(StatExplainer) })]
	public class LearningLimitPatch2
	{
		static bool Prefix(DefaultCharacterDevelopmentModel __instance, int attributeValue, int focusValue, TextObject attributeName, StatExplainer explainer, ref TextObject ____skillFocusText, ref int __result)
		{
			if (Settings.Instance.HeroSkillLearningEnabled)
			{
				ExplainedNumber explainedNumber = new ExplainedNumber(0f, explainer, null);
				explainedNumber.Add((float)((attributeValue - 1) * Settings.Instance.HeroSkillLearningAttributeBonus), attributeName);
				explainedNumber.Add((float)(focusValue * Settings.Instance.HeroSkillLearningFocusPointBonus), ____skillFocusText);
				explainedNumber.LimitMin(Settings.Instance.HeroSkillLearningMinimum);
				__result = (int)explainedNumber.ResultNumber;
				return false;
			}
			return true;
		}
	}
}