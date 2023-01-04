using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200031F RID: 799
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct P2PSessionState_t
	{
		// Token: 0x04000BFA RID: 3066
		public byte m_bConnectionActive;

		// Token: 0x04000BFB RID: 3067
		public byte m_bConnecting;

		// Token: 0x04000BFC RID: 3068
		public byte m_eP2PSessionError;

		// Token: 0x04000BFD RID: 3069
		public byte m_bUsingRelay;

		// Token: 0x04000BFE RID: 3070
		public int m_nBytesQueuedForSend;

		// Token: 0x04000BFF RID: 3071
		public int m_nPacketsQueuedForSend;

		// Token: 0x04000C00 RID: 3072
		public uint m_nRemoteIP;

		// Token: 0x04000C01 RID: 3073
		public ushort m_nRemotePort;
	}
}
