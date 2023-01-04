using System;

namespace Voxels.TowerDefense
{
	// Token: 0x020006F9 RID: 1785
	public interface IHeroDisplay<TManager>
	{
		// Token: 0x06002E4F RID: 11855
		void SetHeroDefinition(TManager manager, HeroDefinition hero);

		// Token: 0x06002E50 RID: 11856
		void ClearHeroDefinition();
	}
}
