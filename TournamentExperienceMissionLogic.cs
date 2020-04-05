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
                Hero affectorHero;

                if (!IsValidAffected(affectedAgent) || !IsValidAffector(affectorAgent, out affectorHero))
                    return;

                if (affectorHero != null)
                {
                    MessageBox.Show($"Giving experience to {affectorAgent.Character.Name} in tournament.\nAffected:{affectedAgent.Character.Name}");
                    Hero captainHero = null;
                    Hero commanderHero = null;
                    //If the affector is the player's companion, set the commander and captain as the player.
                    //if (affectorHero != Hero.MainHero)
                    //{
                    //    CharacterObject leaderCharacter = (CharacterObject)affectorAgent.Team.Leader.Character;
                    //    if (leaderCharacter.HeroObject == Hero.MainHero)
                    //    {
                    //        captainHero = Hero.MainHero;
                    //        commanderHero = Hero.MainHero;
                    //    }
                    //}
                    float hitPointRatio = (Math.Min(damage, affectedAgent.HealthLimit) / affectedAgent.HealthLimit) * 0.5f;
                    bool isTeamKill = affectorAgent.Team == affectedAgent.Team;
                    bool isFatal = affectedAgent.Health <= 0;

                    SkillLevelingManager.OnCombatHit(affectorAgent.Character as CharacterObject, affectedAgent.Character as CharacterObject,
                        captainHero, commanderHero, movementSpeedDamageModifier, shotDifficulty, affectorWeaponKind, hitPointRatio, false, affectorAgent.HasMount,
                        isTeamKill, false, weaponCurrentUsageIndex, damage, isFatal);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during Tournament experience logic:\n\n{ex.ToStringFull()}");
            }
        }

        private bool IsValidAffector(Agent affector, out Hero hero)
        {
            hero = null;

            if (affector == null) return false;
            if (affector.Character == null) return false;
            if (!affector.IsHero) return false;

            hero = ((CharacterObject)affector.Character).HeroObject;

            if (hero == null) return false;
            if (hero.Clan == null) return false;
            if (hero != Hero.MainHero || hero.Clan != Clan.PlayerClan) return false;
            if (hero.MapFaction != Hero.MainHero.MapFaction) return false;
            if (hero.PartyBelongedTo != Hero.MainHero.PartyBelongedTo) return false;
            if (hero.IsWanderer) return false;

            return true;
        }

        private bool IsValidAffected(Agent affected)
        {
            if (affected.Character == null) return false;

            return true;
        }
    }
}
