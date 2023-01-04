using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000224 RID: 548
	[CallbackIdentity(332)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GameServerChangeRequested_t
	{
		// Token: 0x04000506 RID: 1286
		public const int k_iCallback = 332;

		// Token: 0x04000507 RID: 1287
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
		public string m_rgchServer;

		// Token: 0x04000508 RID: 1288
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
		public string m_rgchPassword;
	}
}
