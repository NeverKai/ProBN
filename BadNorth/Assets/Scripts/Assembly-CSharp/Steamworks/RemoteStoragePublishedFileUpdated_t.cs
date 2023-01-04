using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000297 RID: 663
	[CallbackIdentity(1330)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStoragePublishedFileUpdated_t
	{
		// Token: 0x040006C3 RID: 1731
		public const int k_iCallback = 1330;

		// Token: 0x040006C4 RID: 1732
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x040006C5 RID: 1733
		public AppId_t m_nAppID;

		// Token: 0x040006C6 RID: 1734
		public ulong m_ulUnused;
	}
}
