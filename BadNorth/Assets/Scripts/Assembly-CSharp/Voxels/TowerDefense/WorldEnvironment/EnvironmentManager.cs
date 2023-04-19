using System;
using UnityEngine;
using Voxels.TowerDefense.ProfileInternals;

namespace Voxels.TowerDefense.WorldEnvironment
{
	// Token: 0x0200086B RID: 2155
	public class EnvironmentManager : Singleton<EnvironmentManager>, IslandGameplayManager.IAwake, IslandGameplayManager.ISetupIsland, IslandGameplayManager.IWipeIsland
	{
		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x0600387C RID: 14460 RVA: 0x000F4666 File Offset: 0x000F2A66
		public Year year => Profile.campaign.year2;

		// Token: 0x0600387D RID: 14461 RVA: 0x000F4674 File Offset: 0x000F2A74
		void IslandGameplayManager.IAwake.OnAwake(IslandGameplayManager manager)
		{
			this.selectableState = manager.states.Selectable;
			this.tutorialState = manager.states.tutorial;
			this.gameWonState = manager.states.gameWon;
			this.starters = base.GetComponentsInChildren<IStartLevel>(true);
			this.enders = base.GetComponentsInChildren<IEndLevel>(true);
		}

		// Token: 0x0600387E RID: 14462 RVA: 0x000F46CE File Offset: 0x000F2ACE
		private void Update()
		{
			if (this.ShouldAdvanceTime())
			{
				this.day.value = this.day.value + Time.deltaTime * this.daysPerSecond;
			}
		}

		// Token: 0x0600387F RID: 14463 RVA: 0x000F46F9 File Offset: 0x000F2AF9
		private bool ShouldAdvanceTime()
		{
			return this.overridePause || this.gameWonState.active || (this.selectableState.active && !this.tutorialState.active);
		}

		// Token: 0x06003880 RID: 14464 RVA: 0x000F473C File Offset: 0x000F2B3C
		void IslandGameplayManager.ISetupIsland.OnSetup(Island island)
		{
			base.gameObject.SetActive(true);
			LevelNode levelNode = island.levelNode;
			CampaignSave campaignSave = levelNode.campaign.campaignSave;
			this.day = campaignSave.day;
			foreach (IStartLevel startLevel in this.starters)
			{
				startLevel.StartLevel(this, island.levelNode);
			}
		}

		void IslandGameplayManager.IWipeIsland.OnWipe(Island island)
		{
			base.gameObject.SetActive(false);
			foreach (IEndLevel endLevel in this.enders)
			{
				endLevel.EndLevel(this, island.levelNode);
			}
		}

		[Space]
		public float daysPerSecond = 0.001f;

		public Day day;

		public bool overridePause;

		private IStartLevel[] starters;

		private IEndLevel[] enders;

		private State selectableState;

		private State tutorialState;

		private State gameWonState;

		public interface IStartLevel
		{
			void StartLevel(EnvironmentManager envManager, LevelNode level);
		}

		public interface IEndLevel
		{
			void EndLevel(EnvironmentManager envManager, LevelNode level);
		}
	}
}
