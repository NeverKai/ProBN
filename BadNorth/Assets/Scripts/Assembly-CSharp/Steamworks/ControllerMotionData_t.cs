using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200031B RID: 795
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct ControllerMotionData_t
	{
		// Token: 0x04000BE5 RID: 3045
		public float rotQuatX;

		// Token: 0x04000BE6 RID: 3046
		public float rotQuatY;

		// Token: 0x04000BE7 RID: 3047
		public float rotQuatZ;

		// Token: 0x04000BE8 RID: 3048
		public float rotQuatW;

		// Token: 0x04000BE9 RID: 3049
		public float posAccelX;

		// Token: 0x04000BEA RID: 3050
		public float posAccelY;

		// Token: 0x04000BEB RID: 3051
		public float posAccelZ;

		// Token: 0x04000BEC RID: 3052
		public float rotVelX;

		// Token: 0x04000BED RID: 3053
		public float rotVelY;

		// Token: 0x04000BEE RID: 3054
		public float rotVelZ;
	}
}
