using System;
using UnityEngine;

namespace CS.Lights
{
	// Token: 0x02000566 RID: 1382
	public class WeatherLight : LightEffect
	{
		// Token: 0x060023FA RID: 9210 RVA: 0x000709BC File Offset: 0x0006EDBC
		protected override void EffectLogic(SetLightingEvent effect)
		{
			Color clear = Color.clear;
			float max;
			float min;
			if (this.rainIntensity > 0f)
			{
				clear = this.rainCol;
				max = this.rainIntensity * 0.5f;
				min = Mathf.Max(0f, this.rainIntensity - 0.2f);
			}
			else
			{
				if (this.snowIntensity <= 0f)
				{
					return;
				}
				clear = this.snowCol;
				max = this.snowIntensity * 0.5f;
				min = Mathf.Max(0f, this.snowIntensity - 0.2f);
			}
			this.leftVal = Mathf.Clamp(UnityEngine.Random.Range(min, max), this.leftVal - 0.1f, this.leftVal + 0.1f);
			this.rightVal = Mathf.Clamp(UnityEngine.Random.Range(min, max), this.rightVal - 0.1f, this.rightVal + 0.1f);
			clear.a = this.leftVal;
			effect.SetPoint(0U, 0U, clear, this._layer);
			clear.a = this.rightVal;
			effect.SetPoint(2U, 0U, clear, this._layer);
		}

		// Token: 0x04001695 RID: 5781
		public Color rainCol = Color.blue;

		// Token: 0x04001696 RID: 5782
		public Color snowCol = Color.white;

		// Token: 0x04001697 RID: 5783
		public float rainIntensity;

		// Token: 0x04001698 RID: 5784
		public float snowIntensity;

		// Token: 0x04001699 RID: 5785
		private float leftVal;

		// Token: 0x0400169A RID: 5786
		private float rightVal;
	}
}
