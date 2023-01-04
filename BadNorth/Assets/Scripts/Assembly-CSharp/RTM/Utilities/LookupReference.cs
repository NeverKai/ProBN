using System;
using System.Diagnostics;
using UnityEngine;

namespace RTM.Utilities
{
	// Token: 0x020004E9 RID: 1257
	[DebuggerDisplay("{_name} ({id})")]
	[Serializable]
	public abstract class LookupReference
	{
		// Token: 0x06002036 RID: 8246 RVA: 0x0002B713 File Offset: 0x00029B13
		private LookupReference()
		{
		}

		// Token: 0x06002037 RID: 8247 RVA: 0x0002B71B File Offset: 0x00029B1B
		protected LookupReference(string name)
		{
			this._name = name;
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06002038 RID: 8248 RVA: 0x0002B72A File Offset: 0x00029B2A
		public string name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06002039 RID: 8249 RVA: 0x0002B732 File Offset: 0x00029B32
		public int id
		{
			get
			{
				if (!this.valid)
				{
					this._id = this.GetId();
					this.valid = true;
				}
				return this._id;
			}
		}

		// Token: 0x0600203A RID: 8250
		protected abstract int GetId();

		// Token: 0x0600203B RID: 8251 RVA: 0x0002B758 File Offset: 0x00029B58
		public static implicit operator int(LookupReference reference)
		{
			return reference.id;
		}

		// Token: 0x0600203C RID: 8252 RVA: 0x0002B760 File Offset: 0x00029B60
		public override string ToString()
		{
			return string.Format("{0} ({1})", this._name, this.id);
		}

		// Token: 0x04001408 RID: 5128
		[SerializeField]
		private string _name;

		// Token: 0x04001409 RID: 5129
		private int _id;

		// Token: 0x0400140A RID: 5130
		private bool valid;
	}
}
