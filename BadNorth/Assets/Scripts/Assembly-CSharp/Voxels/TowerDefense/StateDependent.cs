using System;
using Voxels.TowerDefense.UI;

namespace Voxels.TowerDefense
{
	// Token: 0x020007FF RID: 2047
	public class StateDependent : StateListener
	{
		// Token: 0x0600359F RID: 13727 RVA: 0x000E6390 File Offset: 0x000E4790
		public override void OnGameAwake()
		{
			this.visibility = base.GetComponent<IUIVisibility>();
			base.OnGameAwake();
			if (this.visibility != null)
			{
				this.visibility.SetVisible(this.active, true);
			}
		}

		// Token: 0x060035A0 RID: 13728 RVA: 0x000E63C1 File Offset: 0x000E47C1
		public override void OnActiveChange(bool active)
		{
			if (this.visibility != null)
			{
				this.visibility.SetVisible(active, false);
			}
			else
			{
				base.gameObject.SetActive(active);
			}
		}

		// Token: 0x060035A1 RID: 13729 RVA: 0x000E63EC File Offset: 0x000E47EC
		public void OnDisable()
		{
			if (this.visibility != null)
			{
				this.visibility.SetVisible(this.active, true);
			}
		}

		// Token: 0x04002464 RID: 9316
		private IUIVisibility visibility;
	}
}
