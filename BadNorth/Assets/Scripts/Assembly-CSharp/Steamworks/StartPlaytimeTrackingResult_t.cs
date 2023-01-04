using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020002A5 RID: 677
	[CallbackIdentity(3410)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct StartPlaytimeTrackingResult_t
	{
		// Token: 0x040006F7 RID: 1783
		public const int k_iCallback = 3410;

		// Token: 0x040006F8 RID: 1784
		public EResult m_eResult;
	}
}
