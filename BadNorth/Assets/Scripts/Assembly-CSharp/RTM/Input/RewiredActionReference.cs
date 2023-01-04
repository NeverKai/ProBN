using System;
using System.Diagnostics;
using Rewired;
using RTM.Utilities;

namespace RTM.Input
{
	// Token: 0x020004BD RID: 1213
	[DebuggerDisplay("{_name} ({id})")]
	[Serializable]
	public class RewiredActionReference : LookupReference
	{
		// Token: 0x06001EAF RID: 7855 RVA: 0x00052511 File Offset: 0x00050911
		private RewiredActionReference(string name) : base(name)
		{
		}

		// Token: 0x06001EB0 RID: 7856 RVA: 0x0005251A File Offset: 0x0005091A
		protected override int GetId()
		{
			return ReInput.mapping.GetActionId(base.name);
		}

		// Token: 0x06001EB1 RID: 7857 RVA: 0x0005252C File Offset: 0x0005092C
		public static implicit operator RewiredActionReference(string name)
		{
			return new RewiredActionReference(name);
		}
	}
}
