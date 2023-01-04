using System;
using UnityEngine;
using Voxels.TowerDefense.Flag;

namespace Voxels.TowerDefense
{
	// Token: 0x0200085D RID: 2141
	public class ReplenishComponent : AgentComponent
	{
		// Token: 0x06003812 RID: 14354 RVA: 0x000F25A8 File Offset: 0x000F09A8
		public override void Setup()
		{
			base.Setup();
			this.replenishState = new AgentState("Replenishing", base.agent.navigationState, false, true);
			this.indoors = new AgentState("Indoors", this.replenishState, false, true);
			this.entering = new AgentState("Entering", this.replenishState, false, true);
			AgentState agentState = this.entering;
			agentState.OnActivate = (Action)Delegate.Combine(agentState.OnActivate, new Action(delegate()
			{
				base.agent.navPos = NavPos.empty;
			}));
			this.entering.OnUpdate += delegate()
			{
				Vector3 position = this.location.entranceTransform.position;
				Vector3 b = this.location.entranceTransform.position + this.location.entranceTransform.forward * 0.2f;
				float value = Vector3.Dot(this.location.entranceTransform.forward, position - base.transform.position);
				Vector3 target = Vector3.Lerp(position, b, ExtraMath.RemapValue(value, 0.1f, -0.1f));
				base.transform.position = Vector3.MoveTowards(base.transform.position, target, base.agent.speed * Time.deltaTime);
				if ((base.transform.position - b).sqrMagnitude < 0.0009f)
				{
					if (this.flagPole)
					{
						this.flagPole.Plant(this.location.flagTransform);
					}
					this.indoors.SetActive(true);
					base.agent.health = base.agent.maxHealth;
					this.location.AgentEntered(this);
				}
			};
			AgentState agentState2 = this.indoors;
			agentState2.OnChange = (Action<bool>)Delegate.Combine(agentState2.OnChange, new Action<bool>(this.IndoorsChange));
			AgentState agentState3 = this.replenishState;
			agentState3.OnDeactivate = (Action)Delegate.Combine(agentState3.OnDeactivate, new Action(delegate()
			{
				this.location = null;
			}));
			Agent agent = base.agent;
			agent.onFinalDeath = (Action)Delegate.Combine(agent.onFinalDeath, new Action(delegate()
			{
				if (this.location)
				{
					this.location.AgentDied(base.agent);
					this.location = null;
				}
			}));
			AgentState deadState = base.agent.deadState;
			deadState.OnActivate = (Action)Delegate.Combine(deadState.OnActivate, new Action(delegate()
			{
				if (this.replenishState.active)
				{
					base.agent.GetComponent<Death>().Die();
				}
			}));
			this.flagPole = base.agent.GetComponentInChildren<FlagPole>();
		}

		// Token: 0x06003813 RID: 14355 RVA: 0x000F26F8 File Offset: 0x000F0AF8
		public void EnterReplenishLocation(SquadReplenishLocation location)
		{
			this.location = location;
			this.entering.SetActive(true);
		}

		// Token: 0x06003814 RID: 14356 RVA: 0x000F2710 File Offset: 0x000F0B10
		public void ExitReplenishLocation(SquadReplenishLocation location)
		{
			NavPos entranceNavPos = location.entranceNavPos;
			entranceNavPos.pos += (location.entranceTransform.forward + UnityEngine.Random.insideUnitSphere) * 0.2f;
			base.agent.navPos = entranceNavPos;
			base.agent.transform.position = location.entranceTransform.position;
			base.agent.groundedState.SetActive(true);
			if (this.flagPole)
			{
				this.flagPole.Collect();
			}
		}

		// Token: 0x06003815 RID: 14357 RVA: 0x000F27A9 File Offset: 0x000F0BA9
		private void IndoorsChange(bool indoors)
		{
			if (base.agent.aliveState.active)
			{
				base.gameObject.SetActive(!indoors);
			}
		}

		// Token: 0x0400263E RID: 9790
		public AgentState replenishState;

		// Token: 0x0400263F RID: 9791
		public AgentState indoors;

		// Token: 0x04002640 RID: 9792
		public AgentState entering;

		// Token: 0x04002641 RID: 9793
		private FlagPole flagPole;

		// Token: 0x04002642 RID: 9794
		private SquadReplenishLocation location;
	}
}
