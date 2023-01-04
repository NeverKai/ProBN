using System;
using RTM.OnScreenDebug;
using UnityEngine;

namespace Voxels.TowerDefense.upgrades
{
	// Token: 0x0200082C RID: 2092
	internal class HeroTraitCheaperUpgrades : HeroUpgradeDefinition
	{
		// Token: 0x06003694 RID: 13972 RVA: 0x000EB04A File Offset: 0x000E944A
		public override void OnAttachedToHero(HeroDefinition hero, int level)
		{
			base.OnAttachedToHero(hero, level);
			hero.discount = this.discount;
			hero.discountType = this.affectsType;
		}

		// Token: 0x04002504 RID: 9476
		private DebugChannelGroup dbgGroup = new DebugChannelGroup("HeroTraitAbilityModifier", EVerbosity.Quiet, 0);

		// Token: 0x04002505 RID: 9477
		[Space]
		[Header("Discount")]
		[SerializeField]
		private HeroUpgradeTypeEnum affectsType;

		// Token: 0x04002506 RID: 9478
		[SerializeField]
		[Range(0f, 1f)]
		private float discount;
	}
}
