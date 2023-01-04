using System;

namespace InspectorExpressions
{
	// Token: 0x0200042C RID: 1068
	public class OperationIf : IValue
	{
		// Token: 0x0600187E RID: 6270 RVA: 0x0003F859 File Offset: 0x0003DC59
		public OperationIf(IValue condition, IValue ifTrue, IValue ifFalse)
		{
			this.condition = condition;
			this.ifTrue = ifTrue;
			this.ifFalse = ifFalse;
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x0600187F RID: 6271 RVA: 0x0003F876 File Offset: 0x0003DC76
		public double Value
		{
			get
			{
				return (this.condition.Value != 1.0) ? this.ifFalse.Value : this.ifTrue.Value;
			}
		}

		// Token: 0x06001880 RID: 6272 RVA: 0x0003F8AC File Offset: 0x0003DCAC
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"( ",
				this.condition,
				" ? ",
				this.ifTrue,
				" : ",
				this.ifFalse,
				" )"
			});
		}

		// Token: 0x04000F2F RID: 3887
		private IValue condition;

		// Token: 0x04000F30 RID: 3888
		private IValue ifTrue;

		// Token: 0x04000F31 RID: 3889
		private IValue ifFalse;
	}
}
