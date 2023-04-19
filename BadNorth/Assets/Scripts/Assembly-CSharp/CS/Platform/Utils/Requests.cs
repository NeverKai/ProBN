using System;
using System.Diagnostics;

namespace CS.Platform.Utils
{
	// Token: 0x02000072 RID: 114
	public static class Requests
	{
		// Token: 0x14000037 RID: 55
		// (add) Token: 0x06000515 RID: 1301 RVA: 0x000153E0 File Offset: 0x000137E0
		// (remove) Token: 0x06000516 RID: 1302 RVA: 0x00015414 File Offset: 0x00013814
		
		public static event Requests.PlatformVoiceConnectionDel VoiceConnectionAttempt;

		// Token: 0x06000517 RID: 1303 RVA: 0x00015448 File Offset: 0x00013848
		public static bool GetVoiceConnectionAttemptResult(BaseUserInfo userInfo)
		{
			return Requests.VoiceConnectionAttempt == null || Requests.VoiceConnectionAttempt(userInfo);
		}

		// Token: 0x14000038 RID: 56
		// (add) Token: 0x06000518 RID: 1304 RVA: 0x00015464 File Offset: 0x00013864
		// (remove) Token: 0x06000519 RID: 1305 RVA: 0x00015498 File Offset: 0x00013898
		
		public static event Requests.PlatformLocaliseDel TextLocalise;

		// Token: 0x0600051A RID: 1306 RVA: 0x000154CC File Offset: 0x000138CC
		public static string GetTextLocalise(string key, string[] parameters)
		{
			if (Requests.TextLocalise != null)
			{
				return Requests.TextLocalise(key, parameters);
			}
			return key;
		}

		// Token: 0x02000073 RID: 115
		// (Invoke) Token: 0x0600051C RID: 1308
		public delegate bool PlatformVoiceConnectionDel(BaseUserInfo userInfo);

		// Token: 0x02000074 RID: 116
		// (Invoke) Token: 0x06000520 RID: 1312
		public delegate string PlatformLocaliseDel(string key, string[] parameters);
	}
}
