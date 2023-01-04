using System;
using Rewired;
using RTM.Input;
using RTM.UISystem;
using UnityEngine;
using UnityEngine.UI;
using Voxels.TowerDefense.ScriptAnimations;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x020008FA RID: 2298
	internal class NewGameInventoryPrompt : MonoBehaviour, IGameSetup
	{
		// Token: 0x06003D26 RID: 15654 RVA: 0x001116AC File Offset: 0x0010FAAC
		void IGameSetup.OnGameAwake()
		{
			LerpTowards targetAnimFuncs = new LerpTowards(10f, 5.5f);
			this.codexPrompt = new AnimatedState("Codex Prompt Visible", this.stateRoot.rootState, false, true, targetAnimFuncs);
			this.dlcPrompt = new AnimatedState("DLC Prompt Visible", this.stateRoot.rootState, false, true, targetAnimFuncs);
			this.dlcButton = new AnimatedState("DLC Button", this.stateRoot.rootState, false, true, targetAnimFuncs);
			this.dlcWarning = new AnimatedState("Not Owned DLC", this.stateRoot.rootState, false, true, targetAnimFuncs);
			this.SetupPrompt(this.codexPrompt, this.codexPromptObject, EUIPadAction.Submit);
			this.SetupPrompt(this.dlcPrompt, this.dlcPromptObject, EUIPadAction.Tertiary);
			this.SetupButton(this.dlcButton, this.dlcButtonObject, delegate
			{
				bool flag = this.ownerMenu.OpenDLCStore();
				this.dlcButton.active = !flag;
				return flag;
			});
			this.Setup(this.dlcWarning, this.dlcWarningObject);
			InputHelpers.onControllerTypeChanged += delegate(ControllerType c)
			{
				this.dlcWarning.active = false;
			};
			UpgradeCarousel[] componentsInChildren = this.uiRoot.GetComponentsInChildren<UpgradeCarousel>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				NewGameInventoryPrompt.<Voxels_TowerDefense_IGameSetup_OnGameAwake>c__AnonStorey1 <Voxels_TowerDefense_IGameSetup_OnGameAwake>c__AnonStorey = new NewGameInventoryPrompt.<Voxels_TowerDefense_IGameSetup_OnGameAwake>c__AnonStorey1();
				<Voxels_TowerDefense_IGameSetup_OnGameAwake>c__AnonStorey.carousel = componentsInChildren[i];
				<Voxels_TowerDefense_IGameSetup_OnGameAwake>c__AnonStorey.$this = this;
				IUINavigable navigable = <Voxels_TowerDefense_IGameSetup_OnGameAwake>c__AnonStorey.carousel.GetComponent<IUINavigable>();
				Action visibleFunc = delegate()
				{
					<Voxels_TowerDefense_IGameSetup_OnGameAwake>c__AnonStorey.$this.codexPrompt.active = (navigable.hasFocus && <Voxels_TowerDefense_IGameSetup_OnGameAwake>c__AnonStorey.carousel.selectedUpgrade && InputHelpers.ControllerTypeIs(ControllerType.Joystick));
				};
				navigable.onFocusChanged += delegate(bool f)
				{
					visibleFunc();
				};
				<Voxels_TowerDefense_IGameSetup_OnGameAwake>c__AnonStorey.carousel.onSelectedUpgradeChanged += delegate(HeroUpgradeDefinition u)
				{
					visibleFunc();
				};
			}
			NewGameHeroSelector[] componentsInChildren2 = this.uiRoot.GetComponentsInChildren<NewGameHeroSelector>(true);
			for (int j = 0; j < componentsInChildren2.Length; j++)
			{
				NewGameHeroSelector newGameHeroSelector = componentsInChildren2[j];
				BoolWidget deluxeDLCWidget = newGameHeroSelector.deluxeDLCWidget;
				UINavigable navigable = deluxeDLCWidget.GetComponent<UINavigable>();
				UIClickable component = deluxeDLCWidget.GetComponent<UIClickable>();
				Action focusVisible = delegate()
				{
					bool flag = navigable.hasFocus && this.ownerMenu.showDeluxe && !this.ownerMenu.hasDeluxe;
					this.dlcPrompt.active = (flag && this.ownerMenu.canShowStore && InputHelpers.ControllerTypeIs(ControllerType.Joystick));
					if (!flag || this.ownerMenu.canShowStore)
					{
						this.dlcWarning.active = false;
					}
				};
				navigable.onFocusChanged += delegate(bool f)
				{
					focusVisible();
				};
				component.onClickFailed += delegate()
				{
					bool flag = this.ownerMenu.showDeluxe && !this.ownerMenu.hasDeluxe;
					bool flag2 = flag && this.ownerMenu.canShowStore;
					bool active = flag && !this.ownerMenu.canShowStore;
					this.dlcButton.active = (flag2 && InputHelpers.ControllerTypeIs(ControllerType.Mouse));
					this.dlcPrompt.active = false;
					this.dlcPrompt.ForceToTarget();
					this.dlcPrompt.active = (flag2 && InputHelpers.ControllerTypeIs(ControllerType.Joystick));
					this.dlcWarning.active = false;
					this.dlcWarning.ForceToTarget();
					this.dlcWarning.active = active;
				};
			}
		}

		// Token: 0x06003D27 RID: 15655 RVA: 0x001118E4 File Offset: 0x0010FCE4
		private void SetupPrompt(AnimatedState state, GameObject go, EUIPadAction action)
		{
			this.Setup(state, go);
			Image buttonIcon = go.GetComponentInChildren<Image>(true);
			Action b = delegate()
			{
				buttonIcon.sprite = Singleton<UIManager>.instance.GetActionIcon(action);
			};
			AnimatedState state2 = state;
			state2.OnActivate = (Action)Delegate.Combine(state2.OnActivate, b);
			InputHelpers.onControllerTypeChanged += delegate(ControllerType c)
			{
				if (c != ControllerType.Joystick)
				{
					state.active = false;
				}
			};
		}

		// Token: 0x06003D28 RID: 15656 RVA: 0x0011195C File Offset: 0x0010FD5C
		private void SetupButton(AnimatedState state, GameObject go, Func<bool> buttonAction)
		{
			this.Setup(state, go);
			ButtonWidget component = go.GetComponent<ButtonWidget>();
			component.Initialize("SETTINGS/DLC/SHOW_IN_STORE", buttonAction);
			InputHelpers.onControllerTypeChanged += delegate(ControllerType ct)
			{
				if (ct != ControllerType.Mouse)
				{
					state.active = false;
				}
			};
		}

		// Token: 0x06003D29 RID: 15657 RVA: 0x001119A8 File Offset: 0x0010FDA8
		private void Setup(AnimatedState state, GameObject go)
		{
			CanvasGroup cg = go.GetComponentInChildren<CanvasGroup>(true);
			Action<float> setFunc = delegate(float a)
			{
				float value = Mathf.Lerp(0.95f, 1f, a);
				cg.alpha = a;
				cg.transform.localScale = cg.transform.localScale.SetX(value).SetY(value);
			};
			state.Subscribe(delegate(bool b)
			{
				go.SetActive(b);
			}, setFunc);
			AnimatedState state2 = state;
			state2.OnChange = (Action<bool>)Delegate.Combine(state2.OnChange, new Action<bool>(delegate(bool a)
			{
				cg.blocksRaycasts = a;
			}));
			state.OnUpdate += delegate()
			{
				if (!InputHelpers.ControllerTypeIs(ControllerType.Joystick) && state.timeSinceActivation > 4f)
				{
					state.active = false;
				}
			};
		}

		// Token: 0x06003D2A RID: 15658 RVA: 0x00111A3E File Offset: 0x0010FE3E
		private void Update()
		{
			this.stateRoot.Update();
		}

		// Token: 0x06003D2B RID: 15659 RVA: 0x00111A4C File Offset: 0x0010FE4C
		private void OnDisable()
		{
			this.codexPrompt.SetActive(false);
			this.codexPrompt.ForceToTarget();
			this.dlcPrompt.SetActive(false);
			this.dlcPrompt.ForceToTarget();
			this.dlcButton.SetActive(false);
			this.dlcButton.ForceToTarget();
		}

		// Token: 0x04002AA3 RID: 10915
		[SerializeField]
		private Transform uiRoot;

		// Token: 0x04002AA4 RID: 10916
		[SerializeField]
		private NewGameOptionsPopup ownerMenu;

		// Token: 0x04002AA5 RID: 10917
		[SerializeField]
		private GameObject codexPromptObject;

		// Token: 0x04002AA6 RID: 10918
		[SerializeField]
		private GameObject dlcPromptObject;

		// Token: 0x04002AA7 RID: 10919
		[SerializeField]
		private GameObject dlcButtonObject;

		// Token: 0x04002AA8 RID: 10920
		[SerializeField]
		private GameObject dlcWarningObject;

		// Token: 0x04002AA9 RID: 10921
		[SerializeField]
		private AgentStateRoot stateRoot = new AgentStateRoot(4);

		// Token: 0x04002AAA RID: 10922
		private AnimatedState codexPrompt;

		// Token: 0x04002AAB RID: 10923
		private AnimatedState dlcPrompt;

		// Token: 0x04002AAC RID: 10924
		private AnimatedState dlcButton;

		// Token: 0x04002AAD RID: 10925
		private AnimatedState dlcWarning;
	}
}
