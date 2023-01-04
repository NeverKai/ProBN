using System;

namespace Voxels.TowerDefense
{
	// Token: 0x020006F3 RID: 1779
	public interface IHeroStats
	{
		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06002E0A RID: 11786
		bool valid { get; }

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x06002E0B RID: 11787
		// (set) Token: 0x06002E0C RID: 11788
		int bountiesCollected { get; set; }

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06002E0D RID: 11789
		// (set) Token: 0x06002E0E RID: 11790
		int soldiersLost { get; set; }

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x06002E0F RID: 11791
		// (set) Token: 0x06002E10 RID: 11792
		int islandsVisited { get; set; }

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06002E11 RID: 11793
		// (set) Token: 0x06002E12 RID: 11794
		int timesFled { get; set; }

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06002E13 RID: 11795
		// (set) Token: 0x06002E14 RID: 11796
		int islandsWon { get; set; }

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06002E15 RID: 11797
		// (set) Token: 0x06002E16 RID: 11798
		int collectedCoins { get; set; }

		// Token: 0x06002E17 RID: 11799
		void KilledViking(VikingAgent.Type type, int count = 1);

		// Token: 0x06002E18 RID: 11800
		int GetVikingsKilled();

		// Token: 0x06002E19 RID: 11801
		int GetVikingsKilled(VikingAgent.Type type);

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06002E1A RID: 11802
		// (set) Token: 0x06002E1B RID: 11803
		[Obsolete("Do Not use vikingsKilled directly anyMore, use GetVikingsKilled or SetVikingsKilled")]
		int vikingsKilled { get; set; }
	}
}
