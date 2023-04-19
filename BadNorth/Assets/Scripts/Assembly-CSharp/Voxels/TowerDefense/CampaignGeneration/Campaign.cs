using System;
using System.Collections;
using System.Collections.Generic;
using TrialVersion;
using UnityEngine;
using Voxels.TowerDefense.ProfileInternals;
using Voxels.TowerDefense.ScriptAnimations;

namespace Voxels.TowerDefense.CampaignGeneration
{
	// Token: 0x020006C9 RID: 1737
	public class Campaign : MonoBehaviour
	{
		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06002D18 RID: 11544 RVA: 0x000A80A5 File Offset: 0x000A64A5
		public LevelNode startLevel
		{
			get
			{
				return this.levels[0];
			}
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06002D19 RID: 11545 RVA: 0x000A80B3 File Offset: 0x000A64B3
		public LevelNode endLevel
		{
			get
			{
				return this.levels[this.levels.Count - 1];
			}
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x06002D1A RID: 11546 RVA: 0x000A80CD File Offset: 0x000A64CD
		public int frontierDepth
		{
			get
			{
				return this.endLevel.frontierDepth;
			}
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x06002D1B RID: 11547 RVA: 0x000A80DA File Offset: 0x000A64DA
		public AgentState rootState
		{
			get
			{
				return this.stateRoot.rootState;
			}
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x06002D1C RID: 11548 RVA: 0x000A80E7 File Offset: 0x000A64E7
		public int seed
		{
			get
			{
				return this.campaignSave.seed;
			}
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x06002D1E RID: 11550 RVA: 0x000A80FD File Offset: 0x000A64FD
		// (set) Token: 0x06002D1D RID: 11549 RVA: 0x000A80F4 File Offset: 0x000A64F4
		public CampaignSave campaignSave
		{
			get
			{
				return this._campaignSave;
			}
			private set
			{
				this._campaignSave = value;
			}
		}

		// Token: 0x06002D1F RID: 11551 RVA: 0x000A8105 File Offset: 0x000A6505
		private void Awake()
		{
			this.stateRoot.MaybeSetup();
		}

		// Token: 0x06002D20 RID: 11552 RVA: 0x000A8112 File Offset: 0x000A6512
		private void Update()
		{
			this.stateRoot.Update();
		}

		// Token: 0x06002D21 RID: 11553 RVA: 0x000A8120 File Offset: 0x000A6520
		public int GetGenerationDepth()
		{
			int result;
			//using ("GetGenerationDepth")
			{
				int num = 0;
				foreach (LevelNode levelNode in this.levels)
				{
					if (levelNode.IsAvailable())
					{
						num = Mathf.Max(num, (int)levelNode.levelState.stepsFromStart);
					}
				}
				if (num >= 9)
				{
				}
				int num2;
				if (num >= 5)
				{
					num2 = 4;
				}
				else
				{
					num2 = 3;
				}
				result = num2;
			}
			return result;
		}

		// Token: 0x06002D22 RID: 11554 RVA: 0x000A81E4 File Offset: 0x000A65E4
		public IEnumerator<GenInfo> Create(CampaignSave campaignSave)
		{
			GenInfo info = new GenInfo("Campaign", GenInfo.Mode.interruptable);
			this.campaignSave = campaignSave;
			UnityEngine.Random.State oldState = UnityEngine.Random.state;
			UnityEngine.Random.InitState(this.seed);
			UnityEngine.Random.State randomState = UnityEngine.Random.state;
			ProtoCampaign protoCampaign = new ProtoCampaign();
			Campaign.ICampaignCreator[] generators = base.GetComponentsInChildren<Campaign.ICampaignCreator>();
			foreach (Campaign.ICampaignCreator generator in generators)
			{
				IEnumerator enumerator = generator.OnCampaigCreation(this, protoCampaign);
				UnityEngine.Random.state = randomState;
				while (enumerator.MoveNext())
				{
					yield return info;
				}
				randomState = UnityEngine.Random.state;
			}
			UnityEngine.Random.state = oldState;
			yield break;
		}

		// Token: 0x06002D23 RID: 11555 RVA: 0x000A8208 File Offset: 0x000A6608
		public IEnumerator<GenInfo> Setup(CampaignSave campaignSave)
		{
			this.campaignSave = campaignSave;
			this.meshPool = base.gameObject.AddComponent<MeshPool>();
			this.texturePool = base.gameObject.AddComponent<TexturePool>();
			UnityEngine.Random.State oldState = UnityEngine.Random.state;
			UnityEngine.Random.InitState(this.seed);
			UnityEngine.Random.State randomState = UnityEngine.Random.state;
			this.levelSpriteBaker = base.GetComponentInChildren<LevelSpriteBaker>();
			this.paintAtlas = base.GetComponentInChildren<PaintAtlas>();
			this.levelSpawner = base.GetComponentInChildren<LevelSpawner>();
			this.heroesAvaliable = new AnimatedState("HeroesAvailable", this.rootState, campaignSave.HeroesAvailableThisTurn(), false);
			Campaign.ICampaignGenerator[] generators = base.GetComponentsInChildren<Campaign.ICampaignGenerator>();
			foreach (Campaign.ICampaignGenerator generator in generators)
			{
				IEnumerator<GenInfo> enumerator = generator.OnCampaignGeneration(this);
				UnityEngine.Random.state = randomState;
				while (enumerator.MoveNext())
				{
					GenInfo genInfo = enumerator.Current;
					yield return genInfo;
				}
				randomState = UnityEngine.Random.state;
			}
			RectTransform rt = base.transform as RectTransform;
			rt.sizeDelta = this.rect.size;
			UnityEngine.Random.state = oldState;
			yield break;
		}

		// Token: 0x06002D24 RID: 11556 RVA: 0x000A822C File Offset: 0x000A662C
		public bool TestAllDeadGameOver()
		{
			foreach (HeroDefinition heroDefinition in this.campaignSave.heroes)
			{
				if (heroDefinition.available)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002D25 RID: 11557 RVA: 0x000A829C File Offset: 0x000A669C
		public void Destroy()
		{
			foreach (LevelNode levelNode in this.levels)
			{
				levelNode.islandProxy.DestroyIsland();
			}
			foreach (Campaign.ICampaignDestroy campaignDestroy in base.gameObject.EnumerateComponentsInChildren(true, true))
			{
				campaignDestroy.OnCampaignDestroy(this);
			}
			if (this.meshPool)
			{
				this.meshPool.ClearPool();
			}
			if (this.texturePool)
			{
				this.texturePool.ClearPool();
			}
			this.meshPool = null;
			this.texturePool = null;
			this.campaignSave = null;
			this.levels = null;
			this.currentLevel = null;
			this.lastPlayedLevel = null;
			this.pendingUnlocks.Clear();
			this.pendingUnlocks = null;
			this.levelSpawner = null;
			this.heroGenerator = null;
			this.levelSpriteBaker = null;
			this.paintAtlas = null;
			this.rootState.OnDestroy();
			this.stateRoot = null;
			this.heroesAvaliable = null;
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x06002D26 RID: 11558 RVA: 0x000A8400 File Offset: 0x000A6800
		public CampaignDifficultySettings GetDifficultySettings()
		{
			Difficulty difficulty = this.campaignSave.prefs.difficulty;
			switch (difficulty)
			{
			case Difficulty.Easy:
				return this.easySettings;
			case Difficulty.Normal:
				return this.normalSettings;
			case Difficulty.Hard:
				return this.hardSettings;
			case Difficulty.VeryHard:
				return this.veryHardSettings;
			default:
				throw new NotImplementedException(string.Format("unknown diffiuclty {0}", difficulty));
			}
		}

		// Token: 0x06002D27 RID: 11559 RVA: 0x000A846C File Offset: 0x000A686C
		private void OnDrawGizmos()
		{
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x06002D28 RID: 11560 RVA: 0x000A846E File Offset: 0x000A686E
		public int trialTurns
		{
			get
			{
				return this.campaignSave.vikingFrontierPosition + 1;
			}
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x06002D29 RID: 11561 RVA: 0x000A847D File Offset: 0x000A687D
		public int trialTurnsRemain
		{
			get
			{
				return 4 - this.campaignSave.vikingFrontierPosition - 1;
			}
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06002D2A RID: 11562 RVA: 0x000A848E File Offset: 0x000A688E
		public bool trialOver
		{
			get
			{
				return Utils.IsTrial() && this.campaignSave.vikingFrontierPosition >= 4;
			}
		}

		// Token: 0x04001D98 RID: 7576
		public HeroGenerator heroGenerator;

		// Token: 0x04001D99 RID: 7577
		[Header("Difficulty Settings")]
		[SerializeField]
		private CampaignDifficultySettings easySettings;

		// Token: 0x04001D9A RID: 7578
		[SerializeField]
		private CampaignDifficultySettings normalSettings;

		// Token: 0x04001D9B RID: 7579
		[SerializeField]
		private CampaignDifficultySettings hardSettings;

		// Token: 0x04001D9C RID: 7580
		[SerializeField]
		private CampaignDifficultySettings veryHardSettings;

		// Token: 0x04001D9D RID: 7581
		[Space]
		[Header("Runtime")]
		public List<LevelNode> levels = new List<LevelNode>();

		// Token: 0x04001D9E RID: 7582
		public LevelNode currentLevel;

		// Token: 0x04001D9F RID: 7583
		public LevelSpriteBaker levelSpriteBaker;

		// Token: 0x04001DA0 RID: 7584
		public PaintAtlas paintAtlas;

		// Token: 0x04001DA1 RID: 7585
		public LevelSpawner levelSpawner;

		// Token: 0x04001DA2 RID: 7586
		public Bounds bounds;

		// Token: 0x04001DA3 RID: 7587
		public Rect rect;

		// Token: 0x04001DA4 RID: 7588
		public TexturePool texturePool;

		// Token: 0x04001DA5 RID: 7589
		public MeshPool meshPool;

		// Token: 0x04001DA6 RID: 7590
		[SerializeField]
		private AgentStateRoot stateRoot = new AgentStateRoot(4);

		// Token: 0x04001DA7 RID: 7591
		public AnimatedState heroesAvaliable;

		// Token: 0x04001DA8 RID: 7592
		[NonSerialized]
		private CampaignSave _campaignSave;

		// Token: 0x04001DA9 RID: 7593
		public LevelNode lastPlayedLevel;

		// Token: 0x04001DAA RID: 7594
		public List<LevelNode> pendingUnlocks = new List<LevelNode>();

		// Token: 0x04001DAB RID: 7595
		private const int maxFrontierDisplay = 5;

		// Token: 0x04001DAC RID: 7596
		private const int frontierDisplayOffset = 1;

		// Token: 0x04001DAD RID: 7597
		private const int maxFrontierPosition = 4;

		// Token: 0x020006CA RID: 1738
		public interface ICampaignCreator
		{
			// Token: 0x06002D2B RID: 11563
			IEnumerator OnCampaigCreation(Campaign campaign, ProtoCampaign protoCampaign);
		}

		// Token: 0x020006CB RID: 1739
		public interface ICampaignGenerator
		{
			// Token: 0x06002D2C RID: 11564
			IEnumerator<GenInfo> OnCampaignGeneration(Campaign campaign);
		}

		// Token: 0x020006CC RID: 1740
		public interface ICampaignDestroy
		{
			// Token: 0x06002D2D RID: 11565
			void OnCampaignDestroy(Campaign campaign);
		}
	}
}
