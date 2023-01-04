using System;
using Voxels.TowerDefense.HeroGeneration;

namespace Voxels.TowerDefense.Upgrades
{
	// Token: 0x0200083A RID: 2106
	public class SquadSizeUpgrade : HeroUpgradeDefinition, MonoHero.IRankModifier
	{
		// Token: 0x060036D2 RID: 14034 RVA: 0x000EB957 File Offset: 0x000E9D57
		public override void OnAttachedToMonoHero(MonoHero monoHero, int level)
		{
			base.OnAttachedToMonoHero(monoHero, level);
			monoHero.UpdateRankCount();
		}

		// Token: 0x060036D3 RID: 14035 RVA: 0x000EB967 File Offset: 0x000E9D67
		public override void OnAppliedToSquad(EnglishSquad squad, int upgradeLevel)
		{
			base.OnAppliedToSquad(squad, upgradeLevel);
			squad.maxCount += this.sizeAtLevel[upgradeLevel] - 9;
		}

		// Token: 0x060036D4 RID: 14036 RVA: 0x000EB989 File Offset: 0x000E9D89
		void MonoHero.IRankModifier.ModifyRank(ref int rankCount, SerializableHeroUpgrade upgrade)
		{
			rankCount += (1 + upgrade.level) * 2;
		}

		// Token: 0x04002541 RID: 9537
		public int[] sizeAtLevel = new int[]
		{
			12,
			16
		};
	}
}
