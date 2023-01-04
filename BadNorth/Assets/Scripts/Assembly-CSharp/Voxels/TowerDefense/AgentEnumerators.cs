using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020006AA RID: 1706
	public static class AgentEnumerators
	{
		// Token: 0x06002C1A RID: 11290 RVA: 0x000A36C5 File Offset: 0x000A1AC5
		public static void ClearStaticList()
		{
			AgentEnumerators.staticList.Clear();
		}

		// Token: 0x06002C1B RID: 11291 RVA: 0x000A36D4 File Offset: 0x000A1AD4
		public static List<Agent> GetStaticListRadius(Vector3 position, float radius, Faction faction = null)
		{
			AgentEnumerators.staticList.Clear();
			List<Squad> list = (!faction) ? Squad.allLiving : faction.livingSquads;
			float num = radius * radius;
			for (int i = 0; i < list.Count; i++)
			{
				Squad squad = list[i];
				Bounds bounds = squad.bounds;
				bounds.extents += Vector3.one * radius;
				if (bounds.Contains(position))
				{
					List<Agent> livingAgents = list[i].livingAgents;
					for (int j = 0; j < livingAgents.Count; j++)
					{
						Agent agent = livingAgents[j];
						float sqrMagnitude = (agent.chestPos - position).sqrMagnitude;
						if (sqrMagnitude <= num)
						{
							AgentEnumerators.staticList.Add(agent);
						}
					}
				}
			}
			return AgentEnumerators.staticList;
		}

		// Token: 0x06002C1C RID: 11292 RVA: 0x000A37C8 File Offset: 0x000A1BC8
		public static List<Agent> GetStaticListRadiusSorted(Vector3 position, float radius, Faction faction = null)
		{
			AgentEnumerators.staticList.Clear();
			List<Squad> list = (!faction) ? Squad.allLiving : faction.livingSquads;
			float num = radius * radius;
			for (int i = 0; i < list.Count; i++)
			{
				Squad squad = list[i];
				Bounds bounds = squad.bounds;
				bounds.extents += Vector3.one * radius;
				if (bounds.Contains(position))
				{
					List<Agent> livingAgents = list[i].livingAgents;
					for (int j = 0; j < livingAgents.Count; j++)
					{
						Agent agent = livingAgents[j];
						float sqrMagnitude = (agent.chestPos - position).sqrMagnitude;
						if (sqrMagnitude <= num)
						{
							int num2 = 0;
							while (num2 < AgentEnumerators.staticList.Count && (AgentEnumerators.staticList[num2].chestPos - position).sqrMagnitude < sqrMagnitude)
							{
								num2++;
							}
							AgentEnumerators.staticList.Insert(num2, agent);
						}
					}
				}
			}
			return AgentEnumerators.staticList;
		}

		// Token: 0x06002C1D RID: 11293 RVA: 0x000A3904 File Offset: 0x000A1D04
		public static IEnumerator<Agent> MultiFrame(Agent origin, float radius, Faction faction)
		{
			List<Squad> squads = (!faction) ? Squad.allLiving : faction.livingSquads;
			Vector3 boundsAdd = radius * Vector3.one;
			float sqRadius = radius * radius;
			for (;;)
			{
				bool empty = true;
				for (int i = 0; i < squads.Count; i++)
				{
					Squad squad = squads[i];
					for (int j = 0; j < squad.livingAgents.Count; j++)
					{
						Bounds bounds = squad.bounds;
						bounds.extents += boundsAdd;
						if (!bounds.Contains(origin.wPos))
						{
							break;
						}
						Agent agent = squad.livingAgents[j];
						if (Vector3.SqrMagnitude(origin.wPos - agent.wPos) < sqRadius)
						{
							empty = false;
							yield return agent;
						}
					}
				}
				if (empty)
				{
					yield return null;
				}
			}
			yield break;
		}

		// Token: 0x06002C1E RID: 11294 RVA: 0x000A3930 File Offset: 0x000A1D30
		public static IEnumerator<Agent> MultiFrameFunc(Faction faction, Func<Squad, bool> squadFunc, Func<Agent, bool> agentFunc)
		{
			List<Squad> squads = (!faction) ? Squad.allLiving : faction.livingSquads;
			for (;;)
			{
				bool empty = true;
				for (int i = 0; i < squads.Count; i++)
				{
					Squad squad = squads[i];
					for (int j = 0; j < squad.livingAgents.Count; j++)
					{
						if (!squadFunc(squad))
						{
							break;
						}
						Agent agent = squad.livingAgents[j];
						if (agentFunc(agent))
						{
							empty = false;
							yield return agent;
						}
					}
				}
				if (empty)
				{
					yield return null;
				}
			}
			yield break;
		}

		// Token: 0x04001CD9 RID: 7385
		private static List<Agent> staticList = new List<Agent>(12);

		// Token: 0x020006AB RID: 1707
		private struct AgentDistSqr
		{
			// Token: 0x06002C20 RID: 11296 RVA: 0x000A3967 File Offset: 0x000A1D67
			public AgentDistSqr(Agent agent, float distSqr)
			{
				this.agent = agent;
				this.distSqr = distSqr;
			}

			// Token: 0x04001CDA RID: 7386
			public Agent agent;

			// Token: 0x04001CDB RID: 7387
			public float distSqr;
		}
	}
}
