using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000293 RID: 659
	[CallbackIdentity(1326)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageEnumerateUserSharedWorkshopFilesResult_t
	{
		// Token: 0x040006B0 RID: 1712
		public const int k_iCallback = 1326;

		// Token: 0x040006B1 RID: 1713
		public EResult m_eResult;

		// Token: 0x040006B2 RID: 1714
		public int m_nResultsReturned;

		// Token: 0x040006B3 RID: 1715
		public int m_nTotalResultCount;

		// Token: 0x040006B4 RID: 1716
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
		public PublishedFileId_t[] m_rgPublishedFileId;
	}
}
