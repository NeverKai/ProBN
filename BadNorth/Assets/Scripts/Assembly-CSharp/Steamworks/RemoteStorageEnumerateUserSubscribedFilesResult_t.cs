using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000287 RID: 647
	[CallbackIdentity(1314)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageEnumerateUserSubscribedFilesResult_t
	{
		// Token: 0x04000667 RID: 1639
		public const int k_iCallback = 1314;

		// Token: 0x04000668 RID: 1640
		public EResult m_eResult;

		// Token: 0x04000669 RID: 1641
		public int m_nResultsReturned;

		// Token: 0x0400066A RID: 1642
		public int m_nTotalResultCount;

		// Token: 0x0400066B RID: 1643
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
		public PublishedFileId_t[] m_rgPublishedFileId;

		// Token: 0x0400066C RID: 1644
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
		public uint[] m_rgRTimeSubscribed;
	}
}
