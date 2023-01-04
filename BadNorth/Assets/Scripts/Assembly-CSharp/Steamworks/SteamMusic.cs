using System;

namespace Steamworks
{
	// Token: 0x0200020F RID: 527
	public static class SteamMusic
	{
		// Token: 0x06000D53 RID: 3411 RVA: 0x0002410E File Offset: 0x0002250E
		public static bool BIsEnabled()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamMusic_BIsEnabled();
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x0002411A File Offset: 0x0002251A
		public static bool BIsPlaying()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamMusic_BIsPlaying();
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x00024126 File Offset: 0x00022526
		public static AudioPlayback_Status GetPlaybackStatus()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamMusic_GetPlaybackStatus();
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x00024132 File Offset: 0x00022532
		public static void Play()
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamMusic_Play();
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x0002413E File Offset: 0x0002253E
		public static void Pause()
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamMusic_Pause();
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x0002414A File Offset: 0x0002254A
		public static void PlayPrevious()
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamMusic_PlayPrevious();
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x00024156 File Offset: 0x00022556
		public static void PlayNext()
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamMusic_PlayNext();
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x00024162 File Offset: 0x00022562
		public static void SetVolume(float flVolume)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamMusic_SetVolume(flVolume);
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x0002416F File Offset: 0x0002256F
		public static float GetVolume()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamMusic_GetVolume();
		}
	}
}
