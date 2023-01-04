using System;

namespace InspectorExpressions
{
	// Token: 0x02000436 RID: 1078
	public class OperationGreaterOrEqual : IValue
	{
		// Token: 0x0600189B RID: 6299 RVA: 0x0003FCF2 File Offset: 0x0003E0F2
		public OperationGreaterOrEqual(IValue value0, IValue value1)
		{
			this.value0 = value0;
			this.value1 = value1;
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x0600189C RID: 6300 RVA: 0x0003FD08 File Offset: 0x0003E108
		public double Value
		{
			get
			{
				return (double)((this.value0.Value < this.value1.Value) ? 0 : 1);
			}
		}

		// Token: 0x0600189D RID: 6301 RVA: 0x0003FD30 File Offset: 0x0003E130
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"( ",
				this.value0.Value,
				">=",
				this.value1.Value,
				" )"
			});
		}

		// Token: 0x04000F3D RID: 3901
		private IValue value0;

		// Token: 0x04000F3E RID: 3902
		private IValue value1;
	}
}
