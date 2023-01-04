using System;
using System.Diagnostics;

namespace RTM.Utilities
{
	// Token: 0x020004EC RID: 1260
	internal class WeakReferenceCache
	{
		// Token: 0x06002045 RID: 8261 RVA: 0x00056E85 File Offset: 0x00055285
		internal WeakReferenceCache(string name, bool fullList)
		{
		}

		// Token: 0x06002046 RID: 8262 RVA: 0x00056E8D File Offset: 0x0005528D
		[Conditional("MEMORY_DIAGNOSTICS")]
		internal void Add(object obj)
		{
			throw new NotImplementedException("Unexpected Memory Diagnostics");
		}

		// Token: 0x06002047 RID: 8263 RVA: 0x00056E99 File Offset: 0x00055299
		[Conditional("MEMORY_DIAGNOSTICS")]
		internal void Print()
		{
			throw new NotImplementedException("Unexpected Memory Diagnostics");
		}
	}
}
