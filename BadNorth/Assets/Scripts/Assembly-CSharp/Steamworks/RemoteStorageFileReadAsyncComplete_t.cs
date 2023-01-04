using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000299 RID: 665
	[CallbackIdentity(1332)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageFileReadAsyncComplete_t
	{
		// Token: 0x040006C9 RID: 1737
		public const int k_iCallback = 1332;

		// Token: 0x040006CA RID: 1738
		public SteamAPICall_t m_hFileReadAsync;

		// Token: 0x040006CB RID: 1739
		public EResult m_eResult;

		// Token: 0x040006CC RID: 1740
		public uint m_nOffset;

		// Token: 0x040006CD RID: 1741
		public uint m_cubRead;
	}
}
