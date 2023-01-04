using System;
using System.Runtime.InteropServices;

namespace Discord
{
	// Token: 0x020000E9 RID: 233
	internal class Utility
	{
		// Token: 0x060006BC RID: 1724 RVA: 0x0001C8CF File Offset: 0x0001ACCF
		internal static IntPtr Retain<T>(T value)
		{
			return GCHandle.ToIntPtr(GCHandle.Alloc(value, GCHandleType.Normal));
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x0001C8E4 File Offset: 0x0001ACE4
		internal static void Release(IntPtr ptr)
		{
			GCHandle.FromIntPtr(ptr).Free();
		}
	}
}
