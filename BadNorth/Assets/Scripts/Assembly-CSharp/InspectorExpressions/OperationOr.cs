using System;
using System.Collections.Generic;

namespace InspectorExpressions
{
	// Token: 0x02000431 RID: 1073
	public class OperationOr : IValue
	{
		// Token: 0x0600188C RID: 6284 RVA: 0x0003FA39 File Offset: 0x0003DE39
		public OperationOr(params IValue[] aValues)
		{
			this.m_Values = aValues;
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x0600188D RID: 6285 RVA: 0x0003FA48 File Offset: 0x0003DE48
		public double Value
		{
			get
			{
				foreach (IValue value in this.m_Values)
				{
					if (value.Value == 1.0)
					{
						return 1.0;
					}
				}
				return 0.0;
			}
		}

		// Token: 0x0600188E RID: 6286 RVA: 0x0003FA9C File Offset: 0x0003DE9C
		public override string ToString()
		{
			List<string> list = new List<string>();
			foreach (IValue value in this.m_Values)
			{
				list.Add(value.ToString());
			}
			return "( " + string.Join(" || ", list.ToArray()) + " )";
		}

		// Token: 0x04000F35 RID: 3893
		private IValue[] m_Values;
	}
}
