using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000276 RID: 630
	[CallbackIdentity(4110)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct MusicPlayerWantsLooped_t
	{
		// Token: 0x0400062F RID: 1583
		public const int k_iCallback = 4110;

		// Token: 0x04000630 RID: 1584
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bLooped;
	}
}
