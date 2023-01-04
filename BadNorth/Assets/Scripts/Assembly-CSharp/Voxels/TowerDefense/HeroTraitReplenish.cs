using System;
using UnityEngine;
using Voxels.TowerDefense.Upgrades;

namespace Voxels.TowerDefense
{
	// Token: 0x02000830 RID: 2096
	internal class HeroTraitReplenish : HeroUpgradeDefinition
	{
		// Token: 0x060036A0 RID: 13984 RVA: 0x000EB36C File Offset: 0x000E976C
		public override void OnAppliedToSquad(EnglishSquad squad, int upgradeLevel)
		{
			base.OnAppliedToSquad(squad, upgradeLevel);
			ReplenishAbility upgrade = squad.upgradeManager.GetUpgrade<ReplenishAbility>();
			if (upgrade)
			{
				upgrade.replenishTime *= this.replenishTimeMultiplier;
			}
		}

		// Token: 0x04002513 RID: 9491
		[SerializeField]
		private float replenishTimeMultiplier = 1f;
	}
}
