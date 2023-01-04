using System;
using UnityEngine;

namespace CS.Lights
{
	// Token: 0x02000562 RID: 1378
	public class ButtonLight : LightEffect
	{
		// Token: 0x060023EC RID: 9196 RVA: 0x0007050C File Offset: 0x0006E90C
		protected override void EffectLogic(SetLightingEvent effect)
		{
			for (uint num = 0U; num < 3U; num += 1U)
			{
				effect.SetPoint(num, 0U, this.baseCol, 0);
			}
			effect.SetPoint(1U, 0U, this.overlayCol, 1);
			this.testCols();
		}

		// Token: 0x060023ED RID: 9197 RVA: 0x00070550 File Offset: 0x0006E950
		private void testCols()
		{
			this.r = (this.g = (this.b = (this.a = 0f)));
			this.r = this.overlayCol.a * this.overlayCol.r + (1f - this.overlayCol.a) * this.baseCol.a * this.baseCol.r;
			this.g = this.overlayCol.a * this.overlayCol.g + (1f - this.overlayCol.a) * this.baseCol.a * this.baseCol.g;
			this.b = this.overlayCol.a * this.overlayCol.b + (1f - this.overlayCol.a) * this.baseCol.a * this.baseCol.b;
			this.a = this.overlayCol.a * this.overlayCol.a + (1f - this.overlayCol.a) * this.baseCol.a * this.baseCol.a;
		}

		// Token: 0x04001686 RID: 5766
		public float r;

		// Token: 0x04001687 RID: 5767
		public float g;

		// Token: 0x04001688 RID: 5768
		public float b;

		// Token: 0x04001689 RID: 5769
		public float a;

		// Token: 0x0400168A RID: 5770
		public Color overlayCol = Color.clear;

		// Token: 0x0400168B RID: 5771
		public Color baseCol = Color.white;
	}
}
