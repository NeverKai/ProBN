using System;

namespace InspectorExpressions
{
	// Token: 0x02000428 RID: 1064
	public class OperationPower : IValue
	{
		// Token: 0x06001870 RID: 6256 RVA: 0x0003F657 File Offset: 0x0003DA57
		public OperationPower(IValue aValue, IValue aPower)
		{
			this.m_Value = aValue;
			this.m_Power = aPower;
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06001871 RID: 6257 RVA: 0x0003F66D File Offset: 0x0003DA6D
		public double Value
		{
			get
			{
				return Math.Pow(this.m_Value.Value, this.m_Power.Value);
			}
		}

		// Token: 0x06001872 RID: 6258 RVA: 0x0003F68A File Offset: 0x0003DA8A
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"( ",
				this.m_Value,
				"^",
				this.m_Power,
				" )"
			});
		}

		// Token: 0x04000F26 RID: 3878
		private IValue m_Value;

		// Token: 0x04000F27 RID: 3879
		private IValue m_Power;
	}
}
