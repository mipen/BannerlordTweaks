using System;
using System.Reflection;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;
using TaleWorlds.Localization;
using TaleWorlds.Library;

namespace BannerlordTweaks
{
    public class TweakedClanTierModel : DefaultClanTierModel
    {
        // 1.5.5 - This method was changed to private. New method is "GetCompanionLimit"
        /* public override int GetCompanionLimitForTier(int clanTier)
        {
            if (BannerlordTweaksSettings.Instance.CompanionLimitTweakEnabled)
                return BannerlordTweaksSettings.Instance.CompanionBaseLimit + clanTier * BannerlordTweaksSettings.Instance.CompanionLimitBonusPerClanTier;
            else
                return base.GetCompanionLimitForTier(clanTier);
        }
        */

        public override int GetCompanionLimit(Clan clan)
        {
            if (BannerlordTweaksSettings.Instance.CompanionLimitTweakEnabled)
            {
                int clanTier = clan.Tier;

                // From the now-private GetCompanionLimitFromTier()
                // int companionLimitFromTier = clanTier + 3;
                int companionLimitFromTier = BannerlordTweaksSettings.Instance.CompanionLimitBonusPerClanTier;

                ExplainedNumber explainedNumber = new ExplainedNumber((float)companionLimitFromTier, null);
                if (clan.Leader.GetPerkValue(DefaultPerks.Leadership.WePledgeOurSwords))
                {
                    explainedNumber.Add(DefaultPerks.Leadership.WePledgeOurSwords.PrimaryBonus, null, null);
                }
                return BannerlordTweaksSettings.Instance.CompanionBaseLimit + (int)explainedNumber.ResultNumber * clanTier;
            }
            else
                return base.GetCompanionLimit(clan);
        }

        // 1.5.5 - Modified based on DefaultClanTierModel.GetPartyLimitforTier + AddPartyLimitPerkEffects - Watch these methods to make sure they don't change from patch-to-patch.
        public override int GetPartyLimitForTier(Clan clan, int clanTierToCheck)
        {
            ExplainedNumber result = new ExplainedNumber(0f, null);

            if (clan.Leader != null && clan.Leader.GetPerkValue(DefaultPerks.Leadership.TalentMagnet))
            {
                result.Add(DefaultPerks.Leadership.TalentMagnet.SecondaryBonus, DefaultPerks.Leadership.TalentMagnet.Name, null);
            }

            if (BannerlordTweaksSettings.Instance.ClanPartiesLimitTweakEnabled && clan == Clan.PlayerClan)
            {
                result.Add((float)(BannerlordTweaksSettings.Instance.BaseClanPartiesLimit + Math.Floor(clanTierToCheck * BannerlordTweaksSettings.Instance.ClanPartiesBonusPerClanTier)), null);
            }

            else if (BannerlordTweaksSettings.Instance.AIClanPartiesLimitTweakEnabled && clan.IsClan && !clan.StringId.Contains("_deserters"))
            {
                if (BannerlordTweaksSettings.Instance.AICustomSpawnPartiesLimitTweakEnabled && clan.StringId.StartsWith("cs_"))
                {
                    result.Add((float)(base.GetPartyLimitForTier(clan, clanTierToCheck) + BannerlordTweaksSettings.Instance.BaseAICustomSpawnPartiesLimit), new TextObject("BT AI Custom Spawn Parties Tweak"));
                }

                else if (clan.IsClan || (BannerlordTweaksSettings.Instance.AIMinorClanPartiesLimitTweakEnabled && clan.IsMinorFaction && !clan.StringId.StartsWith("cs_")))
                {
                    result.Add((float)(base.GetPartyLimitForTier(clan, clanTierToCheck) + BannerlordTweaksSettings.Instance.BaseAIClanPartiesLimit), new TextObject("BT AI Lord Parties Tweak"));
                }
            }

            else
                return base.GetPartyLimitForTier(clan, clanTierToCheck);

            return (int)Math.Ceiling(result.ResultNumber);
        }            
    }
}
