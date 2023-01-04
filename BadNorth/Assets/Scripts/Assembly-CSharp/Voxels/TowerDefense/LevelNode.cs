using System;
using System.Collections;
using System.Collections.Generic;
using Fabric;
using I2.Loc;
using ReflexCLI.Attributes;
using RTM.OnScreenDebug;
using UnityEngine;
using Voxels.TowerDefense.CampaignGeneration;
using Voxels.TowerDefense.ScriptAnimations;
using Voxels.TowerDefense.UI;

namespace Voxels.TowerDefense
{
	// Token: 0x0200070A RID: 1802
	[ConsoleCommandClassCustomizer("Campaign")]
	public class LevelNode : CampaignComponent
	{
		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x06002E8E RID: 11918 RVA: 0x000B5CD8 File Offset: 0x000B40D8
		public IEnumerable<LevelNode> connectedLevels
		{
			get
			{
				foreach (int i in this.levelState.connections)
				{
					yield return base.campaign.levels[i];
				}
				yield break;
			}
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x06002E8F RID: 11919 RVA: 0x000B5CFB File Offset: 0x000B40FB
		// (set) Token: 0x06002E90 RID: 11920 RVA: 0x000B5D03 File Offset: 0x000B4103
		public LevelState levelState { get; private set; }

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06002E91 RID: 11921 RVA: 0x000B5D0C File Offset: 0x000B410C
		// (set) Token: 0x06002E92 RID: 11922 RVA: 0x000B5D14 File Offset: 0x000B4114
		public int stepsFromUnlock
		{
			get
			{
				return this._stepsFromUnlock;
			}
			set
			{
				this._stepsFromUnlock = value;
				this.onStepsFromUnlockChange();
			}
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06002E93 RID: 11923 RVA: 0x000B5D28 File Offset: 0x000B4128
		public int frontierDepth
		{
			get
			{
				return (int)this.levelState.frontierDepth;
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06002E94 RID: 11924 RVA: 0x000B5D35 File Offset: 0x000B4135
		public Vector3Int size
		{
			get
			{
				return new Vector3Int(this.width, this.height, this.width);
			}
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x06002E95 RID: 11925 RVA: 0x000B5D4E File Offset: 0x000B414E
		public float innerRadius
		{
			get
			{
				return this.levelState.innerRadius;
			}
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x06002E96 RID: 11926 RVA: 0x000B5D5B File Offset: 0x000B415B
		public float outerRadius
		{
			get
			{
				return this.levelState.outerRadius;
			}
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x06002E97 RID: 11927 RVA: 0x000B5D68 File Offset: 0x000B4168
		public int width
		{
			get
			{
				return (int)this.levelState.width;
			}
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x06002E98 RID: 11928 RVA: 0x000B5D75 File Offset: 0x000B4175
		public int height
		{
			get
			{
				return (int)this.levelState.height;
			}
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x06002E99 RID: 11929 RVA: 0x000B5D82 File Offset: 0x000B4182
		public int index
		{
			get
			{
				return (int)this.levelState.index;
			}
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x06002E9A RID: 11930 RVA: 0x000B5D8F File Offset: 0x000B418F
		public bool isStart
		{
			get
			{
				return this == base.campaign.startLevel;
			}
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x06002E9B RID: 11931 RVA: 0x000B5DA2 File Offset: 0x000B41A2
		public bool isEnd
		{
			get
			{
				return this == base.campaign.endLevel;
			}
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06002E9C RID: 11932 RVA: 0x000B5DB5 File Offset: 0x000B41B5
		public bool isStartOrEnd
		{
			get
			{
				return this.isStart || this.isEnd;
			}
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06002E9D RID: 11933 RVA: 0x000B5DCB File Offset: 0x000B41CB
		public bool wantsTutorial
		{
			get
			{
				return this.isStart && !this.IsPlayed();
			}
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06002E9E RID: 11934 RVA: 0x000B5DE4 File Offset: 0x000B41E4
		public AgentState rootState
		{
			get
			{
				return this.stateRoot.rootState;
			}
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06002E9F RID: 11935 RVA: 0x000B5DF1 File Offset: 0x000B41F1
		// (set) Token: 0x06002EA0 RID: 11936 RVA: 0x000B5DF9 File Offset: 0x000B41F9
		public AnimatedState behindFrontier { get; private set; }

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06002EA1 RID: 11937 RVA: 0x000B5E02 File Offset: 0x000B4202
		// (set) Token: 0x06002EA2 RID: 11938 RVA: 0x000B5E0A File Offset: 0x000B420A
		public AnimatedState unlocked { get; private set; }

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x06002EA3 RID: 11939 RVA: 0x000B5E13 File Offset: 0x000B4213
		// (set) Token: 0x06002EA4 RID: 11940 RVA: 0x000B5E1B File Offset: 0x000B421B
		public AnimatedState played { get; private set; }

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x06002EA5 RID: 11941 RVA: 0x000B5E24 File Offset: 0x000B4224
		// (set) Token: 0x06002EA6 RID: 11942 RVA: 0x000B5E2C File Offset: 0x000B422C
		public AnimatedState encouraged { get; private set; }

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x06002EA7 RID: 11943 RVA: 0x000B5E35 File Offset: 0x000B4235
		// (set) Token: 0x06002EA8 RID: 11944 RVA: 0x000B5E3D File Offset: 0x000B423D
		public AnimatedState available { get; private set; }

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x06002EA9 RID: 11945 RVA: 0x000B5E46 File Offset: 0x000B4246
		// (set) Token: 0x06002EAA RID: 11946 RVA: 0x000B5E4E File Offset: 0x000B424E
		public AnimatedState visible { get; private set; }

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x06002EAB RID: 11947 RVA: 0x000B5E57 File Offset: 0x000B4257
		public Island island
		{
			get
			{
				return this.islandProxy.island;
			}
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x06002EAC RID: 11948 RVA: 0x000B5E64 File Offset: 0x000B4264
		private int heroId
		{
			get
			{
				return this.levelState.heroId;
			}
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x06002EAD RID: 11949 RVA: 0x000B5E71 File Offset: 0x000B4271
		public bool hasHero
		{
			get
			{
				return this.heroId >= 0;
			}
		}

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06002EAE RID: 11950 RVA: 0x000B5E7F File Offset: 0x000B427F
		public HeroDefinition heroDefinition
		{
			get
			{
				return base.campaign.campaignSave.TryGetHeroDefinition(this.heroId);
			}
		}

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x06002EAF RID: 11951 RVA: 0x000B5E97 File Offset: 0x000B4297
		public HeroUpgradeDefinition item
		{
			get
			{
				return this.levelState.item;
			}
		}

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x06002EB0 RID: 11952 RVA: 0x000B5EA4 File Offset: 0x000B42A4
		public bool hasItem
		{
			get
			{
				return this.levelState.playCount == 0 && this.levelState.item;
			}
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06002EB1 RID: 11953 RVA: 0x000B5EC9 File Offset: 0x000B42C9
		public LevelState.CheckpointState checkpoint
		{
			get
			{
				return this.levelState.checkpointState;
			}
		}

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06002EB2 RID: 11954 RVA: 0x000B5ED6 File Offset: 0x000B42D6
		public bool hasCheckpoint
		{
			get
			{
				return this.checkpoint != LevelState.CheckpointState.None;
			}
		}

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06002EB3 RID: 11955 RVA: 0x000B5EE4 File Offset: 0x000B42E4
		public bool checkpointAvailable
		{
			get
			{
				return this.checkpoint == LevelState.CheckpointState.Available;
			}
		}

		// Token: 0x06002EB4 RID: 11956 RVA: 0x000B5EEF File Offset: 0x000B42EF
		public void Setup(LevelState levelState)
		{
			this.levelState = levelState;
			levelState.GetReferencedObjects(this.enemies);
			this.enemies.Sort((VikingReference a, VikingReference b) => a.transform.GetSiblingIndex().CompareTo(b.transform.GetSiblingIndex()));
		}

		// Token: 0x06002EB5 RID: 11957 RVA: 0x000B5F2C File Offset: 0x000B432C
		public void Play()
		{
			bool flag = this.islandProxy.state == IslandProxy.State.Ready;
			string format = (!flag) ? ScriptLocalization.LOAD_SCREEN.GENERATING_ISLAND : ScriptLocalization.LOAD_SCREEN.PREP_ISLAND;
			string description = string.Format(format, ScriptLocalization.Get(this.levelState.nameTerm, true, 0, true, false, null, null));
			base.campaign.campaignSave.lastPlayedLevelIdx = this.index;
			LoadingScreen.BeginLoadingPhase(description, new Action(this.LevelLoadComplete), new IEnumerator[]
			{
				this.PlayIslandRoutine()
			});
		}

		// Token: 0x06002EB6 RID: 11958 RVA: 0x000B5FB4 File Offset: 0x000B43B4
		public IEnumerator PlayIslandRoutine()
		{
			bool updateDesc = false;
			while (this.islandProxy.state != IslandProxy.State.Ready)
			{
				updateDesc = true;
				yield return null;
			}
			if (updateDesc)
			{
				LoadingScreen.UpdateDescription(string.Format(ScriptLocalization.LOAD_SCREEN.PREP_ISLAND, ScriptLocalization.Get(this.levelState.nameTerm, true, 0, true, false, null, null)));
			}
			IEnumerator<object> r = this.island.EnterIslandLoading();
			while (r.MoveNext())
			{
				yield return null;
			}
			yield break;
		}

		// Token: 0x06002EB7 RID: 11959 RVA: 0x000B5FD0 File Offset: 0x000B43D0
		private void LevelLoadComplete()
		{
			IslandGameplayManager islandGameplayManager = ExtraSceneManager.FindInSceneRootObjects<IslandGameplayManager>("IslandGameplay");
			islandGameplayManager.EnterIsland(this.island);
			Singleton<Stack>.instance.stateLevel.SetActive(true);
		}

		// Token: 0x06002EB8 RID: 11960 RVA: 0x000B6004 File Offset: 0x000B4404
		public bool IsUnlocked()
		{
			return this.levelState.unlocked || this.isStart;
		}

		// Token: 0x06002EB9 RID: 11961 RVA: 0x000B601F File Offset: 0x000B441F
		public bool IsBehindFrontier()
		{
			return Profile.campaign.vikingFrontierPosition >= this.frontierDepth && this.levelState.checkpointState != LevelState.CheckpointState.Current;
		}

		// Token: 0x06002EBA RID: 11962 RVA: 0x000B604A File Offset: 0x000B444A
		public bool IsBehindNextFrontier()
		{
			return Profile.campaign.vikingFrontierPosition >= this.frontierDepth - 1;
		}

		// Token: 0x06002EBB RID: 11963 RVA: 0x000B6063 File Offset: 0x000B4463
		public bool IsPlayed()
		{
			return this.levelState.playCount > 0;
		}

		// Token: 0x06002EBC RID: 11964 RVA: 0x000B6073 File Offset: 0x000B4473
		public bool IsAvailable()
		{
			return LevelNode.allowLockedLevels || (this.IsUnlocked() && !this.IsBehindFrontier());
		}

		// Token: 0x06002EBD RID: 11965 RVA: 0x000B6098 File Offset: 0x000B4498
		public bool IsPlayable()
		{
			return LevelNode.allowLockedLevels || (this.IsUnlocked() && base.campaign.campaignSave.vikingFrontierPosition < this.frontierDepth && this.checkpoint != LevelState.CheckpointState.Current);
		}

		// Token: 0x06002EBE RID: 11966 RVA: 0x000B60E6 File Offset: 0x000B44E6
		private void Update()
		{
			this.stateRoot.Update();
		}

		// Token: 0x06002EBF RID: 11967 RVA: 0x000B60F3 File Offset: 0x000B44F3
		private void Awake()
		{
			this.islandProxy = base.gameObject.GetComponentInChildren<IslandProxy>();
			this.levelVisuals = base.gameObject.GetComponentInChildren<LevelVisuals>();
		}

		// Token: 0x06002EC0 RID: 11968 RVA: 0x000B6118 File Offset: 0x000B4518
		public IEnumerator Setup2()
		{
			int campaignVisibleDepth = Mathf.Clamp(base.campaign.GetGenerationDepth() - 1, 2, 3);
			LerpTowards anim = new LerpTowards(9f, 0.2f);
			this.links = new LinkSpawner.Link[this.levelState.connections.Count];
			this.played = new AnimatedState("Played", this.rootState, this.levelState.playCount > 0, false, anim);
			this.behindFrontier = new AnimatedState("BehindFrontier", this.rootState, this.IsBehindFrontier(), false, anim);
			this.unlocked = new AnimatedState("Unlocked", this.rootState, this.stepsFromUnlock == 0, false, anim);
			this.visible = new AnimatedState("Visible", this.rootState, this.stepsFromUnlock <= campaignVisibleDepth, false, anim);
			this.encouraged = new AnimatedState("Encouraged", this.rootState, false, false, anim);
			Action<bool> encourageFunc = delegate(bool x)
			{
				this.encouraged.SetActive(this.unlocked.active && !this.behindFrontier.active && !this.played.active);
			};
			this.unlocked.Subscribe(encourageFunc);
			this.behindFrontier.Subscribe(encourageFunc);
			this.played.Subscribe(encourageFunc);
			this.encouraged.ForceToTarget();
			this.available = new AnimatedState("Available", this.rootState, false, false, anim);
			Action<bool> avaliableFunc = delegate(bool x)
			{
				this.available.SetActive(this.unlocked.active && !this.behindFrontier.active);
			};
			this.unlocked.Subscribe(avaliableFunc);
			this.behindFrontier.Subscribe(avaliableFunc);
			this.available.ForceToTarget();
			LevelNode.ILevelSetup[] setuppers = base.GetComponentsInChildren<LevelNode.ILevelSetup>(true);
			foreach (LevelNode.ILevelSetup setup in setuppers)
			{
				setup.OnLevelSetup(this);
				yield return null;
			}
			AnimatedState unlocked = this.unlocked;
			unlocked.OnActivate = (Action)Delegate.Combine(unlocked.OnActivate, new Action(delegate()
			{
				EventManager.Instance.PostEvent("UI/Map/Unlocked");
			}));
			AnimatedState visible = this.visible;
			visible.OnActivate = (Action)Delegate.Combine(visible.OnActivate, new Action(delegate()
			{
				EventManager.Instance.PostEvent("UI/Map/Visible");
			}));
			AnimatedState behindFrontier = this.behindFrontier;
			behindFrontier.OnActivate = (Action)Delegate.Combine(behindFrontier.OnActivate, new Action(delegate()
			{
				EventManager.Instance.PostEvent("UI/Map/BehindFrontier");
			}));
			AnimatedState behindFrontier2 = this.behindFrontier;
			behindFrontier2.OnActivate = (Action)Delegate.Combine(behindFrontier2.OnActivate, new Action(this.levelVisuals.UpdateHouses));
			AnimatedState played = this.played;
			played.OnActivate = (Action)Delegate.Combine(played.OnActivate, new Action(this.levelVisuals.UpdateHouses));
			base.transform.localPosition = this.levelState.pos;
			float num = 0f;
			for (int j = 0; j < this.possibleShips.Count; j++)
			{
				num += this.possibleShips[j].area / (this.possibleShips[j].radius * 2f) / (float)this.possibleShips.Count;
			}
			float num2 = 0f;
			for (int k = 0; k < this.enemies.Count; k++)
			{
				num2 += this.enemies[k].agent.area / (float)this.enemies[k].bounty / (float)this.enemies.Count;
			}
			this.minimumBeach = num2 / num * (float)this.levelState.totalBounty * 1.3f + 1f;
			if (this.minimumBeach > (float)(this.levelState.width * 3))
			{
				Debug.LogWarning(string.Format("Minimum beach is very large {0}, {1}", this.minimumBeach, this.levelState.dbgName), this);
			}
			yield break;
		}

		// Token: 0x06002EC1 RID: 11969 RVA: 0x000B6134 File Offset: 0x000B4534
		public bool ShouldBeVisible()
		{
			int num = Mathf.Clamp(base.campaign.GetGenerationDepth() - 1, 2, 3);
			return this.stepsFromUnlock <= num;
		}

		// Token: 0x06002EC2 RID: 11970 RVA: 0x000B6162 File Offset: 0x000B4562
		public bool MaybeVisible()
		{
			return this.visible.SetActive(this.ShouldBeVisible());
		}

		// Token: 0x06002EC3 RID: 11971 RVA: 0x000B6178 File Offset: 0x000B4578
		public void Played()
		{
			this.played.SetActive(true);
			EndOfLevel.Reason reason = this.lastReason;
			if (reason != EndOfLevel.Reason.Won)
			{
				if (reason != EndOfLevel.Reason.Wiped)
				{
					if (reason == EndOfLevel.Reason.Fled)
					{
						this.onWiped();
						EventManager.Instance.PostEvent("UI/Map/Fled");
					}
				}
				else
				{
					EventManager.Instance.PostEvent("UI/Map/Lost");
					this.onWiped();
				}
			}
			else
			{
				EventManager.Instance.PostEvent("UI/Map/Won");
				this.onWon();
			}
			this.levelVisuals.UpdateHouses();
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06002EC4 RID: 11972 RVA: 0x000B621E File Offset: 0x000B461E
		// (set) Token: 0x06002EC5 RID: 11973 RVA: 0x000B6230 File Offset: 0x000B4630
		public Vector2 pos
		{
			get
			{
				return base.transform.localPosition;
			}
			set
			{
				base.transform.localPosition = value;
			}
		}

		// Token: 0x06002EC6 RID: 11974 RVA: 0x000B6244 File Offset: 0x000B4644
		private void OnDrawGizmos()
		{
			if (this.levelState == null)
			{
				return;
			}
			Gizmos.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 45f, 0f), Vector3.one);
			Gizmos.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(90f, 0f, 0f), Vector3.one) * Gizmos.matrix;
			Gizmos.matrix = base.transform.localToWorldMatrix * Gizmos.matrix;
			Gizmos.color = Gizmos.color.SetComponent(3, 0.7f);
			ExtraGizmos.DrawCircle(Vector3.zero, this.innerRadius, 16);
			Gizmos.color = Gizmos.color.SetComponent(3, 0.1f);
			ExtraGizmos.DrawCircle(Vector3.zero, this.outerRadius, 16);
		}

		// Token: 0x06002EC7 RID: 11975 RVA: 0x000B6320 File Offset: 0x000B4720
		private void OnDestroy()
		{
			this.levelState = null;
			this.links = null;
			this.onStepsFromUnlockChange = null;
			this.rootState.OnDestroy();
			this.stateRoot = null;
			this.behindFrontier = null;
			this.behindFrontier = null;
			this.unlocked = null;
			this.played = null;
			this.encouraged = null;
			this.available = null;
			this.visible = null;
			this.islandProxy = null;
			this.levelVisuals = null;
			this.enemies = null;
			this.possibleShips = null;
		}

		// Token: 0x04001EC7 RID: 7879
		private static DebugChannelGroup dbgGroup = new DebugChannelGroup("LevelNode", EVerbosity.Quiet, 100);

		// Token: 0x04001EC9 RID: 7881
		[SerializeField]
		private int _stepsFromUnlock = int.MaxValue;

		// Token: 0x04001ECA RID: 7882
		public LinkSpawner.Link[] links;

		// Token: 0x04001ECB RID: 7883
		public Action onStepsFromUnlockChange = delegate()
		{
		};

		// Token: 0x04001ECC RID: 7884
		[SerializeField]
		protected AgentStateRoot stateRoot = new AgentStateRoot(4);

		// Token: 0x04001ED3 RID: 7891
		public float stepsFromUnlockLerp;

		// Token: 0x04001ED4 RID: 7892
		public float playCountLerp;

		// Token: 0x04001ED5 RID: 7893
		public IslandProxy islandProxy;

		// Token: 0x04001ED6 RID: 7894
		public LevelVisuals levelVisuals;

		// Token: 0x04001ED7 RID: 7895
		[NonSerialized]
		public EndOfLevel.Reason lastReason;

		// Token: 0x04001ED8 RID: 7896
		public List<VikingReference> enemies = new List<VikingReference>();

		// Token: 0x04001ED9 RID: 7897
		public List<Longship> possibleShips = new List<Longship>();

		// Token: 0x04001EDA RID: 7898
		public float minimumBeach;

		// Token: 0x04001EDB RID: 7899
		[NonSerialized]
		public CampaignDifficultySettings diffiucltySettings;

		// Token: 0x04001EDC RID: 7900
		public Action onWon = delegate()
		{
		};

		// Token: 0x04001EDD RID: 7901
		public Action onWiped = delegate()
		{
		};

		// Token: 0x04001EDE RID: 7902
		public Action onFled = delegate()
		{
		};

		// Token: 0x04001EDF RID: 7903
		[ConsoleCommand("")]
		private static bool allowLockedLevels = false;

		// Token: 0x0200070B RID: 1803
		public interface ILevelSetup
		{
			// Token: 0x06002ECE RID: 11982
			void OnLevelSetup(LevelNode level);
		}
	}
}
