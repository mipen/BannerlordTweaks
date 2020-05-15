using HarmonyLib;
using TaleWorlds.CampaignSystem.ViewModelCollection.CharacterDeveloper;

namespace BannerlordTweaks.Patches
{
    [HarmonyPatch(typeof(CharacterAttributeItemVM), "CharacterAttributeItemVM", MethodType.Constructor)]
    public class CharacterAttributeItemVMPatch
    {
        static void Postfix(ref bool ____isInSamePartyAsPlayer)
        {
            ____isInSamePartyAsPlayer = true;
        }

        static bool Prepare()
        {
            return Settings.Instance.RemoteCompanionSkillManagementEnabled;
        }
    }
}
