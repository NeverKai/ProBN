using System;
using UnityEngine;

namespace I2.Loc
{
	// Token: 0x02000401 RID: 1025
	public class LocalizeTarget_UnityStandard_TextMesh : LocalizeTarget<TextMesh>
	{
		// Token: 0x060017C4 RID: 6084 RVA: 0x0003BF9C File Offset: 0x0003A39C
		static LocalizeTarget_UnityStandard_TextMesh()
		{
			LocalizeTarget_UnityStandard_TextMesh.AutoRegister();
		}

		// Token: 0x060017C6 RID: 6086 RVA: 0x0003BFBC File Offset: 0x0003A3BC
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void AutoRegister()
		{
			LocalizationManager.RegisterTarget(new LocalizeTargetDesc_Type<TextMesh, LocalizeTarget_UnityStandard_TextMesh>
			{
				Name = "TextMesh",
				Priority = 100
			});
		}

		// Token: 0x060017C7 RID: 6087 RVA: 0x0003BFE8 File Offset: 0x0003A3E8
		public override eTermType GetPrimaryTermType(Localize cmp)
		{
			return eTermType.Text;
		}

		// Token: 0x060017C8 RID: 6088 RVA: 0x0003BFEB File Offset: 0x0003A3EB
		public override eTermType GetSecondaryTermType(Localize cmp)
		{
			return eTermType.Font;
		}

		// Token: 0x060017C9 RID: 6089 RVA: 0x0003BFEE File Offset: 0x0003A3EE
		public override bool CanUseSecondaryTerm()
		{
			return true;
		}

		// Token: 0x060017CA RID: 6090 RVA: 0x0003BFF1 File Offset: 0x0003A3F1
		public override bool AllowMainTermToBeRTL()
		{
			return true;
		}

		// Token: 0x060017CB RID: 6091 RVA: 0x0003BFF4 File Offset: 0x0003A3F4
		public override bool AllowSecondTermToBeRTL()
		{
			return false;
		}

		// Token: 0x060017CC RID: 6092 RVA: 0x0003BFF8 File Offset: 0x0003A3F8
		public override void GetFinalTerms(Localize cmp, string Main, string Secondary, out string primaryTerm, out string secondaryTerm)
		{
			primaryTerm = ((!this.mTarget) ? null : this.mTarget.text);
			secondaryTerm = ((!string.IsNullOrEmpty(Secondary) || !(this.mTarget.font != null)) ? null : this.mTarget.font.name);
		}

		// Token: 0x060017CD RID: 6093 RVA: 0x0003C064 File Offset: 0x0003A464
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
				this.mTarget.font.RequestCharactersInTexture(mainTranslation);
				this.mTarget.text = mainTranslation;
			}
		}

		// Token: 0x04000E90 RID: 3728
		private TextAlignment mAlignment_RTL = TextAlignment.Right;

		// Token: 0x04000E91 RID: 3729
		private TextAlignment mAlignment_LTR;

		// Token: 0x04000E92 RID: 3730
		private bool mAlignmentWasRTL;

		// Token: 0x04000E93 RID: 3731
		private bool mInitializeAlignment = true;
	}
}
