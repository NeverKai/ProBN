using System;
using RTM.UISystem;
using UnityEngine;
using UnityEngine.EventSystems;
using Voxels.TowerDefense.ScriptAnimations;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x02000913 RID: 2323
	public class PolygonAnimator : UIBehaviour
	{
		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x06003E38 RID: 15928 RVA: 0x00117DC7 File Offset: 0x001161C7
		protected AgentState rootState
		{
			get
			{
				return this.stateRoot.rootState;
			}
		}

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x06003E39 RID: 15929 RVA: 0x00117DD4 File Offset: 0x001161D4
		// (set) Token: 0x06003E3A RID: 15930 RVA: 0x00117DDC File Offset: 0x001161DC
		public AnimatedState none { get; private set; }

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x06003E3B RID: 15931 RVA: 0x00117DE5 File Offset: 0x001161E5
		// (set) Token: 0x06003E3C RID: 15932 RVA: 0x00117DED File Offset: 0x001161ED
		public AnimatedState hover { get; private set; }

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x06003E3D RID: 15933 RVA: 0x00117DF6 File Offset: 0x001161F6
		// (set) Token: 0x06003E3E RID: 15934 RVA: 0x00117DFE File Offset: 0x001161FE
		public AnimatedState buttonDown { get; private set; }

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x06003E3F RID: 15935 RVA: 0x00117E07 File Offset: 0x00116207
		// (set) Token: 0x06003E40 RID: 15936 RVA: 0x00117E0F File Offset: 0x0011620F
		public AnimatedState focus { get; private set; }

		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x06003E41 RID: 15937 RVA: 0x00117E18 File Offset: 0x00116218
		// (set) Token: 0x06003E42 RID: 15938 RVA: 0x00117E20 File Offset: 0x00116220
		public AnimatedState selected { get; private set; }

		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x06003E43 RID: 15939 RVA: 0x00117E29 File Offset: 0x00116229
		// (set) Token: 0x06003E44 RID: 15940 RVA: 0x00117E31 File Offset: 0x00116231
		public AnimatedState disabled { get; private set; }

		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x06003E45 RID: 15941 RVA: 0x00117E3A File Offset: 0x0011623A
		// (set) Token: 0x06003E46 RID: 15942 RVA: 0x00117E42 File Offset: 0x00116242
		public AnimatedState flash { get; private set; }

		// Token: 0x06003E47 RID: 15943 RVA: 0x00117E4C File Offset: 0x0011624C
		public void MaybeInitialize()
		{
			if (this.initialized)
			{
				return;
			}
			this.initialized = true;
			LerpTowards targetAnimFuncs = new LerpTowards(14f, 0.2f);
			this.none = new AnimatedState("none", this.rootState, true, true, targetAnimFuncs);
			this.hover = new AnimatedState("Hover", this.rootState, false, true, targetAnimFuncs);
			this.buttonDown = new AnimatedState("ButtonDown", this.rootState, false, true, targetAnimFuncs);
			this.focus = new AnimatedState("Focus", this.rootState, false, true, targetAnimFuncs);
			this.selected = new AnimatedState("Selected", this.rootState, false, false, targetAnimFuncs);
			this.disabled = new AnimatedState("Disabled", this.rootState, false, false, targetAnimFuncs);
			this.flash = new AnimatedState("Flash", this.rootState, false, false, targetAnimFuncs);
			Action<float> b = delegate(float x)
			{
				float num = 0f;
				num += this.hover.value * Mathf.Lerp(0.2f, 0f, this.disabled.value);
				num += this.buttonDown.value * 0.1f;
				num += this.focus.value * 0.2f;
				num += this.selected.value * 0.3f;
				num *= Mathf.Lerp(1f, 0.4f, this.disabled.value);
				num += Mathf.Lerp(1f, 0.7f, this.disabled.value);
				num = Mathf.Lerp(num, 1.5f, this.flash.value);
				this.graphic.transform.localScale = this.graphic.transform.localScale.SetZ(num);
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
				this.graphic.transform.localScale = this.graphic.transform.localScale.SetX(value).SetY(value);
			}));
			if (this.offsetTransform)
			{
				Vector3 defaultPos = this.offsetTransform.localPosition;
				this.posAnim = new TargetAnimator<Vector3>(() => this.offsetTransform.localPosition - defaultPos, delegate(Vector3 x)
				{
					this.offsetTransform.localPosition = defaultPos + x;
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
			}
			Vector3 defaultGraphicPos = this.graphic.transform.localPosition;
			TargetAnimator<Vector3> graphicPosAnim = new TargetAnimator<Vector3>(() => this.graphic.transform.localPosition - defaultGraphicPos, delegate(Vector3 x)
			{
				this.graphic.transform.localPosition = defaultGraphicPos + x;
			}, this.rootState, new LerpTowards3(20f, 0.2f));
			if (this.selectGraphic)
			{
				LerpTowards focusOpen = new LerpTowards(24f, 0f);
				LerpTowards focusClose = new LerpTowards(14f, 0.2f);
				this.focusAnim = new TargetAnimator(() => this.focusLerp, delegate(float x)
				{
					this.focusLerp = x;
					Rect rect = this.selectGraphic.rectTransform.rect;
					Vector2 a;
					a.x = rect.x / (rect.x - 10f);
					a.y = rect.y / (rect.y - 10f);
					if (a.x == float.PositiveInfinity)
					{
						a.x = 0f;
					}
					if (a.y == float.PositiveInfinity)
					{
						a.y = 0f;
					}
					this.selectGraphic.transform.localScale = (a * (1f - this.focusLerp) + Vector2.one * this.focusLerp).SetZ(1f);
				}, this.rootState, focusOpen);
				this.focusAnim.SetCurrent(0f);
				AnimatedState focus = this.focus;
				focus.OnActivate = (Action)Delegate.Combine(focus.OnActivate, new Action(delegate()
				{
					this.selectGraphic.gameObject.SetActive(true);
					this.focusAnim.SetTarget(1f, null, null, focusOpen, 0f, null);
				}));
				Action focusClosed = delegate()
				{
					this.selectGraphic.gameObject.SetActive(false);
				};
				AnimatedState focus2 = this.focus;
				focus2.OnDeactivate = (Action)Delegate.Combine(focus2.OnDeactivate, new Action(delegate()
				{
					this.focusAnim.SetTarget(0f, null, focusClosed, focusClose, 0f, null);
				}));
				focusClosed();
				AnimatedState focus3 = this.focus;
				focus3.OnChange = (Action<bool>)Delegate.Combine(focus3.OnChange, new Action<bool>(delegate(bool x)
				{
					graphicPosAnim.SetTarget((!x) ? Vector3.zero : Vector3.up, null, null, null, 0f, null);
				}));
			}
			UIClickable component = base.GetComponent<UIClickable>();
			component.onStateChanged += this.Clickable_onStateChanged;
			component.onSelectedChanged += this.Clickable_onSelectedChanged;
			component.onFlash += this.Flash;
			component.onDisabledChanged += delegate(bool x)
			{
				this.disabled.SetActive(x);
			};
			this.Clickable_onStateChanged(component.state);
			this.Clickable_onSelectedChanged(component.selected);
			this.disabled.SetActive(component.disabled);
			component.onClick += delegate()
			{
				if (this.focus.active)
				{
					graphicPosAnim.SetCurrentAndActivate(Vector3.down);
					this.focusAnim.SetCurrentAndActivate(1.1f);
				}
			};
			component.onClickFailed += delegate()
			{
				graphicPosAnim.SetCurrentAndActivate(Vector3.down * 2f);
			};
		}

		// Token: 0x06003E48 RID: 15944 RVA: 0x00118305 File Offset: 0x00116705
		private void Clickable_onSelectedChanged(bool value)
		{
			this.selected.SetActive(value);
		}

		// Token: 0x06003E49 RID: 15945 RVA: 0x00118314 File Offset: 0x00116714
		[ContextMenu("Toggle Disabled")]
		private void Toggle()
		{
			this.disabled.SetActive(!this.disabled.active);
		}

		// Token: 0x06003E4A RID: 15946 RVA: 0x00118330 File Offset: 0x00116730
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

		// Token: 0x06003E4B RID: 15947 RVA: 0x001183C2 File Offset: 0x001167C2
		public void Flash()
		{
			this.flash.anim.SetCurrent(1f);
			this.flash.anim.SetTarget(0f, null, null, null, 0.05f, null);
		}

		// Token: 0x06003E4C RID: 15948 RVA: 0x001183F7 File Offset: 0x001167F7
		public void Push(Vector2 offset)
		{
			this.posAnim.SetCurrent(this.posAnim.current + offset);
			this.posAnim.ForceActive();
		}

		// Token: 0x06003E4D RID: 15949 RVA: 0x00118425 File Offset: 0x00116825
		private void Update()
		{
			this.rootState.Update();
		}

		// Token: 0x06003E4E RID: 15950 RVA: 0x00118432 File Offset: 0x00116832
		protected override void Awake()
		{
			base.Awake();
			this.MaybeInitialize();
		}

		// Token: 0x06003E4F RID: 15951 RVA: 0x00118440 File Offset: 0x00116840
		protected override void OnDestroy()
		{
			base.OnDestroy();
			this.rootState.OnDestroy();
		}

		// Token: 0x06003E50 RID: 15952 RVA: 0x00118453 File Offset: 0x00116853
		protected override void OnRectTransformDimensionsChange()
		{
			if (this.focusAnim != null)
			{
				this.focusAnim.SetCurrent(this.focusAnim.current);
			}
		}

		// Token: 0x04002B7E RID: 11134
		[SerializeField]
		private PolygonMask graphic;

		// Token: 0x04002B7F RID: 11135
		[SerializeField]
		private PolygonMask selectGraphic;

		// Token: 0x04002B80 RID: 11136
		[SerializeField]
		private Transform offsetTransform;

		// Token: 0x04002B81 RID: 11137
		[SerializeField]
		private float hoverOffset = 1f;

		// Token: 0x04002B82 RID: 11138
		[SerializeField]
		protected AgentStateRoot stateRoot = new AgentStateRoot(4);

		// Token: 0x04002B8A RID: 11146
		private TargetAnimator focusAnim;

		// Token: 0x04002B8B RID: 11147
		private TargetAnimator<Vector3> posAnim;

		// Token: 0x04002B8C RID: 11148
		private float focusLerp;

		// Token: 0x04002B8D RID: 11149
		private float disabledLerp;

		// Token: 0x04002B8E RID: 11150
		private bool initialized;
	}
}
