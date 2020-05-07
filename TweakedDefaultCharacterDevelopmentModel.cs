using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;

namespace BannerlordTweaks
{
    public class TweakedDefaultCharacterDevelopmentModel : DefaultCharacterDevelopmentModel
    {
        public override int LevelsPerAttributePoint => Settings.Instance.AttributeFocusPointTweakEnabled
            ? Settings.Instance.AttributePointRequiredLevel
            : base.LevelsPerAttributePoint;

        public override int FocusPointsPerLevel => Settings.Instance.AttributeFocusPointTweakEnabled
            ? Settings.Instance.FocusPointsPerLevel
            : base.FocusPointsPerLevel;
    }
}
