using System;
using RTM.OnScreenDebug;

namespace Voxels.TowerDefense
{
	// Token: 0x02000828 RID: 2088
	[Serializable]
	public class CornucopiaUpgradeDefinition : HeroUpgradeDefinition
	{
		// Token: 0x06003688 RID: 13960 RVA: 0x000EADB2 File Offset: 0x000E91B2
		public override void OnPurchased(HeroDefinition hero, int level)
		{
			base.OnAttachedToHero(hero, level);
			hero.maxUsesPerTurn += 1;
		}

		// Token: 0x040024FC RID: 9468
		private static DebugChannelGroup dbgGroup = new DebugChannelGroup("Cornucopia", EVerbosity.Quiet, 0);
	}
}
