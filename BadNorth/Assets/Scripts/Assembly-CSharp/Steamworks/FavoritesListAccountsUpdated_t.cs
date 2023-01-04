using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200026A RID: 618
	[CallbackIdentity(516)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct FavoritesListAccountsUpdated_t
	{
		// Token: 0x04000620 RID: 1568
		public const int k_iCallback = 516;

		// Token: 0x04000621 RID: 1569
		public EResult m_eResult;
	}
}
