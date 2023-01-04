using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200034C RID: 844
	public class ISteamMatchmakingRulesResponse
	{
		// Token: 0x0600128B RID: 4747 RVA: 0x0002791C File Offset: 0x00025D1C
		public ISteamMatchmakingRulesResponse(ISteamMatchmakingRulesResponse.RulesResponded onRulesResponded, ISteamMatchmakingRulesResponse.RulesFailedToRespond onRulesFailedToRespond, ISteamMatchmakingRulesResponse.RulesRefreshComplete onRulesRefreshComplete)
		{
			if (onRulesResponded == null || onRulesFailedToRespond == null || onRulesRefreshComplete == null)
			{
				throw new ArgumentNullException();
			}
			this.m_RulesResponded = onRulesResponded;
			this.m_RulesFailedToRespond = onRulesFailedToRespond;
			this.m_RulesRefreshComplete = onRulesRefreshComplete;
			this.m_VTable = new ISteamMatchmakingRulesResponse.VTable
			{
				m_VTRulesResponded = new ISteamMatchmakingRulesResponse.InternalRulesResponded(this.InternalOnRulesResponded),
				m_VTRulesFailedToRespond = new ISteamMatchmakingRulesResponse.InternalRulesFailedToRespond(this.InternalOnRulesFailedToRespond),
				m_VTRulesRefreshComplete = new ISteamMatchmakingRulesResponse.InternalRulesRefreshComplete(this.InternalOnRulesRefreshComplete)
			};
			this.m_pVTable = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ISteamMatchmakingRulesResponse.VTable)));
			Marshal.StructureToPtr(this.m_VTable, this.m_pVTable, false);
			this.m_pGCHandle = GCHandle.Alloc(this.m_pVTable, GCHandleType.Pinned);
		}

		// Token: 0x0600128C RID: 4748 RVA: 0x000279E4 File Offset: 0x00025DE4
		~ISteamMatchmakingRulesResponse()
		{
			if (this.m_pVTable != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.m_pVTable);
			}
			if (this.m_pGCHandle.IsAllocated)
			{
				this.m_pGCHandle.Free();
			}
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x00027A48 File Offset: 0x00025E48
		private void InternalOnRulesResponded(IntPtr pchRule, IntPtr pchValue)
		{
			this.m_RulesResponded(InteropHelp.PtrToStringUTF8(pchRule), InteropHelp.PtrToStringUTF8(pchValue));
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x00027A61 File Offset: 0x00025E61
		private void InternalOnRulesFailedToRespond()
		{
			this.m_RulesFailedToRespond();
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x00027A6E File Offset: 0x00025E6E
		private void InternalOnRulesRefreshComplete()
		{
			this.m_RulesRefreshComplete();
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x00027A7B File Offset: 0x00025E7B
		public static explicit operator IntPtr(ISteamMatchmakingRulesResponse that)
		{
			return that.m_pGCHandle.AddrOfPinnedObject();
		}

		// Token: 0x04000C61 RID: 3169
		private ISteamMatchmakingRulesResponse.VTable m_VTable;

		// Token: 0x04000C62 RID: 3170
		private IntPtr m_pVTable;

		// Token: 0x04000C63 RID: 3171
		private GCHandle m_pGCHandle;

		// Token: 0x04000C64 RID: 3172
		private ISteamMatchmakingRulesResponse.RulesResponded m_RulesResponded;

		// Token: 0x04000C65 RID: 3173
		private ISteamMatchmakingRulesResponse.RulesFailedToRespond m_RulesFailedToRespond;

		// Token: 0x04000C66 RID: 3174
		private ISteamMatchmakingRulesResponse.RulesRefreshComplete m_RulesRefreshComplete;

		// Token: 0x0200034D RID: 845
		// (Invoke) Token: 0x06001292 RID: 4754
		public delegate void RulesResponded(string pchRule, string pchValue);

		// Token: 0x0200034E RID: 846
		// (Invoke) Token: 0x06001296 RID: 4758
		public delegate void RulesFailedToRespond();

		// Token: 0x0200034F RID: 847
		// (Invoke) Token: 0x0600129A RID: 4762
		public delegate void RulesRefreshComplete();

		// Token: 0x02000350 RID: 848
		// (Invoke) Token: 0x0600129E RID: 4766
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void InternalRulesResponded(IntPtr pchRule, IntPtr pchValue);

		// Token: 0x02000351 RID: 849
		// (Invoke) Token: 0x060012A2 RID: 4770
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void InternalRulesFailedToRespond();

		// Token: 0x02000352 RID: 850
		// (Invoke) Token: 0x060012A6 RID: 4774
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void InternalRulesRefreshComplete();

		// Token: 0x02000353 RID: 851
		[StructLayout(LayoutKind.Sequential)]
		private class VTable
		{
			// Token: 0x04000C67 RID: 3175
			[NonSerialized]
			[MarshalAs(UnmanagedType.FunctionPtr)]
			public ISteamMatchmakingRulesResponse.InternalRulesResponded m_VTRulesResponded;

			// Token: 0x04000C68 RID: 3176
			[NonSerialized]
			[MarshalAs(UnmanagedType.FunctionPtr)]
			public ISteamMatchmakingRulesResponse.InternalRulesFailedToRespond m_VTRulesFailedToRespond;

			// Token: 0x04000C69 RID: 3177
			[NonSerialized]
			[MarshalAs(UnmanagedType.FunctionPtr)]
			public ISteamMatchmakingRulesResponse.InternalRulesRefreshComplete m_VTRulesRefreshComplete;
		}
	}
}
