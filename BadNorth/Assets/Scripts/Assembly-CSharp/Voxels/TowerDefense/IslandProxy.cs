using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using Voxels.TowerDefense.CampaignGeneration;

namespace Voxels.TowerDefense
{
	// Token: 0x0200061D RID: 1565
	public class IslandProxy : ChildComponent<LevelNode>, IComparable<IslandProxy>
	{
		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06002841 RID: 10305 RVA: 0x00084EB3 File Offset: 0x000832B3
		public State state => this._state;

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06002842 RID: 10306 RVA: 0x00084EBB File Offset: 0x000832BB
		public LevelNode levelNode => base.manager;

		// Token: 0x14000089 RID: 137
		// (add) Token: 0x06002843 RID: 10307 RVA: 0x00084EC4 File Offset: 0x000832C4
		// (remove) Token: 0x06002844 RID: 10308 RVA: 0x00084EFC File Offset: 0x000832FC
		
		public event Action<State> onStateChanged = delegate
		{
		};

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06002845 RID: 10309 RVA: 0x00084F32 File Offset: 0x00083332
		public bool isGenerating => this.state != State.None && this.state != State.Ready;

		// Token: 0x06002846 RID: 10310 RVA: 0x00084F50 File Offset: 0x00083350
		private bool ShouldGenerate(int maxStepsFromUnlock)
		{
			if (this.state != IslandProxy.State.None)
			{
				return false;
			}
			LevelState levelState = this.levelNode.levelState;
			bool flag = levelState.hasSprite && levelState.goodSeed && levelState.houses != null;
			if (flag)
			{
				return levelState.playCount == 0 && this.levelNode.stepsFromUnlock <= 1;
			}
			return this.levelNode.stepsFromUnlock <= maxStepsFromUnlock;
		}

		// Token: 0x06002847 RID: 10311 RVA: 0x00084FD4 File Offset: 0x000833D4
		public void GenerateIsland()
		{
			int seed = base.manager.campaign.seed + manager.index * 43271;
			int seed2 = base.manager.levelState.seed;
			List<string> list = this.levelNode.levelState.GetReferencedStrings(LevelObjectReference.Key.Tiles).ToList();
			if (this.levelNode.hasCheckpoint)
			{
				list.Add("Checkpoint");
			}
			this.multiwave = new MultiWave(this.levelNode.name, this.levelNode.size, seed, seed2, this.levelNode.minimumBeach, (int)this.levelNode.levelState.coinTarget, list);
			this.thread = new ThreadWorker();
			Singleton<CampaignManager>.instance.islandGenerator.Add(this);
		}

		// Token: 0x06002848 RID: 10312 RVA: 0x000850A4 File Offset: 0x000834A4
		public Island CreateIsland(Transform container)
		{
			Island result;
			using (new ScopedProfiler("CreateIsland", null))
			{
				this.island = container.gameObject.InstantiateChild(ScriptableObjectSingleton<PrefabManager>.instance.island, this.levelNode.name);
				result = this.island;
			}
			return result;
		}

		// Token: 0x06002849 RID: 10313 RVA: 0x00085110 File Offset: 0x00083510
		public void DestroyIsland()
		{
			if (this.island)
			{
				this.island.DestroyIsland();
			}
			this.island = null;
		}

		// Token: 0x0600284A RID: 10314 RVA: 0x00085134 File Offset: 0x00083534
		public void FinishedGeneration()
		{
			LevelState levelState = this.levelNode.levelState;
			Campaign campaign = this.levelNode.campaign;
			int seed = levelState.seed;
			levelState.seed = this.multiwave.seed2;
			if (levelState.houses == null)
			{
				int num = 0;
				Village village = this.levelNode.islandProxy.island.village;
				int num2 = village.houses.Length;
				levelState.houses = new HouseState[num2];
				for (int i = 0; i < num2; i++)
				{
					levelState.houses[i] = new HouseState(village.houses[i]);
					num += village.houses[i].coinCount;
				}
				levelState.coinCount = (byte)num;
			}
			else
			{
				Village village2 = this.levelNode.islandProxy.island.village;
				int num3 = village2.houses.Length;
				HouseState[] array = new HouseState[num3];
				for (int j = 0; j < num3; j++)
				{
					if (j < levelState.houses.Length)
					{
						array[j] = levelState.houses[j];
						array[j].value = (byte)village2.houses[j].coinCount;
						array[j].SetBounds(village2.houses[j]);
					}
					else
					{
						array[j] = new HouseState(village2.houses[j]);
						array[j].condition = ((levelState.playCount <= 0) ? HouseState.Condition.Intact : HouseState.Condition.Saved);
					}
				}
				levelState.houses = array;
			}
			if ((!levelState.goodSeed || seed != levelState.seed) && campaign.startLevel.IsPlayed())
			{
				Profile.SaveCampaign(false);
			}
			bool goodSeed = levelState.goodSeed;
			levelState.goodSeed = true;
			this.SetState(IslandProxy.State.Ready);
			this.multiwave.Clear();
			this.multiwave = null;
			this.thread = null;
			this.buildPriority = -1;
			this.preparingToPlay = false;
			if (!goodSeed)
			{
				int num4 = (int)(levelState.coinTarget - levelState.coinCount);
				int num5 = 0;
				while (num5 < campaign.levels.Count * 10 && num4 > 0)
				{
					LevelNode levelNode = campaign.levels[num5 % campaign.levels.Count];
					LevelState levelState2 = levelNode.levelState;
					if (!levelState2.goodSeed && levelNode.islandProxy.state == IslandProxy.State.None)
					{
						LevelState levelState3 = levelState2;
						levelState3.coinTarget += 1;
						num4--;
					}
					num5++;
				}
				int num6 = 0;
				while (num6 < campaign.levels.Count * 10 && num4 < 0)
				{
					LevelNode levelNode2 = campaign.levels[num6 % campaign.levels.Count];
					LevelState levelState4 = levelNode2.levelState;
					if (!levelState4.goodSeed && levelNode2.islandProxy.state == IslandProxy.State.None && levelState4.coinTarget > 2)
					{
						LevelState levelState5 = levelState4;
						levelState5.coinTarget -= 1;
						num4++;
					}
					num6++;
				}
			}
		}

		// Token: 0x0600284B RID: 10315 RVA: 0x00085488 File Offset: 0x00083888
		public void SetState(IslandProxy.State newState)
		{
			IslandProxy.State state = this.state;
			this._state = newState;
			if (state != newState)
			{
				this.onStateChanged(newState);
			}
		}

		// Token: 0x0600284C RID: 10316 RVA: 0x000854B6 File Offset: 0x000838B6
		public void MaybeGenerateIsland(int maxStepsFromUnlock)
		{
			if (this.ShouldGenerate(maxStepsFromUnlock))
			{
				this.GenerateIsland();
			}
		}

		// Token: 0x0600284D RID: 10317 RVA: 0x000854CC File Offset: 0x000838CC
		public void MaybeDestroyIsland()
		{
			bool flag = this.levelNode.IsBehindFrontier() && this.state == IslandProxy.State.Ready;
			if (flag)
			{
				this._state = IslandProxy.State.None;
				this.DestroyIsland();
				this.onStateChanged(this.state);
			}
		}

		// Token: 0x0600284E RID: 10318 RVA: 0x0008551A File Offset: 0x0008391A
		public void UpdateBuildPriority()
		{
			this.buildPriority = this.ComputeBuildPriority();
		}

		// Token: 0x0600284F RID: 10319 RVA: 0x00085528 File Offset: 0x00083928
		private int ComputeBuildPriority()
		{
			LevelState levelState = this.levelNode.levelState;
			if (this.levelNode.IsBehindFrontier())
			{
				return (!levelState.hasSprite) ? int.MaxValue : -1;
			}
			if (this.preparingToPlay)
			{
				return 0;
			}
			if (!this.levelNode.IsAvailable())
			{
				return 1000 * (1 + this.levelNode.stepsFromUnlock) + (int)levelState.stepsFromEnd;
			}
			if (levelState.playCount == 0)
			{
				return (int)(100 + levelState.stepsFromStart);
			}
			return 1000 + (int)levelState.stepsFromEnd;
		}

		// Token: 0x06002850 RID: 10320 RVA: 0x000855C1 File Offset: 0x000839C1
		public void CancelGeneration()
		{
			this.thread = null;
			this.multiwave = null;
			this.DestroyIsland();
			this.buildPriority = -1;
			this.preparingToPlay = false;
			this.SetState(State.None);
		}

		// Token: 0x06002851 RID: 10321 RVA: 0x000855EC File Offset: 0x000839EC
		int IComparable<IslandProxy>.CompareTo(IslandProxy other)
		{
			return this.buildPriority - other.buildPriority;
		}

		// Token: 0x06002852 RID: 10322 RVA: 0x000855FB File Offset: 0x000839FB
		public static implicit operator bool(IslandProxy setup)
		{
			return setup != null;
		}

		// Token: 0x040019CA RID: 6602
		public MultiWave multiwave;

		// Token: 0x040019CB RID: 6603
		public ThreadWorker thread;

		// Token: 0x040019CC RID: 6604
		public Island island;

		// Token: 0x040019CD RID: 6605
		public IslandProxy.State _state;

		// Token: 0x040019CE RID: 6606
		public bool preparingToPlay;

		// Token: 0x040019CF RID: 6607
		public int buildPriority = -1;

		// Token: 0x040019D1 RID: 6609
		public string failName;

		// Token: 0x0200061E RID: 1566
		public enum State
		{
			None,
			GenerateQueue,
			Generating,
			BuildQueue,
			Building,
			Ready
		}
	}
}
