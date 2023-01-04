using System;

namespace InspectorExpressions
{
	// Token: 0x02000427 RID: 1063
	public class OperationModulo : IValue
	{
		// Token: 0x0600186D RID: 6253 RVA: 0x0003F5F1 File Offset: 0x0003D9F1
		public OperationModulo(IValue value0, IValue value1)
		{
			this.value0 = value0;
			this.value1 = value1;
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x0600186E RID: 6254 RVA: 0x0003F607 File Offset: 0x0003DA07
		public double Value
		{
			get
			{
				return this.value0.Value % this.value1.Value;
			}
		}

		// Token: 0x0600186F RID: 6255 RVA: 0x0003F620 File Offset: 0x0003DA20
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"( ",
				this.value0,
				" % ",
				this.value1,
				" )"
			});
		}

		// Token: 0x04000F24 RID: 3876
		private IValue value0;

		// Token: 0x04000F25 RID: 3877
		private IValue value1;
	}
}
