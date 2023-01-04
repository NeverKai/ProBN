using System;
using System.Collections;
using I2.Loc;
using Rewired;
using RTM.Input;
using RTM.UISystem;
using RTM.Utilities;
using SpriteComposing;
using UnityEngine;
using UnityEngine.UI;
using Voxels.TowerDefense.CampaignGeneration;
using Voxels.TowerDefense.ScriptAnimations;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x0200054C RID: 1356
	public class EndOfLevelUIRoot : UIMenu, IslandUIManager.IAwake, IslandGameplayManager.ISetupIsland, IslandGameplayManager.IWipeIsland
	{
		// Token: 0x0600233F RID: 9023 RVA: 0x0006ABF4 File Offset: 0x00068FF4
		void IslandUIManager.IAwake.OnAwake(IslandUIManager manager)
		{
			this.gameplayManager = manager.gameplayManager;
			this.levelCamera = this.gameplayManager.levelCamera;
			this.islandViewfinder = manager.islandViewfinder;
			this.gameplayManager.endOfLevel.preProcess += this.PreProcessEndOfLevel;
			this.shaderModifier = base.GetComponent<ShaderConstantsOverride>();
			this.grayscale = new AnimatedState("Gray", this.root.rootState, false, false);
			this.grayscale.anim.Subscribe(delegate(float x)
			{
				this.shaderModifier.alpha = x;
			});
			this.buttonVisibility = this.ButtonContainer.GetComponent<IUIVisibility>();
			this.buttonVisibility.SetVisible(false, true);
			this.victoryHeroLineup.gameObject.SetActive(false);
			this.victoryCard.gameObject.SetActive(false);
			this.defeatCard.gameObject.SetActive(false);
			this.recruitGameObject.SetActive(false);
			this.checkpointGameObject.SetActive(false);
			this.itemGameObject.SetActive(false);
			this.metaItemGameObject.SetActive(false);
			this.traitGameObject.SetActive(false);
			this.metaTraitGameObject.SetActive(false);
			this.itemGameObject.transform.localPosition = Vector2.zero;
			this.recruitGameObject.transform.localPosition = Vector2.zero;
			this.checkpointGameObject.transform.localPosition = Vector2.zero;
			Transform parent = this.itemGameObject.transform.parent;
			parent.transform.localPosition = Vector2.zero;
		}

		// Token: 0x06002340 RID: 9024 RVA: 0x0006AD98 File Offset: 0x00069198
		private void Update()
		{
			this.root.Update();
		}

		// Token: 0x06002341 RID: 9025 RVA: 0x0006ADA5 File Offset: 0x000691A5
		private void PreProcessEndOfLevel(Island island, EndOfLevel.Reason reason)
		{
			this.eolReason = reason;
		}

		// Token: 0x06002342 RID: 9026 RVA: 0x0006ADAE File Offset: 0x000691AE
		public void OnEnable()
		{
			this.buttonVisibility.SetVisible(false, true);
			this.replayClickable.gameObject.SetActive(this.AllowRestart());
			base.StartCoroutine(this.eolRoutine);
		}

		// Token: 0x06002343 RID: 9027 RVA: 0x0006ADE0 File Offset: 0x000691E0
		public void OnDisable()
		{
			TimeManager.RemoveTimeScale(this);
		}

		// Token: 0x06002344 RID: 9028 RVA: 0x0006ADE8 File Offset: 0x000691E8
		private IEnumerator EOLRoutine(LevelNode levelNode)
		{
			yield return null;
			float endTime = Time.unscaledTime + 0.5f;
			this.heroAtlasFixImage.texture = levelNode.campaign.heroGenerator.GetComponent<SpriteComposer>().tex;
			yield return null;
			foreach (Squad squad in this.gameplayManager.island.english.allSquads)
			{
				EnglishSquad enSquad = (EnglishSquad)squad;
				IEnumerator enumerator4 = enSquad.hero.monoHero.UpdateAliveSprite(enSquad.heroAlive, false).GetEnumerator();
				try
				{
					while (enumerator4.MoveNext())
					{
						object spriteUpdate = enumerator4.Current;
						yield return null;
					}
				}
				finally
				{
					IDisposable disposable;
					if ((disposable = (enumerator4 as IDisposable)) != null)
					{
						disposable.Dispose();
					}
				}
			}
			while (Time.unscaledTime < endTime)
			{
				yield return null;
			}
			this.gameplayManager.states.EOLTitle.SetActive(true);
			this.gameplayManager.states.EOLCoinDispense.SetActive(true);
			this.levelCamera.FocusOnIsland(true, 0.1f);
			FabricWrapper.PostEvent("UI/InGame/CoinScreen");
			this.grayscale.SetActive(true);
			if (this.eolReason == EndOfLevel.Reason.Won)
			{
				IEnumerator enumerator = this.EolWinRoutine(levelNode);
				while (enumerator.MoveNext())
				{
					yield return null;
				}
			}
			else
			{
				this.defeatHeader.gameObject.SetActive(this.eolReason == EndOfLevel.Reason.Wiped);
				this.fledheader.gameObject.SetActive(this.eolReason == EndOfLevel.Reason.Fled);
				TimeManager.RequestTimeScale(this, 0.1f);
				IEnumerator enumerator2 = this.EolDefeatRoutine(levelNode);
				while (enumerator2.MoveNext())
				{
					yield return null;
				}
			}
			this.grayscale.SetActive(false);
			FabricWrapper.PostEvent("UI/InGame/CoinDone");
			float endTimeA = Time.unscaledTime + 0.3f;
			foreach (Squad squad2 in this.gameplayManager.island.english.allSquads)
			{
				EnglishSquad enSquad2 = (EnglishSquad)squad2;
				IEnumerator enumerator6 = enSquad2.hero.monoHero.UpdateAliveSprite(enSquad2.heroAlive, true).GetEnumerator();
				try
				{
					while (enumerator6.MoveNext())
					{
						object spriteUpdate2 = enumerator6.Current;
						yield return null;
					}
				}
				finally
				{
					IDisposable disposable2;
					if ((disposable2 = (enumerator6 as IDisposable)) != null)
					{
						disposable2.Dispose();
					}
				}
			}
			while (Time.unscaledTime < endTimeA)
			{
				yield return null;
			}
			this.gameplayManager.states.EOLCoinDispense.SetActive(false);
			this.gameplayManager.states.EOLContinue.SetActive(true);
			this.buttonVisibility.SetVisible(true, false);
			if (InputHelpers.ControllerTypeIs(ControllerType.Joystick))
			{
				base.FocusOn(this.GetDefaultNavigable());
			}
			while (Mathf.Abs(this.levelCamera.targetPanY - this.levelCamera.position.y) > 0.01f)
			{
				yield return null;
			}
			this.levelCamera.UnlockPanYPos();
			this.levelCamera.GetComponent<CameraZoomer>().LockZoom = false;
			this.heroAtlasFixImage.texture = null;
			yield return null;
			yield break;
		}

		// Token: 0x06002345 RID: 9029 RVA: 0x0006AE0C File Offset: 0x0006920C
		private IEnumerator EolWinRoutine(LevelNode levelNode)
		{
			IEnumerator dispense = this.gameplayManager.coinDispenser.DispenseCoinsRoutine(false);
			while (dispense.MoveNext())
			{
				yield return null;
			}
			float cameraYpos0 = this.levelCamera.targetPanY;
			float cameraYpos = cameraYpos0 + this.levelCamera.orthoHeight * 0.2f;
			this.levelCamera.LockPanYPos(cameraYpos);
			this.victoryCard.Prepare(levelNode);
			if (this.gameplayManager.coinDispenser.totalCoins > 0)
			{
				this.victoryCard.coin.SetActive(true);
				this.victoryCard.coinFlair.SetActive(true);
				this.victoryCard.coin.GetComponentInChildren<Text>(true).text = IntStringCache.GetClean(this.gameplayManager.coinDispenser.totalCoins);
			}
			else
			{
				this.victoryCard.GetComponent<Animator>().playbackTime = 0.6333333f;
				this.victoryCard.coin.SetActive(false);
				this.victoryCard.coinFlair.SetActive(false);
			}
			while (this.victoryCard.playing)
			{
				yield return null;
			}
			this.victoryCard.gameObject.SetActive(false);
			if (this.gameplayManager.endOfLevel.levelItem)
			{
				MaskedSprite maskedSprite = this.itemGameObject.GetComponentInChildren<MaskedSprite>();
				maskedSprite.Set(this.gameplayManager.endOfLevel.levelItem);
				bool isMeta = this.gameplayManager.island.levelNode.levelState.metaReward;
				maskedSprite.transform.localScale = ((!isMeta) ? new Vector3(1.1f, 1.1f, 1f) : Vector3.one);
				MaskedSprite.BorderSettings border = maskedSprite.borders[0];
				border.width = ((!isMeta) ? 0f : 0.3f);
				maskedSprite.borders[0] = border;
				foreach (Localize localize in this.itemLoc)
				{
					localize.Term = this.gameplayManager.endOfLevel.levelItem.nameTerm;
				}
				this.itemGameObject.gameObject.SetActive(true);
				FabricWrapper.PostEvent(EndOfLevelUIRoot.itemFoundAudio);
				if (isMeta)
				{
					float endTime = Time.unscaledTime + 0.6f;
					while (Time.unscaledTime < endTime)
					{
						yield return null;
					}
					this.metaItemGameObject.gameObject.SetActive(true);
					FabricWrapper.PostEvent(EndOfLevelUIRoot.metaItemFoundAudio);
					float endTime2 = Time.unscaledTime + 0.6f;
					while (Time.unscaledTime < endTime2)
					{
						yield return null;
					}
				}
				float endTime3 = Time.unscaledTime + 2.4f;
				while (Time.unscaledTime < endTime3)
				{
					yield return null;
				}
				FabricWrapper.PostEvent(EndOfLevelUIRoot.itemDespawnAudio);
				if (isMeta)
				{
					this.metaItemGameObject.GetComponent<Animator>().Play("Disappear");
					float endTime4 = Time.unscaledTime + 0.3f;
					while (Time.unscaledTime < endTime4)
					{
						yield return null;
					}
				}
				this.itemGameObject.gameObject.GetComponent<Animator>().Play("Disappear");
			}
			if (this.gameplayManager.endOfLevel.checkpointEvent == LevelState.CheckpointState.Saved)
			{
				this.checkpointGameObject.gameObject.SetActive(true);
				this.checkpointReached.gameObject.SetActive(true);
				this.checkpointLost.gameObject.SetActive(false);
				FabricWrapper.PostEvent(EndOfLevelUIRoot.checkpointSavedAudio);
				float endTime5 = Time.unscaledTime + 2.4f;
				while (Time.unscaledTime < endTime5)
				{
					yield return null;
				}
				this.checkpointGameObject.gameObject.GetComponent<Animator>().Play("Disappear");
			}
			if (levelNode.heroDefinition)
			{
				foreach (Squad squad2 in levelNode.island.english.allSquads)
				{
					EnglishSquad squad = (EnglishSquad)squad2;
					if (squad.hero == levelNode.heroDefinition && squad.heroAlive)
					{
						HeroDefinition hero = levelNode.heroDefinition;
						FabricWrapper.PostEvent(EndOfLevelUIRoot.recruitedAudio);
						this.recruitGameObject.GetComponentInChildren<MaskedSprite>().Set(hero.graphics);
						foreach (BannerPolygon bannerPolygon in this.recruitGameObject.EnumerateComponentsInChildren(false, true))
						{
							bannerPolygon.Setup(hero, false);
						}
						this.recruitName.Term = hero.nameTerm;
						GameObject traitObject = null;
						GameObject metaObject = null;
						if (hero.traitUpgrade != null)
						{
							traitObject = this.traitGameObject;
							foreach (Localize localize2 in this.traitLoc)
							{
								localize2.Term = hero.traitUpgrade.nameTerm;
							}
							if (this.gameplayManager.island.levelNode.levelState.metaReward)
							{
								metaObject = this.metaTraitGameObject;
							}
						}
						this.recruitGameObject.SetActive(true);
						if (traitObject)
						{
							traitObject.gameObject.SetActive(true);
							if (metaObject)
							{
								float endTime6 = Time.unscaledTime + 0.6f;
								while (Time.unscaledTime < endTime6)
								{
									yield return null;
								}
								metaObject.gameObject.SetActive(true);
								float endTime7 = Time.unscaledTime + 0.6f;
								while (Time.unscaledTime < endTime7)
								{
									yield return null;
								}
							}
						}
						float endTime8 = Time.unscaledTime + 2.4f;
						while (Time.unscaledTime < endTime8)
						{
							yield return null;
						}
						FabricWrapper.PostEvent(EndOfLevelUIRoot.recruitedDespawnAudio);
						if (traitObject)
						{
							if (metaObject)
							{
								metaObject.GetComponent<Animator>().Play("Disappear");
								float endTime9 = Time.unscaledTime + 0.3f;
								while (Time.unscaledTime < endTime9)
								{
									yield return null;
								}
							}
							traitObject.GetComponent<Animator>().Play("Disappear");
							float endTime10 = Time.unscaledTime + 0.3f;
							while (Time.unscaledTime < endTime10)
							{
								yield return null;
							}
						}
						this.recruitGameObject.GetComponent<Animator>().Play("Disappear");
						float endTime11 = Time.unscaledTime + 0.3f;
						while (Time.unscaledTime < endTime11)
						{
							yield return null;
						}
						break;
					}
				}
			}
			bool anyDead = false;
			int num = 0;
			while (num < levelNode.island.english.allSquads.Count && !anyDead)
			{
				if (!(levelNode.island.english.allSquads[num] as EnglishSquad).heroAlive)
				{
					anyDead = true;
				}
				num++;
			}
			if (anyDead)
			{
				this.victoryHeroLineup.Prepare(levelNode);
				yield return null;
				IEnumerator routine = this.victoryHeroLineup.AnimatedRoutine();
				while (routine.MoveNext())
				{
					yield return null;
				}
			}
			if (this.gameplayManager.endOfLevel.checkpointEvent == LevelState.CheckpointState.Destroyed)
			{
				float endTime12 = Time.unscaledTime + 0.3f;
				while (Time.unscaledTime < endTime12)
				{
					yield return null;
				}
				Animator animator = this.checkpointGameObject.GetComponent<Animator>();
				this.checkpointReached.gameObject.SetActive(false);
				this.checkpointLost.gameObject.SetActive(true);
				animator.gameObject.SetActive(true);
				animator.Play("Lost");
				FabricWrapper.PostEvent(EndOfLevelUIRoot.checkpointLostAudio);
				yield return null;
				while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
				{
					yield return null;
				}
				animator.gameObject.SetActive(false);
			}
			float endTime13 = Time.unscaledTime + 0.3f;
			while (Time.unscaledTime < endTime13)
			{
				yield return null;
			}
			this.itemGameObject.SetActive(false);
			this.metaItemGameObject.SetActive(false);
			this.recruitGameObject.SetActive(false);
			this.checkpointGameObject.SetActive(false);
			this.traitGameObject.SetActive(false);
			this.metaTraitGameObject.SetActive(false);
			this.levelCamera.LockPanYPos(cameraYpos0);
			yield break;
		}

		// Token: 0x06002346 RID: 9030 RVA: 0x0006AE30 File Offset: 0x00069230
		private IEnumerator EolDefeatRoutine(LevelNode levelNode)
		{
			this.defeatCard.Prepare(levelNode);
			while (this.defeatCard.playing)
			{
				yield return null;
			}
			this.defeatCard.gameObject.SetActive(false);
			if (this.gameplayManager.endOfLevel.checkpointEvent == LevelState.CheckpointState.Destroyed)
			{
				float endTime = Time.unscaledTime + 0.3f;
				while (Time.unscaledTime < endTime)
				{
					yield return null;
				}
				Animator animator = this.checkpointGameObject.GetComponent<Animator>();
				this.checkpointReached.gameObject.SetActive(false);
				this.checkpointLost.gameObject.SetActive(true);
				animator.gameObject.SetActive(true);
				animator.Play("Lost");
				FabricWrapper.PostEvent(EndOfLevelUIRoot.checkpointLostAudio);
				yield return null;
				while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
				{
					yield return null;
				}
				animator.gameObject.SetActive(false);
			}
			if (this.gameplayManager.endOfLevel.lostLevelItem)
			{
				float endTime2 = Time.unscaledTime + 0.3f;
				while (Time.unscaledTime < endTime2)
				{
					yield return null;
				}
				MaskedSprite componentInChildren = this.itemGameObject.GetComponentInChildren<MaskedSprite>();
				componentInChildren.Set(this.gameplayManager.endOfLevel.lostLevelItem);
				bool metaReward = this.gameplayManager.island.levelNode.levelState.metaReward;
				componentInChildren.transform.localScale = ((!metaReward) ? new Vector3(1.1f, 1.1f, 1f) : Vector3.one);
				MaskedSprite.BorderSettings borderSettings = componentInChildren.borders[0];
				borderSettings.width = ((!metaReward) ? 0f : 0.3f);
				componentInChildren.borders[0] = borderSettings;
				foreach (Localize localize in this.itemLoc)
				{
					localize.Term = this.gameplayManager.endOfLevel.lostLevelItem.nameTerm;
				}
				Animator animator2 = this.itemGameObject.GetComponent<Animator>();
				animator2.gameObject.SetActive(true);
				animator2.Play("Lost");
				FabricWrapper.PostEvent(EndOfLevelUIRoot.itemLostAudio);
				yield return null;
				while (animator2.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
				{
					yield return null;
				}
				animator2.gameObject.SetActive(false);
			}
			yield return null;
			yield break;
		}

		// Token: 0x06002347 RID: 9031 RVA: 0x0006AE52 File Offset: 0x00069252
		public void FocusCamera()
		{
			this.islandViewfinder.Push(this.IslandCameraArea, null);
		}

		// Token: 0x06002348 RID: 9032 RVA: 0x0006AE66 File Offset: 0x00069266
		public void HandleRestartButton()
		{
			FabricWrapper.PostEvent("UI/InGame/Restart");
			this.gameplayManager.levelLeaver.ReplayLevel();
		}

		// Token: 0x06002349 RID: 9033 RVA: 0x0006AE83 File Offset: 0x00069283
		public void HandleContinueButton()
		{
			FabricWrapper.PostEvent(FabricID.exitLevel);
			this.gameplayManager.levelLeaver.CompleteLevel();
		}

		// Token: 0x0600234A RID: 9034 RVA: 0x0006AEA0 File Offset: 0x000692A0
		public void OnWipe(Island island)
		{
			base.StopAllCoroutines();
			this.shaderModifier.alpha = 0f;
			this.eolRoutine = null;
		}

		// Token: 0x0600234B RID: 9035 RVA: 0x0006AEBF File Offset: 0x000692BF
		void IslandGameplayManager.ISetupIsland.OnSetup(Island island)
		{
			this.eolRoutine = this.EOLRoutine(island.levelNode);
		}

		// Token: 0x0600234C RID: 9036 RVA: 0x0006AED4 File Offset: 0x000692D4
		public bool AllowRestart()
		{
			LevelNode levelNode = this.gameplayManager.island.levelNode;
			Campaign campaign = (!levelNode) ? null : levelNode.campaign;
			return campaign && campaign.campaignSave.prefs.allowReplays;
		}

		// Token: 0x040015D0 RID: 5584
		[SerializeField]
		private RectTransform IslandCameraArea;

		// Token: 0x040015D1 RID: 5585
		[SerializeField]
		private RectTransform ButtonContainer;

		// Token: 0x040015D2 RID: 5586
		[SerializeField]
		private UIClickable continueClickable;

		// Token: 0x040015D3 RID: 5587
		[SerializeField]
		private UIClickable replayClickable;

		// Token: 0x040015D4 RID: 5588
		private IslandGameplayManager gameplayManager;

		// Token: 0x040015D5 RID: 5589
		private LevelCamera levelCamera;

		// Token: 0x040015D6 RID: 5590
		private IslandViewfinder islandViewfinder;

		// Token: 0x040015D7 RID: 5591
		private IUIVisibility buttonVisibility;

		// Token: 0x040015D8 RID: 5592
		[SerializeField]
		private GameObject itemGameObject;

		// Token: 0x040015D9 RID: 5593
		[SerializeField]
		private GameObject metaItemGameObject;

		// Token: 0x040015DA RID: 5594
		[SerializeField]
		private GameObject recruitGameObject;

		// Token: 0x040015DB RID: 5595
		[SerializeField]
		private GameObject checkpointGameObject;

		// Token: 0x040015DC RID: 5596
		[SerializeField]
		private GameObject checkpointReached;

		// Token: 0x040015DD RID: 5597
		[SerializeField]
		private GameObject checkpointLost;

		// Token: 0x040015DE RID: 5598
		[SerializeField]
		private GameObject traitGameObject;

		// Token: 0x040015DF RID: 5599
		[SerializeField]
		private GameObject metaTraitGameObject;

		// Token: 0x040015E0 RID: 5600
		[SerializeField]
		private Localize[] itemLoc;

		// Token: 0x040015E1 RID: 5601
		[SerializeField]
		private Localize[] traitLoc;

		// Token: 0x040015E2 RID: 5602
		[SerializeField]
		private EndLevelCard victoryCard;

		// Token: 0x040015E3 RID: 5603
		[SerializeField]
		private EndLevelCard defeatCard;

		// Token: 0x040015E4 RID: 5604
		[SerializeField]
		private GameObject defeatHeader;

		// Token: 0x040015E5 RID: 5605
		[SerializeField]
		private GameObject fledheader;

		// Token: 0x040015E6 RID: 5606
		[SerializeField]
		private EndLevelHeroLineup victoryHeroLineup;

		// Token: 0x040015E7 RID: 5607
		[SerializeField]
		private Localize recruitName;

		// Token: 0x040015E8 RID: 5608
		[SerializeField]
		private Localize traitName;

		// Token: 0x040015E9 RID: 5609
		[SerializeField]
		private RawImage heroAtlasFixImage;

		// Token: 0x040015EA RID: 5610
		private EndOfLevel.Reason eolReason;

		// Token: 0x040015EB RID: 5611
		private ShaderConstantsOverride shaderModifier;

		// Token: 0x040015EC RID: 5612
		[SerializeField]
		private AgentStateRoot root;

		// Token: 0x040015ED RID: 5613
		private AnimatedState grayscale;

		// Token: 0x040015EE RID: 5614
		private static FabricEventReference recruitedAudio = "UI/InGame/GetCharacter";

		// Token: 0x040015EF RID: 5615
		private static FabricEventReference recruitedDespawnAudio = "UI/InGame/GetCharacterExit";

		// Token: 0x040015F0 RID: 5616
		private static FabricEventReference checkpointSavedAudio = "UI/InGame/CheckpointReached";

		// Token: 0x040015F1 RID: 5617
		private static FabricEventReference checkpointLostAudio = "UI/InGame/LooseCheckpoint";

		// Token: 0x040015F2 RID: 5618
		private static FabricEventReference itemFoundAudio = "UI/InGame/FoundItem";

		// Token: 0x040015F3 RID: 5619
		private static FabricEventReference metaItemFoundAudio = "UI/InGame/FoundItemNew";

		// Token: 0x040015F4 RID: 5620
		private static FabricEventReference itemDespawnAudio = "UI/InGame/GetCharacterExit";

		// Token: 0x040015F5 RID: 5621
		private static FabricEventReference itemLostAudio = "UI/InGame/LooseItem";

		// Token: 0x040015F6 RID: 5622
		private IEnumerator eolRoutine;
	}
}
