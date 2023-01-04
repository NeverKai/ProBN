using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x0200069E RID: 1694
	public class JumpAttack : AgentComponent, IBrainAction, IThreat
	{
		// Token: 0x06002BB1 RID: 11185 RVA: 0x000A0618 File Offset: 0x0009EA18
		public override void Setup()
		{
			base.Setup();
			this.plungeState = new AgentState("Plunge", base.agent.exclusives, false, true);
			this.plungePrepareState = new AgentState("Prepare", this.plungeState, false, true);
			this.plungeAttackState = new AgentState("Attack", this.plungeState, false, true);
			AgentState agentState = this.plungeState;
			agentState.OnChange = (Action<bool>)Delegate.Combine(agentState.OnChange, new Action<bool>(base.agent.SetMoveAnimateInverse));
			Swordsman swordsman = base.agent.brain as Swordsman;
			this.plungeAttackState.OnUpdate += delegate()
			{
				if (this.agent.animationDone && !swordsman.MaybePursue())
				{
					this.agent.brain.brainState.SetActive(true);
				}
			};
			this.plungePrepareState.OnUpdate += delegate()
			{
				if (this.agent.enemyAgent && this.agent.enemyDist < 1.6f)
				{
					this.target = this.agent.enemyAgent;
				}
				if (this.target && (this.target.wPos - this.agent.wPos).sqrMagnitude < 2.5600002f && this.target.navPos.valid)
				{
					this.landPos = this.target.navPos;
					this.target.rangeWorry.PoseThreat(this);
				}
			};
			AgentState agentState2 = this.plungeState;
			agentState2.OnChange = (Action<bool>)Delegate.Combine(agentState2.OnChange, new Action<bool>(base.SetEnabled));
			this.plungeState.OnUpdate += delegate()
			{
				this.agent.enemyMovability = 0.1f;
			};
			this.jumpComponent = base.gameObject.GetOrAddComponent<JumpComponent>();
			this.jumpComponent.Setup();
			AgentState pursuing = swordsman.pursuing;
			pursuing.OnActivate = (Action)Delegate.Combine(pursuing.OnActivate, new Action(delegate()
			{
				Agent agent = (!swordsman.target) ? this.agent.enemyAgent : swordsman.target;
				if (!this.CanJumpAttack(agent))
				{
					return;
				}
				foreach (Agent agent2 in this.agent.squad.livingAgents)
				{
					JumpAttack component = agent2.GetComponent<JumpAttack>();
					if (component)
					{
						if (component.target == agent && component.plungePrepareState.timeSinceActivation < 0.1f)
						{
							return;
						}
					}
				}
				this.Attack(agent);
			}));
		}

		// Token: 0x06002BB2 RID: 11186 RVA: 0x000A077C File Offset: 0x0009EB7C
		private void PlungeJump()
		{
			this.faceDirection += (this.landPos.pos - base.transform.position).normalized;
			this.faceDirection.Normalize();
			this.landPos.wPos = Vector3.MoveTowards(this.landPos.wPos, base.agent.navPos.wPos, base.agent.radius);
			this.jumpComponent.PerformJump(this.landPos, ScriptableObjectSingleton<PrefabManager>.instance.disembarkProjectileSolver);
			this.jumpComponent.OnLanded += this.OnLand;
			this.jumpComponent.jumpingState.OnUpdate += this.OnJumpUpdate;
			AgentState jumpingState = this.jumpComponent.jumpingState;
			jumpingState.OnDeactivate = (Action)Delegate.Combine(jumpingState.OnDeactivate, new Action(this.JumpDeactivate));
			this.trail = UnityEngine.Object.Instantiate<GameObject>(ScriptableObjectSingleton<PrefabManager>.instance.jumpTrail, base.transform.position, Quaternion.identity, base.agent.faction.island.runContainer);
			IslandGameplayManager.RequestCombatAudio(this.fabricLaunchID, base.agent.gameObject);
			this.UpdateTrail();
		}

		// Token: 0x06002BB3 RID: 11187 RVA: 0x000A08CE File Offset: 0x0009ECCE
		private void JumpDeactivate()
		{
			this.trail = null;
		}

		// Token: 0x06002BB4 RID: 11188 RVA: 0x000A08D7 File Offset: 0x0009ECD7
		private void UpdateTrail()
		{
			if (this.trail)
			{
				this.trail.transform.position = base.transform.TransformPoint(Vector3.up * 0.3f);
			}
		}

		// Token: 0x06002BB5 RID: 11189 RVA: 0x000A0913 File Offset: 0x0009ED13
		private void OnJumpUpdate()
		{
			this.UpdateTrail();
		}

		// Token: 0x06002BB6 RID: 11190 RVA: 0x000A091C File Offset: 0x0009ED1C
		private void OnLand()
		{
			this.CancelAttack();
			this.jumpComponent.OnLanded -= this.OnLand;
			Vector3 position = base.agent.chestPos + base.agent.transform.forward * 0.1f;
			base.agent.PlayAnimation(this.attackAnimId);
			base.agent.animator.Update(0f);
			this.plungeAttackState.SetActive(true);
			this.jumpComponent.jumpingState.OnUpdate -= this.OnJumpUpdate;
			AgentState jumpingState = this.jumpComponent.jumpingState;
			jumpingState.OnDeactivate = (Action)Delegate.Remove(jumpingState.OnDeactivate, new Action(this.JumpDeactivate));
			float intensity = 0.02f * base.agent.mass;
			CloseCombatBrain component = base.GetComponent<CloseCombatBrain>();
			List<Agent> staticListRadiusSorted = AgentEnumerators.GetStaticListRadiusSorted(position, 0.4f, base.agent.faction.enemy);
			if (staticListRadiusSorted.Count > 0)
			{
				this.hitShield = false;
				Attack attack = component.GetAttack(staticListRadiusSorted[0]);
				attack.settings = this.attackSettings;
				attack.soundPrefix = string.Empty;
				attack.monoAttacker = this;
				foreach (Agent agent in staticListRadiusSorted)
				{
					attack.pos = (agent.chestPos + base.agent.chestPos) / 2f;
					attack.direction = this.faceDirection + (agent.transform.position - base.agent.transform.position).normalized * 0.3f;
					agent.DealDamage(attack);
					attack.knockback *= 0.65f;
					attack.stun *= 0.8f;
					attack.damage *= 0.25f;
				}
			}
			FabricEventReference eventRef = (!this.hitShield) ? ((staticListRadiusSorted.Count <= 0) ? this.fabricLandMissID : this.fabricLandHitID) : this.fabricLandShieldID;
			IslandGameplayManager.RequestCombatAudio(eventRef, base.agent.gameObject);
			this.hitShield = false;
			this.target = null;
			Singleton<CameraShaker>.instance.ShakeOnce(intensity);
		}

		// Token: 0x06002BB7 RID: 11191 RVA: 0x000A0BC0 File Offset: 0x0009EFC0
		public void CancelAttack()
		{
			this.CancelInvoke(new Action(this.CancelAttack));
			if (base.enabled)
			{
				base.agent.brain.brainState.active = true;
			}
		}

		// Token: 0x06002BB8 RID: 11192 RVA: 0x000A0BF5 File Offset: 0x0009EFF5
		bool IBrainAction.MaybeAct(Brain swordsman)
		{
			return this.MaybeAttack(base.agent.enemyAgent);
		}

		// Token: 0x06002BB9 RID: 11193 RVA: 0x000A0C08 File Offset: 0x0009F008
		private bool CanJumpAttack(Agent possibleTarget)
		{
			return possibleTarget && (base.agent.transform.position - possibleTarget.transform.position).magnitude >= 0.32000002f && possibleTarget.navPos.valid && this.plungeState.timeSinceDeactivation >= 2f && base.agent.enemyDist <= 1.6f;
		}

		// Token: 0x06002BBA RID: 11194 RVA: 0x000A0C97 File Offset: 0x0009F097
		private bool MaybeAttack(Agent possibleTarget)
		{
			if (this.CanJumpAttack(possibleTarget))
			{
				this.Attack(possibleTarget);
				return true;
			}
			return false;
		}

		// Token: 0x06002BBB RID: 11195 RVA: 0x000A0CB0 File Offset: 0x0009F0B0
		private void Attack(Agent possibleTarget)
		{
			this.landPos = possibleTarget.navPos;
			possibleTarget.rangeWorry.PoseThreat(this);
			this.plungePrepareState.SetActive(true);
			base.agent.PlayAnimation(this.plungeJumpId, new Action(this.PlungeJump));
			base.agent.SetDirection(this.landPos.pos - base.transform.position);
		}

		// Token: 0x06002BBC RID: 11196 RVA: 0x000A0D2C File Offset: 0x0009F12C
		private void OnDrawGizmos()
		{
			if (this.landPos.valid)
			{
				Gizmos.DrawLine(base.transform.position, this.landPos.wPos);
			}
		}

		// Token: 0x06002BBD RID: 11197 RVA: 0x000A0D59 File Offset: 0x0009F159
		Vector3 IThreat.GetPos(Agent victim)
		{
			return base.agent.wPos;
		}

		// Token: 0x06002BBE RID: 11198 RVA: 0x000A0D68 File Offset: 0x0009F168
		float IThreat.GetThreatDistance(Agent victim)
		{
			return (base.agent.wPos - victim.wPos).sqrMagnitude;
		}

		// Token: 0x06002BBF RID: 11199 RVA: 0x000A0D94 File Offset: 0x0009F194
		Vector3 IThreat.GetThreatDir(Agent victim)
		{
			return (base.agent.wPos - victim.wPos).normalized;
		}

		// Token: 0x06002BC0 RID: 11200 RVA: 0x000A0DBF File Offset: 0x0009F1BF
		bool IThreat.GetTreatValid(Agent victim)
		{
			return this.jumpComponent.jumpingState.active || this.plungeState.active;
		}

		// Token: 0x04001C80 RID: 7296
		[Header("Audio")]
		[SerializeField]
		private FabricEventReference fabricLaunchID = "Sfx/English/Jump";

		// Token: 0x04001C81 RID: 7297
		[SerializeField]
		private FabricEventReference fabricLandHitID = "Sfx/English/Land";

		// Token: 0x04001C82 RID: 7298
		[SerializeField]
		private FabricEventReference fabricLandMissID = "Sfx/English/Land";

		// Token: 0x04001C83 RID: 7299
		[SerializeField]
		private FabricEventReference fabricLandShieldID = "Sfx/English/Land";

		// Token: 0x04001C84 RID: 7300
		private NavPos landPos;

		// Token: 0x04001C85 RID: 7301
		private Vector3 faceDirection;

		// Token: 0x04001C86 RID: 7302
		private AgentState plungeState;

		// Token: 0x04001C87 RID: 7303
		private AgentState plungePrepareState;

		// Token: 0x04001C88 RID: 7304
		private AgentState plungeAttackState;

		// Token: 0x04001C89 RID: 7305
		[SerializeField]
		private AttackSettings attackSettings = new AttackSettings(3f, 2.5f, 0f, 2.5f);

		// Token: 0x04001C8A RID: 7306
		[SerializeField]
		private Agent target;

		// Token: 0x04001C8B RID: 7307
		private JumpComponent jumpComponent;

		// Token: 0x04001C8C RID: 7308
		private AnimId attackAnimId = "Plunge_Attack";

		// Token: 0x04001C8D RID: 7309
		private AnimId plungeJumpId = "Plunge_Jump";

		// Token: 0x04001C8E RID: 7310
		[NonSerialized]
		public bool hitShield;

		// Token: 0x04001C8F RID: 7311
		private GameObject trail;

		// Token: 0x04001C90 RID: 7312
		private const float distance = 1.6f;

		// Token: 0x04001C91 RID: 7313
		private const float sqDistance = 2.5600002f;
	}
}
