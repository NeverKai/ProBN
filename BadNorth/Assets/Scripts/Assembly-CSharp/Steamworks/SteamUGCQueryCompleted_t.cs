using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200029C RID: 668
	[CallbackIdentity(3401)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamUGCQueryCompleted_t
	{
		// Token: 0x040006D2 RID: 1746
		public const int k_iCallback = 3401;

		// Token: 0x040006D3 RID: 1747
		public UGCQueryHandle_t m_handle;

		// Token: 0x040006D4 RID: 1748
		public EResult m_eResult;

		// Token: 0x040006D5 RID: 1749
		public uint m_unNumResultsReturned;

		// Token: 0x040006D6 RID: 1750
		public uint m_unTotalMatchingResults;

		// Token: 0x040006D7 RID: 1751
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bCachedData;
	}
}
