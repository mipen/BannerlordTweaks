using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Map;
using TaleWorlds.Library;

namespace BannerlordTweaks
{
    public class TweakedCombatXpModel : DefaultCombatXpModel
    {
        public override void GetXpFromHit(CharacterObject attackerTroop, CharacterObject attackedTroop, PartyBase party, int damage, bool isFatal, MissionTypeEnum missionType, out int xpAmount)
        {
            if (attackerTroop == null || attackedTroop == null)
            {
                xpAmount = 0;
                return;
            }
            int num = attackerTroop.MaxHitPoints();
            xpAmount = MBMath.Round(0.4f * ((attackedTroop.GetPower() + 0.5f) * (float)(Math.Min(damage, num) + (isFatal ? num : 0))));
            //There are three things to do here: Tournament Experience, Arena Experience, Troop Experience.
            if (attackerTroop.IsHero)
            {
                if (missionType == MissionTypeEnum.Tournament)
                {
                    if (Settings.Instance.TournamentHeroExperienceMultiplierEnabled)
                        xpAmount = (int)MathF.Round(Settings.Instance.TournamentHeroExperienceMultiplier * (float)xpAmount);
                    else
                        xpAmount = MathF.Round((float)xpAmount * 0.25f);
                }
                else if (missionType == MissionTypeEnum.PracticeFight)
                {
                    if (Settings.Instance.ArenaHeroExperienceMultiplierEnabled)
                        xpAmount = (int)MathF.Round(Settings.Instance.ArenaHeroExperienceMultiplier * (float)xpAmount);
                    else
                        xpAmount = MathF.Round((float)xpAmount * 0.0625f);
                }
            }
            else if ((missionType == MissionTypeEnum.Battle || missionType == MissionTypeEnum.SimulationBattle))
            {
                if (Settings.Instance.TroopBattleSimulationExperienceMultiplierEnabled && missionType == MissionTypeEnum.SimulationBattle)
                    xpAmount = (int)MathF.Round(xpAmount * Settings.Instance.TroopBattleSimulationExperienceMultiplier);
                else if (Settings.Instance.TroopBattleExperienceMultiplierEnabled && missionType == MissionTypeEnum.Battle)
                    xpAmount = (int)MathF.Round(xpAmount * Settings.Instance.TroopBattleExperienceMultiplier);
            }
        }
    }
}
