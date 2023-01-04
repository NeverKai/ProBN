using System;

namespace InspectorExpressions
{
	// Token: 0x02000434 RID: 1076
	public class OperationNotEqual : IValue
	{
		// Token: 0x06001895 RID: 6293 RVA: 0x0003FBCA File Offset: 0x0003DFCA
		public OperationNotEqual(IValue value0, IValue value1)
		{
			this.value0 = value0;
			this.value1 = value1;
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06001896 RID: 6294 RVA: 0x0003FBE0 File Offset: 0x0003DFE0
		public double Value
		{
			get
			{
				return (double)((this.value0.Value == this.value1.Value) ? 0 : 1);
			}
		}

		// Token: 0x06001897 RID: 6295 RVA: 0x0003FC08 File Offset: 0x0003E008
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"( ",
				this.value0.Value,
				"!=",
				this.value1.Value,
				" )"
			});
		}

		// Token: 0x04000F39 RID: 3897
		private IValue value0;

		// Token: 0x04000F3A RID: 3898
		private IValue value1;
	}
}
