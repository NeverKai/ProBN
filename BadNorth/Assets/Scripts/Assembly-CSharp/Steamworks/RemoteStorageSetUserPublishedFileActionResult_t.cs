using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000294 RID: 660
	[CallbackIdentity(1327)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageSetUserPublishedFileActionResult_t
	{
		// Token: 0x040006B5 RID: 1717
		public const int k_iCallback = 1327;

		// Token: 0x040006B6 RID: 1718
		public EResult m_eResult;

		// Token: 0x040006B7 RID: 1719
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x040006B8 RID: 1720
		public EWorkshopFileAction m_eAction;
	}
}
