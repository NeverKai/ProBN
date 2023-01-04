using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200028C RID: 652
	[CallbackIdentity(1319)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageEnumerateWorkshopFilesResult_t
	{
		// Token: 0x04000691 RID: 1681
		public const int k_iCallback = 1319;

		// Token: 0x04000692 RID: 1682
		public EResult m_eResult;

		// Token: 0x04000693 RID: 1683
		public int m_nResultsReturned;

		// Token: 0x04000694 RID: 1684
		public int m_nTotalResultCount;

		// Token: 0x04000695 RID: 1685
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
		public PublishedFileId_t[] m_rgPublishedFileId;

		// Token: 0x04000696 RID: 1686
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
		public float[] m_rgScore;

		// Token: 0x04000697 RID: 1687
		public AppId_t m_nAppId;

		// Token: 0x04000698 RID: 1688
		public uint m_unStartIndex;
	}
}
