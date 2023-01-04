using System;

namespace InspectorExpressions
{
	// Token: 0x02000432 RID: 1074
	public class OperationNot : IValue
	{
		// Token: 0x0600188F RID: 6287 RVA: 0x0003FAF9 File Offset: 0x0003DEF9
		public OperationNot(IValue aValue)
		{
			this.m_Value = aValue;
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06001890 RID: 6288 RVA: 0x0003FB08 File Offset: 0x0003DF08
		public double Value
		{
			get
			{
				return 1.0 - this.m_Value.Value;
			}
		}

		// Token: 0x06001891 RID: 6289 RVA: 0x0003FB1F File Offset: 0x0003DF1F
		public override string ToString()
		{
			return "( !" + this.m_Value + " )";
		}

		// Token: 0x04000F36 RID: 3894
		private IValue m_Value;
	}
}
