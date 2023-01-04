using System;
using UnityEngine;

namespace Steamworks
{
	// Token: 0x02000325 RID: 805
	public static class CallbackDispatcher
	{
		// Token: 0x060011EF RID: 4591 RVA: 0x000268EA File Offset: 0x00024CEA
		public static void ExceptionHandler(Exception e)
		{
			Debug.LogException(e);
		}
	}
}
