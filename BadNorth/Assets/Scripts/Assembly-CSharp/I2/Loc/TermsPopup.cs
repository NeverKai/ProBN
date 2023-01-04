using System;
using UnityEngine;

namespace I2.Loc
{
	// Token: 0x02000408 RID: 1032
	public class TermsPopup : PropertyAttribute
	{
		// Token: 0x060017F7 RID: 6135 RVA: 0x0003C943 File Offset: 0x0003AD43
		public TermsPopup(string filter = "")
		{
			this.Filter = filter;
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060017F8 RID: 6136 RVA: 0x0003C952 File Offset: 0x0003AD52
		// (set) Token: 0x060017F9 RID: 6137 RVA: 0x0003C95A File Offset: 0x0003AD5A
		public string Filter { get; private set; }
	}
}
