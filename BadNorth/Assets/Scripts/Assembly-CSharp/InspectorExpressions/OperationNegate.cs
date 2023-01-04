using System;

namespace InspectorExpressions
{
	// Token: 0x0200042B RID: 1067
	public class OperationNegate : IValue
	{
		// Token: 0x0600187B RID: 6267 RVA: 0x0003F825 File Offset: 0x0003DC25
		public OperationNegate(IValue aValue)
		{
			this.m_Value = aValue;
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600187C RID: 6268 RVA: 0x0003F834 File Offset: 0x0003DC34
		public double Value
		{
			get
			{
				return -this.m_Value.Value;
			}
		}

		// Token: 0x0600187D RID: 6269 RVA: 0x0003F842 File Offset: 0x0003DC42
		public override string ToString()
		{
			return "( -" + this.m_Value + " )";
		}

		// Token: 0x04000F2E RID: 3886
		private IValue m_Value;
	}
}
