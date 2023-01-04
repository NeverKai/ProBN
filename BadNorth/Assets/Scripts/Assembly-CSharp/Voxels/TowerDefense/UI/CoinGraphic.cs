using System;
using RTM.Utilities;
using UnityEngine;
using UnityEngine.UI;
using Voxels.TowerDefense.ScriptAnimations;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x0200091B RID: 2331
	public class CoinGraphic : MonoBehaviour
	{
		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x06003E6A RID: 15978 RVA: 0x001198E4 File Offset: 0x00117CE4
		protected AgentState rootState
		{
			get
			{
				return this.root.rootState;
			}
		}

		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x06003E6B RID: 15979 RVA: 0x001198F1 File Offset: 0x00117CF1
		// (set) Token: 0x06003E6C RID: 15980 RVA: 0x001198F9 File Offset: 0x00117CF9
		public int number
		{
			get
			{
				return this._coinNumber;
			}
			set
			{
				this._coinNumber = value;
				this.MaybeInitialize();
				this.text.text = IntStringCache.GetClean(value);
				this.visible.SetActive(value > 0);
			}
		}

		// Token: 0x06003E6D RID: 15981 RVA: 0x0011992C File Offset: 0x00117D2C
		public void MaybeInitialize()
		{
			if (this.initialized)
			{
				return;
			}
			this.initialized = true;
			this.neutral = new AnimatedState("Neutral", this.rootState, false, true);
			this.bouncy = new AnimatedState("Bouncy", this.rootState, false, true);
			this.unaffordable = new AnimatedState("Unaffordable", this.rootState, false, true);
			this.visible = new AnimatedState("Visible", this.rootState, this._coinNumber > 0, false);
			this.discount = new AnimatedState("Discount", this.rootState, false, false);
			if (this.discountGraphic)
			{
				this.discount.Subscribe(delegate(bool x)
				{
					this.discountGraphic.gameObject.SetActive(x);
				}, delegate(float x)
				{
					this.discountGraphic.fraction = Mathf.Lerp(1f, 0.5f, x);
					this.discountGraphic.transform.localScale = new Vector3(x, x, 1f);
					base.transform.rotation = Quaternion.Euler(0f, 0f, x * -10f);
				});
			}
			this.visible.Subscribe(new Action<bool>(base.transform.gameObject.SetActive), delegate(float x)
			{
				base.transform.transform.localScale = new Vector3(x, x, 1f);
			});
			this.scaleAnim1 = new TargetAnimator("Scale", () => this.maskedSprite.transform.localScale.x, delegate(float x)
			{
				this.maskedSprite.transform.localScale = new Vector3(x, x, this.maskedSprite.transform.localScale.z);
			}, this.rootState, LerpTowards.standard);
			this.yAnim = new TargetAnimator("PosY", () => this.maskedSprite.transform.localPosition.y, delegate(float x)
			{
				this.maskedSprite.transform.localPosition = this.maskedSprite.transform.localPosition.SetY(x);
			}, this.rootState, LerpTowards.standard);
			this.saturationAnim = new TargetAnimator("Saturation", () => this.maskedSprite.shaderSettings.saturation, delegate(float x)
			{
				this.maskedSprite.shaderSettings.saturation = x;
				this.maskedSprite.SetDirty();
			}, this.rootState, LerpTowards.standard);
			this.brightnessAnim = new TargetAnimator("brightness", () => this.maskedSprite.transform.localScale.z, delegate(float x)
			{
				this.maskedSprite.transform.localScale = this.maskedSprite.transform.localScale.SetZ(x);
			}, this.rootState, LerpTowards.standard);
			this.brightnessAnim.SetTargetOrCurrent(1f);
			this.bouncy.OnUpdate += delegate()
			{
				this.yAnim.SetTargetOrCurrent(CoinGraphic.pulse * 2f);
			};
			AnimatedState animatedState = this.bouncy;
			animatedState.OnActivate = (Action)Delegate.Combine(animatedState.OnActivate, new Action(delegate()
			{
				this.PulseHighlight();
			}));
			AnimatedState animatedState2 = this.bouncy;
			animatedState2.OnDeactivate = (Action)Delegate.Combine(animatedState2.OnDeactivate, new Action(delegate()
			{
				this.yAnim.SetTarget(0f, null, null, null, 0f, null);
			}));
			AnimatedState animatedState3 = this.neutral;
			animatedState3.OnActivate = (Action)Delegate.Combine(animatedState3.OnActivate, new Action(delegate()
			{
				this.scaleAnim1.SetTarget(1f, null, null, null, 0f, null);
			}));
			AnimatedState animatedState4 = this.unaffordable;
			animatedState4.OnChange = (Action<bool>)Delegate.Combine(animatedState4.OnChange, new Action<bool>(delegate(bool x)
			{
				this.saturationAnim.SetTarget((!x) ? 1f : 0.5f, null, null, null, 0f, null);
			}));
			AnimatedState animatedState5 = this.unaffordable;
			animatedState5.OnChange = (Action<bool>)Delegate.Combine(animatedState5.OnChange, new Action<bool>(delegate(bool x)
			{
				this.scaleAnim1.SetTarget((!x) ? 1f : 0.9f, null, null, null, 0f, null);
			}));
			AgentState rootState = this.rootState;
			rootState.OnEmpty = (Action)Delegate.Combine(rootState.OnEmpty, new Action(delegate()
			{
				if (this.number > 0)
				{
					this.neutral.SetActiveTrue();
				}
			}));
			this.number = this._coinNumber;
			this.neutral.SetActive(true);
		}

		// Token: 0x06003E6E RID: 15982 RVA: 0x00119C22 File Offset: 0x00118022
		public void SetAffordable(bool affordable)
		{
			if (this.number > 0)
			{
				if (affordable)
				{
					this.bouncy.SetActive(true);
					this.PulseHighlight();
				}
				else
				{
					this.unaffordable.SetActive(true);
				}
			}
		}

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x06003E6F RID: 15983 RVA: 0x00119C5B File Offset: 0x0011805B
		public static float pulse
		{
			get
			{
				return -Mathf.Sin(Time.unscaledTime * 3.1415927f * 4f) * Mathf.Clamp01(Mathf.PingPong(Time.unscaledTime * 2f, 1f) * 2f - 1f);
			}
		}

		// Token: 0x06003E70 RID: 15984 RVA: 0x00119C9B File Offset: 0x0011809B
		public void PulseHighlight()
		{
			if (this.brightnessAnim != null)
			{
				this.brightnessAnim.SetCurrent(1.5f);
				this.brightnessAnim.ForceActive();
			}
		}

		// Token: 0x06003E71 RID: 15985 RVA: 0x00119CC8 File Offset: 0x001180C8
		private void Update()
		{
			this.root.Update();
		}

		// Token: 0x06003E72 RID: 15986 RVA: 0x00119CD5 File Offset: 0x001180D5
		public void ForceAnimations()
		{
			this.MaybeInitialize();
			this.scaleAnim1.ForceToTarget();
			this.visible.anim.ForceToTarget();
		}

		// Token: 0x06003E73 RID: 15987 RVA: 0x00119CF8 File Offset: 0x001180F8
		private void Awake()
		{
			this.MaybeInitialize();
		}

		// Token: 0x06003E74 RID: 15988 RVA: 0x00119D00 File Offset: 0x00118100
		private void OnDestroy()
		{
			this.rootState.OnDestroy();
		}

		// Token: 0x04002BAE RID: 11182
		[SerializeField]
		private Text text;

		// Token: 0x04002BAF RID: 11183
		[SerializeField]
		private MaskedSprite maskedSprite;

		// Token: 0x04002BB0 RID: 11184
		[SerializeField]
		protected AgentStateRoot root = new AgentStateRoot(4);

		// Token: 0x04002BB1 RID: 11185
		public AnimatedState bouncy;

		// Token: 0x04002BB2 RID: 11186
		public AnimatedState neutral;

		// Token: 0x04002BB3 RID: 11187
		public AnimatedState unaffordable;

		// Token: 0x04002BB4 RID: 11188
		public AnimatedState visible;

		// Token: 0x04002BB5 RID: 11189
		public AnimatedState discount;

		// Token: 0x04002BB6 RID: 11190
		[SerializeField]
		private DistanceFieldSettings discountGraphic;

		// Token: 0x04002BB7 RID: 11191
		[SerializeField]
		private int _coinNumber = -1;

		// Token: 0x04002BB8 RID: 11192
		public TargetAnimator scaleAnim1;

		// Token: 0x04002BB9 RID: 11193
		public TargetAnimator saturationAnim;

		// Token: 0x04002BBA RID: 11194
		public TargetAnimator brightnessAnim;

		// Token: 0x04002BBB RID: 11195
		public TargetAnimator yAnim;

		// Token: 0x04002BBC RID: 11196
		private float posY;

		// Token: 0x04002BBD RID: 11197
		private bool initialized;
	}
}
