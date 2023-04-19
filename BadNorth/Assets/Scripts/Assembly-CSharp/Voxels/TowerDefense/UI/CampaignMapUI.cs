using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CS.Platform;
using ReflexCLI.Attributes;
using ReflexCLI.User;
using Rewired;
using RTM.Input;
using RTM.UISystem;
using TrialVersion;
using UnityEngine;
using Voxels.TowerDefense.CampaignGeneration;
using Voxels.TowerDefense.ProfileInternals;
using Voxels.TowerDefense.ScriptAnimations;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x020008AE RID: 2222
	public class CampaignMapUI : UIMenu, IGameSetup, CampaignManager.INewCampaign, CampaignManager.IExitCampaign
	{
		// Token: 0x140000C4 RID: 196
		// (add) Token: 0x06003A22 RID: 14882 RVA: 0x000FF2F0 File Offset: 0x000FD6F0
		// (remove) Token: 0x06003A23 RID: 14883 RVA: 0x000FF328 File Offset: 0x000FD728
		
		public event Action<GameOverReason> onGameOver = delegate(GameOverReason A_0)
		{
		};

		// Token: 0x1700084F RID: 2127
		// (set) Token: 0x06003A24 RID: 14884 RVA: 0x000FF360 File Offset: 0x000FD760
		public override bool blockingInput
		{
			protected set
			{
				base.blockingInput = value;
				if (this.upgradesProxy.menu)
				{
					this.upgradesProxy.menu.CampaignMapAnimating(value);
				}
				this.optionsButtonVisibility.SetVisible(!this.blockingInput, false);
			}
		}

		// Token: 0x06003A25 RID: 14885 RVA: 0x000FF3B0 File Offset: 0x000FD7B0
		void CampaignManager.INewCampaign.OnNewCampaign(CampaignManager campaignManager, Campaign campaign)
		{
			this.islandGenerator = campaignManager.islandGenerator;
			this.frontier = campaign.GetComponentInChildren<Frontier>();
			this.campaign.Target = campaign;
			this.blockingInput = false;
			this.nextTurnClickable.disabled = !this.ShouldEnableNextTurnButton();
			Action b = delegate()
			{
				this.PlayIfVisible(this.cloudUncoverId);
			};
			foreach (LevelNode levelNode in campaign.levels)
			{
				AnimatedState cloudState = levelNode.levelVisuals.cloudState;
				cloudState.OnActivate = (Action)Delegate.Combine(cloudState.OnActivate, b);
			}
			if (this.levelNodeStack == null)
			{
				this.levelNodeStack = new List<LevelNode>(campaign.levels.Count);
			}
			TrialTurnsRemainDisplay.UpdateDisplay(campaign.trialTurnsRemain, false);
			this.gameOverHeader.SetActive(false);
			this.upgradesProxy.menu.onClosed += this.OnUpgradesClosed;
		}

		// Token: 0x06003A26 RID: 14886 RVA: 0x000FF4CC File Offset: 0x000FD8CC
		void CampaignManager.IExitCampaign.OnCampaignExit(CampaignManager manager, Campaign campaign)
		{
			this.upgradesProxy.menu.onClosed -= this.OnUpgradesClosed;
			this.levelNodeStack = null;
		}

		// Token: 0x06003A27 RID: 14887 RVA: 0x000FF4F4 File Offset: 0x000FD8F4
		private void OnUpgradesClosed()
		{
			this.campaign.Target.heroesAvaliable.SetActive(this.campaign.Target.campaignSave.HeroesAvailableThisTurn());
			this.nextTurnDisplay.UpdateHeroesDisplay();
			this.nextTurnClickable.disabled = !this.ShouldEnableNextTurnButton();
		}

		// Token: 0x06003A28 RID: 14888 RVA: 0x000FF54B File Offset: 0x000FD94B
		public void Options()
		{
			FabricWrapper.PostEvent(FabricID.uiButtonClick);
			this.optionsMenu.OpenMenu();
			this.nextTurnDisplay.Close();
			TrialTurnsRemainDisplay.SetVisible(false, false);
		}

		// Token: 0x06003A29 RID: 14889 RVA: 0x000FF575 File Offset: 0x000FD975
		public void NoCommandsAvailablePrompt()
		{
			FabricWrapper.PostEvent(FabricID.uiError);
			this.nextTurnDisplay.PromptAllHeroesFatigued();
		}

		// Token: 0x06003A2A RID: 14890 RVA: 0x000FF590 File Offset: 0x000FD990
		public void HandleNextTurnButton()
		{
			if (this.campaign.Target.campaignSave.HeroesAvailableThisTurn())
			{
				if (this.nextTurnDisplay.promptingHeroesAvailable)
				{
					this.NextTurn();
					FabricWrapper.PostEvent(FabricID.uiButtonClick);
				}
				else
				{
					this.nextTurnDisplay.PromptHeroesAvailable();
					FabricWrapper.PostEvent(FabricID.uiError);
				}
			}
			else
			{
				this.NextTurn();
				FabricWrapper.PostEvent(FabricID.uiButtonClick);
			}
		}

		// Token: 0x06003A2B RID: 14891 RVA: 0x000FF60C File Offset: 0x000FDA0C
		protected override IUINavigable GetDefaultNavigable()
		{
			if (this.campaign.Target.campaignSave.lostGame)
			{
				return null;
			}
			if (!this.campaign.Target)
			{
				return null;
			}
			IUINavigable currentNavigable = base.GetCurrentNavigable();
			if (currentNavigable != null && currentNavigable.isNavigable)
			{
				return currentNavigable;
			}
			IUINavigable lastNavigable = base.GetLastNavigable();
			if (lastNavigable != null && lastNavigable.isNavigable)
			{
				return lastNavigable;
			}
			int lastPlayedLevelIdx = this.campaign.Target.campaignSave.lastPlayedLevelIdx;
			LevelNode levelNode = this.campaign.Target.levels[lastPlayedLevelIdx];
			LevelNodeUIProxy proxy = this.nodes.GetProxy(levelNode);
			return (!proxy) ? null : proxy.GetComponent<IUINavigable>();
		}

		// Token: 0x06003A2C RID: 14892 RVA: 0x000FF6D4 File Offset: 0x000FDAD4
		void IGameSetup.OnGameAwake()
		{
			base.gameObject.SetActive(false);
			Singleton<Stack>.instance.stateCampaign.OnChange += base.gameObject.SetActive;
			ReInput.players.GetPlayer(0).AddInputEventDelegate(new Action<InputActionEventData>(this.OnPauseButtonPressed), UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, this.pauseButtonActionID);
			ReInput.players.GetPlayer(0).AddInputEventDelegate(new Action<InputActionEventData>(this.OnUnpauseButtonPressed), UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, this.unpauseButtonActionID);
			this.upgradesClickable.onClick += this.OpenUpgradesMenu;
			this.nextTurnClickable.onClick += this.HandleNextTurnButton;
			this.leftTabClickable.onClick += delegate()
			{
				this.upgradesProxy.HandleMapTab(-1);
				this.nextTurnDisplay.Close();
			};
			this.rightTabClickable.onClick += delegate()
			{
				this.upgradesProxy.HandleMapTab(1);
				this.nextTurnDisplay.Close();
			};
			this.buttons.wonGameUpgrade.onClick += this.OpenUpgradesMenu;
			this.buttons.wonGameMainManu.onClick += delegate()
			{
				MetaMenuHelpers.ExitToMainMenu();
				FabricWrapper.PostEvent(FabricID.uiButtonClick);
			};
			this.optionsButtonVisibility = this.optionsButton.GetComponent<IUIVisibility>();
			base.onFocusedNavigableChanged += delegate(IUINavigable n)
			{
				if (n != null)
				{
					this.nextTurnDisplay.Close();
				}
			};
			this.cameraController.onDrag += this.nextTurnDisplay.Close;
			this.cameraController.onBackgroundClick += this.nextTurnDisplay.Close;
			this.gameOverHeader.Init(this);
			this.optionsMenu.mapUI = this;
		}

		// Token: 0x06003A2D RID: 14893 RVA: 0x000FF877 File Offset: 0x000FDC77
		private void OpenUpgradesMenu()
		{
			if (this.CanOpenUpgradesMenu())
			{
				this.upgradesProxy.OpenMenu();
			}
		}

		// Token: 0x06003A2E RID: 14894 RVA: 0x000FF88F File Offset: 0x000FDC8F
		private void OnPauseButtonPressed(InputActionEventData data)
		{
			if (base.isFocussed && !this.blockingInput && !this.gainedFocusTime.isThisFrame && !ConsoleStatus.IsConsoleOpen())
			{
				this.Options();
			}
		}

		// Token: 0x06003A2F RID: 14895 RVA: 0x000FF8C8 File Offset: 0x000FDCC8
		private void OnUnpauseButtonPressed(InputActionEventData data)
		{
			if (this.optionsMenu.isOpen && !ConsoleStatus.IsConsoleOpen())
			{
				this.optionsMenu.CloseMenu();
				if (!this.optionsMenu.isOpen)
				{
					FabricWrapper.PostEvent(FabricID.uiBack);
				}
			}
		}

		// Token: 0x06003A30 RID: 14896 RVA: 0x000FF918 File Offset: 0x000FDD18
		private void OnEnable()
		{
			if (this.campaign.Target.lastPlayedLevel)
			{
				if (this.campaign.Target.campaignSave.wonGame)
				{
					this.WonGame();
				}
				else
				{
					this.LevelComplete();
				}
			}
		}

		// Token: 0x06003A31 RID: 14897 RVA: 0x000FF96C File Offset: 0x000FDD6C
		public override void OpenMenu()
		{
			this.optionsButtonVisibility.SetVisible(this.CanOpenUpgradesMenu(), true);
			this.buttons.SetVisible(this.CanShowNextTurnButton(), true);
			TrialTurnsRemainDisplay.SetVisible(true, true);
			if (!this.campaign.Target.lastPlayedLevel)
			{
				this.nextTurnDisplay.UpdateHeroesDisplay();
			}
			base.OpenMenu();
			string[] parameter = new string[]
			{
				Profile.campaign.PercentComplete().ToString()
			};
			BasePlatformManager.Instance.SetRichPresenceDetails("CAMPAIGN_MAP_ALT", parameter);
			BasePlatformManager.Instance.SetRichPresenceLargeImage("CAMPAIGN_MAP");
			BasePlatformManager.Instance.SendRichPresence();
			if (this.campaign.Target.trialOver)
			{
				Utils.DoTrialVersionPopup("TRIAL/OVER/TITLE_2");
			}
		}

		// Token: 0x06003A32 RID: 14898 RVA: 0x000FFA3C File Offset: 0x000FDE3C
		protected override void OnGainedFocus()
		{
			base.OnGainedFocus();
			this.buttons.SetVisible(this.CanShowNextTurnButton(), false);
			this.upgradesDisplay.SetAvailable(this.upgradesProxy.menu.upgradesAvailable, true);
			this.nextTurnClickable.disabled = !this.ShouldEnableNextTurnButton();
			this.optionsButtonVisibility.SetVisible(!this.blockingInput, false);
			TrialTurnsRemainDisplay.SetVisible(true, false);
		}

		// Token: 0x06003A33 RID: 14899 RVA: 0x000FFAAD File Offset: 0x000FDEAD
		protected override void OnLostFocus()
		{
			base.OnLostFocus();
			this.nextTurnClickable.disabled = true;
			this.optionsButtonVisibility.SetVisible(false, false);
			this.nextTurnDisplay.Close();
		}

		// Token: 0x06003A34 RID: 14900 RVA: 0x000FFAD9 File Offset: 0x000FDED9
		public void AnyMapClick()
		{
			if (!this.nextTurnDisplay.promptingAllFatigued)
			{
				this.nextTurnDisplay.Close();
			}
		}

		// Token: 0x06003A35 RID: 14901 RVA: 0x000FFAF6 File Offset: 0x000FDEF6
		public void SetNextTurnVisible(bool visible)
		{
			this.buttons.SetVisible(visible && this.CanShowNextTurnButton(), false);
		}

		// Token: 0x06003A36 RID: 14902 RVA: 0x000FFB14 File Offset: 0x000FDF14
		private bool CanOpenUpgradesMenu()
		{
			foreach (HeroDefinition heroDefinition in this.campaign.Target.campaignSave.heroes)
			{
				if (heroDefinition.alive && heroDefinition.recruited)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003A37 RID: 14903 RVA: 0x000FFB98 File Offset: 0x000FDF98
		private bool CanShowNextTurnButton()
		{
			CampaignSave campaignSave = this.campaign.Target.campaignSave;
			return !campaignSave.gameOver || campaignSave.wonGame;
		}

		// Token: 0x06003A38 RID: 14904 RVA: 0x000FFBCC File Offset: 0x000FDFCC
		private bool ShouldEnableNextTurnButton()
		{
			if (this.campaign.Target.lastPlayedLevel)
			{
				return false;
			}
			if (!base.isFocussed)
			{
				return false;
			}
			foreach (HeroDefinition heroDefinition in this.campaign.Target.campaignSave.heroes)
			{
				if (heroDefinition.available && !heroDefinition.availableThisTurn)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003A39 RID: 14905 RVA: 0x000FFC7C File Offset: 0x000FE07C
		private void PlayIfVisible(FabricEventReference id)
		{
			if (base.isFocussed)
			{
				FabricWrapper.PostEvent(id);
			}
		}

		// Token: 0x06003A3A RID: 14906 RVA: 0x000FFC90 File Offset: 0x000FE090
		[ConsoleCommand("NextTurn")]
		[DebugSetting("Next Turn", "下一轮", DebugSettingLocation.Campaign)]
		private static void DbgNextTurn()
		{
			UnityEngine.Object.FindObjectOfType<CampaignMapUI>().NextTurn();
		}

		// Token: 0x06003A3B RID: 14907 RVA: 0x000FFC9C File Offset: 0x000FE09C
		[DebugSetting("Always Allow Next Turn", DebugSettingLocation.Campaign)]
		private static void AlwaysAllowNextTurn()
		{
			CampaignMapUI.alwaysAllowNextTurn = !CampaignMapUI.alwaysAllowNextTurn;
		}

		// Token: 0x06003A3C RID: 14908 RVA: 0x000FFCAB File Offset: 0x000FE0AB
		public bool ReloadCheckpoint(bool showConfirmPrompt, float minSeekTime = 0.35f)
		{
			if (this.campaign.Target.campaignSave.hasAnyCheckpoints)
			{
				base.StartCoroutine(this.ConfirmReloadCheckpointRoutine(showConfirmPrompt, minSeekTime));
				return true;
			}
			return false;
		}

		// Token: 0x06003A3D RID: 14909 RVA: 0x000FFCDC File Offset: 0x000FE0DC
		private IEnumerator ConfirmReloadCheckpointRoutine(bool showConfirmPrompt, float minSeekTime)
		{
			LevelNode currentCheckpoint = null;
			foreach (LevelNode levelNode in this.campaign.Target.levels)
			{
				if (levelNode.checkpoint == LevelState.CheckpointState.Current)
				{
					currentCheckpoint = levelNode;
					break;
				}
			}
			if (currentCheckpoint != null)
			{
				this.blockingInput = true;
				float pos = currentCheckpoint.transform.position.x;
				this.cameraController.SeekTo(pos, true);
				float startTime = Time.unscaledTime;
				float end = startTime + minSeekTime;
				while (end > Time.unscaledTime || (this.cameraController.seeking && Mathf.Abs(this.cameraController.currentPos - pos) > 12f))
				{
					yield return null;
				}
				float seekTime = Time.unscaledTime - startTime;
				yield return null;
				currentCheckpoint.levelVisuals.hoverState.SetActive(true);
				float end2 = Time.unscaledTime + Mathf.Min(0.75f, seekTime);
				while (end2 > Time.unscaledTime || this.cameraController.seeking)
				{
					yield return null;
				}
			}
			if (showConfirmPrompt)
			{
				ModalOverlay instance = ModalOverlayPool.GetInstance();
				instance.Initialize("CHECKPOINT/LOAD/TITLE", "CHECKPOINT/LOAD/MESSAGE", true);
				instance.AddButton("UI/COMMON/CONTINUE", delegate()
				{
					MetaMenuHelpers.LoadCheckpoint(Profile.activeCampaignMeta, Profile.campaign);
					return true;
				}, null, null);
				if (currentCheckpoint)
				{
					instance.SetCloseAction(new Action(currentCheckpoint.levelVisuals.hoverState.SetActiveFalse));
				}
			}
			else
			{
				MetaMenuHelpers.LoadCheckpoint(Profile.activeCampaignMeta, Profile.campaign);
			}
			this.blockingInput = false;
			yield return null;
			yield break;
		}

		// Token: 0x06003A3E RID: 14910 RVA: 0x000FFD08 File Offset: 0x000FE108
		private void GameOver(GameOverReason reason)
		{
			this.blockingInput = true;
			this.nextTurnClickable.disabled = true;
			this.buttons.SetVisible(false, true);
			LevelNode lastPlayedLevel = this.campaign.Target.lastPlayedLevel;
			for (int i = 0; i < this.campaign.Target.levels.Count; i++)
			{
				this.campaign.Target.levels[i].levelVisuals.UpdateEnemies();
			}
			this.gameOverHeader.SetActive(true);
			this.gameOverHeader.SetReason(reason);
			base.StartCoroutine(CoroutineUtils.ExceptionHandler(this.GameOverRoutine(reason), new Action(this.GameOverExceptionRecovery)));
			this.onGameOver(reason);
		}

		// Token: 0x06003A3F RID: 14911 RVA: 0x000FFDD0 File Offset: 0x000FE1D0
		private IEnumerator GameOverRoutine(GameOverReason reason)
		{
			Campaign campaign = this.campaign.Target;
			this.gameOverHeader.SetDimVisibility(true, false);
			this.upgradesProxy.menu.SetDeadHeroVisibile(false);
			this.upgradesProxy.menu.UpdateCoinBank();
			float end = Time.unscaledTime + 1.2f;
			while (end > Time.unscaledTime)
			{
				yield return null;
			}
			this.gameOverHeader.SetVisible(true, false);
			float end2 = Time.unscaledTime + 2.75f;
			while (end2 > Time.unscaledTime)
			{
				yield return null;
			}
			this.gameOverHeader.SetReasonVisibility(true, false);
			float end3 = Time.unscaledTime + 2f;
			while (end3 > Time.unscaledTime)
			{
				yield return null;
			}
			yield return this.stats.ShowStats(campaign.campaignSave.stats, reason, 0.4f);
			float end4 = Time.unscaledTime + 1f;
			while (end4 > Time.unscaledTime)
			{
				yield return null;
			}
			this.gameOverHeader.SetButtonsVisible(true, campaign.campaignSave.hasCheckpoint, false);
			this.blockingInput = false;
			yield break;
		}

		// Token: 0x06003A40 RID: 14912 RVA: 0x000FFDF2 File Offset: 0x000FE1F2
		private void LostGameExceptionRecovery()
		{
		}

		// Token: 0x06003A41 RID: 14913 RVA: 0x000FFDF4 File Offset: 0x000FE1F4
		private void LevelComplete()
		{
			this.blockingInput = true;
			this.nextTurnClickable.disabled = true;
			LevelNode lastPlayedLevel = this.campaign.Target.lastPlayedLevel;
			for (int i = 0; i < this.campaign.Target.levels.Count; i++)
			{
				this.campaign.Target.levels[i].levelVisuals.UpdateEnemies();
			}
			base.StartCoroutine(CoroutineUtils.ExceptionHandler(this.LevelCompleteRoutine(lastPlayedLevel), new Action(this.LevelCompleteExceptionRecovery)));
		}

		// Token: 0x06003A42 RID: 14914 RVA: 0x000FFE8C File Offset: 0x000FE28C
		private IEnumerator LevelCompleteRoutine(LevelNode lastLevel)
		{
			this.cameraController.UpdateLimits();
			float seekTarget = Mathf.Max(this.cameraController.scrollTargetMin, lastLevel.pos.x);
			this.cameraController.SnapTo(seekTarget);
			this.linkList.Clear();
			this.linkList.AddRange(lastLevel.links);
			float end = Time.unscaledTime + 0.5f;
			while (end > Time.unscaledTime)
			{
				yield return null;
			}
			if (this.upgradesProxy.menu.SetDeadHeroVisibile(false))
			{
				FabricWrapper.PostEvent(this.heroDeadAudioId);
				float end2 = Time.unscaledTime + 0.5f;
				while (end2 > Time.unscaledTime)
				{
					yield return null;
				}
			}
			if (this.upgradesProxy.menu.RevealRecruitedHeroes())
			{
				float end3 = Time.unscaledTime + 0.5f;
				while (end3 > Time.unscaledTime)
				{
					yield return null;
				}
			}
			this.upgradesProxy.menu.RefreshPortraits();
			float end4 = Time.unscaledTime + 0.5f;
			while (end4 > Time.unscaledTime)
			{
				yield return null;
			}
			for (int j = this.linkList.Count - 1; j >= 0; j--)
			{
				LinkSpawner.Link link2 = this.linkList[j];
				LevelNode otherLevel2 = link2.GetOtherLevel(lastLevel);
				if (otherLevel2.IsPlayed() || otherLevel2.IsBehindFrontier())
				{
					link2.Update(3f);
					this.linkList.RemoveAt(j);
				}
			}
			this.campaign.Target.heroesAvaliable.SetActive(this.campaign.Target.campaignSave.HeroesAvailableThisTurn());
			lastLevel.Played();
			float endTime = Time.unscaledDeltaTime + 0.1f;
			foreach (LevelNode level in this.campaign.Target.levels)
			{
				level.levelVisuals.UpdateCheckpoint();
				yield return null;
			}
			float end5 = endTime;
			while (end5 > Time.unscaledTime)
			{
				yield return null;
			}
			for (int k = this.linkList.Count - 1; k >= 0; k--)
			{
				LinkSpawner.Link link3 = this.linkList[k];
				LevelNode otherLevel3 = link3.GetOtherLevel(lastLevel);
				if (otherLevel3.unlocked.active)
				{
					link3.Update(2f);
					this.linkList.RemoveAt(k);
				}
			}
			foreach (LevelNode level2 in this.campaign.Target.levels)
			{
				if (level2.MaybeVisible())
				{
					yield return null;
				}
			}
			float end6 = Time.unscaledTime + 0.5f;
			while (end6 > Time.unscaledTime)
			{
				yield return null;
			}
			for (int l = this.linkList.Count - 1; l >= 0; l--)
			{
				if (!this.linkList[l].GetOtherLevel(lastLevel).IsUnlocked())
				{
					this.linkList.RemoveAt(l);
				}
			}
			LevelNode bestLevel = lastLevel;
			if (this.linkList.Count > 0)
			{
				this.linkList.Sort();
				bestLevel = this.linkList.Last<LinkSpawner.Link>().GetOtherLevel(lastLevel);
				float seekDelta = Mathf.Abs(bestLevel.pos.x - lastLevel.pos.x) / (float)this.linkList.Count;
				for (int i = 0; i < this.linkList.Count; i++)
				{
					LinkSpawner.Link link = this.linkList[i];
					LevelNode otherLevel = link.GetOtherLevel(lastLevel);
					float end7 = Time.unscaledTime + 0.4f;
					while (end7 > Time.unscaledTime)
					{
						yield return null;
					}
					link.Update(4f);
					float sign = Mathf.Sign(otherLevel.pos.x - lastLevel.pos.x);
					seekTarget += seekDelta * sign;
					this.cameraController.SeekTo(seekTarget, true);
					float end8 = Time.unscaledTime + 0.05f;
					while (end8 > Time.unscaledTime)
					{
						yield return null;
					}
					otherLevel.unlocked.SetActive(true);
					otherLevel.levelVisuals.animator.SetBool(this.unlockId, true);
				}
				float end9 = Time.unscaledTime + 0.2f;
				while (end9 > Time.unscaledTime)
				{
					yield return null;
				}
				foreach (LevelNode level3 in lastLevel.connectedLevels)
				{
					level3.levelVisuals.animator.SetBool(this.unlockId, false);
					yield return null;
				}
			}
			float end10 = Time.unscaledTime + 0.3f;
			while (end10 > Time.unscaledTime)
			{
				yield return null;
			}
			this.campaign.Target.lastPlayedLevel = null;
			this.campaign.Target.pendingUnlocks.Clear();
			yield return null;
			float end11 = Time.unscaledTime + 0.3f;
			while (end11 > Time.unscaledTime)
			{
				yield return null;
			}
			this.upgradesProxy.menu.UpdateCoinBank();
			this.upgradesDisplay.SetAvailable(this.upgradesProxy.menu.upgradesAvailable, false);
			if (this.campaign.Target.campaignSave.gameOver)
			{
				this.GameOver(this.campaign.Target.campaignSave.gameOverReason);
			}
			else
			{
				this.islandGenerator.QueueUpGenerations(this.campaign);
				this.nextTurnClickable.disabled = !this.ShouldEnableNextTurnButton();
				this.nextTurnDisplay.UpdateHeroesDisplay();
				this.blockingInput = false;
				if (InputHelpers.GetControllerType() != ControllerType.Mouse)
				{
					LevelNodeUIProxy proxy = this.nodes.GetProxy(bestLevel);
					if (proxy)
					{
						base.FocusOn(proxy.navigable);
					}
				}
			}
			yield return null;
			this.linkList.Clear();
			yield return null;
			yield break;
		}

		// Token: 0x06003A43 RID: 14915 RVA: 0x000FFEB0 File Offset: 0x000FE2B0
		private void LevelCompleteExceptionRecovery()
		{
			Campaign target = this.campaign.Target;
			if (target.campaignSave.gameOver)
			{
				this.GameOver(target.campaignSave.gameOverReason);
			}
			else
			{
				this.nextTurnDisplay.UpdateHeroesDisplay();
				this.blockingInput = false;
				foreach (LevelNode levelNode in target.pendingUnlocks)
				{
					if (levelNode)
					{
						levelNode.unlocked.SetActive(true);
					}
				}
				this.islandGenerator.QueueUpGenerations(target);
				target.lastPlayedLevel = null;
				target.pendingUnlocks.Clear();
				this.nextTurnClickable.disabled = !this.ShouldEnableNextTurnButton();
			}
		}

		// Token: 0x06003A44 RID: 14916 RVA: 0x000FFF94 File Offset: 0x000FE394
		private void NextTurn()
		{
			this.nextTurnDisplay.Close();
			CampaignSave campaignSave = this.campaign.Target.campaignSave;
			campaignSave.NextTurn();
			foreach (LevelState levelState in campaignSave.levelStates)
			{
				if ((int)levelState.frontierDepth == campaignSave.vikingFrontierPosition && levelState.checkpointState == LevelState.CheckpointState.Available)
				{
					levelState.checkpointState = LevelState.CheckpointState.Destroyed;
					campaignSave.stats.checkpointsLost++;
					Profile.userSave.stats.checkpointsLost++;
				}
			}
			this.blockingInput = true;
			base.StartCoroutine(this.NextTurnRotate());
			base.StartCoroutine(this.NextTurnRoutine(0.2f));
		}

		// Token: 0x06003A45 RID: 14917 RVA: 0x00100080 File Offset: 0x000FE480
		private IEnumerator NextTurnRotate()
		{
			for (float t = 0f; t < 1f; t += Time.unscaledDeltaTime)
			{
				float angle = t;
				angle *= angle;
				angle = Mathf.SmoothStep(0f, 1f, angle);
				angle = Mathf.SmoothStep(0f, 1f, angle);
				angle = 1f - angle;
				angle *= 360f;
				Quaternion q = Quaternion.Euler(0f, 0f, angle);
				foreach (Transform transform in CampaignMapUI.nextTurnRotators)
				{
					transform.transform.localRotation = q;
				}
				yield return null;
			}
			foreach (Transform transform2 in CampaignMapUI.nextTurnRotators)
			{
				transform2.transform.localRotation = Quaternion.identity;
			}
			yield break;
		}

		// Token: 0x06003A46 RID: 14918 RVA: 0x00100094 File Offset: 0x000FE494
		private IEnumerator NextTurnRoutine(float initialDelay)
		{
			this.nextTurnClickable.disabled = true;
			LineFrontier lineFrontier = this.campaign.Target.GetComponentInChildren<LineFrontier>(true);
			CampaignBackground campaignBackground = this.campaign.Target.GetComponentInChildren<CampaignBackground>(true);
			float endTime = Time.unscaledTime + initialDelay;
			if (!this.campaign.Target.levels.Any((LevelNode x) => x.IsPlayable()))
			{
				this.campaign.Target.campaignSave.gameOverReason = GameOverReason.VikingFrontier;
			}
			else
			{
				this.levelNodeStack.Clear();
				this.levelNodeStack.Add(this.campaign.Target.endLevel);
				bool trapped = true;
				int i = 0;
				while (i < this.levelNodeStack.Count && trapped)
				{
					LevelNode level0 = this.levelNodeStack[i];
					if (level0.IsAvailable())
					{
						trapped = false;
						break;
					}
					if (i % 10 == 0)
					{
						yield return null;
					}
					foreach (LevelNode levelNode in level0.connectedLevels)
					{
						if (this.campaign.Target.campaignSave.vikingFrontierPosition < levelNode.frontierDepth)
						{
							if (levelNode.IsPlayable())
							{
								UnityEngine.Debug.DrawLine(level0.pos, levelNode.pos, UnityEngine.Color.green, 2f);
								trapped = false;
								break;
							}
							if (!this.levelNodeStack.Contains(levelNode))
							{
								UnityEngine.Debug.DrawLine(level0.pos, levelNode.pos, UnityEngine.Color.yellow, 2f);
								this.levelNodeStack.Add(levelNode);
							}
						}
					}
					i++;
				}
				this.levelNodeStack.Clear();
				if (trapped)
				{
					this.campaign.Target.campaignSave.gameOverReason = GameOverReason.NoLevels;
				}
			}
			if (this.campaign.Target.campaignSave.gameOver)
			{
				foreach (HeroDefinition heroDefinition in this.campaign.Target.campaignSave.heroes)
				{
					if (heroDefinition.recruited)
					{
						heroDefinition.alive = false;
					}
				}
			}
			Profile.SaveCampaign(false);
			while (endTime > Time.unscaledTime)
			{
				yield return null;
			}
			float f0 = lineFrontier.mainLineAnim.current;
			float f = (float)this.campaign.Target.campaignSave.vikingFrontierPosition;
			FabricWrapper.PostEvent(this.frontierMoveAudio);
			for (float t = 0f; t < 1f; t += Time.unscaledDeltaTime)
			{
				lineFrontier.mainLineAnim.SetCurrent(Mathf.Lerp(f0, f, lineFrontier.frontierMoveCurve.Evaluate(t)));
				lineFrontier.darkenAnim.SetCurrent(lineFrontier.frontierDarkenCurve.Evaluate(t));
				yield return null;
			}
			lineFrontier.mainLineAnim.SetCurrent(f);
			lineFrontier.darkenAnim.SetCurrent(0f);
			FabricWrapper.PostEvent(this.frontierPredictMoveAudio);
			foreach (LevelNode level in this.campaign.Target.levels)
			{
				if (level.IsBehindFrontier() && level.behindFrontier.SetActive(true))
				{
					foreach (LinkSpawner.Link link in level.links)
					{
						link.Update(1f);
					}
					float end = Time.unscaledTime + 0.1f;
					while (end > Time.unscaledTime)
					{
						yield return null;
					}
				}
			}
			if (Profile.campaign.gameOver)
			{
				endTime = Time.unscaledTime + 0.5f;
				while (endTime > Time.unscaledTime)
				{
					yield return null;
				}
				this.GameOver(Profile.campaign.gameOverReason);
			}
			else
			{
				this.cameraController.UpdateLimits();
				float t2 = 0f;
				while (t2 < 1f)
				{
					yield return null;
					lineFrontier.nextLineAnim.SetCurrent(f + Mathf.Clamp01(Mathf.Sqrt(t2 += Time.unscaledDeltaTime * 2f)));
				}
				this.upgradesProxy.menu.NextTurnAnimation();
				this.nextTurnDisplay.UpdateHeroesDisplay();
				yield return null;
				this.islandGenerator.DestroyOldIslands(this.campaign);
				TrialTurnsRemainDisplay.UpdateDisplay(this.campaign.Target.trialTurnsRemain, true);
				if (this.campaign.Target.trialOver)
				{
					float end2 = Time.unscaledTime + 1.5f;
					while (end2 > Time.unscaledTime)
					{
						yield return null;
					}
					Utils.DoTrialVersionPopup("TRIAL/OVER/TITLE");
				}
				this.blockingInput = false;
				this.campaign.Target.heroesAvaliable.SetActive(this.campaign.Target.campaignSave.HeroesAvailableThisTurn());
			}
			this.nextTurnClickable.disabled = !this.ShouldEnableNextTurnButton();
			yield return null;
			yield break;
		}

		// Token: 0x06003A47 RID: 14919 RVA: 0x001000B8 File Offset: 0x000FE4B8
		private void WonGame()
		{
			this.blockingInput = true;
			this.nextTurnClickable.disabled = true;
			this.buttons.SetVisible(true, true);
			this.buttons.UpdateButtonGroup(true);
			this.buttons.wonGameMainManu.disabled = true;
			this.buttons.wonGameUpgrade.disabled = true;
			LevelNode lastPlayedLevel = this.campaign.Target.lastPlayedLevel;
			for (int i = 0; i < this.campaign.Target.levels.Count; i++)
			{
				this.campaign.Target.levels[i].levelVisuals.UpdateEnemies();
			}
			this.gameOverHeader.SetActive(true);
			this.gameOverHeader.SetReason(GameOverReason.Won);
			this.onGameOver(GameOverReason.Won);
			base.StartCoroutine(CoroutineUtils.ExceptionHandler(this.WonGameRoutine(lastPlayedLevel), new Action(this.GameOverExceptionRecovery)));
		}

		// Token: 0x06003A48 RID: 14920 RVA: 0x001001B0 File Offset: 0x000FE5B0
		private IEnumerator WonGameRoutine(LevelNode lastLevel)
		{
			Campaign campaign = this.campaign.Target;
			this.cameraController.UpdateLimits();
			this.cameraController.SnapTo(lastLevel);
			float end = Time.unscaledTime + 0.5f;
			while (end > Time.unscaledTime)
			{
				yield return null;
			}
			this.upgradesProxy.menu.RefreshPortraits();
			this.upgradesProxy.menu.UpdateCoinBank();
			float end2 = Time.unscaledTime + 1.5f;
			while (end2 > Time.unscaledTime)
			{
				yield return null;
			}
			this.gameOverHeader.SetVisible(true, false);
			float end3 = Time.unscaledTime + 0.2f;
			while (end3 > Time.unscaledTime)
			{
				yield return null;
			}
			for (int j = lastLevel.links.Length - 1; j >= 0; j--)
			{
				LinkSpawner.Link link = lastLevel.links[j];
				LevelNode otherLevel = link.GetOtherLevel(lastLevel);
				if (otherLevel.IsPlayed() || otherLevel.IsBehindFrontier())
				{
					link.Update(3f);
				}
			}
			float end4 = Time.unscaledTime + 0.1f;
			while (end4 > Time.unscaledTime)
			{
				yield return null;
			}
			lastLevel.Played();
			float statsTime = Time.unscaledTime + 0.75f;
			float end5 = Time.unscaledTime + 0.2f;
			while (end5 > Time.unscaledTime)
			{
				yield return null;
			}
			List<LevelNode> levels = campaign.levels;
			for (int i = levels.Count - 1; i >= 0; i--)
			{
				if (levels[i].encouraged.active)
				{
					levels[i].encouraged.SetActive(false);
					float end6 = Time.unscaledTime + 0.1f;
					while (end6 > Time.unscaledTime)
					{
						yield return null;
					}
				}
			}
			float f = (float)campaign.campaignSave.vikingFrontierPosition;
			LineFrontier lineFrontier = campaign.GetComponentInChildren<LineFrontier>();
			float t = 0f;
			while (t < 1f)
			{
				yield return null;
				lineFrontier.nextLineAnim.SetCurrent(f + 1f - Mathf.Clamp01(Mathf.Sqrt(t += Time.unscaledDeltaTime * 4f)));
			}
			while (Time.unscaledTime < statsTime)
			{
				yield return null;
			}
			yield return this.stats.ShowStats(campaign.campaignSave.stats, GameOverReason.Won, 0.35f);
			foreach (HeroDefinition heroDefinition in campaign.campaignSave.heroes)
			{
				heroDefinition.timesUsedThisTurn = 0;
			}
			this.upgradesProxy.menu.SetDeadHeroVisibile(true);
			this.upgradesProxy.menu.RefreshPortraits();
			this.buttons.SetVisible(true, false);
			campaign.lastPlayedLevel = null;
			campaign.pendingUnlocks.Clear();
			this.buttons.wonGameMainManu.disabled = false;
			this.buttons.wonGameUpgrade.disabled = false;
			this.blockingInput = false;
			yield return null;
			yield break;
		}

		// Token: 0x06003A49 RID: 14921 RVA: 0x001001D2 File Offset: 0x000FE5D2
		private void GameOverExceptionRecovery()
		{
		}

		// Token: 0x04002849 RID: 10313
		[SerializeField]
		private CampaignMapOptions optionsMenu;

		// Token: 0x0400284A RID: 10314
		[SerializeField]
		public CampaignCameraController cameraController;

		// Token: 0x0400284B RID: 10315
		[SerializeField]
		private CampaignMapGameOverHeader gameOverHeader;

		// Token: 0x0400284C RID: 10316
		[SerializeField]
		public VictoryStatsUIWrapper stats;

		// Token: 0x0400284D RID: 10317
		[SerializeField]
		private CampaignMapLevelNodes nodes;

		// Token: 0x0400284E RID: 10318
		[SerializeField]
		private SuperUpgradesCampaignProxy upgradesProxy;

		// Token: 0x0400284F RID: 10319
		[SerializeField]
		private UIClickable upgradesClickable;

		// Token: 0x04002850 RID: 10320
		[SerializeField]
		private UIClickable nextTurnClickable;

		// Token: 0x04002851 RID: 10321
		[SerializeField]
		private UIClickable optionsButton;

		// Token: 0x04002852 RID: 10322
		[SerializeField]
		private UIClickable leftTabClickable;

		// Token: 0x04002853 RID: 10323
		[SerializeField]
		private UIClickable rightTabClickable;

		// Token: 0x04002854 RID: 10324
		public Transform coinTransform;

		// Token: 0x04002855 RID: 10325
		[SerializeField]
		private CampaignMapButtons buttons;

		// Token: 0x04002856 RID: 10326
		[SerializeField]
		private CampaignMapNextTurnDisplay nextTurnDisplay;

		// Token: 0x04002857 RID: 10327
		[SerializeField]
		private CampaignMapUpgradesDisplay upgradesDisplay;

		// Token: 0x04002858 RID: 10328
		private RewiredActionReference pauseButtonActionID = "Pause";

		// Token: 0x04002859 RID: 10329
		private RewiredActionReference unpauseButtonActionID = "Unpause";

		// Token: 0x0400285A RID: 10330
		private FabricEventReference cloudUncoverId = "UI/Map/MoveCloud";

		// Token: 0x0400285B RID: 10331
		private FabricEventReference levelPlayedId = "UI/Map/IslandBloody";

		// Token: 0x0400285C RID: 10332
		private FabricEventReference fatiuguedPopupAudio = "UI/InGame/CommandersFatigued";

		// Token: 0x0400285D RID: 10333
		private FabricEventReference frontierMoveAudio = "UI/Map/LineMove";

		// Token: 0x0400285E RID: 10334
		private FabricEventReference frontierPredictMoveAudio = "UI/Map/EnglishLineMove";

		// Token: 0x0400285F RID: 10335
		private FabricEventReference heroDeadAudioId = "Mus/LostTroop";

		// Token: 0x04002860 RID: 10336
		private IslandGenerator islandGenerator;

		// Token: 0x04002861 RID: 10337
		private RTM.Utilities.WeakReference<Campaign> campaign = new RTM.Utilities.WeakReference<Campaign>(null);

		// Token: 0x04002862 RID: 10338
		private RTM.Utilities.WeakReference<Frontier> frontier = new RTM.Utilities.WeakReference<Frontier>(null);

		// Token: 0x04002863 RID: 10339
		private List<LevelNode> levelNodeStack;

		// Token: 0x04002864 RID: 10340
		private IUIVisibility optionsButtonVisibility;

		// Token: 0x04002866 RID: 10342
		[ConsoleCommand("")]
		private static bool alwaysAllowNextTurn = false;

		// Token: 0x04002867 RID: 10343
		private List<LinkSpawner.Link> linkList = new List<LinkSpawner.Link>(8);

		// Token: 0x04002868 RID: 10344
		private AnimId unlockId = "Unlock";

		// Token: 0x04002869 RID: 10345
		public static List<Transform> nextTurnRotators = new List<Transform>(12);
	}
}
