using System;
using Voxels.TowerDefense.HeroGeneration;

namespace Voxels.TowerDefense
{
	// Token: 0x02000832 RID: 2098
	internal class HeroTraitSturdy : HeroUpgradeAgentComponent<HeroTraitSturdyComponent>
	{
		// Token: 0x060036AF RID: 13999 RVA: 0x000EB7DF File Offset: 0x000E9BDF
		public override void OnAttachedToMonoHero(MonoHero monoHero, int level)
		{
			base.OnAttachedToMonoHero(monoHero, level);
			monoHero.onMinionPrefabChanged += this.MonoHero_onMinionChanged;
			this.MonoHero_onMinionChanged(monoHero.minionPrefab);
		}

		// Token: 0x060036B0 RID: 14000 RVA: 0x000EB808 File Offset: 0x000E9C08
		private void MonoHero_onMinionChanged(Agent agent)
		{
			Spear component = agent.GetComponent<Spear>();
			if (component)
			{
				int i = 0;
				int num = component.attackSettings.Length;
				while (i < num)
				{
					AttackSettings[] attackSettings = component.attackSettings;
					int num2 = i;
					attackSettings[num2].knockback = attackSettings[num2].knockback * 1.25f;
					i++;
				}
			}
		}
	}
}
