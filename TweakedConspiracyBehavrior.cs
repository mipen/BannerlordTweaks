using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using StoryMode.StoryModePhases;

namespace BannerlordTweaks
{
	//internal class TweakedConspiracyBehavior : CampaignBehaviorBase
	public class ConspiracyQuestTimerTweak
	{
		/*
		public override void RegisterEvents()
		{
			CampaignEvents.DailyTickEvent.AddNonSerializedListener(this, new Action(this.ExtendDeadline));
		}
		*/

		public static void Apply(Campaign campaign)
		{
			var obj = new ConspiracyQuestTimerTweak();
			CampaignEvents.DailyTickEvent.AddNonSerializedListener(obj, new Action(obj.ExtendDeadline));
		}

		private void ExtendDeadline()
		{
			if (Campaign.Current != null && Campaign.Current.QuestManager != null)
			{
				foreach (QuestBase questBase in Campaign.Current.QuestManager.Quests)
				{
					bool flag2 = questBase.GetName().ToString().StartsWith("stop_conspiracy_") && questBase.QuestDueTime < CampaignTime.DaysFromNow(5f);
					if (flag2)
					{
						DebugHelpers.ColorGreenMessage("Extending Stop the Conspiracy quest by 1 year.");
						questBase.ChangeQuestDueTime(CampaignTime.YearsFromNow(1f));
						DebugHelpers.ColorGreenMessage("New quest deadline: " + questBase.QuestDueTime.ToString());
					}
					//bool flag3 = questBase.GetName().ToString().StartsWith("conspiacy_quest_") && questBase.QuestDueTime < CampaignTime.DaysFromNow(5f);
					bool flag3 = questBase.StringId.StartsWith("conspiracy_quest_") && questBase.QuestDueTime < CampaignTime.DaysFromNow(7f);
					if (flag3)
                    {
						questBase.ChangeQuestDueTime(CampaignTime.WeeksFromNow(3f));
						DebugHelpers.ColorGreenMessage("BT Extend Conspiracy Tweak: Extended conspiracy quest.");
						float cStrngth = SecondPhase.Instance.ConspiracyStrength;
						if (cStrngth > 1000 && cStrngth > 250)
                        {
							SecondPhase.Instance.DecreaseConspiracyStrength();
							SecondPhase.Instance.DecreaseConspiracyStrength();
							SecondPhase.Instance.DecreaseConspiracyStrength();
							SecondPhase.Instance.DecreaseConspiracyStrength();
							DebugHelpers.ColorGreenMessage("BT Extend Conspiracy Tweak: Reduced conspiracy strength.");
						}
						
					 }
				}
			}
		}

		/*
		public override void SyncData(IDataStore dataStore)
		{
		}
		*/
	}
}
