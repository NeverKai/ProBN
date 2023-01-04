using System;

namespace InspectorExpressions
{
	// Token: 0x0200042A RID: 1066
	public class OperationZeroBiasRoll : IValue
	{
		// Token: 0x06001877 RID: 6263 RVA: 0x0003F773 File Offset: 0x0003DB73
		public OperationZeroBiasRoll(IValue aValue, IValue aPower)
		{
			this.m_DiceCount = aValue;
			this.m_DiceFaces = aPower;
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06001878 RID: 6264 RVA: 0x0003F78C File Offset: 0x0003DB8C
		public double Value
		{
			get
			{
				double num = 0.0;
				double value = this.m_DiceCount.Value;
				int maxValue = (int)this.m_DiceFaces.Value;
				int num2 = 0;
				while ((double)num2 < value)
				{
					num += (double)OperationZeroBiasRoll.rnd.Next(0, maxValue);
					num2++;
				}
				return num;
			}
		}

		// Token: 0x06001879 RID: 6265 RVA: 0x0003F7E0 File Offset: 0x0003DBE0
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"( ",
				this.m_DiceCount,
				'#',
				this.m_DiceFaces,
				" )"
			});
		}

		// Token: 0x04000F2B RID: 3883
		private IValue m_DiceCount;

		// Token: 0x04000F2C RID: 3884
		private IValue m_DiceFaces;

		// Token: 0x04000F2D RID: 3885
		private static Random rnd = new Random();
	}
}
