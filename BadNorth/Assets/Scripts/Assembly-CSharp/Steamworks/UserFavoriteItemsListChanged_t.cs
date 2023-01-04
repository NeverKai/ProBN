using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020002A2 RID: 674
	[CallbackIdentity(3407)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct UserFavoriteItemsListChanged_t
	{
		// Token: 0x040006E9 RID: 1769
		public const int k_iCallback = 3407;

		// Token: 0x040006EA RID: 1770
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x040006EB RID: 1771
		public EResult m_eResult;

		// Token: 0x040006EC RID: 1772
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bWasAddRequest;
	}
}
