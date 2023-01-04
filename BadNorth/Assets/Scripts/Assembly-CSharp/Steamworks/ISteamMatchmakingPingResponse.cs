using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200033E RID: 830
	public class ISteamMatchmakingPingResponse
	{
		// Token: 0x06001256 RID: 4694 RVA: 0x00027670 File Offset: 0x00025A70
		public ISteamMatchmakingPingResponse(ISteamMatchmakingPingResponse.ServerResponded onServerResponded, ISteamMatchmakingPingResponse.ServerFailedToRespond onServerFailedToRespond)
		{
			if (onServerResponded == null || onServerFailedToRespond == null)
			{
				throw new ArgumentNullException();
			}
			this.m_ServerResponded = onServerResponded;
			this.m_ServerFailedToRespond = onServerFailedToRespond;
			this.m_VTable = new ISteamMatchmakingPingResponse.VTable
			{
				m_VTServerResponded = new ISteamMatchmakingPingResponse.InternalServerResponded(this.InternalOnServerResponded),
				m_VTServerFailedToRespond = new ISteamMatchmakingPingResponse.InternalServerFailedToRespond(this.InternalOnServerFailedToRespond)
			};
			this.m_pVTable = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ISteamMatchmakingPingResponse.VTable)));
			Marshal.StructureToPtr(this.m_VTable, this.m_pVTable, false);
			this.m_pGCHandle = GCHandle.Alloc(this.m_pVTable, GCHandleType.Pinned);
		}

		// Token: 0x06001257 RID: 4695 RVA: 0x00027718 File Offset: 0x00025B18
		~ISteamMatchmakingPingResponse()
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

		// Token: 0x06001258 RID: 4696 RVA: 0x0002777C File Offset: 0x00025B7C
		private void InternalOnServerResponded(gameserveritem_t server)
		{
			this.m_ServerResponded(server);
		}

		// Token: 0x06001259 RID: 4697 RVA: 0x0002778A File Offset: 0x00025B8A
		private void InternalOnServerFailedToRespond()
		{
			this.m_ServerFailedToRespond();
		}

		// Token: 0x0600125A RID: 4698 RVA: 0x00027797 File Offset: 0x00025B97
		public static explicit operator IntPtr(ISteamMatchmakingPingResponse that)
		{
			return that.m_pGCHandle.AddrOfPinnedObject();
		}

		// Token: 0x04000C51 RID: 3153
		private ISteamMatchmakingPingResponse.VTable m_VTable;

		// Token: 0x04000C52 RID: 3154
		private IntPtr m_pVTable;

		// Token: 0x04000C53 RID: 3155
		private GCHandle m_pGCHandle;

		// Token: 0x04000C54 RID: 3156
		private ISteamMatchmakingPingResponse.ServerResponded m_ServerResponded;

		// Token: 0x04000C55 RID: 3157
		private ISteamMatchmakingPingResponse.ServerFailedToRespond m_ServerFailedToRespond;

		// Token: 0x0200033F RID: 831
		// (Invoke) Token: 0x0600125C RID: 4700
		public delegate void ServerResponded(gameserveritem_t server);

		// Token: 0x02000340 RID: 832
		// (Invoke) Token: 0x06001260 RID: 4704
		public delegate void ServerFailedToRespond();

		// Token: 0x02000341 RID: 833
		// (Invoke) Token: 0x06001264 RID: 4708
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		private delegate void InternalServerResponded(gameserveritem_t server);

		// Token: 0x02000342 RID: 834
		// (Invoke) Token: 0x06001268 RID: 4712
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		private delegate void InternalServerFailedToRespond();

		// Token: 0x02000343 RID: 835
		[StructLayout(LayoutKind.Sequential)]
		private class VTable
		{
			// Token: 0x04000C56 RID: 3158
			[NonSerialized]
			[MarshalAs(UnmanagedType.FunctionPtr)]
			public ISteamMatchmakingPingResponse.InternalServerResponded m_VTServerResponded;

			// Token: 0x04000C57 RID: 3159
			[NonSerialized]
			[MarshalAs(UnmanagedType.FunctionPtr)]
			public ISteamMatchmakingPingResponse.InternalServerFailedToRespond m_VTServerFailedToRespond;
		}
	}
}
