using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000354 RID: 852
	public static class Packsize
	{
		// Token: 0x060012AA RID: 4778 RVA: 0x00027A90 File Offset: 0x00025E90
		public static bool Test()
		{
			int num = Marshal.SizeOf(typeof(Packsize.ValvePackingSentinel_t));
			int num2 = Marshal.SizeOf(typeof(RemoteStorageEnumerateUserSubscribedFilesResult_t));
			return num == 32 && num2 == 616;
		}

		// Token: 0x04000C6A RID: 3178
		public const int value = 8;

		// Token: 0x02000355 RID: 853
		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		private struct ValvePackingSentinel_t
		{
			// Token: 0x04000C6B RID: 3179
			private uint m_u32;

			// Token: 0x04000C6C RID: 3180
			private ulong m_u64;

			// Token: 0x04000C6D RID: 3181
			private ushort m_u16;

			// Token: 0x04000C6E RID: 3182
			private double m_d;
		}
	}
}
