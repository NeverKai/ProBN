using System;
using System.Collections.Generic;
using UnityEngine;

namespace RTM.Utilities
{
	// Token: 0x020004E3 RID: 1251
	internal class CompilationDependent : MonoBehaviour
	{
		// Token: 0x040013F2 RID: 5106
		[SerializeField]
		private CompilationDependent.EBehaviour Behaviour;

		// Token: 0x040013F3 RID: 5107
		[SerializeField]
		private List<string> Defines;

		// Token: 0x040013F4 RID: 5108
		[SerializeField]
		[HideInInspector]
		private bool isValid;

		// Token: 0x020004E4 RID: 1252
		private enum EBehaviour
		{
			// Token: 0x040013F6 RID: 5110
			RequireAll,
			// Token: 0x040013F7 RID: 5111
			RequireNone,
			// Token: 0x040013F8 RID: 5112
			RequireAny
		}
	}
}
