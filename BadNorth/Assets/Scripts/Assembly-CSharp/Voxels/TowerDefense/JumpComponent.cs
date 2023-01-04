using System;
using System.Diagnostics;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x02000790 RID: 1936
	public class JumpComponent : AgentComponent, IAttackResponder
	{
		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x060031F0 RID: 12784 RVA: 0x000D3718 File Offset: 0x000D1B18
		public float gravity
		{
			get
			{
				return Physics.gravity.y * 3f;
			}
		}

		// Token: 0x140000A3 RID: 163
		// (add) Token: 0x060031F1 RID: 12785 RVA: 0x000D3738 File Offset: 0x000D1B38
		// (remove) Token: 0x060031F2 RID: 12786 RVA: 0x000D3770 File Offset: 0x000D1B70
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event JumpComponent.OnLandedDelegate OnLanded = delegate()
		{
		};

		// Token: 0x060031F3 RID: 12787 RVA: 0x000D37A8 File Offset: 0x000D1BA8
		public override void Setup()
		{
			base.Setup();
			if (!this.setup)
			{
				this.setup = true;
				this.jumpingState = new AgentState("Jump", base.agent.navigationState, false, true);
				AgentState agentState = this.jumpingState;
				agentState.OnChange = (Action<bool>)Delegate.Combine(agentState.OnChange, new Action<bool>(base.SetEnabled));
				AgentState agentState2 = this.jumpingState;
				agentState2.OnChange = (Action<bool>)Delegate.Combine(agentState2.OnChange, new Action<bool>(base.agent.SetColliderInverse));
				this.jumpingState.OnUpdate += this.JumpingUpdate;
				base.enabled = false;
				if (!base.agent.attackResponders.Contains(this))
				{
					base.agent.attackResponders.Add(this);
				}
			}
		}

		// Token: 0x060031F4 RID: 12788 RVA: 0x000D3884 File Offset: 0x000D1C84
		public void PerformJump(ref NavPos targetPos, Vector3 targetSpeed, IProjectileSolver solver)
		{
			this.targetPos = targetPos;
			if (base.agent.navPos.onMain)
			{
				Singleton<DustParticles>.instance.SpawnParticles(base.agent.transform.position);
			}
			base.agent.transform.SetParent(targetPos.transform.parent);
			this.jumpMotion.Launch(base.agent.transform.position, targetPos.wPos, this.gravity, solver);
			targetPos.pos += targetSpeed * this.jumpMotion.timeRemaining;
			this.jumpMotion.Launch(base.agent.transform.position, targetPos.wPos, this.gravity, solver);
			this.jumpingState.SetActive(true);
			base.agent.moveAnimate = false;
		}

		// Token: 0x060031F5 RID: 12789 RVA: 0x000D3974 File Offset: 0x000D1D74
		public void PerformJump(NavPos targetPos, IProjectileSolver solver)
		{
			this.targetPos = targetPos;
			if (base.agent.navPos.onMain)
			{
				Singleton<DustParticles>.instance.SpawnParticles(base.agent.transform.position);
			}
			base.agent.transform.SetParent(targetPos.transform.parent);
			this.jumpMotion.Launch(base.agent.transform.position, targetPos.wPos, this.gravity, solver);
			this.jumpingState.SetActive(true);
			base.agent.moveAnimate = false;
		}

		// Token: 0x060031F6 RID: 12790 RVA: 0x000D3A18 File Offset: 0x000D1E18
		public void JumpingUpdate()
		{
			this.jumpMotion.Update();
			base.agent.transform.position = this.jumpMotion.position;
			UnityEngine.Debug.DrawRay(base.agent.transform.position, -this.jumpMotion.velocity, Color.red);
			if (this.jumpMotion.hasArrived && this.TryAttach())
			{
				this.OnLanded();
			}
		}

		// Token: 0x060031F7 RID: 12791 RVA: 0x000D3A9C File Offset: 0x000D1E9C
		private bool TryAttach()
		{
			if (base.agent.deadState.active)
			{
				return false;
			}
			base.agent.velocity = Vector3.zero;
			base.agent.navPos = this.targetPos;
			base.agent.moveAnimate = true;
			base.agent.groundedState.SetActive(true);
			base.agent.SetDirection(base.agent.transform.forward);
			base.agent.transform.position = base.agent.navPos.pos;
			base.agent.transform.rotation = Quaternion.LookRotation(Vector3.up, -base.agent.transform.forward) * Quaternion.Euler(90f, 0f, 0f);
			return true;
		}

		// Token: 0x060031F8 RID: 12792 RVA: 0x000D3B83 File Offset: 0x000D1F83
		public float GetTimeToLanding()
		{
			return this.jumpMotion.timeRemaining;
		}

		// Token: 0x060031F9 RID: 12793 RVA: 0x000D3B90 File Offset: 0x000D1F90
		void IAttackResponder.ModifyAttack(ref Attack attack)
		{
			attack.ignore |= this.jumpingState.active;
		}

		// Token: 0x040021D1 RID: 8657
		private const float gravityScale = 3f;

		// Token: 0x040021D2 RID: 8658
		private SimpleProjectile jumpMotion = new SimpleProjectile();

		// Token: 0x040021D3 RID: 8659
		public AgentState jumpingState;

		// Token: 0x040021D5 RID: 8661
		private NavPos targetPos;

		// Token: 0x040021D6 RID: 8662
		private bool setup;

		// Token: 0x02000791 RID: 1937
		// (Invoke) Token: 0x060031FC RID: 12796
		public delegate void OnLandedDelegate();
	}
}
