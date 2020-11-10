using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;

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
						InformationManager.DisplayMessage(new InformationMessage("Extending Stop the Conspiracy quest by 1 year."));
						questBase.ChangeQuestDueTime(CampaignTime.YearsFromNow(1f));
						InformationManager.DisplayMessage(new InformationMessage("New quest deadline: " + questBase.QuestDueTime.ToString()));
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
