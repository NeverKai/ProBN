using System;
using Rewired;
using RTM.Input;
using RTM.UISystem;
using UnityEngine;
using UnityEngine.UI;
using Voxels.TowerDefense.ScriptAnimations;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x020008FD RID: 2301
	internal class NewGamePanelAnimator : MonoBehaviour, IGameSetup
	{
		// Token: 0x06003D54 RID: 15700 RVA: 0x00112908 File Offset: 0x00110D08
		void IGameSetup.OnGameAwake()
		{
			this.up = new AnimatedState("Up", this.stateRoot.rootState, false, false, this.upAnimFuncs);
			TargetAnimator<float> anim = this.up.anim;
			anim.setFunc = (Action<float>)Delegate.Combine(anim.setFunc, new Action<float>(delegate(float a)
			{
				this.transform.localPosition = this.transform.localPosition.SetY(this.travel * a);
			}));
			if (this.arrowGraphic)
			{
				this.arrow = new AnimatedState("Arrow", this.stateRoot.rootState, false, false, this.upAnimFuncs);
				Transform trans = this.arrowGraphic.transform;
				Vector3 defaultArrowPos = trans.localPosition;
				CanvasGroup cg = this.arrowGraphic.GetComponent<CanvasGroup>();
				Action<float> b = delegate(float x)
				{
					float current = this.up.anim.current;
					float current2 = this.arrow.anim.current;
					float num = current * current2;
					trans.localPosition = trans.localPosition.SetY(defaultArrowPos.y - this.travel + (1f - num) * (this.travel * current + 20f * current2));
					cg.alpha = num;
					trans.localScale = new Vector3(0.2f + 0.8f * num, num * num, 1f);
				};
				TargetAnimator<float> anim2 = this.up.anim;
				anim2.setFunc = (Action<float>)Delegate.Combine(anim2.setFunc, b);
				TargetAnimator<float> anim3 = this.arrow.anim;
				anim3.setFunc = (Action<float>)Delegate.Combine(anim3.setFunc, b);
				Action arrowController = delegate()
				{
					bool active = InputHelpers.ControllerTypeIs(ControllerType.Joystick) || Platform.Is(EPlatform.Switch);
					this.arrow.SetActive(active);
				};
				InputHelpers.onControllerTypeChanged += delegate(ControllerType x)
				{
					arrowController();
				};
				Platform.onPlatformUpdated += arrowController;
				arrowController();
				this.arrow.ForceToTarget();
			}
			UIMenu menu = base.GetComponent<UIMenu>();
			Action upController = delegate()
			{
				bool flag = InputHelpers.ControllerTypeIs(ControllerType.Joystick) || Platform.Is(EPlatform.Switch);
				this.up.SetActive(flag && menu.isFocussed);
			};
			menu.onFocusChanged += delegate(bool x)
			{
				upController();
			};
			InputHelpers.onControllerTypeChanged += delegate(ControllerType x)
			{
				upController();
			};
			Platform.onPlatformUpdated += upController;
			upController();
		}

		// Token: 0x06003D55 RID: 15701 RVA: 0x00112AD3 File Offset: 0x00110ED3
		private void OnDisable()
		{
			this.up.SetActive(false);
			this.up.ForceToTarget();
		}

		// Token: 0x06003D56 RID: 15702 RVA: 0x00112AED File Offset: 0x00110EED
		private void Update()
		{
			this.stateRoot.Update();
		}

		// Token: 0x04002ACB RID: 10955
		[SerializeField]
		private float travel = 25f;

		// Token: 0x04002ACC RID: 10956
		[SerializeField]
		private LerpTowards upAnimFuncs = new LerpTowards(14f, 0.2f);

		// Token: 0x04002ACD RID: 10957
		[SerializeField]
		private Graphic arrowGraphic;

		// Token: 0x04002ACE RID: 10958
		[SerializeField]
		private AgentStateRoot stateRoot = new AgentStateRoot(4);

		// Token: 0x04002ACF RID: 10959
		private AnimatedState up;

		// Token: 0x04002AD0 RID: 10960
		private AnimatedState arrow;
	}
}
