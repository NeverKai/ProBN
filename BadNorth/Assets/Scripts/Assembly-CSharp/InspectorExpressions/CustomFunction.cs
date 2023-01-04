using System;
using System.Collections.Generic;

namespace InspectorExpressions
{
	// Token: 0x02000438 RID: 1080
	public class CustomFunction : IValue
	{
		// Token: 0x060018A2 RID: 6306 RVA: 0x0003FE12 File Offset: 0x0003E212
		public CustomFunction(string aName, Func<double[], double> aDelegate, params IValue[] aValues)
		{
			this.m_Delegate = aDelegate;
			this.m_Params = aValues;
			this.m_Name = aName;
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060018A3 RID: 6307 RVA: 0x0003FE30 File Offset: 0x0003E230
		public double Value
		{
			get
			{
				if (this.m_Params == null)
				{
					return this.m_Delegate(null);
				}
				List<double> list = new List<double>();
				foreach (IValue value in this.m_Params)
				{
					list.Add(value.Value);
				}
				return this.m_Delegate(list.ToArray());
			}
		}

		// Token: 0x060018A4 RID: 6308 RVA: 0x0003FE98 File Offset: 0x0003E298
		public override string ToString()
		{
			if (this.m_Params == null)
			{
				return this.m_Name;
			}
			List<string> list = new List<string>();
			foreach (IValue value in this.m_Params)
			{
				list.Add(value.ToString());
			}
			return this.m_Name + "( " + string.Join(", ", list.ToArray()) + " )";
		}

		// Token: 0x04000F40 RID: 3904
		private IValue[] m_Params;

		// Token: 0x04000F41 RID: 3905
		private Func<double[], double> m_Delegate;

		// Token: 0x04000F42 RID: 3906
		private string m_Name;
	}
}
