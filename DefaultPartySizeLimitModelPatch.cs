using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Party;
using TaleWorlds.Core;

namespace BannerlordTweaks
{
    [HarmonyPatch(typeof(DefaultPartySizeLimitModel), "CalculateMobilePartyMemberSizeLimit")]
    public class DefaultPartySizeLimitModelPatch
    {
        static void Postfix(MobileParty party, StatExplainer explanation, ref int __result)
        {
            if (party.LeaderHero != null && party.LeaderHero == Hero.MainHero)
            {
                SkillObject skill = SkillObject.FindFirst((x) => { return x.Name.ToString() == "Leadership"; });
                int num = (int)Math.Ceiling(party.LeaderHero.GetSkillValue(skill) * 0.3d);
                __result += num;
                explanation?.AddLine("Leadership bonus", num);
                skill = SkillObject.FindFirst((x) => x.Name.ToString() == "Steward");
                num = (int)Math.Ceiling(party.LeaderHero.GetSkillValue(skill) * 0.3d);
                __result += num;
                explanation?.AddLine("Steward bonus", num);
            }
        }
    }
}
