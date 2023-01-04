using System;
using System.Collections.Generic;
using UnityEngine;

namespace InspectorExpressions
{
	// Token: 0x02000442 RID: 1090
	public abstract class ExpressionSerialized
	{
		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060018D3 RID: 6355 RVA: 0x00003813 File Offset: 0x00001C13
		public string Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060018D4 RID: 6356
		public abstract IList<string> ParameterNames { get; }

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060018D5 RID: 6357
		public abstract IList<string> ParameterWarnings { get; }

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060018D6 RID: 6358
		public abstract bool IsValid { get; }

		// Token: 0x060018D7 RID: 6359
		public abstract void ParseExpression(out string error);

		// Token: 0x060018D8 RID: 6360
		public abstract string TryParseExpression();

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060018D9 RID: 6361 RVA: 0x0000381B File Offset: 0x00001C1B
		protected virtual Consts Constants
		{
			get
			{
				return Utils.BaseConstants;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060018DA RID: 6362 RVA: 0x00003822 File Offset: 0x00001C22
		protected virtual Funcs Functions
		{
			get
			{
				return Utils.BaseFunctions;
			}
		}

		// Token: 0x04000F4C RID: 3916
		[SerializeField]
		protected string expression;
	}
}
