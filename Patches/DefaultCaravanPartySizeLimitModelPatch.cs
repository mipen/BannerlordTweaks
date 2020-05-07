using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Party;

namespace BannerlordTweaks.Patches
{
    [HarmonyPatch(typeof(DefaultPartySizeLimitModel), "CalculateMobilePartyMemberSizeLimit")]
    public class DefaultCaravanPartySizeLimitModelPatch
    {
        static void Postfix(MobileParty party, StatExplainer explanation, ref int __result)
        {
            if (party.IsCaravan && party.Party?.Owner != null && party.Party.Owner == Hero.MainHero)
            {
                __result = Settings.Instance.PlayerCaravanPartySize;
            }
        }

        static bool Prepare()
        {
            return Settings.Instance.PlayerCaravanPartySizeTweakEnabled;
        }
    }
}
