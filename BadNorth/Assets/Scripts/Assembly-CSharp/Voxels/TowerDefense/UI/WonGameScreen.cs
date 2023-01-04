using System;
using System.Collections;
using Fabric;
using I2.Loc;
using RTM.UISystem;
using UnityEngine;
using UnityEngine.UI;
using Voxels.TowerDefense.ProfileInternals;
using Voxels.TowerDefense.ScriptAnimations;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x02000561 RID: 1377
	public class WonGameScreen : UIMenu, IslandUIManager.IAwake, IslandGameplayManager.IWipeIsland
	{
		// Token: 0x060023DD RID: 9181 RVA: 0x0006FB11 File Offset: 0x0006DF11
		private void InitVisibility(UnityEngine.Component comp, ref IUIVisibility vis)
		{
			vis = comp.GetComponent<IUIVisibility>();
			vis.SetVisible(false, true);
		}

		// Token: 0x060023DE RID: 9182 RVA: 0x0006FB24 File Offset: 0x0006DF24
		void IslandUIManager.IAwake.OnAwake(IslandUIManager manager)
		{
			base.gameObject.SetActive(false);
			this.manager = manager;
			this.InitVisibility(this.buttonContainer, ref this.buttonVisibility);
			this.InitVisibility(this.replayButton, ref this.replayButtonVisibility);
			this.levelCamera = manager.gameplayManager.levelCamera;
			ShaderConstantsOverride shaderModifier = base.GetComponent<ShaderConstantsOverride>();
			this.grayscale = new AnimatedState("Gray", this.root.rootState, false, false);
			this.grayscale.anim.Subscribe(delegate(float x)
			{
				shaderModifier.alpha = x;
			});
			this.congratulationsCard.gameObject.SetActive(false);
			this.veryHardUnlock.gameObject.SetActive(false);
			this.veryHardUnlock.transform.localPosition = Vector3.zero;
			this.dim.gameObject.SetActive(false);
		}

		// Token: 0x060023DF RID: 9183 RVA: 0x0006FC0C File Offset: 0x0006E00C
		public override void OpenMenu()
		{
			throw new Exception("calling WonGameScreen with no parameter!");
		}

		// Token: 0x060023E0 RID: 9184 RVA: 0x0006FC18 File Offset: 0x0006E018
		public void OpenMenu(Island island)
		{
			bool allowReplays = island.levelNode.campaign.campaignSave.prefs.allowReplays;
			this.playingCredits = false;
			IslandGenerator.AddBlocker(this, this);
			FabricWrapper.PostEvent(this.musicLoop);
			this.dim.color = this.dim.color.SetA(0f);
			this.buttonVisibility.SetVisible(false, true);
			this.replayButtonVisibility.SetVisible(allowReplays, true);
			this.continueButtonLocalize.Term = "UI/COMMON/CONTINUE";
			this.winState.SetActive(true);
			base.gameObject.SetActive(true);
			base.StartCoroutine(this.Animate());
			this.continueButtonLocalize.Term = this.continueButtonLoc;
			base.OpenMenu();
		}

		// Token: 0x060023E1 RID: 9185 RVA: 0x0006FCE0 File Offset: 0x0006E0E0
		private IEnumerator Animate()
		{
			LevelCamera levelCamera = this.manager.gameplayManager.levelCamera;
			levelCamera.FocusOnIsland(false, 0.15f);
			this.grayscale.SetActive(true);
			float endTime = Time.unscaledTime + 0.6f;
			while (Time.unscaledTime < endTime)
			{
				yield return null;
			}
			this.congratulationsCard.Prepare(Singleton<IslandGameplayManager>.instance.island.levelNode);
			while (this.congratulationsCard.playing)
			{
				yield return null;
			}
			this.congratulationsCard.gameObject.SetActive(false);
			UserSave userSave = Profile.userSave;
			CampaignSave campaignSave = Profile.campaign;
			Difficulty difficulty = campaignSave.prefs.difficulty;
			if (difficulty == Difficulty.Hard && userSave.maxDifficulty == Difficulty.Hard)
			{
				float endTime2 = Time.unscaledTime + 0.2f;
				while (Time.unscaledTime < endTime2)
				{
					yield return null;
				}
				FabricWrapper.PostEvent(this.veryHardUnlockSound);
				this.veryHardUnlock.gameObject.SetActive(true);
				yield return null;
				while (this.veryHardUnlock.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
				{
					yield return null;
				}
				this.veryHardUnlock.gameObject.SetActive(false);
				userSave.maxDifficulty = Difficulty.VeryHard;
				userSave.campaignPrefs.difficulty = Difficulty.VeryHard;
				Profile.SaveUserSave();
			}
			this.grayscale.SetActive(false);
			levelCamera.UnlockPanYPos();
			levelCamera.GetComponent<CameraZoomer>().LockZoom = false;
			this.RollCredits();
			this.buttonVisibility.SetVisible(true, false);
			yield break;
		}

		// Token: 0x060023E2 RID: 9186 RVA: 0x0006FCFC File Offset: 0x0006E0FC
		private void RollCredits()
		{
			CreditsUI.Roll(TextAlignment.Left, UnityEngine.Color.black);
			CreditsUI.onCreditsComplete += this.OnCreditsComplete;
			this.playingCredits = true;
			this.manager.islandViewfinder.Push(this.islandView, this.preCreditsIslandAnim);
		}

		// Token: 0x060023E3 RID: 9187 RVA: 0x0006FD48 File Offset: 0x0006E148
		private void CancelCredits()
		{
			if (this.playingCredits)
			{
				this.playingCredits = false;
				CreditsUI.Cancel();
				CreditsUI.onCreditsComplete -= this.OnCreditsComplete;
			}
		}

		// Token: 0x060023E4 RID: 9188 RVA: 0x0006FD72 File Offset: 0x0006E172
		private void OnCreditsComplete()
		{
			this.manager.islandViewfinder.Remove(this.islandView, this.postCreditsIslandAnim);
			CreditsUI.onCreditsComplete -= this.OnCreditsComplete;
			this.continueButtonLocalize.Term = this.continueButtonLoc;
		}

		// Token: 0x060023E5 RID: 9189 RVA: 0x0006FDB4 File Offset: 0x0006E1B4
		private IEnumerator AnimateAway()
		{
			this.buttonVisibility.SetVisible(false, false);
			this.CancelCredits();
			this.dim.gameObject.SetActive(true);
			for (float a = 0f; a < 1f; a += Time.unscaledDeltaTime / this.fadeTime)
			{
				this.dim.color = this.dim.color.SetA(a);
				yield return null;
			}
			this.dim.color = this.dim.color.SetA(1f);
			foreach (Squad squad in this.manager.gameplayManager.island.english.allSquads)
			{
				EnglishSquad enSquad = (EnglishSquad)squad;
				IEnumerator enumerator2 = enSquad.hero.monoHero.UpdateAliveSprite(enSquad.heroAlive, false).GetEnumerator();
				try
				{
					while (enumerator2.MoveNext())
					{
						object spriteUpdate = enumerator2.Current;
						yield return null;
					}
				}
				finally
				{
					IDisposable disposable;
					if ((disposable = (enumerator2 as IDisposable)) != null)
					{
						disposable.Dispose();
					}
				}
			}
			float end = Time.unscaledTime + this.postFadeDelay;
			while (end > Time.unscaledTime)
			{
				yield return null;
			}
			this.manager.gameplayManager.levelLeaver.CompleteLevel();
			yield break;
		}

		// Token: 0x060023E6 RID: 9190 RVA: 0x0006FDCF File Offset: 0x0006E1CF
		public override void CloseMenu()
		{
			base.gameObject.SetActive(false);
			IslandGenerator.RemoveBlocker(this, this);
			base.CloseMenu();
		}

		// Token: 0x060023E7 RID: 9191 RVA: 0x0006FDEA File Offset: 0x0006E1EA
		public void HandleReplayButton()
		{
			this.CancelCredits();
			FabricWrapper.PostEvent(this.musicLoop, EventAction.StopSound);
			this.manager.gameplayManager.levelLeaver.ReplayLevel();
		}

		// Token: 0x060023E8 RID: 9192 RVA: 0x0006FE14 File Offset: 0x0006E214
		public void HandleContinueButton()
		{
			this.CancelCredits();
			base.StartCoroutine(this.AnimateAway());
		}

		// Token: 0x060023E9 RID: 9193 RVA: 0x0006FE29 File Offset: 0x0006E229
		void IslandGameplayManager.IWipeIsland.OnWipe(Island island)
		{
			this.dim.gameObject.SetActive(false);
			if (base.isOpen)
			{
				this.CloseMenu();
			}
		}

		// Token: 0x060023EA RID: 9194 RVA: 0x0006FE4D File Offset: 0x0006E24D
		private void Update()
		{
			this.root.Update();
		}

		// Token: 0x0400166F RID: 5743
		[SerializeField]
		private State winState;

		// Token: 0x04001670 RID: 5744
		[SerializeField]
		private RectTransform islandView;

		// Token: 0x04001671 RID: 5745
		private IslandUIManager manager;

		// Token: 0x04001672 RID: 5746
		[SerializeField]
		private RectTransform buttonContainer;

		// Token: 0x04001673 RID: 5747
		private IUIVisibility buttonVisibility;

		// Token: 0x04001674 RID: 5748
		[SerializeField]
		private UIClickable replayButton;

		// Token: 0x04001675 RID: 5749
		private IUIVisibility replayButtonVisibility;

		// Token: 0x04001676 RID: 5750
		[SerializeField]
		private Localize continueButtonLocalize;

		// Token: 0x04001677 RID: 5751
		[SerializeField]
		[TermsPopup("")]
		private string continueButtonLoc = "UI/COMMON/CONTINUE";

		// Token: 0x04001678 RID: 5752
		[SerializeField]
		private Image dim;

		// Token: 0x04001679 RID: 5753
		[SerializeField]
		private EndLevelCard congratulationsCard;

		// Token: 0x0400167A RID: 5754
		[SerializeField]
		private Animator veryHardUnlock;

		// Token: 0x0400167B RID: 5755
		[SerializeField]
		private float fadeawayDelay = 1f;

		// Token: 0x0400167C RID: 5756
		[SerializeField]
		private float fadeTime = 4f;

		// Token: 0x0400167D RID: 5757
		[SerializeField]
		private float postFadeDelay = 1.5f;

		// Token: 0x0400167E RID: 5758
		private FabricEventReference musicLoop = "Mus/GameWin";

		// Token: 0x0400167F RID: 5759
		private LevelCamera levelCamera;

		// Token: 0x04001680 RID: 5760
		private bool playingCredits;

		// Token: 0x04001681 RID: 5761
		private LerpTowards preCreditsIslandAnim = new LerpTowards(1f, 0.02f);

		// Token: 0x04001682 RID: 5762
		private LerpTowards postCreditsIslandAnim = new LerpTowards(2f, 0.03f);

		// Token: 0x04001683 RID: 5763
		private FabricEventReference veryHardUnlockSound = "UI/InGame/UnlockVeryHard";

		// Token: 0x04001684 RID: 5764
		[SerializeField]
		private AgentStateRoot root;

		// Token: 0x04001685 RID: 5765
		private AnimatedState grayscale;
	}
}
