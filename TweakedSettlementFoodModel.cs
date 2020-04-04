using System;
using System.Linq;
using System.Windows.Forms;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;
using TaleWorlds.Localization;
using BannerlordTweaks.Lib;

namespace BannerlordTweaks
{
    public class TweakedSettlementFoodModel : DefaultSettlementFoodModel
    {
        public override float CalculateTownFoodStocksChange(Town town, StatExplainer explanation = null)
        {
            float baseVal = base.CalculateTownFoodStocksChange(town, explanation);
            if (Settings.Instance.SettlementFoodBonusEnabled)
            {
                try
                {
                    ExplainedNumber en = new ExplainedNumber(baseVal, explanation);
                    explanation?.Lines.Remove(explanation.Lines.Last());

                    if (town.IsCastle)
                        en.Add(Settings.Instance.CastleFoodBonus, new TextObject("Military rations"));
                    else if (town.IsTown)
                        en.Add(Settings.Instance.TownFoodBonus, new TextObject("Citizen food drive"));

                    return en.ResultNumber;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred in TweakedSettlementFoodModel: {ex.ToStringFull()}");
                    return baseVal;
                }
            }
            else
                return baseVal;
        }
    }
}
