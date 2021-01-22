using TaleWorlds.CampaignSystem.SandBox.GameComponents;

namespace BannerlordTweaks
{
    public class TweakedDifficultyModel : DefaultDifficultyModel
    {
        public override float GetDamageToFriendsMultiplier()
        {
            return BannerlordTweaksSettings.Instance.DamageToFriendsTweakEnabled ? BannerlordTweaksSettings.Instance.DamageToFriendsMultiplier : base.GetDamageToFriendsMultiplier();
        }

        public override float GetDamageToPlayerMultiplier()
        {
            return BannerlordTweaksSettings.Instance.DamageToPlayerTweakEnabled ? BannerlordTweaksSettings.Instance.DamageToPlayerMultiplier : base.GetDamageToPlayerMultiplier();
        }

        public override float GetPlayerTroopsReceivedDamageMultiplier()
        {
            return BannerlordTweaksSettings.Instance.DamageToTroopsTweakEnabled ? BannerlordTweaksSettings.Instance.DamageToTroopsMultiplier : base.GetPlayerTroopsReceivedDamageMultiplier();
        }

        public override float GetCombatAIDifficultyMultiplier()
        {
            return BannerlordTweaksSettings.Instance.CombatAIDifficultyTweakEnabled ? BannerlordTweaksSettings.Instance.CombatAIDifficultyMultiplier : base.GetCombatAIDifficultyMultiplier();
        }

        public override float GetPlayerMapMovementSpeedBonusMultiplier()
        {
            return BannerlordTweaksSettings.Instance.PlayerMapMovementSpeedBonusTweakEnabled ? BannerlordTweaksSettings.Instance.PlayerMapMovementSpeedBonusMultiplier : base.GetPlayerMapMovementSpeedBonusMultiplier();
        }
    }
}
