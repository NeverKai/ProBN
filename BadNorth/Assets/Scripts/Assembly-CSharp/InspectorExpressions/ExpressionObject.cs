using System;
using System.Collections.Generic;
using UnityEngine;

namespace InspectorExpressions
{
	// Token: 0x02000422 RID: 1058
	public abstract class ExpressionObject : ScriptableObject
	{
		// Token: 0x1700015A RID: 346
		// (get) Token: 0x0600185B RID: 6235 RVA: 0x0003F3EA File Offset: 0x0003D7EA
		public string Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x0600185C RID: 6236
		public abstract IList<string> ParameterNames { get; }

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600185D RID: 6237
		public abstract IList<string> ParameterWarnings { get; }

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x0600185E RID: 6238
		public abstract bool IsValid { get; }

		// Token: 0x0600185F RID: 6239
		public abstract void ParseExpression(out string error);

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06001860 RID: 6240 RVA: 0x0003F3F2 File Offset: 0x0003D7F2
		protected virtual Consts Constatns
		{
			get
			{
				return Utils.BaseConstants;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06001861 RID: 6241 RVA: 0x0003F3F9 File Offset: 0x0003D7F9
		protected virtual Funcs Functions
		{
			get
			{
				return Utils.BaseFunctions;
			}
		}

		// Token: 0x04000F20 RID: 3872
		[SerializeField]
		protected string expression;
	}
}
