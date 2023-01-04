using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020002C3 RID: 707
	[CallbackIdentity(702)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LowBatteryPower_t
	{
		// Token: 0x04000757 RID: 1879
		public const int k_iCallback = 702;

		// Token: 0x04000758 RID: 1880
		public byte m_nMinutesBatteryLeft;
	}
}
