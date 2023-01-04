using System;

namespace InspectorExpressions
{
	// Token: 0x02000424 RID: 1060
	public class Number : IValue
	{
		// Token: 0x06001863 RID: 6243 RVA: 0x0003F400 File Offset: 0x0003D800
		public Number(double aValue)
		{
			this.m_Value = aValue;
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06001864 RID: 6244 RVA: 0x0003F40F File Offset: 0x0003D80F
		// (set) Token: 0x06001865 RID: 6245 RVA: 0x0003F417 File Offset: 0x0003D817
		public double Value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.m_Value = value;
			}
		}

		// Token: 0x06001866 RID: 6246 RVA: 0x0003F420 File Offset: 0x0003D820
		public override string ToString()
		{
			return string.Empty + this.m_Value + string.Empty;
		}

		// Token: 0x04000F21 RID: 3873
		private double m_Value;
	}
}
