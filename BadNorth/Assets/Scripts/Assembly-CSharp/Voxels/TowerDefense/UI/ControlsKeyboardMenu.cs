using System;
using System.Collections.Generic;
using System.Diagnostics;
using Rewired;
using RTM.Input;
using RTM.Utilities;
using UnityEngine;
using Voxels.TowerDefense.ScriptAnimations;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x020008B8 RID: 2232
	internal class ControlsKeyboardMenu : GeneratedMenu
	{
		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x06003AA3 RID: 15011 RVA: 0x00105142 File Offset: 0x00103542
		private bool listening
		{
			get
			{
				return this.clickBlocker.activeInHierarchy;
			}
		}

		// Token: 0x06003AA4 RID: 15012 RVA: 0x0010514F File Offset: 0x0010354F
		public static void Open(IslandUIManager ium)
		{
			ControlsKeyboardMenu.instance.OpenMenu(ium);
		}

		// Token: 0x06003AA5 RID: 15013 RVA: 0x0010515C File Offset: 0x0010355C
		private void OpenMenu(IslandUIManager ium)
		{
			this.OpenMenu();
			this.islandUIManager = ium;
			if (this.islandUIManager)
			{
				this.islandUIManager.Target.islandViewfinder.Push(this.islandViewPort, null);
				this.container.pivot = this.container.pivot.SetX(1f);
			}
			else
			{
				this.container.pivot = this.container.pivot.SetX(0.5f);
			}
		}

		// Token: 0x06003AA6 RID: 15014 RVA: 0x001051EC File Offset: 0x001035EC
		public override void OpenMenu()
		{
			base.OpenMenu();
			using ("Force Update widgets")
			{
				this.ForceUpdateAll();
			}
			this.somethingChanged = false;
		}

		// Token: 0x06003AA7 RID: 15015 RVA: 0x0010523C File Offset: 0x0010363C
		protected override void OnGainedFocus()
		{
			this.slideSide.SetActive(true);
			base.OnGainedFocus();
		}

		// Token: 0x06003AA8 RID: 15016 RVA: 0x00105254 File Offset: 0x00103654
		public override void CloseMenu()
		{
			base.CloseMenu();
			if (this.somethingChanged)
			{
				Profile.SaveUserControlMapping(0);
			}
			this.somethingChanged = false;
			this.slideSide.SetActive(false);
			if (this.islandUIManager)
			{
				this.islandUIManager.Target.islandViewfinder.Remove(this.islandViewPort, null);
			}
		}

		// Token: 0x06003AA9 RID: 15017 RVA: 0x001052B8 File Offset: 0x001036B8
		public override void ForceUpdateAll()
		{
			GeneratedMenu.ForceUpdateAll(this.cameraActionContainer);
			GeneratedMenu.ForceUpdateAll(this.timeActionContainer);
			GeneratedMenu.ForceUpdateAll(this.squadActionContainer);
			GeneratedMenu.ForceUpdateAll(this.abilityActionContainer);
		}

		// Token: 0x06003AAA RID: 15018 RVA: 0x001052E8 File Offset: 0x001036E8
		protected override void Initialize()
		{
			ControlsKeyboardMenu.instance = this;
			base.Initialize();
			this.onStartListening = new Action(this.OnStartListening);
			this.onStopListening = new Action(this.OnStopListening);
			Player player = ReInput.players.GetPlayer(0);
			this.CreateWidgets(player, this.cameraActionContainer, this.cameraActions);
			this.CreateWidgets(player, this.timeActionContainer, this.timeActions);
			this.CreateWidgets(player, this.squadActionContainer, this.squadActions);
			this.CreateWidgets(player, this.abilityActionContainer, this.abilityActions);
			this.AddButton("SETTINGS/RESTORE_DEFAULTS", new Func<bool>(this.ConfirmDefaults), this.restoreContainer, null);
			this.AddBoolWidget("CONTROLS/KEYBOARD/CAM_ROTATE_SNAP", () => !Profile.userSettings.suppressCameraRotateSnap, delegate(bool value)
			{
				Profile.userSettings.suppressCameraRotateSnap = !value;
				Profile.userSettings.ProcessUpdate(true);
				return true;
			}, this.cameraActionContainer).SetSuccessAudio(FabricID.settingChange);
			this.AddBoolWidget("CONTROLS/KEYBOARD/ZOOM_SNAP", () => !Profile.userSettings.suppressCameraZoomSnap, delegate(bool value)
			{
				Profile.userSettings.suppressCameraZoomSnap = !value;
				Profile.userSettings.ProcessUpdate(true);
				return true;
			}, this.cameraActionContainer).SetSuccessAudio(FabricID.settingChange);
			this.clickBlocker.SetActive(false);
			CanvasGroup canvasGroup = base.GetComponent<CanvasGroup>();
			this.stateRoot = new AgentStateRoot(4);
			this.slideSide = new AnimatedState("slideSide", this.stateRoot.rootState, false, false, this.slideAnim);
			TargetAnimator<float> anim = this.slideSide.anim;
			anim.setFunc = (Action<float>)Delegate.Combine(anim.setFunc, new Action<float>(delegate(float a)
			{
				canvasGroup.alpha = a;
				float num = (!this.islandUIManager) ? 40f : -20f;
				Vector2 vector = new Vector2(a * num, 0f);
				this.positionOffsetTransform.offsetMin = vector;
				this.positionOffsetTransform.offsetMax = vector;
			}));
			AnimatedState animatedState = this.slideSide;
			animatedState.OnChange = (Action<bool>)Delegate.Combine(animatedState.OnChange, new Action<bool>(delegate(bool x)
			{
				canvasGroup.blocksRaycasts = x;
				canvasGroup.interactable = x;
				if (x)
				{
					this.gameObject.SetActive(true);
				}
			}));
			TargetAnimator<float> anim2 = this.slideSide.anim;
			anim2.onDone = (Action)Delegate.Combine(anim2.onDone, new Action(delegate()
			{
				if (this.slideSide.value == 0f)
				{
					this.gameObject.SetActive(false);
				}
			}));
			base.gameObject.SetActive(false);
		}

		// Token: 0x06003AAB RID: 15019 RVA: 0x00105527 File Offset: 0x00103927
		protected override void ClearWidgets()
		{
			base.ClearWidgets();
			this.cameraActionContainer.DestroyChildren();
			this.timeActionContainer.DestroyChildren();
			this.squadActionContainer.DestroyChildren();
			this.abilityActionContainer.DestroyChildren();
			this.restoreContainer.DestroyChildren();
		}

		// Token: 0x06003AAC RID: 15020 RVA: 0x00105568 File Offset: 0x00103968
		private void CreateWidgets(Player player, RectTransform container, IEnumerable<RewiredActionUIReference> actions)
		{
			foreach (RewiredActionUIReference rewiredActionUIReference in actions)
			{
				ActionElementMap actionElementMap = RewiredRemapUtils.GetActionElementMap(player, rewiredActionUIReference.actionId, rewiredActionUIReference.pole);
				this.AddControlRemapper(rewiredActionUIReference.locId, rewiredActionUIReference.actionNameIdx, player, actionElementMap, container);
			}
		}

		// Token: 0x06003AAD RID: 15021 RVA: 0x001055E4 File Offset: 0x001039E4
		public virtual ControlRemapWidget AddControlRemapper(string labelLocTerm, int labelNum, Player player, ActionElementMap aem, RectTransform parentTransform)
		{
			ControlRemapWidget controlRemapWidget = parentTransform.gameObject.InstantiateChild(this.controlRemapWidget, null).Initialize(labelLocTerm, labelNum, player, aem);
			controlRemapWidget.onStartListening += this.onStartListening;
			controlRemapWidget.onStopListening += this.onStopListening;
			controlRemapWidget.onMappingChanged += this.OnAnyMappingChanged;
			base.slaveUpdates += controlRemapWidget.SlaveUpdate;
			return controlRemapWidget;
		}

		// Token: 0x06003AAE RID: 15022 RVA: 0x0010564D File Offset: 0x00103A4D
		public void OnAnyMappingChanged()
		{
			this.ForceUpdateAll();
			this.somethingChanged = true;
		}

		// Token: 0x06003AAF RID: 15023 RVA: 0x0010565C File Offset: 0x00103A5C
		[Conditional("UNITY_EDITOR")]
		private void DebugPrintActions(string title, IEnumerable<RewiredActionUIReference> actions)
		{
		}

		// Token: 0x06003AB0 RID: 15024 RVA: 0x0010565E File Offset: 0x00103A5E
		private void OnStartListening()
		{
			this.blockingInput = true;
			this.clickBlocker.SetActive(true);
		}

		// Token: 0x06003AB1 RID: 15025 RVA: 0x00105673 File Offset: 0x00103A73
		private void OnStopListening()
		{
			this.blockingInput = false;
			this.clickBlocker.SetActive(false);
		}

		// Token: 0x06003AB2 RID: 15026 RVA: 0x00105688 File Offset: 0x00103A88
		private bool ConfirmDefaults()
		{
			ModalOverlay.GetInstance().Initialize("SETTINGS/RESTORE_WARNING/TITLE", "SETTINGS/RESTORE_WARNING/MESSAGE", true).AddOKButton(new Func<bool>(this.ResetToDefaults)).SetSuccessAudio(this.resetAudio.name);
			return true;
		}

		// Token: 0x06003AB3 RID: 15027 RVA: 0x001056C8 File Offset: 0x00103AC8
		private bool ResetToDefaults()
		{
			Player player = ReInput.players.GetPlayer(0);
			RewiredRemapUtils.SetDefaults(player);
			Profile.DeletedUserControlMappings(player.id);
			this.ForceUpdateAll();
			return true;
		}

		// Token: 0x06003AB4 RID: 15028 RVA: 0x001056F9 File Offset: 0x00103AF9
		protected override void Update()
		{
			base.Update();
			this.stateRoot.Update();
		}

		// Token: 0x06003AB5 RID: 15029 RVA: 0x0010570C File Offset: 0x00103B0C
		public void HandleClickOff()
		{
			this.HandleBackButton();
		}

		// Token: 0x040028B5 RID: 10421
		[Header("Controls Remapping")]
		[SerializeField]
		private ControlRemapWidget controlRemapWidget;

		// Token: 0x040028B6 RID: 10422
		[SerializeField]
		private RectTransform cameraActionContainer;

		// Token: 0x040028B7 RID: 10423
		[SerializeField]
		private RectTransform timeActionContainer;

		// Token: 0x040028B8 RID: 10424
		[SerializeField]
		private RectTransform squadActionContainer;

		// Token: 0x040028B9 RID: 10425
		[SerializeField]
		private RectTransform abilityActionContainer;

		// Token: 0x040028BA RID: 10426
		[SerializeField]
		private RectTransform restoreContainer;

		// Token: 0x040028BB RID: 10427
		[SerializeField]
		private RectTransform islandViewPort;

		// Token: 0x040028BC RID: 10428
		[SerializeField]
		private RectTransform container;

		// Token: 0x040028BD RID: 10429
		[SerializeField]
		private RectTransform positionOffsetTransform;

		// Token: 0x040028BE RID: 10430
		[SerializeField]
		private GameObject clickBlocker;

		// Token: 0x040028BF RID: 10431
		[Header("Actions")]
		[SerializeField]
		private RewiredActionUIReference[] cameraActions;

		// Token: 0x040028C0 RID: 10432
		[SerializeField]
		private RewiredActionUIReference[] timeActions;

		// Token: 0x040028C1 RID: 10433
		[SerializeField]
		private RewiredActionUIReference[] squadActions;

		// Token: 0x040028C2 RID: 10434
		[SerializeField]
		private RewiredActionUIReference[] abilityActions;

		// Token: 0x040028C3 RID: 10435
		private static ControlsKeyboardMenu instance;

		// Token: 0x040028C4 RID: 10436
		private FabricEventReference resetAudio = "UI/Menu/ResetSettings";

		// Token: 0x040028C5 RID: 10437
		private Action onStartListening;

		// Token: 0x040028C6 RID: 10438
		private Action onStopListening;

		// Token: 0x040028C7 RID: 10439
		private bool somethingChanged;

		// Token: 0x040028C8 RID: 10440
		private WeakReference<IslandUIManager> islandUIManager = new WeakReference<IslandUIManager>(null);

		// Token: 0x040028C9 RID: 10441
		[Header("Animations")]
		[SerializeField]
		private LerpTowards slideAnim = new LerpTowards(14f, 0.2f);

		// Token: 0x040028CA RID: 10442
		[SerializeField]
		private AgentStateRoot stateRoot;

		// Token: 0x040028CB RID: 10443
		private AnimatedState slideSide;
	}
}
