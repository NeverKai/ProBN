using System;
using I2.Loc;
using RTM.Pools;
using UnityEngine;
using UnityEngine.UI;
using Voxels.TowerDefense.ScriptAnimations;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x02000903 RID: 2307
	internal class UpgradeCarouselItemName : UpgradeCarouselItem, IPoolable
	{
		// Token: 0x06003DAA RID: 15786 RVA: 0x00114CF4 File Offset: 0x001130F4
		protected override void Init()
		{
			base.Init();
			this.label = base.GetComponentInChildren<Localize>();
			this.text = this.label.GetComponent<Text>();
			this.textColor = this.text.color;
			this.focusColor = this.textColor.SetA(1f);
			this.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, ((RectTransform)this.rectTransform.parent).rect.width);
			this.focus.anim.SetAnimFuncs(UpgradeCarouselItemName.animFuncs);
			this.hover.anim.SetAnimFuncs(UpgradeCarouselItemName.animFuncs);
			this.buttonDown.anim.SetAnimFuncs(UpgradeCarouselItemName.animFuncs);
			this.flash.SetAnimFuncs(UpgradeCarouselItemName.flashFuncs);
			Action<float> b = delegate(float a)
			{
				this.ColorFunc();
			};
			TargetAnimator<float> anim = this.focus.anim;
			anim.setFunc = (Action<float>)Delegate.Combine(anim.setFunc, b);
			TargetAnimator<float> anim2 = this.hover.anim;
			anim2.setFunc = (Action<float>)Delegate.Combine(anim2.setFunc, b);
			TargetAnimator<float> anim3 = this.buttonDown.anim;
			anim3.setFunc = (Action<float>)Delegate.Combine(anim3.setFunc, b);
			TargetAnimator flash = this.flash;
			flash.setFunc = (Action<float>)Delegate.Combine(flash.setFunc, b);
		}

		// Token: 0x06003DAB RID: 15787 RVA: 0x00114E50 File Offset: 0x00113250
		private void ColorFunc()
		{
			float t = Mathf.Max(this.focus.anim.current, Mathf.Max(this.hover.anim.current, this.buttonDown.anim.current));
			Color color = Color.Lerp(this.textColor, this.focusColor, t);
			float num = Mathf.Clamp(1f - this.animPos * this.animPos, 0f, 1f);
			num *= ((!this.isAvailable) ? 0.75f : 1f);
			color.a *= num;
			color = Color.Lerp(color, Color.white, base.flashValue);
			this.text.color = color;
		}

		// Token: 0x06003DAC RID: 15788 RVA: 0x00114F18 File Offset: 0x00113318
		protected override void SetUpgrade(HeroUpgradeDefinition upgradeDef, bool isAvailable)
		{
			this.label.Term = ((!upgradeDef) ? this.emptyString : upgradeDef.nameTerm);
			this.text.color = this.textColor;
		}

		// Token: 0x06003DAD RID: 15789 RVA: 0x00114F52 File Offset: 0x00113352
		public override void Animate(float currentPos, float targetPos, bool snap)
		{
			base.Animate(currentPos, targetPos, snap);
			this.rectTransform.pivot = new Vector2(-this.animPos * this.travelDist + 0.5f, 0.5f);
			this.ColorFunc();
		}

		// Token: 0x04002B10 RID: 11024
		[SerializeField]
		[TermsPopup("")]
		private string emptyString = string.Empty;

		// Token: 0x04002B11 RID: 11025
		[SerializeField]
		[Range(0f, 1f)]
		private float travelDist = 0.5f;

		// Token: 0x04002B12 RID: 11026
		private Localize label;

		// Token: 0x04002B13 RID: 11027
		private Text text;

		// Token: 0x04002B14 RID: 11028
		private Color textColor = default(Color);

		// Token: 0x04002B15 RID: 11029
		private Color focusColor = default(Color);

		// Token: 0x04002B16 RID: 11030
		private RectTransform labelRectTransform;

		// Token: 0x04002B17 RID: 11031
		public static readonly LerpTowards animFuncs = new LerpTowards(16f, 4f);

		// Token: 0x04002B18 RID: 11032
		public static readonly LerpTowards flashFuncs = new LerpTowards(8f, 0.1f);
	}
}
