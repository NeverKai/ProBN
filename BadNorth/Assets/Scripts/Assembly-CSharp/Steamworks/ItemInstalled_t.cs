using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020002A0 RID: 672
	[CallbackIdentity(3405)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct ItemInstalled_t
	{
		// Token: 0x040006E2 RID: 1762
		public const int k_iCallback = 3405;

		// Token: 0x040006E3 RID: 1763
		public AppId_t m_unAppID;

		// Token: 0x040006E4 RID: 1764
		public PublishedFileId_t m_nPublishedFileId;
	}
}
