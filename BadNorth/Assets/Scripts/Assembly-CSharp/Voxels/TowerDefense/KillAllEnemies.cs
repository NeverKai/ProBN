using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x0200069F RID: 1695
	public class KillAllEnemies : AgentComponent, IAgentOrder
	{
		// Token: 0x06002BC2 RID: 11202 RVA: 0x000A1050 File Offset: 0x0009F450
		void IAgentOrder.ApplyOrder()
		{
			base.agent.walkDir += base.agent.enemyDir;
			base.agent.LookInDirection(base.agent.walkDir, 720f, 20f);
		}

		// Token: 0x06002BC3 RID: 11203 RVA: 0x000A109E File Offset: 0x0009F49E
		void IAgentOrder.ApplyWalk()
		{
			base.agent.walkDir += base.agent.enemyDir;
		}

		// Token: 0x06002BC4 RID: 11204 RVA: 0x000A10C1 File Offset: 0x0009F4C1
		bool IAgentOrder.WantsControl()
		{
			return base.agent && base.agent.faction.enemy.agents.Count > 0;
		}

		// Token: 0x06002BC5 RID: 11205 RVA: 0x000A10F4 File Offset: 0x0009F4F4
		void IAgentOrder.SampleOrder(NavPos navPos, ref Vector3 dir, ref float dist)
		{
			if (navPos == base.agent.navPos)
			{
				dir = base.agent.enemyDir;
				dist = base.agent.enemyDist;
			}
			else
			{
				Agent agent = null;
				base.agent.faction.enemy.presence.SampleFullData(navPos, ref dist, ref dir, ref agent);
			}
		}
	}
}
