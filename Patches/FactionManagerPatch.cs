using HarmonyLib;
using TaleWorlds.CampaignSystem;

namespace BannerlordTweaks.Patches
{
    [HarmonyPatch(typeof(FactionManager), "RegisterCampaignWar")]
    class RegisterCampaignWarPatch
    {
        public static bool Prefix(IFaction faction1, IFaction faction2)
        {
            return faction1.StringId != faction2.StringId;
        }

        public static bool Prepare()
        {
            return Settings.Instance.FixNoDeclareWarOnSelf;
        }
    }
}
