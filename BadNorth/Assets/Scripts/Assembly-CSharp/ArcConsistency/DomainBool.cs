using System;
using System.Collections.Generic;

namespace ArcConsistency
{
	// Token: 0x0200000A RID: 10
	public class DomainBool : Domain
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000021 RID: 33 RVA: 0x0000345D File Offset: 0x0000185D
		public bool definetlyTrue
		{
			get
			{
				return this.values.Count == 1 && this.values[0] == 1f;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00003486 File Offset: 0x00001886
		public bool definetlyFalse
		{
			get
			{
				return this.values.Count == 1 && this.values[0] == 0f;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000034AF File Offset: 0x000018AF
		public bool couldBeTrue
		{
			get
			{
				return this.values.Contains(1f);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000034C1 File Offset: 0x000018C1
		public bool couldBeFalse
		{
			get
			{
				return this.values.Contains(0f);
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000034D3 File Offset: 0x000018D3
		protected override List<float> GenerateValues()
		{
			return DomainBool._list;
		}

		// Token: 0x04000019 RID: 25
		private static List<float> _list = new List<float>
		{
			0f,
			1f
		};
	}
}
