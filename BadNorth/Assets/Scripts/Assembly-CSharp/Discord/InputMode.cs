using System;
using System.Runtime.InteropServices;

namespace Discord
{
	// Token: 0x020000D4 RID: 212
	public struct InputMode
	{
		// Token: 0x040003D6 RID: 982
		public InputModeType Type;

		// Token: 0x040003D7 RID: 983
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string Shortcut;
	}
}
