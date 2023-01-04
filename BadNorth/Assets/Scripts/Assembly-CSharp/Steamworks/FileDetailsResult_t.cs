using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000221 RID: 545
	[CallbackIdentity(1023)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct FileDetailsResult_t
	{
		// Token: 0x040004FC RID: 1276
		public const int k_iCallback = 1023;

		// Token: 0x040004FD RID: 1277
		public EResult m_eResult;

		// Token: 0x040004FE RID: 1278
		public ulong m_ulFileSize;

		// Token: 0x040004FF RID: 1279
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
		public byte[] m_FileSHA;

		// Token: 0x04000500 RID: 1280
		public uint m_unFlags;
	}
}
