using System;

namespace Steamworks
{
	// Token: 0x02000210 RID: 528
	public static class SteamMusicRemote
	{
		// Token: 0x06000D5C RID: 3420 RVA: 0x0002417C File Offset: 0x0002257C
		public static bool RegisterSteamMusicRemote(string pchName)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchName))
			{
				result = NativeMethods.ISteamMusicRemote_RegisterSteamMusicRemote(utf8StringHandle);
			}
			return result;
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x000241C0 File Offset: 0x000225C0
		public static bool DeregisterSteamMusicRemote()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamMusicRemote_DeregisterSteamMusicRemote();
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x000241CC File Offset: 0x000225CC
		public static bool BIsCurrentMusicRemote()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamMusicRemote_BIsCurrentMusicRemote();
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x000241D8 File Offset: 0x000225D8
		public static bool BActivationSuccess(bool bValue)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamMusicRemote_BActivationSuccess(bValue);
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x000241E8 File Offset: 0x000225E8
		public static bool SetDisplayName(string pchDisplayName)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchDisplayName))
			{
				result = NativeMethods.ISteamMusicRemote_SetDisplayName(utf8StringHandle);
			}
			return result;
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x0002422C File Offset: 0x0002262C
		public static bool SetPNGIcon_64x64(byte[] pvBuffer, uint cbBufferLength)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamMusicRemote_SetPNGIcon_64x64(pvBuffer, cbBufferLength);
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x0002423A File Offset: 0x0002263A
		public static bool EnablePlayPrevious(bool bValue)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamMusicRemote_EnablePlayPrevious(bValue);
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x00024247 File Offset: 0x00022647
		public static bool EnablePlayNext(bool bValue)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamMusicRemote_EnablePlayNext(bValue);
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x00024254 File Offset: 0x00022654
		public static bool EnableShuffled(bool bValue)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamMusicRemote_EnableShuffled(bValue);
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x00024261 File Offset: 0x00022661
		public static bool EnableLooped(bool bValue)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamMusicRemote_EnableLooped(bValue);
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x0002426E File Offset: 0x0002266E
		public static bool EnableQueue(bool bValue)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamMusicRemote_EnableQueue(bValue);
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x0002427B File Offset: 0x0002267B
		public static bool EnablePlaylists(bool bValue)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamMusicRemote_EnablePlaylists(bValue);
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x00024288 File Offset: 0x00022688
		public static bool UpdatePlaybackStatus(AudioPlayback_Status nStatus)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamMusicRemote_UpdatePlaybackStatus(nStatus);
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x00024295 File Offset: 0x00022695
		public static bool UpdateShuffled(bool bValue)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamMusicRemote_UpdateShuffled(bValue);
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x000242A2 File Offset: 0x000226A2
		public static bool UpdateLooped(bool bValue)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamMusicRemote_UpdateLooped(bValue);
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x000242AF File Offset: 0x000226AF
		public static bool UpdateVolume(float flValue)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamMusicRemote_UpdateVolume(flValue);
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x000242BC File Offset: 0x000226BC
		public static bool CurrentEntryWillChange()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamMusicRemote_CurrentEntryWillChange();
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x000242C8 File Offset: 0x000226C8
		public static bool CurrentEntryIsAvailable(bool bAvailable)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamMusicRemote_CurrentEntryIsAvailable(bAvailable);
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x000242D8 File Offset: 0x000226D8
		public static bool UpdateCurrentEntryText(string pchText)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchText))
			{
				result = NativeMethods.ISteamMusicRemote_UpdateCurrentEntryText(utf8StringHandle);
			}
			return result;
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x0002431C File Offset: 0x0002271C
		public static bool UpdateCurrentEntryElapsedSeconds(int nValue)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamMusicRemote_UpdateCurrentEntryElapsedSeconds(nValue);
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x00024329 File Offset: 0x00022729
		public static bool UpdateCurrentEntryCoverArt(byte[] pvBuffer, uint cbBufferLength)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamMusicRemote_UpdateCurrentEntryCoverArt(pvBuffer, cbBufferLength);
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x00024337 File Offset: 0x00022737
		public static bool CurrentEntryDidChange()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamMusicRemote_CurrentEntryDidChange();
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x00024343 File Offset: 0x00022743
		public static bool QueueWillChange()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamMusicRemote_QueueWillChange();
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x0002434F File Offset: 0x0002274F
		public static bool ResetQueueEntries()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamMusicRemote_ResetQueueEntries();
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x0002435C File Offset: 0x0002275C
		public static bool SetQueueEntry(int nID, int nPosition, string pchEntryText)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchEntryText))
			{
				result = NativeMethods.ISteamMusicRemote_SetQueueEntry(nID, nPosition, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x000243A4 File Offset: 0x000227A4
		public static bool SetCurrentQueueEntry(int nID)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamMusicRemote_SetCurrentQueueEntry(nID);
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x000243B1 File Offset: 0x000227B1
		public static bool QueueDidChange()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamMusicRemote_QueueDidChange();
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x000243BD File Offset: 0x000227BD
		public static bool PlaylistWillChange()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamMusicRemote_PlaylistWillChange();
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x000243C9 File Offset: 0x000227C9
		public static bool ResetPlaylistEntries()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamMusicRemote_ResetPlaylistEntries();
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x000243D8 File Offset: 0x000227D8
		public static bool SetPlaylistEntry(int nID, int nPosition, string pchEntryText)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchEntryText))
			{
				result = NativeMethods.ISteamMusicRemote_SetPlaylistEntry(nID, nPosition, utf8StringHandle);
			}
			return result;
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x00024420 File Offset: 0x00022820
		public static bool SetCurrentPlaylistEntry(int nID)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamMusicRemote_SetCurrentPlaylistEntry(nID);
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x0002442D File Offset: 0x0002282D
		public static bool PlaylistDidChange()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamMusicRemote_PlaylistDidChange();
		}
	}
}
