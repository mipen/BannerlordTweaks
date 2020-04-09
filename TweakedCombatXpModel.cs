using StoryMode.GameModels;
using System;
using TaleWorlds.CampaignSystem;

namespace BannerlordTweaks
{
    public class TweakedCombatXpModel : StoryModeCombatXpModel
    {
        public override void GetXpFromHit(CharacterObject attackerTroop, CharacterObject attackedTroop, int damage, bool isFatal, MissionTypeEnum missionType, out int xpAmount)
        {
            int baseXpAmount = 0;
            if (attackerTroop != null && attackedTroop != null)
            {
                base.GetXpFromHit(attackerTroop, attackedTroop, damage, isFatal, missionType, out baseXpAmount);

                if (!attackerTroop.IsHero)
                {
                    if (Settings.Instance.TroopBattleExperienceMultiplierEnabled && missionType == MissionTypeEnum.Battle)
                        baseXpAmount = (int)Math.Ceiling(Settings.Instance.TroopBattleExperienceMultiplier * baseXpAmount);
                    else if (Settings.Instance.TroopBattleSimulationExperienceMultiplierEnabled && missionType == MissionTypeEnum.SimulationBattle)
                        baseXpAmount = (int)Math.Ceiling(Settings.Instance.TroopBattleSimulationExperienceMultiplier * baseXpAmount);
                }
                //MessageBox.Show($"Attacker: {attackerTroop.Name}\nAttacked: {attackedTroop.Name}\nDefault xp: {baseXpAmount / Settings.Instance.TroopExperienceBattleMultiplier}\nMultiplied xp: {baseXpAmount}\nDamage:{damage}");
            }
            xpAmount = baseXpAmount;
        }
    }
}
