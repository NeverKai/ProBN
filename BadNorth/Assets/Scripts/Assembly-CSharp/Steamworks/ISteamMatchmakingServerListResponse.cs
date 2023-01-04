using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000336 RID: 822
	public class ISteamMatchmakingServerListResponse
	{
		// Token: 0x06001237 RID: 4663 RVA: 0x00027500 File Offset: 0x00025900
		public ISteamMatchmakingServerListResponse(ISteamMatchmakingServerListResponse.ServerResponded onServerResponded, ISteamMatchmakingServerListResponse.ServerFailedToRespond onServerFailedToRespond, ISteamMatchmakingServerListResponse.RefreshComplete onRefreshComplete)
		{
			if (onServerResponded == null || onServerFailedToRespond == null || onRefreshComplete == null)
			{
				throw new ArgumentNullException();
			}
			this.m_ServerResponded = onServerResponded;
			this.m_ServerFailedToRespond = onServerFailedToRespond;
			this.m_RefreshComplete = onRefreshComplete;
			this.m_VTable = new ISteamMatchmakingServerListResponse.VTable
			{
				m_VTServerResponded = new ISteamMatchmakingServerListResponse.InternalServerResponded(this.InternalOnServerResponded),
				m_VTServerFailedToRespond = new ISteamMatchmakingServerListResponse.InternalServerFailedToRespond(this.InternalOnServerFailedToRespond),
				m_VTRefreshComplete = new ISteamMatchmakingServerListResponse.InternalRefreshComplete(this.InternalOnRefreshComplete)
			};
			this.m_pVTable = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ISteamMatchmakingServerListResponse.VTable)));
			Marshal.StructureToPtr(this.m_VTable, this.m_pVTable, false);
			this.m_pGCHandle = GCHandle.Alloc(this.m_pVTable, GCHandleType.Pinned);
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x000275C8 File Offset: 0x000259C8
		~ISteamMatchmakingServerListResponse()
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

		// Token: 0x06001239 RID: 4665 RVA: 0x0002762C File Offset: 0x00025A2C
		private void InternalOnServerResponded(HServerListRequest hRequest, int iServer)
		{
			this.m_ServerResponded(hRequest, iServer);
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x0002763B File Offset: 0x00025A3B
		private void InternalOnServerFailedToRespond(HServerListRequest hRequest, int iServer)
		{
			this.m_ServerFailedToRespond(hRequest, iServer);
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x0002764A File Offset: 0x00025A4A
		private void InternalOnRefreshComplete(HServerListRequest hRequest, EMatchMakingServerResponse response)
		{
			this.m_RefreshComplete(hRequest, response);
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x00027659 File Offset: 0x00025A59
		public static explicit operator IntPtr(ISteamMatchmakingServerListResponse that)
		{
			return that.m_pGCHandle.AddrOfPinnedObject();
		}

		// Token: 0x04000C48 RID: 3144
		private ISteamMatchmakingServerListResponse.VTable m_VTable;

		// Token: 0x04000C49 RID: 3145
		private IntPtr m_pVTable;

		// Token: 0x04000C4A RID: 3146
		private GCHandle m_pGCHandle;

		// Token: 0x04000C4B RID: 3147
		private ISteamMatchmakingServerListResponse.ServerResponded m_ServerResponded;

		// Token: 0x04000C4C RID: 3148
		private ISteamMatchmakingServerListResponse.ServerFailedToRespond m_ServerFailedToRespond;

		// Token: 0x04000C4D RID: 3149
		private ISteamMatchmakingServerListResponse.RefreshComplete m_RefreshComplete;

		// Token: 0x02000337 RID: 823
		// (Invoke) Token: 0x0600123E RID: 4670
		public delegate void ServerResponded(HServerListRequest hRequest, int iServer);

		// Token: 0x02000338 RID: 824
		// (Invoke) Token: 0x06001242 RID: 4674
		public delegate void ServerFailedToRespond(HServerListRequest hRequest, int iServer);

		// Token: 0x02000339 RID: 825
		// (Invoke) Token: 0x06001246 RID: 4678
		public delegate void RefreshComplete(HServerListRequest hRequest, EMatchMakingServerResponse response);

		// Token: 0x0200033A RID: 826
		// (Invoke) Token: 0x0600124A RID: 4682
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		private delegate void InternalServerResponded(HServerListRequest hRequest, int iServer);

		// Token: 0x0200033B RID: 827
		// (Invoke) Token: 0x0600124E RID: 4686
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		private delegate void InternalServerFailedToRespond(HServerListRequest hRequest, int iServer);

		// Token: 0x0200033C RID: 828
		// (Invoke) Token: 0x06001252 RID: 4690
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		private delegate void InternalRefreshComplete(HServerListRequest hRequest, EMatchMakingServerResponse response);

		// Token: 0x0200033D RID: 829
		[StructLayout(LayoutKind.Sequential)]
		private class VTable
		{
			// Token: 0x04000C4E RID: 3150
			[NonSerialized]
			[MarshalAs(UnmanagedType.FunctionPtr)]
			public ISteamMatchmakingServerListResponse.InternalServerResponded m_VTServerResponded;

			// Token: 0x04000C4F RID: 3151
			[NonSerialized]
			[MarshalAs(UnmanagedType.FunctionPtr)]
			public ISteamMatchmakingServerListResponse.InternalServerFailedToRespond m_VTServerFailedToRespond;

			// Token: 0x04000C50 RID: 3152
			[NonSerialized]
			[MarshalAs(UnmanagedType.FunctionPtr)]
			public ISteamMatchmakingServerListResponse.InternalRefreshComplete m_VTRefreshComplete;
		}
	}
}
