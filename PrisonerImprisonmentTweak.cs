using System;
using System.Linq;
using System.Reflection;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;

// 1.5.5 Update - DailyHeroTick is now private, and escape behaviors have changed. The Tweak needs to be reviewed.

namespace BannerlordTweaks
{
    public static class PrisonerImprisonmentTweak
    {
        public static void Apply(Campaign campaign)
        {
            if (campaign == null) throw new ArgumentNullException(nameof(campaign));
            var escapeBehaviour = campaign.GetCampaignBehavior<PrisonerEscapeCampaignBehavior>();
            if (escapeBehaviour != null && CampaignEvents.DailyTickHeroEvent != null)
            {
                CampaignEvents.DailyTickHeroEvent.ClearListeners(escapeBehaviour);
                CampaignEvents.DailyTickHeroEvent.AddNonSerializedListener(escapeBehaviour, (Hero h) => { Check(escapeBehaviour, h); });
            }
        }

        private static void Check(PrisonerEscapeCampaignBehavior escapeBehaviour, Hero hero)
         {
            if (escapeBehaviour == null || !(BannerlordTweaksSettings.Instance is { } settings) || !hero.IsPrisoner) return;

            if (hero.PartyBelongedToAsPrisoner != null && hero.PartyBelongedToAsPrisoner.MapFaction != null)
            {
                bool flag = hero.PartyBelongedToAsPrisoner.MapFaction == Hero.MainHero.MapFaction || (hero.PartyBelongedToAsPrisoner.IsSettlement && hero.PartyBelongedToAsPrisoner.Settlement.OwnerClan == Clan.PlayerClan);
                
                if ((settings.PrisonerImprisonmentPlayerOnly && flag) || (settings.PrisonerImprisonmentPlayerOnly == false && (Kingdom.All.Contains(hero.PartyBelongedToAsPrisoner.MapFaction) || hero.PartyBelongedToAsPrisoner.IsSettlement)) )
                    flag = true;

                if (flag == true)
                {
                    //If the party doesn't have enough healthy soldiers, is starving, is at peace with prisoners faction, or if imprisoned long enough, allow to attempt to escape.
                    if ( (hero.PartyBelongedToAsPrisoner.NumberOfHealthyMembers < hero.PartyBelongedToAsPrisoner.NumberOfPrisoners && !hero.PartyBelongedToAsPrisoner.IsSettlement) ||
                        hero.PartyBelongedToAsPrisoner.IsStarving ||
                        (hero.MapFaction != null && FactionManager.IsNeutralWithFaction(hero.MapFaction, hero.PartyBelongedToAsPrisoner.MapFaction)) ||
                        (int)hero.CaptivityStartTime.ElapsedDaysUntilNow > settings.MinimumDaysOfImprisonment)
                    {
                        //DebugHelpers.DebugMessage("Prisoner Tweak DailyHeroTick: [" + hero.Name + "] Escape Conditions met. Allow Escape Attempt.");
                        typeof(PrisonerEscapeCampaignBehavior).GetMethod("DailyHeroTick", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(escapeBehaviour, new object[] { hero });
                        return;
                    }
                    //DebugHelpers.DebugMessage("Prisoner Tweak DailyHeroTick: [" + hero.Name + "] Escape conditions not met. No Escape attempt.");
                    return;
                }
                
                else
                {
                    //DebugHelpers.DebugMessage("Prisoner Tweak DailyHeroTick: [" + hero.Name + "] Tweak Flag is false. Allow Escape Attmpt.");
                    typeof(PrisonerEscapeCampaignBehavior).GetMethod("DailyHeroTick", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(escapeBehaviour, new object[] { hero });
                }                
                return;

            }
            else
            {
                //DebugHelpers.DebugMessage("Prisoner Tweak DailyHeroTick: [" + hero.Name + "] Else Condition met. Operate as normal, allow escape attempt.");
                typeof(PrisonerEscapeCampaignBehavior).GetMethod("DailyHeroTick", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(escapeBehaviour, new object[] { hero });
            }
        }

        public static void DailyTick()
        {
            DebugHelpers.DebugMessage("Respawn Fix : Triggered Daily Tick");
            foreach (Hero hero in Hero.All)
            {
                if (hero == null) return;
                if (hero.PartyBelongedToAsPrisoner == null && hero.IsPrisoner && hero.IsAlive && !hero.IsActive && !hero.IsNotSpawned && !hero.IsReleased)
                {
                    Hero.CharacterStates heroState = hero.HeroState;

                    float days = hero.CaptivityStartTime.ElapsedDaysUntilNow;
                    if ( BannerlordTweaksSettings.Instance is { } settings && (days > (settings.MinimumDaysOfImprisonment + 3)) )
                    {
                        DebugHelpers.ColorGreenMessage("Releasing " + hero.Name + " due to Missing Hero Bug. (" + (int)days + " days)");
                        DebugHelpers.QuickInformationMessage("Releasing " + hero.Name + " due to Missing Hero Bug. (" + (int)days + " days)");
                        EndCaptivityAction.ApplyByReleasing(hero);
                    }

                    DebugHelpers.DebugMessage("Tracking Hero for possible bug: " + hero.Name + " | State: " + heroState + " | Loc: " + hero.LastSeenPlace + " | Captivity days: " + (int)days);
                }
            }
        }
    }
}
