using System;
using I2.Loc;
using Rewired;

namespace RTM.Input
{
	// Token: 0x020004B9 RID: 1209
	[Serializable]
	public struct RewiredActionUIReference
	{
		// Token: 0x04001305 RID: 4869
		public int actionId;

		// Token: 0x04001306 RID: 4870
		public Pole pole;

		// Token: 0x04001307 RID: 4871
		[TermsPopup("")]
		public string locId;

		// Token: 0x04001308 RID: 4872
		public int actionNameIdx;
	}
}
