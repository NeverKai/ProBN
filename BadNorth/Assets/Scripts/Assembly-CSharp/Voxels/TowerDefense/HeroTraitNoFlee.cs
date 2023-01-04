using System;
using Voxels.TowerDefense.Upgrades;

namespace Voxels.TowerDefense
{
	// Token: 0x0200082F RID: 2095
	internal class HeroTraitNoFlee : HeroUpgradeDefinition
	{
		// Token: 0x0600369E RID: 13982 RVA: 0x000EB320 File Offset: 0x000E9720
		public override void OnAppliedToSquad(EnglishSquad squad, int upgradeLevel)
		{
			base.OnAppliedToSquad(squad, upgradeLevel);
			EvacuateAbility upgrade = squad.upgradeManager.GetUpgrade<EvacuateAbility>();
			if (upgrade)
			{
				upgrade.BanAbility("HERO_TRAITS/NO_FLEE/ABILITY_TOOLTIP", null, null);
			}
		}
	}
}
