using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020002BF RID: 703
	[CallbackIdentity(1110)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GlobalAchievementPercentagesReady_t
	{
		// Token: 0x0400074D RID: 1869
		public const int k_iCallback = 1110;

		// Token: 0x0400074E RID: 1870
		public ulong m_nGameID;

		// Token: 0x0400074F RID: 1871
		public EResult m_eResult;
	}
}
