using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000282 RID: 642
	[CallbackIdentity(1307)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RemoteStorageFileShareResult_t
	{
		// Token: 0x04000654 RID: 1620
		public const int k_iCallback = 1307;

		// Token: 0x04000655 RID: 1621
		public EResult m_eResult;

		// Token: 0x04000656 RID: 1622
		public UGCHandle_t m_hFile;

		// Token: 0x04000657 RID: 1623
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		public string m_rgchFilename;
	}
}
