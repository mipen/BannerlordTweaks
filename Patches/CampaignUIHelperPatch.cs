using HarmonyLib;
using TaleWorlds.CampaignSystem.ViewModelCollection;

namespace BannerlordTweaks.Patches
{
    [HarmonyPatch(typeof(CampaignUIHelper), "GetAddFocusHintString")]
    public class CampaignUIHelperPatch
    {
        static void Prefix(ref bool isInSamePartyAsPlayer)
        {
            isInSamePartyAsPlayer = true;
        }

        static bool Prepare() => BannerlordTweaksSettings.Instance is { } settings && settings.RemoteCompanionSkillManagementEnabled;
    }
}
