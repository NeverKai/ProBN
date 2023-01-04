using System;
using ReflexCLI.Attributes;
using RTM.OnScreenDebug;
using UnityEngine;
using Voxels.TowerDefense.CampaignGeneration;

namespace Voxels.TowerDefense.Upgrades
{
	// Token: 0x0200085C RID: 2140
	public class ReplenishAbility : HouseTargetableAbility
	{
		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x06003807 RID: 14343 RVA: 0x000F2224 File Offset: 0x000F0624
		private float healTime
		{
			get
			{
				return this.replenishTime;
			}
		}

		// Token: 0x06003808 RID: 14344 RVA: 0x000F222C File Offset: 0x000F062C
		protected override void DoSquadSpawnAction_Implementation()
		{
			base.DoSquadSpawnAction_Implementation();
			this.squadPather = base.squad.pather;
			this.active.OnUpdate += this.AwaitArrival;
			CampaignDifficultySettings diffiucltySettings = base.squad.faction.island.levelNode.diffiucltySettings;
			float num = this.replenishTime;
			this.replenishTime *= diffiucltySettings.replenishTimeMultiplier;
			base.squad.onAgentRemoved += this.Squad_onAgentRemoved;
			this.onSquadBeginExit = new Action(this.OnSquadBeginExit);
		}

		// Token: 0x06003809 RID: 14345 RVA: 0x000F22C8 File Offset: 0x000F06C8
		private void Squad_onAgentRemoved(Agent obj)
		{
			if (this.location && !base.squad.alive)
			{
				this.location.SquadDead();
				this.location.onSquadBeginExit -= this.onSquadBeginExit;
				this.location = null;
			}
		}

		// Token: 0x0600380A RID: 14346 RVA: 0x000F2318 File Offset: 0x000F0718
		public override void SelectLocation(SquadReplenishLocation location)
		{
			this.location = location;
			this.squadPather.SetPatherTarget(location, true);
			FabricWrapper.PostEvent("UI/InGame/OrderReplenish");
			location.AssignSquad(base.squad, this.replenishTime, this.healTime);
			location.onSquadBeginExit += this.onSquadBeginExit;
			base.OnActivated();
		}

		// Token: 0x0600380B RID: 14347 RVA: 0x000F2370 File Offset: 0x000F0770
		private void AwaitArrival()
		{
			if (Time.time > this.timer && this.location.numContainedAgents < base.squad.livingAgents.Count)
			{
				Agent agent = null;
				float num = 0.5f;
				foreach (Agent agent2 in base.squad.livingAgents)
				{
					if (!this.location.Contains(agent2))
					{
						agent2.movability *= 0.2f;
						if (agent2.orderDist < agent2.radius && agent2.orderDist < num)
						{
							agent = agent2;
							num = agent2.orderDist;
						}
					}
				}
				if (agent)
				{
					if (num < agent.radius)
					{
						this.AgentArrived(agent);
						this.timer = Time.time + 0.2f;
					}
					else if (num < 0.3f)
					{
						agent.navPos = this.location.entranceNavPos;
						this.timer = Time.time + 0.1f;
					}
				}
			}
		}

		// Token: 0x0600380C RID: 14348 RVA: 0x000F24AC File Offset: 0x000F08AC
		private void AgentArrived(Agent agent)
		{
			ReplenishComponent orAddComponent = agent.GetOrAddComponent<ReplenishComponent>();
			orAddComponent.EnterReplenishLocation(this.location);
		}

		// Token: 0x0600380D RID: 14349 RVA: 0x000F24CC File Offset: 0x000F08CC
		private void OnSquadBeginExit()
		{
			this.location.onSquadBeginExit -= this.onSquadBeginExit;
			this.location = null;
			base.OnEnded();
		}

		// Token: 0x0600380E RID: 14350 RVA: 0x000F24EC File Offset: 0x000F08EC
		public override void Cancel()
		{
			base.Cancel();
		}

		// Token: 0x0600380F RID: 14351 RVA: 0x000F24F4 File Offset: 0x000F08F4
		public override void LevelEnded()
		{
			base.LevelEnded();
			if (this.active.active && this.location)
			{
				this.location.LevelEnded();
				ReplenishComponent component = base.squad.heroAgent.GetComponent<ReplenishComponent>();
				if (!component || !component.indoors.active)
				{
					base.squad.navSpotOccupant.SetNavSpot(NavSpot.GetNavSpot(base.squad.heroAgent.transform.position, false), true);
				}
			}
		}

		// Token: 0x04002639 RID: 9785
		private static DebugChannelGroup dbgGroup = new DebugChannelGroup("ReplenishAbility", EVerbosity.Quiet, 0);

		// Token: 0x0400263A RID: 9786
		[Header("Delays")]
		[SerializeField]
		[ConsoleCommand("")]
		public float replenishTime = 2.2f;

		// Token: 0x0400263B RID: 9787
		private EnglishPatherSquad squadPather;

		// Token: 0x0400263C RID: 9788
		private float timer;

		// Token: 0x0400263D RID: 9789
		private Action onSquadBeginExit;
	}
}
