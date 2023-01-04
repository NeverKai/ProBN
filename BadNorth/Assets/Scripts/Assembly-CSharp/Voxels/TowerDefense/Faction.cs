using System;
using System.Collections.Generic;
using UnityEngine;
using Voxels.TowerDefense.TriFlow;

namespace Voxels.TowerDefense
{
	// Token: 0x02000755 RID: 1877
	public class Faction : IslandComponent, IIslandWipe
	{
		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06003108 RID: 12552 RVA: 0x000CA1AF File Offset: 0x000C85AF
		public FlowField presence
		{
			get
			{
				return this.presenceObj.flowField;
			}
		}

		// Token: 0x06003109 RID: 12553 RVA: 0x000CA1BC File Offset: 0x000C85BC
		public IEnumerator<GenInfo> OnIslandWipe(Island island)
		{
			for (int i = this.agents.Count - 1; i >= 0; i--)
			{
				UnityEngine.Object.Destroy(this.agents[i].gameObject);
			}
			for (int j = this.allSquads.Count - 1; j >= 0; j--)
			{
				UnityEngine.Object.Destroy(this.allSquads[j].gameObject);
			}
			this.agents.Clear();
			this.allSquads.Clear();
			this.livingSquads.Clear();
			yield return new GenInfo("Wiping island", GenInfo.Mode.interruptable);
			yield break;
		}

		// Token: 0x0600310A RID: 12554 RVA: 0x000CA1D7 File Offset: 0x000C85D7
		public void OnDestroy()
		{
			this.allSquads.Clear();
			this.livingSquads.Clear();
			this.agents.Clear();
			this.enemy = null;
			this.presenceObj = null;
		}

		// Token: 0x040020BF RID: 8383
		public FabricEventReference bloodStainSound = "Sfx/English/Blood";

		// Token: 0x040020C0 RID: 8384
		public List<Squad> allSquads = new List<Squad>();

		// Token: 0x040020C1 RID: 8385
		public List<Squad> livingSquads = new List<Squad>();

		// Token: 0x040020C2 RID: 8386
		public List<Agent> agents = new List<Agent>();

		// Token: 0x040020C3 RID: 8387
		public Faction enemy;

		// Token: 0x040020C4 RID: 8388
		public Color color = Color.white;

		// Token: 0x040020C5 RID: 8389
		public TriFlowBehaviour presenceObj;

		// Token: 0x040020C6 RID: 8390
		public Faction.Side side;

		// Token: 0x02000756 RID: 1878
		public enum Side
		{
			// Token: 0x040020C8 RID: 8392
			English,
			// Token: 0x040020C9 RID: 8393
			Viking
		}
	}
}
