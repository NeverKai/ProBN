using System;
using System.Collections.Generic;

namespace InspectorExpressions
{
	// Token: 0x02000426 RID: 1062
	public class OperationProduct : IValue
	{
		// Token: 0x0600186A RID: 6250 RVA: 0x0003F545 File Offset: 0x0003D945
		public OperationProduct(params IValue[] aValues)
		{
			this.m_Values = aValues;
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x0600186B RID: 6251 RVA: 0x0003F554 File Offset: 0x0003D954
		public double Value
		{
			get
			{
				double num = 1.0;
				foreach (IValue value in this.m_Values)
				{
					num *= value.Value;
				}
				return num;
			}
		}

		// Token: 0x0600186C RID: 6252 RVA: 0x0003F594 File Offset: 0x0003D994
		public override string ToString()
		{
			List<string> list = new List<string>();
			foreach (IValue value in this.m_Values)
			{
				list.Add(value.ToString());
			}
			return "( " + string.Join(" * ", list.ToArray()) + " )";
		}

		// Token: 0x04000F23 RID: 3875
		private IValue[] m_Values;
	}
}
