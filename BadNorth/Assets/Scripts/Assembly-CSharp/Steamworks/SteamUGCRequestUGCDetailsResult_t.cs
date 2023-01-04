using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200029D RID: 669
	[CallbackIdentity(3402)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamUGCRequestUGCDetailsResult_t
	{
		// Token: 0x040006D8 RID: 1752
		public const int k_iCallback = 3402;

		// Token: 0x040006D9 RID: 1753
		public SteamUGCDetails_t m_details;

		// Token: 0x040006DA RID: 1754
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bCachedData;
	}
}
