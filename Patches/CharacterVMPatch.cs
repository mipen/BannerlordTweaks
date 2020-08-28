using HarmonyLib;
using TaleWorlds.CampaignSystem.ViewModelCollection.CharacterDeveloper;

namespace BannerlordTweaks.Patches
{
    [HarmonyPatch(typeof(CharacterVM), "CanAddFocusToSkillWithFocusAmount")]
    public class CharacterVMPatch
    {
        static bool Prefix(int ____unspentCharacterPoints, int currentFocusAmount, ref bool __result)
        {
            __result = currentFocusAmount < 5 && ____unspentCharacterPoints > 0;
            return false;
        }

        static bool Prepare()
        {
            return BannerlordTweaksSettings.Instance.RemoteCompanionSkillManagementEnabled;
        }
    }
}
