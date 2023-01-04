using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000277 RID: 631
	[CallbackIdentity(4011)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct MusicPlayerWantsVolume_t
	{
		// Token: 0x04000631 RID: 1585
		public const int k_iCallback = 4011;

		// Token: 0x04000632 RID: 1586
		public float m_flNewVolume;
	}
}
