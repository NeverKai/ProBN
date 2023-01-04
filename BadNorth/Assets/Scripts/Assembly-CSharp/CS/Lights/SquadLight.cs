using System;
using UnityEngine;

namespace CS.Lights
{
	// Token: 0x02000565 RID: 1381
	public class SquadLight : LightEffect
	{
		// Token: 0x060023F6 RID: 9206 RVA: 0x0007085C File Offset: 0x0006EC5C
		public void SetActive(bool newActive)
		{
			this.isDisplayed = newActive;
		}

		// Token: 0x060023F7 RID: 9207 RVA: 0x00070868 File Offset: 0x0006EC68
		public void SetSquadColour(Color col)
		{
			float h;
			float num;
			float v;
			Color.RGBToHSV(col, out h, out num, out v);
			v = 1f;
			num *= 1.2f;
			this.squadColor = Color.HSVToRGB(h, num, v);
			this.squadColor.r = this.squadColor.r * 1.1f;
			this.squadColor.r = Mathf.Min(this.squadColor.r, 1f);
			this.squadColor.g = this.squadColor.g * 1.2f;
			this.squadColor.g = Mathf.Min(this.squadColor.g, 1f);
			this.squadColor.b = this.squadColor.b * 0.65f;
			this.squadColor.a = 1f;
		}

		// Token: 0x060023F8 RID: 9208 RVA: 0x00070934 File Offset: 0x0006ED34
		protected override void EffectLogic(SetLightingEvent effect)
		{
			if (this.isDisplayed)
			{
				effect.SetPoint(0U, 0U, this.squadColor, this._layer);
				effect.SetPoint(1U, 0U, this.squadColor, this._layer);
				effect.SetPoint(2U, 0U, this.squadColor, this._layer);
				effect.SetPoint(1U, 2U, this.squadColor, this._layer);
			}
		}

		// Token: 0x04001693 RID: 5779
		public Color squadColor = Color.white;

		// Token: 0x04001694 RID: 5780
		private bool isDisplayed;
	}
}
