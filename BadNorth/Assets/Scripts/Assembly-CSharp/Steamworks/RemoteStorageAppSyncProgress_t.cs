using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000280 RID: 640
	[CallbackIdentity(1303)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageAppSyncProgress_t
	{
		// Token: 0x0400064B RID: 1611
		public const int k_iCallback = 1303;

		// Token: 0x0400064C RID: 1612
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		public string m_rgchCurrentFile;

		// Token: 0x0400064D RID: 1613
		public AppId_t m_nAppID;

		// Token: 0x0400064E RID: 1614
		public uint m_uBytesTransferredThisChunk;

		// Token: 0x0400064F RID: 1615
		public double m_dAppPercentComplete;

		// Token: 0x04000650 RID: 1616
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bUploading;
	}
}
