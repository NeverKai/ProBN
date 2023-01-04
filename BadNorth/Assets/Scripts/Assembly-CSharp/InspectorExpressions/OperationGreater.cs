using System;

namespace InspectorExpressions
{
	// Token: 0x02000435 RID: 1077
	public class OperationGreater : IValue
	{
		// Token: 0x06001898 RID: 6296 RVA: 0x0003FC5E File Offset: 0x0003E05E
		public OperationGreater(IValue value0, IValue value1)
		{
			this.value0 = value0;
			this.value1 = value1;
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06001899 RID: 6297 RVA: 0x0003FC74 File Offset: 0x0003E074
		public double Value
		{
			get
			{
				return (double)((this.value0.Value <= this.value1.Value) ? 0 : 1);
			}
		}

		// Token: 0x0600189A RID: 6298 RVA: 0x0003FC9C File Offset: 0x0003E09C
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"( ",
				this.value0.Value,
				">",
				this.value1.Value,
				" )"
			});
		}

		// Token: 0x04000F3B RID: 3899
		private IValue value0;

		// Token: 0x04000F3C RID: 3900
		private IValue value1;
	}
}
