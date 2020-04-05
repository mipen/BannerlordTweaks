using HarmonyLib;
using System;
using System.Windows.Forms;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;

namespace BannerlordTweaks.Patches
{
    [HarmonyPatch(typeof(Hero), "AddSkillXp")]
    public class AddSkillXpPatch
    {
        private static double GetMultiplier(int skillLevel)
        {
            if (Settings.Instance.HeroSkillExperienceGeneralMultiplier >= 1)
                return Settings.Instance.HeroSkillExperienceGeneralMultiplier;
            else
                return Math.Max(1, 0.0315769 * Math.Pow(skillLevel, 1.020743));
        }

        static bool Prefix(Hero __instance, SkillObject skill, int xpAmount)
        {
            try
            {
                HeroDeveloper hd = (HeroDeveloper)(typeof(Hero).GetField("_heroDeveloper", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(__instance));
                if (hd != null)
                {
                    double multiplier = GetMultiplier(__instance.GetSkillValue(skill));
                    int newXpAmount = (int)Math.Ceiling(xpAmount * multiplier);
                    hd.AddSkillXp(skill, newXpAmount);
                    //MessageBox.Show($"Skill: {skill.Name}\nLevel: {__instance.GetSkillValue(skill)}\nMultiplier: {multiplier}\nOld xp amount: {xpAmount}\nNew xp amount: {newXpAmount}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An exception occurred whilst trying to apply the xp multiplier.\n\nException:\n{ex.Message}\n\n{ex.InnerException?.Message}");
            }
            return false;
        }

        static bool Prepare()
        {
            return Settings.Instance.HeroSkillExperienceMultiplierEnabled;
        }
    }
}
