using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace BannerlordTweaks
{
    public class DismembermentMissionBehaviour : MissionBehaviour
    {
        public override MissionBehaviourType BehaviourType => MissionBehaviourType.Other;

        public override void OnRegisterBlow(Agent attacker, Agent victim, GameEntity realHitEntity, Blow b, ref AttackCollisionData collisionData)
        {
            if (attacker == null || victim == null) return;
            if (attacker == victim) return;

            if (IsValidAttacker(attacker, out bool isPlayerAttacker) && IsValidVictim(victim))
            {
                var attackDir = attacker.AttackDirection;
                //Wanted to do arms as well, but currently not possible. Will have to wait until we can add custom meshes and skeletons
                if (collisionData.VictimHitBodyPart == BoneBodyPartType.Head && (attackDir == Agent.UsageDirection.AttackRight ||
                    attackDir == Agent.UsageDirection.AttackLeft) && b.DamageType == DamageTypes.Cut)
                    DismemberAgent(victim, attacker, BodyPart.Head, isPlayerAttacker);
                //else if (attackDir == Agent.UsageDirection.AttackRight && (collisionData.VictimHitBodyPart == BoneBodyPartType.BipedalArmLeft || collisionData.VictimHitBodyPart == BoneBodyPartType.ShoulderLeft))
                //    DismemberAgent(victim, BodyPart.LeftArm);
                //else if (attackDir == Agent.UsageDirection.AttackLeft && (collisionData.VictimHitBodyPart == BoneBodyPartType.ShoulderRight || collisionData.VictimHitBodyPart == BoneBodyPartType.BipedalArmRight))
                //    DismemberAgent(victim, BodyPart.RightArm);
            }
            else if (IsValidAttacker(attacker, out isPlayerAttacker) && victim.Health <= 0 && victim.Character == null)
            {
                DismemberAgent(victim, attacker, BodyPart.Head, false);
            }
        }

        private void DismemberAgent(Agent victim, Agent attacker, BodyPart bodyPart, bool isPlayerAttacker)
        {
            AgentBuildData bodyPartData = new AgentBuildData(victim.Character);
            bodyPartData.NoWeapons(true);
            bodyPartData.NoHorses(true);
            bodyPartData.Team(victim.Team);
            bodyPartData.TroopOrigin(victim.Origin);
            bodyPartData.InitialFrame(new MatrixFrame(victim.Frame.rotation, victim.Frame.origin));

            Agent bodyPartAgent = Mission.SpawnAgent(bodyPartData);
            SetSkeletonParts(bodyPartAgent.AgentVisuals.GetSkeleton(), true);
            SetSkeletonParts(victim.AgentVisuals.GetSkeleton(), false);
            MakeDead(bodyPartAgent, attacker);
            bodyPartAgent.AgentVisuals.GetEntity().ActivateRagdoll();
            //TODO:: Add blood spurt
            //if (isPlayerAttacker && Settings.Instance.ReportPlayerDecapitatedSomeoneEnabled)
            //    ReportDismemberment(victim, bodyPart);
        }

        private static void MakeDead(Agent victim, Agent attacker)
        {
            Blow blow = new Blow();
            blow.AttackType = AgentAttackType.Standard;
            blow.DamageType = DamageTypes.Cut;
            blow.StrikeType = StrikeType.Swing;
            blow.InflictedDamage = 1000;
            blow.BoneIndex = victim.Monster.HeadLookDirectionBoneIndex;
            blow.BaseMagnitude = 1000;
            blow.Position = victim.Position;
            blow.Position.z = victim.GetEyeGlobalHeight();
            blow.WeaponRecord.FillWith(null, -1, -1);
            blow.SwingDirection = new Vec3(attacker.MovementVelocity * -1, 0, -1);
            blow.SwingDirection.Normalize();
            blow.Direction = blow.SwingDirection;
            blow.DamageCalculated = true;
            victim.RegisterBlow(blow);
        }

        private static void SetSkeletonParts(Skeleton skeleton, bool invert = false)
        {
            //Non-inverted hides the head. Inverted hides everything else.
            foreach (var mesh in skeleton.GetAllMeshes())
            {
                bool flag = mesh.Name.Contains("head") || mesh.Name.Contains("beard") || mesh.Name.Contains("hair") || mesh.Name.Contains("eyebrow") ||
                    mesh.Name.Contains("helmet") || mesh.Name.Contains("_cap_");
                if (invert) flag = !flag;
                if (flag)
                {
                    mesh.SetVisibilityMask(VisibilityMaskFlags.EditModeAny);
                    mesh.ClearMesh();
                }
            }
        }

        //int num = 0;
        //private void PrintMeshInfo(Skeleton s)
        //{
        //    string folderpath = @"C:\Users\Liam\Desktop\Meshes\";
        //    if (!Directory.Exists(folderpath))
        //        Directory.CreateDirectory(folderpath);
        //    string filepath = System.IO.Path.Combine(folderpath, $"{s.GetName()} {num++}.txt");
        //    using (var stream = new StreamWriter(filepath))
        //    {
        //        stream.WriteLine(s.GetName());
        //        stream.WriteLine();
        //        foreach (var mesh in s.GetAllMeshes())
        //        {
        //            stream.WriteLine(mesh.Name);
        //        }
        //        stream.Close();
        //    }
        //}

        private bool IsValidAttacker(Agent attacker, out bool isPlayerAttacker)
        {
            isPlayerAttacker = false;
            if (attacker == Agent.Main)
            {
                isPlayerAttacker = true;
                return true;
            }
            //if (Settings.Instance.AICanDecapitate)
            return true;
            //return false;
        }

        private bool IsValidVictim(Agent victim)
        {
            if (victim.Health <= 0 && victim.Character != null)
            {
                if (victim.Character is CharacterObject && (victim.Character as CharacterObject).HeroObject != null)
                    return false;
                return true;
            }
            return false;
        }

        private static void ReportDismemberment(Agent victim, BodyPart bodyPart)
        {
            //string word = bodyPart == BodyPart.Head ? "Decapitated" : "Dismembered";
            InformationManager.DisplayMessage(new InformationMessage($"Decapitated {victim.Character.Name}!", Colors.Red));
        }

        private enum BodyPart
        {
            Head,
            LeftArm,
            RightArm,
        }
    }
}
