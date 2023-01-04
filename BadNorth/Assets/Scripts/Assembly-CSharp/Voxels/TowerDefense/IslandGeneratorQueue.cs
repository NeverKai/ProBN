using System;
using System.Collections;
using System.Collections.Generic;
using RTM.OnScreenDebug;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x0200061B RID: 1563
	public class IslandGeneratorQueue
	{
		// Token: 0x0600282C RID: 10284 RVA: 0x00084198 File Offset: 0x00082598
		public IslandGeneratorQueue()
		{
			int processorCount = Environment.ProcessorCount;
			this.maxThreads = Mathf.Clamp(processorCount - 2, 1, 7);
			Debug.LogFormat("Logical cores available: {0}, allowing {1} threads for island generation", new object[]
			{
				processorCount,
				this.maxThreads
			});
			this.threads = new List<IslandProxy>(this.maxThreads);
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x0600282D RID: 10285 RVA: 0x0008422C File Offset: 0x0008262C
		public bool enabled
		{
			get
			{
				return this._enabled;
			}
		}

		// Token: 0x0600282E RID: 10286 RVA: 0x00084234 File Offset: 0x00082634
		public void Add(IslandProxy level, bool allowThreads)
		{
			if (this.Contains(level) || level.state == IslandProxy.State.Ready)
			{
				return;
			}
			List<IslandProxy> list = (!allowThreads) ? this.buildQueue : this.threadQueue;
			level.SetState(IslandProxy.State.GenerateQueue);
			list.Add(level);
			list.Sort();
		}

		// Token: 0x0600282F RID: 10287 RVA: 0x00084286 File Offset: 0x00082686
		public bool ContainsAny()
		{
			return this.Count() > 0;
		}

		// Token: 0x06002830 RID: 10288 RVA: 0x00084291 File Offset: 0x00082691
		public int Count()
		{
			return this.threadQueue.Count + this.threads.Count + this.buildQueue.Count;
		}

		// Token: 0x06002831 RID: 10289 RVA: 0x000842B6 File Offset: 0x000826B6
		public bool Contains(IslandProxy level)
		{
			return this.GetQueue(level) != null;
		}

		// Token: 0x06002832 RID: 10290 RVA: 0x000842C8 File Offset: 0x000826C8
		private List<IslandProxy> GetQueue(IslandProxy level)
		{
			if (this.threadQueue.Contains(level))
			{
				return this.threadQueue;
			}
			if (this.threads.Contains(level))
			{
				return this.threads;
			}
			if (this.buildQueue.Contains(level))
			{
				return this.buildQueue;
			}
			return null;
		}

		// Token: 0x06002833 RID: 10291 RVA: 0x00084320 File Offset: 0x00082720
		public void Update()
		{
			bool flag = false;
			for (int i = 0; i < this.threads.Count; i++)
			{
				if (this.threads[i].thread.IsCompleted())
				{
					using (new ScopedStopwatch("IslandGenQ - removethread()", null, 100f))
					{
						IslandProxy islandProxy = this.threads[i];
						this.threads.RemoveAt(i);
						if (islandProxy.thread.Exceptioned())
						{
							islandProxy.SetState(IslandProxy.State.None);
							islandProxy.GenerateIsland();
						}
						else
						{
							islandProxy.SetState(IslandProxy.State.BuildQueue);
							this.buildQueue.Add(islandProxy);
						}
						flag = true;
						continue;
					}
				}
			}
			if (flag)
			{
				this.threadQueue.Sort();
				this.buildQueue.Sort();
			}
			flag = false;
			int num = 4;
			while (this.threads.Count < this.maxThreads && this.threadQueue.Count > 0 && num > 0)
			{
				using (new ScopedStopwatch("IslandGenQ - startThread()", null, 100f))
				{
					IslandProxy islandProxy2 = this.threadQueue[0];
					bool flag2 = true;
					try
					{
						islandProxy2.SetState(IslandProxy.State.Generating);
						islandProxy2.thread.Start(IslandGenerator.GenerationCoroutine(islandProxy2.multiwave, islandProxy2.levelNode.name), islandProxy2.multiwave.name);
					}
					catch (Exception exception)
					{
						islandProxy2.SetState(IslandProxy.State.None);
						flag2 = false;
						this.threadQueue.RemoveAt(0);
						this.threadQueue.Add(islandProxy2);
						num--;
						Debug.LogFormat("Exception occured in {0} : ", new object[]
						{
							islandProxy2.levelNode.name
						});
						Debug.LogException(exception);
					}
					if (flag2)
					{
						this.threads.Add(islandProxy2);
						this.threadQueue.RemoveAt(0);
						flag = true;
					}
				}
			}
			foreach (IslandProxy islandProxy3 in this.threadQueue)
			{
			}
			foreach (IslandProxy islandProxy4 in this.threads)
			{
			}
			foreach (IslandProxy islandProxy5 in this.buildQueue)
			{
			}
		}

		// Token: 0x06002834 RID: 10292 RVA: 0x0008461C File Offset: 0x00082A1C
		public void Sort()
		{
			this.threadQueue.Sort();
			this.buildQueue.Sort();
		}

		// Token: 0x06002835 RID: 10293 RVA: 0x00084634 File Offset: 0x00082A34
		public void RemoveUnbuildables()
		{
			this.RemoveUnbuildables(this.threadQueue);
			this.RemoveUnbuildables(this.buildQueue);
		}

		// Token: 0x06002836 RID: 10294 RVA: 0x00084650 File Offset: 0x00082A50
		private void RemoveUnbuildables(List<IslandProxy> proxies)
		{
			for (int i = proxies.Count - 1; i >= 0; i--)
			{
				if (proxies[i].buildPriority < 0)
				{
					IslandProxy islandProxy = proxies[i];
					proxies.RemoveAt(i);
					islandProxy.CancelGeneration();
				}
			}
		}

		// Token: 0x06002837 RID: 10295 RVA: 0x000846A0 File Offset: 0x00082AA0
		public void OnEnable()
		{
			this._enabled = true;
			int i = 0;
			int count = this.threads.Count;
			while (i < count)
			{
				this.threads[i].thread.Resume();
				i++;
			}
		}

		// Token: 0x06002838 RID: 10296 RVA: 0x000846E8 File Offset: 0x00082AE8
		public void OnDisable()
		{
			this._enabled = false;
			int i = 0;
			int count = this.threads.Count;
			while (i < count)
			{
				this.threads[i].thread.Suspend();
				i++;
			}
		}

		// Token: 0x06002839 RID: 10297 RVA: 0x00084730 File Offset: 0x00082B30
		public void ShutDown()
		{
			int i = 0;
			int count = this.threads.Count;
			while (i < count)
			{
				this.threads[i].thread.Abort(true);
				i++;
			}
			this.threadQueue.Clear();
			this.threads.Clear();
			this.buildQueue.Clear();
		}

		// Token: 0x0600283A RID: 10298 RVA: 0x00084794 File Offset: 0x00082B94
		public IEnumerator ShutDownRoutine()
		{
			int i = 0;
			int count = this.threads.Count;
			while (i < count)
			{
				this.threads[i].thread.Abort(false);
				i++;
			}
			yield return null;
			bool anyRemain = true;
			while (anyRemain)
			{
				anyRemain = false;
				foreach (IslandProxy islandProxy in this.threads)
				{
					if (!islandProxy.thread.IsCompleted())
					{
						anyRemain = true;
					}
				}
				yield return null;
			}
			this.threadQueue.Clear();
			this.threads.Clear();
			this.buildQueue.Clear();
			yield break;
		}

		// Token: 0x0600283B RID: 10299 RVA: 0x000847B0 File Offset: 0x00082BB0
		public IslandProxy TryPop()
		{
			IslandProxy result = null;
			if (this.buildQueue.Count != 0)
			{
				result = this.buildQueue[0];
				this.buildQueue.RemoveAt(0);
			}
			return result;
		}

		// Token: 0x0600283C RID: 10300 RVA: 0x000847E9 File Offset: 0x00082BE9
		public void LevelBuildFailed(IslandProxy level)
		{
			UnityEngine.Object.Destroy(level.island);
			this.Add(level, true);
		}

		// Token: 0x040019C2 RID: 6594
		private DebugChannelGroup dbgGroup = new DebugChannelGroup("IslandGenerationQueue", EVerbosity.Quiet, 0);

		// Token: 0x040019C3 RID: 6595
		private int maxThreads = 3;

		// Token: 0x040019C4 RID: 6596
		private bool _enabled;

		// Token: 0x040019C5 RID: 6597
		private List<IslandProxy> threadQueue = new List<IslandProxy>(16);

		// Token: 0x040019C6 RID: 6598
		private List<IslandProxy> threads;

		// Token: 0x040019C7 RID: 6599
		private List<IslandProxy> buildQueue = new List<IslandProxy>(16);
	}
}
