using System;
using RTM.OnScreenDebug;
using UnityEngine;
using Voxels.TowerDefense.Upgrades;

namespace Voxels.TowerDefense.upgrades
{
	// Token: 0x0200082B RID: 2091
	internal class HeroTraitAbilityModifier : HeroUpgradeDefinition
	{
		// Token: 0x06003692 RID: 13970 RVA: 0x000EAF0C File Offset: 0x000E930C
		public override void OnAppliedToSquad(EnglishSquad squad, int upgradeLevel)
		{
			base.OnAppliedToSquad(squad, upgradeLevel);
			foreach (UpgradeComponent upgradeComponent in squad.upgradeManager)
			{
				ActiveAbility activeAbility = upgradeComponent as ActiveAbility;
				if (activeAbility)
				{
					if (this.extraCharges != 0 && activeAbility.hasLimitedCharges)
					{
						activeAbility.AddCharges(this.extraCharges);
					}
					if (this.cooldownMultiplier != 1f && activeAbility.hasCooldown)
					{
						activeAbility.modifyCooldown(this.cooldownMultiplier);
					}
				}
			}
			if (this.replenishTimeMultiplier != 1f)
			{
				ReplenishAbility upgrade = squad.upgradeManager.GetUpgrade<ReplenishAbility>();
				if (upgrade)
				{
					upgrade.replenishTime *= this.replenishTimeMultiplier;
				}
			}
			if (this.banEvac)
			{
				EvacuateAbility upgrade2 = squad.upgradeManager.GetUpgrade<EvacuateAbility>();
				if (upgrade2)
				{
					upgrade2.BanAbility("HERO_TRAITS/NO_FLEE/ABILITY_TOOLTIP", null, null);
				}
			}
		}

		// Token: 0x040024FF RID: 9471
		private DebugChannelGroup dbgGroup = new DebugChannelGroup("HeroTraitAbilityModifier", EVerbosity.Quiet, 0);

		// Token: 0x04002500 RID: 9472
		[Space]
		[Header("AbilityModifications")]
		[SerializeField]
		private int extraCharges;

		// Token: 0x04002501 RID: 9473
		[SerializeField]
		private float cooldownMultiplier = 1f;

		// Token: 0x04002502 RID: 9474
		[SerializeField]
		private float replenishTimeMultiplier = 1f;

		// Token: 0x04002503 RID: 9475
		[SerializeField]
		private bool banEvac;
	}
}
