using System;
using I2.Loc;
using Rewired;
using RTM.Input;
using RTM.UISystem;
using UnityEngine;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x020008B7 RID: 2231
	internal class ControlsGamepadMenu : UIMenu, IGameSetup, IScenePostprocessor
	{
		// Token: 0x06003A91 RID: 14993 RVA: 0x00104BEE File Offset: 0x00102FEE
		public static string GetTitleLocTerm()
		{
			if (Platform.Is(EPlatform.PC) || Platform.Is(EPlatform.Mobile))
			{
				return "CONTROLS/GAMEPAD/NAME";
			}
			if (Platform.Is(EPlatform.Console))
			{
				return "CONTROLS/TITLE";
			}
			return "CONTROLS/TITLE";
		}

		// Token: 0x06003A92 RID: 14994 RVA: 0x00104C2C File Offset: 0x0010302C
		void IGameSetup.OnGameAwake()
		{
			ControlsGamepadMenu.instance = this;
			this.visibilities = base.GetComponentsInChildren<IUIVisibility>(true);
			foreach (IUIVisibility iuivisibility in this.visibilities)
			{
				iuivisibility.SetVisible(false, true);
			}
			this.pcGamepad = this.xboxOneGamepad;
			this.InputHelpers_onControllerTypeChanged(InputHelpers.GetControllerType());
			InputHelpers.onControllerTypeChanged += this.InputHelpers_onControllerTypeChanged;
			this.widget = base.GetComponentInChildren<MultiSelectWidget>(true);
			this.widget.SetValues(new string[]
			{
				"CONTROLS/GAMEPAD/LAYOUT/DEFAULT",
				"CONTROLS/GAMEPAD/LAYOUT/QUICK_SELECT"
			}, true).SetIncrementCycling(false).Initialize("CONTROLS/GAMEPAD/NAME", () => (int)Profile.userSettings.gamepadLayout, delegate(int value)
			{
				Profile.userSettings.gamepadLayout = (UserSettings.GamepadLayout)value;
				Profile.userSettings.ProcessUpdate(true);
				return true;
			}).SetSuccessAudio(FabricID.settingChange);
			this.widget.ForceUpdate();
			this.UpdateDisplay();
			Platform.onPlatformUpdated += this.UpdateDisplay;
			this.OnUserSettingsUpdated(Profile.userSettings);
			UserSettings.onUpdated += this.OnUserSettingsUpdated;
		}

		// Token: 0x06003A93 RID: 14995 RVA: 0x00104D60 File Offset: 0x00103160
		private void OnUserSettingsUpdated(UserSettings settings)
		{
			if (!settings)
			{
				return;
			}
			UserSettings.GamepadLayout gamepadLayout = settings.gamepadLayout;
			foreach (GameObject gameObject in this.classicOnlyObjects)
			{
				if (gameObject)
				{
					gameObject.SetActive(gamepadLayout == UserSettings.GamepadLayout.Classic);
				}
			}
			foreach (GameObject gameObject2 in this.quickSelectOnlyObjects)
			{
				if (gameObject2)
				{
					gameObject2.SetActive(gamepadLayout == UserSettings.GamepadLayout.QuickSelect);
				}
			}
		}

		// Token: 0x06003A94 RID: 14996 RVA: 0x00104DF3 File Offset: 0x001031F3
		public override void OpenMenu()
		{
			this.UpdateSelectAndCancel();
			base.OpenMenu();
		}

		// Token: 0x06003A95 RID: 14997 RVA: 0x00104E04 File Offset: 0x00103204
		private void UpdateDisplay()
		{
			this.HideGamepad(this.switchGamePad);
			this.HideGamepad(this.ps4Gamepad);
			this.HideGamepad(this.xboxOneGamepad);
			this.HideGamepad(this.pcGamepad);
			if (Platform.Is(EPlatform.PS4))
			{
				this.ps4Gamepad.SetActive(true);
			}
			else if (Platform.Is(EPlatform.XboxOne))
			{
				this.xboxOneGamepad.SetActive(true);
			}
			else if (Platform.Is(EPlatform.Switch))
			{
				this.switchGamePad.SetActive(true);
			}
			else if (Platform.Is(EPlatform.Windows | EPlatform.Mac | EPlatform.Linux | EPlatform.AndroidPhone | EPlatform.IOSPhone | EPlatform.AndroidTablet | EPlatform.IOSTablet))
			{
				this.pcGamepad.SetActive(true);
			}
			else
			{
				this.xboxOneGamepad.SetActive(true);
			}
			this.UpdateSelectAndCancel();
			this.UpdateTitle();
			float d = (!Platform.Is(this.tvScaledPlatforms)) ? 1f : 1.3f;
			this.gamepadRoot.transform.localScale = (Vector3.one * d).SetZ(1f);
		}

		// Token: 0x06003A96 RID: 14998 RVA: 0x00104F18 File Offset: 0x00103318
		private void InputHelpers_onControllerTypeChanged(ControllerType controllerType)
		{
			if (controllerType == ControllerType.Joystick)
			{
				Joystick joystick = InputHelpers.GetController() as Joystick;
				if (joystick != null)
				{
					this.pcGamepad = this.GetGamepadForJoystick(joystick.hardwareTypeGuid);
				}
				this.UpdateDisplay();
			}
		}

		// Token: 0x06003A97 RID: 14999 RVA: 0x00104F58 File Offset: 0x00103358
		private GameObject GetGamepadForJoystick(Guid joystickGuid)
		{
			if (joystickGuid == Guid.Empty)
			{
				return this.xboxOneGamepad;
			}
			string a = joystickGuid.ToString();
			bool flag = a == "c3ad3cad-c7cf-4ca8-8c2e-e3df8d9960bb" || a == "71dfe6c8-9e81-428f-a58e-c7e664b7fbed" || a == "cd9718bf-a87a-44bc-8716-60a0def28a9f";
			if (flag)
			{
				return this.ps4Gamepad;
			}
			bool flag2 = a == "521b808c-0248-4526-bc10-f1d16ee76bf1" || a == "7bf3154b-9db8-4d52-950f-cd0eed8a5819";
			if (flag2)
			{
				return this.switchGamePad;
			}
			return this.xboxOneGamepad;
		}

		// Token: 0x06003A98 RID: 15000 RVA: 0x00104FF8 File Offset: 0x001033F8
		private void UpdateSelectAndCancel()
		{
		}

		// Token: 0x06003A99 RID: 15001 RVA: 0x00104FFA File Offset: 0x001033FA
		private void HideGamepad(GameObject gamepad)
		{
			if (gamepad)
			{
				gamepad.SetActive(false);
			}
		}

		// Token: 0x06003A9A RID: 15002 RVA: 0x0010500E File Offset: 0x0010340E
		public static void Open(bool saveOnExit)
		{
			ControlsGamepadMenu.instance.saveOnExit = saveOnExit;
			ControlsGamepadMenu.instance.OpenMenu();
		}

		// Token: 0x06003A9B RID: 15003 RVA: 0x00105025 File Offset: 0x00103425
		public void UpdateTitle()
		{
			this.titleLoc.Term = ControlsGamepadMenu.GetTitleLocTerm();
		}

		// Token: 0x06003A9C RID: 15004 RVA: 0x00105038 File Offset: 0x00103438
		protected override void OnGainedFocus()
		{
			base.OnGainedFocus();
			foreach (IUIVisibility iuivisibility in this.visibilities)
			{
				iuivisibility.SetVisible(true, false);
			}
			this.widget.ForceUpdate();
		}

		// Token: 0x06003A9D RID: 15005 RVA: 0x00105080 File Offset: 0x00103480
		public override void CloseMenu()
		{
			base.CloseMenu();
			foreach (IUIVisibility iuivisibility in this.visibilities)
			{
				iuivisibility.SetVisible(false, false);
			}
			if (this.saveOnExit && Profile.userSettings.dirty)
			{
				Profile.SaveSettings();
			}
			this.saveOnExit = false;
		}

		// Token: 0x06003A9E RID: 15006 RVA: 0x001050E0 File Offset: 0x001034E0
		void IScenePostprocessor.OnPostprocessScene()
		{
		}

		// Token: 0x040028A2 RID: 10402
		[SerializeField]
		private Localize titleLoc;

		// Token: 0x040028A3 RID: 10403
		[SerializeField]
		private GameObject gamepadRoot;

		// Token: 0x040028A4 RID: 10404
		[SerializeField]
		private GameObject ps4Gamepad;

		// Token: 0x040028A5 RID: 10405
		[SerializeField]
		private GameObject switchGamePad;

		// Token: 0x040028A6 RID: 10406
		[SerializeField]
		private GameObject xboxOneGamepad;

		// Token: 0x040028A7 RID: 10407
		[SerializeField]
		private EPlatform tvScaledPlatforms;

		// Token: 0x040028A8 RID: 10408
		[Header("PS4")]
		[SerializeField]
		private Localize crossLabel;

		// Token: 0x040028A9 RID: 10409
		[SerializeField]
		private Localize circleLabel;

		// Token: 0x040028AA RID: 10410
		[SerializeField]
		[TermsPopup("")]
		private string selectTerm;

		// Token: 0x040028AB RID: 10411
		[SerializeField]
		[TermsPopup("")]
		private string cancelTerm;

		// Token: 0x040028AC RID: 10412
		[Header("Layouts")]
		[SerializeField]
		private GameObject[] classicOnlyObjects = new GameObject[0];

		// Token: 0x040028AD RID: 10413
		[SerializeField]
		private GameObject[] quickSelectOnlyObjects = new GameObject[0];

		// Token: 0x040028AE RID: 10414
		private GameObject pcGamepad;

		// Token: 0x040028AF RID: 10415
		private static ControlsGamepadMenu instance;

		// Token: 0x040028B0 RID: 10416
		private IUIVisibility[] visibilities;

		// Token: 0x040028B1 RID: 10417
		private MultiSelectWidget widget;

		// Token: 0x040028B2 RID: 10418
		private bool saveOnExit;
	}
}
