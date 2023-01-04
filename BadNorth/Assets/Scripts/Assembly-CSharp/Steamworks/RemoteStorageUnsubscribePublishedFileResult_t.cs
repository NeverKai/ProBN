using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000288 RID: 648
	[CallbackIdentity(1315)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageUnsubscribePublishedFileResult_t
	{
		// Token: 0x0400066D RID: 1645
		public const int k_iCallback = 1315;

		// Token: 0x0400066E RID: 1646
		public EResult m_eResult;

		// Token: 0x0400066F RID: 1647
		public PublishedFileId_t m_nPublishedFileId;
	}
}
