using System;
using UnityEngine;
using UnityEngine.UI;

namespace ReflexCLI.UI
{
	// Token: 0x0200045B RID: 1115
	public class ReflexHistoryUIContainer : MonoBehaviour
	{
		// Token: 0x04000FBE RID: 4030
		[SerializeField]
		private ScrollRect Scroller;

		// Token: 0x04000FBF RID: 4031
		[SerializeField]
		private RectTransform ItemParent;

		// Token: 0x04000FC0 RID: 4032
		[SerializeField]
		private ReflexHistoryUIItem ItemTemplate;
	}
}
