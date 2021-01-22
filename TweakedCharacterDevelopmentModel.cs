using TaleWorlds.CampaignSystem.SandBox.GameComponents;

namespace BannerlordTweaks
{
    public class TweakedCharacterDevelopmentModel : DefaultCharacterDevelopmentModel
    {
        public override int LevelsPerAttributePoint => BannerlordTweaksSettings.Instance.AttributeFocusPointTweakEnabled
            ? BannerlordTweaksSettings.Instance.AttributePointRequiredLevel
            : base.LevelsPerAttributePoint;

        public override int FocusPointsPerLevel => BannerlordTweaksSettings.Instance.AttributeFocusPointTweakEnabled
            ? BannerlordTweaksSettings.Instance.FocusPointsPerLevel
            : base.FocusPointsPerLevel;
    }
}
