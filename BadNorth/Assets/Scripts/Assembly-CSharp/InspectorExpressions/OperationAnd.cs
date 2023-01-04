using System;
using System.Collections.Generic;

namespace InspectorExpressions
{
	// Token: 0x02000430 RID: 1072
	public class OperationAnd : IValue
	{
		// Token: 0x06001889 RID: 6281 RVA: 0x0003F978 File Offset: 0x0003DD78
		public OperationAnd(params IValue[] aValues)
		{
			this.m_Values = aValues;
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x0600188A RID: 6282 RVA: 0x0003F988 File Offset: 0x0003DD88
		public double Value
		{
			get
			{
				foreach (IValue value in this.m_Values)
				{
					if (value.Value == 0.0)
					{
						return 0.0;
					}
				}
				return 1.0;
			}
		}

		// Token: 0x0600188B RID: 6283 RVA: 0x0003F9DC File Offset: 0x0003DDDC
		public override string ToString()
		{
			List<string> list = new List<string>();
			foreach (IValue value in this.m_Values)
			{
				list.Add(value.ToString());
			}
			return "( " + string.Join(" && ", list.ToArray()) + " )";
		}

		// Token: 0x04000F34 RID: 3892
		private IValue[] m_Values;
	}
}
