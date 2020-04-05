using StoryMode.GameModels;
using System;
using TaleWorlds.CampaignSystem;

namespace BannerlordTweaks
{
    public class TweakedCombatXpModel : StoryModeCombatXpModel
    {
        public override void GetXpFromHit(CharacterObject attackerTroop, CharacterObject attackedTroop, int damage, bool isFatal, bool isSimulated, out int xpAmount)
        {
            int baseXpAmount;
            if (attackerTroop != null && attackedTroop != null)
            {
                base.GetXpFromHit(attackerTroop, attackedTroop, damage, isFatal, isSimulated, out baseXpAmount);

                if (Settings.Instance.TroopExperienceBattleMultiplierEnabled && !attackerTroop.IsHero && !isSimulated)
                {
                    baseXpAmount = (int)Math.Ceiling((Settings.Instance.TroopExperienceBattleMultiplier * baseXpAmount));
                    //MessageBox.Show($"Attacker: {attackerTroop.Name}\nAttacked: {attackedTroop.Name}\nDefault xp: {baseXpAmount / Settings.Instance.TroopExperienceBattleMultiplier}\nMultiplied xp: {baseXpAmount}\nDamage:{damage}");
                }
                else if (Settings.Instance.TroopExperienceSimulationMultiplierEnabled && !attackerTroop.IsHero && isSimulated)
                {
                    baseXpAmount = (int)Math.Ceiling((Settings.Instance.TroopExperienceSimulationMultiplier * baseXpAmount));
                }
                xpAmount = baseXpAmount;
            }
            else
                xpAmount = 0;

        }
    }
}
