using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200028D RID: 653
	[CallbackIdentity(1320)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageGetPublishedItemVoteDetailsResult_t
	{
		// Token: 0x04000699 RID: 1689
		public const int k_iCallback = 1320;

		// Token: 0x0400069A RID: 1690
		public EResult m_eResult;

		// Token: 0x0400069B RID: 1691
		public PublishedFileId_t m_unPublishedFileId;

		// Token: 0x0400069C RID: 1692
		public int m_nVotesFor;

		// Token: 0x0400069D RID: 1693
		public int m_nVotesAgainst;

		// Token: 0x0400069E RID: 1694
		public int m_nReports;

		// Token: 0x0400069F RID: 1695
		public float m_fScore;
	}
}
