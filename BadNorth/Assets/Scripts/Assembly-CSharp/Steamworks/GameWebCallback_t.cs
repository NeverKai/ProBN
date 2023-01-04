using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020002B4 RID: 692
	[CallbackIdentity(164)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GameWebCallback_t
	{
		// Token: 0x04000724 RID: 1828
		public const int k_iCallback = 164;

		// Token: 0x04000725 RID: 1829
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string m_szURL;
	}
}
