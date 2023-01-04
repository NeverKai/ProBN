using System;
using System.Collections;
using RTM.Pools;
using RTM.Utilities;
using UnityEngine;
using Voxels.TowerDefense.ProfileInternals;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x020008B0 RID: 2224
	public class GameOverStatsUI : MonoBehaviour, IGameSetup
	{
		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x06003A58 RID: 14936 RVA: 0x00102653 File Offset: 0x00100A53
		// (set) Token: 0x06003A59 RID: 14937 RVA: 0x0010265B File Offset: 0x00100A5B
		public RectTransform rectTransform { get; private set; }

		// Token: 0x06003A5A RID: 14938 RVA: 0x00102664 File Offset: 0x00100A64
		void IGameSetup.OnGameAwake()
		{
			this.rectTransform = (RectTransform)base.transform;
			this.statsWidgets = new LocalPool<GameOverStatWidget>(base.GetComponentsInChildren<GameOverStatWidget>(true), null);
			this.statsWidgets.ExpandTo(16);
		}

		// Token: 0x06003A5B RID: 14939 RVA: 0x00102697 File Offset: 0x00100A97
		public void Clear()
		{
			this.statsWidgets.ReturnAll();
		}

		// Token: 0x06003A5C RID: 14940 RVA: 0x001026A4 File Offset: 0x00100AA4
		public IEnumerator ShowStats(CampaignStats stats, GameOverReason reason, float timing)
		{
			Difficulty difficulty = Profile.campaign.prefs.difficulty;
			this.statsWidgets.GetInstance().SetupLocalized(reason, "GAME_OVER/STATS/DIFFICULTY", difficulty.GetLocTerm());
			if (reason != GameOverReason.Won)
			{
				this.statsWidgets.GetInstance().Setup(reason, "GAME_OVER/STATS/PERCENT_COMPLETE", IntStringCache.percent.Get(Profile.campaign.PercentComplete()));
			}
			this.statsWidgets.GetInstance().Setup(reason, "GAME_OVER/STATS/VIKINGS_KILLED", stats.GetVikingsKilled());
			this.statsWidgets.GetInstance().Setup(reason, "GAME_OVER/STATS/ENGLISH_KILLED", stats.englishKilled);
			this.statsWidgets.GetInstance().Setup(reason, "GAME_OVER/STATS/GOLD_COLLECTED", stats.coinsCollected);
			this.statsWidgets.GetInstance().Setup(reason, "GAME_OVER/STATS/HEROES_RECRUITED", stats.heroesRecruited);
			this.statsWidgets.GetInstance().Setup(reason, "GAME_OVER/STATS/HEROES_SURVIVED", this.GetTotalHeroesSurvived());
			this.statsWidgets.GetInstance().Setup(reason, "GAME_OVER/STATS/ISLANDS_VISITED", stats.uniqueIslandsVisited);
			this.statsWidgets.GetInstance().Setup(reason, "GAME_OVER/STATS/ISLANDS_DEFENDED", stats.uniqueIslandsDefended);
			this.statsWidgets.GetInstance().Setup(reason, "GAME_OVER/STATS/ISLANDS_FLED", stats.islandsFled);
			if (Profile.campaign.hasCheckpoint)
			{
				this.statsWidgets.GetInstance().Setup(reason, "CHECKPOINT/STATS/NUM_SAVED", stats.checkpointsSaved);
				this.statsWidgets.GetInstance().Setup(reason, "CHECKPOINT/STATS/NUM_LOST", stats.checkpointsLost);
			}
			foreach (GameOverStatWidget stat in this.statsWidgets.inUse)
			{
				stat.SetVisible(true);
				float end = Time.unscaledTime + timing;
				while (end > Time.unscaledTime)
				{
					yield return null;
				}
			}
			yield break;
		}

		// Token: 0x06003A5D RID: 14941 RVA: 0x001026D4 File Offset: 0x00100AD4
		private int GetTotalHeroesSurvived()
		{
			int num = 0;
			foreach (HeroDefinition heroDefinition in Profile.campaign.heroes)
			{
				if (heroDefinition.alive && heroDefinition.recruited)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06003A5E RID: 14942 RVA: 0x0010274C File Offset: 0x00100B4C
		public void FadeAway()
		{
			foreach (GameOverStatWidget gameOverStatWidget in this.statsWidgets.inUse)
			{
				gameOverStatWidget.SetVisible(false);
			}
		}

		// Token: 0x04002871 RID: 10353
		private LocalPool<GameOverStatWidget> statsWidgets;
	}
}
