using System;

namespace InspectorExpressions
{
	// Token: 0x02000429 RID: 1065
	public class OperationDiceRoll : IValue
	{
		// Token: 0x06001873 RID: 6259 RVA: 0x0003F6C1 File Offset: 0x0003DAC1
		public OperationDiceRoll(IValue aValue, IValue aPower)
		{
			this.m_DiceCount = aValue;
			this.m_DiceFaces = aPower;
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06001874 RID: 6260 RVA: 0x0003F6D8 File Offset: 0x0003DAD8
		public double Value
		{
			get
			{
				double num = 0.0;
				double value = this.m_DiceCount.Value;
				int num2 = (int)this.m_DiceFaces.Value;
				int num3 = 0;
				while ((double)num3 < value)
				{
					num += (double)OperationDiceRoll.rnd.Next(1, num2 + 1);
					num3++;
				}
				return num;
			}
		}

		// Token: 0x06001875 RID: 6261 RVA: 0x0003F72E File Offset: 0x0003DB2E
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"( ",
				this.m_DiceCount,
				'@',
				this.m_DiceFaces,
				" )"
			});
		}

		// Token: 0x04000F28 RID: 3880
		private IValue m_DiceCount;

		// Token: 0x04000F29 RID: 3881
		private IValue m_DiceFaces;

		// Token: 0x04000F2A RID: 3882
		private static Random rnd = new Random();
	}
}
