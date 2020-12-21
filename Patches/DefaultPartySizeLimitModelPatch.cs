using HarmonyLib;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Party;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace BannerlordTweaks.Patches
{
    [HarmonyPatch(typeof(DefaultPartySizeLimitModel), "CalculateMobilePartyMemberSizeLimit")]
    public class DefaultPartySizeLimitModelPatch
    {
        static void Postfix(MobileParty party, StatExplainer explanation, ref int __result)
        {
            if (party.LeaderHero != null && party.LeaderHero == Hero.MainHero && BannerlordTweaksSettings.Instance is not null)
            {
                int num;
                if (BannerlordTweaksSettings.Instance.LeadershipPartySizeBonusEnabled)
                {
                    num = (int)Math.Ceiling(party.LeaderHero.GetSkillValue(DefaultSkills.Leadership) * BannerlordTweaksSettings.Instance.LeadershipPartySizeBonus);
                    __result += num;
                    explanation?.AddLine("BT Leadership bonus", num);
                }

                if (BannerlordTweaksSettings.Instance.StewardPartySizeBonusEnabled)
                {
                    num = (int)Math.Ceiling(party.LeaderHero.GetSkillValue(DefaultSkills.Steward) * BannerlordTweaksSettings.Instance.StewardPartySizeBonus);
                    __result += num;
                    explanation?.AddLine("BT Steward bonus", num);
                }
            }
        }

        static bool Prepare()
        {
            return BannerlordTweaksSettings.Instance.PartySizeTweakEnabled;
        }
    }
    
    [HarmonyPatch(typeof(DefaultPartySizeLimitModel), "CalculateMobilePartyPrisonerSizeLimitInternal")]
    public class DefaultPrisonerSizeLimitModelPatch
    {
        static void Postfix(PartyBase party, StatExplainer explanation, ref int __result)
        {
            if (party.LeaderHero != null && party.LeaderHero == Hero.MainHero)
            {
                if (BannerlordTweaksSettings.Instance.PrisonerSizeTweakEnabled)
                {
                    double percent = Math.Abs((double)(BannerlordTweaksSettings.Instance.PrisonerSizeTweakPercent) / 100);
                    double num = (int)Math.Ceiling(__result * percent);
                    __result += (int)Math.Round(num);
                    explanation?.AddLine("BT Prisoner Limit Bonus", (float)num);
                }
            }
        }

        static bool Prepare()
        {
            return BannerlordTweaksSettings.Instance.PrisonerSizeTweakEnabled;
        }
    }

}
