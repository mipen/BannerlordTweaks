using TaleWorlds.CampaignSystem.SandBox.GameComponents;

namespace BannerlordTweaks
{
    public class TweakedCharacterDevelopmentModel : DefaultCharacterDevelopmentModel
    {
        public override int LevelsPerAttributePoint => Settings.Instance.AttributeFocusPointTweakEnabled
            ? Settings.Instance.AttributePointRequiredLevel
            : base.LevelsPerAttributePoint;

        public override int FocusPointsPerLevel => Settings.Instance.AttributeFocusPointTweakEnabled
            ? Settings.Instance.FocusPointsPerLevel
            : base.FocusPointsPerLevel;
    }
}
