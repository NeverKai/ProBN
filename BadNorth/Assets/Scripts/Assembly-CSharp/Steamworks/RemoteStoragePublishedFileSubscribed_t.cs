using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200028E RID: 654
	[CallbackIdentity(1321)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStoragePublishedFileSubscribed_t
	{
		// Token: 0x040006A0 RID: 1696
		public const int k_iCallback = 1321;

		// Token: 0x040006A1 RID: 1697
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x040006A2 RID: 1698
		public AppId_t m_nAppID;
	}
}
