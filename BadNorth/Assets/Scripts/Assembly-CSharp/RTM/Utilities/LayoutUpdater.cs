using System;
using UnityEngine;

namespace RTM.Utilities
{
	// Token: 0x020004E8 RID: 1256
	public class LayoutUpdater : MonoBehaviour
	{
		// Token: 0x06002033 RID: 8243 RVA: 0x00056D78 File Offset: 0x00055178
		public void UpdateLayouts()
		{
			this.UpdateLayoutsInternal();
		}

		// Token: 0x06002034 RID: 8244 RVA: 0x00056D80 File Offset: 0x00055180
		private void UpdateLayoutsInternal()
		{
			this.root = ((!this.root) ? base.transform : this.root);
			if (this.root.gameObject.activeInHierarchy)
			{
				this.root.ForceChildLayoutUpdates(false);
			}
		}

		// Token: 0x06002035 RID: 8245 RVA: 0x00056DD5 File Offset: 0x000551D5
		public void OnEnable()
		{
			if (this.updateOnEnable)
			{
				this.UpdateLayoutsInternal();
			}
		}

		// Token: 0x04001406 RID: 5126
		[SerializeField]
		private Transform root;

		// Token: 0x04001407 RID: 5127
		[Header("Behaviour")]
		[SerializeField]
		private bool updateOnEnable;
	}
}
