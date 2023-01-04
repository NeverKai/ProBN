using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000226 RID: 550
	[CallbackIdentity(334)]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct AvatarImageLoaded_t
	{
		// Token: 0x0400050C RID: 1292
		public const int k_iCallback = 334;

		// Token: 0x0400050D RID: 1293
		public CSteamID m_steamID;

		// Token: 0x0400050E RID: 1294
		public int m_iImage;

		// Token: 0x0400050F RID: 1295
		public int m_iWide;

		// Token: 0x04000510 RID: 1296
		public int m_iTall;
	}
}
