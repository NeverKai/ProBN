using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020007E5 RID: 2021
	public class ArcherSquadCoordinator : SquadCoordinatorAgentTracker<Archery>
	{
		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x06003472 RID: 13426 RVA: 0x000E249A File Offset: 0x000E089A
		public List<Archery> archers
		{
			get
			{
				return this.agentComponents;
			}
		}

		// Token: 0x06003473 RID: 13427 RVA: 0x000E24A2 File Offset: 0x000E08A2
		protected override void OnAgentComponentAdded(Archery archery)
		{
			base.OnAgentComponentAdded(archery);
		}

		// Token: 0x06003474 RID: 13428 RVA: 0x000E24AC File Offset: 0x000E08AC
		public float GetSpread(float spread)
		{
			if (base.squad.faction.side == Faction.Side.English && Time.time > this.luckyShotTime && UnityEngine.Random.value < 0.4f)
			{
				spread *= 0.01f;
				this.luckyShotTime = Time.time + (float)UnityEngine.Random.Range(20, 30);
			}
			return spread;
		}

		// Token: 0x040023C9 RID: 9161
		private float luckyShotTime;
	}
}
