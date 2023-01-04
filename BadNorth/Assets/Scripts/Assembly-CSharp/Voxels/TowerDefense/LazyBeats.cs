using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x02000685 RID: 1669
	public class LazyBeats
	{
		// Token: 0x06002AAA RID: 10922 RVA: 0x000989A4 File Offset: 0x00096DA4
		public LazyBeats(float range = 100f)
		{
			this.offset = UnityEngine.Random.value * range;
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06002AAB RID: 10923 RVA: 0x000989B9 File Offset: 0x00096DB9
		public bool everyOtherSecond
		{
			get
			{
				return this.GetBeat(2f);
			}
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x06002AAC RID: 10924 RVA: 0x000989C6 File Offset: 0x00096DC6
		public bool everySecond
		{
			get
			{
				return this.GetBeat(1f);
			}
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x06002AAD RID: 10925 RVA: 0x000989D3 File Offset: 0x00096DD3
		public bool everyHalfSecond
		{
			get
			{
				return this.GetBeat(0.5f);
			}
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06002AAE RID: 10926 RVA: 0x000989E0 File Offset: 0x00096DE0
		public bool everyQuarterSecond
		{
			get
			{
				return this.GetBeat(0.25f);
			}
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06002AAF RID: 10927 RVA: 0x000989ED File Offset: 0x00096DED
		public bool hz1
		{
			get
			{
				return this.GetBeat(1f);
			}
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06002AB0 RID: 10928 RVA: 0x000989FA File Offset: 0x00096DFA
		public bool hz2
		{
			get
			{
				return this.GetBeat(0.5f);
			}
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06002AB1 RID: 10929 RVA: 0x00098A07 File Offset: 0x00096E07
		public bool hz4
		{
			get
			{
				return this.GetBeat(0.25f);
			}
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06002AB2 RID: 10930 RVA: 0x00098A14 File Offset: 0x00096E14
		public bool hz8
		{
			get
			{
				return this.GetBeat(0.125f);
			}
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x06002AB3 RID: 10931 RVA: 0x00098A21 File Offset: 0x00096E21
		public bool hz16
		{
			get
			{
				return this.GetBeat(0.0625f);
			}
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x06002AB4 RID: 10932 RVA: 0x00098A2E File Offset: 0x00096E2E
		public bool hz32
		{
			get
			{
				return this.GetBeat(0.03125f);
			}
		}

		// Token: 0x06002AB5 RID: 10933 RVA: 0x00098A3C File Offset: 0x00096E3C
		public bool GetBeat(float frequency)
		{
			float num = Time.time + this.offset;
			float num2 = num - Time.deltaTime;
			num = Mathf.Floor(num / frequency);
			num2 = Mathf.Floor(num2 / frequency);
			return num2 != num;
		}

		// Token: 0x04001BC8 RID: 7112
		private float offset;
	}
}
