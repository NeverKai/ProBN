using System;
using System.Runtime.InteropServices;
using CS.Platform.Utils;

namespace CS.Platform
{
	// Token: 0x02000068 RID: 104
	public static class PlatformLibs
	{
		// Token: 0x060004C1 RID: 1217
		[DllImport("user32.dll")]
		public static extern IntPtr FindWindow(string className, string windowName);

		// Token: 0x060004C2 RID: 1218 RVA: 0x00013F98 File Offset: 0x00012398
		public static IntPtr GetWindowHandle()
		{
			IntPtr intPtr = PlatformLibs.FindWindow(null, "Gang Beasts");
			if (IntPtr.Zero == intPtr)
			{
				Debug.LogError("[SU] Failed find window: {0}", new object[]
				{
					intPtr
				});
			}
			return intPtr;
		}

		// Token: 0x060004C3 RID: 1219
		[DllImport("user32.dll", SetLastError = true)]
		private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

		// Token: 0x060004C4 RID: 1220
		[DllImport("user32.dll")]
		private static extern uint GetWindowThreadProcessId(IntPtr hWnd, IntPtr ProcessId);

		// Token: 0x060004C5 RID: 1221
		[DllImport("kernel32.dll")]
		private static extern uint GetCurrentThreadId();

		// Token: 0x060004C6 RID: 1222
		[DllImport("user32.dll")]
		private static extern IntPtr GetForegroundWindow();

		// Token: 0x060004C7 RID: 1223
		[DllImport("user32.dll")]
		private static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);

		// Token: 0x060004C8 RID: 1224
		[DllImport("user32.dll", SetLastError = true)]
		private static extern bool BringWindowToTop(IntPtr hWnd);

		// Token: 0x060004C9 RID: 1225
		[DllImport("user32.dll", SetLastError = true)]
		private static extern bool BringWindowToTop(HandleRef hWnd);

		// Token: 0x060004CA RID: 1226
		[DllImport("user32.dll")]
		private static extern bool ShowWindow(IntPtr hWnd, uint nCmdShow);

		// Token: 0x060004CB RID: 1227 RVA: 0x00013FDC File Offset: 0x000123DC
		public static bool GetFocus()
		{
			uint windowThreadProcessId = PlatformLibs.GetWindowThreadProcessId(PlatformLibs.GetForegroundWindow(), IntPtr.Zero);
			uint currentThreadId = PlatformLibs.GetCurrentThreadId();
			IntPtr windowHandle = PlatformLibs.GetWindowHandle();
			if (windowHandle == IntPtr.Zero)
			{
				return false;
			}
			if (windowThreadProcessId != currentThreadId)
			{
				bool flag = PlatformLibs.AttachThreadInput(windowThreadProcessId, currentThreadId, true);
				bool flag2 = PlatformLibs.BringWindowToTop(windowHandle);
				bool flag3 = PlatformLibs.ShowWindow(windowHandle, 5U);
				bool flag4 = PlatformLibs.AttachThreadInput(windowThreadProcessId, currentThreadId, false);
				if (!flag || !flag2 || !flag3 || !flag4)
				{
					Debug.LogError("[SU] Failed Hidden Focus Steal | attach: {0} | Bring: {1} | Show: {2} | Dettach: {3}", new object[]
					{
						flag,
						flag2,
						flag3,
						flag4
					});
				}
				else
				{
					Debug.LogInfo("[SU] Hidden Focus Steal | attach: {0} | Bring: {1} | Show: {2} | Dettach: {3}", new object[]
					{
						flag,
						flag2,
						flag3,
						flag4
					});
				}
			}
			else
			{
				bool flag5 = PlatformLibs.BringWindowToTop(windowHandle);
				bool flag6 = PlatformLibs.ShowWindow(windowHandle, 5U);
				if (!flag5 || !flag6)
				{
					Debug.LogError("[SU] Failed Forgrounded Focus | Bring: {0} | Show: {1}", new object[]
					{
						flag5,
						flag6
					});
				}
				else
				{
					Debug.LogInfo("[SU] Forgrounded Focus | Bring: {0} | Show: {1}", new object[]
					{
						flag5,
						flag6
					});
				}
			}
			return true;
		}
	}
}
