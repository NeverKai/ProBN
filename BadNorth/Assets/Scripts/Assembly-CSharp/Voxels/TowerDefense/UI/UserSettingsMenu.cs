using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using RTM.Pools;
using RTM.UISystem;
using RTM.Utilities;
using UnityEngine;
using UnityEngine.UI;
using Voxels.TowerDefense.ScriptAnimations;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x020008C0 RID: 2240
	public class UserSettingsMenu : GeneratedMenu
	{
		// Token: 0x06003B11 RID: 15121 RVA: 0x001062FC File Offset: 0x001046FC
		protected override void Awake()
		{
			base.Awake();
			Profile.OnSettingsLoaded += this.ForceUpdateAll;
			MobileScreenDetector.onSafeZoneChanged += this.UpdateMobileSafeZone;
		}

		// Token: 0x06003B12 RID: 15122 RVA: 0x00106327 File Offset: 0x00104727
		private void UpdateMobileSafeZone(Vector4 safeZone, DeviceOrientation deviceOrientation)
		{
			if (Platform.Is(EPlatform.IOSPhone) && deviceOrientation == DeviceOrientation.LandscapeRight)
			{
				this.iPhonePadding = safeZone.x;
			}
			else
			{
				this.iPhonePadding = 0f;
			}
			this.UpdatePadding();
		}

		// Token: 0x06003B13 RID: 15123 RVA: 0x0010635F File Offset: 0x0010475F
		private void OnDestroy()
		{
			Profile.OnSettingsLoaded -= this.ForceUpdateAll;
		}

		// Token: 0x06003B14 RID: 15124 RVA: 0x00106373 File Offset: 0x00104773
		public static void Open(float backgroundAlpha)
		{
			UserSettingsMenu._instance.OpenMenu(null, backgroundAlpha);
		}

		// Token: 0x06003B15 RID: 15125 RVA: 0x00106381 File Offset: 0x00104781
		public static void Open(IslandUIManager ium)
		{
			UserSettingsMenu._instance.OpenMenu(ium, 0f);
		}

		// Token: 0x06003B16 RID: 15126 RVA: 0x00106394 File Offset: 0x00104794
		public void OpenMenu(IslandUIManager ium, float backgroundAlpha)
		{
			this.islandUIManager.Target = ium;
			if (ium)
			{
				this.container.pivot = this.container.pivot.SetX(1f);
				ium.islandViewfinder.Push(this.islandViewport, null);
			}
			else
			{
				this.container.pivot = this.container.pivot.SetX(0.5f);
			}
			this.centered = !ium;
			this.UpdatePadding();
			this.background.color = this.background.color.SetA(backgroundAlpha);
			this.safeAreaVisibility.SetVisible(false, true);
			this.visibility.SetVisible(true, false);
			base.OpenMenu();
			base.transform.ForceChildLayoutUpdates(false);
			this.slideSide.active = false;
			this.slideSide.ForceToTarget();
		}

		// Token: 0x06003B17 RID: 15127 RVA: 0x00106484 File Offset: 0x00104884
		private void UpdatePadding()
		{
			int num = this.categoryLayouts[0].padding.left;
			int num2 = 0;
			if (!this.centered)
			{
				num = -num;
				num2 = Mathf.CeilToInt(this.iPhonePadding * (float)Screen.width / base.transform.lossyScale.x);
				num += num2;
			}
			this.confirmRestoreTransform.GetComponent<LayoutGroup>().padding = new RectOffset(0, num2, 0, 0);
			foreach (VerticalLayoutGroup verticalLayoutGroup in this.categoryLayouts)
			{
				RectOffset padding = verticalLayoutGroup.padding;
				padding.right = num;
				verticalLayoutGroup.padding = padding;
			}
		}

		// Token: 0x06003B18 RID: 15128 RVA: 0x00106560 File Offset: 0x00104960
		public override void CloseMenu()
		{
			base.CloseMenu();
			this.SaveIfDirty();
			this.visibility.SetVisible(false, false);
			if (this.islandUIManager)
			{
				this.islandUIManager.Target.islandViewfinder.Remove(this.islandViewport, null);
			}
		}

		// Token: 0x06003B19 RID: 15129 RVA: 0x001065B2 File Offset: 0x001049B2
		protected override void OnGainedFocus()
		{
			base.OnGainedFocus();
			this.slideSide.SetActive(false);
			this.visibility.SetVisible(true, false);
		}

		// Token: 0x06003B1A RID: 15130 RVA: 0x001065D4 File Offset: 0x001049D4
		private bool ConfirmDefaults()
		{
			ModalOverlay.GetInstance().Initialize("SETTINGS/RESTORE_WARNING/TITLE", "SETTINGS/RESTORE_WARNING/MESSAGE", true).AddOKButton(new Func<bool>(this.SetDefaults)).SetSuccessAudio(this.resetAudio.name);
			return true;
		}

		// Token: 0x06003B1B RID: 15131 RVA: 0x00106614 File Offset: 0x00104A14
		private bool SetDefaults()
		{
			Profile.ResetUserSettings(true);
			Resolution[] resolutions = Screen.resolutions;
			Resolution resolution = resolutions.Last<Resolution>();
			Screen.SetResolution(resolution.width, resolution.height, true);
			this.SafeAreaChanged();
			this.ForceUpdateAll();
			return true;
		}

		// Token: 0x06003B1C RID: 15132 RVA: 0x00106655 File Offset: 0x00104A55
		private void SaveIfDirty()
		{
			if (Profile.userSettings.dirty)
			{
				Profile.SaveSettings();
			}
		}

		// Token: 0x06003B1D RID: 15133 RVA: 0x0010666B File Offset: 0x00104A6B
		private void UpdateOccasionally(Widget widget, bool includeDisabled = false)
		{
			if (Time.frameCount % 15 == 0 && (widget.isActiveAndEnabled || includeDisabled))
			{
				widget.ForceUpdate();
			}
		}

		// Token: 0x06003B1E RID: 15134 RVA: 0x00106694 File Offset: 0x00104A94
		protected override void Initialize()
		{
			UserSettingsMenu.<Initialize>c__AnonStorey0 <Initialize>c__AnonStorey = new UserSettingsMenu.<Initialize>c__AnonStorey0();
			<Initialize>c__AnonStorey.$this = this;
			base.Initialize();
			UserSettingsMenu._instance = this;
			base.gameObject.SetActive(false);
			this.visibility = base.GetComponent<IUIVisibility>();
			this.visibility.SetVisible(false, true);
			this.categoryLayouts.Add(this.categoryControls.parent.GetComponent<VerticalLayoutGroup>());
			this.categoryLayouts.Add(this.categoryDisplay.parent.GetComponent<VerticalLayoutGroup>());
			this.categoryLayouts.Add(this.categoryGraphics.parent.GetComponent<VerticalLayoutGroup>());
			this.categoryLayouts.Add(this.categoryAudio.parent.GetComponent<VerticalLayoutGroup>());
			this.safeAreaVisibility = this.safeAreaIndicator.GetComponent<IUIVisibility>();
			this.safeAreaVisibility.SetVisible(false, true);
			<Initialize>c__AnonStorey.languageWidget = this.AddPopupWidget("SETTINGS/LANGUAGE/NAME", UserSettings.LanguageDisplayNames, false, () => (int)Profile.userSettings.language.value, delegate(int value)
			{
				Profile.userSettings.language = (UserSettings.Language)value;
				Profile.userSettings.ProcessUpdate(true);
				return true;
			}, this.categoryGraphics, null);
			<Initialize>c__AnonStorey.languageWidget.SetSuccessAudio(FabricID.settingChange);
			<Initialize>c__AnonStorey.languageWidget.popup.onOpened += delegate()
			{
				LocalPool<PopupWidgetOption> options = <Initialize>c__AnonStorey.languageWidget.popup.options;
				List<PopupWidgetOption> inUse = options.inUse;
				for (int i = inUse.Count - 1; i >= 0; i--)
				{
					PopupWidgetOption popupWidgetOption = inUse[i];
					UserSettings.Language language = (UserSettings.Language)i;
					Font font = <Initialize>c__AnonStorey.$this.defaultLanguageFont;
					foreach (UserSettingsMenu.LanguageFontMap languageFontMap in <Initialize>c__AnonStorey.$this.fontMap)
					{
						if (languageFontMap.language == language)
						{
							font = languageFontMap.font;
						}
					}
					popupWidgetOption.localize.SecondaryTerm = " ";
					popupWidgetOption.localize.FinalSecondaryTerm = " ";
					popupWidgetOption.text.font = font;
				}
			};
			<Initialize>c__AnonStorey.resolution = this.AddResolutionWidget("SETTINGS/RESOLUTION", this.categoryDisplay, this.popupExample);
			<Initialize>c__AnonStorey.resolution.widget.SetIncrementCycling(false).SetUpdateAction(delegate
			{
				<Initialize>c__AnonStorey.$this.UpdateOccasionally(<Initialize>c__AnonStorey.resolution.widget, false);
			}).SetSuccessAudio(FabricID.settingChange).SetVisibilityCallback(() => Application.isEditor || Screen.fullScreen);
			UserSettingsMenu.<Initialize>c__AnonStorey0 <Initialize>c__AnonStorey2 = <Initialize>c__AnonStorey;
			string labelLocTerm = "SETTINGS/FULLSCREEN/NAME";
			if (UserSettingsMenu.<>f__mg$cache0 == null)
			{
				UserSettingsMenu.<>f__mg$cache0 = new Func<bool>(Screen.get_fullScreen);
			}
			<Initialize>c__AnonStorey2.fullscreen = this.AddBoolWidget(labelLocTerm, UserSettingsMenu.<>f__mg$cache0, new Func<bool, bool>(this.SetFullscreen), this.categoryDisplay);
			<Initialize>c__AnonStorey.fullscreen.onValueChanged += delegate(bool f)
			{
				if (f)
				{
					<Initialize>c__AnonStorey.resolution.UpdateList();
				}
			};
			<Initialize>c__AnonStorey.fullscreen.SetSuccessAudio(FabricID.settingChange);
			<Initialize>c__AnonStorey.fullscreen.SetUpdateAction(delegate
			{
				<Initialize>c__AnonStorey.$this.UpdateOccasionally(<Initialize>c__AnonStorey.fullscreen, false);
			});
			string[] values = new string[]
			{
				"SETTINGS/UI_SCALE_MODE/DESKTOP",
				"SETTINGS/UI_SCALE_MODE/TV"
			};
			this.AddMultiSelectWidget("SETTINGS/UI_SCALE_MODE/NAME", values, true, () => (int)Profile.userSettings.pcScaleMode, delegate(int value)
			{
				Profile.userSettings.pcScaleMode = (PlatformCanvasScaler.PCScaleMode)value;
				Profile.userSettings.ProcessUpdate(true);
				return true;
			}, this.categoryDisplay).SetSuccessAudio(FabricID.settingChange);
			IntWidget intWidget = this.AddIntWidget("SETTINGS/DISPLAY_SAFE_AREA", () => Mathf.RoundToInt(Profile.userSettings.displaySafeArea * 100f), delegate(int value)
			{
				Profile.userSettings.displaySafeArea = (float)value / 100f;
				Profile.userSettings.ProcessUpdate(true);
				return true;
			}, this.categoryDisplay).SetMinMax(90, 100);
			intWidget.percentDisplay = true;
			intWidget.onValueChanged += delegate(int x)
			{
				<Initialize>c__AnonStorey.$this.SafeAreaChanged();
			};
			intWidget.navigable.onFocusChanged += this.SafeAreaFocusChanged;
			intWidget.SetSuccessAudio(FabricID.settingChange);
			intWidget.gameObject.AddComponent<UIPointerReceiver>().onStateChanged += delegate(UIPointerReceiver.State s)
			{
				<Initialize>c__AnonStorey.$this.SafeAreaFocusChanged(s >= UIPointerReceiver.State.Hover);
			};
			string[] values2 = new string[]
			{
				"SETTINGS/ANTIALIASING/NONE",
				"SETTINGS/ANTIALIASING/MSAA",
				"SETTINGS/ANTIALIASING/SUPERSAMPLE"
			};
			this.AddMultiSelectWidget("SETTINGS/ANTIALIASING/NAME", values2, true, () => (int)Profile.userSettings.antiAliasingLevel.value, delegate(int value)
			{
				Profile.userSettings.antiAliasingLevel = (UserSettings.AntiAliasOption)value;
				Profile.userSettings.ProcessUpdate(true);
				return true;
			}, this.categoryGraphics).SetSuccessAudio(FabricID.settingChange);
			this.AddBoolWidget("SETTINGS/VSYNC", () => Profile.userSettings.vSync, delegate(bool value)
			{
				Profile.userSettings.vSync = value;
				Profile.userSettings.ProcessUpdate(true);
				return true;
			}, this.categoryGraphics).SetSuccessAudio(FabricID.settingChange);
			this.AddBoolWidget("SETTINGS/BLOOD", () => Profile.userSettings.showBlood, delegate(bool value)
			{
				Profile.userSettings.showBlood = value;
				Profile.userSettings.ProcessUpdate(true);
				return true;
			}, this.categoryGraphics).SetSuccessAudio(FabricID.settingChange);
			Widget widget = this.AddMultiSelectWidget("SETTINGS/MOUSE/BEHAVIOUR/NAME", new string[]
			{
				"SETTINGS/MOUSE/BEHAVIOUR/MOUSE_TWO_BUTTON",
				"SETTINGS/MOUSE/BEHAVIOUR/MOUSE_ONE_BUTTON",
				"SETTINGS/MOUSE/BEHAVIOUR/TOUCH"
			}, true, () => (int)Profile.userSettings.cursorBehaviour, delegate(int value)
			{
				Profile.userSettings.cursorBehaviour = (UserSettings.CursorBehaviour)value;
				Profile.userSettings.ProcessUpdate(true);
				return true;
			}, this.categoryControls).SetIncrementCycling(true).SetSuccessAudio(FabricID.settingChange);
			ButtonWidget buttonWidget = this.AddButton("CONTROLS/KEYBOARD/NAME", delegate()
			{
				ControlsKeyboardMenu.Open(<Initialize>c__AnonStorey.$this.islandUIManager);
				<Initialize>c__AnonStorey.$this.slideSide.SetActive(true);
				return true;
			}, this.categoryControls, this.wideButtonPrefab);
			ButtonWidget buttonWidget2 = this.AddButton(ControlsGamepadMenu.GetTitleLocTerm(), delegate()
			{
				ControlsGamepadMenu.Open(false);
				if (<Initialize>c__AnonStorey.$this.islandUIManager)
				{
					<Initialize>c__AnonStorey.$this.slideSide.SetActive(true);
				}
				return true;
			}, this.categoryControls, this.wideButtonPrefab);
			this.AddBoolWidget("SETTINGS/CAMERA_INVERT_X", () => Profile.userSettings.invertGamePadCameraX, delegate(bool value)
			{
				Profile.userSettings.invertGamePadCameraX = value;
				Profile.userSettings.ProcessUpdate(true);
				return true;
			}, this.categoryControls).SetSuccessAudio(FabricID.settingChange);
			this.AddBoolWidget("SETTINGS/CAMERA_INVERT_Y", () => Profile.userSettings.invertGamePadCameraY, delegate(bool value)
			{
				Profile.userSettings.invertGamePadCameraY = value;
				Profile.userSettings.ProcessUpdate(true);
				return true;
			}, this.categoryControls).SetSuccessAudio(FabricID.settingChange);
			this.AddIntWidget("SETTINGS/AUDIO/SFX_VOLUME", () => Profile.userSettings.sfxVolume, delegate(int value)
			{
				Profile.userSettings.sfxVolume = value;
				Profile.userSettings.ProcessUpdate(true);
				return true;
			}, this.categoryAudio).SetMinMax(0, 10).SetSuccessAudio(FabricID.settingChange);
			this.AddIntWidget("SETTINGS/AUDIO/MUSIC_VOLUME", () => Profile.userSettings.musicVolume, delegate(int value)
			{
				Profile.userSettings.musicVolume = value;
				Profile.userSettings.ProcessUpdate(true);
				return true;
			}, this.categoryAudio).SetMinMax(0, 10).SetSuccessAudio(FabricID.settingChange);
			this.AddButton("SETTINGS/RESTORE_DEFAULTS", new Func<bool>(this.ConfirmDefaults), this.confirmRestoreTransform, null);
			UnityEngine.Object.Destroy(this.popupExample.gameObject);
			this.stateRoot = new AgentStateRoot(4);
			this.slideSide = new AnimatedState("slideSide", this.stateRoot.rootState, false, false, this.slideAnim);
			TargetAnimator<float> anim = this.slideSide.anim;
			anim.setFunc = (Action<float>)Delegate.Combine(anim.setFunc, new Action<float>(delegate(float a)
			{
				float num = (!<Initialize>c__AnonStorey.$this.islandUIManager) ? -75f : 300f;
				Vector2 vector = new Vector2(a * num, 0f);
				<Initialize>c__AnonStorey.$this.positionOffsetTransform.offsetMin = vector;
				<Initialize>c__AnonStorey.$this.positionOffsetTransform.offsetMax = vector;
			}));
		}

		// Token: 0x06003B1F RID: 15135 RVA: 0x00106DD1 File Offset: 0x001051D1
		private void SafeAreaFocusChanged(bool hasFocus)
		{
			this.safeAreaVisibility.SetVisible(hasFocus, false);
			this.safeAreaFocus = hasFocus;
		}

		// Token: 0x06003B20 RID: 15136 RVA: 0x00106DE7 File Offset: 0x001051E7
		private void SafeAreaChanged()
		{
			if (!this.safeAreaFocus && Platform.Is(EPlatform.PC))
			{
				this.safeAreaVisibility.SetVisible(true, true);
				this.safeAreaVisibility.SetVisible(false, false);
			}
		}

		// Token: 0x06003B21 RID: 15137 RVA: 0x00106E1C File Offset: 0x0010521C
		private bool SetFullscreen(bool fullscreen)
		{
			Screen.fullScreen = fullscreen;
			if (fullscreen)
			{
				Screen.SetResolution(10000, 10000, fullscreen);
			}
			else
			{
				Resolution currentResolution = Screen.currentResolution;
				Resolution[] resolutions = Screen.resolutions;
				Resolution resolution = default(Resolution);
				Resolution resolution2 = default(Resolution);
				resolution = resolutions.Last<Resolution>();
				int num = 0;
				resolution2.height = num;
				resolution2.width = num;
				if (currentResolution.height >= resolution.height && currentResolution.width >= resolution.width)
				{
					foreach (Resolution resolution3 in resolutions)
					{
						bool flag = resolution3.width >= resolution2.width && resolution3.height >= resolution2.height && resolution3.width < resolution.width && resolution3.height < resolution.height - 150;
						if (flag)
						{
							resolution2 = resolution3;
						}
					}
					if (resolution2.width > 0 && resolution2.height > 0)
					{
						Screen.SetResolution(resolution2.width, resolution2.height, fullscreen);
					}
				}
			}
			return true;
		}

		// Token: 0x06003B22 RID: 15138 RVA: 0x00106F61 File Offset: 0x00105361
		protected override void OnEnable()
		{
			base.OnEnable();
			base.transform.ForceChildLayoutUpdates(true);
			base.transform.MarkChildLayoutsForRebuild(true);
		}

		// Token: 0x06003B23 RID: 15139 RVA: 0x00106F81 File Offset: 0x00105381
		public override void ForceUpdateAll()
		{
			base.ForceUpdateAll();
			GeneratedMenu.ForceUpdateAll(this.categoryDisplay);
			GeneratedMenu.ForceUpdateAll(this.categoryGraphics);
			GeneratedMenu.ForceUpdateAll(this.categoryAudio);
			GeneratedMenu.ForceUpdateAll(this.confirmRestoreTransform);
		}

		// Token: 0x06003B24 RID: 15140 RVA: 0x00106FB8 File Offset: 0x001053B8
		protected override void ClearWidgets()
		{
			base.ClearWidgets();
			this.confirmRestoreTransform.DestroyChildren();
			this.categoryControls.DestroyChildren();
			this.categoryDisplay.DestroyChildren();
			this.categoryGraphics.DestroyChildren();
			this.categoryAudio.DestroyChildren();
			this.confirmRestoreTransform.DestroyChildren();
		}

		// Token: 0x06003B25 RID: 15141 RVA: 0x00107010 File Offset: 0x00105410
		private PopupWidget AddPopupWidget(string labelLocTerm, string[] values, bool wantsLocalizedValues, Func<int> getAction, Func<int, bool> setAction, Transform overrideParentTransform = null, Transform popupTransformOverride = null)
		{
			Transform transform = (!overrideParentTransform) ? this.widgetParent : overrideParentTransform;
			PopupWidget popupWidget = transform.gameObject.InstantiateChild(this.popupWidgetPrefab, null).SetValues(values, wantsLocalizedValues).Initialize(labelLocTerm, getAction, setAction) as PopupWidget;
			base.slaveUpdates += popupWidget.SlaveUpdate;
			Transform transform2 = (!popupTransformOverride) ? this.popupExample.transform.parent : popupTransformOverride;
			PopupWidget_Popup popupWidget_Popup = transform2.gameObject.InstantiateChild(this.popupExample, null);
			popupWidget.SetPopup(popupWidget_Popup);
			popupWidget_Popup.gameObject.SetActive(false);
			return popupWidget;
		}

		// Token: 0x06003B26 RID: 15142 RVA: 0x001070C0 File Offset: 0x001054C0
		private ResolutionSelectWidgetHelper AddResolutionWidget(string labelTerm, Transform parentTransform, PopupWidget_Popup popupExample)
		{
			PopupWidget popupWidget = parentTransform.gameObject.InstantiateChild(this.popupWidgetPrefab, null);
			ResolutionSelectWidgetHelper resolutionSelectWidgetHelper = popupWidget.gameObject.AddComponent<ResolutionSelectWidgetHelper>();
			resolutionSelectWidgetHelper.Setup(labelTerm);
			PopupWidget_Popup popupWidget_Popup = popupExample.transform.parent.gameObject.InstantiateChild(popupExample, null);
			popupWidget.SetPopup(popupWidget_Popup);
			popupWidget_Popup.gameObject.SetActive(false);
			base.slaveUpdates += popupWidget.SlaveUpdate;
			return resolutionSelectWidgetHelper;
		}

		// Token: 0x06003B27 RID: 15143 RVA: 0x00107133 File Offset: 0x00105533
		protected override void Update()
		{
			base.Update();
			this.stateRoot.Update();
			if (this.openTime.framesSince <= 1)
			{
				base.transform.MarkChildLayoutsForRebuild(false);
			}
		}

		// Token: 0x06003B28 RID: 15144 RVA: 0x00107163 File Offset: 0x00105563
		public void HandleClickOff()
		{
			this.HandleBackButton();
		}

		// Token: 0x040028FD RID: 10493
		[SerializeField]
		private RectTransform container;

		// Token: 0x040028FE RID: 10494
		[SerializeField]
		private RectTransform islandViewport;

		// Token: 0x040028FF RID: 10495
		[SerializeField]
		private RectTransform safeAreaIndicator;

		// Token: 0x04002900 RID: 10496
		private IUIVisibility safeAreaVisibility;

		// Token: 0x04002901 RID: 10497
		private bool safeAreaFocus;

		// Token: 0x04002902 RID: 10498
		[SerializeField]
		private PopupWidget popupWidgetPrefab;

		// Token: 0x04002903 RID: 10499
		[SerializeField]
		private PopupWidget_Popup popupExample;

		// Token: 0x04002904 RID: 10500
		[SerializeField]
		private ButtonWidget wideButtonPrefab;

		// Token: 0x04002905 RID: 10501
		[SerializeField]
		private Image background;

		// Token: 0x04002906 RID: 10502
		[SerializeField]
		private Transform categoryDisplayContainer;

		// Token: 0x04002907 RID: 10503
		[SerializeField]
		private Transform categoryControlsContainer;

		// Token: 0x04002908 RID: 10504
		[SerializeField]
		private Transform categoryControls;

		// Token: 0x04002909 RID: 10505
		[SerializeField]
		private Transform categoryDisplay;

		// Token: 0x0400290A RID: 10506
		[SerializeField]
		private Transform categoryGraphics;

		// Token: 0x0400290B RID: 10507
		[SerializeField]
		private Transform categoryAudio;

		// Token: 0x0400290C RID: 10508
		[SerializeField]
		private Transform confirmRestoreTransform;

		// Token: 0x0400290D RID: 10509
		[SerializeField]
		private RectTransform positionOffsetTransform;

		// Token: 0x0400290E RID: 10510
		private static UserSettingsMenu _instance;

		// Token: 0x0400290F RID: 10511
		private FabricEventReference resetAudio = "UI/Menu/ResetSettings";

		// Token: 0x04002910 RID: 10512
		private List<VerticalLayoutGroup> categoryLayouts = new List<VerticalLayoutGroup>(4);

		// Token: 0x04002911 RID: 10513
		[SerializeField]
		private LerpTowards slideAnim = new LerpTowards(14f, 0.2f);

		// Token: 0x04002912 RID: 10514
		[SerializeField]
		private AgentStateRoot stateRoot;

		// Token: 0x04002913 RID: 10515
		private AnimatedState slideSide;

		// Token: 0x04002914 RID: 10516
		[SerializeField]
		private Font defaultLanguageFont;

		// Token: 0x04002915 RID: 10517
		[SerializeField]
		private UserSettingsMenu.LanguageFontMap[] fontMap = new UserSettingsMenu.LanguageFontMap[0];

		// Token: 0x04002916 RID: 10518
		private float iPhonePadding;

		// Token: 0x04002917 RID: 10519
		private bool centered;

		// Token: 0x04002918 RID: 10520
		private WeakReference<IslandUIManager> islandUIManager = new WeakReference<IslandUIManager>(null);

		// Token: 0x04002919 RID: 10521
		private IUIVisibility visibility;

		// Token: 0x0400291A RID: 10522
		[CompilerGenerated]
		private static Func<bool> <>f__mg$cache0;

		// Token: 0x020008C1 RID: 2241
		[Serializable]
		private struct LanguageFontMap
		{
			// Token: 0x04002932 RID: 10546
			public UserSettings.Language language;

			// Token: 0x04002933 RID: 10547
			public Font font;
		}
	}
}
