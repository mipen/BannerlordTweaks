using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;

namespace BannerlordTweaks.Patches
{
    [HarmonyPatch(typeof(DefaultAgeModel), "BecomeInfantAge", MethodType.Getter)]
    public class BecomeInfantAgePatch
    {
        private static void Postfix(ref int __result)
        {
            __result = Settings.Instance.BecomeInfantAge;
        }

        private static bool Prepare()
        {
            return Settings.Instance.DefaultAgeTweakEnabled;
        }
    }

    [HarmonyPatch(typeof(DefaultAgeModel), "BecomeChildAge", MethodType.Getter)]
    public class BecomeChildAgePatch
    {
        private static void Postfix(ref int __result)
        {
            __result = Settings.Instance.BecomeChildAge;
        }

        private static bool Prepare()
        {
            return Settings.Instance.DefaultAgeTweakEnabled;
        }
    }

    [HarmonyPatch(typeof(DefaultAgeModel), "BecomeTeenagerAge", MethodType.Getter)]
    public class BecomeTeenagerAgePatch
    {
        private static void Postfix(ref int __result)
        {
            __result = Settings.Instance.BecomeTeenagerAge;
        }

        private static bool Prepare()
        {
            return Settings.Instance.DefaultAgeTweakEnabled;
        }
    }

    [HarmonyPatch(typeof(DefaultAgeModel), "HeroComesOfAge", MethodType.Getter)]
    public class HeroComesOfAgePatch
    {
        private static void Postfix(ref int __result)
        {
            __result = Settings.Instance.HeroComesOfAge;
        }

        private static bool Prepare()
        {
            return Settings.Instance.DefaultAgeTweakEnabled;
        }
    }

    [HarmonyPatch(typeof(DefaultAgeModel), "BecomeOldAge", MethodType.Getter)]
    public class BecomeOldAgePatch
    {
        private static void Postfix(ref int __result)
        {
            __result = Settings.Instance.BecomeOldAge;
        }

        private static bool Prepare()
        {
            return Settings.Instance.DefaultAgeTweakEnabled;
        }
    }

    [HarmonyPatch(typeof(DefaultAgeModel), "MaxAge", MethodType.Getter)]
    public class MaxAgePatch
    {
        private static void Postfix(ref int __result)
        {
            __result = Settings.Instance.MaxAge;
        }

        private static bool Prepare()
        {
            return Settings.Instance.DefaultAgeTweakEnabled;
        }
    }
}
