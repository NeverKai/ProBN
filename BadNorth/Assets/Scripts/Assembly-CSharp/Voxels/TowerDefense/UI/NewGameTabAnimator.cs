using System;
using Rewired;
using RTM.Input;
using RTM.UISystem;
using UnityEngine;
using UnityEngine.UI;
using Voxels.TowerDefense.ScriptAnimations;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x020008FE RID: 2302
	internal class NewGameTabAnimator : MonoBehaviour
	{
		// Token: 0x06003D58 RID: 15704 RVA: 0x00112D24 File Offset: 0x00111124
		public void Init(NewGameHeroSelector heroSelector)
		{
			this.focus = new AnimatedState("focus", this.stateRoot.rootState, false, false);
			if (this.focusImage)
			{
				Graphic focusGraphic = this.focusImage.GetComponent<Graphic>();
				float defaultFraction = this.focusImage.fraction;
				this.focus.Subscribe(delegate(bool x)
				{
					this.focusImage.gameObject.SetActive(x);
				}, delegate(float x)
				{
					float fraction = Mathf.Lerp(this.mainImage.fraction, defaultFraction, x);
					this.focusImage.fraction = fraction;
					focusGraphic.color = focusGraphic.color.SetA(x);
				});
			}
			this.navigable.onFocusChanged += delegate(bool x)
			{
				this.focus.SetActive(x);
			};
			this.visible = new AnimatedState("visible", this.stateRoot.rootState, true, false, this.expandAnimFuncs);
			this.expanded = new AnimatedState("extended", this.stateRoot.rootState, false, false, this.expandAnimFuncs);
			RectTransform rt = (RectTransform)base.transform;
			Vector2 expansion = this.expandTransform.rect.size - rt.rect.size;
			Action<float> action = delegate(float a)
			{
				Vector2 vector = Vector2.Lerp(this.peekSize * this.visible.anim.current, expansion, this.expanded.anim.current * this.visible.anim.current);
				this.expandTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rt.rect.width + vector.x);
				this.expandTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, rt.rect.height + vector.y);
			};
			Action expandFunction = delegate()
			{
				bool active = Platform.Is(EPlatform.Touchscreen) || Profile.userSettings.cursorBehaviour == UserSettings.CursorBehaviour.Touch || heroSelector.pointerHover || heroSelector.isFocussed || this.navigable.hasFocus;
				if (this.expanded.SetActive(active))
				{
					FabricWrapper.PostEvent(this.tabAnimate);
				}
			};
			Platform.onPlatformUpdated += expandFunction;
			UserSettings.onUpdated += delegate(UserSettings x)
			{
				expandFunction();
			};
			InputHelpers.onControllerTypeChanged += delegate(ControllerType x)
			{
				expandFunction();
			};
			heroSelector.onFocusChanged += delegate(bool x)
			{
				expandFunction();
			};
			heroSelector.onHoverChanged += delegate(bool x)
			{
				expandFunction();
			};
			this.navigable.onFocusChanged += delegate(bool x)
			{
				expandFunction();
			};
			heroSelector.onParentOpened += expandFunction;
			TargetAnimator<float> anim = this.visible.anim;
			anim.setFunc = (Action<float>)Delegate.Combine(anim.setFunc, action);
			TargetAnimator<float> anim2 = this.expanded.anim;
			anim2.setFunc = (Action<float>)Delegate.Combine(anim2.setFunc, action);
			expandFunction();
			action(0f);
			this.SetUpHover(this.mainImage);
			this.SetUpHover(this.secondaryImage);
		}

		// Token: 0x06003D59 RID: 15705 RVA: 0x00112F7C File Offset: 0x0011137C
		private void SetUpHover(Component animateRoot)
		{
			if (!animateRoot)
			{
				return;
			}
			UIPointerReceiver componentInParentIncludingInactive = animateRoot.gameObject.GetComponentInParentIncludingInactive<UIPointerReceiver>();
			AnimatedState hover = new AnimatedState("TabHover", this.stateRoot.rootState, false, false);
			AnimatedState buttonDown = new AnimatedState("TabButtonDown", this.stateRoot.rootState, false, false);
			Action<float> b = delegate(float a)
			{
				float num = 1f;
				num += hover.anim.current * 0.2f;
				num += this.focus.anim.current * 0.2f;
				num -= buttonDown.anim.current * 0.1f;
				num = Mathf.Min(num, 1.2f);
				animateRoot.transform.localScale = animateRoot.transform.localScale.SetZ(num);
			};
			TargetAnimator<float> anim = hover.anim;
			anim.setFunc = (Action<float>)Delegate.Combine(anim.setFunc, b);
			TargetAnimator<float> anim2 = buttonDown.anim;
			anim2.setFunc = (Action<float>)Delegate.Combine(anim2.setFunc, b);
			TargetAnimator<float> anim3 = this.focus.anim;
			anim3.setFunc = (Action<float>)Delegate.Combine(anim3.setFunc, b);
			componentInParentIncludingInactive.onStateChanged += delegate(UIPointerReceiver.State s)
			{
				hover.active = (s != UIPointerReceiver.State.None);
			};
			componentInParentIncludingInactive.onStateChanged += delegate(UIPointerReceiver.State s)
			{
				buttonDown.active = (s == UIPointerReceiver.State.ButtonDown);
			};
		}

		// Token: 0x06003D5A RID: 15706 RVA: 0x0011308D File Offset: 0x0011148D
		private void OnDisable()
		{
			this.expanded.SetActive(false);
			this.expanded.ForceToTarget();
		}

		// Token: 0x06003D5B RID: 15707 RVA: 0x001130A7 File Offset: 0x001114A7
		private void Update()
		{
			this.stateRoot.Update();
		}

		// Token: 0x04002AD1 RID: 10961
		[SerializeField]
		private UINavigable navigable;

		// Token: 0x04002AD2 RID: 10962
		[SerializeField]
		private DistanceFieldSettings mainImage;

		// Token: 0x04002AD3 RID: 10963
		[SerializeField]
		private DistanceFieldSettings secondaryImage;

		// Token: 0x04002AD4 RID: 10964
		[SerializeField]
		private DistanceFieldSettings focusImage;

		// Token: 0x04002AD5 RID: 10965
		[SerializeField]
		private RectTransform expandTransform;

		// Token: 0x04002AD6 RID: 10966
		[SerializeField]
		private Vector2 peekSize = new Vector2(25f, 0f);

		// Token: 0x04002AD7 RID: 10967
		[SerializeField]
		private AgentStateRoot stateRoot = new AgentStateRoot(4);

		// Token: 0x04002AD8 RID: 10968
		private AnimatedState focus;

		// Token: 0x04002AD9 RID: 10969
		private AnimatedState visible;

		// Token: 0x04002ADA RID: 10970
		private AnimatedState expanded;

		// Token: 0x04002ADB RID: 10971
		private LerpTowards expandAnimFuncs = new LerpTowards(16f, 3f);

		// Token: 0x04002ADC RID: 10972
		private FabricEventReference tabAnimate = "UI/Menu/NewGameTabs";
	}
}
