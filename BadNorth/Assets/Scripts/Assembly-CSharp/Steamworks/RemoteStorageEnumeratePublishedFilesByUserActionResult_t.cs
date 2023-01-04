using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000295 RID: 661
	[CallbackIdentity(1328)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageEnumeratePublishedFilesByUserActionResult_t
	{
		// Token: 0x040006B9 RID: 1721
		public const int k_iCallback = 1328;

		// Token: 0x040006BA RID: 1722
		public EResult m_eResult;

		// Token: 0x040006BB RID: 1723
		public EWorkshopFileAction m_eAction;

		// Token: 0x040006BC RID: 1724
		public int m_nResultsReturned;

		// Token: 0x040006BD RID: 1725
		public int m_nTotalResultCount;

		// Token: 0x040006BE RID: 1726
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
		public PublishedFileId_t[] m_rgPublishedFileId;

		// Token: 0x040006BF RID: 1727
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
		public uint[] m_rgRTimeUpdated;
	}
}
