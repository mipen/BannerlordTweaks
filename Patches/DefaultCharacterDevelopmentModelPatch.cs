using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;

namespace BannerlordTweaks.Patches
{
    [HarmonyPatch(typeof(DefaultCharacterDevelopmentModel), "get_LevelsPerAttributePoint")]
    public class DefaultCharacterDevelopmentAttributePatch
    {
        private static void Postfix(ref int __result)
        {
            __result = Settings.Instance.AttributePointRequiredLevel;
        }

        static bool Prepare()
        {
            return Settings.Instance.AttributeFocusPointTweakEnabled;
        }
    }

    [HarmonyPatch(typeof(DefaultCharacterDevelopmentModel), "get_FocusPointsPerLevel")]
    public class DefaultCharacterDevelopmentFocusPatch
    {
        private static void Postfix(ref int __result)
        {            
            __result = Settings.Instance.FocusPointsPerLevel;
        }

        static bool Prepare()
        {
            return Settings.Instance.AttributeFocusPointTweakEnabled;
        }
    }
}
