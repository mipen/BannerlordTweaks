﻿using HarmonyLib;
using Helpers;
using System;
using System.Windows.Forms;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Map;
using TaleWorlds.Core;

namespace BannerlordTweaks.Patches
{
    [HarmonyPatch(typeof(DefaultBattleRewardModel), "CalculateRenownGain")]
    public class DefaultBattleRewardModelPatch
    {
        static bool Prefix(PartyBase party, float renownValueOfBattle, float contributionShare, StatExplainer explanation, ref float __result)
        {
            bool patched = false;
            try
            {
                var battleRenownMultiplier = 1f;

                if (BannerlordTweaksSettings.Instance.BattleRewardApplyToAI || party.LeaderHero != null && party.LeaderHero == Hero.MainHero)
                {
                    battleRenownMultiplier = BannerlordTweaksSettings.Instance.BattleRenownMultiplier;
                }

                var stat = new ExplainedNumber((renownValueOfBattle * contributionShare) * battleRenownMultiplier, explanation);
                /* Debug to test complaint of Battle Renown multiplier not being calculated right.
                if (party.LeaderHero == Hero.MainHero)
                {
                    MessageBox.Show($"DefaultBattleRewardModelRenownPatch. renownValueOfBattle is: "+ renownValueOfBattle+"\nbattleRenownMultiplier is:" + battleRenownMultiplier + "\nexplanation:"+ explanation+"\n");
                }
                */

                //TODO:: Implement this the same as native in next game update
                if (party.IsMobile && party.MobileParty.HasPerk(DefaultPerks.Charm.ShowYourScars, false))
                {
                    PerkHelper.AddPerkBonusForParty(DefaultPerks.Charm.ShowYourScars, party.MobileParty, true, ref stat);
                    // Debug
                    // InformationManager.DisplayMessage(new InformationMessage($"DefaultBattleRewardModelRenownPatch. Show Your Scars Bonus\nHero is:"+party.LeaderHero.Name+"\nrenownValueOfBattle is: " + renownValueOfBattle + "\nbattleRenownMultiplier is:" + battleRenownMultiplier + "\nexplanation:" + explanation + "\n"));
                }

                // Replaced with L34 'todo' method for v1.5.0
                //if (party.IsMobile && party.Leader != null && party.Leader.HeroObject != null && party.LeaderHero.GetPerkValue(DefaultPerks.Charm.ShowYourScars))
                //    PerkHelper.AddPerkBonusForParty(DefaultPerks.Charm.ShowYourScars, party.MobileParty, true, ref stat);

                __result = stat.ResultNumber;
                patched = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during DefaultBattleRewardModelRenownPatch. Reverting to original behaviour...\n\nException:\n{ex.Message}\n\n{ex.InnerException?.Message}\n\n{ex.InnerException?.InnerException?.Message}");
            }

            return !patched;
        }

        static bool Prepare()
        {
            return BannerlordTweaksSettings.Instance.BattleRewardTweaksEnabled;
        }
    }

    [HarmonyPatch(typeof(DefaultBattleRewardModel), "CalculateInfluenceGain")]
    public class DefaultBattleRewardModelInfluencePatch
    {
        static bool Prefix(PartyBase party, float influenceValueOfBattle, float contributionShare, StatExplainer explanation, ref float __result)
        {
            bool patched = false;
            try
            {
                var battleInfluenceMultiplier = 1f;

                if (BannerlordTweaksSettings.Instance.BattleRewardApplyToAI || party.LeaderHero != null && party.LeaderHero == Hero.MainHero)
                {
                    battleInfluenceMultiplier = BannerlordTweaksSettings.Instance.BattleInfluenceMultiplier;
                }

                var stat = new ExplainedNumber(party.MapFaction.IsKingdomFaction ? (influenceValueOfBattle * contributionShare * battleInfluenceMultiplier) : 0f, explanation, null);

                __result = stat.ResultNumber;
                patched = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during DefaultBattleRewardModelInfluencePatch. Reverting to original behavior... \n\nException:\n{ex.Message}\n\n{ex.InnerException?.Message}\n\n{ex.InnerException?.Message}");
            }

            return !patched;
        }

        static bool Prepare()
        {
            return BannerlordTweaksSettings.Instance.BattleRewardTweaksEnabled;
        }
    }
}
