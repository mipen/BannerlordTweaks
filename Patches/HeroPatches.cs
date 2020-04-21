using HarmonyLib;
using ModLib.Debugging;
using System;
using System.Reflection;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement;
using TaleWorlds.Core;

namespace BannerlordTweaks.Patches
{
    [HarmonyPatch(typeof(Hero), "AddSkillXp")]
    public class AddSkillXpPatch
    {
        private static FieldInfo hdFieldInfo = null;

        //private static float GetMultiplier()
        //{
        // if (Settings.Instance.HeroSkillExperienceOverrideMultiplierEnabled)
        //return Settings.Instance.HeroSkillExperienceMultiplier;
        //else
        //    return Math.Max(1, 0.0315769 * Math.Pow(skillLevel, 1.020743));
        //}

        static bool Prefix(Hero __instance, SkillObject skill, float xpAmount)
        {
            try
            {
                if (hdFieldInfo == null) GetFieldInfo();

                HeroDeveloper hd = (HeroDeveloper)hdFieldInfo.GetValue(__instance);

                if (hd != null)
                {
                    if (xpAmount > 0)
                    {
                        float newXpAmount = (int)Math.Ceiling(xpAmount * Settings.Instance.HeroSkillExperienceMultiplier);
                        hd.AddSkillXp(skill, newXpAmount, true, true);
                    }
                    else
                        hd.AddSkillXp(skill, xpAmount, true, true);
                }
            }
            catch (Exception ex)
            {
                ModDebug.ShowError($"An exception occurred whilst trying to apply the hero xp multiplier.", "", ex);
            }
            return false;
        }

        static bool Prepare()
        {
            if (Settings.Instance.HeroSkillExperienceMultiplierEnabled)
                GetFieldInfo();
            return Settings.Instance.HeroSkillExperienceMultiplierEnabled;
        }

        private static void GetFieldInfo()
        {
            hdFieldInfo = typeof(Hero).GetField("_heroDeveloper", BindingFlags.Instance | BindingFlags.NonPublic);
        }
    }
}
