using System;
using System.Collections.Generic;
using UnityEngine;
using Voxels.TowerDefense.TriFlow;

namespace Voxels.TowerDefense
{
	// Token: 0x0200074F RID: 1871
	public class DisembarkAttack : AgentComponent, IBrainAction
	{
		// Token: 0x060030C4 RID: 12484 RVA: 0x000C7440 File Offset: 0x000C5840
		public override void Setup()
		{
			base.Setup();
			this.plungeState = new AgentState("Plunge", base.agent.exclusives, false, true);
			this.plungePrepareState = new AgentState("Prepare", this.plungeState, false, true);
			this.plungeAttackState = new AgentState("Attack", this.plungeState, false, true);
			AgentState agentState = this.plungeState;
			agentState.OnChange = (Action<bool>)Delegate.Combine(agentState.OnChange, new Action<bool>(base.agent.SetMoveAnimateInverse));
			this.plungeAttackState.OnUpdate += delegate()
			{
				if (base.agent.animationDone)
				{
					base.agent.brain.brainState.SetActive(true);
				}
			};
			this.pirate = base.GetComponent<Pirate>();
			Pirate pirate = this.pirate;
			pirate.onRemovedFromShip = (Action)Delegate.Combine(pirate.onRemovedFromShip, new Action(delegate()
			{
				base.agent.brain.RemoveAction(this);
			}));
			AgentState agentState2 = this.plungeState;
			agentState2.OnChange = (Action<bool>)Delegate.Combine(agentState2.OnChange, new Action<bool>(base.SetEnabled));
			this.jumpComponent = base.gameObject.GetOrAddComponent<JumpComponent>();
			this.jumpComponent.Setup();
		}

		// Token: 0x060030C5 RID: 12485 RVA: 0x000C755C File Offset: 0x000C595C
		private void PlungeJump()
		{
			Data data = base.agent.faction.enemy.presence.SampleData(this.landPos);
			if ((data.navPos.pos - base.transform.position).sqrMagnitude < 1f)
			{
				this.landPos = data.navPos;
				Vector3 vector = this.landPos.pos;
				vector = Vector3.MoveTowards(vector, base.agent.transform.position, 0.1f);
				vector -= base.agent.navPos.transform.TransformVector(base.agent.navPos.GetBorderVector()) * 0.2f;
				this.landPos.pos = vector;
				base.agent.SetDirection(this.landPos.pos - base.transform.position);
			}
			this.faceDirection += (this.landPos.pos - base.transform.position).normalized;
			this.faceDirection.Normalize();
			this.jumpComponent.PerformJump(this.landPos, ScriptableObjectSingleton<PrefabManager>.instance.disembarkProjectileSolver);
			this.jumpComponent.OnLanded += this.OnLand;
			this.jumpComponent.jumpingState.OnUpdate += this.OnJumpUpdate;
			AgentState jumpingState = this.jumpComponent.jumpingState;
			jumpingState.OnDeactivate = (Action)Delegate.Combine(jumpingState.OnDeactivate, new Action(this.JumpDeactivate));
			this.trail = UnityEngine.Object.Instantiate<GameObject>(ScriptableObjectSingleton<PrefabManager>.instance.jumpTrail, base.transform.position, Quaternion.identity, base.agent.faction.island.runContainer);
			IslandGameplayManager.RequestCombatAudio(this.fabricLaunchID, base.agent.gameObject);
			this.UpdateTrail();
		}

		// Token: 0x060030C6 RID: 12486 RVA: 0x000C7764 File Offset: 0x000C5B64
		private void JumpDeactivate()
		{
			this.trail = null;
		}

		// Token: 0x060030C7 RID: 12487 RVA: 0x000C776D File Offset: 0x000C5B6D
		private void UpdateTrail()
		{
			if (this.trail)
			{
				this.trail.transform.position = base.transform.TransformPoint(Vector3.up * 0.3f);
			}
		}

		// Token: 0x060030C8 RID: 12488 RVA: 0x000C77A9 File Offset: 0x000C5BA9
		private void OnJumpUpdate()
		{
			this.UpdateTrail();
		}

		// Token: 0x060030C9 RID: 12489 RVA: 0x000C77B4 File Offset: 0x000C5BB4
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
			IslandGameplayManager.RequestCombatAudio(this.fabricLandID, base.agent.gameObject);
			float intensity = 0.02f * base.agent.mass;
			CloseCombatBrain component = base.GetComponent<CloseCombatBrain>();
			List<Agent> staticListRadiusSorted = AgentEnumerators.GetStaticListRadiusSorted(position, 0.4f, base.agent.faction.enemy);
			if (staticListRadiusSorted.Count > 0)
			{
				Attack attack = component.GetAttack(staticListRadiusSorted[0]);
				attack.stun *= 3f;
				attack.knockback *= 1.2f;
				foreach (Agent agent in staticListRadiusSorted)
				{
					attack.pos = (agent.chestPos + base.agent.chestPos) / 2f;
					attack.direction = this.faceDirection + (agent.transform.position - base.agent.transform.position).normalized * 0.3f;
					agent.DealDamage(attack);
					attack.knockback *= 0.8f;
					attack.stun *= 0.6f;
					attack.damage *= 0.1f;
				}
			}
			Singleton<CameraShaker>.instance.ShakeOnce(intensity);
		}

		// Token: 0x060030CA RID: 12490 RVA: 0x000C7A18 File Offset: 0x000C5E18
		public void CancelAttack()
		{
			this.CancelInvoke(new Action(this.CancelAttack));
			if (base.enabled)
			{
				base.agent.brain.brainState.active = true;
			}
		}

		// Token: 0x060030CB RID: 12491 RVA: 0x000C7A50 File Offset: 0x000C5E50
		bool IBrainAction.MaybeAct(Brain swordsman)
		{
			Longship longship = this.pirate.longship;
			if (!longship)
			{
				base.agent.brain.RemoveAction(this);
				return false;
			}
			this.reason = "landed false";
			if (!longship.landed)
			{
				return false;
			}
			this.reason = "orderDist " + base.agent.orderDist;
			if (base.agent.orderDist > 0.2f)
			{
				return false;
			}
			this.reason = "targetEnemyDist " + longship.targetEnemyDist;
			if (longship.targetEnemyDist > 0.7f)
			{
				return false;
			}
			NavPos navPos = this.pirate.longship.landing.navPos;
			this.reason = "Attacked!";
			Vector3 vector = base.transform.position;
			vector += longship.targetEnemyDir;
			vector -= base.agent.navPos.transform.TransformVector(base.agent.navPos.GetBorderVector()) * 0.3f;
			vector -= this.pirate.longship.landing.dir * 0.1f;
			navPos.pos = vector;
			this.landPos = navPos;
			this.faceDirection = -this.pirate.longship.landing.dir;
			this.plungePrepareState.SetActive(true);
			base.agent.PlayAnimation(this.plungeJumpId, new Action(this.PlungeJump));
			base.agent.SetDirection(this.landPos.pos - base.transform.position);
			base.agent.brain.RemoveAction(this);
			return true;
		}

		// Token: 0x060030CC RID: 12492 RVA: 0x000C7C2F File Offset: 0x000C602F
		private void OnDrawGizmos()
		{
			if (this.landPos.valid)
			{
				Gizmos.DrawLine(base.transform.position, this.landPos.wPos);
			}
		}

		// Token: 0x04002098 RID: 8344
		[Header("Audio")]
		[SerializeField]
		private string fabricLaunchID = "Sfx/English/Jump";

		// Token: 0x04002099 RID: 8345
		[SerializeField]
		private string fabricLandID = "Sfx/English/Land";

		// Token: 0x0400209A RID: 8346
		private NavPos landPos;

		// Token: 0x0400209B RID: 8347
		private Vector3 faceDirection;

		// Token: 0x0400209C RID: 8348
		private AgentState plungeState;

		// Token: 0x0400209D RID: 8349
		private AgentState plungePrepareState;

		// Token: 0x0400209E RID: 8350
		private AgentState plungeAttackState;

		// Token: 0x0400209F RID: 8351
		[SerializeField]
		private Agent target;

		// Token: 0x040020A0 RID: 8352
		private JumpComponent jumpComponent;

		// Token: 0x040020A1 RID: 8353
		private AnimId attackAnimId = "Plunge_Attack";

		// Token: 0x040020A2 RID: 8354
		private AnimId plungeJumpId = "Plunge_Jump";

		// Token: 0x040020A3 RID: 8355
		private GameObject trail;

		// Token: 0x040020A4 RID: 8356
		private Pirate pirate;

		// Token: 0x040020A5 RID: 8357
		[SerializeField]
		private string reason = "none";
	}
}
