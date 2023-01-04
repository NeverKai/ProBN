using System;
using UnityEngine;

namespace CS.Lights
{
	// Token: 0x02000564 RID: 1380
	public class HouseLight : LightEffect
	{
		// Token: 0x060023F2 RID: 9202 RVA: 0x0007072E File Offset: 0x0006EB2E
		public void HouseDestroyed()
		{
			this.flashTimer = this.flashDuration;
			this.lastUpdateTime = Time.unscaledTime;
		}

		// Token: 0x060023F3 RID: 9203 RVA: 0x00070747 File Offset: 0x0006EB47
		public void SetHouseBurning(bool isBurning)
		{
			this.houseIsBurning = isBurning;
		}

		// Token: 0x060023F4 RID: 9204 RVA: 0x00070750 File Offset: 0x0006EB50
		protected override void EffectLogic(SetLightingEvent effect)
		{
			Color explosionCol = this.ExplosionCol;
			if (this.houseIsBurning)
			{
				float min = 0.4f;
				float max = 0.8f;
				explosionCol.a = UnityEngine.Random.Range(min, max);
				effect.SetPoint(0U, 0U, explosionCol, this._layer);
				explosionCol.a = UnityEngine.Random.Range(min, max);
				effect.SetPoint(1U, 0U, explosionCol, this._layer);
				explosionCol.a = UnityEngine.Random.Range(min, max);
				effect.SetPoint(2U, 0U, explosionCol, this._layer);
			}
			if (this.flashTimer > 0f)
			{
				explosionCol.a = this.flashTimer / this.flashDuration;
				effect.SetPoint(0U, 0U, explosionCol, this._layer);
				effect.SetPoint(1U, 0U, explosionCol, this._layer);
				effect.SetPoint(2U, 0U, explosionCol, this._layer);
				float num = Time.unscaledTime - this.lastUpdateTime;
				this.lastUpdateTime = Time.unscaledTime;
				this.flashTimer -= num;
			}
		}

		// Token: 0x0400168E RID: 5774
		public Color ExplosionCol = Color.red;

		// Token: 0x0400168F RID: 5775
		public float flashDuration = 1f;

		// Token: 0x04001690 RID: 5776
		private float flashTimer;

		// Token: 0x04001691 RID: 5777
		private float lastUpdateTime;

		// Token: 0x04001692 RID: 5778
		private bool houseIsBurning;
	}
}
