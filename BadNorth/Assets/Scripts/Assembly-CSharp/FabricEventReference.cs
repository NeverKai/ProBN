using System;
using System.Diagnostics;
using Fabric;
using RTM.Utilities;

// Token: 0x02000395 RID: 917
[DebuggerDisplay("{_name} ({id})")]
[ObjectDumper.LeafAttribute]
[Serializable]
public class FabricEventReference : LookupReference
{
	// Token: 0x060014ED RID: 5357 RVA: 0x0002B77D File Offset: 0x00029B7D
	private FabricEventReference(string name) : base(name)
	{
	}

	// Token: 0x060014EE RID: 5358 RVA: 0x0002B786 File Offset: 0x00029B86
	protected override int GetId()
	{
		return EventManager.GetIDFromEventName(base.name);
	}

	// Token: 0x060014EF RID: 5359 RVA: 0x0002B793 File Offset: 0x00029B93
	public bool ForceInit()
	{
		return base.id != 0;
	}

	// Token: 0x060014F0 RID: 5360 RVA: 0x0002B7A1 File Offset: 0x00029BA1
	public static implicit operator FabricEventReference(string name)
	{
		return new FabricEventReference(name);
	}

	// Token: 0x04000D01 RID: 3329
	public static FabricEventReference none = string.Empty;
}
