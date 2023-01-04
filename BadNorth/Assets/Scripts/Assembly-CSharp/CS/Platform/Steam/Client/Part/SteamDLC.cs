using System;
using CS.Platform.Utils;
using Steamworks;
using UnityEngine;

namespace CS.Platform.Steam.Client.Part
{
	// Token: 0x02000042 RID: 66
	public class SteamDLC : MonoBehaviour
	{
		// Token: 0x060002A4 RID: 676 RVA: 0x0000CC8C File Offset: 0x0000B08C
		public bool HasDLC(string dlcKey)
		{
			uint num = DLC.DLCSteamAPI(dlcKey);
			if (num != 0U)
			{
				return SteamApps.BIsDlcInstalled((AppId_t)num);
			}
			CS.Platform.Utils.Debug.LogWarning("[Steamworks] DLC Check Failed: No Steam API  | Key: {0}", new object[]
			{
				dlcKey
			});
			return false;
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000CCC8 File Offset: 0x0000B0C8
		public bool CanShowDLCStore(string dlcKey)
		{
			try
			{
				return 0U != DLC.DLCSteamAPI(dlcKey);
			}
			catch (Exception exception)
			{
				UnityEngine.Debug.LogException(exception);
			}
			return false;
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000CD08 File Offset: 0x0000B108
		public bool ShowDLCStore(string dlcKey)
		{
			uint num = DLC.DLCSteamAPI(dlcKey);
			if (num != 0U)
			{
				SteamFriends.ActivateGameOverlayToStore((AppId_t)num, EOverlayToStoreFlag.k_EOverlayToStoreFlag_None);
				return true;
			}
			CS.Platform.Utils.Debug.LogWarning("[Steamworks] DLC Store Page Failed: No Steam API | Key: {0}", new object[]
			{
				dlcKey
			});
			return false;
		}
	}
}
