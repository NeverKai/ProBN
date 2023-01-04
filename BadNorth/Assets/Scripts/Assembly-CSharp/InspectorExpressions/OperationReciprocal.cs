using System;

namespace InspectorExpressions
{
	// Token: 0x0200042D RID: 1069
	public class OperationReciprocal : IValue
	{
		// Token: 0x06001881 RID: 6273 RVA: 0x0003F8FF File Offset: 0x0003DCFF
		public OperationReciprocal(IValue aValue)
		{
			this.m_Value = aValue;
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06001882 RID: 6274 RVA: 0x0003F90E File Offset: 0x0003DD0E
		public double Value
		{
			get
			{
				return 1.0 / this.m_Value.Value;
			}
		}

		// Token: 0x06001883 RID: 6275 RVA: 0x0003F925 File Offset: 0x0003DD25
		public override string ToString()
		{
			return "( 1/" + this.m_Value + " )";
		}

		// Token: 0x04000F32 RID: 3890
		private IValue m_Value;
	}
}
