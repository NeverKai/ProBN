using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000283 RID: 643
	[CallbackIdentity(1309)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStoragePublishFileResult_t
	{
		// Token: 0x04000658 RID: 1624
		public const int k_iCallback = 1309;

		// Token: 0x04000659 RID: 1625
		public EResult m_eResult;

		// Token: 0x0400065A RID: 1626
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x0400065B RID: 1627
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bUserNeedsToAcceptWorkshopLegalAgreement;
	}
}
