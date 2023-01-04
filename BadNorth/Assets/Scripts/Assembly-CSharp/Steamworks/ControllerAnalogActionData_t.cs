using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000319 RID: 793
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct ControllerAnalogActionData_t
	{
		// Token: 0x04000BDF RID: 3039
		public EControllerSourceMode eMode;

		// Token: 0x04000BE0 RID: 3040
		public float x;

		// Token: 0x04000BE1 RID: 3041
		public float y;

		// Token: 0x04000BE2 RID: 3042
		public byte bActive;
	}
}
