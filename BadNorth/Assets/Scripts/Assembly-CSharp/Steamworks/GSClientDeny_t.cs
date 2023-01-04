using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000237 RID: 567
	[CallbackIdentity(202)]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct GSClientDeny_t
	{
		// Token: 0x04000546 RID: 1350
		public const int k_iCallback = 202;

		// Token: 0x04000547 RID: 1351
		public CSteamID m_SteamID;

		// Token: 0x04000548 RID: 1352
		public EDenyReason m_eDenyReason;

		// Token: 0x04000549 RID: 1353
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string m_rgchOptionalText;
	}
}
