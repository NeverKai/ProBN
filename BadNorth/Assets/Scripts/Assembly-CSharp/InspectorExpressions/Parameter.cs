using System;
using UnityEngine;

namespace InspectorExpressions
{
	// Token: 0x02000439 RID: 1081
	public class Parameter : Number, IBool
	{
		// Token: 0x060018A5 RID: 6309 RVA: 0x0003FF0D File Offset: 0x0003E30D
		public Parameter(string aName) : base(0.0)
		{
			this.Name = aName;
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060018A6 RID: 6310 RVA: 0x0003FF25 File Offset: 0x0003E325
		// (set) Token: 0x060018A7 RID: 6311 RVA: 0x0003FF2D File Offset: 0x0003E32D
		public string Name { get; private set; }

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060018A8 RID: 6312 RVA: 0x0003FF38 File Offset: 0x0003E338
		bool IBool.BoolValue
		{
			get
			{
				if (base.Value == 1.0)
				{
					return true;
				}
				if (base.Value == 0.0)
				{
					return false;
				}
				Debug.LogWarning(string.Concat(new object[]
				{
					"Parameter ",
					this.Name,
					" has value ",
					base.Value,
					" but is being parsed as a bool"
				}));
				return base.Value > 0.5;
			}
		}

		// Token: 0x060018A9 RID: 6313 RVA: 0x0003FFC1 File Offset: 0x0003E3C1
		public override string ToString()
		{
			return this.Name + "[" + base.ToString() + "]";
		}
	}
}
