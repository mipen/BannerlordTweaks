using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;

namespace BannerlordTweaks
{
    public class TweakedWorkshopModel : DefaultWorkshopModel
    {
        public override int GetMaxWorkshopCountForPlayer()
        {
            if (!BannerlordTweaksSettings.Instance.MaxWorkshopCountTweakEnabled)
                return base.GetMaxWorkshopCountForPlayer();
            else
                return BannerlordTweaksSettings.Instance.BaseWorkshopCount + Clan.PlayerClan.Tier * BannerlordTweaksSettings.Instance.BonusWorkshopsPerClanTier;
        }

        public override int GetBuyingCostForPlayer(Workshop workshop)
        {
            if (workshop == null) throw new ArgumentNullException(nameof(workshop));
            if (BannerlordTweaksSettings.Instance.WorkshopBuyingCostTweakEnabled)
                return workshop.WorkshopType.EquipmentCost + (int)workshop.Settlement.Prosperity / 2 + BannerlordTweaksSettings.Instance.WorkshopBaseCost;
            else
                return base.GetBuyingCostForPlayer(workshop);
        }
    }
}
