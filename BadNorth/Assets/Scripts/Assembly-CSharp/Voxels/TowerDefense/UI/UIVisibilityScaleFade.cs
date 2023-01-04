using System;
using RTM.UISystem;
using UnityEngine;
using Voxels.TowerDefense.ScriptAnimations;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x02000827 RID: 2087
	internal class UIVisibilityScaleFade : MonoBehaviour, IUIVisibility
	{
		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x06003679 RID: 13945 RVA: 0x000EA7D0 File Offset: 0x000E8BD0
		private AgentState visible
		{
			get
			{
				if (this._visible == null)
				{
					AgentState agentState = new AgentState("Visible", this.stateRoot.rootState, false, true);
					this._visible = agentState;
					AgentState agentState2 = agentState;
					agentState2.OnActivate = (Action)Delegate.Combine(agentState2.OnActivate, new Action(delegate()
					{
						base.gameObject.SetActive(true);
						this.animator.SetTarget(1f, null, null, this.anim, (this.animator.current != 0f) ? 0f : this.appearDelay, null);
						this.SetInteractible(true);
					}));
					AgentState agentState3 = agentState;
					agentState3.OnDeactivate = (Action)Delegate.Combine(agentState3.OnDeactivate, new Action(delegate()
					{
						this.animator.SetTarget(0f, null, this.goSetActiveFalse, this.anim, (this.animator.current != 1f) ? 0f : this.disappearDelay, null);
						this.SetInteractible(false);
					}));
				}
				return this._visible;
			}
		}

		// Token: 0x0600367A RID: 13946 RVA: 0x000EA854 File Offset: 0x000E8C54
		private void MaybeInit()
		{
			if (this.animator != null)
			{
				return;
			}
			this.interactable = base.GetComponent<UIInteractable>();
			this.canvasGroup = base.GetComponent<CanvasGroup>();
			this.goSetActiveFalse = delegate()
			{
				base.gameObject.SetActive(false);
			};
			Action<float> setFunc = delegate(float l)
			{
				this.alpha = l;
				float num = Mathf.Lerp(this.hiddenScale, 1f, this.alpha);
				base.transform.localScale = new Vector3(num, num, 1f);
				if (this.canvasGroup)
				{
					this.canvasGroup.alpha = this.alpha;
				}
			};
			this.animator = new TargetAnimator<float>(() => this.alpha, setFunc, this.stateRoot.rootState, this.anim);
		}

		// Token: 0x0600367B RID: 13947 RVA: 0x000EA8CD File Offset: 0x000E8CCD
		private void Awake()
		{
			this.MaybeInit();
		}

		// Token: 0x0600367C RID: 13948 RVA: 0x000EA8D5 File Offset: 0x000E8CD5
		private void Update()
		{
			this.stateRoot.Update();
		}

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x0600367D RID: 13949 RVA: 0x000EA8E2 File Offset: 0x000E8CE2
		// (set) Token: 0x0600367E RID: 13950 RVA: 0x000EA8EF File Offset: 0x000E8CEF
		bool IUIVisibility.visible
		{
			get
			{
				return this.visible.active;
			}
			set
			{
				((IUIVisibility)this).SetVisible(value, false);
			}
		}

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x0600367F RID: 13951 RVA: 0x000EA8F9 File Offset: 0x000E8CF9
		float IUIVisibility.alpha
		{
			get
			{
				return this.animator.current;
			}
		}

		// Token: 0x06003680 RID: 13952 RVA: 0x000EA908 File Offset: 0x000E8D08
		void IUIVisibility.SetVisible(bool visible, bool snap)
		{
			this.MaybeInit();
			this.visible.SetActive(visible);
			if (snap)
			{
				this.animator.SetTarget((!visible) ? 0f : 1f, null, null, null, 0f, null);
				this.animator.ForceToTarget();
				if (!visible)
				{
					this.goSetActiveFalse();
				}
			}
		}

		// Token: 0x06003681 RID: 13953 RVA: 0x000EA974 File Offset: 0x000E8D74
		private void SetInteractible(bool value)
		{
			if (this.interactable)
			{
				this.interactable.disabled = !value;
			}
			if (this.canvasGroup)
			{
				CanvasGroup canvasGroup = this.canvasGroup;
				this.canvasGroup.blocksRaycasts = value;
				canvasGroup.interactable = value;
			}
		}

		// Token: 0x040024F1 RID: 9457
		[SerializeField]
		private float hiddenScale = 0.9f;

		// Token: 0x040024F2 RID: 9458
		[SerializeField]
		private LerpTowards anim = new LerpTowards(0f, 7.5f);

		// Token: 0x040024F3 RID: 9459
		[SerializeField]
		private float appearDelay;

		// Token: 0x040024F4 RID: 9460
		[SerializeField]
		private float disappearDelay;

		// Token: 0x040024F5 RID: 9461
		[SerializeField]
		private AgentStateRoot stateRoot = new AgentStateRoot(4);

		// Token: 0x040024F6 RID: 9462
		private TargetAnimator<float> animator;

		// Token: 0x040024F7 RID: 9463
		private UIInteractable interactable;

		// Token: 0x040024F8 RID: 9464
		private float alpha;

		// Token: 0x040024F9 RID: 9465
		private CanvasGroup canvasGroup;

		// Token: 0x040024FA RID: 9466
		private Action goSetActiveFalse;

		// Token: 0x040024FB RID: 9467
		private AgentState _visible;
	}
}
