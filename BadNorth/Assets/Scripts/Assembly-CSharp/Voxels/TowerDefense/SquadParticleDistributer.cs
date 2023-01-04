using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020007EC RID: 2028
	public class SquadParticleDistributer : MonoBehaviour
	{
		// Token: 0x060034D2 RID: 13522 RVA: 0x000E33AB File Offset: 0x000E17AB
		private void OnEnable()
		{
			this.AutoPlayParticle();
		}

		// Token: 0x060034D3 RID: 13523 RVA: 0x000E33B3 File Offset: 0x000E17B3
		private void OnDisable()
		{
			this.CancelInvoke(new Action(this.AutoPlayParticle));
		}

		// Token: 0x060034D4 RID: 13524 RVA: 0x000E33C8 File Offset: 0x000E17C8
		private void AutoPlayParticle()
		{
			this.CancelInvoke(new Action(this.AutoPlayParticle));
			if (this.targetSquad && this.agentParticle)
			{
				this.PlayParticle();
				this.Invoke(new Action(this.AutoPlayParticle), this.GetDelay());
			}
		}

		// Token: 0x060034D5 RID: 13525 RVA: 0x000E3428 File Offset: 0x000E1828
		private void PlayParticle()
		{
			List<Agent> agents = this.targetSquad.agents;
			Agent agent = this.SelectAgent(agents, this.lastAgent);
			this.PlayParticle(agent);
		}

		// Token: 0x060034D6 RID: 13526 RVA: 0x000E3456 File Offset: 0x000E1856
		private void PlayParticle(Agent agent)
		{
			if (agent)
			{
				this.agentParticle.PlayAt(this.GetAgentPosition(agent));
			}
			this.lastAgent = agent;
		}

		// Token: 0x060034D7 RID: 13527 RVA: 0x000E3480 File Offset: 0x000E1880
		private Agent SelectAgent(List<Agent> agents, Agent lastAgent)
		{
			int count = agents.Count;
			if (count > 1)
			{
				Agent agent;
				do
				{
					agent = agents[UnityEngine.Random.Range(0, agents.Count)];
				}
				while (agent == lastAgent);
				return agent;
			}
			if (count == 1)
			{
				return agents[0];
			}
			return null;
		}

		// Token: 0x060034D8 RID: 13528 RVA: 0x000E34CE File Offset: 0x000E18CE
		private Vector3 GetAgentPosition(Agent agent)
		{
			return Vector3.Lerp(agent.chestPos, agent.wChestPos, this.LeadMovementAmount);
		}

		// Token: 0x060034D9 RID: 13529 RVA: 0x000E34E8 File Offset: 0x000E18E8
		private float GetDelay()
		{
			float num = this.frequencyPerAgent;
			int count = this.targetSquad.agents.Count;
			if (!this.targetSquad || !this.agentParticle || count > 0)
			{
				num = Mathf.Min(this.frequencyPerAgent * (float)count, this.maxFrequency);
			}
			return 1f / num;
		}

		// Token: 0x040023EF RID: 9199
		[Header("References")]
		public Squad targetSquad;

		// Token: 0x040023F0 RID: 9200
		public ReusableParticle agentParticle;

		// Token: 0x040023F1 RID: 9201
		[Header("Frequency")]
		[SerializeField]
		private float frequencyPerAgent = 1f;

		// Token: 0x040023F2 RID: 9202
		[SerializeField]
		private float maxFrequency = 20f;

		// Token: 0x040023F3 RID: 9203
		[Header("Position")]
		[SerializeField]
		[Range(0f, 1f)]
		private float LeadMovementAmount = 0.75f;

		// Token: 0x040023F4 RID: 9204
		private Agent lastAgent;
	}
}
