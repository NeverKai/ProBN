using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020002A7 RID: 679
	[CallbackIdentity(3412)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct AddUGCDependencyResult_t
	{
		// Token: 0x040006FB RID: 1787
		public const int k_iCallback = 3412;

		// Token: 0x040006FC RID: 1788
		public EResult m_eResult;

		// Token: 0x040006FD RID: 1789
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x040006FE RID: 1790
		public PublishedFileId_t m_nChildPublishedFileId;
	}
}
