using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000344 RID: 836
	public class ISteamMatchmakingPlayersResponse
	{
		// Token: 0x0600126C RID: 4716 RVA: 0x000277AC File Offset: 0x00025BAC
		public ISteamMatchmakingPlayersResponse(ISteamMatchmakingPlayersResponse.AddPlayerToList onAddPlayerToList, ISteamMatchmakingPlayersResponse.PlayersFailedToRespond onPlayersFailedToRespond, ISteamMatchmakingPlayersResponse.PlayersRefreshComplete onPlayersRefreshComplete)
		{
			if (onAddPlayerToList == null || onPlayersFailedToRespond == null || onPlayersRefreshComplete == null)
			{
				throw new ArgumentNullException();
			}
			this.m_AddPlayerToList = onAddPlayerToList;
			this.m_PlayersFailedToRespond = onPlayersFailedToRespond;
			this.m_PlayersRefreshComplete = onPlayersRefreshComplete;
			this.m_VTable = new ISteamMatchmakingPlayersResponse.VTable
			{
				m_VTAddPlayerToList = new ISteamMatchmakingPlayersResponse.InternalAddPlayerToList(this.InternalOnAddPlayerToList),
				m_VTPlayersFailedToRespond = new ISteamMatchmakingPlayersResponse.InternalPlayersFailedToRespond(this.InternalOnPlayersFailedToRespond),
				m_VTPlayersRefreshComplete = new ISteamMatchmakingPlayersResponse.InternalPlayersRefreshComplete(this.InternalOnPlayersRefreshComplete)
			};
			this.m_pVTable = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ISteamMatchmakingPlayersResponse.VTable)));
			Marshal.StructureToPtr(this.m_VTable, this.m_pVTable, false);
			this.m_pGCHandle = GCHandle.Alloc(this.m_pVTable, GCHandleType.Pinned);
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x00027874 File Offset: 0x00025C74
		~ISteamMatchmakingPlayersResponse()
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

		// Token: 0x0600126E RID: 4718 RVA: 0x000278D8 File Offset: 0x00025CD8
		private void InternalOnAddPlayerToList(IntPtr pchName, int nScore, float flTimePlayed)
		{
			this.m_AddPlayerToList(InteropHelp.PtrToStringUTF8(pchName), nScore, flTimePlayed);
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x000278ED File Offset: 0x00025CED
		private void InternalOnPlayersFailedToRespond()
		{
			this.m_PlayersFailedToRespond();
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x000278FA File Offset: 0x00025CFA
		private void InternalOnPlayersRefreshComplete()
		{
			this.m_PlayersRefreshComplete();
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x00027907 File Offset: 0x00025D07
		public static explicit operator IntPtr(ISteamMatchmakingPlayersResponse that)
		{
			return that.m_pGCHandle.AddrOfPinnedObject();
		}

		// Token: 0x04000C58 RID: 3160
		private ISteamMatchmakingPlayersResponse.VTable m_VTable;

		// Token: 0x04000C59 RID: 3161
		private IntPtr m_pVTable;

		// Token: 0x04000C5A RID: 3162
		private GCHandle m_pGCHandle;

		// Token: 0x04000C5B RID: 3163
		private ISteamMatchmakingPlayersResponse.AddPlayerToList m_AddPlayerToList;

		// Token: 0x04000C5C RID: 3164
		private ISteamMatchmakingPlayersResponse.PlayersFailedToRespond m_PlayersFailedToRespond;

		// Token: 0x04000C5D RID: 3165
		private ISteamMatchmakingPlayersResponse.PlayersRefreshComplete m_PlayersRefreshComplete;

		// Token: 0x02000345 RID: 837
		// (Invoke) Token: 0x06001273 RID: 4723
		public delegate void AddPlayerToList(string pchName, int nScore, float flTimePlayed);

		// Token: 0x02000346 RID: 838
		// (Invoke) Token: 0x06001277 RID: 4727
		public delegate void PlayersFailedToRespond();

		// Token: 0x02000347 RID: 839
		// (Invoke) Token: 0x0600127B RID: 4731
		public delegate void PlayersRefreshComplete();

		// Token: 0x02000348 RID: 840
		// (Invoke) Token: 0x0600127F RID: 4735
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void InternalAddPlayerToList(IntPtr pchName, int nScore, float flTimePlayed);

		// Token: 0x02000349 RID: 841
		// (Invoke) Token: 0x06001283 RID: 4739
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void InternalPlayersFailedToRespond();

		// Token: 0x0200034A RID: 842
		// (Invoke) Token: 0x06001287 RID: 4743
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void InternalPlayersRefreshComplete();

		// Token: 0x0200034B RID: 843
		[StructLayout(LayoutKind.Sequential)]
		private class VTable
		{
			// Token: 0x04000C5E RID: 3166
			[NonSerialized]
			[MarshalAs(UnmanagedType.FunctionPtr)]
			public ISteamMatchmakingPlayersResponse.InternalAddPlayerToList m_VTAddPlayerToList;

			// Token: 0x04000C5F RID: 3167
			[NonSerialized]
			[MarshalAs(UnmanagedType.FunctionPtr)]
			public ISteamMatchmakingPlayersResponse.InternalPlayersFailedToRespond m_VTPlayersFailedToRespond;

			// Token: 0x04000C60 RID: 3168
			[NonSerialized]
			[MarshalAs(UnmanagedType.FunctionPtr)]
			public ISteamMatchmakingPlayersResponse.InternalPlayersRefreshComplete m_VTPlayersRefreshComplete;
		}
	}
}
