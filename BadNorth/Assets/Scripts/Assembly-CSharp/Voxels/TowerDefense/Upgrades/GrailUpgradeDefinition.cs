using System;
using System.Collections;

namespace Voxels.TowerDefense.Upgrades
{
	// Token: 0x02000829 RID: 2089
	internal class GrailUpgradeDefinition : HeroUpgradeDefinition
	{
		// Token: 0x0600368B RID: 13963 RVA: 0x000EADE6 File Offset: 0x000E91E6
		public override bool AvailableFor(HeroDefinition hero, int atLevel)
		{
			return hero.recruited && !hero.alive;
		}

		// Token: 0x0600368C RID: 13964 RVA: 0x000EAE00 File Offset: 0x000E9200
		public override void OnPurchased(HeroDefinition hero, int level)
		{
			base.OnPurchased(hero, level);
			hero.alive = true;
			hero.deathLevelId = -1;
			IEnumerator enumerator = hero.monoHero.UpdateAliveSprite(true, true).GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
		}
	}
}
