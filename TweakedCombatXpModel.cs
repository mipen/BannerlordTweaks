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

                if (!attackerTroop.IsHero)
                {
                    baseXpAmount = (int)Math.Ceiling(Settings.Instance.TroopBattleExperienceMultiplier * baseXpAmount);

                    if (Settings.Instance.TroopBattleSimulationAdditionalExperienceMultiplierEnabled && isSimulated)
                        baseXpAmount = (int)Math.Ceiling(Settings.Instance.TroopBattleSimulationAdditionalExperienceMultiplier * baseXpAmount);
                }
                xpAmount = baseXpAmount;
                //MessageBox.Show($"Attacker: {attackerTroop.Name}\nAttacked: {attackedTroop.Name}\nDefault xp: {baseXpAmount / Settings.Instance.TroopExperienceBattleMultiplier}\nMultiplied xp: {baseXpAmount}\nDamage:{damage}");
            }
            else
                xpAmount = 0;

        }
    }
}
