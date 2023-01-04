using System;
using ReflexCLI.Attributes;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Voxels.TowerDefense.UI.GridScrolling
{
	// Token: 0x02000924 RID: 2340
	public class ScrollGridSwitcher : UIBehaviour
	{
		// Token: 0x06003EF5 RID: 16117 RVA: 0x0011C0DB File Offset: 0x0011A4DB
		[ConsoleCommand("")]
		public void DoSwitch()
		{
			this.grid.ChangeSetup((!this.alternate) ? this.def1 : this.def0, false, null);
			this.alternate = !this.alternate;
		}

		// Token: 0x04002BF7 RID: 11255
		[SerializeField]
		private ScrollGrid grid;

		// Token: 0x04002BF8 RID: 11256
		[SerializeField]
		private ScrollGrid.Def def0;

		// Token: 0x04002BF9 RID: 11257
		[SerializeField]
		private ScrollGrid.Def def1;

		// Token: 0x04002BFA RID: 11258
		private bool alternate;
	}
}
