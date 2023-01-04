using System;
using UnityEngine;
using Voxels.TowerDefense.ScriptAnimations;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x02000902 RID: 2306
	internal class UpgradeCarouselItemIcon : UpgradeCarouselItem
	{
		// Token: 0x06003DA0 RID: 15776 RVA: 0x00114738 File Offset: 0x00112B38
		protected override void Init()
		{
			base.Init();
			this.maskedSprite = base.GetComponentInChildren<MaskedSprite>(true);
			this.iconRectTransform = ((!this.maskedSprite) ? null : ((RectTransform)this.maskedSprite.transform));
			this.canvas = base.GetComponent<Canvas>();
			this.canvasGroup = base.GetComponent<CanvasGroup>();
			this.unfocusBorderSettings = (this.focusBorderSettings = this.maskedSprite.borders[1]);
			this.unfocusBorderSettings.width = 0f;
			this.unfocusBorderSettings.outlineWidth = 0f;
			this.maskedSprite.borders[1] = this.unfocusBorderSettings;
			TargetAnimator<float> anim = this.focus.anim;
			anim.setFunc = (Action<float>)Delegate.Combine(anim.setFunc, new Action<float>(delegate(float a)
			{
				this.BorderFunc();
			}));
			Action<float> b = delegate(float a)
			{
				this.BrightnessFunc();
			};
			TargetAnimator<float> anim2 = this.focus.anim;
			anim2.setFunc = (Action<float>)Delegate.Combine(anim2.setFunc, b);
			TargetAnimator<float> anim3 = this.hover.anim;
			anim3.setFunc = (Action<float>)Delegate.Combine(anim3.setFunc, b);
			TargetAnimator flash = this.flash;
			flash.setFunc = (Action<float>)Delegate.Combine(flash.setFunc, b);
			TargetAnimator<float> anim4 = this.buttonDown.anim;
			anim4.setFunc = (Action<float>)Delegate.Combine(anim4.setFunc, b);
			Action<float> b2 = delegate(float a)
			{
				this.ScaleFunc();
			};
			TargetAnimator<float> anim5 = this.hover.anim;
			anim5.setFunc = (Action<float>)Delegate.Combine(anim5.setFunc, b2);
			TargetAnimator<float> anim6 = this.buttonDown.anim;
			anim6.setFunc = (Action<float>)Delegate.Combine(anim6.setFunc, b2);
			TargetAnimator flash2 = this.flash;
			flash2.setFunc = (Action<float>)Delegate.Combine(flash2.setFunc, b2);
		}

		// Token: 0x06003DA1 RID: 15777 RVA: 0x00114920 File Offset: 0x00112D20
		protected override void SetUpgrade(HeroUpgradeDefinition upgradeDef, bool isAvailable)
		{
			if (upgradeDef)
			{
				this.maskedSprite.Set(upgradeDef, 0);
			}
			else
			{
				base.gameObject.SetActive(false);
			}
			this.maskedSprite.saturation = ((!isAvailable) ? 0.5f : 1f);
			this.maskedSprite.brightness = ((!isAvailable) ? 0.6f : 1f);
			this.canvas.overrideSorting = true;
			this.canvas.sortingOrder = (this.defaultSortOrder = base.transform.parent.GetComponentInParent<Canvas>().sortingOrder + 1);
			this.isAvailable = isAvailable;
		}

		// Token: 0x06003DA2 RID: 15778 RVA: 0x001149D4 File Offset: 0x00112DD4
		public override void Animate(float currentPos, float targetPos, bool snap)
		{
			base.Animate(currentPos, targetPos, snap);
			float num = this.animPos * this.animPos;
			bool flag = ((float)base.pos - currentPos) * ((float)base.pos - targetPos) <= 0f;
			this.rectTransform.localPosition = this.rectTransform.localPosition.SetX(this.animPos * 0.6f * this.rectTransform.rect.width);
			this.canvas.sortingOrder = ((!flag) ? this.defaultSortOrder : (this.defaultSortOrder + 1));
			this.canvasGroup.alpha = 1f - num;
			this.BorderFunc();
			this.BrightnessFunc();
			this.ScaleFunc();
		}

		// Token: 0x06003DA3 RID: 15779 RVA: 0x00114A9C File Offset: 0x00112E9C
		private void BorderFunc()
		{
			float num = Mathf.InverseLerp(0.5f, 1f, 1f - Mathf.Abs(this.animPos));
			this.maskedSprite.borders[1] = MaskedSprite.BorderSettings.Lerp(this.unfocusBorderSettings, this.focusBorderSettings, this.focus.anim.current * num * num);
			this.maskedSprite.SetDirty();
		}

		// Token: 0x06003DA4 RID: 15780 RVA: 0x00114B10 File Offset: 0x00112F10
		private void BrightnessFunc()
		{
			float num = 0f;
			num += 0.05f * this.focus.anim.current;
			num += 0.2f * this.hover.anim.current;
			num -= 0.1f * this.buttonDown.anim.current;
			num += 0.4f * base.flashValue;
			num *= Mathf.InverseLerp(0.5f, 1f, 1f - Mathf.Abs(this.animPos));
			num += 1f;
			this.maskedSprite.transform.localScale = this.maskedSprite.transform.localScale.SetZ(num);
		}

		// Token: 0x06003DA5 RID: 15781 RVA: 0x00114BD0 File Offset: 0x00112FD0
		private void ScaleFunc()
		{
			float num = 0f;
			num += this.hover.anim.current * 0.03f - this.buttonDown.anim.current * 0.08f;
			num *= Mathf.InverseLerp(0.5f, 1f, 1f - Mathf.Abs(this.animPos));
			float num2 = base.flashValue * 0.1f;
			float value = Mathf.Min(1.15f, num + num2 + 1f) * (1f - this.animPos * this.animPos * 0.5f);
			this.iconRectTransform.localScale = this.iconRectTransform.localScale.SetX(value).SetY(value);
		}

		// Token: 0x04002B06 RID: 11014
		[SerializeField]
		private Sprite noItemSprite;

		// Token: 0x04002B07 RID: 11015
		[SerializeField]
		private Sprite noItemMask;

		// Token: 0x04002B08 RID: 11016
		[SerializeField]
		private HeroUpgradeDefinition emptyDef;

		// Token: 0x04002B09 RID: 11017
		private MaskedSprite maskedSprite;

		// Token: 0x04002B0A RID: 11018
		private RectTransform iconRectTransform;

		// Token: 0x04002B0B RID: 11019
		private Canvas canvas;

		// Token: 0x04002B0C RID: 11020
		private CanvasGroup canvasGroup;

		// Token: 0x04002B0D RID: 11021
		private int defaultSortOrder;

		// Token: 0x04002B0E RID: 11022
		private MaskedSprite.BorderSettings focusBorderSettings;

		// Token: 0x04002B0F RID: 11023
		private MaskedSprite.BorderSettings unfocusBorderSettings;
	}
}
