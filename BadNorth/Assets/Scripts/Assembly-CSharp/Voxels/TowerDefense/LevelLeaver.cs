using System;
using System.Collections;
using System.Collections.Generic;
using CS.Platform;
using RTM.OnScreenDebug;
using UnityEngine;
using Voxels.TowerDefense.CampaignGeneration;
using Voxels.TowerDefense.ProfileInternals;
using Voxels.TowerDefense.UI;
using Voxels.TowerDefense.Upgrades;
using Voxels.TowerDefense.WorldEnvironment;

namespace Voxels.TowerDefense
{
	// Token: 0x02000535 RID: 1333
	public class LevelLeaver : MonoBehaviour, IslandGameplayManager.IAwake, IslandGameplayManager.ISetupIsland, IslandGameplayManager.IWipeIsland
	{
		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x060022A7 RID: 8871 RVA: 0x00065156 File Offset: 0x00063556
		// (set) Token: 0x060022A8 RID: 8872 RVA: 0x00065163 File Offset: 0x00063563
		private Island island
		{
			get
			{
				return this._island;
			}
			set
			{
				this._island.Target = value;
			}
		}

		// Token: 0x060022A9 RID: 8873 RVA: 0x00065171 File Offset: 0x00063571
		void IslandGameplayManager.IAwake.OnAwake(IslandGameplayManager manager)
		{
			this.gameplayManager = manager;
		}

		// Token: 0x060022AA RID: 8874 RVA: 0x0006517A File Offset: 0x0006357A
		void IslandGameplayManager.ISetupIsland.OnSetup(Island island)
		{
			this.island = island;
		}

		// Token: 0x060022AB RID: 8875 RVA: 0x00065183 File Offset: 0x00063583
		void IslandGameplayManager.IWipeIsland.OnWipe(Island island)
		{
			this.island = null;
		}

		// Token: 0x060022AC RID: 8876 RVA: 0x0006518C File Offset: 0x0006358C
		public void ExitToMainMenu()
		{
			LoadingScreen.BeginLoadingPhase(string.Empty, delegate
			{
				Singleton<Stack>.instance.stateMeta.SetActive(true);
			}, new IEnumerator[]
			{
				this.Wrapper(this.gameplayManager.LeaveIslandRoutine(true)),
				MetaMenuHelpers.ExitToMainMenuRoutine()
			});
		}

		// Token: 0x060022AD RID: 8877 RVA: 0x000651E4 File Offset: 0x000635E4
		public void ExitLevel()
		{
			LoadingScreen.BeginLoadingPhase(string.Empty, delegate
			{
				Singleton<Stack>.instance.stateCampaign.SetActive(true);
			}, new IEnumerator[]
			{
				this.Wrapper(this.gameplayManager.LeaveIslandRoutine(!this.gameplayManager.states.loadout.active))
			});
		}

		// Token: 0x060022AE RID: 8878 RVA: 0x0006524A File Offset: 0x0006364A
		public void ReplayLevel()
		{
			LoadingScreen.BeginLoadingPhase(null, null, new IEnumerator[]
			{
				this.Wrapper(this.gameplayManager.ReplayIslandRoutine())
			});
		}

		// Token: 0x060022AF RID: 8879 RVA: 0x0006526D File Offset: 0x0006366D
		public void CompleteLevel()
		{
			LoadingScreen.BeginLoadingPhase(string.Empty, delegate
			{
				Singleton<Stack>.instance.stateCampaign.SetActive(true);
			}, new IEnumerator[]
			{
				this.Wrapper(this.CompleteLevelRoutine())
			});
		}

		// Token: 0x060022B0 RID: 8880 RVA: 0x000652AB File Offset: 0x000636AB
		public void AbandonLevel()
		{
			LoadingScreen.BeginLoadingPhase(string.Empty, delegate
			{
				Singleton<Stack>.instance.stateCampaign.SetActive(true);
			}, new IEnumerator[]
			{
				this.Wrapper(this.AbandonLevelRoutine())
			});
		}

		// Token: 0x060022B1 RID: 8881 RVA: 0x000652EC File Offset: 0x000636EC
		private IEnumerator Wrapper(IEnumerator<GenInfo> routine)
		{
			float minCompleteTime = Time.unscaledTime + 0.25f;
			IEnumerator r = CoroutineUtils.GenerateTimer(20f, routine);
			while (r.MoveNext())
			{
				yield return null;
			}
			while (Time.unscaledTime < minCompleteTime)
			{
				yield return null;
			}
			yield break;
		}

		// Token: 0x060022B2 RID: 8882 RVA: 0x00065308 File Offset: 0x00063708
		private IEnumerator<GenInfo> CompleteLevelRoutine()
		{
			EndOfLevel eol = this.gameplayManager.endOfLevel;
			EndOfLevel.Reason eolReason = eol.reason;
			LevelState.CheckpointState checkpointEvent = eol.checkpointEvent;
			BasePlatformManager.Instance.IncrementStatistic("STAT_ISLAND_VISITED", 1f);
			if (eolReason == EndOfLevel.Reason.Won)
			{
				BasePlatformManager.Instance.IncrementStatistic("STAT_ISLAND_DEFENDED", 1f);
			}
			this.UpdateProfile();
			yield return default(GenInfo);
			Profile.SaveCampaign(this.gameplayManager.endOfLevel.checkpointEvent == LevelState.CheckpointState.Saved);
			yield return new GenInfo("saveCampaign", GenInfo.Mode.forceInterrupt);
			this.PaintExtraSoot();
			yield return default(GenInfo);
			IEnumerator<GenInfo> r = this.gameplayManager.LeaveIslandRoutine(false);
			while (r.MoveNext())
			{
				GenInfo genInfo = r.Current;
				yield return genInfo;
			}
			yield break;
		}

		// Token: 0x060022B3 RID: 8883 RVA: 0x00065324 File Offset: 0x00063724
		private IEnumerator<GenInfo> AbandonLevelRoutine()
		{
			LevelNode levelNode = this.island.levelNode;
			Campaign campaign = levelNode.campaign;
			foreach (Squad squad in this.island.english.allSquads)
			{
				EnglishSquad englishSquad = (EnglishSquad)squad;
				EvacuateAbility upgrade = englishSquad.upgradeManager.GetUpgrade<EvacuateAbility>();
				if (!upgrade || upgrade.state < EvacuateAbility.State.Confirmed)
				{
					englishSquad.hero.alive = false;
				}
			}
			EndOfLevel eol = this.gameplayManager.endOfLevel;
			eol.OnAbandoning();
			this.UpdateHeroStates(eol.reason, levelNode);
			this.UpdateHouseStates(levelNode.levelState, eol.reason);
			yield return default(GenInfo);
			this.PaintExtraSoot();
			yield return default(GenInfo);
			this.UpdateProfile();
			yield return default(GenInfo);
			Profile.SaveCampaign(false);
			yield return new GenInfo("save campaign", GenInfo.Mode.forceInterrupt);
			IEnumerator<GenInfo> r = this.gameplayManager.LeaveIslandRoutine(false);
			while (r.MoveNext())
			{
				GenInfo genInfo = r.Current;
				yield return genInfo;
			}
			yield break;
		}

		// Token: 0x060022B4 RID: 8884 RVA: 0x00065340 File Offset: 0x00063740
		private void UpdateProfile()
		{
			//using ("UpdateProfile")
			{
				LevelNode levelNode = this.island.levelNode;
				Campaign campaign = levelNode.campaign;
				CampaignSave campaignSave = campaign.campaignSave;
				EndOfLevel.Reason reason = this.gameplayManager.endOfLevel.reason;
				GameOverReason gameOverReason = this.gameplayManager.endOfLevel.gameOverReason;
				this.UpdateHeroStates(reason, levelNode);
				this.UpdateStats(campaignSave.stats, campaignSave, levelNode.levelState, reason);
				this.UpdateStats(Profile.userSave.stats, campaignSave, levelNode.levelState, reason);
				this.UpdateHouseStates(levelNode.levelState, reason);
				this.UpdateLevelStates(levelNode, reason);
				this.CollectItem(levelNode);
				this.UpdateCheckpoint(levelNode);
				int totalCoins = this.gameplayManager.coinDispenser.totalCoins;
				campaignSave.coinBank += totalCoins;
				campaignSave.stats.coinsCollected += totalCoins;
				campaignSave.battleCount++;
				LevelState levelState = levelNode.levelState;
				levelState.playCount += 1;
				levelNode.lastReason = reason;
				if (reason == EndOfLevel.Reason.Won)
				{
					campaignSave.battlesWon++;
				}
				if (levelNode.campaign.TestAllDeadGameOver())
				{
					gameOverReason = GameOverReason.AllDead;
				}
				campaignSave.gameOverReason = gameOverReason;
				campaignSave.day = Singleton<EnvironmentManager>.instance.day;
				campaignSave.day.hour = Mathf.Max(campaignSave.day.hour + UnityEngine.Random.Range(0.5f, 1f), campaignSave.turnStartDay.hour + 4f * (float)campaignSave.battleCount);
				campaignSave.weatherSystem = Singleton<WorldWeather>.instance.weatherSystem;
				if (campaignSave.gameOver)
				{
					if (campaignSave.lostGame)
					{
						foreach (HeroDefinition heroDefinition in campaign.campaignSave.heroes)
						{
							if (heroDefinition.recruited)
							{
								heroDefinition.alive = false;
							}
						}
					}
					else if (campaignSave.wonGame)
					{
						Profile.userSave.RegisterCampaignWin(campaign.campaignSave.prefs.difficulty);
					}
				}
				foreach (LevelNode levelNode2 in campaign.pendingUnlocks)
				{
				}
				List<int> deployOrder = campaignSave.deployOrder;
				foreach (Squad squad in this.island.english.allSquads)
				{
					EnglishSquad englishSquad = (EnglishSquad)squad;
					if (englishSquad.hero && !deployOrder.Contains(englishSquad.hero.id))
					{
						deployOrder.Add(englishSquad.hero.id);
					}
				}
			}
		}

		// Token: 0x060022B5 RID: 8885 RVA: 0x000656B8 File Offset: 0x00063AB8
		private void UpdateHeroStates(EndOfLevel.Reason eolReason, LevelNode levelNode)
		{
			//using ("UpdateHeroStates")
			{
				foreach (Squad squad in this.island.english.allSquads)
				{
					EnglishSquad englishSquad = (EnglishSquad)squad;
					HeroDefinition hero = englishSquad.hero;
					HeroDefinition heroDefinition = hero;
					heroDefinition.timesUsedThisTurn += 1;
					hero.stats.islandsVisited++;
					EvacuateAbility upgrade = englishSquad.upgradeManager.GetUpgrade<EvacuateAbility>();
					bool flag = upgrade && upgrade.state != EvacuateAbility.State.None;
					if (flag)
					{
						hero.stats.timesFled++;
					}
					else if (eolReason == EndOfLevel.Reason.Won)
					{
						hero.stats.islandsWon++;
					}
					if (englishSquad.dead || !englishSquad.heroAgent)
					{
						//using ("HeroDied")
						{
							hero.alive = false;
							hero.deathLevelId = this.island.levelNode.index;
							continue;
						}
					}
					if (!hero.recruited)
					{
						hero.recruited = true;
						foreach (SerializableHeroUpgrade serializableHeroUpgrade in hero.upgrades)
						{
							if (serializableHeroUpgrade != null && serializableHeroUpgrade.definition != null)
							{
								bool isStarting = serializableHeroUpgrade.definition.typeEnum == HeroUpgradeTypeEnum.Trait && levelNode.levelState.metaReward && hero == levelNode.heroDefinition;
								Profile.userSave.inventory.Add(serializableHeroUpgrade.definition, serializableHeroUpgrade.level, isStarting);
							}
						}
						BasePlatformManager.Instance.IncrementStatistic("STAT_RECRUIT", 1f);
						Profile.campaign.stats.heroesRecruited++;
						Profile.userSave.stats.heroesRecruited++;
					}
				}
			}
		}

		// Token: 0x060022B6 RID: 8886 RVA: 0x00065968 File Offset: 0x00063D68
		private void UpdateLevelStates(LevelNode levelNode, EndOfLevel.Reason eolReason)
		{
			//using ("UpdateLevelStates")
			{
				Campaign campaign = levelNode.campaign;
				campaign.pendingUnlocks.Clear();
				campaign.lastPlayedLevel = levelNode;
				switch (eolReason)
				{
				case EndOfLevel.Reason.Won:
					foreach (LevelNode levelNode2 in levelNode.connectedLevels)
					{
						if (this.CanUnlock(levelNode2))
						{
							campaign.pendingUnlocks.Add(levelNode2);
						}
					}
					break;
				case EndOfLevel.Reason.Wiped:
					break;
				case EndOfLevel.Reason.Fled:
				{
					LevelNode levelNode3 = null;
					foreach (LevelNode levelNode4 in levelNode.connectedLevels)
					{
						if (this.CanUnlock(levelNode4) && this.IsCloserToEnd(levelNode4, levelNode3))
						{
							levelNode3 = levelNode4;
						}
					}
					if (levelNode3)
					{
						campaign.pendingUnlocks.Add(levelNode3);
					}
					break;
				}
				default:
					throw new NotImplementedException(string.Format("unknown reason {0}", eolReason));
				}
				foreach (LevelNode levelNode5 in campaign.pendingUnlocks)
				{
					levelNode5.levelState.unlocked = true;
				}
			}
		}

		// Token: 0x060022B7 RID: 8887 RVA: 0x00065B5C File Offset: 0x00063F5C
		private bool CanUnlock(LevelNode level)
		{
			return !level.levelState.unlocked && !level.IsBehindFrontier();
		}

		// Token: 0x060022B8 RID: 8888 RVA: 0x00065B7A File Offset: 0x00063F7A
		private bool IsCloserToEnd(LevelNode a, LevelNode b)
		{
			return !b || a.levelState.stepsFromEnd < b.levelState.stepsFromEnd;
		}

		// Token: 0x060022B9 RID: 8889 RVA: 0x00065BA8 File Offset: 0x00063FA8
		private void UpdateHouseStates(LevelState levelState, EndOfLevel.Reason eolReason)
		{
			//using ("UpdateHouseStates")
			{
				House[] houses = this.island.village.houses;
				HouseState[] houses2 = levelState.houses;
				bool flag = true;
				int i = 0;
				int num = houses.Length;
				while (i < num)
				{
					bool flag2 = eolReason == EndOfLevel.Reason.Won && !houses[i].destroyed;
					flag = (flag && flag2);
					houses2[i].condition = ((!flag2) ? HouseState.Condition.Pillaged : HouseState.Condition.Saved);
					i++;
				}
				if (flag)
				{
					BasePlatformManager.Instance.IncrementStatistic("STAT_PERFECT_ISLAND", 1f);
					Profile.campaign.perfectDefenceStreak++;
					if (Profile.campaign.perfectDefenceStreak == 10)
					{
						BasePlatformManager.Instance.UnlockAchievement("ACHIEVEMENT_PROTECTOR_OF_THE_REALM");
					}
				}
				else
				{
					Profile.campaign.perfectDefenceStreak = 0;
				}
			}
		}

		// Token: 0x060022BA RID: 8890 RVA: 0x00065CB0 File Offset: 0x000640B0
		private void UpdateStats(CampaignStats stats, CampaignSave campaignSave, LevelState levelState, EndOfLevel.Reason eolReason)
		{
			//using ("UpdateStats")
			{
				bool flag = levelState.playCount == 0;
				int num = (!flag) ? 0 : 1;
				stats.islandsVisited++;
				if (flag)
				{
					stats.uniqueIslandsVisited++;
				}
				if (this.island.levelNode.isStart && flag)
				{
					foreach (HeroDefinition heroDefinition in campaignSave.heroes)
					{
						stats.heroesRecruited += ((!heroDefinition.available) ? 0 : 1);
					}
				}
				bool flag2 = levelState.checkpointState == LevelState.CheckpointState.Available;
				switch (eolReason)
				{
				case EndOfLevel.Reason.Won:
					stats.islandsDefended++;
					stats.uniqueIslandsDefended += num;
					break;
				case EndOfLevel.Reason.Wiped:
					stats.islandsLost++;
					stats.uniqueIslandsLost += num;
					break;
				case EndOfLevel.Reason.Fled:
					stats.islandsLost++;
					stats.uniqueIslandsLost += num;
					stats.islandsFled++;
					break;
				}
			}
		}

		// Token: 0x060022BB RID: 8891 RVA: 0x00065E5C File Offset: 0x0006425C
		private void CollectItem(LevelNode levelNode)
		{
			HeroUpgradeDefinition levelItem = this.gameplayManager.endOfLevel.levelItem;
			if (levelItem != null)
			{
				CampaignSave campaignSave = levelNode.campaign.campaignSave;
				campaignSave.inventory.Add(levelItem);
				Profile.userSave.inventory.Add(levelItem, 0, levelNode.levelState.metaReward);
				foreach (SerializableHeroUpgrade serializableHeroUpgrade in campaignSave.inventory)
				{
				}
				BasePlatformManager.Instance.IncrementStatistic("STAT_ITEMS", 1f);
			}
		}

		// Token: 0x060022BC RID: 8892 RVA: 0x00065F1C File Offset: 0x0006431C
		private void UpdateCheckpoint(LevelNode levelNode)
		{
			LevelState.CheckpointState checkpointEvent = this.gameplayManager.endOfLevel.checkpointEvent;
			switch (checkpointEvent)
			{
			case LevelState.CheckpointState.None:
				return;
			case LevelState.CheckpointState.Destroyed:
				levelNode.levelState.checkpointState = checkpointEvent;
				Profile.campaign.stats.checkpointsLost++;
				Profile.userSave.stats.checkpointsLost++;
				return;
			case LevelState.CheckpointState.Saved:
				foreach (LevelNode levelNode2 in levelNode.campaign.levels)
				{
					LevelState levelState = levelNode2.levelState;
					if (levelState.checkpointState == LevelState.CheckpointState.Current)
					{
						levelState.checkpointState = LevelState.CheckpointState.Saved;
						levelNode2.behindFrontier.SetActive(levelNode2.IsBehindFrontier());
					}
				}
				levelNode.levelState.checkpointState = LevelState.CheckpointState.Current;
				Profile.campaign.stats.checkpointsSaved++;
				Profile.userSave.stats.checkpointsSaved++;
				return;
			}
			throw new NotImplementedException("Invalid Checkpoint event - " + checkpointEvent);
		}

		// Token: 0x060022BD RID: 8893 RVA: 0x00066068 File Offset: 0x00064468
		private void PaintExtraSoot()
		{
			//using ("PaintExtraSoot")
			{
				House[] houses = this.island.village.houses;
				HouseState[] houses2 = this.island.levelNode.levelState.houses;
				int i = 0;
				int num = houses.Length;
				while (i < num)
				{
					if (!houses[i].destroyed && houses2[i].condition == HouseState.Condition.Pillaged)
					{
						houses[i].PaintSoot();
					}
					i++;
				}
			}
		}

		// Token: 0x0400152E RID: 5422
		private DebugChannelGroup dbgGroup = new DebugChannelGroup("LevelLeaver", EVerbosity.Quiet, 0);

		// Token: 0x0400152F RID: 5423
		private IslandGameplayManager gameplayManager;

		// Token: 0x04001530 RID: 5424
		private RTM.Utilities.WeakReference<Island> _island = null;
	}
}
