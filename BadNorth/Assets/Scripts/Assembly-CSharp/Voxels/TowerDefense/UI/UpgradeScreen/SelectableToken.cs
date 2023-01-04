using System;
using RTM.Pools;
using RTM.UISystem;
using UnityEngine;
using Voxels.TowerDefense.ScriptAnimations;
using Voxels.TowerDefense.UI.GridScrolling;

namespace Voxels.TowerDefense.UI.UpgradeScreen
{
	// Token: 0x02000937 RID: 2359
	public abstract class SelectableToken : MonoBehaviour, IPoolable
	{
		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x06003F9A RID: 16282 RVA: 0x001208CF File Offset: 0x0011ECCF
		protected AgentState rootState
		{
			get
			{
				return this.stateRoot.rootState;
			}
		}

		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x06003F9B RID: 16283 RVA: 0x001208DC File Offset: 0x0011ECDC
		// (set) Token: 0x06003F9C RID: 16284 RVA: 0x001208E4 File Offset: 0x0011ECE4
		public ScrollGridItem scrollItem { get; private set; }

		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x06003F9D RID: 16285 RVA: 0x001208ED File Offset: 0x0011ECED
		// (set) Token: 0x06003F9E RID: 16286 RVA: 0x001208F5 File Offset: 0x0011ECF5
		public UINavigable navigable { get; private set; }

		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x06003F9F RID: 16287 RVA: 0x001208FE File Offset: 0x0011ECFE
		// (set) Token: 0x06003FA0 RID: 16288 RVA: 0x00120906 File Offset: 0x0011ED06
		public UIClickable clickable { get; private set; }

		// Token: 0x06003FA1 RID: 16289 RVA: 0x0012090F File Offset: 0x0011ED0F
		private void OnValidate()
		{
			this.coin = base.GetComponentInChildren<CoinGraphic>();
		}

		// Token: 0x06003FA2 RID: 16290 RVA: 0x00120920 File Offset: 0x0011ED20
		public void MaybeInitialize()
		{
			if (this.initialized)
			{
				return;
			}
			this.initialized = true;
			if (this.coin)
			{
				this.coin.MaybeInitialize();
			}
			this.canvasGroup = base.GetComponent<CanvasGroup>();
			this.visibleState = new AnimatedState("VisibleState", this.rootState, false, false);
			this.selectedState = new AgentState("Selected", this.rootState, false, false);
			AnimatedState animatedState = this.visibleState;
			animatedState.OnChange = (Action<bool>)Delegate.Combine(animatedState.OnChange, new Action<bool>(delegate(bool x)
			{
				this.canvasGroup.blocksRaycasts = x;
			}));
			this.visibleState.Subscribe(delegate(bool x)
			{
				this.gameObject.SetActive(x);
				if (!x)
				{
					this.onInvisible();
				}
			}, delegate(float x)
			{
				this.canvasGroup.alpha = x;
				this.transform.localScale = Vector3.Lerp(new Vector3(0.6f, 0.6f, 1f), Vector3.one, x);
			});
			Action<float> setFunc = delegate(float x)
			{
				this.selectedLerp = x;
				this.maskedSprite.borders[this.maskedSprite.borders.Length - 1].width = 0.5f * x;
				this.maskedSprite.SetDirty();
			};
			TargetAnimator<float> selectedAnim = new TargetAnimator<float>(() => this.selectedLerp, setFunc, this.rootState, new LerpTowards(14f, 0.2f));
			AgentState agentState = this.selectedState;
			agentState.OnActivate = (Action)Delegate.Combine(agentState.OnActivate, new Action(delegate()
			{
				selectedAnim.SetTarget(1f, null, null, null, 0f, null);
			}));
			AgentState agentState2 = this.selectedState;
			agentState2.OnDeactivate = (Action)Delegate.Combine(agentState2.OnDeactivate, new Action(delegate()
			{
				selectedAnim.SetTarget(0f, null, null, null, 0f, null);
			}));
			selectedAnim.SetCurrent(0f);
			this.scrollItem = base.GetComponent<ScrollGridItem>();
			if (this.scrollItem)
			{
				this.scrollItem.MaybeInitialize();
				TargetAnimator marginAnim = this.scrollItem.marginAnim;
				marginAnim.setFunc = (Action<float>)Delegate.Combine(marginAnim.setFunc, new Action<float>(delegate(float x)
				{
					this.maskedSprite.transform.localScale = Vector3.Lerp(Vector3.one, new Vector3(0.8f, 0.8f, 0.6f), Mathf.Abs(x));
				}));
				TargetAnimator<Vector2> moveAnim = this.scrollItem.moveAnim;
				moveAnim.setFunc = (Action<Vector2>)Delegate.Combine(moveAnim.setFunc, new Action<Vector2>(delegate(Vector2 x)
				{
					this.onDirty();
				}));
			}
			AnimatedState animatedState2 = this.visibleState;
			animatedState2.OnDeactivate = (Action)Delegate.Combine(animatedState2.OnDeactivate, new Action(delegate()
			{
				this.selectedState.SetActive(false);
				if (this.scrollItem && this.scrollItem.grid)
				{
					this.scrollItem.grid.RemoveItem(this.scrollItem);
				}
			}));
			this.clickable = base.GetComponent<UIClickable>();
			this.clickable.onClick += this.Clicked;
			this.clickable.onStateChanged += this.Clickable_onStateChanged;
			this.navigable = base.GetComponent<UINavigable>();
			this.navigable.onConsumedNavigation += this.OnConsumedNavigation;
			this.buttonStates = new AgentState("ButtonStates", this.rootState, false, false);
			this.buttonDown = new AgentState("ButtonDown", this.buttonStates, false, true);
			TargetAnimator<float> scaleAnim = new TargetAnimator<float>(() => this.transform.localScale.x, delegate(float x)
			{
				this.transform.localScale = Vector3.one * x;
			}, this.rootState, new LerpTowards(14f, 0.2f));
			AgentState agentState3 = this.buttonDown;
			agentState3.OnChange = (Action<bool>)Delegate.Combine(agentState3.OnChange, new Action<bool>(delegate(bool x)
			{
				scaleAnim.SetTarget((!x) ? 1f : 0.95f, null, null, null, 0f, null);
			}));
			this.hoverState = new AgentState("Hover", this.buttonStates, false, true);
			TargetAnimator<float> brightnessAnim = new TargetAnimator<float>(() => this.maskedSprite.brightness, delegate(float x)
			{
				this.maskedSprite.brightness = x;
			}, this.rootState, new LerpTowards(14f, 0.2f));
			AgentState agentState4 = this.hoverState;
			agentState4.OnChange = (Action<bool>)Delegate.Combine(agentState4.OnChange, new Action<bool>(delegate(bool x)
			{
				brightnessAnim.SetTarget((!x) ? 1f : 1.3f, null, null, null, 0f, null);
			}));
			AgentState agentState5 = this.hoverState;
			agentState5.OnChange = (Action<bool>)Delegate.Combine(agentState5.OnChange, new Action<bool>(delegate(bool x)
			{
				scaleAnim.SetTarget((!x) ? 1f : 1.05f, null, null, null, 0f, null);
			}));
			this.OnInitialize();
		}

		// Token: 0x06003FA3 RID: 16291 RVA: 0x00120CC0 File Offset: 0x0011F0C0
		private void Clickable_onStateChanged(UIInteractable.State state)
		{
			if (state != UIInteractable.State.Hover)
			{
				if (state != UIInteractable.State.PointerButtonDown)
				{
					this.buttonStates.SetActive(false);
				}
				else
				{
					this.buttonDown.SetActive(true);
				}
			}
			else
			{
				this.hoverState.SetActive(true);
			}
		}

		// Token: 0x06003FA4 RID: 16292
		protected abstract void OnInitialize();

		// Token: 0x06003FA5 RID: 16293
		protected abstract void OnClicked();

		// Token: 0x06003FA6 RID: 16294
		protected abstract void OnConsumedNavigation(Vector2 dir);

		// Token: 0x06003FA7 RID: 16295 RVA: 0x00120D16 File Offset: 0x0011F116
		public void StartRemove()
		{
			this.visibleState.SetActive(false);
		}

		// Token: 0x06003FA8 RID: 16296 RVA: 0x00120D25 File Offset: 0x0011F125
		public void ReturnToPool()
		{
			if (!this.inPool)
			{
				this.pool.ReturnToPool(this);
			}
		}

		// Token: 0x06003FA9 RID: 16297 RVA: 0x00120D3E File Offset: 0x0011F13E
		public void Clicked()
		{
			if (this.visibleState.active)
			{
				this.OnClicked();
			}
		}

		// Token: 0x06003FAA RID: 16298 RVA: 0x00120D56 File Offset: 0x0011F156
		private void Update()
		{
			this.stateRoot.Update();
		}

		// Token: 0x06003FAB RID: 16299 RVA: 0x00120D63 File Offset: 0x0011F163
		private void OnDestroy()
		{
			this.stateRoot.OnDestroy();
		}

		// Token: 0x06003FAC RID: 16300 RVA: 0x00120D70 File Offset: 0x0011F170
		void IPoolable.SetPool<T>(LocalPool<T> pool)
		{
			this.pool = (pool as LocalPool<SelectableToken>);
			this.MaybeInitialize();
		}

		// Token: 0x06003FAD RID: 16301 RVA: 0x00120D89 File Offset: 0x0011F189
		void IPoolable.OnRemoved()
		{
			base.gameObject.SetActive(true);
			this.visibleState.SetActive(true);
			this.inPool = false;
		}

		// Token: 0x06003FAE RID: 16302 RVA: 0x00120DAC File Offset: 0x0011F1AC
		void IPoolable.OnReturned()
		{
			if (this.scrollItem && this.scrollItem.grid)
			{
				this.scrollItem.grid.RemoveItem(this.scrollItem);
			}
			this.inPool = true;
			this.onReturnToPool();
			base.gameObject.SetActive(false);
		}

		// Token: 0x04002C97 RID: 11415
		[SerializeField]
		protected AgentStateRoot stateRoot = new AgentStateRoot(4);

		// Token: 0x04002C98 RID: 11416
		public AgentState selectedState;

		// Token: 0x04002C99 RID: 11417
		public AnimatedState visibleState;

		// Token: 0x04002C9A RID: 11418
		[SerializeField]
		protected MaskedSprite maskedSprite;

		// Token: 0x04002C9B RID: 11419
		private CanvasGroup canvasGroup;

		// Token: 0x04002C9C RID: 11420
		[Header("Coin")]
		[SerializeField]
		public CoinGraphic coin;

		// Token: 0x04002C9D RID: 11421
		[SerializeField]
		protected SuperUpgradeMenu menu;

		// Token: 0x04002CA0 RID: 11424
		private LocalPool<SelectableToken> pool;

		// Token: 0x04002CA1 RID: 11425
		private float selectedLerp;

		// Token: 0x04002CA2 RID: 11426
		public Action onReturnToPool = delegate()
		{
		};

		// Token: 0x04002CA3 RID: 11427
		public Action onInvisible = delegate()
		{
		};

		// Token: 0x04002CA4 RID: 11428
		public Action onDirty = delegate()
		{
		};

		// Token: 0x04002CA6 RID: 11430
		private bool initialized;

		// Token: 0x04002CA7 RID: 11431
		private AgentState hoverState;

		// Token: 0x04002CA8 RID: 11432
		private AgentState buttonDown;

		// Token: 0x04002CA9 RID: 11433
		private AgentState buttonStates;

		// Token: 0x04002CAA RID: 11434
		private bool inPool = true;
	}
}
