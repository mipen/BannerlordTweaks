using ModLib;
using System;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace BannerlordTweaks
{
    public class TweakedSettlementFoodModel : DefaultSettlementFoodModel
    {
        public override float CalculateTownFoodStocksChange(Town town, StatExplainer explanation = null)
        {
            if (town == null) throw new ArgumentNullException(nameof(town));
            float baseVal = base.CalculateTownFoodStocksChange(town, explanation);
            if (Settings.Instance.SettlementFoodBonusEnabled)
            {
                ExplainedNumber en = new ExplainedNumber(baseVal, explanation);
                explanation?.Lines.Remove(explanation.Lines.Last());

                if (town.IsCastle)
                    en.Add(Settings.Instance.CastleFoodBonus, new TextObject("Military rations"));
                else if (town.IsTown)
                    en.Add(Settings.Instance.TownFoodBonus, new TextObject("Citizen food drive"));

                if (Settings.Instance.SettlementProsperityFoodMalusTweakEnabled && Settings.Instance.SettlementProsperityFoodMalusDivisor != 50)
                {
                    float malus = town.Owner.Settlement.Prosperity / 50f;
                    en.Add(malus, new TextObject("shouldn't be seen!"));
                    explanation?.Lines.Remove(explanation.Lines.Last());

                    TextObject prosperityTextObj = GameTexts.FindText("str_prosperity", null);
                    var line = explanation?.Lines.Where((x) => !string.IsNullOrWhiteSpace(x.Name) && x.Name == prosperityTextObj.ToString()).FirstOrDefault();
                    if (line != null) explanation?.Lines.Remove(line);

                    malus = -town.Owner.Settlement.Prosperity / Settings.Instance.SettlementProsperityFoodMalusDivisor;
                    en.Add(malus, prosperityTextObj);
                }

                return en.ResultNumber;
            }
            else
                return baseVal;
        }
    }
}
