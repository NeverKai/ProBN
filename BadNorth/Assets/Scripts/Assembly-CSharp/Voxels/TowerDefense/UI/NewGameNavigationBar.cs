using System;
using Rewired;
using RTM.Input;
using RTM.UISystem;
using UnityEngine;
using UnityEngine.UI;
using Voxels.TowerDefense.ScriptAnimations;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x020008FB RID: 2299
	internal class NewGameNavigationBar : MonoBehaviour, IGameSetup
	{
		// Token: 0x06003D2F RID: 15663 RVA: 0x00111E48 File Offset: 0x00110248
		void IGameSetup.OnGameAwake()
		{
			this.visible = new AnimatedState("Visible", this.stateRoot.rootState, false, false, this.anims);
			Action<Image, float> visibleFunc = delegate(Image image, float a)
			{
				float value = Mathf.Lerp(0.6f, 1f, a);
				image.color = image.color.SetA(a);
				image.transform.localScale = image.transform.localScale.SetX(value).SetY(value);
			};
			TargetAnimator<float> anim = this.visible.anim;
			anim.setFunc = (Action<float>)Delegate.Combine(anim.setFunc, new Action<float>(delegate(float a)
			{
				visibleFunc(this.leftTab, a);
			}));
			TargetAnimator<float> anim2 = this.visible.anim;
			anim2.setFunc = (Action<float>)Delegate.Combine(anim2.setFunc, new Action<float>(delegate(float a)
			{
				visibleFunc(this.rightTab, a);
			}));
			Action updateVisibility = delegate()
			{
				bool flag = InputHelpers.ControllerTypeIs(ControllerType.Joystick) || Platform.Is(EPlatform.Switch);
				this.visible.SetActive(flag);
				if (flag)
				{
					this.leftTab.sprite = Singleton<UIManager>.instance.GetActionIcon(EUIPadAction.TabLeft);
					this.rightTab.sprite = Singleton<UIManager>.instance.GetActionIcon(EUIPadAction.TabRight);
				}
			};
			InputHelpers.onControllerTypeChanged += delegate(ControllerType x)
			{
				updateVisibility();
			};
			Platform.onPlatformUpdated += updateVisibility;
			updateVisibility();
			this.visible.ForceToTarget();
		}

		// Token: 0x06003D30 RID: 15664 RVA: 0x00111F44 File Offset: 0x00110344
		private void Update()
		{
			this.stateRoot.Update();
		}

		// Token: 0x04002AAE RID: 10926
		[SerializeField]
		private Image leftTab;

		// Token: 0x04002AAF RID: 10927
		[SerializeField]
		private Image rightTab;

		// Token: 0x04002AB0 RID: 10928
		[SerializeField]
		private LerpTowards anims = new LerpTowards(16f, 4f);

		// Token: 0x04002AB1 RID: 10929
		[SerializeField]
		private AgentStateRoot stateRoot = new AgentStateRoot(4);

		// Token: 0x04002AB2 RID: 10930
		private AnimatedState visible;
	}
}
