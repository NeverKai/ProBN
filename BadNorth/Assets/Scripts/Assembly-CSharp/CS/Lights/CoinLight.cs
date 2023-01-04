using System;
using UnityEngine;

namespace CS.Lights
{
	// Token: 0x02000563 RID: 1379
	public class CoinLight : LightEffect
	{
		// Token: 0x060023EF RID: 9199 RVA: 0x000706B2 File Offset: 0x0006EAB2
		public void ShowCoin(bool display)
		{
			this.flashCoin = display;
		}

		// Token: 0x060023F0 RID: 9200 RVA: 0x000706BC File Offset: 0x0006EABC
		protected override void EffectLogic(SetLightingEvent effect)
		{
			if (this.flashCoin)
			{
				effect.SetPoint(0U, 0U, this.coinCol, this._layer);
				effect.SetPoint(1U, 0U, this.coinCol, this._layer);
				effect.SetPoint(2U, 0U, this.coinCol, this._layer);
			}
		}

		// Token: 0x0400168C RID: 5772
		public Color coinCol = Color.yellow;

		// Token: 0x0400168D RID: 5773
		private bool flashCoin;
	}
}
