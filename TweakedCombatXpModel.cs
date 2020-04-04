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

                if (Settings.Instance.TroopExperienceMultiplierEnabled && !attackerTroop.IsHero)
                    baseXpAmount = (int)Math.Ceiling((Settings.Instance.TroopExperienceMultiplier * baseXpAmount));
                //MessageBox.Show($"Attacker: {attackerTroop.Name}\nAttacked: {attackedTroop.Name}\nDefault xp: {baseXpAmount / Settings.Instance.TroopExperienceMultiplier}\nMultiplied xp: {baseXpAmount}\nDamage:{damage}");
                xpAmount = baseXpAmount;
                //MessageBox.Show($"Attacker: {attackerTroop.Name}\nAttacked: {attackedTroop.Name}\nExp amount: {xpAmount}");
            }
            else
                xpAmount = 0;

        }
    }
}
