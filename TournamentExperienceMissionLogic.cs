using System;
using System.Windows.Forms;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CharacterDevelopment.Managers;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using BannerlordTweaks.Lib;

namespace BannerlordTweaks
{
    public class TournamentExperienceMissionLogic : MissionLogic
    {
        public override void OnScoreHit(Agent affectedAgent, Agent affectorAgent, int affectorWeaponKind, bool isBlocked, float damage, float movementSpeedDamageModifier, float hitDistance, AgentAttackType attackType, float shotDifficulty, int weaponCurrentUsageIndex)
        {
            try
            {
                //Check to see if the affector is a hero
                if (affectedAgent == null || affectedAgent.Character == null || affectorAgent == null || affectorAgent.Character == null || !affectorAgent.IsHero)
                    return;

                Hero affectorHero = ((CharacterObject)affectorAgent.Character).HeroObject;
                //If the hero is not the player or in the player's party, don't do anything
                if (affectorHero != null && (affectorHero == Hero.MainHero ||
                    affectorHero.PartyBelongedTo != null && affectorHero.PartyBelongedTo == Hero.MainHero.PartyBelongedTo))
                {
                    Hero captainHero = null;
                    Hero commanderHero = null;
                    //If the affector is the player's companion, set the commander and captain as the player.
                    if (affectorHero != Hero.MainHero)
                    {
                        CharacterObject leaderCharacter = (CharacterObject)affectorAgent.Team.Leader.Character;
                        if (leaderCharacter.HeroObject == Hero.MainHero)
                        {
                            captainHero = Hero.MainHero;
                            commanderHero = Hero.MainHero;
                        }
                    }
                    float hitPointRatio = (Math.Min(damage, affectedAgent.HealthLimit) / affectedAgent.HealthLimit) * 0.5f;
                    bool isTeamKill = affectorAgent.Team == affectedAgent.Team;
                    bool isFatal = affectedAgent.Health <= 0;

                    SkillLevelingManager.OnCombatHit(affectorAgent.Character as CharacterObject, affectedAgent.Character as CharacterObject,
                        captainHero, commanderHero, movementSpeedDamageModifier, shotDifficulty, affectorWeaponKind, hitPointRatio, false, affectorAgent.HasMount,
                        isTeamKill, false, weaponCurrentUsageIndex, damage, isFatal);
                    //MessageBox.Show($"Did this!\nAffector:{affectorAgent.Character.Name}\nAffected:{affectedAgent.Character.Name}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during Tournament experience logic:\n\n{ex.ToStringFull()}");
            }
        }
    }
}
