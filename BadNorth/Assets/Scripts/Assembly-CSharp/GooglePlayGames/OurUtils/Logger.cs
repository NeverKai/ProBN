using System;
using UnityEngine;

namespace GooglePlayGames.OurUtils
{
	// Token: 0x020003BC RID: 956
	public class Logger
	{
		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06001568 RID: 5480 RVA: 0x0002C06E File Offset: 0x0002A46E
		// (set) Token: 0x06001569 RID: 5481 RVA: 0x0002C075 File Offset: 0x0002A475
		public static bool DebugLogEnabled
		{
			get
			{
				return Logger.debugLogEnabled;
			}
			set
			{
				Logger.debugLogEnabled = value;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600156A RID: 5482 RVA: 0x0002C07D File Offset: 0x0002A47D
		// (set) Token: 0x0600156B RID: 5483 RVA: 0x0002C084 File Offset: 0x0002A484
		public static bool WarningLogEnabled
		{
			get
			{
				return Logger.warningLogEnabled;
			}
			set
			{
				Logger.warningLogEnabled = value;
			}
		}

		// Token: 0x0600156C RID: 5484 RVA: 0x0002C08C File Offset: 0x0002A48C
		public static void d(string msg)
		{
			if (Logger.debugLogEnabled)
			{
				PlayGamesHelperObject.RunOnGameThread(delegate
				{
					Debug.Log(Logger.ToLogMessage(string.Empty, "DEBUG", msg));
				});
			}
		}

		// Token: 0x0600156D RID: 5485 RVA: 0x0002C0C4 File Offset: 0x0002A4C4
		public static void w(string msg)
		{
			if (Logger.warningLogEnabled)
			{
				PlayGamesHelperObject.RunOnGameThread(delegate
				{
					Debug.LogWarning(Logger.ToLogMessage("!!!", "WARNING", msg));
				});
			}
		}

		// Token: 0x0600156E RID: 5486 RVA: 0x0002C0FC File Offset: 0x0002A4FC
		public static void e(string msg)
		{
			if (Logger.warningLogEnabled)
			{
				PlayGamesHelperObject.RunOnGameThread(delegate
				{
					Debug.LogWarning(Logger.ToLogMessage("***", "ERROR", msg));
				});
			}
		}

		// Token: 0x0600156F RID: 5487 RVA: 0x0002C131 File Offset: 0x0002A531
		public static string describe(byte[] b)
		{
			return (b != null) ? ("byte[" + b.Length + "]") : "(null)";
		}

		// Token: 0x06001570 RID: 5488 RVA: 0x0002C15C File Offset: 0x0002A55C
		private static string ToLogMessage(string prefix, string logType, string msg)
		{
			return string.Format("{0} [Play Games Plugin DLL] {1} {2}: {3}", new object[]
			{
				prefix,
				DateTime.Now.ToString("MM/dd/yy H:mm:ss zzz"),
				logType,
				msg
			});
		}

		// Token: 0x04000D91 RID: 3473
		private static bool debugLogEnabled;

		// Token: 0x04000D92 RID: 3474
		private static bool warningLogEnabled = true;
	}
}
