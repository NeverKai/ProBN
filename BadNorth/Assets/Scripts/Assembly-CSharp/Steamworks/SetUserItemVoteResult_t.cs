using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020002A3 RID: 675
	[CallbackIdentity(3408)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SetUserItemVoteResult_t
	{
		// Token: 0x040006ED RID: 1773
		public const int k_iCallback = 3408;

		// Token: 0x040006EE RID: 1774
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x040006EF RID: 1775
		public EResult m_eResult;

		// Token: 0x040006F0 RID: 1776
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bVoteUp;
	}
}
