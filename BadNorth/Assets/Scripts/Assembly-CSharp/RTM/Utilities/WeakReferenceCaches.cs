using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace RTM.Utilities
{
	// Token: 0x020004ED RID: 1261
	public static class WeakReferenceCaches
	{
		// Token: 0x06002048 RID: 8264 RVA: 0x00056EA5 File Offset: 0x000552A5
		[Conditional("MEMORY_DIAGNOSTICS")]
		public static void Add<T>(T obj) where T : class
		{
			throw new NotImplementedException("Unexpected Memory Diagnostics");
		}

		// Token: 0x06002049 RID: 8265 RVA: 0x00056EB1 File Offset: 0x000552B1
		[Conditional("MEMORY_DIAGNOSTICS")]
		public static void AddCollection<T>(IEnumerable<T> collection) where T : class
		{
			throw new NotImplementedException("Unexpected Memory Diagnostics");
		}

		// Token: 0x0600204A RID: 8266 RVA: 0x00056EBD File Offset: 0x000552BD
		[Conditional("MEMORY_DIAGNOSTICS")]
		public static void PrintAll(string prefix = "", bool fullList = false)
		{
			throw new NotImplementedException("Unexpected Memory Diagnostics");
		}

		// Token: 0x0600204B RID: 8267 RVA: 0x00056EC9 File Offset: 0x000552C9
		[Conditional("MEMORY_DIAGNOSTICS")]
		public static void Print(string prefix = "", bool fullList = false, params Type[] types)
		{
			throw new NotImplementedException("Unexpected Memory Diagnostics");
		}

		// Token: 0x0600204C RID: 8268 RVA: 0x00056ED5 File Offset: 0x000552D5
		[Conditional("MEMORY_DIAGNOSTICS")]
		public static void Print<T>(bool fullList = true)
		{
			throw new NotImplementedException("Unexpected Memory Diagnostics");
		}

		// Token: 0x0600204D RID: 8269 RVA: 0x00056EE1 File Offset: 0x000552E1
		[Conditional("MEMORY_DIAGNOSTICS")]
		public static void Print(Type type, bool fullList = true)
		{
			throw new NotImplementedException("Unexpected Memory Diagnostics");
		}

		// Token: 0x0600204E RID: 8270 RVA: 0x00056EED File Offset: 0x000552ED
		[Conditional("MEMORY_DIAGNOSTICS")]
		public static void Clear(bool deadOnly = false)
		{
			throw new NotImplementedException("Unexpected Memory Diagnostics");
		}
	}
}
