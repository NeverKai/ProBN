using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200032A RID: 810
	[StructLayout(LayoutKind.Sequential)]
	internal class CCallbackBase
	{
		// Token: 0x04000C39 RID: 3129
		public const byte k_ECallbackFlagsRegistered = 1;

		// Token: 0x04000C3A RID: 3130
		public const byte k_ECallbackFlagsGameServer = 2;

		// Token: 0x04000C3B RID: 3131
		public IntPtr m_vfptr;

		// Token: 0x04000C3C RID: 3132
		public byte m_nCallbackFlags;

		// Token: 0x04000C3D RID: 3133
		public int m_iCallback;
	}
}
