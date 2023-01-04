using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200021C RID: 540
	[CallbackIdentity(3902)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamAppUninstalled_t
	{
		// Token: 0x040004EF RID: 1263
		public const int k_iCallback = 3902;

		// Token: 0x040004F0 RID: 1264
		public AppId_t m_nAppID;
	}
}
