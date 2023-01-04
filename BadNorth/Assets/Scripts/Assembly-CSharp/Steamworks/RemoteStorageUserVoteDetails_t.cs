using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000292 RID: 658
	[CallbackIdentity(1325)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageUserVoteDetails_t
	{
		// Token: 0x040006AC RID: 1708
		public const int k_iCallback = 1325;

		// Token: 0x040006AD RID: 1709
		public EResult m_eResult;

		// Token: 0x040006AE RID: 1710
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x040006AF RID: 1711
		public EWorkshopVote m_eVote;
	}
}
