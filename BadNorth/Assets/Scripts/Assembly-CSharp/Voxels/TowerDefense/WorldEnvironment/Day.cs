using System;
using UnityEngine;

namespace Voxels.TowerDefense.WorldEnvironment
{
	// Token: 0x020006E1 RID: 1761
	[Serializable]
	public struct Day
	{
		// Token: 0x06002D8A RID: 11658 RVA: 0x000ADDA7 File Offset: 0x000AC1A7
		public Day(float value)
		{
			this.value = value;
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06002D8B RID: 11659 RVA: 0x000ADDB0 File Offset: 0x000AC1B0
		// (set) Token: 0x06002D8C RID: 11660 RVA: 0x000ADDD0 File Offset: 0x000AC1D0
		public float dayFraction
		{
			get
			{
				return (this.value - Mathf.Floor(this.value) + 1f) % 1f;
			}
			set
			{
				this.value = Mathf.Floor(this.value) + value + (float)((value >= this.dayFraction) ? 0 : 1);
			}
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06002D8D RID: 11661 RVA: 0x000ADDFA File Offset: 0x000AC1FA
		// (set) Token: 0x06002D8E RID: 11662 RVA: 0x000ADE08 File Offset: 0x000AC208
		public float hours
		{
			get
			{
				return this.value * 24f;
			}
			set
			{
				this.value = value / 24f;
			}
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06002D8F RID: 11663 RVA: 0x000ADE17 File Offset: 0x000AC217
		// (set) Token: 0x06002D90 RID: 11664 RVA: 0x000ADE25 File Offset: 0x000AC225
		public float hour
		{
			get
			{
				return this.dayFraction * 24f;
			}
			set
			{
				this.dayFraction = value / 24f;
			}
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06002D91 RID: 11665 RVA: 0x000ADE34 File Offset: 0x000AC234
		public DayPhase dayPhase
		{
			get
			{
				return (DayPhase)(Mathf.RoundToInt(this.dayFraction * 4f) % 4);
			}
		}

		// Token: 0x06002D92 RID: 11666 RVA: 0x000ADE49 File Offset: 0x000AC249
		public static implicit operator float(Day d)
		{
			return d.value;
		}

		// Token: 0x06002D93 RID: 11667 RVA: 0x000ADE52 File Offset: 0x000AC252
		public static implicit operator Day(float f)
		{
			return new Day(f);
		}

		// Token: 0x04001E0E RID: 7694
		public float value;
	}
}
