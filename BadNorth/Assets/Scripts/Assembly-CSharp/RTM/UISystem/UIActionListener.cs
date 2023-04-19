using System;
using System.Diagnostics;
using UnityEngine;

namespace RTM.UISystem
{
	// Token: 0x020004D2 RID: 1234
	public sealed class UIActionListener : MonoBehaviour
	{
		// Token: 0x14000062 RID: 98
		// (add) Token: 0x06001F1C RID: 7964 RVA: 0x00053594 File Offset: 0x00051994
		// (remove) Token: 0x06001F1D RID: 7965 RVA: 0x000535CC File Offset: 0x000519CC
		
		public event Action onActionRecieved = delegate()
		{
		};

		// Token: 0x06001F1E RID: 7966 RVA: 0x00053602 File Offset: 0x00051A02
		public void ReceiveAction()
		{
			this.onActionRecieved();
		}

		// Token: 0x04001354 RID: 4948
		public EUIPadAction action = EUIPadAction.Secondary;
	}
}
