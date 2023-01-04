using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200031A RID: 794
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct ControllerDigitalActionData_t
	{
		// Token: 0x04000BE3 RID: 3043
		public byte bState;

		// Token: 0x04000BE4 RID: 3044
		public byte bActive;
	}
}
