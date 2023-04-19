using System;
using System.Diagnostics;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020007A5 RID: 1957
	public class EnglishPatherAgent : AgentComponent, IAgentOrder
	{
		// Token: 0x140000A8 RID: 168
		// (add) Token: 0x060032A3 RID: 12963 RVA: 0x000D7088 File Offset: 0x000D5488
		// (remove) Token: 0x060032A4 RID: 12964 RVA: 0x000D70C0 File Offset: 0x000D54C0
		
		public event Action<IPathTarget> onPathTargetChanged = delegate(IPathTarget A_0)
		{
		};

		// Token: 0x060032A5 RID: 12965 RVA: 0x000D70F6 File Offset: 0x000D54F6
		public override void Setup()
		{
			base.Setup();
			this.squadPather = base.squad.GetSquadCoordinator<EnglishPatherSquad>();
			this.agentFormation = base.GetComponent<EnglishFormationAgent>();
		}

		// Token: 0x060032A6 RID: 12966 RVA: 0x000D711B File Offset: 0x000D551B
		private void Start()
		{
			this.squadPather.onPathTargetChanged += this.OnPathTargetChanged;
		}

		// Token: 0x060032A7 RID: 12967 RVA: 0x000D7134 File Offset: 0x000D5534
		private void OnDestroy()
		{
			if (this.squadPather)
			{
				this.squadPather.onPathTargetChanged -= this.OnPathTargetChanged;
			}
			this.onPathTargetChanged = null;
			this.squadPather = null;
			this.agentFormation = null;
		}

		// Token: 0x060032A8 RID: 12968 RVA: 0x000D7174 File Offset: 0x000D5574
		private void OnPathTargetChanged(IPathTarget target)
		{
			if (target != null && base.agent.navPos.valid)
			{
				base.agent.brain.order.SampleOrder(base.agent.navPos, ref base.agent.orderDir, ref base.agent.orderDist);
			}
			this.onPathTargetChanged(target);
		}

		// Token: 0x060032A9 RID: 12969 RVA: 0x000D71E0 File Offset: 0x000D55E0
		void IAgentOrder.ApplyOrder()
		{
			base.agent.walkDir = base.agent.orderDir;
			base.agent.speed = base.agent.maxSpeed * this.walkSpeed;
			this.ApplyLookAt(base.agent.orderDist);
			base.agent.movability = Mathf.Lerp(2f, 0.2f, base.agent.orderDist) * this.agentFormation.movability;
		}

		// Token: 0x060032AA RID: 12970 RVA: 0x000D7264 File Offset: 0x000D5664
		void IAgentOrder.ApplyWalk()
		{
			base.agent.walkDir = base.agent.orderDir;
			base.agent.movability = Mathf.Lerp(2f, 0.2f, base.agent.orderDist) * this.agentFormation.movability;
		}

		// Token: 0x060032AB RID: 12971 RVA: 0x000D72B8 File Offset: 0x000D56B8
		private void ApplyLookAt(float orderDist)
		{
			EnglishFormationAgent.OrderPos orderPos = this.agentFormation.orderPos;
			bool arrived = base.squad.GetSquadCoordinator<NavSpotFormationSquad>().arrived;
			base.agent.LookInDirection(Vector3.Lerp(orderPos.lookDir, base.agent.orderDir, base.agent.orderDist - ((!arrived) ? 0.2f : 2f)), 720f, 20f);
		}

		// Token: 0x060032AC RID: 12972 RVA: 0x000D732E File Offset: 0x000D572E
		void IAgentOrder.SampleOrder(NavPos navPos, ref Vector3 dir, ref float dist)
		{
			this.squadPather.target.SampleDistanceDir(navPos, ref dir, ref dist);
			this.agentFormation.ModifyPath(navPos, ref dir, ref dist);
		}

		// Token: 0x060032AD RID: 12973 RVA: 0x000D7351 File Offset: 0x000D5751
		bool IAgentOrder.WantsControl()
		{
			return true;
		}

		// Token: 0x0400226A RID: 8810
		private EnglishPatherSquad squadPather;

		// Token: 0x0400226B RID: 8811
		private EnglishFormationAgent agentFormation;

		// Token: 0x0400226C RID: 8812
		[HideInInspector]
		public float walkSpeed = 1f;
	}
}
