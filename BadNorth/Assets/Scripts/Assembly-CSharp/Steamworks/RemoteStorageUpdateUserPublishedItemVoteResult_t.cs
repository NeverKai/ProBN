using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000291 RID: 657
	[CallbackIdentity(1324)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageUpdateUserPublishedItemVoteResult_t
	{
		// Token: 0x040006A9 RID: 1705
		public const int k_iCallback = 1324;

		// Token: 0x040006AA RID: 1706
		public EResult m_eResult;

		// Token: 0x040006AB RID: 1707
		public PublishedFileId_t m_nPublishedFileId;
	}
}
