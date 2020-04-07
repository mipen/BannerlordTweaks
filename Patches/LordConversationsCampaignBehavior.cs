using HarmonyLib;
using System.Reflection;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;

namespace BannerlordTweaks.Patches
{
    [HarmonyPatch]
    public class LordConversationsCampaignBehaviorPatch
    {
        static MethodBase TargetMethod()
        {
            return AccessTools.Method("SandBox.LordConversationsCampaignBehavior:conversation_player_leave_faction_accepted_on_consequence");
        }

        static bool Prefix()
        {
            if (Clan.PlayerClan.IsUnderMercenaryService)
                ChangeKingdomAction.ApplyByLeaveKingdomAsMercenaryForNoPayment(Clan.PlayerClan, Clan.PlayerClan.Kingdom, true);
            else
                ChangeKingdomAction.ApplyByLeaveKingdom(Hero.MainHero.Clan, true);
            return false;
        }

        static bool Prepare()
        {
            return false;
        }
    }
}
