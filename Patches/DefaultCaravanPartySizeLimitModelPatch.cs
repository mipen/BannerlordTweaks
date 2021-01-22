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
            if (party.IsCaravan && party.Party?.Owner != null && party.Party.Owner == Hero.MainHero && BannerlordTweaksSettings.Instance is not null)
            {
                __result = BannerlordTweaksSettings.Instance.PlayerCaravanPartySize;
            }
        }

        static bool Prepare() => BannerlordTweaksSettings.Instance is { } settings && settings.PlayerCaravanPartySizeTweakEnabled;
    }
}
