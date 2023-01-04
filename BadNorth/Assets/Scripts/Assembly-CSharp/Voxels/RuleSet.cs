using System;
using System.Collections.Generic;

namespace Voxels
{
	// Token: 0x0200066D RID: 1645
	[Serializable]
	public class RuleSet
	{
		// Token: 0x04001B61 RID: 7009
		public List<RuleSet.RuleSettings> ruleSettings;

		// Token: 0x0200066E RID: 1646
		[Serializable]
		public class RuleSettings
		{
			// Token: 0x04001B62 RID: 7010
			public ModuleSet moduleSet;

			// Token: 0x04001B63 RID: 7011
			public ModuleSet.Mode overrideMode;
		}
	}
}
