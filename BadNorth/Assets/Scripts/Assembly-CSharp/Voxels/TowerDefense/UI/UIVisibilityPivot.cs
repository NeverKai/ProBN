using System;
using RTM.UISystem;
using UnityEngine;
using Voxels.TowerDefense.ScriptAnimations;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x02000826 RID: 2086
	internal class UIVisibilityPivot : MonoBehaviour, IUIVisibility
	{
		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x0600366A RID: 13930 RVA: 0x000EA49C File Offset: 0x000E889C
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
						this.animator.SetTarget(1f, null, null, this.showAnim, (this.animator.current != 0f) ? 0f : this.appearDelay, null);
						this.SetInteractible(true);
					}));
					AgentState agentState3 = agentState;
					agentState3.OnDeactivate = (Action)Delegate.Combine(agentState3.OnDeactivate, new Action(delegate()
					{
						this.animator.SetTarget(0f, null, this.goSetActiveFalse, this.hideAnim, (this.animator.current != 1f) ? 0f : this.disappearDelay, null);
						this.SetInteractible(false);
					}));
				}
				return this._visible;
			}
		}

		// Token: 0x0600366B RID: 13931 RVA: 0x000EA520 File Offset: 0x000E8920
		private void MaybeInit()
		{
			if (this.animator != null)
			{
				return;
			}
			this.rt = (base.transform as RectTransform);
			this.interactable = base.GetComponent<UIInteractable>();
			this.canvasGroup = base.GetComponent<CanvasGroup>();
			this.goSetActiveFalse = delegate()
			{
				base.gameObject.SetActive(false);
			};
			Action<float> setFunc = delegate(float l)
			{
				this.alpha = l;
				this.rt.pivot = Vector2.Lerp(this.hiddenPivot, this.visiblePivot, l);
			};
			this.animator = new TargetAnimator<float>(() => this.alpha, setFunc, this.stateRoot.rootState, this.showAnim);
		}

		// Token: 0x0600366C RID: 13932 RVA: 0x000EA5AA File Offset: 0x000E89AA
		private void Awake()
		{
			this.MaybeInit();
		}

		// Token: 0x0600366D RID: 13933 RVA: 0x000EA5B2 File Offset: 0x000E89B2
		private void Update()
		{
			this.stateRoot.Update();
		}

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x0600366E RID: 13934 RVA: 0x000EA5BF File Offset: 0x000E89BF
		// (set) Token: 0x0600366F RID: 13935 RVA: 0x000EA5CC File Offset: 0x000E89CC
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

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x06003670 RID: 13936 RVA: 0x000EA5D6 File Offset: 0x000E89D6
		float IUIVisibility.alpha
		{
			get
			{
				return this.animator.current;
			}
		}

		// Token: 0x06003671 RID: 13937 RVA: 0x000EA5E4 File Offset: 0x000E89E4
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

		// Token: 0x06003672 RID: 13938 RVA: 0x000EA650 File Offset: 0x000E8A50
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

		// Token: 0x040024E3 RID: 9443
		[SerializeField]
		private LerpTowards showAnim = new LerpTowards(14f, 0.2f);

		// Token: 0x040024E4 RID: 9444
		[SerializeField]
		private ZoomPast hideAnim = new ZoomPast(150f);

		// Token: 0x040024E5 RID: 9445
		[SerializeField]
		private float appearDelay;

		// Token: 0x040024E6 RID: 9446
		[SerializeField]
		private float disappearDelay;

		// Token: 0x040024E7 RID: 9447
		[SerializeField]
		private Vector2 visiblePivot = new Vector2(1f, 0f);

		// Token: 0x040024E8 RID: 9448
		[SerializeField]
		private Vector2 hiddenPivot = new Vector2(1f, 1.5f);

		// Token: 0x040024E9 RID: 9449
		[SerializeField]
		private AgentStateRoot stateRoot = new AgentStateRoot(4);

		// Token: 0x040024EA RID: 9450
		private TargetAnimator<float> animator;

		// Token: 0x040024EB RID: 9451
		private RectTransform rt;

		// Token: 0x040024EC RID: 9452
		private UIInteractable interactable;

		// Token: 0x040024ED RID: 9453
		private float alpha;

		// Token: 0x040024EE RID: 9454
		private CanvasGroup canvasGroup;

		// Token: 0x040024EF RID: 9455
		private Action goSetActiveFalse;

		// Token: 0x040024F0 RID: 9456
		private AgentState _visible;
	}
}
