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

        static bool Prepare()
        {
            return BannerlordTweaksSettings.Instance.RemoteCompanionSkillManagementEnabled;
        }
    }
}
