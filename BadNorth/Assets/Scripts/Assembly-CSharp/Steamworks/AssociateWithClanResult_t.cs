using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200023E RID: 574
	[CallbackIdentity(210)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct AssociateWithClanResult_t
	{
		// Token: 0x04000565 RID: 1381
		public const int k_iCallback = 210;

		// Token: 0x04000566 RID: 1382
		public EResult m_eResult;
	}
}
