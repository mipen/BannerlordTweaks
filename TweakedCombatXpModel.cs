using StoryMode.GameModels;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Library;

namespace BannerlordTweaks
{
    public class TweakedCombatXpModel : StoryModeCombatXpModel
    {
        public override void GetXpFromHit(CharacterObject attackerTroop, CharacterObject attackedTroop, int damage, bool isFatal, MissionTypeEnum missionType, out int xpAmount)
        {
            int num = attackerTroop.MaxHitPoints();
            xpAmount = MBMath.Round(0.4f * ((attackedTroop.GetPower() + 0.5f) * (float)(Math.Min(damage, num) + (isFatal ? num : 0))));
            //There are three things to do here: Tournament Experience, Arena Experience, Troop Experience.
            if (attackerTroop.IsHero)
            {
                if (missionType == MissionTypeEnum.Tournament)
                {
                    if (Settings.Instance.TournamentHeroExperienceMultiplierEnabled)
                        xpAmount = (int)Math.Round(Settings.Instance.TournamentHeroExperienceMultiplier * (float)xpAmount);
                    else
                        xpAmount = MathF.Round((float)xpAmount * 0.25f);
                }
                else if (missionType == MissionTypeEnum.PracticeFight)
                {
                    if (Settings.Instance.ArenaHeroExperienceMultiplierEnabled)
                        xpAmount = (int)Math.Round(Settings.Instance.ArenaHeroExperienceMultiplier * (float)xpAmount);
                    else
                        xpAmount = MathF.Round((float)xpAmount * 0.0625f);
                }
            }
            else if ((missionType == MissionTypeEnum.Battle || missionType == MissionTypeEnum.SimulationBattle))
            {
                if (Settings.Instance.TroopBattleSimulationExperienceMultiplierEnabled && missionType == MissionTypeEnum.SimulationBattle)
                    xpAmount = (int)Math.Round(xpAmount * Settings.Instance.TroopBattleSimulationExperienceMultiplier);
                else if (missionType == MissionTypeEnum.SimulationBattle)
                {
                    xpAmount *= 8;
                }
                else if (Settings.Instance.TroopBattleExperienceMultiplierEnabled && missionType == MissionTypeEnum.Battle)
                    xpAmount = (int)Math.Round(xpAmount * Settings.Instance.TroopBattleExperienceMultiplier);
            }
        }
    }
}
