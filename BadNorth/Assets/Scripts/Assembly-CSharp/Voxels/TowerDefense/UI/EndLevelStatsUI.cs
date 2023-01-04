using System;
using System.Collections.Generic;
using RTM.Pools;
using UnityEngine;
using Voxels.TowerDefense.Upgrades;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x0200054B RID: 1355
	public class EndLevelStatsUI : MonoBehaviour, IslandUIManager.IAwake, IslandGameplayManager.IWipeIsland
	{
		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x0600233A RID: 9018 RVA: 0x0006AA5E File Offset: 0x00068E5E
		public bool canAssignAny
		{
			get
			{
				return this.coinAssignable.Count > 0;
			}
		}

		// Token: 0x0600233B RID: 9019 RVA: 0x0006AA6E File Offset: 0x00068E6E
		void IslandUIManager.IAwake.OnAwake(IslandUIManager manager)
		{
			this.manager = manager;
			this.heroPerformancePool = new LocalPool<EndLevelHeroPerformanceUI>(this.heroPerformancePrefab, this.heroPerformancePrefab.transform.parent);
			this.heroPerformancePool.ExpandTo(6);
		}

		// Token: 0x0600233C RID: 9020 RVA: 0x0006AAA4 File Offset: 0x00068EA4
		public void Setup(Island island)
		{
			using ("EndLevelStatsUI.postProcessEndOfLevel")
			{
				foreach (Squad squad in island.english.allSquads)
				{
					EnglishSquad englishSquad = (EnglishSquad)squad;
					HeroDefinition hero = englishSquad.hero;
					EvacuateAbility upgrade = englishSquad.upgradeManager.GetUpgrade<EvacuateAbility>();
					bool flag = upgrade && upgrade.state != EvacuateAbility.State.None;
					bool recruited = island.levelNode.heroDefinition == hero;
					EndLevelHeroPerformanceUI instance = this.heroPerformancePool.GetInstance();
					instance.gameObject.SetActive(true);
					instance.Setup(this.manager.gameplayManager.coinDispenser, hero, !englishSquad.heroAlive, flag, recruited);
					if (hero.alive && !flag)
					{
						this.coinAssignable.Add(instance);
					}
				}
			}
			this.autoCoinIdx = 0;
		}

		// Token: 0x0600233D RID: 9021 RVA: 0x0006ABD4 File Offset: 0x00068FD4
		void IslandGameplayManager.IWipeIsland.OnWipe(Island island)
		{
			this.heroPerformancePool.ReturnAll();
			this.coinAssignable.Clear();
		}

		// Token: 0x040015CB RID: 5579
		[Header("HeroPerformance")]
		[SerializeField]
		private EndLevelHeroPerformanceUI heroPerformancePrefab;

		// Token: 0x040015CC RID: 5580
		private LocalPool<EndLevelHeroPerformanceUI> heroPerformancePool;

		// Token: 0x040015CD RID: 5581
		private IslandUIManager manager;

		// Token: 0x040015CE RID: 5582
		private List<EndLevelHeroPerformanceUI> coinAssignable = new List<EndLevelHeroPerformanceUI>();

		// Token: 0x040015CF RID: 5583
		private int autoCoinIdx;
	}
}
