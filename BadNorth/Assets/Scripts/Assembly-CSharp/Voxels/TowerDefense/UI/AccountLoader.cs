using System;
using System.Collections;
using CS.Platform;
using CS.VT;
using Fabric;
using Rewired;
using RTM.Input;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x020008F3 RID: 2291
	public class AccountLoader : GeneratedMenu
	{
		// Token: 0x06003CC1 RID: 15553 RVA: 0x0010F5F0 File Offset: 0x0010D9F0
		private new void Awake()
		{
			this._wantsBackButton = Platform.Is(EPlatform.Android);
			this.buttonDecoration.SetActive(false);
			this.quitFadeVisibility = this.quitFade.GetComponent<IUIVisibility>();
			this.quitFadeVisibility.SetVisible(false, true);
			if (this.logoContainer != null)
			{
				this.logoVisibility = this.logoContainer.GetComponentInChildren<IUIVisibility>();
			}
			this._parentCloseAllowed = false;
			this._doneSplash = SplashScreen.isFinished;
			this.dots = this.dotContainer.GetComponentsInChildren<Image>(true);
			base.Awake();
			if (CorePlatformSetup.Setup())
			{
				this.HideStart();
			}
			else if (BasePlatformManager.Instance.DynamicUsers)
			{
				this.UpdateActiveScreen();
			}
			else
			{
				this.ShowStart(new Func<bool>(this.AccountChoosen));
			}
		}

		// Token: 0x06003CC2 RID: 15554 RVA: 0x0010F6C4 File Offset: 0x0010DAC4
		private new void OnEnable()
		{
			base.OnEnable();
			AccountLoader.s_doneStart = false;
			this.UpdateActiveScreen();
			PlatformEvents.OnPlatformInitializedEvent += this.UpdateActiveScreen;
			PlatformEvents.OnPlatformLoadUnblockedEvent += this.UpdateActiveScreen;
			PlatformEvents.OnEntitlementChangedEvent += this.UpdateActiveScreen;
			PlatformEvents.OnLoadLocalCompleteEvent += this.UpdateActiveScreen;
			PlatformEvents.OnMainUserStateEvent += this.UpdateActiveScreen;
		}

		// Token: 0x06003CC3 RID: 15555 RVA: 0x0010F738 File Offset: 0x0010DB38
		private void OnDisable()
		{
			this.HideStart();
			PlatformEvents.OnPlatformInitializedEvent -= this.UpdateActiveScreen;
			PlatformEvents.OnPlatformLoadUnblockedEvent -= this.UpdateActiveScreen;
			PlatformEvents.OnEntitlementChangedEvent -= this.UpdateActiveScreen;
			PlatformEvents.OnLoadLocalCompleteEvent -= this.UpdateActiveScreen;
			PlatformEvents.OnMainUserStateEvent -= this.UpdateActiveScreen;
		}

		// Token: 0x06003CC4 RID: 15556 RVA: 0x0010F7A0 File Offset: 0x0010DBA0
		public void UpdateActiveScreen()
		{
			if (this.logoVisibility != null)
			{
				this.logoVisibility.SetVisible(true, true);
			}
			if (!this.PlatformSetupComplete())
			{
				this.HideStart();
				this.dotContainer.SetActive(true);
			}
			else if (BasePlatformManager.Instance.MainUserID < 0 || !AccountLoader.s_doneStart)
			{
				this.dotContainer.SetActive(false);
				this.ShowStart(new Func<bool>(this.PlatformReadyActivate));
			}
			else if (!this.UserSetupComplete())
			{
				this.HideStart();
				this.dotContainer.SetActive(true);
			}
			else
			{
				this.AccountChoosen();
			}
		}

		// Token: 0x06003CC5 RID: 15557 RVA: 0x0010F84D File Offset: 0x0010DC4D
		private void UpdateActiveScreen(bool overload)
		{
			this.UpdateActiveScreen();
		}

		// Token: 0x06003CC6 RID: 15558 RVA: 0x0010F855 File Offset: 0x0010DC55
		private bool AccountChoosen()
		{
			Singleton<Stack>.instance.stateMeta.children[0].SetActiveFalse();
			BasePlatformManager.StartGame();
			return true;
		}

		// Token: 0x06003CC7 RID: 15559 RVA: 0x0010F874 File Offset: 0x0010DC74
		private bool PlatformSetupComplete()
		{
			float num = 0f;
			num += (float)((!BasePlatformManager.Initialized) ? 0 : 1);
			num += (float)((!BasePlatformManager.EntitlementChecked) ? 0 : 1);
			num += (float)((!BasePlatformManager.HasEntitlement) ? 0 : 1);
			num += (float)((!SplashScreen.isFinished) ? 0 : 1);
			if (BasePlatformManager.Instance != null)
			{
				num += (float)(BasePlatformManager.Instance.IsLoadBlocked ? 0 : 1);
			}
			return num == 5f;
		}

		// Token: 0x06003CC8 RID: 15560 RVA: 0x0010F910 File Offset: 0x0010DD10
		private bool UserSetupComplete()
		{
			float num = 0f;
			if (!BasePlatformManager.Instance.IsLoadBlocked)
			{
				num += 1f;
				num += (float)((!this._coreplatformLoadStarted) ? 0 : 1);
				if (!this._coreplatformLoadStarted)
				{
					this._coreplatformLoadStarted = true;
					Profile.ReloadProfile();
				}
				else
				{
					num += (float)(Profile.Loading ? 0 : 1);
				}
				num += (float)(BasePlatformManager.Instance.IsLocalLoading ? 0 : 1);
			}
			return num == 4f;
		}

		// Token: 0x06003CC9 RID: 15561 RVA: 0x0010F9A4 File Offset: 0x0010DDA4
		protected override void Update()
		{
			base.Update();
			for (int i = 0; i < this.dots.Length; i++)
			{
				Image image = this.dots[i];
				float f = Time.unscaledTime * 5f - 6.2831855f * ((float)i / (float)this.dots.Length / 1.5f);
				image.transform.localScale = Vector3.one * (1f + 0.3f * Mathf.Sin(f));
			}
			if (SplashScreen.isFinished != this._doneSplash)
			{
				this._doneSplash = SplashScreen.isFinished;
				this.UpdateActiveScreen();
			}
			if (this._shownStart && ReInput.isReady && !BasePlatformManager.Instance.GUIActive && ReInput.controllers.GetAnyButtonDown(ControllerType.Joystick))
			{
				FabricWrapper.PostEvent(FabricID.uiButtonClick);
				this.PlatformReadyActivate();
			}
		}

		// Token: 0x06003CCA RID: 15562 RVA: 0x0010FA8C File Offset: 0x0010DE8C
		private void ShowStart(Func<bool> startAction)
		{
			if (!base.isOpen)
			{
				this.OpenMenu();
			}
			this.dotContainer.SetActive(false);
			this.buttonDecoration.SetActive(false);
			this.ClearWidgets();
			this.AddButton("UI/MAIN_MENU/START", startAction, null, null).SetSuccessAudio("UI/Menu/Start");
			this._shownStart = true;
			InputHelpers.SearchForJoystick = false;
			ReInput.players.GetPlayer(0).controllers.ClearControllersOfType(ControllerType.Joystick);
			base.transform.ForceChildLayoutUpdates(false);
		}

		// Token: 0x06003CCB RID: 15563 RVA: 0x0010FB18 File Offset: 0x0010DF18
		private bool PlatformReadyActivate()
		{
			Controller lastActiveController = ReInput.controllers.GetLastActiveController(ControllerType.Joystick);
			ReInput.players.GetPlayer(0).controllers.ClearControllersOfType(ControllerType.Joystick);
			if (lastActiveController != null && lastActiveController.isConnected)
			{
				ReInput.players.GetPlayer(0).controllers.AddController(lastActiveController, true);
				RewiredHelpers.ApplyPendingSwaps(ControllerType.Joystick, lastActiveController.id);
			}
			if (0 <= BasePlatformManager.Instance.MainUserID)
			{
				AccountLoader.s_doneStart = true;
				this.UpdateActiveScreen();
				InputHelpers.SearchForJoystick = true;
				return true;
			}
			AccountLoader.s_doneStart = true;
			this._coreplatformLoadStarted = false;
			BasePlatformManager.Instance.UserJoined(0);
			InputHelpers.SearchForJoystick = true;
			return false;
		}

		// Token: 0x06003CCC RID: 15564 RVA: 0x0010FBBF File Offset: 0x0010DFBF
		private void HideStart()
		{
			this.ClearWidgets();
			if (base.isOpen)
			{
				this.CloseMenu();
			}
			this._shownStart = false;
		}

		// Token: 0x06003CCD RID: 15565 RVA: 0x0010FBDF File Offset: 0x0010DFDF
		public override bool HandleBackButton()
		{
			ModalOverlay.GetInstance().Initialize("UI/COMMON/EXIT_TO_DESKTOP", "UI/MAIN_MENU/LEAVE_GAME", true).AddButton("UI/COMMON/OK", delegate()
			{
				this.FadeToQuit();
				return true;
			}, null, null);
			return true;
		}

		// Token: 0x06003CCE RID: 15566 RVA: 0x0010FC10 File Offset: 0x0010E010
		private void FadeToQuit()
		{
			this.blockingInput = true;
			this.quitFadeVisibility.visible = true;
			FabricWrapper.PostEvent("Mus/ExitGame");
			FabricWrapper.PostEvent("Mus/Menu", EventAction.StopSound);
			base.StartCoroutine(this.Quit(2.8f, 0.8f));
		}

		// Token: 0x06003CCF RID: 15567 RVA: 0x0010FC60 File Offset: 0x0010E060
		private IEnumerator Quit(float seconds, float musFadeTime)
		{
			float startTime = Time.unscaledTime;
			while (Time.unscaledTime < startTime + seconds)
			{
				float param = Mathf.InverseLerp(musFadeTime, 0f, Time.unscaledTime - startTime);
				EventManager.Instance.SetParameter("Mus/Menu", "ExitGame", param, null);
				yield return null;
			}
			MetaMenuHelpers.Quit();
			yield break;
		}

		// Token: 0x04002A6F RID: 10863
		private bool _coreplatformLoadStarted;

		// Token: 0x04002A70 RID: 10864
		private static bool s_doneStart;

		// Token: 0x04002A71 RID: 10865
		private bool _doneSplash;

		// Token: 0x04002A72 RID: 10866
		private bool _shownStart;

		// Token: 0x04002A73 RID: 10867
		[SerializeField]
		private GameObject dotContainer;

		// Token: 0x04002A74 RID: 10868
		private Image[] dots;

		// Token: 0x04002A75 RID: 10869
		[SerializeField]
		private Transform logoContainer;

		// Token: 0x04002A76 RID: 10870
		private IUIVisibility logoVisibility;

		// Token: 0x04002A77 RID: 10871
		[SerializeField]
		[SpritePreview]
		private Sprite QQLogo;

		// Token: 0x04002A78 RID: 10872
		[SerializeField]
		[SpritePreview]
		private Sprite WechatLogo;

		// Token: 0x04002A79 RID: 10873
		[SerializeField]
		private GameObject buttonDecoration;

		// Token: 0x04002A7A RID: 10874
		[SerializeField]
		private GameObject quitFade;

		// Token: 0x04002A7B RID: 10875
		private IUIVisibility quitFadeVisibility;
	}
}
