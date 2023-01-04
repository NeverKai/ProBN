using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200027A RID: 634
	[CallbackIdentity(4114)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct MusicPlayerWantsPlayingRepeatStatus_t
	{
		// Token: 0x04000637 RID: 1591
		public const int k_iCallback = 4114;

		// Token: 0x04000638 RID: 1592
		public int m_nPlayingRepeatStatus;
	}
}
