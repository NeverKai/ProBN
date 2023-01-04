using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000284 RID: 644
	[CallbackIdentity(1311)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageDeletePublishedFileResult_t
	{
		// Token: 0x0400065C RID: 1628
		public const int k_iCallback = 1311;

		// Token: 0x0400065D RID: 1629
		public EResult m_eResult;

		// Token: 0x0400065E RID: 1630
		public PublishedFileId_t m_nPublishedFileId;
	}
}
