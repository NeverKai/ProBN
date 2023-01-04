using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020007C8 RID: 1992
	[RequireComponent(typeof(Agent))]
	public class Proximity : AgentComponent, ISensor
	{
		// Token: 0x060033AD RID: 13229 RVA: 0x000DE938 File Offset: 0x000DCD38
		public void Sense(Agent agent)
		{
			using (new ScopedProfiler("Proximity", null))
			{
				using (new ScopedProfiler("Enemy", null))
				{
					if (this.squadProximity.threats.Count > 0)
					{
						if (this.squadIndex >= this.squadProximity.threats.Count)
						{
							this.squadIndex = 0;
						}
						this.agentIndex++;
						if (this.agentIndex >= this.squadProximity.threats[this.squadIndex].agents.Count)
						{
							this.agentIndex = 0;
							this.squadIndex++;
						}
						if (this.squadIndex >= this.squadProximity.threats.Count)
						{
							this.squadIndex = 0;
						}
						Squad squad = this.squadProximity.threats[this.squadIndex];
						if (squad.agents.Count > 0)
						{
							Agent otherAgent = squad.agents[this.agentIndex];
							this.closestEnemy = this.bestAgent(this.closestEnemy, otherAgent, ref this.dirToEnemy, ref this.distToEnemy);
							if (this.distToEnemy == 0f)
							{
								this.dirToEnemy = UnityEngine.Random.insideUnitSphere;
							}
						}
						else
						{
							this.closestEnemy = this.bestAgent(this.closestEnemy, this.closestEnemy, ref this.dirToEnemy, ref this.distToEnemy);
							if (this.distToEnemy == 0f)
							{
								this.dirToEnemy = UnityEngine.Random.insideUnitSphere;
							}
						}
					}
					else
					{
						this.closestEnemy = null;
						this.distToEnemy = float.MaxValue;
						this.dirToEnemy = Vector3.zero;
					}
				}
				using (new ScopedProfiler("Friend", null))
				{
					if (agent.squad.agents.Count > 1)
					{
						this.friendIndex = (this.friendIndex + 1) % agent.squad.agents.Count;
						if (agent.squad.agents[this.friendIndex] == agent)
						{
							this.friendIndex = (this.friendIndex + 1) % agent.squad.agents.Count;
						}
						Agent otherAgent2 = agent.squad.agents[this.friendIndex];
						this.closestFriend = this.bestAgent(this.closestFriend, otherAgent2, ref this.dirToFriend, ref this.distToFriend);
						if (this.distToFriend == 0f)
						{
							this.dirToFriend = UnityEngine.Random.insideUnitSphere;
						}
					}
					else
					{
						this.closestFriend = null;
						this.distToFriend = float.MaxValue;
						this.dirToFriend = Vector3.zero;
					}
				}
			}
		}

		// Token: 0x060033AE RID: 13230 RVA: 0x000DEC60 File Offset: 0x000DD060
		public Agent bestAgent(Agent currentAgent, Agent otherAgent, ref Vector3 dir, ref float dist)
		{
			if (currentAgent)
			{
				if (currentAgent == otherAgent)
				{
					if (base.agent.navPos.TriCast(otherAgent.navPos))
					{
						Vector3 a = otherAgent.lPos - base.agent.lPos;
						dist = a.magnitude;
						dir = a / dist;
						return otherAgent;
					}
					dir = Vector3.zero;
					dist = float.MaxValue;
					return null;
				}
				else if (base.agent.navPos.TriCast(currentAgent.navPos))
				{
					Vector3 a2 = currentAgent.lPos - base.agent.lPos;
					float sqrMagnitude = a2.sqrMagnitude;
					Vector3 a3 = base.agent.lPos - otherAgent.lPos;
					float sqrMagnitude2 = a3.sqrMagnitude;
					if (sqrMagnitude2 < sqrMagnitude && base.agent.navPos.TriCast(otherAgent.navPos))
					{
						dist = Mathf.Sqrt(sqrMagnitude2);
						dir = a3 / dist;
						return otherAgent;
					}
					dist = Mathf.Sqrt(sqrMagnitude);
					dir = a2 / dist;
					return currentAgent;
				}
				else
				{
					if (base.agent.navPos.TriCast(otherAgent.navPos))
					{
						Vector3 a4 = otherAgent.lPos - base.agent.lPos;
						dist = a4.magnitude;
						dir = a4 / dist;
						return otherAgent;
					}
					dir = Vector3.zero;
					dist = float.MaxValue;
					return null;
				}
			}
			else
			{
				if (otherAgent && base.agent.navPos.TriCast(otherAgent.navPos))
				{
					Vector3 a5 = otherAgent.lPos - base.agent.lPos;
					dist = a5.magnitude;
					dir = a5 / dist;
					return otherAgent;
				}
				dir = Vector3.zero;
				dist = float.MaxValue;
				return null;
			}
		}

		// Token: 0x060033AF RID: 13231 RVA: 0x000DEE6A File Offset: 0x000DD26A
		public override void Setup()
		{
			this.squadProximity = base.agent.squad.GetComponent<SquadProximity>();
			this.distToEnemy = float.MaxValue;
			this.distToFriend = float.MaxValue;
		}

		// Token: 0x060033B0 RID: 13232 RVA: 0x000DEE98 File Offset: 0x000DD298
		public void OnDrawGizmos()
		{
			if (this.closestEnemy)
			{
				Gizmos.color = new Color(1f, 0f, 0f, 1f);
				Gizmos.DrawLine(this.closestEnemy.wPos, Vector3.Lerp(this.closestEnemy.wPos, base.agent.wPos, 0.2f));
				Gizmos.color = new Color(1f, 0f, 0f, 0.2f);
				Gizmos.DrawLine(base.agent.wPos, this.closestEnemy.wPos);
			}
			if (this.closestFriend)
			{
				Gizmos.color = new Color(1f, 1f, 1f, 1f);
				Gizmos.DrawLine(this.closestFriend.wPos, Vector3.Lerp(this.closestFriend.wPos, base.agent.wPos, 0.2f));
				Gizmos.color = new Color(1f, 1f, 1f, 0.2f);
				Gizmos.DrawLine(base.agent.wPos, this.closestFriend.wPos);
			}
		}

		// Token: 0x0400232B RID: 9003
		private SquadProximity squadProximity;

		// Token: 0x0400232C RID: 9004
		public Agent closestFriend;

		// Token: 0x0400232D RID: 9005
		public float distToFriend;

		// Token: 0x0400232E RID: 9006
		public Vector3 dirToFriend;

		// Token: 0x0400232F RID: 9007
		private int friendIndex;

		// Token: 0x04002330 RID: 9008
		public Agent closestEnemy;

		// Token: 0x04002331 RID: 9009
		public float distToEnemy;

		// Token: 0x04002332 RID: 9010
		public Vector3 dirToEnemy;

		// Token: 0x04002333 RID: 9011
		private int squadIndex;

		// Token: 0x04002334 RID: 9012
		private int agentIndex;
	}
}
