using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Map;
using TaleWorlds.Library;

namespace BannerlordTweaks
{
    public class TweakedCombatXpModel : DefaultCombatXpModel
    {
        // v1.4.3 - They added PartyBase param. Updated method to address build error.
        // v1.5.5 - They added Captain param.
        // ToDo: Update formula based on new code in DefaultCombatXpModel
        public override void GetXpFromHit(CharacterObject attackerTroop, CharacterObject captain, CharacterObject attackedTroop, PartyBase party, int damage, bool isFatal, MissionTypeEnum missionType, out int xpAmount)
        {
            if (attackerTroop == null || attackedTroop == null || !(BannerlordTweaksSettings.Instance is { } settings))
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
                    if (settings.TournamentHeroExperienceMultiplierEnabled)
                        xpAmount = (int)MathF.Round(settings.TournamentHeroExperienceMultiplier * (float)xpAmount);
                    else
                        xpAmount = MathF.Round((float)xpAmount * 0.33f);
                }
                else if (missionType == MissionTypeEnum.PracticeFight)
                {
                    if (settings.ArenaHeroExperienceMultiplierEnabled)
                        xpAmount = (int)MathF.Round(settings.ArenaHeroExperienceMultiplier * (float)xpAmount);
                    else
                        xpAmount = MathF.Round((float)xpAmount * 0.0625f);
                }
            }
            else if ((missionType == MissionTypeEnum.Battle || missionType == MissionTypeEnum.SimulationBattle))
            {
                if (settings.TroopBattleSimulationExperienceMultiplierEnabled && missionType == MissionTypeEnum.SimulationBattle)
                    xpAmount = (int)MathF.Round(xpAmount * settings.TroopBattleSimulationExperienceMultiplier);
                else if (settings.TroopBattleExperienceMultiplierEnabled && missionType == MissionTypeEnum.Battle)
                    xpAmount = (int)MathF.Round(xpAmount * settings.TroopBattleExperienceMultiplier);
            }
        }
    }
}
