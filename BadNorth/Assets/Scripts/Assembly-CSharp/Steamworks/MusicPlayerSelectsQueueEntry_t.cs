using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000278 RID: 632
	[CallbackIdentity(4012)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct MusicPlayerSelectsQueueEntry_t
	{
		// Token: 0x04000633 RID: 1587
		public const int k_iCallback = 4012;

		// Token: 0x04000634 RID: 1588
		public int nID;
	}
}
