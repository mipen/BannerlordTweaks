using HarmonyLib;
using System;
using System.Reflection;
using TaleWorlds.Localization;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Map;

// Courtest of Ruihan - Since it costs 0 influence to raise cohesion on all-clan-party armies, even if low-morale or starving, so why make them click and add cohesion?


namespace BannerlordTweaks.Patches
{

    /* Option to run it as a replacement model
    public class TweakedDefaultArmyManagementCalculationModel : DefaultArmyManagementCalculationModel
    {
        public override float CalculateCohesionChange(Army army, StatExplainer? explanation = null)
        {
            int num1 = 0;
            ExplainedNumber explainedNumber = new ExplainedNumber(-2f, explanation, null);

            foreach (MobileParty party in army.Parties)
            {
                if (party.LeaderHero.Clan != army.LeaderParty.LeaderHero.Clan)
                {
                    num1++;
                }
            }
            
            if (num1 > 0)
            {
                // make this a reflection call
                typeof(DefaultArmyManagementCalculationModel).GetMethod("CalculateCohesionChangeInternal", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(this, new object[] { army, explainedNumber });
                //base.CalculateCohesionChangeInternal(army, ref explainedNumber);
            }
            else
            {
                explainedNumber.Add(2f, new TextObject("Clan-Only Armies Suffer No Cohesion Loss"), null);
            }
            return explainedNumber.ResultNumber;
        }
    }
    */

    [HarmonyPatch(typeof(DefaultArmyManagementCalculationModel), "CalculateCohesionChange")]
    public class CalculateCohesionChangePatch
    {

        static bool Prefix(Army army, ref float __result, StatExplainer? explanation = null)
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
                if (explanation is null) explanation = new StatExplainer();
                explanation.AddLine("Clan-Only Armies Suffer No Cohesion Loss", 0);
                ExplainedNumber explainedNumber = new ExplainedNumber(0f, explanation);
                __result = explainedNumber.ResultNumber;
                return false;
            }
        }

        static bool Prepare() => BannerlordTweaksSettings.Instance is { } settings && settings.ClanArmyLosesNoCohesionEnabled;
    }
}
