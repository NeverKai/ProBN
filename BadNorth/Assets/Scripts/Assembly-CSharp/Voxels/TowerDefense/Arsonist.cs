using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x0200068D RID: 1677
	public class Arsonist : AgentComponent, IBrainAction, IAgentOrder
	{
		// Token: 0x06002ACF RID: 10959 RVA: 0x00098EF8 File Offset: 0x000972F8
		public override void Setup()
		{
			base.Setup();
			this.throwing = new AgentState("ThrowingTorch", base.agent.brain.actingState, false, true);
			AgentState agentState = this.throwing;
			agentState.OnChange = (Action<bool>)Delegate.Combine(agentState.OnChange, new Action<bool>(base.agent.SetMoveAnimateInverse));
			this.throwing.OnUpdate += delegate()
			{
				base.agent.LookInDirection(this.target.worldTargetPos - base.agent.wPos, 720f, 20f);
				if (base.agent.animationDone)
				{
					this.torch = null;
					this.throwing.SetActive(false);
				}
			};
			AgentState agentState2 = this.throwing;
			agentState2.OnActivate = (Action)Delegate.Combine(agentState2.OnActivate, new Action(delegate()
			{
				base.agent.SetDirection(this.target.worldTargetPos - base.agent.wPos);
				base.agent.PlayAnimation(Arsonist.ThrowID);
				base.agent.onThrow = new Action(this.Throw);
				base.agent.onStartThrow = new Action(this.SpawnTorch);
			}));
			AgentState agentState3 = this.throwing;
			agentState3.OnDeactivate = (Action)Delegate.Combine(agentState3.OnDeactivate, new Action(delegate()
			{
				if (this.torch)
				{
					UnityEngine.Object.Destroy(this.torch.gameObject);
				}
			}));
		}

		// Token: 0x06002AD0 RID: 10960 RVA: 0x00098FC0 File Offset: 0x000973C0
		private void SpawnTorch()
		{
			this.torch = ScriptableObjectSingleton<PrefabManager>.instance.torch.GetInstance<Torch>();
			this.torch.transform.localScale = Vector3.one;
			this.torch.transform.SetParent(this.torchTransform);
			this.torch.transform.localPosition = Vector3.up * 0.1f;
			this.torch.transform.rotation = Quaternion.identity;
		}

		// Token: 0x06002AD1 RID: 10961 RVA: 0x00099044 File Offset: 0x00097444
		private void Throw()
		{
			if (!this.torch)
			{
				return;
			}
			Vector3 position = this.torch.transform.position;
			this.torch.transform.SetParent(base.agent.faction.island.runContainer);
			this.torch.Shoot(position, this.target);
			this.torch = null;
		}

		// Token: 0x06002AD2 RID: 10962 RVA: 0x000990B4 File Offset: 0x000974B4
		bool IBrainAction.MaybeAct(Brain swordsman)
		{
			if (!base.agent.navPos.onMain)
			{
				return false;
			}
			House newTarget = this.GetNewTarget(swordsman);
			if (newTarget != this.target)
			{
				if (this.target)
				{
					this.target.arsonists.Remove(this);
				}
				if (newTarget)
				{
					newTarget.arsonists.Add(this);
				}
			}
			this.target = newTarget;
			if (!this.target)
			{
				return false;
			}
			if (!this.target.TryThrow(this))
			{
				return false;
			}
			this.throwing.SetActive(true);
			return true;
		}

		// Token: 0x06002AD3 RID: 10963 RVA: 0x00099164 File Offset: 0x00097564
		private House GetNewTarget(Brain swordsman)
		{
			if (!base.agent.navPos.onMain)
			{
				return null;
			}
			if (base.agent.enemyDist < 1f)
			{
				return null;
			}
			House house = this.pather.house;
			if (!house)
			{
				return null;
			}
			if (!house.intact)
			{
				return null;
			}
			if (base.agent.orderDist > 0.2f)
			{
				return null;
			}
			return house;
		}

		// Token: 0x06002AD4 RID: 10964 RVA: 0x000991DD File Offset: 0x000975DD
		private void OnDestroy()
		{
			if (this.target)
			{
				this.target.arsonists.Remove(this);
			}
		}

		// Token: 0x06002AD5 RID: 10965 RVA: 0x00099204 File Offset: 0x00097604
		public void ApplyOrder()
		{
			if (base.agent.navPos.valid)
			{
				this.ApplyWalk();
				if (!base.agent.navPos.island)
				{
					Vector3 vector = -base.agent.navPos.GetBorderVector() * 0.2f + Vector3.forward;
					base.agent.LookInDirection(base.agent.navPos.transform.TransformVector(vector), 720f, 20f);
				}
				else
				{
					base.agent.LookInDirection(base.agent.walkDir, 720f, 20f);
				}
			}
		}

		// Token: 0x06002AD6 RID: 10966 RVA: 0x000992C0 File Offset: 0x000976C0
		public void ApplyWalk()
		{
			base.agent.walkDir = base.agent.orderDir;
			base.agent.speed = base.agent.maxSpeed * this.walkSpeed;
		}

		// Token: 0x06002AD7 RID: 10967 RVA: 0x000992F5 File Offset: 0x000976F5
		public bool WantsControl()
		{
			return this.pather && this.pather.WantsControl();
		}

		// Token: 0x06002AD8 RID: 10968 RVA: 0x00099315 File Offset: 0x00097715
		public void SampleOrder(NavPos navPos, ref Vector3 dir, ref float dist)
		{
			this.pather.SampleOrder(navPos, ref dir, ref dist);
		}

		// Token: 0x04001BD3 RID: 7123
		private House target;

		// Token: 0x04001BD4 RID: 7124
		private static int ThrowID = Animator.StringToHash("Throw");

		// Token: 0x04001BD5 RID: 7125
		private Torch torch;

		// Token: 0x04001BD6 RID: 7126
		public Transform torchTransform;

		// Token: 0x04001BD7 RID: 7127
		public VikingPatherSquad pather;

		// Token: 0x04001BD8 RID: 7128
		public float walkSpeed;

		// Token: 0x04001BD9 RID: 7129
		public AgentState throwing;
	}
}
