using System;
using UnityEngine;

namespace I2.Loc
{
	// Token: 0x020003E6 RID: 998
	[CreateAssetMenu(fileName = "I2Languages", menuName = "I2 Localization/LanguageSource", order = 1)]
	public class LanguageSourceAsset : ScriptableObject, ILanguageSource
	{
		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060016E2 RID: 5858 RVA: 0x000385B9 File Offset: 0x000369B9
		// (set) Token: 0x060016E3 RID: 5859 RVA: 0x000385C1 File Offset: 0x000369C1
		public LanguageSourceData SourceData
		{
			get
			{
				return this.mSource;
			}
			set
			{
				this.mSource = value;
			}
		}

		// Token: 0x04000E37 RID: 3639
		public LanguageSourceData mSource = new LanguageSourceData();
	}
}
