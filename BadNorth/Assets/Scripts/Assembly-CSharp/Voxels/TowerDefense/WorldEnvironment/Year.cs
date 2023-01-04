using System;
using UnityEngine;

namespace Voxels.TowerDefense.WorldEnvironment
{
	// Token: 0x020006E0 RID: 1760
	[Serializable]
	public struct Year
	{
		// Token: 0x06002D79 RID: 11641 RVA: 0x000ADC74 File Offset: 0x000AC074
		public Year(float value)
		{
			this.value = value;
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06002D7A RID: 11642 RVA: 0x000ADC7D File Offset: 0x000AC07D
		// (set) Token: 0x06002D7B RID: 11643 RVA: 0x000ADC9D File Offset: 0x000AC09D
		public float yearFraction
		{
			get
			{
				return (this.value - Mathf.Floor(this.value) + 1f) % 1f;
			}
			set
			{
				this.value = Mathf.Floor(this.value) + value + (float)((value >= this.yearFraction) ? 0 : 1);
			}
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06002D7C RID: 11644 RVA: 0x000ADCC7 File Offset: 0x000AC0C7
		public Season season
		{
			get
			{
				return (Season)(Mathf.RoundToInt(this.yearFraction * 4f) % 4);
			}
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06002D7D RID: 11645 RVA: 0x000ADCDC File Offset: 0x000AC0DC
		public float seasonRadian
		{
			get
			{
				return this.value * 2f * 3.1415927f;
			}
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06002D7E RID: 11646 RVA: 0x000ADCF0 File Offset: 0x000AC0F0
		public float winterSummerWave
		{
			get
			{
				return Mathf.Cos(this.seasonRadian);
			}
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06002D7F RID: 11647 RVA: 0x000ADCFD File Offset: 0x000AC0FD
		public float springAutumnWave
		{
			get
			{
				return Mathf.Sin(this.seasonRadian);
			}
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06002D80 RID: 11648 RVA: 0x000ADD0A File Offset: 0x000AC10A
		public float winterness
		{
			get
			{
				return 0.5f + 0.5f * this.winterSummerWave;
			}
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06002D81 RID: 11649 RVA: 0x000ADD1E File Offset: 0x000AC11E
		public float summerness
		{
			get
			{
				return 0.5f - 0.5f * this.winterSummerWave;
			}
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06002D82 RID: 11650 RVA: 0x000ADD32 File Offset: 0x000AC132
		public float springiness
		{
			get
			{
				return 0.5f + 0.5f * this.springAutumnWave;
			}
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06002D83 RID: 11651 RVA: 0x000ADD46 File Offset: 0x000AC146
		public float autumness
		{
			get
			{
				return 0.5f - 0.5f * this.springAutumnWave;
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06002D84 RID: 11652 RVA: 0x000ADD5A File Offset: 0x000AC15A
		// (set) Token: 0x06002D85 RID: 11653 RVA: 0x000ADD68 File Offset: 0x000AC168
		public float months
		{
			get
			{
				return this.value * 12f;
			}
			set
			{
				this.value = value / 12f;
			}
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06002D86 RID: 11654 RVA: 0x000ADD77 File Offset: 0x000AC177
		// (set) Token: 0x06002D87 RID: 11655 RVA: 0x000ADD86 File Offset: 0x000AC186
		public Month month
		{
			get
			{
				return (Month)(this.months % 12f);
			}
			set
			{
				this.yearFraction = (float)value / 12f;
			}
		}

		// Token: 0x06002D88 RID: 11656 RVA: 0x000ADD96 File Offset: 0x000AC196
		public static implicit operator float(Year d)
		{
			return d.value;
		}

		// Token: 0x06002D89 RID: 11657 RVA: 0x000ADD9F File Offset: 0x000AC19F
		public static implicit operator Year(float f)
		{
			return new Year(f);
		}

		// Token: 0x04001E0D RID: 7693
		public float value;
	}
}
