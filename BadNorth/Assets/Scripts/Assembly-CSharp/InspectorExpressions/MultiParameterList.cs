using System;
using System.Collections.Generic;

namespace InspectorExpressions
{
	// Token: 0x02000437 RID: 1079
	public class MultiParameterList : IValue
	{
		// Token: 0x0600189E RID: 6302 RVA: 0x0003FD86 File Offset: 0x0003E186
		public MultiParameterList(params IValue[] aValues)
		{
			this.m_Values = aValues;
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x0600189F RID: 6303 RVA: 0x0003FD95 File Offset: 0x0003E195
		public IValue[] Parameters
		{
			get
			{
				return this.m_Values;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060018A0 RID: 6304 RVA: 0x0003FD9D File Offset: 0x0003E19D
		public double Value
		{
			get
			{
				if (this.m_Values.Length == 0)
				{
					return 0.0;
				}
				return this.m_Values[0].Value;
			}
		}

		// Token: 0x060018A1 RID: 6305 RVA: 0x0003FDC4 File Offset: 0x0003E1C4
		public override string ToString()
		{
			List<string> list = new List<string>();
			foreach (IValue value in this.m_Values)
			{
				list.Add(value.ToString());
			}
			return string.Join(", ", list.ToArray());
		}

		// Token: 0x04000F3F RID: 3903
		private IValue[] m_Values;
	}
}
