using System;
using UnityEngine.Events;

namespace Voxels.TowerDefense
{
	// Token: 0x020007FE RID: 2046
	public class StateChangeEvent : StateListener
	{
		// Token: 0x0600359D RID: 13725 RVA: 0x000E6365 File Offset: 0x000E4765
		public override void OnActiveChange(bool active)
		{
			if (active)
			{
				this.OnActivate.Invoke();
			}
			else
			{
				this.OnDeactivate.Invoke();
			}
		}

		// Token: 0x04002462 RID: 9314
		public UnityEvent OnActivate;

		// Token: 0x04002463 RID: 9315
		public UnityEvent OnDeactivate;
	}
}
