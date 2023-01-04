using System;
using RTM.Pools;
using RTM.UISystem;
using UnityEngine;
using Voxels.TowerDefense.ScriptAnimations;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x02000901 RID: 2305
	internal abstract class UpgradeCarouselItem : MonoBehaviour, IPoolable
	{
		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x06003D90 RID: 15760 RVA: 0x0011447F File Offset: 0x0011287F
		// (set) Token: 0x06003D91 RID: 15761 RVA: 0x00114487 File Offset: 0x00112887
		public HeroUpgradeDefinition upgradeDef { get; private set; }

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x06003D92 RID: 15762 RVA: 0x00114490 File Offset: 0x00112890
		// (set) Token: 0x06003D93 RID: 15763 RVA: 0x00114498 File Offset: 0x00112898
		public int pos { get; private set; }

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x06003D94 RID: 15764 RVA: 0x001144A4 File Offset: 0x001128A4
		protected float flashValue
		{
			get
			{
				float num = this.flash.current * 2f - 1f;
				return 1f - num * num;
			}
		}

		// Token: 0x06003D95 RID: 15765 RVA: 0x001144D4 File Offset: 0x001128D4
		protected virtual void Init()
		{
			this.pos = int.MinValue;
			this.rectTransform = (RectTransform)base.transform;
			this.focus = new AnimatedState("Focus", this.stateRoot.rootState, false, false);
			this.hover = new AnimatedState("Hover", this.stateRoot.rootState, false, false);
			this.buttonDown = new AnimatedState("ButtonDown", this.stateRoot.rootState, false, false);
			float flashAlpha = 0f;
			this.flash = new TargetAnimator("Flash", () => flashAlpha, delegate(float a)
			{
				flashAlpha = a;
			}, this.stateRoot.rootState, LerpTowards.standard);
		}

		// Token: 0x06003D96 RID: 15766 RVA: 0x001145A0 File Offset: 0x001129A0
		public void Setup(HeroUpgradeDefinition upgradeDef, int pos, UIInteractable.State state, bool isAvailable)
		{
			if (this.upgradeDef == upgradeDef && this.pos == pos)
			{
				return;
			}
			this.upgradeDef = upgradeDef;
			this.pos = pos;
			this.isAvailable = isAvailable;
			this.SetUpgrade(upgradeDef, isAvailable);
			this.SetState(state);
			base.transform.ForceChildLayoutUpdates(false);
		}

		// Token: 0x06003D97 RID: 15767 RVA: 0x001145FD File Offset: 0x001129FD
		public virtual void Animate(float currentPos, float targetPos, bool snap = false)
		{
			this.animPos = (float)this.pos - currentPos;
			if (snap)
			{
				this.focus.ForceToTarget();
				this.hover.ForceToTarget();
				this.buttonDown.ForceToTarget();
			}
		}

		// Token: 0x06003D98 RID: 15768
		protected abstract void SetUpgrade(HeroUpgradeDefinition upgradeDef, bool isAvailable);

		// Token: 0x06003D99 RID: 15769 RVA: 0x00114638 File Offset: 0x00112A38
		public virtual void SetState(UIInteractable.State state)
		{
			state = ((!this.isAvailable || !this.upgradeDef) ? UIInteractable.State.None : state);
			this.focus.active = (state == UIInteractable.State.Focus);
			this.hover.active = (state == UIInteractable.State.Hover || state == UIInteractable.State.PointerButtonDown);
			this.buttonDown.active = (state == UIInteractable.State.PointerButtonDown);
		}

		// Token: 0x06003D9A RID: 15770 RVA: 0x001146A0 File Offset: 0x00112AA0
		public void Flash(float holdTime = 0f)
		{
			this.flash.SetTargetAndCurrent(1f);
			TargetAnimator<float> targetAnimator = this.flash;
			float target = 0f;
			targetAnimator.SetTarget(target, null, null, null, holdTime, null);
		}

		// Token: 0x06003D9B RID: 15771 RVA: 0x001146D6 File Offset: 0x00112AD6
		private void Update()
		{
			this.stateRoot.Update();
		}

		// Token: 0x06003D9C RID: 15772 RVA: 0x001146E3 File Offset: 0x00112AE3
		void IPoolable.SetPool<T>(LocalPool<T> pool)
		{
			this.Init();
		}

		// Token: 0x06003D9D RID: 15773 RVA: 0x001146EB File Offset: 0x00112AEB
		void IPoolable.OnRemoved()
		{
			base.gameObject.SetActive(true);
		}

		// Token: 0x06003D9E RID: 15774 RVA: 0x001146F9 File Offset: 0x00112AF9
		void IPoolable.OnReturned()
		{
			base.gameObject.SetActive(false);
			this.Setup(null, int.MinValue, UIInteractable.State.None, true);
		}

		// Token: 0x04002AFC RID: 11004
		protected RectTransform rectTransform;

		// Token: 0x04002AFF RID: 11007
		[SerializeField]
		private AgentStateRoot stateRoot = new AgentStateRoot(4);

		// Token: 0x04002B00 RID: 11008
		protected float animPos;

		// Token: 0x04002B01 RID: 11009
		protected bool isAvailable;

		// Token: 0x04002B02 RID: 11010
		protected AnimatedState focus;

		// Token: 0x04002B03 RID: 11011
		protected AnimatedState hover;

		// Token: 0x04002B04 RID: 11012
		protected AnimatedState buttonDown;

		// Token: 0x04002B05 RID: 11013
		protected TargetAnimator flash;
	}
}
