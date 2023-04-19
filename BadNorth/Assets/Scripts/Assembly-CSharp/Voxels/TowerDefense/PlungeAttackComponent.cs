using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020007B5 RID: 1973
	public class PlungeAttackComponent : AgentComponent, IAttackResponder
	{
		// Token: 0x140000AA RID: 170
		// (add) Token: 0x06003311 RID: 13073 RVA: 0x000DAC5C File Offset: 0x000D905C
		// (remove) Token: 0x06003312 RID: 13074 RVA: 0x000DAC94 File Offset: 0x000D9094
		
		public event Action OnLanded = delegate()
		{
		};

		// Token: 0x06003313 RID: 13075 RVA: 0x000DACCC File Offset: 0x000D90CC
		public override void Setup()
		{
			base.Setup();
			this.plungeState = new AgentState("Plunge", base.agent.exclusives, false, true);
			this.plungeRunState = new AgentState("Run", this.plungeState, false, true);
			this.plungePrepareState = new AgentState("Prepare", this.plungeState, false, true);
			this.plungeAttackState = new AgentState("Attack", this.plungeState, false, true);
			this.plungeRunState.OnUpdate += delegate()
			{
				Vector3 vector = this.landPos.wPos - base.agent.navPos.pos;
				if (this.plungeRunState.timeSinceActivation > this.timer)
				{
					this.plungePrepareState.SetActive(true);
					base.agent.PlayAnimation(this.plungeJumpId, new Action(this.PlungeJump));
					base.agent.SetDirection(vector);
				}
				else
				{
					float cliffDistance = base.agent.navPos.GetCliffDistance();
					float value = Vector3.Dot(base.agent.navPos.GetCliffVector(), vector);
					base.agent.walkDir = vector * (1f - Mathf.Clamp01(value));
					base.agent.LookInDirection(vector, 720f, 5f);
				}
			};
			AgentState agentState = this.plungePrepareState;
			agentState.OnChange = (Action<bool>)Delegate.Combine(agentState.OnChange, new Action<bool>(base.agent.SetMoveAnimateInverse));
			AgentState agentState2 = this.plungeAttackState;
			agentState2.OnChange = (Action<bool>)Delegate.Combine(agentState2.OnChange, new Action<bool>(base.agent.SetMoveAnimateInverse));
			this.plungeAttackState.OnUpdate += delegate()
			{
				if (base.agent.animationDone)
				{
					Swordsman swordsman = base.agent.brain as Swordsman;
					if (!swordsman.MaybePursue())
					{
						base.agent.brain.brainState.SetActive(true);
					}
				}
			};
			AgentState agentState3 = this.plungeState;
			agentState3.OnChange = (Action<bool>)Delegate.Combine(agentState3.OnChange, new Action<bool>(base.SetEnabled));
			this.jumpComponent = base.GetComponent<JumpComponent>();
		}

		// Token: 0x06003314 RID: 13076 RVA: 0x000DAE00 File Offset: 0x000D9200
		private void PlungeJump()
		{
			this.jumpComponent.PerformJump(this.landPos, this.jumpSolver);
			this.jumpComponent.OnLanded += this.OnLand;
			this.jumpComponent.jumpingState.OnUpdate += this.OnJumpUpdate;
			AgentState jumpingState = this.jumpComponent.jumpingState;
			jumpingState.OnDeactivate = (Action)Delegate.Combine(jumpingState.OnDeactivate, new Action(this.JumpDeactivate));
			this.trail = UnityEngine.Object.Instantiate<GameObject>(ScriptableObjectSingleton<PrefabManager>.instance.jumpTrail, base.transform.position, Quaternion.identity, base.agent.faction.island.runContainer);
			IslandGameplayManager.RequestCombatAudio(this.jumpSound, base.agent.gameObject);
			this.UpdateTrail();
		}

		// Token: 0x06003315 RID: 13077 RVA: 0x000DAED9 File Offset: 0x000D92D9
		private void JumpDeactivate()
		{
			this.trail = null;
		}

		// Token: 0x06003316 RID: 13078 RVA: 0x000DAEE2 File Offset: 0x000D92E2
		private void UpdateTrail()
		{
			if (this.trail)
			{
				this.trail.transform.position = base.transform.TransformPoint(Vector3.up * 0.3f);
			}
		}

		// Token: 0x06003317 RID: 13079 RVA: 0x000DAF20 File Offset: 0x000D9320
		public void DoPlungeAttack(Vector3 jumpOffPos, NavPos landPos, Vector3 faceDirection, IProjectileSolver jumpSolver, ExplosionDef attackExplosion, FabricEventReference jumpSound, float timer)
		{
			if (!base.agent.aliveAndGrounded.active)
			{
				return;
			}
			this.timer = timer;
			this.landPos = landPos;
			this.faceDirection = faceDirection.GetZeroY().normalized;
			this.attackExplosion = attackExplosion;
			this.jumpSound = jumpSound;
			this.jumpSolver = jumpSolver;
			if (this.faceDirection.sqrMagnitude < 0.01f)
			{
				this.faceDirection = base.agent.lookDir;
			}
			this.target = null;
			this.plungeState.SetActive(true);
			this.plungeRunState.SetActive(true);
		}

		// Token: 0x06003318 RID: 13080 RVA: 0x000DAFC8 File Offset: 0x000D93C8
		private void OnJumpUpdate()
		{
			this.UpdateTrail();
			float timeToLanding = this.jumpComponent.GetTimeToLanding();
			if (timeToLanding < this.attackTargetingTime)
			{
				this.SearchForTarget();
				this.FaceTarget();
			}
		}

		// Token: 0x06003319 RID: 13081 RVA: 0x000DB000 File Offset: 0x000D9400
		private void SearchForTarget()
		{
			if (!this.attackExplosion)
			{
				return;
			}
			float damageRadius = this.attackExplosion.damageRadius;
			Vector3 wPos = this.landPos.wPos;
			List<Agent> staticListRadius = AgentEnumerators.GetStaticListRadius(wPos, damageRadius, base.agent.faction.enemy);
			Agent exists = null;
			float num = 0f;
			foreach (Agent agent in staticListRadius)
			{
				Vector3 normalized = (agent.chestPos - wPos).GetZeroY().normalized;
				float num2 = Vector3.Dot(this.faceDirection, normalized);
				if (num2 > num)
				{
					num = num2;
					exists = agent;
				}
			}
			if (exists)
			{
				this.target = exists;
			}
			Color color = (!exists) ? Color.green : Color.yellow;
			Vector3 vector = this.landPos.wPos + Vector3.up * 0.05f;
			if (this.target)
			{
				UnityEngine.Debug.DrawLine(base.agent.chestPos, this.target.chestPos, color, 0f);
			}
		}

		// Token: 0x0600331A RID: 13082 RVA: 0x000DB15C File Offset: 0x000D955C
		private void FaceTarget()
		{
			if (this.attackTargetingTime > 0f)
			{
				if (this.target)
				{
					UnityEngine.Debug.DrawLine(base.agent.transform.position, this.target.transform.position);
					this.faceDirection = (this.target.transform.position - base.agent.transform.position).GetZeroY().normalized;
				}
				base.agent.LookInDirection(this.faceDirection, 225f / this.attackTargetingTime, 5f);
			}
		}

		// Token: 0x0600331B RID: 13083 RVA: 0x000DB208 File Offset: 0x000D9608
		private void OnLand()
		{
			this.CancelAttack();
			base.gameObject.GetComponent<JumpComponent>().OnLanded -= this.OnLand;
			bool flag = base.enSquad.heroAgent == base.agent;
			FabricEventReference eventRef = (!flag) ? PlungeAttackComponent.fabricLandID : base.enSquad.hero.voice.plungeLandSound;
			Vector3 forward = base.agent.transform.forward;
			Vector3 position = base.agent.transform.position + forward * this.attackForwardPosition;
			base.agent.animator.Play(this.attackAnimId);
			base.agent.animator.Update(0f);
			base.agent.moveAnimate = false;
			Singleton<DustParticles>.instance.SpawnParticles(base.transform.position, Vector3.up);
			Singleton<DustParticles>.instance.SpawnParticles(base.transform.position, Vector3.zero);
			this.plungeAttackState.SetActive(true);
			this.jumpComponent.jumpingState.OnUpdate -= this.OnJumpUpdate;
			AgentState jumpingState = this.jumpComponent.jumpingState;
			jumpingState.OnDeactivate = (Action)Delegate.Remove(jumpingState.OnDeactivate, new Action(this.JumpDeactivate));
			IslandGameplayManager.RequestCombatAudio(eventRef, base.agent.gameObject);
			ExplosionHelpers.CreateExplosion(position, this.landPos.GetNormal(), forward, this.attackExplosion, base.agent.gameObject, base.agent);
			this.OnLanded();
		}

		// Token: 0x0600331C RID: 13084 RVA: 0x000DB3B1 File Offset: 0x000D97B1
		public void CancelAttack()
		{
			this.CancelInvoke(new Action(this.CancelAttack));
			if (base.enabled)
			{
				base.agent.brain.brainState.active = true;
			}
		}

		// Token: 0x0600331D RID: 13085 RVA: 0x000DB3E8 File Offset: 0x000D97E8
		[Conditional("UNITY_EDITOR")]
		private void DebugDrawAttack(Vector3 landPos, Vector3 attackPos)
		{
			landPos += Vector3.up * 0.05f;
			attackPos += Vector3.up * 0.05f;
			UnityEngine.Debug.DrawLine(landPos, attackPos, Color.red, 15f);
			float damageRadius = this.attackExplosion.damageRadius;
			if (this.target)
			{
				UnityEngine.Debug.DrawLine(attackPos, this.target.transform.position, Color.yellow, 15f);
				UnityEngine.Debug.DrawRay(this.target.transform.position, Vector3.up, Color.yellow, 15f);
			}
		}

		// Token: 0x0600331E RID: 13086 RVA: 0x000DB494 File Offset: 0x000D9894
		void IAttackResponder.ModifyAttack(ref Attack attack)
		{
			if (this.plungeState.active)
			{
				attack.ignore = true;
			}
		}

		// Token: 0x040022B3 RID: 8883
		[Header("Attack Properties")]
		[SerializeField]
		private float attackForwardPosition = 0.2f;

		// Token: 0x040022B4 RID: 8884
		[SerializeField]
		private float attackTargetingTime = 0.2f;

		// Token: 0x040022B5 RID: 8885
		public static readonly FabricEventReference fabricLaunchID = "Sfx/English/Jump";

		// Token: 0x040022B6 RID: 8886
		public static readonly FabricEventReference fabricLandID = "Sfx/English/Land";

		// Token: 0x040022B7 RID: 8887
		private NavPos jumpOffPos;

		// Token: 0x040022B8 RID: 8888
		private NavPos landPos;

		// Token: 0x040022B9 RID: 8889
		private Vector3 faceDirection;

		// Token: 0x040022BA RID: 8890
		private ExplosionDef attackExplosion;

		// Token: 0x040022BB RID: 8891
		private FabricEventReference jumpSound;

		// Token: 0x040022BC RID: 8892
		private IProjectileSolver jumpSolver;

		// Token: 0x040022BD RID: 8893
		private float timer;

		// Token: 0x040022BE RID: 8894
		private AgentState plungeState;

		// Token: 0x040022BF RID: 8895
		private AgentState plungeRunState;

		// Token: 0x040022C0 RID: 8896
		private AgentState plungePrepareState;

		// Token: 0x040022C1 RID: 8897
		private AgentState plungeAttackState;

		// Token: 0x040022C2 RID: 8898
		private Agent target;

		// Token: 0x040022C3 RID: 8899
		private JumpComponent jumpComponent;

		// Token: 0x040022C4 RID: 8900
		private AnimId attackAnimId = "Plunge_Attack";

		// Token: 0x040022C5 RID: 8901
		private AnimId plungeJumpId = "Plunge_Jump";

		// Token: 0x040022C6 RID: 8902
		private GameObject trail;
	}
}
