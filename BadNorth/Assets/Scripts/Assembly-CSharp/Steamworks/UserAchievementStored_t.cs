using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020002B8 RID: 696
	[CallbackIdentity(1103)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct UserAchievementStored_t
	{
		// Token: 0x0400072F RID: 1839
		public const int k_iCallback = 1103;

		// Token: 0x04000730 RID: 1840
		public ulong m_nGameID;

		// Token: 0x04000731 RID: 1841
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bGroupAchievement;

		// Token: 0x04000732 RID: 1842
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string m_rgchAchievementName;

		// Token: 0x04000733 RID: 1843
		public uint m_nCurProgress;

		// Token: 0x04000734 RID: 1844
		public uint m_nMaxProgress;
	}
}
