using HarmonyLib;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ViewModelCollection.CharacterDeveloper;
using TaleWorlds.Core;

namespace BannerlordTweaks.Patches
{
    [HarmonyPatch(typeof(CharacterAttributeItemVM), MethodType.Constructor)]
    [HarmonyPatch(new Type[] { typeof(Hero), typeof(CharacterAttributesEnum), typeof(CharacterVM), typeof(Action<CharacterAttributeItemVM>), typeof(Action<CharacterAttributeItemVM>) })]
    public class CharacterAttributeItemVMPatch
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
