using ModLib;
using System;
using System.Windows.Forms;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;
using TaleWorlds.Localization;

namespace BannerlordTweaks
{
    public class TweakedSettlementMilitiaModel : DefaultSettlementMilitiaModel
    {
        public override float CalculateMilitiaChange(Settlement settlement, StatExplainer explanation = null)
        {
            if (settlement == null) throw new ArgumentNullException(nameof(settlement));
            float baseVal = base.CalculateMilitiaChange(settlement, explanation);
            ExplainedNumber en = new ExplainedNumber(0f, explanation);
            en.Add(baseVal);
            try
            {
                if (Settings.Instance.SettlementMilitiaBonusEnabled)
                {
                    if (settlement.IsCastle)
                        en.Add(Settings.Instance.CastleMilitiaBonus, new TextObject("Recruitment drive"));
                    if (settlement.IsTown)
                        en.Add(Settings.Instance.TownMilitiaBonus, new TextObject("Citizen militia"));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred in TweakedSettlementMilitiaModule:\n\n{ex.ToStringFull()}");
            }
            return en.ResultNumber;
        }

        public override void CalculateMilitiaSpawnRate(Settlement settlement, out float meleeTroopRate, out float rangedTroopRate, out float meleeEliteTroopRate, out float rangedEliteTroopRate)
        {
            base.CalculateMilitiaSpawnRate(settlement, out meleeTroopRate, out rangedTroopRate, out float _meleeEliteTroopRate, out float _rangedEliteTroopRate);

            if (Settings.Instance.SettlementMilitiaEliteSpawnRateBonusEnabled)
            {
                _meleeEliteTroopRate += Settings.Instance.SettlementEliteMeleeSpawnRateBonus;
                _rangedEliteTroopRate += Settings.Instance.SettlementEliteRangedSpawnRateBonus;
            }
            meleeEliteTroopRate = _meleeEliteTroopRate;
            rangedEliteTroopRate = _rangedEliteTroopRate;
        }
    }
}
