using System;
using System.Threading;
using UnityEngine;

namespace CS.Platform.Utils
{
	// Token: 0x0200006C RID: 108
	public static class Debug
	{
		// Token: 0x060004E7 RID: 1255 RVA: 0x0001436B File Offset: 0x0001276B
		public static void SetDebugColour()
		{
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060004E8 RID: 1256 RVA: 0x0001436D File Offset: 0x0001276D
		public static bool IsDebug
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x00014370 File Offset: 0x00012770
		public static void LogError(string message, params object[] args)
		{
			Debug.LogErrorFormat(message, args);
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x00014379 File Offset: 0x00012779
		public static void LogWarning(string message, params object[] args)
		{
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x0001437B File Offset: 0x0001277B
		public static void LogInfo(string message, params object[] args)
		{
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00014380 File Offset: 0x00012780
		public static void ThreadLogError(string message, params object[] args)
		{
			string name = Thread.CurrentThread.ManagedThreadId + " | " + Thread.CurrentThread.Name;
			BasePlatformManager.Instance.AddToNextUpdate(delegate
			{
				Debug.LogError("[Thread: " + name + "] | " + message, args);
			});
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x000143E0 File Offset: 0x000127E0
		public static void ThreadLogWarning(string message, params object[] args)
		{
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x000143E2 File Offset: 0x000127E2
		public static void ThreadLogInfo(string message, params object[] args)
		{
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x000143E4 File Offset: 0x000127E4
		private static Texture2D _DebugBox
		{
			get
			{
				if (Debug._debugBox == null)
				{
					Debug._debugBox = new Texture2D(1, 1);
					Debug._debugBox.SetPixel(0, 0, Color.magenta);
					Debug._debugBox.Apply();
				}
				return Debug._debugBox;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060004F0 RID: 1264 RVA: 0x00014422 File Offset: 0x00012822
		public static bool ShowInfoPlus
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00014425 File Offset: 0x00012825
		public static void DrawGUIBox(Rect area, Color colour)
		{
		}

		// Token: 0x040001FC RID: 508
		private static Texture2D _debugBox;

		// Token: 0x0200006D RID: 109
		public static class XboxLayout
		{
			// Token: 0x040001FD RID: 509
			public static Rect UtilData = new Rect(25f, 25f, 300f, 500f);

			// Token: 0x040001FE RID: 510
			public static Rect SaveData = new Rect(25f, 550f, 300f, 500f);

			// Token: 0x040001FF RID: 511
			public static Rect UserData = new Rect(350f, 25f, 300f, 1030f);

			// Token: 0x04000200 RID: 512
			public static Rect AchievementData = new Rect(675f, 25f, 300f, 500f);

			// Token: 0x04000201 RID: 513
			public static Rect StatData = new Rect(675f, 550f, 300f, 500f);

			// Token: 0x04000202 RID: 514
			public static Rect LobbyData = new Rect(1000f, 25f, 300f, 1030f);

			// Token: 0x04000203 RID: 515
			public static Rect NetworkData = new Rect(1325f, 25f, 300f, 1030f);

			// Token: 0x04000204 RID: 516
			public static Rect VoiceData = new Rect(1650f, 25f, 300f, 500f);

			// Token: 0x04000205 RID: 517
			public static Rect Manager = new Rect(1650f, 550f, 300f, 500f);
		}

		// Token: 0x0200006E RID: 110
		public static class PS4Layout
		{
			// Token: 0x04000206 RID: 518
			public static Rect MainData = new Rect(25f, 25f, 600f, 300f);

			// Token: 0x04000207 RID: 519
			public static Rect UtilData = new Rect(25f, 325f, 300f, 300f);

			// Token: 0x04000208 RID: 520
			public static Rect SaveData = new Rect(25f, 650f, 300f, 300f);

			// Token: 0x04000209 RID: 521
			public static Rect UserData = new Rect(350f, 325f, 300f, 730f);

			// Token: 0x0400020A RID: 522
			public static Rect AchievementData = new Rect(675f, 25f, 300f, 500f);

			// Token: 0x0400020B RID: 523
			public static Rect StatData = new Rect(675f, 550f, 300f, 500f);

			// Token: 0x0400020C RID: 524
			public static Rect LobbyData = new Rect(1000f, 25f, 300f, 1030f);

			// Token: 0x0400020D RID: 525
			public static Rect NetworkData = new Rect(1325f, 25f, 300f, 1030f);

			// Token: 0x0400020E RID: 526
			public static Rect VoiceData = new Rect(1650f, 25f, 300f, 1030f);
		}

		// Token: 0x0200006F RID: 111
		public static class SwitchLayout
		{
			// Token: 0x0400020F RID: 527
			public static Rect UtilData = new Rect(25f, 25f, 300f, 500f);

			// Token: 0x04000210 RID: 528
			public static Rect SaveData = new Rect(325f, 25f, 300f, 500f);
		}
	}
}
