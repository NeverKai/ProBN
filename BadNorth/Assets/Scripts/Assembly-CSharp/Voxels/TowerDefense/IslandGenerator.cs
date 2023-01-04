using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using ReflexCLI.Attributes;
using RTM.OnScreenDebug;
using UnityEngine;
using Voxels.TowerDefense.CampaignGeneration;

namespace Voxels.TowerDefense
{
	// Token: 0x0200061A RID: 1562
	public class IslandGenerator : MonoBehaviour
	{
		// Token: 0x06002812 RID: 10258 RVA: 0x00082FCD File Offset: 0x000813CD
		private void Awake()
		{
			Singleton<Stack>.instance.stateMeta.OnActivate += this.Clear;
		}

		// Token: 0x06002813 RID: 10259 RVA: 0x00082FEA File Offset: 0x000813EA
		public void Add(IslandProxy level)
		{
			this.queue.Add(level, true);
		}

		// Token: 0x06002814 RID: 10260 RVA: 0x00082FFC File Offset: 0x000813FC
		public void QueueUpGenerations(Campaign campaign)
		{
			int generationDepth = campaign.GetGenerationDepth();
			foreach (LevelNode levelNode in campaign.levels)
			{
				levelNode.islandProxy.MaybeGenerateIsland(generationDepth);
				levelNode.islandProxy.UpdateBuildPriority();
			}
			this.queue.RemoveUnbuildables();
			this.queue.Sort();
		}

		// Token: 0x06002815 RID: 10261 RVA: 0x00083088 File Offset: 0x00081488
		public void DestroyOldIslands(Campaign campaign)
		{
			foreach (LevelNode levelNode in campaign.levels)
			{
				levelNode.islandProxy.MaybeDestroyIsland();
			}
		}

		// Token: 0x06002816 RID: 10262 RVA: 0x000830E8 File Offset: 0x000814E8
		public static void AddBlocker(object coroutineBlocker, object threadBlocker)
		{
			if (coroutineBlocker != null && !IslandGenerator.coroutineBlockers.Contains(coroutineBlocker))
			{
				IslandGenerator.coroutineBlockers.Add(coroutineBlocker);
			}
			if (threadBlocker != null && !IslandGenerator.threadBlockers.Contains(threadBlocker))
			{
				IslandGenerator.threadBlockers.Add(threadBlocker);
			}
			if (IslandGenerator.instance)
			{
				IslandGenerator.instance.UpdateBlockage();
			}
		}

		// Token: 0x06002817 RID: 10263 RVA: 0x00083150 File Offset: 0x00081550
		public static void RemoveBlocker(object coroutineBlocker, object threadBlocker)
		{
			IslandGenerator.coroutineBlockers.Remove(coroutineBlocker);
			IslandGenerator.threadBlockers.Remove(threadBlocker);
			if (IslandGenerator.instance)
			{
				IslandGenerator.instance.UpdateBlockage();
			}
		}

		// Token: 0x06002818 RID: 10264 RVA: 0x00083184 File Offset: 0x00081584
		private void UpdateBlockage()
		{
			if (IslandGenerator.threadBlockers.Count == 0)
			{
				if (!this.queue.enabled)
				{
					this.queue.OnEnable();
				}
			}
			else if (this.queue.enabled)
			{
				this.queue.OnDisable();
			}
			if (this.currentIslandProxy && this.currentIslandProxy.island)
			{
				this.currentIslandProxy.island.gameObject.SetActive(IslandGenerator.coroutineBlockers.Count == 0);
			}
		}

		// Token: 0x06002819 RID: 10265 RVA: 0x00083224 File Offset: 0x00081624
		private void Update()
		{
			if (IslandGenerator.threadBlockers.Count == 0)
			{
				this.queue.Update();
			}
			if (IslandGenerator.coroutineBlockers.Count == 0 && this.timerCoroutine != null)
			{
				this.timerCoroutine.MoveNext();
			}
			using ("DBG")
			{
				foreach (object obj in IslandGenerator.threadBlockers)
				{
				}
				foreach (object obj2 in IslandGenerator.coroutineBlockers)
				{
				}
			}
		}

		// Token: 0x0600281A RID: 10266 RVA: 0x0008332C File Offset: 0x0008172C
		private IEnumerator<GenInfo> InfiniteEnumerator()
		{
			for (;;)
			{
				this.currentIslandProxy = this.queue.TryPop();
				if (this.currentIslandProxy)
				{
					Campaign campaign = this.currentIslandProxy.levelNode.campaign;
					this.currentIslandProxy.SetState(IslandProxy.State.Building);
					this.currentIslandProxy.CreateIsland(this.islandContainer);
					yield return new GenInfo("Instantiate Island", GenInfo.Mode.forceInterrupt);
					Island island = this.currentIslandProxy.island;
					bool broken = false;
					IEnumerator<GenInfo> islandSetupRoutine = island.SetupRoutine(this.currentIslandProxy.levelNode, this.currentIslandProxy.multiwave);
					while (islandSetupRoutine.MoveNext())
					{
						GenInfo genInfo = islandSetupRoutine.Current;
						if (genInfo.broken)
						{
							string failName = this.currentIslandProxy.failName;
							GenInfo genInfo2 = islandSetupRoutine.Current;
							IslandGenerationData.AddFailData(failName, genInfo2.text);
							this.currentIslandProxy.DestroyIsland();
							this.queue.Add(this.currentIslandProxy, true);
							broken = true;
							this.currentIslandProxy = null;
							GenInfo genInfo3 = islandSetupRoutine.Current;
							UnityEngine.Debug.Log(genInfo3.text);
							yield return new GenInfo("Broken island", GenInfo.Mode.interruptable);
							break;
						}
						yield return islandSetupRoutine.Current;
					}
					if (!broken)
					{
						this.currentIslandProxy.FinishedGeneration();
						this.currentIslandProxy = null;
						island.gameObject.SetActive(false);
						island = null;
						if (!this.queue.ContainsAny() && campaign.startLevel.IsPlayed())
						{
							yield return new GenInfo("Pre-saving for all Islands built", GenInfo.Mode.forceInterrupt);
							Profile.SaveCampaign(false);
						}
						yield return new GenInfo("Island Done", GenInfo.Mode.forceInterrupt);
					}
				}
				else
				{
					yield return new GenInfo("Nothing", GenInfo.Mode.forceInterrupt);
				}
			}
			yield break;
		}

		// Token: 0x0600281B RID: 10267 RVA: 0x00083348 File Offset: 0x00081748
		public static IEnumerator<GenInfo> GenerationCoroutine(MultiWave multiwave, string levelName)
		{
			Stopwatch setupStopwatch = Stopwatch.StartNew();
			if (!multiwave.hasLevel)
			{
				setupStopwatch.Start();
				IEnumerator enumerator = multiwave.Setup().GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						object s = enumerator.Current;
						yield return new GenInfo("Setup", GenInfo.Mode.interruptable);
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
			setupStopwatch.Stop();
			Stopwatch resolveStopwatch = Stopwatch.StartNew();
			resolveStopwatch.Start();
			IEnumerator<GenInfo> resolveRoutine = multiwave.Generate();
			while (resolveRoutine.MoveNext())
			{
				GenInfo genInfo = resolveRoutine.Current;
				yield return genInfo;
			}
			yield break;
		}

		// Token: 0x0600281C RID: 10268 RVA: 0x00083363 File Offset: 0x00081763
		public void OnGameAwake()
		{
		}

		// Token: 0x0600281D RID: 10269 RVA: 0x00083365 File Offset: 0x00081765
		private void Start()
		{
			IslandGenerator.instance = this;
			this.UpdateBlockage();
			this.timerCoroutine = CoroutineUtils.GenerateTimer(2.5f, this.InfiniteEnumerator());
		}

		// Token: 0x0600281E RID: 10270 RVA: 0x00083389 File Offset: 0x00081789
		private void OnEnable()
		{
			this.UpdateBlockage();
		}

		// Token: 0x0600281F RID: 10271 RVA: 0x00083391 File Offset: 0x00081791
		private void OnDisable()
		{
			this.UpdateBlockage();
		}

		// Token: 0x06002820 RID: 10272 RVA: 0x00083399 File Offset: 0x00081799
		private void OnDestroy()
		{
			this.timerCoroutine = null;
			this.currentIslandProxy = null;
			this.queue.ShutDown();
		}

		// Token: 0x06002821 RID: 10273 RVA: 0x000833B4 File Offset: 0x000817B4
		private void OnApplicationQuit()
		{
			this.OnDestroy();
		}

		// Token: 0x06002822 RID: 10274 RVA: 0x000833BC File Offset: 0x000817BC
		public void Clear()
		{
			this.queue.ShutDown();
			this.islandContainer.DestroyChildren();
			this.currentIslandProxy = null;
			this.timerCoroutine = CoroutineUtils.GenerateTimer(2.5f, this.InfiniteEnumerator());
		}

		// Token: 0x06002823 RID: 10275 RVA: 0x000833F4 File Offset: 0x000817F4
		public IEnumerator ShutDownRoutine()
		{
			IslandGenerator.AddBlocker(this, this);
			this.timerCoroutine = null;
			this.currentIslandProxy = null;
			IEnumerator r = this.queue.ShutDownRoutine();
			while (r.MoveNext())
			{
				yield return null;
			}
			this.islandContainer.DestroyChildren();
			IslandGenerator.RemoveBlocker(this, this);
			yield break;
		}

		// Token: 0x06002824 RID: 10276 RVA: 0x00083410 File Offset: 0x00081810
		public IEnumerator GenerateLevelsForCampaignInit()
		{
			using (new ScopedStopwatch("Generate Initial Levels", null, 0f))
			{
				foreach (LevelNode levelNode in Singleton<CampaignManager>.instance.campaign.levels)
				{
					levelNode.islandProxy.MaybeGenerateIsland(2);
				}
				while (this.queue.ContainsAny() || this.currentIslandProxy)
				{
					yield return null;
				}
				float end = Time.unscaledTime + 0.15f;
				while (end > Time.unscaledTime)
				{
					yield return null;
				}
			}
			yield break;
		}

		// Token: 0x06002825 RID: 10277 RVA: 0x0008342C File Offset: 0x0008182C
		public IEnumerator GenerateLevelsForCampaignLoad(Campaign campaign, int minLevelsToWait, float maxSecondsToWait)
		{
			using (ScopedStopwatch s = new ScopedStopwatch("Generate Initial Levels", null, 0f))
			{
				float maxMs = maxSecondsToWait * 1000f;
				this.QueueUpGenerations(campaign);
				int waitCount = Mathf.Max(this.queue.Count() - minLevelsToWait, 0);
				while (this.queue.Count() + ((!this.currentIslandProxy) ? 0 : 1) > waitCount && maxMs > s.elapsedMS)
				{
					yield return null;
				}
				float end = Time.unscaledTime + 0.15f;
				while (end > Time.unscaledTime)
				{
					yield return null;
				}
			}
			yield break;
		}

		// Token: 0x06002826 RID: 10278 RVA: 0x0008345C File Offset: 0x0008185C
		public IEnumerator GenerateAllAvailableAndWait()
		{
			this.GenerateAllAvailable();
			while (this.queue.ContainsAny() || this.currentIslandProxy)
			{
				yield return null;
			}
			yield break;
		}

		// Token: 0x06002827 RID: 10279 RVA: 0x00083478 File Offset: 0x00081878
		public int GenerateAllAvailable()
		{
			int num = 0;
			foreach (LevelNode levelNode in Singleton<CampaignManager>.instance.campaign.levels)
			{
				if (levelNode.IsAvailable() && !levelNode.islandProxy.island)
				{
					levelNode.islandProxy.GenerateIsland();
					num++;
				}
			}
			return num;
		}

		// Token: 0x06002828 RID: 10280 RVA: 0x00083508 File Offset: 0x00081908
		[ConsoleCommand("GenerateAllAvailable")]
		[DebugSetting("Generate All Available Islands", "生成所有可玩岛屿", DebugSettingLocation.Campaign)]
		private static void GenerateAllAvailable_Console()
		{
			Singleton<CampaignManager>.instance.islandGenerator.GenerateAllAvailable();
		}

		// Token: 0x06002829 RID: 10281 RVA: 0x0008351C File Offset: 0x0008191C
		[ConsoleCommand("")]
		[DebugSetting("Generate All Islands", "生成所有岛屿", DebugSettingLocation.Campaign)]
		private static void GenerateAll()
		{
			foreach (LevelNode levelNode in Singleton<CampaignManager>.instance.campaign.levels)
			{
				if (!levelNode.islandProxy.island)
				{
					levelNode.islandProxy.GenerateIsland();
				}
			}
		}

		// Token: 0x0600282A RID: 10282 RVA: 0x0008359C File Offset: 0x0008199C
		[ConsoleCommand("")]
		private static void DestroyAll()
		{
			foreach (LevelNode levelNode in Singleton<CampaignManager>.instance.campaign.levels)
			{
				if (levelNode.islandProxy)
				{
					levelNode.islandProxy.DestroyIsland();
				}
			}
		}

		// Token: 0x040019B7 RID: 6583
		private const float maxCoroutineTime = 2.5f;

		// Token: 0x040019B8 RID: 6584
		private DebugChannelGroup dbgGroup = new DebugChannelGroup("IslandGenerator", EVerbosity.Quiet, 1000);

		// Token: 0x040019B9 RID: 6585
		private static IslandGenerator instance = null;

		// Token: 0x040019BA RID: 6586
		[SerializeField]
		private Transform islandContainer;

		// Token: 0x040019BB RID: 6587
		[SerializeField]
		private bool _allowThreads = true;

		// Token: 0x040019BC RID: 6588
		private IslandGeneratorQueue queue = new IslandGeneratorQueue();

		// Token: 0x040019BD RID: 6589
		private IEnumerator timerCoroutine;

		// Token: 0x040019BE RID: 6590
		private IslandProxy currentIslandProxy;

		// Token: 0x040019BF RID: 6591
		private static List<object> coroutineBlockers = new List<object>();

		// Token: 0x040019C0 RID: 6592
		private static List<object> threadBlockers = new List<object>();

		// Token: 0x040019C1 RID: 6593
		private const bool allowThreads = true;
	}
}
