using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200029E RID: 670
	[CallbackIdentity(3403)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct CreateItemResult_t
	{
		// Token: 0x040006DB RID: 1755
		public const int k_iCallback = 3403;

		// Token: 0x040006DC RID: 1756
		public EResult m_eResult;

		// Token: 0x040006DD RID: 1757
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x040006DE RID: 1758
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bUserNeedsToAcceptWorkshopLegalAgreement;
	}
}
