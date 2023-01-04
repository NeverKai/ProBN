using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020002CA RID: 714
	[CallbackIdentity(4611)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GetVideoURLResult_t
	{
		// Token: 0x04000766 RID: 1894
		public const int k_iCallback = 4611;

		// Token: 0x04000767 RID: 1895
		public EResult m_eResult;

		// Token: 0x04000768 RID: 1896
		public AppId_t m_unVideoAppID;

		// Token: 0x04000769 RID: 1897
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string m_rgchURL;
	}
}
