using System;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;

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
            if (escapeBehaviour == null) return;

            if (hero.IsPrisoner && hero.PartyBelongedToAsPrisoner != null && hero.PartyBelongedToAsPrisoner.MapFaction != null)
            {
                bool flag = hero.PartyBelongedToAsPrisoner.MapFaction == Hero.MainHero.MapFaction || (hero.PartyBelongedToAsPrisoner.IsSettlement && hero.PartyBelongedToAsPrisoner.Settlement.OwnerClan == Clan.PlayerClan);
                if (!BannerlordTweaksSettings.Instance.PrisonerImprisonmentPlayerOnly)
                    flag = flag || Kingdom.All.Contains(hero.PartyBelongedToAsPrisoner.MapFaction) || (hero.PartyBelongedToAsPrisoner.IsSettlement);

                if (flag)
                {
                    //If the party doesn't have enough healthy soldiers or is starving or is at peace with prisoners faction, allow to attempt to escape.
                    if (hero.PartyBelongedToAsPrisoner.NumberOfHealthyMembers < hero.PartyBelongedToAsPrisoner.NumberOfPrisoners ||
                        hero.PartyBelongedToAsPrisoner.IsStarving ||
                        (hero.MapFaction != null && FactionManager.IsNeutralWithFaction(hero.MapFaction, hero.PartyBelongedToAsPrisoner.MapFaction)) ||
                        (int)hero.CaptivityStartTime.ElapsedDaysUntilNow > BannerlordTweaksSettings.Instance.MinimumDaysOfImprisonment)
                    {
                        escapeBehaviour.DailyHeroTick(hero);
                    }
                }
                else
                    escapeBehaviour.DailyHeroTick(hero);
            }
            else
                escapeBehaviour.DailyHeroTick(hero);
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
                    if (days > (BannerlordTweaksSettings.Instance.MinimumDaysOfImprisonment + 3))
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
