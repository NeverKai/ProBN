using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200028A RID: 650
	[CallbackIdentity(1317)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageDownloadUGCResult_t
	{
		// Token: 0x04000674 RID: 1652
		public const int k_iCallback = 1317;

		// Token: 0x04000675 RID: 1653
		public EResult m_eResult;

		// Token: 0x04000676 RID: 1654
		public UGCHandle_t m_hFile;

		// Token: 0x04000677 RID: 1655
		public AppId_t m_nAppID;

		// Token: 0x04000678 RID: 1656
		public int m_nSizeInBytes;

		// Token: 0x04000679 RID: 1657
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		public string m_pchFileName;

		// Token: 0x0400067A RID: 1658
		public ulong m_ulSteamIDOwner;
	}
}
