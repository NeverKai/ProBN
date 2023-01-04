using System;
using System.Diagnostics;
using UnityEngine;

namespace ReflexCLI.UI
{
	// Token: 0x0200045E RID: 1118
	public class ReflexUIManager : MonoBehaviour
	{
		// Token: 0x14000057 RID: 87
		// (add) Token: 0x0600196B RID: 6507 RVA: 0x000432EC File Offset: 0x000416EC
		// (remove) Token: 0x0600196C RID: 6508 RVA: 0x00043320 File Offset: 0x00041720
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action OnConsoleOpened;

		// Token: 0x14000058 RID: 88
		// (add) Token: 0x0600196D RID: 6509 RVA: 0x00043354 File Offset: 0x00041754
		// (remove) Token: 0x0600196E RID: 6510 RVA: 0x00043388 File Offset: 0x00041788
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action OnConsoleClosed;

		// Token: 0x0600196F RID: 6511 RVA: 0x000433BC File Offset: 0x000417BC
		private void Awake()
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x06001970 RID: 6512 RVA: 0x000433C9 File Offset: 0x000417C9
		public static bool IsConsoleOpen()
		{
			return false;
		}

		// Token: 0x06001971 RID: 6513 RVA: 0x000433CC File Offset: 0x000417CC
		public static void StaticOpen()
		{
		}

		// Token: 0x06001972 RID: 6514 RVA: 0x000433CE File Offset: 0x000417CE
		public static void StaticClose()
		{
		}

		// Token: 0x06001973 RID: 6515 RVA: 0x000433D0 File Offset: 0x000417D0
		// Note: this type is marked as 'beforefieldinit'.
		static ReflexUIManager()
		{
			ReflexUIManager.OnConsoleOpened = delegate()
			{
			};
			ReflexUIManager.OnConsoleClosed = delegate()
			{
			};
		}
	}
}
