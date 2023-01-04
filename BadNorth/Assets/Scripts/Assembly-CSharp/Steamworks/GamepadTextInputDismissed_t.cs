using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020002C7 RID: 711
	[CallbackIdentity(714)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GamepadTextInputDismissed_t
	{
		// Token: 0x04000760 RID: 1888
		public const int k_iCallback = 714;

		// Token: 0x04000761 RID: 1889
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bSubmitted;

		// Token: 0x04000762 RID: 1890
		public uint m_unSubmittedText;
	}
}
