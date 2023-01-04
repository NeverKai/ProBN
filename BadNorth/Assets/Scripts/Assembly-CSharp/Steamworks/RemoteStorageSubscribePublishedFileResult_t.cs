using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000286 RID: 646
	[CallbackIdentity(1313)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageSubscribePublishedFileResult_t
	{
		// Token: 0x04000664 RID: 1636
		public const int k_iCallback = 1313;

		// Token: 0x04000665 RID: 1637
		public EResult m_eResult;

		// Token: 0x04000666 RID: 1638
		public PublishedFileId_t m_nPublishedFileId;
	}
}
