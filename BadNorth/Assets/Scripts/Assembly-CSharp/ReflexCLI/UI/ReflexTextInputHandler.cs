using System;
using UnityEngine;
using UnityEngine.UI;

namespace ReflexCLI.UI
{
	// Token: 0x0200045D RID: 1117
	public class ReflexTextInputHandler : MonoBehaviour, IUIStyleUpdater
	{
		// Token: 0x06001968 RID: 6504 RVA: 0x000432D2 File Offset: 0x000416D2
		private void Awake()
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x06001969 RID: 6505 RVA: 0x000432DF File Offset: 0x000416DF
		void IUIStyleUpdater.UpdateStyles()
		{
		}

		// Token: 0x04000FC4 RID: 4036
		[SerializeField]
		private InputField InputText;

		// Token: 0x04000FC5 RID: 4037
		[SerializeField]
		private Text SuggestionText;

		// Token: 0x04000FC6 RID: 4038
		[SerializeField]
		private ReflexHistoryUIContainer HistoryContainer;
	}
}
