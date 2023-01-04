using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020002AB RID: 683
	[CallbackIdentity(102)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamServerConnectFailure_t
	{
		// Token: 0x04000709 RID: 1801
		public const int k_iCallback = 102;

		// Token: 0x0400070A RID: 1802
		public EResult m_eResult;

		// Token: 0x0400070B RID: 1803
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bStillRetrying;
	}
}
