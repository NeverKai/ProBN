using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000320 RID: 800
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamParamStringArray_t
	{
		// Token: 0x04000C02 RID: 3074
		public IntPtr m_ppStrings;

		// Token: 0x04000C03 RID: 3075
		public int m_nNumStrings;
	}
}
