using System;
using System.Diagnostics;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x0200088D RID: 2189
	public class ThrowComponent : AgentComponent, IBrainAction
	{
		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x0600394D RID: 14669 RVA: 0x000FACA8 File Offset: 0x000F90A8
		public bool isPreparingThrow
		{
			get
			{
				return this.throwable != null;
			}
		}

		// Token: 0x140000C2 RID: 194
		// (add) Token: 0x0600394E RID: 14670 RVA: 0x000FACB8 File Offset: 0x000F90B8
		// (remove) Token: 0x0600394F RID: 14671 RVA: 0x000FACF0 File Offset: 0x000F90F0
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action onThrowRelease = delegate()
		{
		};

		// Token: 0x140000C3 RID: 195
		// (add) Token: 0x06003950 RID: 14672 RVA: 0x000FAD28 File Offset: 0x000F9128
		// (remove) Token: 0x06003951 RID: 14673 RVA: 0x000FAD60 File Offset: 0x000F9160
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action onThrowComplete = delegate()
		{
		};

		// Token: 0x06003952 RID: 14674 RVA: 0x000FAD98 File Offset: 0x000F9198
		public override void Setup()
		{
			base.Setup();
			this.throwing = new AgentState("ThrowingSomething", base.agent.brain.actingState, false, true);
			AgentState agentState = this.throwing;
			agentState.OnChange = (Action<bool>)Delegate.Combine(agentState.OnChange, new Action<bool>(base.agent.SetMoveAnimateInverse));
			this.throwing.OnUpdate += delegate()
			{
				base.agent.LookInDirection(this.worldSpaceTarget - base.agent.wPos, 720f, 20f);
				if (base.agent.animationDone || !this.isPreparingThrow)
				{
					this.throwing.SetActive(false);
					this.onThrowComplete();
				}
			};
			AgentState agentState2 = this.throwing;
			agentState2.OnActivate = (Action)Delegate.Combine(agentState2.OnActivate, new Action(delegate()
			{
				base.agent.SetDirection(this.worldSpaceTarget - base.agent.wPos);
				base.agent.PlayAnimation(ThrowComponent.throwID);
				base.agent.onThrow = new Action(this.Throw);
				base.agent.onStartThrow = new Action(this.SpawnTorch);
			}));
			AgentState agentState3 = this.throwing;
			agentState3.OnDeactivate = (Action)Delegate.Combine(agentState3.OnDeactivate, new Action(delegate()
			{
				this.Drop();
			}));
		}

		// Token: 0x06003953 RID: 14675 RVA: 0x000FAE5E File Offset: 0x000F925E
		public void BeginThrowing(IThrowable throwable, Vector3 worldSpaceTarget, FabricEventReference audioOverride = null)
		{
			this.throwable = throwable;
			this.worldSpaceTarget = worldSpaceTarget;
			this.audioOverride = audioOverride;
			throwable.AttachTo(this.attachmentTransform);
			throwable.SetVisible(false);
		}

		// Token: 0x06003954 RID: 14676 RVA: 0x000FAE88 File Offset: 0x000F9288
		private void SpawnTorch()
		{
			if (this.isPreparingThrow)
			{
				this.throwable.SetVisible(true);
			}
		}

		// Token: 0x06003955 RID: 14677 RVA: 0x000FAEA4 File Offset: 0x000F92A4
		private void Throw()
		{
			if (this.isPreparingThrow)
			{
				this.throwable.ThrowAt(this.worldSpaceTarget);
				this.throwable = null;
				this.onThrowRelease();
				IslandGameplayManager.RequestCombatAudio((this.audioOverride == null) ? this.defaultThrowSound : this.audioOverride, base.agent.gameObject);
			}
		}

		// Token: 0x06003956 RID: 14678 RVA: 0x000FAF0B File Offset: 0x000F930B
		public void Drop()
		{
			if (this.isPreparingThrow)
			{
				this.throwable.Drop();
				this.throwable = null;
				this.onThrowRelease();
				this.onThrowComplete();
			}
		}

		// Token: 0x06003957 RID: 14679 RVA: 0x000FAF40 File Offset: 0x000F9340
		bool IBrainAction.MaybeAct(Brain swordsman)
		{
			if (this.throwable != null)
			{
				this.throwing.SetActive(true);
				base.agent.SetDirection(this.worldSpaceTarget - base.agent.wPos);
				base.agent.PlayAnimation(ThrowComponent.throwID);
				return true;
			}
			return false;
		}

		// Token: 0x0400275B RID: 10075
		private static int throwID = Animator.StringToHash("Throw");

		// Token: 0x0400275C RID: 10076
		[SerializeField]
		private Transform attachmentTransform;

		// Token: 0x0400275D RID: 10077
		private FabricEventReference defaultThrowSound = "Sfx/Torch/Throw";

		// Token: 0x0400275E RID: 10078
		public AgentState throwing;

		// Token: 0x0400275F RID: 10079
		private IThrowable throwable;

		// Token: 0x04002760 RID: 10080
		private Vector3 worldSpaceTarget = Vector3.zero;

		// Token: 0x04002761 RID: 10081
		private FabricEventReference audioOverride;
	}
}
