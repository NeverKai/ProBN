using System;
using RTM.UISystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Voxels.TowerDefense.ScriptAnimations;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x02000908 RID: 2312
	public class DistanceFieldAnimator : UIBehaviour
	{
		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x06003DD2 RID: 15826 RVA: 0x00115815 File Offset: 0x00113C15
		protected AgentState rootState
		{
			get
			{
				return this.stateRoot.rootState;
			}
		}

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x06003DD3 RID: 15827 RVA: 0x00115822 File Offset: 0x00113C22
		// (set) Token: 0x06003DD4 RID: 15828 RVA: 0x0011582A File Offset: 0x00113C2A
		public AnimatedState none { get; private set; }

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x06003DD5 RID: 15829 RVA: 0x00115833 File Offset: 0x00113C33
		// (set) Token: 0x06003DD6 RID: 15830 RVA: 0x0011583B File Offset: 0x00113C3B
		public AnimatedState hover { get; private set; }

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x06003DD7 RID: 15831 RVA: 0x00115844 File Offset: 0x00113C44
		// (set) Token: 0x06003DD8 RID: 15832 RVA: 0x0011584C File Offset: 0x00113C4C
		public AnimatedState buttonDown { get; private set; }

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x06003DD9 RID: 15833 RVA: 0x00115855 File Offset: 0x00113C55
		// (set) Token: 0x06003DDA RID: 15834 RVA: 0x0011585D File Offset: 0x00113C5D
		public AnimatedState focus { get; private set; }

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x06003DDB RID: 15835 RVA: 0x00115866 File Offset: 0x00113C66
		// (set) Token: 0x06003DDC RID: 15836 RVA: 0x0011586E File Offset: 0x00113C6E
		public AnimatedState selected { get; private set; }

		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x06003DDD RID: 15837 RVA: 0x00115877 File Offset: 0x00113C77
		// (set) Token: 0x06003DDE RID: 15838 RVA: 0x0011587F File Offset: 0x00113C7F
		public AnimatedState disabled { get; private set; }

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x06003DDF RID: 15839 RVA: 0x00115888 File Offset: 0x00113C88
		// (set) Token: 0x06003DE0 RID: 15840 RVA: 0x00115890 File Offset: 0x00113C90
		public AnimatedState flash { get; private set; }

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x06003DE1 RID: 15841 RVA: 0x00115899 File Offset: 0x00113C99
		// (set) Token: 0x06003DE2 RID: 15842 RVA: 0x001158A1 File Offset: 0x00113CA1
		public AnimatedState modifyColor { get; private set; }

		// Token: 0x06003DE3 RID: 15843 RVA: 0x001158AC File Offset: 0x00113CAC
		public void MaybeInitialize()
		{
			if (this.initialized)
			{
				return;
			}
			this.initialized = true;
			this.none = new AnimatedState("none", this.rootState, true, true);
			this.hover = new AnimatedState("Hover", this.rootState, false, true);
			this.buttonDown = new AnimatedState("ButtonDown", this.rootState, false, true);
			this.focus = new AnimatedState("Focus", this.rootState, false, true);
			this.selected = new AnimatedState("Selected", this.rootState, false, false);
			this.disabled = new AnimatedState("Disabled", this.rootState, false, false);
			this.flash = new AnimatedState("Flash", this.rootState, false, false);
			Transform offsetTransform = this.mainImage.transform;
			Action<float> b = delegate(float x)
			{
				float num = 1f;
				num += this.hover.value * Mathf.Lerp(0.2f, 0f, this.disabled.value);
				num += this.buttonDown.value * 0.1f;
				num += this.focus.value * 0.2f;
				num += this.selected.value * 0.3f;
				num = Mathf.Lerp(num, 1.5f, this.flash.value);
				offsetTransform.localScale = offsetTransform.localScale.SetZ(num);
			};
			TargetAnimator<float> anim = this.hover.anim;
			anim.setFunc = (Action<float>)Delegate.Combine(anim.setFunc, b);
			TargetAnimator<float> anim2 = this.buttonDown.anim;
			anim2.setFunc = (Action<float>)Delegate.Combine(anim2.setFunc, b);
			TargetAnimator<float> anim3 = this.focus.anim;
			anim3.setFunc = (Action<float>)Delegate.Combine(anim3.setFunc, b);
			TargetAnimator<float> anim4 = this.selected.anim;
			anim4.setFunc = (Action<float>)Delegate.Combine(anim4.setFunc, b);
			TargetAnimator<float> anim5 = this.disabled.anim;
			anim5.setFunc = (Action<float>)Delegate.Combine(anim5.setFunc, b);
			TargetAnimator<float> anim6 = this.flash.anim;
			anim6.setFunc = (Action<float>)Delegate.Combine(anim6.setFunc, b);
			TargetAnimator<float> anim7 = this.flash.anim;
			anim7.setFunc = (Action<float>)Delegate.Combine(anim7.setFunc, new Action<float>(delegate(float v)
			{
				float value = Mathf.Lerp(1f, 1.05f, v);
				offsetTransform.localScale = offsetTransform.localScale.SetX(value).SetY(value);
			}));
			if (this.hsv)
			{
				this.disabled.anim.Subscribe(delegate(float x)
				{
					this.hsv.value = Mathf.Lerp(1f, 0.7f, x);
					this.hsv.saturation = Mathf.Lerp(1f, 0.4f, x);
				});
			}
			Vector3 defaultPos = offsetTransform.localPosition;
			this.posAnim = new TargetAnimator<Vector3>(() => offsetTransform.localPosition - defaultPos, delegate(Vector3 x)
			{
				offsetTransform.localPosition = defaultPos + x;
			}, this.rootState, new LerpTowards3(20f, 0.2f));
			AnimatedState buttonDown = this.buttonDown;
			buttonDown.OnChange = (Action<bool>)Delegate.Combine(buttonDown.OnChange, new Action<bool>(delegate(bool x)
			{
				this.posAnim.SetTarget((!x) ? Vector3.zero : (Vector3.down * 2f), null, null, null, 0f, null);
			}));
			AnimatedState hover = this.hover;
			hover.OnChange = (Action<bool>)Delegate.Combine(hover.OnChange, new Action<bool>(delegate(bool x)
			{
				this.posAnim.SetTarget((!x) ? Vector3.zero : (Vector3.up * this.hoverOffset), null, null, null, 0f, null);
			}));
			AnimatedState focus = this.focus;
			focus.OnChange = (Action<bool>)Delegate.Combine(focus.OnChange, new Action<bool>(delegate(bool x)
			{
				this.posAnim.SetTarget((!x) ? Vector3.zero : (Vector3.up * this.hoverOffset), null, null, null, 0f, null);
			}));
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
					if (this.shadowImage)
					{
						this.shadowImage.fraction = fraction;
					}
					focusGraphic.color = focusGraphic.color.SetA(x);
				});
			}
			ColorModifier colorModifier = this.mainImage.GetComponent<ColorModifier>();
			if (colorModifier)
			{
				this.modifyColor = new AnimatedState("ModifyColor", this.rootState, false, false);
				this.modifyColor.Subscribe(delegate(bool x)
				{
					colorModifier.enabled = x;
				}, delegate(float x)
				{
					colorModifier.alpha = x;
				});
			}
			if (this.disableDim)
			{
				this.disabled.anim.Subscribe(delegate(float x)
				{
					this.disableDim.alpha = Mathf.Lerp(1f, 0.5f, x);
				});
			}
			UIClickable component = base.GetComponent<UIClickable>();
			if (component)
			{
				component.onStateChanged += this.Clickable_onStateChanged;
				component.onSelectedChanged += this.Clickable_onSelectedChanged;
				component.onFlash += this.Flash;
				component.onDisabledChanged += delegate(bool x)
				{
					this.disabled.SetActive(x);
				};
				this.disabled.SetActive(component.disabled);
				this.Clickable_onSelectedChanged(component.selected);
				component.onClick += delegate()
				{
					if (this.focus.active)
					{
						this.posAnim.SetCurrentAndActivate(this.posAnim.current + Vector3.down);
					}
				};
				component.onClickFailed += delegate()
				{
					this.posAnim.SetCurrentAndActivate(this.posAnim.current + Vector3.down);
				};
			}
			else
			{
				UINavigable component2 = base.GetComponent<UINavigable>();
				component2.onFocusChanged += delegate(bool f)
				{
					this.focus.SetActive(f);
				};
			}
		}

		// Token: 0x06003DE4 RID: 15844 RVA: 0x00115D21 File Offset: 0x00114121
		private void Clickable_onSelectedChanged(bool value)
		{
			this.selected.SetActive(value);
		}

		// Token: 0x06003DE5 RID: 15845 RVA: 0x00115D30 File Offset: 0x00114130
		[ContextMenu("Toggle Disabled")]
		private void Toggle()
		{
			this.disabled.SetActive(!this.disabled.active);
		}

		// Token: 0x06003DE6 RID: 15846 RVA: 0x00115D4C File Offset: 0x0011414C
		[ContextMenu("Toggle Color Modifier")]
		private void ToggleColorModifier()
		{
			this.modifyColor.SetActive(!this.modifyColor.active);
		}

		// Token: 0x06003DE7 RID: 15847 RVA: 0x00115D68 File Offset: 0x00114168
		private void Clickable_onStateChanged(UIInteractable.State obj)
		{
			switch (obj)
			{
			case UIInteractable.State.None:
				this.none.SetActive(true);
				break;
			case UIInteractable.State.Hover:
				this.hover.SetActive(true);
				break;
			case UIInteractable.State.PointerButtonDown:
				this.buttonDown.SetActive(true);
				break;
			case UIInteractable.State.Focus:
				this.focus.SetActive(true);
				break;
			default:
				Debug.LogError("Unhandled state: " + obj.ToString(), this);
				break;
			}
		}

		// Token: 0x06003DE8 RID: 15848 RVA: 0x00115DFA File Offset: 0x001141FA
		public void Flash()
		{
			this.flash.anim.SetCurrent(1f);
			this.flash.anim.SetTarget(0f, null, null, null, 0.05f, null);
		}

		// Token: 0x06003DE9 RID: 15849 RVA: 0x00115E2F File Offset: 0x0011422F
		public void Push(Vector2 offset)
		{
			this.posAnim.SetCurrent(this.posAnim.current + offset);
			this.posAnim.ForceActive();
		}

		// Token: 0x06003DEA RID: 15850 RVA: 0x00115E5D File Offset: 0x0011425D
		private void Update()
		{
			this.rootState.Update();
		}

		// Token: 0x06003DEB RID: 15851 RVA: 0x00115E6A File Offset: 0x0011426A
		protected override void Awake()
		{
			base.Awake();
			this.MaybeInitialize();
		}

		// Token: 0x06003DEC RID: 15852 RVA: 0x00115E78 File Offset: 0x00114278
		protected override void OnDestroy()
		{
			base.OnDestroy();
			this.rootState.OnDestroy();
		}

		// Token: 0x04002B2B RID: 11051
		[SerializeField]
		private DistanceFieldSettings focusImage;

		// Token: 0x04002B2C RID: 11052
		[SerializeField]
		private DistanceFieldSettings mainImage;

		// Token: 0x04002B2D RID: 11053
		[SerializeField]
		private DistanceFieldSettings shadowImage;

		// Token: 0x04002B2E RID: 11054
		[SerializeField]
		private CanvasGroup disableDim;

		// Token: 0x04002B2F RID: 11055
		[SerializeField]
		private GraphicHSV hsv;

		// Token: 0x04002B30 RID: 11056
		[SerializeField]
		private float hoverOffset = 1f;

		// Token: 0x04002B31 RID: 11057
		[SerializeField]
		private bool animateHeight = true;

		// Token: 0x04002B32 RID: 11058
		[SerializeField]
		protected AgentStateRoot stateRoot = new AgentStateRoot(4);

		// Token: 0x04002B3B RID: 11067
		private TargetAnimator<Vector3> posAnim;

		// Token: 0x04002B3C RID: 11068
		private float focusLerp;

		// Token: 0x04002B3D RID: 11069
		private float disabledLerp;

		// Token: 0x04002B3E RID: 11070
		private static LerpTowards focusOpen = new LerpTowards(24f, 0f);

		// Token: 0x04002B3F RID: 11071
		private static LerpTowards focusClose = new LerpTowards(14f, 0.2f);

		// Token: 0x04002B40 RID: 11072
		private bool initialized;
	}
}
