using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000222 RID: 546
	[CallbackIdentity(304)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct PersonaStateChange_t
	{
		// Token: 0x04000501 RID: 1281
		public const int k_iCallback = 304;

		// Token: 0x04000502 RID: 1282
		public ulong m_ulSteamID;

		// Token: 0x04000503 RID: 1283
		public EPersonaChange m_nChangeFlags;
	}
}
