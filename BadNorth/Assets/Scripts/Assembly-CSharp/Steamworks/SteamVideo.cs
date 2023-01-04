using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000219 RID: 537
	public static class SteamVideo
	{
		// Token: 0x06000E82 RID: 3714 RVA: 0x00026874 File Offset: 0x00024C74
		public static void GetVideoURL(AppId_t unVideoAppID)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamVideo_GetVideoURL(unVideoAppID);
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x00026881 File Offset: 0x00024C81
		public static bool IsBroadcasting(out int pnNumViewers)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamVideo_IsBroadcasting(out pnNumViewers);
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x0002688E File Offset: 0x00024C8E
		public static void GetOPFSettings(AppId_t unVideoAppID)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamVideo_GetOPFSettings(unVideoAppID);
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x0002689C File Offset: 0x00024C9C
		public static bool GetOPFStringForApp(AppId_t unVideoAppID, out string pchBuffer, ref int pnBufferSize)
		{
			InteropHelp.TestIfAvailableClient();
			IntPtr intPtr = Marshal.AllocHGlobal(pnBufferSize);
			bool flag = NativeMethods.ISteamVideo_GetOPFStringForApp(unVideoAppID, intPtr, ref pnBufferSize);
			pchBuffer = ((!flag) ? null : InteropHelp.PtrToStringUTF8(intPtr));
			Marshal.FreeHGlobal(intPtr);
			return flag;
		}
	}
}
