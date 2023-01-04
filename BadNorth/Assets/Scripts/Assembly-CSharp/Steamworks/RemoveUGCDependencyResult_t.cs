using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020002A8 RID: 680
	[CallbackIdentity(3413)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoveUGCDependencyResult_t
	{
		// Token: 0x040006FF RID: 1791
		public const int k_iCallback = 3413;

		// Token: 0x04000700 RID: 1792
		public EResult m_eResult;

		// Token: 0x04000701 RID: 1793
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x04000702 RID: 1794
		public PublishedFileId_t m_nChildPublishedFileId;
	}
}
