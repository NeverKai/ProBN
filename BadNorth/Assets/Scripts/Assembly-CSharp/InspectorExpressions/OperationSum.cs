using System;
using System.Collections.Generic;

namespace InspectorExpressions
{
	// Token: 0x02000425 RID: 1061
	public class OperationSum : IValue
	{
		// Token: 0x06001867 RID: 6247 RVA: 0x0003F43C File Offset: 0x0003D83C
		public OperationSum(params IValue[] aValues)
		{
			List<IValue> list = new List<IValue>(aValues.Length);
			foreach (IValue value in aValues)
			{
				OperationSum operationSum = value as OperationSum;
				if (operationSum == null)
				{
					list.Add(value);
				}
				else
				{
					list.AddRange(operationSum.m_Values);
				}
			}
			this.m_Values = list.ToArray();
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06001868 RID: 6248 RVA: 0x0003F4A8 File Offset: 0x0003D8A8
		public double Value
		{
			get
			{
				double num = 0.0;
				foreach (IValue value in this.m_Values)
				{
					num += value.Value;
				}
				return num;
			}
		}

		// Token: 0x06001869 RID: 6249 RVA: 0x0003F4E8 File Offset: 0x0003D8E8
		public override string ToString()
		{
			List<string> list = new List<string>();
			foreach (IValue value in this.m_Values)
			{
				list.Add(value.ToString());
			}
			return "( " + string.Join(" + ", list.ToArray()) + " )";
		}

		// Token: 0x04000F22 RID: 3874
		private IValue[] m_Values;
	}
}
