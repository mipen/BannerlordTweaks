using StoryMode.GameModels;
using System;
using TaleWorlds.CampaignSystem;

namespace BannerlordTweaks
{
    public class TweakedCombatXpModel : StoryModeCombatXpModel
    {
        public override void GetXpFromHit(CharacterObject attackerTroop, CharacterObject attackedTroop, int damage, bool isFatal, bool isSimulated, out int xpAmount)
        {
            int baseXpAmount = 0;
            if (attackerTroop != null && attackedTroop != null)
            {
                base.GetXpFromHit(attackerTroop, attackedTroop, damage, isFatal, isSimulated, out baseXpAmount);

                if (!attackerTroop.IsHero)
                {
                    if (Settings.Instance.TroopBattleExperienceMultiplierEnabled && !isSimulated)
                        baseXpAmount = (int)Math.Ceiling(Settings.Instance.TroopBattleExperienceMultiplier * baseXpAmount);
                    else if (Settings.Instance.TroopBattleSimulationExperienceMultiplierEnabled && isSimulated)
                        baseXpAmount = (int)Math.Ceiling(Settings.Instance.TroopBattleSimulationExperienceMultiplier * baseXpAmount);
                }
                xpAmount = baseXpAmount;
                //MessageBox.Show($"Attacker: {attackerTroop.Name}\nAttacked: {attackedTroop.Name}\nDefault xp: {baseXpAmount / Settings.Instance.TroopExperienceBattleMultiplier}\nMultiplied xp: {baseXpAmount}\nDamage:{damage}");
            }
            else
                xpAmount = baseXpAmount;
        }
    }
}
