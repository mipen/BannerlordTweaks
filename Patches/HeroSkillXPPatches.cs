using HarmonyLib;
using System;
using System.Linq;
using System.Reflection;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;

namespace BannerlordTweaks.Patches
{
    [HarmonyPatch(typeof(Hero), "AddSkillXp")]
    public class AddSkillXpPatch
    {
        private static FieldInfo? hdFieldInfo = null;

        static bool Prefix(Hero __instance, SkillObject skill, float xpAmount)
        {
            try
            {

                if (hdFieldInfo == null) GetFieldInfo();

                HeroDeveloper hd = (HeroDeveloper)hdFieldInfo!.GetValue(__instance);

                if (!(BannerlordTweaksSettings.Instance is { } settings))
                    return true;

                if (hd != null)
                {
                    if (xpAmount > 0)
                    {
                        if (settings.HeroSkillExperienceMultiplierEnabled && hd.Hero.IsHumanPlayerCharacter)
                        {
                            float newXpAmount = (int)Math.Ceiling(xpAmount * settings.HeroSkillExperienceMultiplier);
                            if (settings.PerSkillBonusEnabled)
                            {
                                float PerSkillBonus = GetPerSkillBonus(skill, xpAmount);
                                xpAmount += PerSkillBonus;
                                //DebugHelpers.DebugMessage("HeroSkillXPPatch - Per-Skill Bonus Added: Player: " + hd.Hero.Name + "\nSkill is: " + skill.Name + "\nXPAmount = " + xpAmount);
                            }
                            hd.AddSkillXp(skill, newXpAmount, true, true);
                            //DebugHelpers.DebugMessage("HeroSkillXPPatch: Player: " + hd.Hero.Name+ "\nSkill is: " + skill.Name + "\nXPAmount = " + xpAmount + "\nNewXPAmount = " + newXpAmount);
                        }
                        else if (settings.CompanionSkillExperienceMultiplierEnabled && !hd.Hero.IsHumanPlayerCharacter &&
                           (hd.Hero.Clan == Hero.MainHero.Clan))
                        {
                            float newXpAmount = (int)Math.Ceiling(xpAmount * settings.CompanionSkillExperienceMultiplier);
                            if (settings.PerSkillBonusEnabled)
                            {
                                float PerSkillBonus = GetPerSkillBonus(skill, xpAmount);
                                xpAmount += PerSkillBonus;
                                //DebugHelpers.DebugMessage("HeroSkillXPPatch - Per-Skill Bonus Added: Player: " + hd.Hero.Name + "\nSkill is: " + skill.Name + "\nXPAmount = " + xpAmount);
                            }
                            hd.AddSkillXp(skill, newXpAmount, true, true);
                            //DebugHelpers.DebugMessage("HeroSkillXPPatch: Companion: " + hd.Hero.Name + " - Clan: "+ hd.Hero.Clan.Name + " - Skill is: " + skill.Name + " - XPAmount = " + xpAmount + " - NewXPAmount = " + newXpAmount);
                        }

                        else
                            hd.AddSkillXp(skill, xpAmount, true, true);
                    }
                    else
                        hd.AddSkillXp(skill, xpAmount, true, true);
                }
            }
            catch (Exception ex)
            {
                DebugHelpers.ShowError("An exception occurred while trying to apply the hero xp multiplier.", "", ex);
            }
            return false;
        }

        static bool Prepare()
        {
            if (BannerlordTweaksSettings.Instance is { } settings)
            {
                if (settings.SkillExperienceMultipliersEnabled)
                    GetFieldInfo();
                return settings.SkillExperienceMultipliersEnabled;
            }
            else return false;
        }

        private static void GetFieldInfo()
        {
            hdFieldInfo = typeof(Hero).GetField("_heroDeveloper", BindingFlags.Instance | BindingFlags.NonPublic);
        }

        private static float GetPerSkillBonus(SkillObject skill, float xpamount)
        {
            float newxpamount = xpamount;
            string skillname = skill.Name.ToString();

            if (!(BannerlordTweaksSettings.Instance is { } settings))
                return 0;


            switch (skillname)
            {
                case "Engineering":
                    return (newxpamount * settings.SkillBonusEngineering);
                case "Leadership":
                    return (newxpamount * settings.SkillBonusLeadership);
                case "Medicine":
                    return (newxpamount * settings.SkillBonusMedicine);
                case "Riding":
                    return (newxpamount * settings.SkillBonusRiding);
                case "Roguery":
                    return (newxpamount * settings.SkillBonusRoguery);
                case "Scouting":
                    return (newxpamount * settings.SkillBonusScouting);
                case "Trade":
                    return (newxpamount * settings.SkillBonusTrade);
                default:
                    //DebugHelpers.DebugMessage("GetPerSkillBonus did not find the skill: " + skillname);
                    return xpamount;
            }
        }
    }
}