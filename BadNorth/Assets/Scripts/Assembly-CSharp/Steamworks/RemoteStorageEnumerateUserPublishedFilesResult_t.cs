using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000285 RID: 645
	[CallbackIdentity(1312)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageEnumerateUserPublishedFilesResult_t
	{
		// Token: 0x0400065F RID: 1631
		public const int k_iCallback = 1312;

		// Token: 0x04000660 RID: 1632
		public EResult m_eResult;

		// Token: 0x04000661 RID: 1633
		public int m_nResultsReturned;

		// Token: 0x04000662 RID: 1634
		public int m_nTotalResultCount;

		// Token: 0x04000663 RID: 1635
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
		public PublishedFileId_t[] m_rgPublishedFileId;
	}
}
