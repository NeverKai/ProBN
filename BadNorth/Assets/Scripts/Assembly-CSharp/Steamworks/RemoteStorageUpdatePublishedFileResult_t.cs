using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000289 RID: 649
	[CallbackIdentity(1316)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageUpdatePublishedFileResult_t
	{
		// Token: 0x04000670 RID: 1648
		public const int k_iCallback = 1316;

		// Token: 0x04000671 RID: 1649
		public EResult m_eResult;

		// Token: 0x04000672 RID: 1650
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x04000673 RID: 1651
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bUserNeedsToAcceptWorkshopLegalAgreement;
	}
}
