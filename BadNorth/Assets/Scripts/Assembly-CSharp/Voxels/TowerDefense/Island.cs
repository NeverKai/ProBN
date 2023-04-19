using System.Collections.Generic;
using System.Diagnostics;
using RTM.OnScreenDebug;
using UnityEngine;

namespace Voxels.TowerDefense
{
	public class Island : MonoBehaviour
	{
		public NavigationMesh navMesh => this.navManager.navigationMesh;

		public TexturePool texturePool => this.levelNode.campaign.texturePool;

		public MeshPool meshPool => this.levelNode.campaign.meshPool;

		private void SelfDestruct()
		{
			Destroy(base.gameObject);
		}

		private void OnDestroy()
		{
			this.english = null;
			this.vikings = null;
			this.levelNode = null;
			this.navManager = null;
			this.beaches = null;
			this.painter = null;
			this.aoBaker = null;
			this.fog = null;
			this.navSpotter = null;
			this.spriter = null;
			this.moduleContainer = null;
			this.village = null;
			this.voxelSpace = null;
			this.raid = null;
			this.wind = null;
			this.runContainer = null;
			this.islandComponents = null;
			this.awakers = null;
			this.processors = null;
			this.enterers = null;
			this.firstEnterers = null;
			this.islandPlayers = null;
			this.islandWipers = null;
			this.islandResetters = null;
			this.islandLeavers = null;
			this.savedWave = null;
		}

		public IEnumerator<GenInfo> SetupRoutine(LevelNode levelNode, MultiWave multiWave)
		{
			this.savedWave = multiWave.savedWave;
			this.levelNode = levelNode;
			this.size = multiWave.size;
			this.state = Island.State.Processing;
			this.awakers = base.GetComponentsInChildren<IIslandAwake>(true);
			foreach (IIslandAwake islandAwake in this.awakers)
			{
				islandAwake.OnIslandAwake(this);
			}
			this.islandComponents = base.GetComponentsInChildren<IslandComponent>();
			foreach (IslandComponent i in this.islandComponents)
			{
				yield return default(GenInfo);
				i.SetIsland(this);
			}
			this.processors = base.GetComponentsInChildren<IIslandProcessor>();
			int j = 0;
			int num = this.processors.Length;
			while (j < num)
			{
				string profilerName = this.processors[j].GetType().Name + ".OnIslandProcess()";
				IEnumerator<GenInfo> routine = this.processors[j].OnIslandProcess(this, multiWave.savedWave);
				bool cont = false;
				do
				{
					using (new ScopedProfiler(profilerName, null))
					{
						cont = routine.MoveNext();
					}
					yield return routine.Current;
				}
				while (cont);
				j++;
			}
			yield return default(GenInfo);
			this.islandWipers = base.GetComponentsInChildren<IIslandWipe>(true);
			this.islandPlayers = base.GetComponentsInChildren<IIslandPlay>(true);
			this.enterers = base.GetComponentsInChildren<IIslandEnter>(true);
			this.firstEnterers = base.GetComponentsInChildren<IIslandFirstEnter>(true);
			this.islandResetters = base.GetComponentsInChildren<IIslandReset>(true);
			this.islandLeavers = base.GetComponentsInChildren<IIslandLeave>(true);
			this.generated = true;
			this.state = State.Idle;
			yield return default(GenInfo);
			yield return default(GenInfo);
			base.gameObject.SetActive(false);
		}

		// Token: 0x060031BF RID: 12735 RVA: 0x000D00BC File Offset: 0x000CE4BC
		public IEnumerator<object> EnterIslandLoading()
		{
			IslandGenerator.AddBlocker(this, null);
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			base.gameObject.SetActive(true);
			UnityEngine.Random.State oldState = UnityEngine.Random.state;
			Random.InitState(this.levelNode.index * 98765 + this.levelNode.campaign.seed);
			IEnumerator<GenInfo> enumerator = this.EnterIsland();
			while (enumerator.MoveNext())
			{
				if (stopwatch.ElapsedMilliseconds > 16L)
				{
					UnityEngine.Random.State state = UnityEngine.Random.state;
					yield return null;
					stopwatch.Reset();
					stopwatch.Start();
					UnityEngine.Random.state = state;
				}
			}
			yield return ScenePreloader.islandGameplay;
			Random.state = oldState;
			yield return null;
		}

		// Token: 0x060031C0 RID: 12736 RVA: 0x000D00D8 File Offset: 0x000CE4D8
		public IEnumerator<GenInfo> EnterIsland()
		{
			this.state = Island.State.Playing;
			this.levelNode.campaign.currentLevel = this.levelNode;
			Shader.SetGlobalVector("_WaveSize", this.size);
			LevelState levelState = this.levelNode.levelState;
			Rect spriteRect = levelState.spriteRect;
			Shader.SetGlobalVector("_LevelRect", new Vector4(spriteRect.xMin, spriteRect.yMin, spriteRect.xMax, spriteRect.yMax));
			if (!this.hasBeenEnteredBefore)
			{
				foreach (IIslandFirstEnter i in this.firstEnterers)
				{
					IEnumerator<GenInfo> routine = i.OnIslandFirstEnter(this);
					while (routine.MoveNext())
					{
						GenInfo genInfo = routine.Current;
						yield return genInfo;
					}
				}
				this.hasBeenEnteredBefore = true;
				this.savedWave = null;
			}
			foreach (IIslandEnter j in this.enterers)
			{
				IEnumerator<GenInfo> routine2 = j.OnIslandEnter(this);
				while (routine2.MoveNext())
				{
					GenInfo genInfo2 = routine2.Current;
					yield return genInfo2;
				}
			}
			yield break;
		}

		// Token: 0x060031C1 RID: 12737 RVA: 0x000D00F4 File Offset: 0x000CE4F4
		public IEnumerator<GenInfo> PlayIsland()
		{
			this.runContainer = gameObject.AddEmptyChild("RunContainer").transform;
			UnityEngine.Random.State oldState = UnityEngine.Random.state;
			UnityEngine.Random.InitState(this.levelNode.index * 7652132 + this.levelNode.campaign.seed);
			for (int i = 0; i < this.islandPlayers.Length; i++)
			{
				IEnumerator<GenInfo> routine = this.islandPlayers[i].OnIslandPlay(this);
				while (routine.MoveNext())
				{
					GenInfo genInfo = routine.Current;
					yield return genInfo;
				}
			}
			foreach (SquadEvacuationLocation evac in base.GetComponentsInChildren<SquadEvacuationLocation>(true))
			{
				yield return new GenInfo("Init_Evacs", GenInfo.Mode.interruptable);
				evac.Setup();
			}
			UnityEngine.Random.state = oldState;
			yield return default(GenInfo);
		}

		// Token: 0x060031C2 RID: 12738 RVA: 0x000D0110 File Offset: 0x000CE510
		public IEnumerator<GenInfo> WipeIsland()
		{
			for (int i = 0; i < this.islandWipers.Length; i++)
			{
				IEnumerator<GenInfo> routine = this.islandWipers[i].OnIslandWipe(this);
				while (routine.MoveNext())
				{
					GenInfo genInfo = routine.Current;
					yield return genInfo;
				}
			}
			UnityEngine.Object.Destroy(this.runContainer.gameObject);
			yield return default(GenInfo);
			yield break;
		}

		// Token: 0x060031C3 RID: 12739 RVA: 0x000D012C File Offset: 0x000CE52C
		public IEnumerator<GenInfo> ResetIsland()
		{
			for (int i = 0; i < this.islandResetters.Length; i++)
			{
				IEnumerator<GenInfo> routine = this.islandResetters[i].OnIslandReset(this);
				while (routine.MoveNext())
				{
					GenInfo genInfo = routine.Current;
					yield return genInfo;
				}
			}
			yield break;
		}

		// Token: 0x060031C4 RID: 12740 RVA: 0x000D0148 File Offset: 0x000CE548
		public IEnumerator<GenInfo> LeaveIsland()
		{
			this.levelNode.campaign.currentLevel = null;
			this.state = Island.State.Idle;
			for (int i = 0; i < this.islandLeavers.Length; i++)
			{
				IEnumerator<GenInfo> routine = this.islandLeavers[i].OnIslandLeave(this);
				while (routine.MoveNext())
				{
					GenInfo genInfo = routine.Current;
					yield return genInfo;
				}
			}
			base.gameObject.SetActive(false);
			IslandGenerator.RemoveBlocker(this, null);
			yield return default(GenInfo);
			yield break;
		}

		// Token: 0x060031C5 RID: 12741 RVA: 0x000D0164 File Offset: 0x000CE564
		public void DestroyIsland()
		{
			if (this.hasBeenEnteredBefore)
			{
				foreach (IIslandDestroyEntered islandDestroyEntered in base.gameObject.EnumerateComponentsInChildren(true, true))
				{
					islandDestroyEntered.OnIslandDestroyEntered(this);
				}
			}
			foreach (IIslandDestroy islandDestroy in base.gameObject.EnumerateComponentsInChildren(true, true))
			{
				islandDestroy.OnIslandDestroy(this);
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x04002184 RID: 8580
		private static DebugChannelGroup dbgGroup = new DebugChannelGroup("LevelNode", EVerbosity.Quiet, 100);

		// Token: 0x04002185 RID: 8581
		[Header("Editor")]
		public Faction english;

		// Token: 0x04002186 RID: 8582
		public Faction vikings;

		// Token: 0x04002187 RID: 8583
		[Header("Runtime")]
		public bool generated;

		// Token: 0x04002188 RID: 8584
		public bool hasBeenEnteredBefore;

		// Token: 0x04002189 RID: 8585
		public Vector3 size = new Vector3(8f, 4f, 8f);

		// Token: 0x0400218A RID: 8586
		public LevelNode levelNode;

		// Token: 0x0400218B RID: 8587
		public Island.State state;

		// Token: 0x0400218C RID: 8588
		public NavigationManager navManager;

		// Token: 0x0400218D RID: 8589
		public Beaches beaches;

		// Token: 0x0400218E RID: 8590
		public Painter painter;

		// Token: 0x0400218F RID: 8591
		public AoBaker aoBaker;

		// Token: 0x04002190 RID: 8592
		public Fog fog;

		// Token: 0x04002191 RID: 8593
		public NavSpotter navSpotter;

		// Token: 0x04002192 RID: 8594
		public IslandSpriter spriter;

		// Token: 0x04002193 RID: 8595
		public ModuleContainer moduleContainer;

		// Token: 0x04002194 RID: 8596
		public Village village;

		// Token: 0x04002195 RID: 8597
		public VoxelSpace voxelSpace;

		// Token: 0x04002196 RID: 8598
		public Raid raid;

		// Token: 0x04002197 RID: 8599
		public Wind wind;

		// Token: 0x04002198 RID: 8600
		public SavedWave savedWave;

		// Token: 0x04002199 RID: 8601
		[HideInInspector]
		public Transform runContainer;

		// Token: 0x0400219A RID: 8602
		public IslandComponent[] islandComponents;

		// Token: 0x0400219B RID: 8603
		private IIslandAwake[] awakers;

		// Token: 0x0400219C RID: 8604
		private IIslandProcessor[] processors;

		// Token: 0x0400219D RID: 8605
		private IIslandEnter[] enterers;

		// Token: 0x0400219E RID: 8606
		private IIslandFirstEnter[] firstEnterers;

		// Token: 0x0400219F RID: 8607
		private IIslandPlay[] islandPlayers;

		// Token: 0x040021A0 RID: 8608
		private IIslandWipe[] islandWipers;

		// Token: 0x040021A1 RID: 8609
		private IIslandReset[] islandResetters;

		// Token: 0x040021A2 RID: 8610
		private IIslandLeave[] islandLeavers;

		// Token: 0x0200077B RID: 1915
		public enum State
		{
			Created,
			Processing,
			Idle,
			Playing
		}
	}
}
