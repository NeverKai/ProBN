using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020006A3 RID: 1699
	public class RoamAround : AgentComponent, IAgentOrder
	{
		// Token: 0x06002BE5 RID: 11237 RVA: 0x000A1F4C File Offset: 0x000A034C
		public override void Setup()
		{
			base.Setup();
			base.agent.aliveAndGrounded.OnUpdate += this.OnUpdate;
		}

		// Token: 0x06002BE6 RID: 11238 RVA: 0x000A1F70 File Offset: 0x000A0370
		private void OnUpdate()
		{
			if (!this.navSpot)
			{
				List<NavSpot> navSpots = base.agent.faction.island.navSpotter.navSpots;
				this.navSpot = navSpots[UnityEngine.Random.Range(0, navSpots.Count)];
			}
			if (base.agent.orderDist > 0.1f)
			{
				this.time = Time.time;
			}
			else if (Time.time > this.time + 2f)
			{
				List<NavSpot> navSpots2 = base.agent.faction.island.navSpotter.navSpots;
				this.navSpot = navSpots2[UnityEngine.Random.Range(0, navSpots2.Count)];
			}
		}

		// Token: 0x06002BE7 RID: 11239 RVA: 0x000A202E File Offset: 0x000A042E
		void IAgentOrder.ApplyOrder()
		{
			this.ApplyWalk();
			base.agent.LookInDirection(base.agent.walkDir, 720f, 20f);
		}

		// Token: 0x06002BE8 RID: 11240 RVA: 0x000A2056 File Offset: 0x000A0456
		public void ApplyWalk()
		{
			base.agent.walkDir += base.agent.orderDir;
		}

		// Token: 0x06002BE9 RID: 11241 RVA: 0x000A2079 File Offset: 0x000A0479
		bool IAgentOrder.WantsControl()
		{
			return base.agent.faction.enemy.agents.Count > 0;
		}

		// Token: 0x06002BEA RID: 11242 RVA: 0x000A2098 File Offset: 0x000A0498
		public void SampleOrder(NavPos navPos, ref Vector3 dir, ref float dist)
		{
			this.navSpot.distanceField.Sample(navPos, ref dir, ref dist);
			dist = Mathf.Max(new float[]
			{
				dist - 0.5f
			});
			dir = Vector3.ClampMagnitude(dir, dist);
		}

		// Token: 0x04001CA3 RID: 7331
		private NavSpot navSpot;

		// Token: 0x04001CA4 RID: 7332
		private float time;
	}
}
