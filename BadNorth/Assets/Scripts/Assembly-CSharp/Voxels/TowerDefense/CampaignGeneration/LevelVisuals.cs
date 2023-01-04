using System;
using System.Collections.Generic;
using I2.Loc;
using RTM.Pools;
using RTM.UISystem;
using UnityEngine;
using UnityEngine.UI;
using Voxels.TowerDefense.ScriptAnimations;
using Voxels.TowerDefense.UI;

namespace Voxels.TowerDefense.CampaignGeneration
{
	// Token: 0x02000712 RID: 1810
	public class LevelVisuals : ChildComponent<LevelNode>, LevelNode.ILevelSetup
	{
		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06002EF0 RID: 12016 RVA: 0x000B80AF File Offset: 0x000B64AF
		public Sprite sprite
		{
			get
			{
				return this.mapImage.sprite;
			}
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06002EF1 RID: 12017 RVA: 0x000B80BC File Offset: 0x000B64BC
		public float scaling
		{
			get
			{
				return (float)base.manager.width / this.referenceWidth;
			}
		}

		// Token: 0x06002EF2 RID: 12018 RVA: 0x000B80D4 File Offset: 0x000B64D4
		void LevelNode.ILevelSetup.OnLevelSetup(LevelNode level)
		{
			this.nameLocalize.Term = level.levelState.nameTerm;
			this.nameLocalize.gameObject.AddComponent<BoxCollider2D>();
			this.animator = base.GetComponent<Animator>();
			LevelSpawner levelSpawner = base.manager.campaign.levelSpawner;
			this.spriteState = new AnimatedState("HasSprite", level.rootState, false, false, LerpTowards.standard);
			this.cloudState = new AnimatedState("CloudState", level.rootState, false, false, new LerpTowards(6f, 0.1f));
			this.hoverState = new AnimatedState("HoverState", level.rootState, false, false, LerpTowards.standard);
			this.linkedState = new AnimatedState("Linked", level.rootState, base.manager.played.active, false, new LerpTowards(8f, 0.1f));
			base.manager.unlocked.Subscribe(this.animator, "Unlocked");
			base.manager.behindFrontier.Subscribe(this.animator, "Frontier");
			base.manager.played.Subscribe(this.animator, "Played");
			base.manager.encouraged.Subscribe(this.animator, "Encouraged");
			base.manager.available.Subscribe(this.animator, "Available");
			this.hoverState.Subscribe(this.animator, "Hover");
			LevelNode manager = base.manager;
			manager.onWon = (Action)Delegate.Combine(manager.onWon, new Action(delegate()
			{
				this.animator.SetTrigger("Won");
			}));
			Text nameText = this.nameLocalize.GetComponent<Text>();
			if (base.manager.isStartOrEnd)
			{
				nameText.resizeTextMaxSize = nameText.resizeTextMaxSize * 4 / 3;
			}
			this.rt.sizeDelta = (float)level.width * Vector2.one * 10f * Mathf.Sqrt(2f);
			if (base.manager.hasHero)
			{
				this.bannerGraphic.sprite = base.manager.heroDefinition.graphics.flag;
				this.bannerGraphic.color = base.manager.heroDefinition.color;
			}
			this.mapImage.sprite = base.manager.campaign.levelSpriteBaker.GetSprite(base.manager.levelState);
			this.mapImage.SetNativeSize();
			if (base.manager.levelState.hasSprite)
			{
				this.spriteState.SetActive(true);
			}
			this.houses = new LocalPool<LevelNodeHouse>(this.houseExample, this.houseExample.transform.parent);
			this.SetupHouses();
			base.manager.islandProxy.onStateChanged += delegate(IslandProxy.State x)
			{
				if (x == IslandProxy.State.Ready)
				{
					this.spriteState.SetActive(true);
					this.SetupHouses();
				}
			};
			LevelNodeEnemy componentInChildren = base.GetComponentInChildren<LevelNodeEnemy>();
			List<VikingReference> enemies = base.manager.enemies;
			this.enemyVisuals = new List<LevelNodeEnemy>(enemies.Count);
			for (int i = 0; i < enemies.Count; i++)
			{
				this.enemyVisuals.Add(UnityEngine.Object.Instantiate<LevelNodeEnemy>(componentInChildren, componentInChildren.transform.parent).Setup(enemies[i]));
			}
			float num = 4f;
			if (this.enemyVisuals.Count % 2 == 1)
			{
				num = this.enemyVisuals[enemies.Count / 2].width * 0.5f + 2f;
			}
			for (int j = -1; j <= 1; j += 2)
			{
				float num2 = num;
				for (int k = 0; k < this.enemyVisuals.Count / 2; k++)
				{
					int index = (j >= 0) ? (this.enemyVisuals.Count / 2 + k + this.enemyVisuals.Count % 2) : (this.enemyVisuals.Count / 2 - k - 1);
					LevelNodeEnemy levelNodeEnemy = this.enemyVisuals[index];
					num2 += levelNodeEnemy.width * 0.43f;
					levelNodeEnemy.transform.localPosition = new Vector3(num2 * (float)j, -num2 + 6f);
					num2 += levelNodeEnemy.width * 0.43f;
				}
			}
			this.UpdateEnemies();
			UnityEngine.Object.Destroy(componentInChildren.gameObject);
			this.start.gameObject.SetActive(base.manager.isStart);
			this.end.gameObject.SetActive(base.manager.isEnd);
			Action<bool> onChange = delegate(bool x)
			{
				this.cloudState.SetActive(this.manager.visible.active && this.spriteState.active);
			};
			this.spriteState.Subscribe(onChange);
			base.manager.visible.Subscribe(onChange);
			this.SetUIState(UIInteractable.State.None);
			this.nameLocalize.transform.SetParent(levelSpawner.nameContainer);
			this.enemyCanvasGroup.transform.SetParent(levelSpawner.enemyContainer);
			this.housesCanvasGroup.transform.SetParent(levelSpawner.houseContainer);
			this.mapImage.transform.SetParent(levelSpawner.mapContainer);
			base.manager.available.anim.Subscribe(delegate(float x)
			{
				Vector3 vector = Vector3.Lerp(new Vector3(0.9f, 0.9f, 1f), Vector3.one, x);
				vector = Vector3.Scale(vector, this.transform.localScale);
				this.mapImage.transform.localScale = vector;
				this.housesCanvasGroup.transform.localScale = vector;
				nameText.color = nameText.color.SetA(Mathf.Lerp(0.5f, 0.7f, x));
			});
			Vector3 textPos = nameText.transform.position;
			base.manager.encouraged.anim.Subscribe(delegate(float x)
			{
				nameText.transform.position = textPos + Vector3.up * 22f * x * this.transform.localScale.y;
				this.enemyCanvasGroup.alpha = x;
			});
			base.manager.behindFrontier.anim.Subscribe(delegate(float x)
			{
				this.mapImage.color = this.mapImage.color.SetR(x);
			});
			base.manager.unlocked.anim.Subscribe(delegate(float x)
			{
				this.mapImage.color = this.mapImage.color.SetG(x);
			});
			this.hoverState.anim.Subscribe(delegate(float x)
			{
				this.mapImage.color = this.mapImage.color.SetB(x);
			});
			base.manager.played.anim.Subscribe(delegate(float x)
			{
				this.mapImage.color = this.mapImage.color.SetA(x);
			});
			this.cloudState.Subscribe(delegate(bool x)
			{
				this.gameObject.SetActive(x);
				this.enemyCanvasGroup.gameObject.SetActive(x);
				this.housesCanvasGroup.gameObject.SetActive(x);
				this.mapImage.gameObject.SetActive(x);
				this.nameLocalize.gameObject.SetActive(x);
			});
			for (int l = 0; l < this.decors.Length; l++)
			{
				Transform transform = this.decors[l].transform;
				while (transform != base.transform)
				{
					transform = transform.transform.parent;
					if (transform == base.transform)
					{
						this.decors[l].transform.SetParent(levelSpawner.decorContainer);
						this.cloudState.Subscribe(new Action<bool>(this.decors[l].gameObject.SetActive));
					}
					else if (!transform.gameObject.activeSelf)
					{
						UnityEngine.Object.Destroy(this.decors[l].gameObject);
						break;
					}
				}
			}
			if (base.manager.hasItem)
			{
				AnimatedState itemState = new AnimatedState("Item", level.rootState, this.ShouldShowItem(), false, LerpTowards.standard);
				itemState.Subscribe(this.itemCanvasGroup);
				Action<bool> b = delegate(bool x)
				{
					itemState.SetActive(this.ShouldShowItem());
				};
				AnimatedState behindFrontier = base.manager.behindFrontier;
				behindFrontier.OnChange = (Action<bool>)Delegate.Combine(behindFrontier.OnChange, b);
				AnimatedState played = base.manager.played;
				played.OnChange = (Action<bool>)Delegate.Combine(played.OnChange, b);
				AnimatedState animatedState = this.cloudState;
				animatedState.OnChange = (Action<bool>)Delegate.Combine(animatedState.OnChange, b);
				CanvasGroup canvasGroup = this.itemCanvasGroup;
				canvasGroup.name = canvasGroup.name + " " + base.name;
				this.itemCanvasGroup.transform.SetParent(levelSpawner.itemContainer);
				if (this.metaItem)
				{
					this.metaItem.gameObject.SetActive(base.manager.levelState.metaReward);
				}
			}
			else
			{
				UnityEngine.Object.Destroy(this.itemCanvasGroup.gameObject);
			}
			if (base.manager.hasHero)
			{
				AnimatedState heroState = new AnimatedState("Hero", level.rootState, this.ShouldShowHero(), false, LerpTowards.standard);
				heroState.Subscribe(this.heroCanvasGroup);
				Action<bool> b2 = delegate(bool x)
				{
					heroState.SetActive(this.ShouldShowHero());
				};
				AnimatedState behindFrontier2 = base.manager.behindFrontier;
				behindFrontier2.OnChange = (Action<bool>)Delegate.Combine(behindFrontier2.OnChange, b2);
				AnimatedState played2 = base.manager.played;
				played2.OnChange = (Action<bool>)Delegate.Combine(played2.OnChange, b2);
				AnimatedState animatedState2 = this.cloudState;
				animatedState2.OnChange = (Action<bool>)Delegate.Combine(animatedState2.OnChange, b2);
				CanvasGroup canvasGroup2 = this.heroCanvasGroup;
				canvasGroup2.name = canvasGroup2.name + " " + base.name;
				this.heroCanvasGroup.transform.SetParent(levelSpawner.bannerContainer);
				if (this.metaTrait)
				{
					this.metaTrait.gameObject.SetActive(base.manager.levelState.metaReward);
				}
			}
			else
			{
				UnityEngine.Object.Destroy(this.heroCanvasGroup.gameObject);
			}
			AnimatedState currentCheckpoint = null;
			if (base.manager.hasCheckpoint)
			{
				AgentState agentState = new AgentState("Checkpoint", level.rootState, this.ShouldShowCheckpoint(), false);
				AnimatedState idle = new AnimatedState("Idle", level.rootState, false, true, LerpTowards.standard);
				AnimatedState dead = new AnimatedState("Dead", level.rootState, false, true, LerpTowards.standard);
				AnimatedState current = currentCheckpoint = new AnimatedState("Current", level.rootState, false, true, LerpTowards.standard);
				Action<bool> action = delegate(bool x)
				{
					if (this.IsCurrentCheckpoint())
					{
						current.SetActive(true);
					}
					else if (this.ShouldShowCheckpoint())
					{
						idle.SetActive(true);
					}
					else
					{
						dead.SetActive(true);
					}
				};
				AnimatedState behindFrontier3 = base.manager.behindFrontier;
				behindFrontier3.OnChange = (Action<bool>)Delegate.Combine(behindFrontier3.OnChange, action);
				AnimatedState played3 = base.manager.played;
				played3.OnChange = (Action<bool>)Delegate.Combine(played3.OnChange, action);
				AnimatedState animatedState3 = this.cloudState;
				animatedState3.OnChange = (Action<bool>)Delegate.Combine(animatedState3.OnChange, action);
				this.updateCheckpoint = (Action<bool>)Delegate.Combine(this.updateCheckpoint, action);
				CanvasGroup canvasGroup3 = this.checkpointCanvasGroup;
				canvasGroup3.name = canvasGroup3.name + " " + base.name;
				this.checkpointCanvasGroup.transform.SetParent(levelSpawner.decorContainer);
				Vector3 scale0 = this.checkpoint0.transform.localScale;
				Vector3 scale1 = scale0;
				scale0 = (scale0 * 0.95f).SetZ(scale0.z);
				scale1 = (scale1 * 1.02f).SetZ(scale1.z);
				current.AddOnActivity(new Action<bool>(this.checkpoint1.gameObject.SetActive));
				Image chekpointImage0 = this.checkpoint0.GetComponent<Image>();
				Image chekpointImage1 = this.checkpoint1.GetComponent<Image>();
				float defaultAlpha0 = chekpointImage0.color.a;
				float defaultAlpha1 = chekpointImage1.color.a;
				current.anim.Subscribe(delegate(float x)
				{
					this.checkpoint0.fraction = Mathf.Lerp(0.55f, 0.6f, x);
					this.checkpoint1.fraction = Mathf.Lerp(0.55f, 0.4f, x);
					Vector3 localScale = Vector3.Lerp(scale0, scale1, x);
					this.checkpoint0.transform.localScale = localScale;
					this.checkpoint1.transform.localScale = localScale;
					this.checkpoint1.pixelWidth = x * this.checkpoint0.pixelWidth;
					chekpointImage0.color = chekpointImage0.color.SetA(Mathf.Lerp(defaultAlpha0, 0.9f, x));
					chekpointImage1.color = chekpointImage1.color.SetA(Mathf.Lerp(defaultAlpha1, 0.9f, x));
				});
				dead.InverseSubscribe(this.checkpointCanvasGroup);
				Vector3 deadScale = this.checkpointCanvasGroup.transform.localScale;
				dead.anim.Subscribe(delegate(float x)
				{
					this.checkpointCanvasGroup.transform.localScale = new Vector3(deadScale.x * (1f - x), deadScale.y * (1f - x), deadScale.z);
				});
				action(true);
			}
			else
			{
				UnityEngine.Object.Destroy(this.checkpointCanvasGroup.gameObject);
			}
			this.hoverState.OnUpdate += delegate()
			{
				float a = this.manager.campaign.heroesAvaliable.value * this.manager.available.value;
				float b3 = (currentCheckpoint == null) ? 0f : currentCheckpoint.value;
				this.animator.SetFloat(LevelVisuals.playableId, Mathf.Max(a, b3));
			};
		}

		// Token: 0x06002EF3 RID: 12019 RVA: 0x000B8D5B File Offset: 0x000B715B
		public void UpdateCheckpoint()
		{
			this.updateCheckpoint(false);
		}

		// Token: 0x06002EF4 RID: 12020 RVA: 0x000B8D69 File Offset: 0x000B7169
		private bool ShouldShowItem()
		{
			return base.manager.hasItem && !base.manager.IsBehindFrontier() && this.cloudState.active;
		}

		// Token: 0x06002EF5 RID: 12021 RVA: 0x000B8D9C File Offset: 0x000B719C
		private bool ShouldShowHero()
		{
			return base.manager.hasHero && base.manager.heroDefinition.recruitable && !base.manager.behindFrontier.active && this.cloudState.active;
		}

		// Token: 0x06002EF6 RID: 12022 RVA: 0x000B8DF1 File Offset: 0x000B71F1
		private bool ShouldShowCheckpoint()
		{
			return (base.manager.levelState.checkpointState == LevelState.CheckpointState.Available || base.manager.levelState.checkpointState == LevelState.CheckpointState.Saved) && this.cloudState.active;
		}

		// Token: 0x06002EF7 RID: 12023 RVA: 0x000B8E2D File Offset: 0x000B722D
		private bool IsCurrentCheckpoint()
		{
			return base.manager.levelState.checkpointState == LevelState.CheckpointState.Current;
		}

		// Token: 0x06002EF8 RID: 12024 RVA: 0x000B8E44 File Offset: 0x000B7244
		private void OnEnable()
		{
			if (this.animator)
			{
				this.animator.SetFloat("Unlocked", base.manager.unlocked.value);
				this.animator.SetFloat("Frontier", base.manager.behindFrontier.value);
				this.animator.SetFloat("Played", base.manager.played.value);
				this.animator.SetFloat("Encouraged", base.manager.encouraged.value);
				this.animator.SetFloat("Available", base.manager.available.value);
				base.manager.unlocked.anim.Update();
				base.manager.behindFrontier.anim.Update();
				base.manager.played.anim.Update();
				this.hoverState.anim.Update();
			}
		}

		// Token: 0x06002EF9 RID: 12025 RVA: 0x000B8F50 File Offset: 0x000B7350
		private void SetupHouses()
		{
			if (!base.manager.levelState.hasSprite)
			{
				return;
			}
			this.houses.ReturnAll();
			for (int i = 0; i < base.manager.levelState.houses.Length; i++)
			{
				this.houses.GetInstance().Setup(base.manager, base.manager.levelState.houses[i]);
			}
			this.UpdateHouses();
			AnimatedState behindFrontier = base.manager.behindFrontier;
			behindFrontier.OnActivate = (Action)Delegate.Combine(behindFrontier.OnActivate, new Action(this.UpdateHouses));
			AnimatedState played = base.manager.played;
			played.OnActivate = (Action)Delegate.Combine(played.OnActivate, new Action(this.UpdateHouses));
		}

		// Token: 0x06002EFA RID: 12026 RVA: 0x000B9034 File Offset: 0x000B7434
		public void UpdateHouses()
		{
			if (base.manager.levelState.hasSprite)
			{
				for (int i = 0; i < base.manager.levelState.houses.Length; i++)
				{
					HouseState houseState = base.manager.levelState.houses[i];
					Color white = Color.white;
					if (base.manager.IsBehindFrontier())
					{
						HouseState.Condition condition = houseState.condition;
						if (condition != HouseState.Condition.Saved)
						{
							this.houses.inUse[i].SetColor(this.pillagedColor, false, 0.6f);
						}
						else
						{
							this.houses.inUse[i].SetColor(this.frontierColor, true, 1f);
						}
					}
					else
					{
						switch (houseState.condition)
						{
						case HouseState.Condition.Intact:
							this.houses.inUse[i].SetColor(this.intactColor, true, 1f);
							break;
						case HouseState.Condition.Pillaged:
							this.houses.inUse[i].SetColor(this.pillagedColor, false, 0.6f);
							break;
						case HouseState.Condition.Saved:
							this.houses.inUse[i].SetColor(this.savedColor, true, 1f);
							break;
						}
					}
				}
			}
		}

		// Token: 0x06002EFB RID: 12027 RVA: 0x000B91B0 File Offset: 0x000B75B0
		public bool UpdateEnemies()
		{
			bool flag = false;
			foreach (LevelNodeEnemy levelNodeEnemy in this.enemyVisuals)
			{
				if (levelNodeEnemy.UpdateVisual())
				{
					flag = true;
				}
			}
			return base.manager.encouraged.active && flag;
		}

		// Token: 0x06002EFC RID: 12028 RVA: 0x000B9230 File Offset: 0x000B7630
		public void SetUIState(UIInteractable.State state)
		{
			this.hoverState.SetActive(state != UIInteractable.State.None);
		}

		// Token: 0x06002EFD RID: 12029 RVA: 0x000B9248 File Offset: 0x000B7648
		private void OnDrawGizmos()
		{
			Gizmos.color = Color.yellow;
			Gizmos.matrix = base.transform.localToWorldMatrix * Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(90f, 0f, 0f), Vector3.one);
			ExtraGizmos.DrawCircle(Vector3.zero, this.referenceWidth * 0.8f, 16);
			Gizmos.matrix = base.transform.localToWorldMatrix * Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, 45f), Vector3.one);
			Gizmos.DrawWireCube(Vector3.zero, Vector2.one * this.referenceWidth);
		}

		// Token: 0x04001EF6 RID: 7926
		[SerializeField]
		private RectTransform rt;

		// Token: 0x04001EF7 RID: 7927
		[SerializeField]
		private Localize nameLocalize;

		// Token: 0x04001EF8 RID: 7928
		[SerializeField]
		public Image mapImage;

		// Token: 0x04001EF9 RID: 7929
		[SerializeField]
		private LevelNodeHouse houseExample;

		// Token: 0x04001EFA RID: 7930
		private LocalPool<LevelNodeHouse> houses;

		// Token: 0x04001EFB RID: 7931
		[SerializeField]
		private List<LevelNodeEnemy> enemyVisuals;

		// Token: 0x04001EFC RID: 7932
		[SerializeField]
		private Image bannerGraphic;

		// Token: 0x04001EFD RID: 7933
		[SerializeField]
		private GameObject start;

		// Token: 0x04001EFE RID: 7934
		[SerializeField]
		private GameObject end;

		// Token: 0x04001EFF RID: 7935
		[SerializeField]
		private GameObject metaItem;

		// Token: 0x04001F00 RID: 7936
		[SerializeField]
		private GameObject metaTrait;

		// Token: 0x04001F01 RID: 7937
		public AnimatedState spriteState;

		// Token: 0x04001F02 RID: 7938
		public AnimatedState cloudState;

		// Token: 0x04001F03 RID: 7939
		public AnimatedState hoverState;

		// Token: 0x04001F04 RID: 7940
		public AnimatedState linkedState;

		// Token: 0x04001F05 RID: 7941
		[SerializeField]
		private GameObject[] decors;

		// Token: 0x04001F06 RID: 7942
		[SerializeField]
		private Color targetColor;

		// Token: 0x04001F07 RID: 7943
		[SerializeField]
		private CanvasGroup canvasGroup;

		// Token: 0x04001F08 RID: 7944
		[SerializeField]
		private CanvasGroup itemCanvasGroup;

		// Token: 0x04001F09 RID: 7945
		[SerializeField]
		private CanvasGroup heroCanvasGroup;

		// Token: 0x04001F0A RID: 7946
		[SerializeField]
		private CanvasGroup housesCanvasGroup;

		// Token: 0x04001F0B RID: 7947
		[SerializeField]
		private CanvasGroup enemyCanvasGroup;

		// Token: 0x04001F0C RID: 7948
		[SerializeField]
		private CanvasGroup checkpointCanvasGroup;

		// Token: 0x04001F0D RID: 7949
		[SerializeField]
		private DistanceFieldSettings checkpoint0;

		// Token: 0x04001F0E RID: 7950
		[SerializeField]
		private DistanceFieldSettings checkpoint1;

		// Token: 0x04001F0F RID: 7951
		private static AnimId playableId = "Playable";

		// Token: 0x04001F10 RID: 7952
		[Space]
		public float referenceWidth = 9f;

		// Token: 0x04001F11 RID: 7953
		public Animator animator;

		// Token: 0x04001F12 RID: 7954
		[Header("HouseColors")]
		[SerializeField]
		public Color intactColor;

		// Token: 0x04001F13 RID: 7955
		[SerializeField]
		public Color savedColor;

		// Token: 0x04001F14 RID: 7956
		[SerializeField]
		public Color pillagedColor;

		// Token: 0x04001F15 RID: 7957
		[SerializeField]
		public Color frontierColor;

		// Token: 0x04001F16 RID: 7958
		private static RectTransform allTextsRoot;

		// Token: 0x04001F17 RID: 7959
		private Action<bool> updateCheckpoint = delegate(bool A_0)
		{
		};
	}
}
