using System;
using System.Collections.Generic;
using UnityEngine;
using Voxels.TowerDefense.ScriptAnimations;

namespace Voxels.TowerDefense.UI.UpgradeScreen
{
	// Token: 0x02000938 RID: 2360
	public class SlotToken : SelectableToken
	{
		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x06003FB3 RID: 16307 RVA: 0x001217D5 File Offset: 0x0011FBD5
		public HeroUpgradeType upgradeType
		{
			get
			{
				return this.upgradeTypes[0];
			}
		}

		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x06003FB4 RID: 16308 RVA: 0x001217E3 File Offset: 0x0011FBE3
		public HeroUpgradeTypeEnum typeEnum
		{
			get
			{
				return this.upgradeType.typeEnum;
			}
		}

		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x06003FB5 RID: 16309 RVA: 0x001217F0 File Offset: 0x0011FBF0
		public SerializableHeroUpgrade heroUpgrade
		{
			get
			{
				return this.portrait.heroDef.GetUpgrade(this.typeEnum);
			}
		}

		// Token: 0x06003FB6 RID: 16310 RVA: 0x00121808 File Offset: 0x0011FC08
		protected override void OnInitialize()
		{
			this.flairAnimator = base.GetComponentInChildren<Animator>(true);
			this.onReturnToPool = (Action)Delegate.Combine(this.onReturnToPool, new Action(delegate()
			{
				base.transform.localPosition = base.transform.localPosition.SetX(this.startX);
				TargetAnimator<Vector2> moveAnim = this.portrait.scrollItem.moveAnim;
				moveAnim.setFunc = (Action<Vector2>)Delegate.Remove(moveAnim.setFunc, new Action<Vector2>(this.FollowPortrait));
			}));
			this.onInvisible = (Action)Delegate.Combine(this.onInvisible, new Action(delegate()
			{
				base.ReturnToPool();
			}));
		}

		// Token: 0x06003FB7 RID: 16311 RVA: 0x00121866 File Offset: 0x0011FC66
		private void Awake()
		{
			base.MaybeInitialize();
		}

		// Token: 0x06003FB8 RID: 16312 RVA: 0x0012186E File Offset: 0x0011FC6E
		public void PlayFlair()
		{
			this.flairAnimator.Play(SlotToken.flairId, 0, 0f);
		}

		// Token: 0x06003FB9 RID: 16313 RVA: 0x0012188C File Offset: 0x0011FC8C
		public SlotToken Setup(SuperUpgradeMenu menu, PortraitToken portrait)
		{
			base.MaybeInitialize();
			this.portrait = portrait;
			this.maskedSprite.borders[0].color = (portrait.heroDef.color * 1.2f).SetA(1f);
			this.maskedSprite.image.color = (portrait.heroDef.color * 0.85f).SetA(1f);
			this.startX = base.transform.localPosition.x;
			TargetAnimator<Vector2> moveAnim = portrait.scrollItem.moveAnim;
			moveAnim.setFunc = (Action<Vector2>)Delegate.Combine(moveAnim.setFunc, new Action<Vector2>(this.FollowPortrait));
			this.UpdateUpgrade();
			this.FollowPortrait(portrait.transform.localPosition);
			base.transform.SetAsLastSibling();
			base.navigable.focusAudio = SlotToken.focusAudio;
			return this;
		}

		// Token: 0x06003FBA RID: 16314 RVA: 0x00121988 File Offset: 0x0011FD88
		public void UpdateUpgrade()
		{
			if (this.heroUpgrade == null)
			{
				this.contentSprite.gameObject.SetActive(false);
			}
			else
			{
				this.contentSprite.gameObject.SetActive(true);
				this.contentSprite.Set(this.heroUpgrade);
				this.maskedSprite.SetDirty();
			}
		}

		// Token: 0x06003FBB RID: 16315 RVA: 0x001219E4 File Offset: 0x0011FDE4
		private void FollowPortrait(Vector2 pos)
		{
			pos = this.portrait.transform.parent.TransformPoint(pos);
			pos = base.transform.parent.InverseTransformPoint(pos);
			base.transform.localPosition = base.transform.localPosition.SetX(this.startX + pos.x * 0.2f);
			this.onDirty();
		}

		// Token: 0x06003FBC RID: 16316 RVA: 0x00121A6A File Offset: 0x0011FE6A
		protected override void OnClicked()
		{
			this.menu.OnClicked(this);
		}

		// Token: 0x06003FBD RID: 16317 RVA: 0x00121A78 File Offset: 0x0011FE78
		protected override void OnConsumedNavigation(Vector2 dir)
		{
			this.menu.OnConsumedNavigation(this, dir);
		}

		// Token: 0x04002CAE RID: 11438
		private static FabricEventReference focusAudio = "UI/Upgrade/Hover";

		// Token: 0x04002CAF RID: 11439
		[SerializeField]
		private MaskedSprite contentSprite;

		// Token: 0x04002CB0 RID: 11440
		private PortraitToken portrait;

		// Token: 0x04002CB1 RID: 11441
		private Animator flairAnimator;

		// Token: 0x04002CB2 RID: 11442
		private static AnimId flairId = "Flair";

		// Token: 0x04002CB3 RID: 11443
		[SerializeField]
		public List<HeroUpgradeType> upgradeTypes;

		// Token: 0x04002CB4 RID: 11444
		public List<UpgradeToken> avaliableUpgradeTokens = new List<UpgradeToken>();

		// Token: 0x04002CB5 RID: 11445
		public List<UpgradeCurve> curves = new List<UpgradeCurve>();

		// Token: 0x04002CB6 RID: 11446
		private float startX;
	}
}
