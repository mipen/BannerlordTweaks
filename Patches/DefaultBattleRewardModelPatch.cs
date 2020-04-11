using HarmonyLib;
using Helpers;
using System;
using System.Windows.Forms;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Map;

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
                ExplainedNumber stat;
                if (party.LeaderHero != null && party.LeaderHero == Hero.MainHero)
                    stat = new ExplainedNumber((renownValueOfBattle * contributionShare) * Settings.Instance.BattleRenownMultiplier, explanation);
                else
                    stat = new ExplainedNumber(renownValueOfBattle * contributionShare, explanation);
                if (party.IsMobile)
                {
                    if (party.MobileParty.HasPerk(DefaultPerks.TwoHanded.Notorious))
                    {
                        PerkHelper.AddPerkBonusForParty(DefaultPerks.TwoHanded.Notorious, party.MobileParty, ref stat);
                    }
                    if (party.MobileParty.HasPerk(DefaultPerks.Charm.ShowYourScars))
                    {
                        PerkHelper.AddPerkBonusForParty(DefaultPerks.Charm.ShowYourScars, party.MobileParty, ref stat);
                    }
                }
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
            return Settings.Instance.BattleRewardTweaksEnabled;
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
                ExplainedNumber stat;
                if (party != null)
                    stat = new ExplainedNumber(party.MapFaction.IsKingdomFaction ? (influenceValueOfBattle * contributionShare * Settings.Instance.BattleInfluenceMultiplier) : 0f, explanation, null);
                else
                    stat = new ExplainedNumber(party.MapFaction.IsKingdomFaction ? (influenceValueOfBattle * contributionShare) : 0f, explanation, null);

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
            return Settings.Instance.BattleRewardTweaksEnabled;
        }
    }
}
