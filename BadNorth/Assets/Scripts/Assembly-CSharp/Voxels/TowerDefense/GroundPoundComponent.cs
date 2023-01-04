using System;
using System.Diagnostics;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x0200084C RID: 2124
	public class GroundPoundComponent : AgentComponent, IAttackResponder
	{
		// Token: 0x140000BF RID: 191
		// (add) Token: 0x06003787 RID: 14215 RVA: 0x000EFB94 File Offset: 0x000EDF94
		// (remove) Token: 0x06003788 RID: 14216 RVA: 0x000EFBCC File Offset: 0x000EDFCC
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action onComplete = delegate()
		{
		};

		// Token: 0x06003789 RID: 14217 RVA: 0x000EFC04 File Offset: 0x000EE004
		public override void Setup()
		{
			this.agentAnimator = base.GetComponent<Animator>();
			this.groundPoundState = new AgentState("GroundPound", base.agent.exclusives, false, true);
			this.jumpPrepareState = new AgentState("Prepare", this.groundPoundState, false, true);
			this.poundAttackState = new AgentState("Attack", this.groundPoundState, false, true);
			if (!base.agent.attackResponders.Contains(this))
			{
				base.agent.attackResponders.Add(this);
			}
			this.jumpComponent = base.gameObject.GetOrAddComponent<JumpComponent>();
			this.jumpComponent.Setup(base.agent);
		}

		// Token: 0x0600378A RID: 14218 RVA: 0x000EFCB4 File Offset: 0x000EE0B4
		public void GroundPound(GroundPoundComponent.Settings settings, NavPos landPos)
		{
			this.settings = settings;
			this.landPos = landPos;
			this.faceDir = (landPos.pos - base.transform.position).normalized;
			this.groundPoundState.active = true;
			this.jumpPrepareState.SetActive(true);
			base.agent.moveAnimate = false;
			base.agent.PlayAnimation(GroundPoundComponent.jumpAnimId);
		}

		// Token: 0x0600378B RID: 14219 RVA: 0x000EFD2C File Offset: 0x000EE12C
		public void GroundPoundJump()
		{
			if (this.settings && this.jumpComponent)
			{
				this.jumpComponent.PerformJump(this.landPos, this.settings.jumpSolver);
				this.jumpComponent.OnLanded += this.OnLand;
				this.jumpComponent.jumpingState.OnUpdate += this.OnJumpUpdate;
				AgentState jumpingState = this.jumpComponent.jumpingState;
				jumpingState.OnDeactivate = (Action)Delegate.Combine(jumpingState.OnDeactivate, new Action(this.JumpDeactivate));
				base.agent.moveAnimate = false;
				this.trail = UnityEngine.Object.Instantiate<GameObject>(ScriptableObjectSingleton<PrefabManager>.instance.jumpTrail, base.transform.position, Quaternion.identity, base.agent.faction.island.runContainer);
				FabricWrapper.PostEvent(this.settings.fabricLaunchID, base.agent.gameObject);
			}
		}

		// Token: 0x0600378C RID: 14220 RVA: 0x000EFE38 File Offset: 0x000EE238
		private void Update()
		{
			if (this.poundAttackState.active && this.agentAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
			{
				base.agent.brain.brainState.active = true;
				base.agent.moveAnimate = true;
			}
			if (this.groundPoundState.active)
			{
				this.UpdateLookAt();
			}
		}

		// Token: 0x0600378D RID: 14221 RVA: 0x000EFEAC File Offset: 0x000EE2AC
		private void OnLand()
		{
			this.jumpComponent.OnLanded -= this.OnLand;
			this.jumpComponent.jumpingState.OnUpdate -= this.OnJumpUpdate;
			AgentState jumpingState = this.jumpComponent.jumpingState;
			jumpingState.OnDeactivate = (Action)Delegate.Remove(jumpingState.OnDeactivate, new Action(this.JumpDeactivate));
			Vector3 forward = base.agent.transform.forward;
			this.agentAnimator.Play(GroundPoundComponent.attackAnimId);
			this.agentAnimator.Update(0f);
			base.agent.moveAnimate = false;
			FabricWrapper.PostEvent(this.settings.fabricLandID, base.agent.gameObject);
			this.groundPoundState.active = true;
			this.poundAttackState.active = true;
			ExplosionHelpers.CreateExplosion(this.landPos.pos, this.landPos.GetNormal(), forward, this.settings.explosionDefinition, base.agent.gameObject, base.agent);
		}

		// Token: 0x0600378E RID: 14222 RVA: 0x000EFFC1 File Offset: 0x000EE3C1
		private void UpdateLookAt()
		{
			base.agent.LookInDirection(this.faceDir, 720f, 0f);
		}

		// Token: 0x0600378F RID: 14223 RVA: 0x000EFFE0 File Offset: 0x000EE3E0
		private void OnJumpUpdate()
		{
			if (this.jumpComponent.jumpingState.active)
			{
				this.UpdateLookAt();
				this.trail.transform.position = base.transform.TransformPoint(Vector3.up * 0.3f);
			}
		}

		// Token: 0x06003790 RID: 14224 RVA: 0x000F0032 File Offset: 0x000EE432
		private void JumpDeactivate()
		{
			this.trail = null;
			this.onComplete();
		}

		// Token: 0x06003791 RID: 14225 RVA: 0x000F0046 File Offset: 0x000EE446
		void IAttackResponder.ModifyAttack(ref Attack attack)
		{
			attack.ignore |= this.groundPoundState.active;
		}

		// Token: 0x040025C8 RID: 9672
		private GroundPoundComponent.Settings settings;

		// Token: 0x040025C9 RID: 9673
		private NavPos landPos;

		// Token: 0x040025CA RID: 9674
		private Vector3 faceDir;

		// Token: 0x040025CB RID: 9675
		private GameObject trail;

		// Token: 0x040025CC RID: 9676
		private JumpComponent jumpComponent;

		// Token: 0x040025CD RID: 9677
		private AgentState groundPoundState;

		// Token: 0x040025CE RID: 9678
		private AgentState jumpPrepareState;

		// Token: 0x040025CF RID: 9679
		private AgentState poundAttackState;

		// Token: 0x040025D0 RID: 9680
		private Animator agentAnimator;

		// Token: 0x040025D1 RID: 9681
		private static readonly int attackAnimId = Animator.StringToHash("Ground_Pound_Attack");

		// Token: 0x040025D2 RID: 9682
		private static readonly int jumpAnimId = Animator.StringToHash("Ground_Pound_Jump");

		// Token: 0x0200084D RID: 2125
		[Serializable]
		public struct Settings
		{
			// Token: 0x06003794 RID: 14228 RVA: 0x000F0082 File Offset: 0x000EE482
			public static implicit operator bool(GroundPoundComponent.Settings settings)
			{
				return settings.jumpSolver != null && settings.fabricLaunchID != null && settings.fabricLandID != null;
			}

			// Token: 0x040025D5 RID: 9685
			public ExplosionDef explosionDefinition;

			// Token: 0x040025D6 RID: 9686
			[NonSerialized]
			public FabricEventReference fabricLaunchID;

			// Token: 0x040025D7 RID: 9687
			[NonSerialized]
			public FabricEventReference fabricLandID;

			// Token: 0x040025D8 RID: 9688
			public IProjectileSolver jumpSolver;
		}
	}
}
