using System;
using System.Diagnostics;
using CS.Platform;
using RTM.OnScreenDebug;
using UnityEngine;
using Voxels.TowerDefense.CampaignGeneration;
using Voxels.TowerDefense.ProfileInternals;
using Voxels.TowerDefense.UI;
using Voxels.TowerDefense.WorldEnvironment;

namespace Voxels.TowerDefense
{
	// Token: 0x02000529 RID: 1321
	public class EndOfLevel : MonoBehaviour, IslandGameplayManager.IAwake, IslandGameplayManager.ISetupIsland, IslandGameplayManager.IWipeIsland
	{
		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06002232 RID: 8754 RVA: 0x00063237 File Offset: 0x00061637
		private Island island
		{
			get
			{
				return this._island;
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06002233 RID: 8755 RVA: 0x00063244 File Offset: 0x00061644
		// (set) Token: 0x06002234 RID: 8756 RVA: 0x0006324C File Offset: 0x0006164C
		public EndOfLevel.Reason reason { get; private set; }

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06002235 RID: 8757 RVA: 0x00063255 File Offset: 0x00061655
		// (set) Token: 0x06002236 RID: 8758 RVA: 0x0006325D File Offset: 0x0006165D
		public GameOverReason gameOverReason { get; private set; }

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06002237 RID: 8759 RVA: 0x00063266 File Offset: 0x00061666
		// (set) Token: 0x06002238 RID: 8760 RVA: 0x0006326E File Offset: 0x0006166E
		public HeroUpgradeDefinition levelItem { get; private set; }

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06002239 RID: 8761 RVA: 0x00063277 File Offset: 0x00061677
		// (set) Token: 0x0600223A RID: 8762 RVA: 0x0006327F File Offset: 0x0006167F
		public HeroUpgradeDefinition lostLevelItem { get; private set; }

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x0600223B RID: 8763 RVA: 0x00063288 File Offset: 0x00061688
		// (set) Token: 0x0600223C RID: 8764 RVA: 0x00063290 File Offset: 0x00061690
		public LevelState.CheckpointState checkpointEvent { get; private set; }

		// Token: 0x14000078 RID: 120
		// (add) Token: 0x0600223D RID: 8765 RVA: 0x0006329C File Offset: 0x0006169C
		// (remove) Token: 0x0600223E RID: 8766 RVA: 0x000632D4 File Offset: 0x000616D4
		
		public event Action<Island, EndOfLevel.Reason> preProcess = delegate(Island A_0, EndOfLevel.Reason A_1)
		{
		};

		// Token: 0x14000079 RID: 121
		// (add) Token: 0x0600223F RID: 8767 RVA: 0x0006330C File Offset: 0x0006170C
		// (remove) Token: 0x06002240 RID: 8768 RVA: 0x00063344 File Offset: 0x00061744
		
		public event Action<Island> postProcess = delegate(Island A_0)
		{
		};

		// Token: 0x06002241 RID: 8769 RVA: 0x0006337A File Offset: 0x0006177A
		void IslandGameplayManager.IAwake.OnAwake(IslandGameplayManager manager)
		{
			this.manager = manager;
		}

		// Token: 0x06002242 RID: 8770 RVA: 0x00063384 File Offset: 0x00061784
		void IslandGameplayManager.ISetupIsland.OnSetup(Island island)
		{
			this._island.Target = island;
			this.reason = EndOfLevel.Reason.None;
			this.gameOverReason = GameOverReason.None;
			this.levelItem = ((!island.levelNode.hasItem) ? null : island.levelNode.item);
			LevelState.CheckpointState checkpoint = island.levelNode.checkpoint;
			this.checkpointEvent = ((checkpoint != LevelState.CheckpointState.Available) ? LevelState.CheckpointState.None : LevelState.CheckpointState.Available);
		}

		// Token: 0x06002243 RID: 8771 RVA: 0x000633F2 File Offset: 0x000617F2
		void IslandGameplayManager.IWipeIsland.OnWipe(Island island)
		{
			this._island.Target = null;
		}

		// Token: 0x06002244 RID: 8772 RVA: 0x00063400 File Offset: 0x00061800
		public void OnAbandoning()
		{
			this.reason = EndOfLevel.Reason.Wiped;
		}

		// Token: 0x06002245 RID: 8773 RVA: 0x00063409 File Offset: 0x00061809
		public void AllVikingsKilled()
		{
			this.ProcessEOL(EndOfLevel.Reason.Won);
		}

		// Token: 0x06002246 RID: 8774 RVA: 0x00063412 File Offset: 0x00061812
		public void AllEnglishKilled()
		{
			this.ProcessEOL(EndOfLevel.Reason.Wiped);
		}

		// Token: 0x06002247 RID: 8775 RVA: 0x0006341B File Offset: 0x0006181B
		public void Evacuated()
		{
			this.ProcessEOL(EndOfLevel.Reason.Fled);
		}

		// Token: 0x06002248 RID: 8776 RVA: 0x00063424 File Offset: 0x00061824
		private void ProcessEOL(EndOfLevel.Reason reason)
		{
			//using ("ProcessEndOfLevel")
			{
				this.reason = reason;
				this.MakeHousesInvulnerable();
				this.NotifySquads();
				this.preProcess(this.island, reason);
				bool flag = reason == EndOfLevel.Reason.Won && this.island.levelNode.isEnd;
				if (flag)
				{
					this.gameOverReason = GameOverReason.Won;
				}
				this.TestItem();
				this.TestCheckpoint();
				this.TriggerEOLAchievements();
				if (this.gameOverReason == GameOverReason.Won)
				{
					this.wonGameScreen.OpenMenu(this.island);
				}
				else
				{
					//using ("TriggerEndOfLevel")
					{
						//using ("PostProcess Delegate")
						{
							this.postProcess(this.island);
						}
						//using ("EndOfLevel StateChange")
						{
							this.manager.states.EndOfLevel.SetActive(true);
						}
					}
				}
			}
		}

		// Token: 0x06002249 RID: 8777 RVA: 0x000635B8 File Offset: 0x000619B8
		private void TriggerEOLAchievements()
		{
			LevelNode levelNode = this.island.levelNode;
			Campaign campaign = levelNode.campaign;
			CampaignSave campaignSave = campaign.campaignSave;
			if (this.reason == EndOfLevel.Reason.Won)
			{
				if (campaignSave.battlesWon == 2)
				{
					BasePlatformManager.Instance.UnlockAchievement("ACHIEVEMENT_SPLIT_THE_PARTY");
				}
				if (Singleton<WorldWeather>.instance.hasBeenNightOnIsland)
				{
					BasePlatformManager.Instance.UnlockAchievement("ACHIEVEMENT_NIGHTWATCH");
				}
				if (Singleton<WorldWeather>.instance.hasSnowedOnIsland)
				{
					BasePlatformManager.Instance.UnlockAchievement("ACHIEVEMENT_COLD_STEEL");
				}
			}
			if (this.gameOverReason == GameOverReason.Won)
			{
				BasePlatformManager.Instance.UnlockAchievement("ACHIEVEMENT_A_NEW_HOME");
				if (campaignSave.prefs.difficulty >= Difficulty.Hard)
				{
					bool flag = true;
					foreach (HeroDefinition heroDefinition in campaignSave.heroes)
					{
						flag &= (heroDefinition.recruited && heroDefinition.alive);
					}
					foreach (Squad squad in this.island.english.allSquads)
					{
						flag &= squad.alive;
					}
					if (flag)
					{
						BasePlatformManager.Instance.UnlockAchievement("ACHIEVEMENT_FOLK_HERO");
					}
				}
			}
		}

		// Token: 0x0600224A RID: 8778 RVA: 0x00063744 File Offset: 0x00061B44
		private void TestItem()
		{
			if (this.reason != EndOfLevel.Reason.Won)
			{
				this.lostLevelItem = this.levelItem;
				this.levelItem = null;
			}
			else
			{
				this.lostLevelItem = null;
			}
		}

		// Token: 0x0600224B RID: 8779 RVA: 0x00063774 File Offset: 0x00061B74
		private void TestCheckpoint()
		{
			LevelState.CheckpointState checkpointEvent = this.checkpointEvent;
			CheckpointHouse checkpointHouse = this.island.village.checkpointHouse;
			if (this.checkpointEvent == LevelState.CheckpointState.Available && checkpointHouse != null)
			{
				if (this.reason == EndOfLevel.Reason.Won && !checkpointHouse.house.destroyed)
				{
					this.checkpointEvent = LevelState.CheckpointState.Saved;
				}
				else
				{
					this.checkpointEvent = LevelState.CheckpointState.Destroyed;
				}
			}
			else
			{
				this.checkpointEvent = LevelState.CheckpointState.None;
			}
		}

		// Token: 0x0600224C RID: 8780 RVA: 0x000637EC File Offset: 0x00061BEC
		private void MakeHousesInvulnerable()
		{
			foreach (House house in this.island.village.houses)
			{
				house.SetInvulnerable();
				if (this.reason != EndOfLevel.Reason.Won)
				{
					house.SetSuppressAudio(true);
				}
			}
		}

		// Token: 0x0600224D RID: 8781 RVA: 0x0006383C File Offset: 0x00061C3C
		private void NotifySquads()
		{
			//using ("NotifySquads")
			{
				foreach (Squad squad in this.island.english.allSquads)
				{
					squad.LevelEnded(this.reason);
				}
				foreach (Squad squad2 in this.island.vikings.allSquads)
				{
					squad2.LevelEnded(this.reason);
				}
			}
		}

		// Token: 0x040014EA RID: 5354
		[SerializeField]
		private WonGameScreen wonGameScreen;

		// Token: 0x040014EB RID: 5355
		private DebugChannelGroup dbgGroup = new DebugChannelGroup("EndOfLevel", EVerbosity.Quiet, 0);

		// Token: 0x040014EC RID: 5356
		private IslandGameplayManager manager;

		// Token: 0x040014ED RID: 5357
		private RTM.Utilities.WeakReference<Island> _island = new RTM.Utilities.WeakReference<Island>(null);

		// Token: 0x0200052A RID: 1322
		public enum Reason
		{
			// Token: 0x040014F8 RID: 5368
			None,
			// Token: 0x040014F9 RID: 5369
			Won,
			// Token: 0x040014FA RID: 5370
			Wiped,
			// Token: 0x040014FB RID: 5371
			Fled
		}
	}
}
