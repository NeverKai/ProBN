using System;
using UnityEngine;
using UnityEngine.UI;

namespace ReflexCLI.UI
{
	// Token: 0x0200045C RID: 1116
	public class ReflexHistoryUIItem : MonoBehaviour, IUIStyleUpdater
	{
		// Token: 0x06001966 RID: 6502 RVA: 0x000432C8 File Offset: 0x000416C8
		void IUIStyleUpdater.UpdateStyles()
		{
		}

		// Token: 0x04000FC1 RID: 4033
		[SerializeField]
		private Text CommandText;

		// Token: 0x04000FC2 RID: 4034
		[SerializeField]
		private Text ResultText;

		// Token: 0x04000FC3 RID: 4035
		[SerializeField]
		private Text PromptMarker;
	}
}
