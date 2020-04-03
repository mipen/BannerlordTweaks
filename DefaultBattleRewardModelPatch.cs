using HarmonyLib;
using Helpers;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Map;

namespace BannerlordTweaks
{
    [HarmonyPatch(typeof(DefaultBattleRewardModel), "CalculateRenownGain")]
    public class DefaultBattleRewardModelPatch
    {
        static bool Prefix(PartyBase party, float renownValueOfBattle, float contributionShare, StatExplainer explanation, ref float __result)
        {
            ExplainedNumber stat;
            if (party.LeaderHero != null && party.LeaderHero == Hero.MainHero)
                stat = new ExplainedNumber((renownValueOfBattle * contributionShare) * 2f, explanation);
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
            return false;
        }
    }
}
