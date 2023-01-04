using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020002A1 RID: 673
	[CallbackIdentity(3406)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct DownloadItemResult_t
	{
		// Token: 0x040006E5 RID: 1765
		public const int k_iCallback = 3406;

		// Token: 0x040006E6 RID: 1766
		public AppId_t m_unAppID;

		// Token: 0x040006E7 RID: 1767
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x040006E8 RID: 1768
		public EResult m_eResult;
	}
}
