using HarmonyLib;
using TaleWorlds.Core;

namespace BannerlordTweaks.Patches
{
    [HarmonyPatch(typeof(WeaponComponentData), "CanHitMultipleTargets", MethodType.Getter)]
    public class WeaponComponentDataPatch
    {
        static void Postfix(ref bool __result, WeaponComponentData __instance)
        {
            __result = (BannerlordTweaksSettings.Instance.TwoHandedWeaponsSliceThroughEnabled && __instance.WeaponClass == WeaponClass.TwoHandedAxe || __instance.WeaponClass == WeaponClass.TwoHandedMace ||
                __instance.WeaponClass == WeaponClass.TwoHandedPolearm || __instance.WeaponClass == WeaponClass.TwoHandedSword) ||
                (BannerlordTweaksSettings.Instance.SingleHandedWeaponsSliceThroughEnabled && __instance.WeaponClass == WeaponClass.OneHandedSword ||
                __instance.WeaponClass == WeaponClass.OneHandedPolearm || __instance.WeaponClass == WeaponClass.OneHandedAxe);
        }

        static bool Prepare()
        {
            return BannerlordTweaksSettings.Instance.TwoHandedWeaponsSliceThroughEnabled || BannerlordTweaksSettings.Instance.SingleHandedWeaponsSliceThroughEnabled;
        }
    }
}
