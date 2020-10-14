using HarmonyLib;
using TaleWorlds.CampaignSystem.ViewModelCollection.CharacterDeveloper;

namespace BannerlordTweaks.Patches
{
    [HarmonyPatch(typeof(SkillVM), "InitializeValues")]
    public class SkillVMPatch
    {
        static void Postfix(ref bool ____isInSamePartyAsPlayer)
        {
            ____isInSamePartyAsPlayer = true;
        }

        static bool Prepare()
        {
            return BannerlordTweaksSettings.Instance.RemoteCompanionSkillManagementEnabled;
        }
    }
}
