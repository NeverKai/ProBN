using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200021B RID: 539
	[CallbackIdentity(3901)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamAppInstalled_t
	{
		// Token: 0x040004ED RID: 1261
		public const int k_iCallback = 3901;

		// Token: 0x040004EE RID: 1262
		public AppId_t m_nAppID;
	}
}
