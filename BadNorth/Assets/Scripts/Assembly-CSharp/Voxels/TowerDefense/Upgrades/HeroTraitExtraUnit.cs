using System;
using Voxels.TowerDefense.HeroGeneration;

namespace Voxels.TowerDefense.Upgrades
{
	// Token: 0x0200082D RID: 2093
	public class HeroTraitExtraUnit : HeroUpgradeDefinition, MonoHero.IRankModifier
	{
		// Token: 0x06003696 RID: 13974 RVA: 0x000EB074 File Offset: 0x000E9474
		public override void OnAppliedToSquad(EnglishSquad squad, int upgradeLevel)
		{
			base.OnAppliedToSquad(squad, upgradeLevel);
			squad.maxCount++;
		}

		// Token: 0x06003697 RID: 13975 RVA: 0x000EB08C File Offset: 0x000E948C
		void MonoHero.IRankModifier.ModifyRank(ref int rankCount, SerializableHeroUpgrade upgrade)
		{
			rankCount++;
		}
	}
}
