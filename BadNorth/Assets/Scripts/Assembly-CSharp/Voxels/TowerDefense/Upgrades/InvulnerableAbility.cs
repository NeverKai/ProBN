using System;
using System.Collections.Generic;

namespace Voxels.TowerDefense.Upgrades
{
	// Token: 0x02000850 RID: 2128
	public class InvulnerableAbility : ActiveAbility, IAttackResponder
	{
		// Token: 0x060037B3 RID: 14259 RVA: 0x000F048F File Offset: 0x000EE88F
		protected override void DoSquadSpawnAction_Implementation()
		{
			base.DoSquadSpawnAction_Implementation();
			this.particleDistributer = base.GetComponent<SquadParticleDistributer>();
			if (this.particleDistributer)
			{
				this.particleDistributer.targetSquad = base.squad;
			}
		}

		// Token: 0x060037B4 RID: 14260 RVA: 0x000F04C4 File Offset: 0x000EE8C4
		public override void OnConfirmed()
		{
			this.BeginEffect();
		}

		// Token: 0x060037B5 RID: 14261 RVA: 0x000F04CC File Offset: 0x000EE8CC
		private void BeginEffect()
		{
			this.particleDistributer.enabled = true;
			List<Agent> agents = base.squad.agents;
			int i = 0;
			int count = agents.Count;
			while (i < count)
			{
				this.ApplyEffectToAgent(agents[i]);
				i++;
			}
			base.squad.onAgentSpawned += this.ApplyEffectToAgent;
			base.OnActivated();
		}

		// Token: 0x060037B6 RID: 14262 RVA: 0x000F0534 File Offset: 0x000EE934
		protected override void OnEffectEnded()
		{
			base.OnEffectEnded();
			this.particleDistributer.enabled = false;
			List<Agent> agents = base.squad.agents;
			int i = 0;
			int count = agents.Count;
			while (i < count)
			{
				this.RemovedEffectFromAgent(agents[i]);
				i++;
			}
			base.squad.onAgentSpawned -= this.ApplyEffectToAgent;
		}

		// Token: 0x060037B7 RID: 14263 RVA: 0x000F059C File Offset: 0x000EE99C
		private void ApplyEffectToAgent(Agent agent)
		{
			agent.attackResponders.Add(this);
		}

		// Token: 0x060037B8 RID: 14264 RVA: 0x000F05AA File Offset: 0x000EE9AA
		private void RemovedEffectFromAgent(Agent agent)
		{
			agent.attackResponders.Remove(this);
		}

		// Token: 0x060037B9 RID: 14265 RVA: 0x000F05BC File Offset: 0x000EE9BC
		public void ModifyAttack(ref Attack attack)
		{
			if (base.isActive)
			{
				attack.damage = 0f;
				attack.launchImpulse = 0f;
				attack.direction.y = 0f;
				attack.knockback *= 0.2f;
			}
		}

		// Token: 0x040025E7 RID: 9703
		private SquadParticleDistributer particleDistributer;
	}
}
