using System;

namespace InspectorExpressions
{
	// Token: 0x02000433 RID: 1075
	public class OperationEqual : IValue
	{
		// Token: 0x06001892 RID: 6290 RVA: 0x0003FB36 File Offset: 0x0003DF36
		public OperationEqual(IValue value0, IValue value1)
		{
			this.value0 = value0;
			this.value1 = value1;
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06001893 RID: 6291 RVA: 0x0003FB4C File Offset: 0x0003DF4C
		public double Value
		{
			get
			{
				return (double)((this.value0.Value != this.value1.Value) ? 0 : 1);
			}
		}

		// Token: 0x06001894 RID: 6292 RVA: 0x0003FB74 File Offset: 0x0003DF74
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"( ",
				this.value0.Value,
				"==",
				this.value1.Value,
				" )"
			});
		}

		// Token: 0x04000F37 RID: 3895
		private IValue value0;

		// Token: 0x04000F38 RID: 3896
		private IValue value1;
	}
}
