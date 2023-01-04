using System;
using I2.Loc;
using UnityEngine;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x02000553 RID: 1363
	public class IslandTitle : MonoBehaviour, IslandGameplayManager.IAwake, IslandGameplayManager.ISetupIsland
	{
		// Token: 0x0600236A RID: 9066 RVA: 0x0006D9A0 File Offset: 0x0006BDA0
		void IslandGameplayManager.IAwake.OnAwake(IslandGameplayManager manager)
		{
			base.gameObject.SetActive(false);
			manager.endOfLevel.preProcess += this.OnEndOfLevel;
			if (!this.subtitleContainer)
			{
				this.subtitleContainer = this.islandSubtitleText.gameObject;
			}
		}

		// Token: 0x0600236B RID: 9067 RVA: 0x0006D9F4 File Offset: 0x0006BDF4
		void IslandGameplayManager.ISetupIsland.OnSetup(Island island)
		{
			this.islandNameText.Term = island.levelNode.levelState.nameTerm;
			LevelNode levelNode = island.levelNode;
			HeroDefinition heroDefinition = levelNode.heroDefinition;
			this.subtitleContainer.SetActive(false);
			if (!this.showSubtitle)
			{
				return;
			}
			if (levelNode.isStart)
			{
				this.islandSubtitleText.Term = "LOADOUT/START_LEVEL_SUBTITLE";
				this.subtitleContainer.SetActive(true);
			}
			else if (levelNode.isEnd)
			{
				this.islandSubtitleText.Term = "LOADOUT/END_LEVEL_SUBTITLE";
				this.subtitleContainer.SetActive(true);
			}
			else if (heroDefinition)
			{
				this.islandSubtitleText.Term = "LOADOUT/HERO_ISLAND_SUBTITLE";
				this.islandSubtitleParams.SetParameterValue("NAME", heroDefinition.nameTerm, true);
				this.subtitleContainer.SetActive(true);
			}
			else if (levelNode.checkpointAvailable)
			{
				this.islandSubtitleText.Term = "CHECKPOINT/ISLAND_SUBTITLE";
				this.subtitleContainer.SetActive(true);
			}
		}

		// Token: 0x0600236C RID: 9068 RVA: 0x0006DB04 File Offset: 0x0006BF04
		private void OnEndOfLevel(Island i, EndOfLevel.Reason reason)
		{
			switch (reason)
			{
			case EndOfLevel.Reason.Won:
				this.islandSubtitleText.Term = "UI/GAMEPLAY/VICTORY_TITLE";
				break;
			case EndOfLevel.Reason.Wiped:
				this.islandSubtitleText.Term = "UI/GAMEPLAY/DEFEAT_TITLE";
				break;
			case EndOfLevel.Reason.Fled:
				this.islandSubtitleText.Term = "UI/GAMEPLAY/FLED_TITLE";
				break;
			default:
				throw new NotImplementedException(string.Format("unknown reason {0}", reason));
			}
			this.subtitleContainer.SetActive(true);
		}

		// Token: 0x04001614 RID: 5652
		[SerializeField]
		private Localize islandNameText;

		// Token: 0x04001615 RID: 5653
		[SerializeField]
		private GameObject subtitleContainer;

		// Token: 0x04001616 RID: 5654
		[SerializeField]
		private Localize islandSubtitleText;

		// Token: 0x04001617 RID: 5655
		[SerializeField]
		private LocalizationParamsManager islandSubtitleParams;

		// Token: 0x04001618 RID: 5656
		[SerializeField]
		private bool showSubtitle = true;
	}
}
