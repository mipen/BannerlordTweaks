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
                int num;
                if (BannerlordTweaksSettings.Instance.LeadershipPartySizeBonusEnabled)
                {
                    num = (int)Math.Ceiling(party.LeaderHero.GetSkillValue(DefaultSkills.Leadership) * BannerlordTweaksSettings.Instance.LeadershipPartySizeBonus);
                    __result += num;
                    explanation?.AddLine("Leadership bonus", num);
                }

                if (BannerlordTweaksSettings.Instance.StewardPartySizeBonusEnabled)
                {
                    num = (int)Math.Ceiling(party.LeaderHero.GetSkillValue(DefaultSkills.Steward) * BannerlordTweaksSettings.Instance.StewardPartySizeBonus);
                    __result += num;
                    explanation?.AddLine("Steward bonus", num);
                }
            }
        }

        static bool Prepare()
        {
            return BannerlordTweaksSettings.Instance.PartySizeTweakEnabled;
        }
    }
}
