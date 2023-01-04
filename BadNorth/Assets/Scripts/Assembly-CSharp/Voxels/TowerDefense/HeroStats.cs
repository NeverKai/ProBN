using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Voxels.TowerDefense
{
	// Token: 0x020006F4 RID: 1780
	[Serializable]
	public class HeroStats : IHeroStats
	{
		// Token: 0x06002E1C RID: 11804 RVA: 0x000B3E1F File Offset: 0x000B221F
		public HeroStats()
		{
			this.vikingTypesKilled = new List<int>(VikingAgent.numTypes);
			this.ResizeVikingTypesKilled();
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06002E1D RID: 11805 RVA: 0x000B3E3D File Offset: 0x000B223D
		bool IHeroStats.valid
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06002E1E RID: 11806 RVA: 0x000B3E40 File Offset: 0x000B2240
		// (set) Token: 0x06002E1F RID: 11807 RVA: 0x000B3E48 File Offset: 0x000B2248
		int IHeroStats.vikingsKilled { get; set; }

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06002E20 RID: 11808 RVA: 0x000B3E51 File Offset: 0x000B2251
		// (set) Token: 0x06002E21 RID: 11809 RVA: 0x000B3E59 File Offset: 0x000B2259
		int IHeroStats.bountiesCollected { get; set; }

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06002E22 RID: 11810 RVA: 0x000B3E62 File Offset: 0x000B2262
		// (set) Token: 0x06002E23 RID: 11811 RVA: 0x000B3E6A File Offset: 0x000B226A
		int IHeroStats.soldiersLost { get; set; }

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06002E24 RID: 11812 RVA: 0x000B3E73 File Offset: 0x000B2273
		// (set) Token: 0x06002E25 RID: 11813 RVA: 0x000B3E7B File Offset: 0x000B227B
		int IHeroStats.islandsVisited { get; set; }

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06002E26 RID: 11814 RVA: 0x000B3E84 File Offset: 0x000B2284
		// (set) Token: 0x06002E27 RID: 11815 RVA: 0x000B3E8C File Offset: 0x000B228C
		int IHeroStats.timesFled { get; set; }

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06002E28 RID: 11816 RVA: 0x000B3E95 File Offset: 0x000B2295
		// (set) Token: 0x06002E29 RID: 11817 RVA: 0x000B3E9D File Offset: 0x000B229D
		int IHeroStats.islandsWon { get; set; }

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06002E2A RID: 11818 RVA: 0x000B3EA6 File Offset: 0x000B22A6
		// (set) Token: 0x06002E2B RID: 11819 RVA: 0x000B3EAE File Offset: 0x000B22AE
		int IHeroStats.collectedCoins { get; set; }

		// Token: 0x06002E2C RID: 11820 RVA: 0x000B3EB8 File Offset: 0x000B22B8
		void IHeroStats.KilledViking(VikingAgent.Type type, int count)
		{
			((IHeroStats)this).vikingsKilled = ((IHeroStats)this).vikingsKilled + count;
			if (this.vikingTypesKilled != null)
			{
				List<int> list;
				(list = this.vikingTypesKilled)[(int)type] = list[(int)type] + count;
			}
		}

		// Token: 0x06002E2D RID: 11821 RVA: 0x000B3EF7 File Offset: 0x000B22F7
		int IHeroStats.GetVikingsKilled()
		{
			return ((IHeroStats)this).vikingsKilled;
		}

		// Token: 0x06002E2E RID: 11822 RVA: 0x000B3F00 File Offset: 0x000B2300
		int IHeroStats.GetVikingsKilled(VikingAgent.Type type)
		{
			if (this.vikingTypesKilled == null)
			{
				return -1;
			}
			return this.vikingTypesKilled[(int)type];
		}

		// Token: 0x06002E2F RID: 11823 RVA: 0x000B3F28 File Offset: 0x000B2328
		[OnDeserialized]
		private void PostLoad(StreamingContext context)
		{
			this.ResizeVikingTypesKilled();
		}

		// Token: 0x06002E30 RID: 11824 RVA: 0x000B3F30 File Offset: 0x000B2330
		private void ResizeVikingTypesKilled()
		{
			if (this.vikingTypesKilled != null)
			{
				int numTypes = VikingAgent.numTypes;
				this.vikingTypesKilled.Capacity = numTypes;
				while (this.vikingTypesKilled.Count < numTypes)
				{
					this.vikingTypesKilled.Add(0);
				}
			}
		}

		// Token: 0x06002E31 RID: 11825 RVA: 0x000B3F7C File Offset: 0x000B237C
		public static implicit operator bool(HeroStats h)
		{
			return h != null;
		}

		// Token: 0x04001E95 RID: 7829
		[ObjectDumper.EnumIndexedCollectionAttribute(typeof(VikingAgent.Type))]
		private List<int> vikingTypesKilled;
	}
}
