using HarmonyLib;
using System;
using TaleWorlds.Localization;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Map;

// Courtest of Ruihan - Since it costs 0 influence to raise cohesion on all-clan-party armies, even if low-morale or starving, so why make them click and add cohesion?

// TO DO: Figure out why patch keeps crapping out on Ref ExplainedNumber - cohesionChange causes null ref, and __cohesionChange results in Harmony error.

namespace BannerlordTweaks.Patches
{
    [HarmonyPatch(typeof(DefaultArmyManagementCalculationModel), "CalculateCohesionChangeInternal")]
    public class CalculateCohesionChangeInternalPatch
    {
        static bool Prefix(Army army, ref ExplainedNumber __cohesionChange)
        {
            int num1 = 0;
            foreach (MobileParty party in army.Parties)
            {
                if (party.LeaderHero.Clan != army.LeaderParty.LeaderHero.Clan)
                {
                    num1++;
                }
            }
            if (num1 > 0)
            {
                return true;
            }
            else
            {
                __cohesionChange.Add(2.00f, new TextObject("Clan-Only Armies Suffer No Cohesion Loss"));
                return false;
            }
        }

        static bool Prepare() => BannerlordTweaksSettings.Instance is { } settings && settings.ClanArmyLosesNoCohesionEnabled;
        
        /*
        static bool Prepare()
        {
            if (BannerlordTweaksSettings.Instance is { } settings)
            {
                if (settings.ClanArmyLosesNoCohesionEnabled)
                    return true;
            }
            return false;
        }
        
        static bool Prepare()
        {
            return BannerlordTweaksSettings.Instance.ClanArmyLosesNoCohesionEnabled;
        }
        */
    }
}
