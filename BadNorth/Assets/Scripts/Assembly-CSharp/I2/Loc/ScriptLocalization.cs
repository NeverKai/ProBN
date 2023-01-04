using System;
using UnityEngine;

namespace I2.Loc
{
	// Token: 0x0200041F RID: 1055
	public static class ScriptLocalization
	{
		// Token: 0x06001850 RID: 6224 RVA: 0x0003F339 File Offset: 0x0003D739
		public static string Get(string Term, bool FixForRTL = true, int maxLineLengthForRTL = 0, bool ignoreRTLnumbers = true, bool applyParameters = false, GameObject localParametersRoot = null, string overrideLanguage = null)
		{
			return LocalizationManager.GetTranslation(Term, FixForRTL, maxLineLengthForRTL, ignoreRTLnumbers, applyParameters, localParametersRoot, overrideLanguage);
		}

		// Token: 0x02000420 RID: 1056
		public static class LOAD_SCREEN
		{
			// Token: 0x17000152 RID: 338
			// (get) Token: 0x06001851 RID: 6225 RVA: 0x0003F34A File Offset: 0x0003D74A
			public static string TITLE
			{
				get
				{
					return ScriptLocalization.Get("LOAD_SCREEN/TITLE", true, 0, true, false, null, null);
				}
			}

			// Token: 0x17000153 RID: 339
			// (get) Token: 0x06001852 RID: 6226 RVA: 0x0003F35C File Offset: 0x0003D75C
			public static string TITLE_GENERATING
			{
				get
				{
					return ScriptLocalization.Get("LOAD_SCREEN/TITLE_GENERATING", true, 0, true, false, null, null);
				}
			}

			// Token: 0x17000154 RID: 340
			// (get) Token: 0x06001853 RID: 6227 RVA: 0x0003F36E File Offset: 0x0003D76E
			public static string INIT_GAMEPLAY
			{
				get
				{
					return ScriptLocalization.Get("LOAD_SCREEN/INIT_GAMEPLAY", true, 0, true, false, null, null);
				}
			}

			// Token: 0x17000155 RID: 341
			// (get) Token: 0x06001854 RID: 6228 RVA: 0x0003F380 File Offset: 0x0003D780
			public static string NEW_CAMPAIGN
			{
				get
				{
					return ScriptLocalization.Get("LOAD_SCREEN/NEW_CAMPAIGN", true, 0, true, false, null, null);
				}
			}

			// Token: 0x17000156 RID: 342
			// (get) Token: 0x06001855 RID: 6229 RVA: 0x0003F392 File Offset: 0x0003D792
			public static string GENERATING_ISLAND
			{
				get
				{
					return ScriptLocalization.Get("LOAD_SCREEN/GENERATING_ISLAND", true, 0, true, false, null, null);
				}
			}

			// Token: 0x17000157 RID: 343
			// (get) Token: 0x06001856 RID: 6230 RVA: 0x0003F3A4 File Offset: 0x0003D7A4
			public static string GENERATING_ISLANDS
			{
				get
				{
					return ScriptLocalization.Get("LOAD_SCREEN/GENERATING_ISLANDS", true, 0, true, false, null, null);
				}
			}

			// Token: 0x17000158 RID: 344
			// (get) Token: 0x06001857 RID: 6231 RVA: 0x0003F3B6 File Offset: 0x0003D7B6
			public static string PREP_ISLAND
			{
				get
				{
					return ScriptLocalization.Get("LOAD_SCREEN/PREP_ISLAND", true, 0, true, false, null, null);
				}
			}

			// Token: 0x17000159 RID: 345
			// (get) Token: 0x06001858 RID: 6232 RVA: 0x0003F3C8 File Offset: 0x0003D7C8
			public static string RESUME_CAMPAIGN
			{
				get
				{
					return ScriptLocalization.Get("LOAD_SCREEN/RESUME_CAMPAIGN", true, 0, true, false, null, null);
				}
			}
		}
	}
}
