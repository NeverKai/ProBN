using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020002A6 RID: 678
	[CallbackIdentity(3411)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct StopPlaytimeTrackingResult_t
	{
		// Token: 0x040006F9 RID: 1785
		public const int k_iCallback = 3411;

		// Token: 0x040006FA RID: 1786
		public EResult m_eResult;
	}
}
