using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020002CB RID: 715
	[CallbackIdentity(4624)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GetOPFSettingsResult_t
	{
		// Token: 0x0400076A RID: 1898
		public const int k_iCallback = 4624;

		// Token: 0x0400076B RID: 1899
		public EResult m_eResult;

		// Token: 0x0400076C RID: 1900
		public AppId_t m_unVideoAppID;
	}
}
