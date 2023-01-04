using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200028F RID: 655
	[CallbackIdentity(1322)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStoragePublishedFileUnsubscribed_t
	{
		// Token: 0x040006A3 RID: 1699
		public const int k_iCallback = 1322;

		// Token: 0x040006A4 RID: 1700
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x040006A5 RID: 1701
		public AppId_t m_nAppID;
	}
}
