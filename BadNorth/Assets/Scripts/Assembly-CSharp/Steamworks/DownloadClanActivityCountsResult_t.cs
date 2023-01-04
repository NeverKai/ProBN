using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200022D RID: 557
	[CallbackIdentity(341)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct DownloadClanActivityCountsResult_t
	{
		// Token: 0x04000527 RID: 1319
		public const int k_iCallback = 341;

		// Token: 0x04000528 RID: 1320
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bSuccess;
	}
}
