using System;

namespace I2.Loc
{
	// Token: 0x02000411 RID: 1041
	[Serializable]
	public struct LocalizedString
	{
		// Token: 0x0600181F RID: 6175 RVA: 0x0003D410 File Offset: 0x0003B810
		public LocalizedString(LocalizedString str)
		{
			this.mTerm = str.mTerm;
			this.mRTL_IgnoreArabicFix = str.mRTL_IgnoreArabicFix;
			this.mRTL_MaxLineLength = str.mRTL_MaxLineLength;
			this.mRTL_ConvertNumbers = str.mRTL_ConvertNumbers;
			this.m_DontLocalizeParameters = str.m_DontLocalizeParameters;
		}

		// Token: 0x06001820 RID: 6176 RVA: 0x0003D45E File Offset: 0x0003B85E
		public static implicit operator string(LocalizedString s)
		{
			return s.ToString();
		}

		// Token: 0x06001821 RID: 6177 RVA: 0x0003D470 File Offset: 0x0003B870
		public static implicit operator LocalizedString(string term)
		{
			return new LocalizedString
			{
				mTerm = term
			};
		}

		// Token: 0x06001822 RID: 6178 RVA: 0x0003D490 File Offset: 0x0003B890
		public override string ToString()
		{
			string translation = LocalizationManager.GetTranslation(this.mTerm, !this.mRTL_IgnoreArabicFix, this.mRTL_MaxLineLength, !this.mRTL_ConvertNumbers, true, null, null);
			LocalizationManager.ApplyLocalizationParams(ref translation, !this.m_DontLocalizeParameters);
			return translation;
		}

		// Token: 0x04000EB8 RID: 3768
		public string mTerm;

		// Token: 0x04000EB9 RID: 3769
		public bool mRTL_IgnoreArabicFix;

		// Token: 0x04000EBA RID: 3770
		public int mRTL_MaxLineLength;

		// Token: 0x04000EBB RID: 3771
		public bool mRTL_ConvertNumbers;

		// Token: 0x04000EBC RID: 3772
		public bool m_DontLocalizeParameters;
	}
}
