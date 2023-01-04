using System;
using System.Diagnostics;
using ReflexCLI.UI;
using UnityEngine;

namespace ReflexCLI.User
{
	// Token: 0x0200045F RID: 1119
	public static class ConsoleStatus
	{
		// Token: 0x14000059 RID: 89
		// (add) Token: 0x06001976 RID: 6518 RVA: 0x000433F8 File Offset: 0x000417F8
		// (remove) Token: 0x06001977 RID: 6519 RVA: 0x0004342C File Offset: 0x0004182C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action OnConsoleOpened;

		// Token: 0x1400005A RID: 90
		// (add) Token: 0x06001978 RID: 6520 RVA: 0x00043460 File Offset: 0x00041860
		// (remove) Token: 0x06001979 RID: 6521 RVA: 0x00043494 File Offset: 0x00041894
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action OnConsoleClosed;

		// Token: 0x0600197A RID: 6522 RVA: 0x000434C8 File Offset: 0x000418C8
		public static bool IsConsoleOpen()
		{
			return ReflexUIManager.IsConsoleOpen();
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x000434CF File Offset: 0x000418CF
		public static void OpenConsole()
		{
			ReflexUIManager.StaticOpen();
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x000434D6 File Offset: 0x000418D6
		public static void CloseConsole()
		{
			ReflexUIManager.StaticClose();
		}

		// Token: 0x0600197D RID: 6525 RVA: 0x000434DD File Offset: 0x000418DD
		[RuntimeInitializeOnLoadMethod]
		private static void Init()
		{
			ReflexUIManager.OnConsoleOpened += ConsoleStatus.OnConsoleOpened;
			ReflexUIManager.OnConsoleClosed += ConsoleStatus.OnConsoleClosed;
		}

		// Token: 0x0600197E RID: 6526 RVA: 0x000434F3 File Offset: 0x000418F3
		// Note: this type is marked as 'beforefieldinit'.
		static ConsoleStatus()
		{
			ConsoleStatus.OnConsoleOpened = delegate()
			{
			};
			ConsoleStatus.OnConsoleClosed = delegate()
			{
			};
		}
	}
}
