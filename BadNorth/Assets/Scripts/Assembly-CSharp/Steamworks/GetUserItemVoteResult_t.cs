using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020002A4 RID: 676
	[CallbackIdentity(3409)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GetUserItemVoteResult_t
	{
		// Token: 0x040006F1 RID: 1777
		public const int k_iCallback = 3409;

		// Token: 0x040006F2 RID: 1778
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x040006F3 RID: 1779
		public EResult m_eResult;

		// Token: 0x040006F4 RID: 1780
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bVotedUp;

		// Token: 0x040006F5 RID: 1781
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bVotedDown;

		// Token: 0x040006F6 RID: 1782
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bVoteSkipped;
	}
}
