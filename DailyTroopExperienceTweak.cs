using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;

namespace BannerlordTweaks
{
    public class DailyTroopExperienceTweak
    {
        public static void Apply(Campaign campaign)
        {
            var obj = new DailyTroopExperienceTweak();
            CampaignEvents.DailyTickPartyEvent.AddNonSerializedListener(obj, (MobileParty mp) => { obj.DailyTick(mp); });
        }

        private void DailyTick(MobileParty party)
        {
            if (party.LeaderHero != null)
            {
                int count = party.MemberRoster.Troops.Count();
                if (party.LeaderHero == Hero.MainHero || Settings.Instance.DailyTroopExperienceApplyToAllNPC ||
                    Settings.Instance.DailyTroopExperienceApplyToPlayerClanMembers && party.LeaderHero.Clan == Clan.PlayerClan)
                {
                    int experienceAmount = ExperienceAmount(party.LeaderHero);
                    foreach (var troop in party.MemberRoster.Troops)
                    {
                        party.MemberRoster.AddXpToTroop(experienceAmount, troop);
                    }

                    if (Settings.Instance.DisplayMessageDailyExperienceGain)
                    {
                        string troops = count == 1 ? "soldier" : "troops";
                        //Debug
                        //InformationManager.DisplayMessage(new InformationMessage($"Granted {experienceAmount} experience to {count} {troops}."));
                        if (party.LeaderHero == Hero.MainHero)
                            InformationManager.DisplayMessage(new InformationMessage($"Granted {experienceAmount} experience to {count} {troops}."));
                    }
                }
            }
        }

        private int ExperienceAmount(Hero h)
        {
            return Convert.ToInt32(Settings.Instance.LeadershipPercentageForDailyExperienceGain * h.GetSkillValue(DefaultSkills.Leadership));
        }
    }
}
