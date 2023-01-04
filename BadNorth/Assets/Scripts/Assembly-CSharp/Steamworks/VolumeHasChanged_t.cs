using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200026C RID: 620
	[CallbackIdentity(4002)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct VolumeHasChanged_t
	{
		// Token: 0x04000623 RID: 1571
		public const int k_iCallback = 4002;

		// Token: 0x04000624 RID: 1572
		public float m_flNewVolume;
	}
}
