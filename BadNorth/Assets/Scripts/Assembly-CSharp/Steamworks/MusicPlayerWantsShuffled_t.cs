using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000275 RID: 629
	[CallbackIdentity(4109)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct MusicPlayerWantsShuffled_t
	{
		// Token: 0x0400062D RID: 1581
		public const int k_iCallback = 4109;

		// Token: 0x0400062E RID: 1582
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bShuffled;
	}
}
