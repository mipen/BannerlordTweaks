using TaleWorlds.CampaignSystem.SandBox.GameComponents;

namespace BannerlordTweaks
{
    public class TweakedDifficultyModel : DefaultDifficultyModel
    {
        public override float GetDamageToFriendsMultiplier()
        {
            return Settings.Instance.DamageToFriendsTweakEnabled ? Settings.Instance.DamageToFriendsMultiplier : base.GetDamageToFriendsMultiplier();
        }

        public override float GetDamageToPlayerMultiplier()
        {
            return Settings.Instance.DamageToPlayerTweakEnabled ? Settings.Instance.DamageToPlayerMultiplier : base.GetDamageToPlayerMultiplier();
        }

        public override float GetPlayerTroopsReceivedDamageMultiplier()
        {
            return Settings.Instance.DamageToTroopsTweakEnabled ? Settings.Instance.DamageToTroopsMultiplier : base.GetPlayerTroopsReceivedDamageMultiplier();
        }

        public override float GetCombatAIDifficultyMultiplier()
        {
            return Settings.Instance.CombatAIDifficultyTweakEnabled ? Settings.Instance.CombatAIDifficultyMultiplier : base.GetCombatAIDifficultyMultiplier();
        }

        public override float GetPlayerMapMovementSpeedBonusMultiplier()
        {
            return Settings.Instance.PlayerMapMovementSpeedBonusTweakEnabled ? Settings.Instance.PlayerMapMovementSpeedBonusMultiplier : base.GetPlayerMapMovementSpeedBonusMultiplier();
        }
    }
}
