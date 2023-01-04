using System;
using System.Collections;
using CS.Platform;
using Fabric;
using TrialVersion;
using UnityEngine;
using Voxels.TowerDefense.ProfileInternals;
using Voxels.TowerDefense.UI.MetaInventory;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x020008F4 RID: 2292
	public class MainMenu : GeneratedMenu
	{
		// Token: 0x06003CD3 RID: 15571 RVA: 0x0010FD85 File Offset: 0x0010E185
		private bool HasSaves()
		{
			return Profile.meta.HasSaves;
		}

		// Token: 0x06003CD4 RID: 15572 RVA: 0x0010FD91 File Offset: 0x0010E191
		private bool IsAuthenticated()
		{
			return Social.localUser.authenticated;
		}

		// Token: 0x06003CD5 RID: 15573 RVA: 0x0010FDA0 File Offset: 0x0010E1A0
		protected override void Initialize()
		{
			base.gameObject.SetActive(false);
			base.Initialize();
			this.SetupBackButton();
			Platform.onPlatformUpdated += this.SetupBackButton;
			this.visibility = this.widgetParent.GetComponentInChildren<IUIVisibility>();
			this.visibility.SetVisible(false, true);
			this.quitFadeVisibility = this.quitFade.GetComponentInChildren<IUIVisibility>();
			this.quitFadeVisibility.SetVisible(false, true);
			this.AddButton("TRIAL/FULL_VERSION", Utils.launchStore, null, null).SetHoverAudio(FabricID.uiHover).SetVisibilityCallback(new Func<bool>(this.HasSaves));
			this.AddButton("UI/MAIN_MENU/PLAY", new Func<bool>(this.Play), null, null).SetSuccessAudio("UI/Menu/Load").SetHoverAudio(FabricID.uiHover);
			this.AddButton("META_INVENTORY/TITLE", delegate()
			{
				MetaInventoryMenu.OpenMenuStatic(null, null);
				return true;
			}, null, null).SetSuccessAudio("UI/Menu/Inventory").SetHoverAudio(FabricID.uiHover);
			this.AddButton("UI/COMMON/SETTINGS", delegate()
			{
				UserSettingsMenu.Open(0.92f);
				return true;
			}, null, null).SetSuccessAudio("UI/Menu/Settings").SetHoverAudio(FabricID.uiHover);
			this.AddButton("CREDITS/TITLE", delegate()
			{
				CreditsContainerUI.Show();
				return true;
			}, null, null).SetHoverAudio(FabricID.uiHover);
			this.AddButton("UI/COMMON/EXIT_TO_DESKTOP", delegate()
			{
				this.FadeToQuit();
				return true;
			}, null, null).SetSuccessAudio("UI/Menu/Quit").SetHoverAudio(FabricID.uiHover).SetVisibilityCallback(() => Platform.Is(EPlatform.PC));
		}

		// Token: 0x06003CD6 RID: 15574 RVA: 0x0010FF85 File Offset: 0x0010E385
		private void SetupBackButton()
		{
			this._wantsBackButton = Platform.Is(EPlatform.Android);
		}

		// Token: 0x06003CD7 RID: 15575 RVA: 0x0010FF97 File Offset: 0x0010E397
		protected override void OnEnable()
		{
			base.OnEnable();
			this.OpenMenu();
			base.transform.ForceChildLayoutUpdates(false);
			IslandGenerator.AddBlocker(this, this);
		}

		// Token: 0x06003CD8 RID: 15576 RVA: 0x0010FFB8 File Offset: 0x0010E3B8
		private void OnDisable()
		{
			this.CloseMenu();
			IslandGenerator.RemoveBlocker(this, this);
		}

		// Token: 0x06003CD9 RID: 15577 RVA: 0x0010FFC8 File Offset: 0x0010E3C8
		public override void OpenMenu()
		{
			Utils.RefreshEntitlement();
			base.OpenMenu();
			if (BasePlatformManager.Instance != null)
			{
				BasePlatformManager.Instance.SetRichPresenceDetails("MAIN_MENU", null);
				BasePlatformManager.Instance.SetRichPresenceLargeImage("MAIN_MENU");
				BasePlatformManager.Instance.SendRichPresence();
			}
			if (!Social.localUser.authenticated)
			{
			}
		}

		// Token: 0x06003CDA RID: 15578 RVA: 0x00110028 File Offset: 0x0010E428
		protected override void OnGainedFocus()
		{
			this.visibility.SetVisible(true, false);
			base.OnGainedFocus();
		}

		// Token: 0x06003CDB RID: 15579 RVA: 0x0011003D File Offset: 0x0010E43D
		protected override void OnLostFocus()
		{
			this.visibility.SetVisible(false, false);
			base.OnLostFocus();
		}

		// Token: 0x06003CDC RID: 15580 RVA: 0x00110052 File Offset: 0x0010E452
		private bool Play()
		{
			if (this.HasSaves())
			{
				SaveGameManagementMenu.Open();
			}
			else
			{
				NewGameOptionsPopup.OpenMenu(null);
			}
			return true;
		}

		// Token: 0x06003CDD RID: 15581 RVA: 0x00110070 File Offset: 0x0010E470
		private bool ContinueLastCampaign()
		{
			CampaignSaveMeta campaignSaveMeta = null;
			foreach (CampaignSaveMeta campaignSaveMeta2 in Profile.meta.campaigns)
			{
				if (campaignSaveMeta2 != null)
				{
					if (campaignSaveMeta == null || campaignSaveMeta2.savedTime > campaignSaveMeta.savedTime)
					{
						campaignSaveMeta = campaignSaveMeta2;
					}
				}
			}
			return campaignSaveMeta != null && MetaMenuHelpers.LoadGame(campaignSaveMeta);
		}

		// Token: 0x06003CDE RID: 15582 RVA: 0x00110104 File Offset: 0x0010E504
		private void FadeToQuit()
		{
			this.blockingInput = true;
			base.StartCoroutine(this.Quit(0.15f, 2.8f, 0.8f));
		}

		// Token: 0x06003CDF RID: 15583 RVA: 0x0011012C File Offset: 0x0010E52C
		private IEnumerator Quit(float delay, float seconds, float musFadeTime)
		{
			FabricWrapper.PostEvent("Mus/ExitGame");
			FabricWrapper.PostEvent("Mus/Menu", EventAction.StopSound);
			float startTime = Time.unscaledTime;
			while (Time.unscaledTime < startTime + delay)
			{
				yield return null;
			}
			this.quitFadeVisibility.visible = true;
			startTime = Time.unscaledTime;
			while (Time.unscaledTime < startTime + seconds)
			{
				float param = Mathf.InverseLerp(musFadeTime, 0f, Time.unscaledTime - startTime);
				EventManager.Instance.SetParameter("Mus/Menu", "ExitGame", param, null);
				yield return null;
			}
			MetaMenuHelpers.Quit();
			yield break;
		}

		// Token: 0x06003CE0 RID: 15584 RVA: 0x0011015C File Offset: 0x0010E55C
		public override bool HandleBackButton()
		{
			ModalOverlay.GetInstance().Initialize("UI/COMMON/EXIT_TO_DESKTOP", "UI/MAIN_MENU/LEAVE_GAME", true).AddButton("UI/COMMON/OK", delegate()
			{
				this.FadeToQuit();
				return true;
			}, null, null);
			return true;
		}

		// Token: 0x04002A7C RID: 10876
		[Space]
		[Header("Main Menu")]
		[SerializeField]
		private Transform quitFade;

		// Token: 0x04002A7D RID: 10877
		private IUIVisibility visibility;

		// Token: 0x04002A7E RID: 10878
		private IUIVisibility quitFadeVisibility;
	}
}
