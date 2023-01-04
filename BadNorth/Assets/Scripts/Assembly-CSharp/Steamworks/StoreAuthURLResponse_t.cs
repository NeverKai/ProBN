using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020002B5 RID: 693
	[CallbackIdentity(165)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct StoreAuthURLResponse_t
	{
		// Token: 0x04000726 RID: 1830
		public const int k_iCallback = 165;

		// Token: 0x04000727 RID: 1831
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
		public string m_szURL;
	}
}
