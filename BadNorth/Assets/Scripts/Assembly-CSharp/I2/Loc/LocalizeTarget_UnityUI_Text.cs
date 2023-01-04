using System;
using UnityEngine;
using UnityEngine.UI;

namespace I2.Loc
{
	// Token: 0x02000404 RID: 1028
	public class LocalizeTarget_UnityUI_Text : LocalizeTarget<Text>
	{
		// Token: 0x060017E2 RID: 6114 RVA: 0x0003C3A0 File Offset: 0x0003A7A0
		static LocalizeTarget_UnityUI_Text()
		{
			LocalizeTarget_UnityUI_Text.AutoRegister();
		}

		// Token: 0x060017E4 RID: 6116 RVA: 0x0003C3C0 File Offset: 0x0003A7C0
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void AutoRegister()
		{
			LocalizationManager.RegisterTarget(new LocalizeTargetDesc_Type<Text, LocalizeTarget_UnityUI_Text>
			{
				Name = "Text",
				Priority = 100
			});
		}

		// Token: 0x060017E5 RID: 6117 RVA: 0x0003C3EC File Offset: 0x0003A7EC
		public override eTermType GetPrimaryTermType(Localize cmp)
		{
			return eTermType.Text;
		}

		// Token: 0x060017E6 RID: 6118 RVA: 0x0003C3EF File Offset: 0x0003A7EF
		public override eTermType GetSecondaryTermType(Localize cmp)
		{
			return eTermType.Font;
		}

		// Token: 0x060017E7 RID: 6119 RVA: 0x0003C3F2 File Offset: 0x0003A7F2
		public override bool CanUseSecondaryTerm()
		{
			return true;
		}

		// Token: 0x060017E8 RID: 6120 RVA: 0x0003C3F5 File Offset: 0x0003A7F5
		public override bool AllowMainTermToBeRTL()
		{
			return true;
		}

		// Token: 0x060017E9 RID: 6121 RVA: 0x0003C3F8 File Offset: 0x0003A7F8
		public override bool AllowSecondTermToBeRTL()
		{
			return false;
		}

		// Token: 0x060017EA RID: 6122 RVA: 0x0003C3FC File Offset: 0x0003A7FC
		public override void GetFinalTerms(Localize cmp, string Main, string Secondary, out string primaryTerm, out string secondaryTerm)
		{
			primaryTerm = ((!this.mTarget) ? null : this.mTarget.text);
			secondaryTerm = ((!(this.mTarget.font != null)) ? string.Empty : this.mTarget.font.name);
		}

		// Token: 0x060017EB RID: 6123 RVA: 0x0003C460 File Offset: 0x0003A860
		public override void DoLocalize(Localize cmp, string mainTranslation, string secondaryTranslation)
		{
			Font secondaryTranslatedObj = cmp.GetSecondaryTranslatedObj<Font>(ref mainTranslation, ref secondaryTranslation);
			if (secondaryTranslatedObj != null && secondaryTranslatedObj != this.mTarget.font)
			{
				this.mTarget.font = secondaryTranslatedObj;
			}
			if (this.mInitializeAlignment)
			{
				this.mInitializeAlignment = false;
				this.mAlignmentWasRTL = LocalizationManager.IsRight2Left;
				this.InitAlignment(this.mAlignmentWasRTL, this.mTarget.alignment, out this.mAlignment_LTR, out this.mAlignment_RTL);
			}
			else
			{
				TextAnchor textAnchor;
				TextAnchor textAnchor2;
				this.InitAlignment(this.mAlignmentWasRTL, this.mTarget.alignment, out textAnchor, out textAnchor2);
				if ((this.mAlignmentWasRTL && this.mAlignment_RTL != textAnchor2) || (!this.mAlignmentWasRTL && this.mAlignment_LTR != textAnchor))
				{
					this.mAlignment_LTR = textAnchor;
					this.mAlignment_RTL = textAnchor2;
				}
				this.mAlignmentWasRTL = LocalizationManager.IsRight2Left;
			}
			if (mainTranslation != null && this.mTarget.text != mainTranslation)
			{
				if (cmp.CorrectAlignmentForRTL)
				{
					this.mTarget.alignment = ((!LocalizationManager.IsRight2Left) ? this.mAlignment_LTR : this.mAlignment_RTL);
				}
				this.mTarget.text = mainTranslation;
				this.mTarget.SetVerticesDirty();
			}
		}

		// Token: 0x060017EC RID: 6124 RVA: 0x0003C5B4 File Offset: 0x0003A9B4
		private void InitAlignment(bool isRTL, TextAnchor alignment, out TextAnchor alignLTR, out TextAnchor alignRTL)
		{
			alignRTL = alignment;
			alignLTR = alignment;
			if (isRTL)
			{
				switch (alignment)
				{
				case TextAnchor.UpperLeft:
					alignLTR = TextAnchor.UpperRight;
					break;
				case TextAnchor.UpperRight:
					alignLTR = TextAnchor.UpperLeft;
					break;
				case TextAnchor.MiddleLeft:
					alignLTR = TextAnchor.MiddleRight;
					break;
				case TextAnchor.MiddleRight:
					alignLTR = TextAnchor.MiddleLeft;
					break;
				case TextAnchor.LowerLeft:
					alignLTR = TextAnchor.LowerRight;
					break;
				case TextAnchor.LowerRight:
					alignLTR = TextAnchor.LowerLeft;
					break;
				}
			}
			else
			{
				switch (alignment)
				{
				case TextAnchor.UpperLeft:
					alignRTL = TextAnchor.UpperRight;
					break;
				case TextAnchor.UpperRight:
					alignRTL = TextAnchor.UpperLeft;
					break;
				case TextAnchor.MiddleLeft:
					alignRTL = TextAnchor.MiddleRight;
					break;
				case TextAnchor.MiddleRight:
					alignRTL = TextAnchor.MiddleLeft;
					break;
				case TextAnchor.LowerLeft:
					alignRTL = TextAnchor.LowerRight;
					break;
				case TextAnchor.LowerRight:
					alignRTL = TextAnchor.LowerLeft;
					break;
				}
			}
		}

		// Token: 0x04000E94 RID: 3732
		private TextAnchor mAlignment_RTL = TextAnchor.UpperRight;

		// Token: 0x04000E95 RID: 3733
		private TextAnchor mAlignment_LTR;

		// Token: 0x04000E96 RID: 3734
		private bool mAlignmentWasRTL;

		// Token: 0x04000E97 RID: 3735
		private bool mInitializeAlignment = true;
	}
}
