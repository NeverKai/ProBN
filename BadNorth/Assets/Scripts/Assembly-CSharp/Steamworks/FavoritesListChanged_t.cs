using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000260 RID: 608
	[CallbackIdentity(502)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct FavoritesListChanged_t
	{
		// Token: 0x040005F3 RID: 1523
		public const int k_iCallback = 502;

		// Token: 0x040005F4 RID: 1524
		public uint m_nIP;

		// Token: 0x040005F5 RID: 1525
		public uint m_nQueryPort;

		// Token: 0x040005F6 RID: 1526
		public uint m_nConnPort;

		// Token: 0x040005F7 RID: 1527
		public uint m_nAppID;

		// Token: 0x040005F8 RID: 1528
		public uint m_nFlags;

		// Token: 0x040005F9 RID: 1529
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bAdd;

		// Token: 0x040005FA RID: 1530
		public AccountID_t m_unAccountId;
	}
}
