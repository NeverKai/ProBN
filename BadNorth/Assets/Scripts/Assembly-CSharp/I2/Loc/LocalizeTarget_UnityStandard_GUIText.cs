using System;
using UnityEngine;

namespace I2.Loc
{
	// Token: 0x020003FB RID: 1019
	public class LocalizeTarget_UnityStandard_GUIText : LocalizeTarget<GUIText>
	{
		// Token: 0x0600178E RID: 6030 RVA: 0x0003B8E4 File Offset: 0x00039CE4
		static LocalizeTarget_UnityStandard_GUIText()
		{
			LocalizeTarget_UnityStandard_GUIText.AutoRegister();
		}

		// Token: 0x06001790 RID: 6032 RVA: 0x0003B904 File Offset: 0x00039D04
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void AutoRegister()
		{
			LocalizationManager.RegisterTarget(new LocalizeTargetDesc_Type<GUIText, LocalizeTarget_UnityStandard_GUIText>
			{
				Name = "GUIText",
				Priority = 100
			});
		}

		// Token: 0x06001791 RID: 6033 RVA: 0x0003B930 File Offset: 0x00039D30
		public override eTermType GetPrimaryTermType(Localize cmp)
		{
			return eTermType.Text;
		}

		// Token: 0x06001792 RID: 6034 RVA: 0x0003B933 File Offset: 0x00039D33
		public override eTermType GetSecondaryTermType(Localize cmp)
		{
			return eTermType.Font;
		}

		// Token: 0x06001793 RID: 6035 RVA: 0x0003B936 File Offset: 0x00039D36
		public override bool CanUseSecondaryTerm()
		{
			return true;
		}

		// Token: 0x06001794 RID: 6036 RVA: 0x0003B939 File Offset: 0x00039D39
		public override bool AllowMainTermToBeRTL()
		{
			return true;
		}

		// Token: 0x06001795 RID: 6037 RVA: 0x0003B93C File Offset: 0x00039D3C
		public override bool AllowSecondTermToBeRTL()
		{
			return false;
		}

		// Token: 0x06001796 RID: 6038 RVA: 0x0003B940 File Offset: 0x00039D40
		public override void GetFinalTerms(Localize cmp, string Main, string Secondary, out string primaryTerm, out string secondaryTerm)
		{
			primaryTerm = ((!this.mTarget) ? null : this.mTarget.text);
			secondaryTerm = ((!string.IsNullOrEmpty(Secondary) || !(this.mTarget.font != null)) ? null : this.mTarget.font.name);
		}

		// Token: 0x06001797 RID: 6039 RVA: 0x0003B9AC File Offset: 0x00039DAC
		public override void DoLocalize(Localize cmp, string mainTranslation, string secondaryTranslation)
		{
			Font secondaryTranslatedObj = cmp.GetSecondaryTranslatedObj<Font>(ref mainTranslation, ref secondaryTranslation);
			if (secondaryTranslatedObj != null && this.mTarget.font != secondaryTranslatedObj)
			{
				this.mTarget.font = secondaryTranslatedObj;
			}
			if (this.mInitializeAlignment)
			{
				this.mInitializeAlignment = false;
				this.mAlignment_LTR = (this.mAlignment_RTL = this.mTarget.alignment);
				if (LocalizationManager.IsRight2Left && this.mAlignment_RTL == TextAlignment.Right)
				{
					this.mAlignment_LTR = TextAlignment.Left;
				}
				if (!LocalizationManager.IsRight2Left && this.mAlignment_LTR == TextAlignment.Left)
				{
					this.mAlignment_RTL = TextAlignment.Right;
				}
			}
			if (mainTranslation != null && this.mTarget.text != mainTranslation)
			{
				if (cmp.CorrectAlignmentForRTL && this.mTarget.alignment != TextAlignment.Center)
				{
					this.mTarget.alignment = ((!LocalizationManager.IsRight2Left) ? this.mAlignment_LTR : this.mAlignment_RTL);
				}
				this.mTarget.text = mainTranslation;
			}
		}

		// Token: 0x04000E8C RID: 3724
		private TextAlignment mAlignment_RTL = TextAlignment.Right;

		// Token: 0x04000E8D RID: 3725
		private TextAlignment mAlignment_LTR;

		// Token: 0x04000E8E RID: 3726
		private bool mAlignmentWasRTL;

		// Token: 0x04000E8F RID: 3727
		private bool mInitializeAlignment = true;
	}
}
