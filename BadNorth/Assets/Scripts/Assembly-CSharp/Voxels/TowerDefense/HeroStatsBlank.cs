using System;

namespace Voxels.TowerDefense
{
	// Token: 0x020006F5 RID: 1781
	public class HeroStatsBlank : IHeroStats
	{
		// Token: 0x06002E32 RID: 11826 RVA: 0x000B3F85 File Offset: 0x000B2385
		private HeroStatsBlank()
		{
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x06002E33 RID: 11827 RVA: 0x000B3F8D File Offset: 0x000B238D
		bool IHeroStats.valid
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x06002E34 RID: 11828 RVA: 0x000B3F90 File Offset: 0x000B2390
		// (set) Token: 0x06002E35 RID: 11829 RVA: 0x000B3F93 File Offset: 0x000B2393
		int IHeroStats.vikingsKilled
		{
			get
			{
				return -1;
			}
			set
			{
			}
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06002E36 RID: 11830 RVA: 0x000B3F95 File Offset: 0x000B2395
		// (set) Token: 0x06002E37 RID: 11831 RVA: 0x000B3F98 File Offset: 0x000B2398
		int IHeroStats.bountiesCollected
		{
			get
			{
				return -1;
			}
			set
			{
			}
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06002E38 RID: 11832 RVA: 0x000B3F9A File Offset: 0x000B239A
		// (set) Token: 0x06002E39 RID: 11833 RVA: 0x000B3F9D File Offset: 0x000B239D
		int IHeroStats.soldiersLost
		{
			get
			{
				return -1;
			}
			set
			{
			}
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06002E3A RID: 11834 RVA: 0x000B3F9F File Offset: 0x000B239F
		// (set) Token: 0x06002E3B RID: 11835 RVA: 0x000B3FA2 File Offset: 0x000B23A2
		int IHeroStats.islandsVisited
		{
			get
			{
				return -1;
			}
			set
			{
			}
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06002E3C RID: 11836 RVA: 0x000B3FA4 File Offset: 0x000B23A4
		// (set) Token: 0x06002E3D RID: 11837 RVA: 0x000B3FA7 File Offset: 0x000B23A7
		int IHeroStats.timesFled
		{
			get
			{
				return -1;
			}
			set
			{
			}
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06002E3E RID: 11838 RVA: 0x000B3FA9 File Offset: 0x000B23A9
		// (set) Token: 0x06002E3F RID: 11839 RVA: 0x000B3FAC File Offset: 0x000B23AC
		int IHeroStats.islandsWon
		{
			get
			{
				return -1;
			}
			set
			{
			}
		}

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06002E40 RID: 11840 RVA: 0x000B3FAE File Offset: 0x000B23AE
		// (set) Token: 0x06002E41 RID: 11841 RVA: 0x000B3FB1 File Offset: 0x000B23B1
		int IHeroStats.collectedCoins
		{
			get
			{
				return -1;
			}
			set
			{
			}
		}

		// Token: 0x06002E42 RID: 11842 RVA: 0x000B3FB3 File Offset: 0x000B23B3
		void IHeroStats.KilledViking(VikingAgent.Type type, int count)
		{
		}

		// Token: 0x06002E43 RID: 11843 RVA: 0x000B3FB5 File Offset: 0x000B23B5
		int IHeroStats.GetVikingsKilled()
		{
			return 0;
		}

		// Token: 0x06002E44 RID: 11844 RVA: 0x000B3FB8 File Offset: 0x000B23B8
		int IHeroStats.GetVikingsKilled(VikingAgent.Type type)
		{
			return 0;
		}

		// Token: 0x04001E96 RID: 7830
		public static readonly IHeroStats inst = new HeroStatsBlank();
	}
}
