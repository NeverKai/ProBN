using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200023B RID: 571
	[CallbackIdentity(207)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GSGameplayStats_t
	{
		// Token: 0x04000553 RID: 1363
		public const int k_iCallback = 207;

		// Token: 0x04000554 RID: 1364
		public EResult m_eResult;

		// Token: 0x04000555 RID: 1365
		public int m_nRank;

		// Token: 0x04000556 RID: 1366
		public uint m_unTotalConnects;

		// Token: 0x04000557 RID: 1367
		public uint m_unTotalMinutesPlayed;
	}
}
