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
		public Year year
		{
			get
			{
				return Profile.campaign.year2;
			}
		}

		// Token: 0x0600387D RID: 14461 RVA: 0x000F4674 File Offset: 0x000F2A74
		void IslandGameplayManager.IAwake.OnAwake(IslandGameplayManager manager)
		{
			this.selectableState = manager.states.Selectable;
			this.tutorialState = manager.states.tutorial;
			this.gameWonState = manager.states.gameWon;
			this.starters = base.GetComponentsInChildren<EnvironmentManager.IStartLevel>(true);
			this.enders = base.GetComponentsInChildren<EnvironmentManager.IEndLevel>(true);
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
			foreach (EnvironmentManager.IStartLevel startLevel in this.starters)
			{
				startLevel.StartLevel(this, island.levelNode);
			}
		}

		// Token: 0x06003881 RID: 14465 RVA: 0x000F47A8 File Offset: 0x000F2BA8
		void IslandGameplayManager.IWipeIsland.OnWipe(Island island)
		{
			base.gameObject.SetActive(false);
			foreach (EnvironmentManager.IEndLevel endLevel in this.enders)
			{
				endLevel.EndLevel(this, island.levelNode);
			}
		}

		// Token: 0x04002676 RID: 9846
		[Space]
		public float daysPerSecond = 0.001f;

		// Token: 0x04002677 RID: 9847
		public Day day;

		// Token: 0x04002678 RID: 9848
		public bool overridePause;

		// Token: 0x04002679 RID: 9849
		private EnvironmentManager.IStartLevel[] starters;

		// Token: 0x0400267A RID: 9850
		private EnvironmentManager.IEndLevel[] enders;

		// Token: 0x0400267B RID: 9851
		private State selectableState;

		// Token: 0x0400267C RID: 9852
		private State tutorialState;

		// Token: 0x0400267D RID: 9853
		private State gameWonState;

		// Token: 0x0200086C RID: 2156
		public interface IStartLevel
		{
			// Token: 0x06003882 RID: 14466
			void StartLevel(EnvironmentManager envManager, LevelNode level);
		}

		// Token: 0x0200086D RID: 2157
		public interface IEndLevel
		{
			// Token: 0x06003883 RID: 14467
			void EndLevel(EnvironmentManager envManager, LevelNode level);
		}
	}
}
