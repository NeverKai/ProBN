using System;
using System.Diagnostics;
using RTM.Utilities;
using UnityEngine;

// Token: 0x020005D6 RID: 1494
[DebuggerDisplay("{_name} ({id})")]
[Serializable]
public class AnimId : LookupReference
{
	// Token: 0x060026DC RID: 9948 RVA: 0x0007C574 File Offset: 0x0007A974
	private AnimId(string name) : base(name)
	{
	}

	// Token: 0x060026DD RID: 9949 RVA: 0x0007C57D File Offset: 0x0007A97D
	protected override int GetId()
	{
		return Animator.StringToHash(base.name);
	}

	// Token: 0x060026DE RID: 9950 RVA: 0x0007C58A File Offset: 0x0007A98A
	public static implicit operator AnimId(string name)
	{
		return new AnimId(name);
	}
}
