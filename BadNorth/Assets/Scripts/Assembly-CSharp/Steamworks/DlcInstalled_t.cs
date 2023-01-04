using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200021D RID: 541
	[CallbackIdentity(1005)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct DlcInstalled_t
	{
		// Token: 0x040004F1 RID: 1265
		public const int k_iCallback = 1005;

		// Token: 0x040004F2 RID: 1266
		public AppId_t m_nAppID;
	}
}
