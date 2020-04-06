using HarmonyLib;
using System.Reflection;
using TaleWorlds.CampaignSystem;

namespace BannerlordTweaks.Patches
{
    [HarmonyPatch]
    public class LordConversationsCampaignBehaviorPatch
    {
        static MethodBase TargetMethod()
        {
            return AccessTools.Method("SandBox.LordConversationsCampaignBehavior:conversation_player_want_to_end_service_as_mercenary_on_condition");
        }

        static void Postfix(ref bool __result)
        {
            __result = Hero.MainHero.MapFaction == Hero.OneToOneConversationHero.MapFaction && Hero.OneToOneConversationHero.Clan != Hero.MainHero.Clan && Hero.MainHero.Clan.IsUnderMercenaryService;
        }
    }
}
