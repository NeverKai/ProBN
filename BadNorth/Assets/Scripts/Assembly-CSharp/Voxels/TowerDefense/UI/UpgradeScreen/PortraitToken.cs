using System;
using I2.Loc;
using UnityEngine;
using UnityEngine.UI;
using Voxels.TowerDefense.ScriptAnimations;

namespace Voxels.TowerDefense.UI.UpgradeScreen
{
	// Token: 0x02000936 RID: 2358
	public class PortraitToken : SelectableToken, IComparable<PortraitToken>
	{
		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x06003F90 RID: 16272 RVA: 0x001210FC File Offset: 0x0011F4FC
		// (set) Token: 0x06003F91 RID: 16273 RVA: 0x00121104 File Offset: 0x0011F504
		public HeroDefinition heroDef { get; private set; }

		// Token: 0x06003F92 RID: 16274 RVA: 0x00121110 File Offset: 0x0011F510
		protected override void OnInitialize()
		{
			CampaignMapUI.nextTurnRotators.Add(this.fatigueIcon.transform);
			this.fatigueState = new AnimatedState("Fatigue", base.rootState, false, false);
			this.deadState = new AnimatedState("Dead", base.rootState, false, false);
			this.grayState = new AnimatedState("Gray", base.rootState, false, false);
			this.nameLocalize = this.nameText.GetComponent<Localize>();
			TargetAnimator visAnim = base.scrollItem.visAnim;
			visAnim.setFunc = (Action<float>)Delegate.Combine(visAnim.setFunc, new Action<float>(delegate(float x)
			{
				this.nameText.color = this.nameText.color.SetA(x * x);
				this.nameText.transform.localScale = Vector3.one * (x + 1f) * 0.5f;
			}));
			this.fatigueState.Subscribe(delegate(bool x)
			{
				this.fatigueIcon.gameObject.SetActive(x);
			}, delegate(float x)
			{
				this.fatigueIcon.color = this.fatigueIcon.color.SetA(x);
				this.maskedSprite.spriteScale = Mathf.Lerp(0.87f, 0.82f, x);
				this.maskedSprite.SetDirty();
			});
			this.cornucopiaStates = new AnimatedState[this.cornucopiaIcons.Length];
			for (int i = 0; i < this.cornucopiaIcons.Length; i++)
			{
				this.cornucopiaStates[i] = new AnimatedState("Cornucopia " + i, base.rootState, false, false);
				this.cornucopiaStates[i].Subscribe(this.cornucopiaIcons[i]);
			}
			this.grayState.anim.Subscribe(delegate(float x)
			{
				this.maskedSprite.saturation = 1f - x;
			});
			this.onReturnToPool = (Action)Delegate.Combine(this.onReturnToPool, new Action(delegate()
			{
				this.heroDef = null;
			}));
			float borderSize = this.maskedSprite.borders[0].width;
			TargetAnimator<float> borderAnim = new TargetAnimator<float>("BorderAnim", () => this.maskedSprite.borders[0].width, delegate(float x)
			{
				MaskedSprite.BorderSettings borderSettings = this.maskedSprite.borders[0];
				borderSettings.width = x;
				this.maskedSprite.borders[0] = borderSettings;
				this.maskedSprite.SetDirty();
			}, base.rootState, LerpTowards.standard);
			AnimatedState animatedState = this.deadState;
			animatedState.OnChange = (Action<bool>)Delegate.Combine(animatedState.OnChange, new Action<bool>(delegate(bool x)
			{
				borderAnim.SetTarget((!x) ? borderSize : 0f, null, null, null, 0f, null);
			}));
			TargetAnimator yAnim = new TargetAnimator("PosY", () => this.maskedSprite.transform.localPosition.y, delegate(float x)
			{
				this.maskedSprite.transform.localPosition = this.maskedSprite.transform.localPosition.SetY(x);
			}, base.rootState, LerpTowards.standard);
			this.upgradesAvailable = new AnimatedState("UpgradesAvailable", base.rootState, false, false);
			this.upgradesAvailable.OnUpdate += delegate()
			{
				yAnim.SetTargetOrCurrent(CoinGraphic.pulse * ((!this.menu.up.active) ? 1f : 2f));
			};
			AnimatedState animatedState2 = this.upgradesAvailable;
			animatedState2.OnDeactivate = (Action)Delegate.Combine(animatedState2.OnDeactivate, new Action(delegate()
			{
				yAnim.SetTarget(0f, null, null, null, 0f, null);
			}));
		}

		// Token: 0x06003F93 RID: 16275 RVA: 0x0012138A File Offset: 0x0011F78A
		private void SetGrayState(bool x)
		{
			this.grayState.SetActive(!this.menu.up.active && this.fatigueState.active);
		}

		// Token: 0x06003F94 RID: 16276 RVA: 0x001213BC File Offset: 0x0011F7BC
		public PortraitToken Setup(HeroDefinition heroDef, SuperUpgradeMenu menu)
		{
			base.MaybeInitialize();
			base.scrollItem.sortOrder = (float)heroDef.id;
			this.heroDef = heroDef;
			this.nameLocalize.Term = heroDef.nameTerm;
			if (heroDef.graphics != null)
			{
				this.maskedSprite.Set(heroDef.graphics);
			}
			base.navigable.suppressFocusAudio = true;
			this.RefreshVisuals();
			base.scrollItem.visAnim.SetCurrent(0f);
			return this;
		}

		// Token: 0x06003F95 RID: 16277 RVA: 0x00121440 File Offset: 0x0011F840
		[ContextMenu("RefreshVisuals")]
		public void RefreshVisuals()
		{
			this.fatigueState.SetActive(this.heroDef.fatigued);
			this.grayState.SetActive(this.heroDef.fatigued && !this.menu.up.active);
			for (int i = 0; i < this.cornucopiaStates.Length; i++)
			{
				this.cornucopiaStates[i].SetActive(this.heroDef.maxUsesPerTurn > 1 && i < (int)this.heroDef.maxUsesPerTurn && (int)this.heroDef.timesUsedThisTurn <= i);
			}
			this.deadState.SetActive(!this.heroDef.alive);
		}

		// Token: 0x06003F96 RID: 16278 RVA: 0x0012150D File Offset: 0x0011F90D
		protected override void OnClicked()
		{
			this.menu.OnClicked(this);
		}

		// Token: 0x06003F97 RID: 16279 RVA: 0x0012151B File Offset: 0x0011F91B
		protected override void OnConsumedNavigation(Vector2 dir)
		{
			this.menu.OnConsumedNavigation(this, dir);
		}

		// Token: 0x06003F98 RID: 16280 RVA: 0x0012152A File Offset: 0x0011F92A
		int IComparable<PortraitToken>.CompareTo(PortraitToken other)
		{
			return base.scrollItem.sortOrder.CompareTo(other.scrollItem.sortOrder);
		}

		// Token: 0x04002C8C RID: 11404
		[SerializeField]
		private Text nameText;

		// Token: 0x04002C8D RID: 11405
		private Localize nameLocalize;

		// Token: 0x04002C8F RID: 11407
		[SerializeField]
		private HeroInfo heroInfo;

		// Token: 0x04002C90 RID: 11408
		[SerializeField]
		private Image fatigueIcon;

		// Token: 0x04002C91 RID: 11409
		[SerializeField]
		private Image[] cornucopiaIcons;

		// Token: 0x04002C92 RID: 11410
		public AnimatedState upgradesAvailable;

		// Token: 0x04002C93 RID: 11411
		private AnimatedState fatigueState;

		// Token: 0x04002C94 RID: 11412
		private AnimatedState deadState;

		// Token: 0x04002C95 RID: 11413
		private AnimatedState grayState;

		// Token: 0x04002C96 RID: 11414
		private AnimatedState[] cornucopiaStates;
	}
}
