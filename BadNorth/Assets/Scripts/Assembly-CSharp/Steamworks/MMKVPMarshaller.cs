using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000334 RID: 820
	public class MMKVPMarshaller
	{
		// Token: 0x06001232 RID: 4658 RVA: 0x000273D4 File Offset: 0x000257D4
		public MMKVPMarshaller(MatchMakingKeyValuePair_t[] filters)
		{
			if (filters == null)
			{
				return;
			}
			int num = Marshal.SizeOf(typeof(MatchMakingKeyValuePair_t));
			this.m_pNativeArray = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(IntPtr)) * filters.Length);
			this.m_pArrayEntries = Marshal.AllocHGlobal(num * filters.Length);
			for (int i = 0; i < filters.Length; i++)
			{
				Marshal.StructureToPtr(filters[i], new IntPtr(this.m_pArrayEntries.ToInt64() + (long)(i * num)), false);
			}
			Marshal.WriteIntPtr(this.m_pNativeArray, this.m_pArrayEntries);
		}

		// Token: 0x06001233 RID: 4659 RVA: 0x00027480 File Offset: 0x00025880
		~MMKVPMarshaller()
		{
			if (this.m_pArrayEntries != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.m_pArrayEntries);
			}
			if (this.m_pNativeArray != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.m_pNativeArray);
			}
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x000274EC File Offset: 0x000258EC
		public static implicit operator IntPtr(MMKVPMarshaller that)
		{
			return that.m_pNativeArray;
		}

		// Token: 0x04000C46 RID: 3142
		private IntPtr m_pNativeArray;

		// Token: 0x04000C47 RID: 3143
		private IntPtr m_pArrayEntries;
	}
}
