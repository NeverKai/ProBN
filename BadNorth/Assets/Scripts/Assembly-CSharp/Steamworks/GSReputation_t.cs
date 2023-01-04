using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200023D RID: 573
	[CallbackIdentity(209)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GSReputation_t
	{
		// Token: 0x0400055D RID: 1373
		public const int k_iCallback = 209;

		// Token: 0x0400055E RID: 1374
		public EResult m_eResult;

		// Token: 0x0400055F RID: 1375
		public uint m_unReputationScore;

		// Token: 0x04000560 RID: 1376
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bBanned;

		// Token: 0x04000561 RID: 1377
		public uint m_unBannedIP;

		// Token: 0x04000562 RID: 1378
		public ushort m_usBannedPort;

		// Token: 0x04000563 RID: 1379
		public ulong m_ulBannedGameID;

		// Token: 0x04000564 RID: 1380
		public uint m_unBanExpires;
	}
}
