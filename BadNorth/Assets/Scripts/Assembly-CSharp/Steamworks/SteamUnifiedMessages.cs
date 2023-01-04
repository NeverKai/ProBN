using System;

namespace Steamworks
{
	// Token: 0x02000215 RID: 533
	public static class SteamUnifiedMessages
	{
		// Token: 0x06000E18 RID: 3608 RVA: 0x00025AE4 File Offset: 0x00023EE4
		public static ClientUnifiedMessageHandle SendMethod(string pchServiceMethod, byte[] pRequestBuffer, uint unRequestBufferSize, ulong unContext)
		{
			InteropHelp.TestIfAvailableClient();
			ClientUnifiedMessageHandle result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchServiceMethod))
			{
				result = (ClientUnifiedMessageHandle)NativeMethods.ISteamUnifiedMessages_SendMethod(utf8StringHandle, pRequestBuffer, unRequestBufferSize, unContext);
			}
			return result;
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x00025B30 File Offset: 0x00023F30
		public static bool GetMethodResponseInfo(ClientUnifiedMessageHandle hHandle, out uint punResponseSize, out EResult peResult)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUnifiedMessages_GetMethodResponseInfo(hHandle, out punResponseSize, out peResult);
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x00025B3F File Offset: 0x00023F3F
		public static bool GetMethodResponseData(ClientUnifiedMessageHandle hHandle, byte[] pResponseBuffer, uint unResponseBufferSize, bool bAutoRelease)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUnifiedMessages_GetMethodResponseData(hHandle, pResponseBuffer, unResponseBufferSize, bAutoRelease);
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x00025B4F File Offset: 0x00023F4F
		public static bool ReleaseMethod(ClientUnifiedMessageHandle hHandle)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamUnifiedMessages_ReleaseMethod(hHandle);
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x00025B5C File Offset: 0x00023F5C
		public static bool SendNotification(string pchServiceNotification, byte[] pNotificationBuffer, uint unNotificationBufferSize)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchServiceNotification))
			{
				result = NativeMethods.ISteamUnifiedMessages_SendNotification(utf8StringHandle, pNotificationBuffer, unNotificationBufferSize);
			}
			return result;
		}
	}
}
