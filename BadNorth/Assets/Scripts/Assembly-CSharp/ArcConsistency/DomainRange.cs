using System;
using System.Collections.Generic;
using UnityEngine;

namespace ArcConsistency
{
	// Token: 0x0200000B RID: 11
	public class DomainRange : Domain
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00003529 File Offset: 0x00001929
		private float range
		{
			get
			{
				return this.max - this.min;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00003538 File Offset: 0x00001938
		private int count
		{
			get
			{
				switch (this.mode)
				{
				case DomainRange.Mode.Spacing:
					return Mathf.CeilToInt(this.spacing / this.range);
				case DomainRange.Mode.Frequency:
					return Mathf.CeilToInt(this.range / this.spacing);
				case DomainRange.Mode.Count:
					return Mathf.CeilToInt(this.parameter);
				default:
					return 1;
				}
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00003598 File Offset: 0x00001998
		private float spacing
		{
			get
			{
				switch (this.mode)
				{
				case DomainRange.Mode.Spacing:
					return this.parameter;
				case DomainRange.Mode.Frequency:
					return 1f / this.parameter;
				case DomainRange.Mode.Count:
					return this.range / (float)this.count;
				default:
					return 1f;
				}
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000035EC File Offset: 0x000019EC
		protected override List<float> GenerateValues()
		{
			switch (this.mode)
			{
			case DomainRange.Mode.Spacing:
				return this.CreateDomain_Spacing(this.parameter);
			case DomainRange.Mode.Frequency:
				return this.CreateDomain_Spacing(1f / this.parameter);
			case DomainRange.Mode.Count:
				return this.CreateDomain_Count((int)this.parameter);
			default:
				return null;
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00003648 File Offset: 0x00001A48
		private List<float> CreateDomain_Spacing(float spacing)
		{
			List<float> list = new List<float>(Mathf.CeilToInt(this.range / spacing));
			for (float num = this.min; num <= this.max; num += spacing)
			{
				list.Add(num);
			}
			return list;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000368C File Offset: 0x00001A8C
		private List<float> CreateDomain_Count(int count)
		{
			List<float> list = new List<float>(count);
			for (int i = 0; i <= count; i++)
			{
				list.Add(Mathf.Lerp(this.min, this.max, (float)i / (float)count));
			}
			return list;
		}

		// Token: 0x0400001A RID: 26
		[Space]
		[SerializeField]
		public float min;

		// Token: 0x0400001B RID: 27
		public float max = 10f;

		// Token: 0x0400001C RID: 28
		[SerializeField]
		private DomainRange.Mode mode;

		// Token: 0x0400001D RID: 29
		[SerializeField]
		private float parameter = 1f;

		// Token: 0x0200000C RID: 12
		private enum Mode
		{
			// Token: 0x0400001F RID: 31
			Spacing,
			// Token: 0x04000020 RID: 32
			Frequency,
			// Token: 0x04000021 RID: 33
			Count
		}
	}
}
