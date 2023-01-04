using System;
using UnityEngine.Events;

namespace Voxels.TowerDefense
{
	// Token: 0x02000730 RID: 1840
	public class ConfirmButtonEvent : ConfirmButton
	{
		// Token: 0x06002FC6 RID: 12230 RVA: 0x000C3251 File Offset: 0x000C1651
		protected override void OnConfirmed()
		{
			base.OnConfirmed();
			this.onConfirm.Invoke();
		}

		// Token: 0x06002FC7 RID: 12231 RVA: 0x000C3264 File Offset: 0x000C1664
		protected override void OnGainedFocus()
		{
			base.OnGainedFocus();
			this.onGainedFocus.Invoke();
		}

		// Token: 0x06002FC8 RID: 12232 RVA: 0x000C3277 File Offset: 0x000C1677
		protected override void OnLostFocus()
		{
			base.OnLostFocus();
			this.onLostFocus.Invoke();
		}

		// Token: 0x04001FEC RID: 8172
		public UnityEvent onConfirm;

		// Token: 0x04001FED RID: 8173
		public UnityEvent onGainedFocus;

		// Token: 0x04001FEE RID: 8174
		public UnityEvent onLostFocus;
	}
}
