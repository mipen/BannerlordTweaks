using HarmonyLib;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Party;
using TaleWorlds.Core;

namespace BannerlordTweaks.Patches
{
    [HarmonyPatch(typeof(DefaultPartySizeLimitModel), "CalculateMobilePartyMemberSizeLimit")]
    public class DefaultPartySizeLimitModelPatch
    {
        static void Postfix(MobileParty party, StatExplainer explanation, ref int __result)
        {
            if (party.LeaderHero != null && party.LeaderHero == Hero.MainHero)
            {
                SkillObject skill;
                int num;
                if (Settings.Instance.LeadershipPartySizeBonusEnabled)
                {
                    num = (int)Math.Ceiling(party.LeaderHero.GetSkillValue(DefaultSkills.Leadership) * Settings.Instance.LeadershipPartySizeBonus);
                    __result += num;
                    explanation?.AddLine("Leadership bonus", num);
                }

                if (Settings.Instance.StewardPartySizeBonusEnabled)
                {
                    num = (int)Math.Ceiling(party.LeaderHero.GetSkillValue(DefaultSkills.Steward) * Settings.Instance.StewardPartySizeBonus);
                    __result += num;
                    explanation?.AddLine("Steward bonus", num);
                }
            }
        }

        static bool Prepare()
        {
            return Settings.Instance.PartySizeTweakEnabled;
        }
    }
}
