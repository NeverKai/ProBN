using System;
using Voxels.TowerDefense.UI;

namespace Voxels.TowerDefense
{
	// Token: 0x020006A8 RID: 1704
	public class VikingAgent : AgentComponent, IAttackResponder
	{
		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06002C13 RID: 11283 RVA: 0x000A35AB File Offset: 0x000A19AB
		public VikingAgent.Type type
		{
			get
			{
				return this.vikingReference.type;
			}
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06002C14 RID: 11284 RVA: 0x000A35B8 File Offset: 0x000A19B8
		public int bounty
		{
			get
			{
				return this.vikingReference.bounty;
			}
		}

		// Token: 0x06002C15 RID: 11285 RVA: 0x000A35C5 File Offset: 0x000A19C5
		public void ModifyAttack(ref Attack attack)
		{
			this.killerSquad = attack.killerSquad;
		}

		// Token: 0x06002C16 RID: 11286 RVA: 0x000A35D4 File Offset: 0x000A19D4
		public override void Setup()
		{
			base.Setup();
			Agent agent = base.agent;
			agent.onFinalDeath = (Action)Delegate.Combine(agent.onFinalDeath, new Action(this.OnFinalDeath));
			AgentState deadState = base.agent.deadState;
			deadState.OnActivate = (Action)Delegate.Combine(deadState.OnActivate, new Action(this.OnImmediateDeath));
		}

		// Token: 0x06002C17 RID: 11287 RVA: 0x000A363C File Offset: 0x000A1A3C
		private void OnImmediateDeath()
		{
			EnglishSquad englishSquad = (EnglishSquad)this.killerSquad;
			if (englishSquad)
			{
				englishSquad.RegisterImmediateKill(this);
			}
		}

		// Token: 0x06002C18 RID: 11288 RVA: 0x000A3668 File Offset: 0x000A1A68
		private void OnFinalDeath()
		{
			EnglishSquad englishSquad = (EnglishSquad)this.killerSquad;
			if (englishSquad)
			{
				englishSquad.RegisterFinalKill(this);
			}
			EnemyLineupIntro.instance.RegisterKill(this.vikingReference);
			this.killerSquad = null;
		}

		// Token: 0x04001CCD RID: 7373
		public static readonly int numTypes = Enum.GetValues(typeof(VikingAgent.Type)).Length;

		// Token: 0x04001CCE RID: 7374
		public VikingReference vikingReference;

		// Token: 0x04001CCF RID: 7375
		private Squad killerSquad;

		// Token: 0x020006A9 RID: 1705
		public enum Type
		{
			// Token: 0x04001CD1 RID: 7377
			Sword,
			// Token: 0x04001CD2 RID: 7378
			SwordShield,
			// Token: 0x04001CD3 RID: 7379
			Archer,
			// Token: 0x04001CD4 RID: 7380
			Tank,
			// Token: 0x04001CD5 RID: 7381
			Beserker,
			// Token: 0x04001CD6 RID: 7382
			AxeThrower,
			// Token: 0x04001CD7 RID: 7383
			TankArcher,
			// Token: 0x04001CD8 RID: 7384
			Twohanded
		}
	}
}
