using HarmonyLib;
using System;
using System.Runtime.CompilerServices;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Barterables;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Party;
using TaleWorlds.Core;
using TaleWorlds.Localization;
using TaleWorlds.Library;
using System.Text.RegularExpressions;

namespace BannerlordTweaks.Patches
{
    [HarmonyPatch(typeof(JoinKingdomAsClanBarterable), "GetUnitValueForFaction")]
    public class BarterablePatches
    {
        static void Postfix(ref int __result, IFaction factionForEvaluation)
        {
            if (BannerlordTweaksSettings.Instance is null) return;

            Hero leader = factionForEvaluation.Leader;

            // Don't let Faction Leaders Defect from their Own Factions
            if (leader == null || leader.IsFactionLeader) return;

            int num = BannerlordTweaksSettings.Instance.BarterablesJoinKingdomAsClanAdjustment;
            if (BannerlordTweaksSettings.Instance.BarterablesTweaksEnabled)
            {
                __result = (int)__result * num;
            }
            if (BannerlordTweaksSettings.Instance.BarterablesJoinKingdomAsClanAltFormulaEnabled)
            {
                //int original_result = __result;
                __result /= 10;

                int relations = Hero.MainHero.GetRelation(leader);
                if (relations > 100) relations = 99;

                double percent = Math.Abs(((double)(relations) / 100) - 1);

                // Make it very expensive to try to lure a Lord w/ negative relations.
                double num2 = (relations > -1) ? (__result * percent) : (__result * percent) * 100;

                __result = (int)(Math.Round(num2));
                //DebugHelpers.DebugMessage("Relations = " + relations + " | Original = " + original_result + " | adjusted = " + percent + " | num2 =" + num2 + " | Result = " + __result);
                //DebugHelpers.DebugMessage("Leader: "+leader.Name+" | MapFaction: "+leader.MapFaction.Name+" | MapFaction Leader: "+leader.MapFaction.Leader.Name);
            }
        }

        static bool Prepare => BannerlordTweaksSettings.Instance is { } settings && settings.BarterablesTweaksEnabled;
    }
}
