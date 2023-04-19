using System;
using System.Runtime.CompilerServices;
using CS.Lights.Alien;
using CS.Platform;
using CS.Platform.Base.Client.Part;
using CS.Platform.Steam.Client;
using CS.Platform.Utils;
using CS.Platform.Utils.Data;
using I2.Loc;
using Steamworks;
using UnityEngine;
using Voxels.TowerDefense;

namespace CS.VT
{
	// Token: 0x02000386 RID: 902
	public static class CorePlatformSetup
	{
		// Token: 0x060014A5 RID: 5285 RVA: 0x0002A2FC File Offset: 0x000286FC
		public static bool Setup()
		{
			if (BasePlatformManager.Instance == null)
			{
				UserSettings.I2LanguageInitToSystem();
				if (CorePlatformSetup.action == null)
				{
					CorePlatformSetup.action = new LocalizationManager.OnLocalizeCallback(CorePlatformSetup.SetLocKeys);
				}
				LocalizationManager.OnLocalizeEvent += CorePlatformSetup.action;
				CorePlatformSetup.SetLocKeys();
				GameObject gameObject = new GameObject("PlatformManager");
				UnityEngine.Object.DontDestroyOnLoad(gameObject);
				gameObject.AddComponent<I2LocPlatformParam>();
				Acheivements.AcheivementSetup(null);
				Statistics.StatisticsSetup(null);
				Storage.StorageSetup(null);
				AgeRating.AgeRatingSetup(null);
				Presence.PreseneceSetup(null);
				DLC.DLCSetup(null);
				CS.Platform.Utils.Random.SetGameBasedUI(typeof(CorePlatformEvents));
				SonyServiceDatabase.ApplySonyServiceInfo(null);
				SteamManager.DesiredGameID = (AppId_t)1138840U;
				gameObject.AddComponent<SteamManager>();
				AlienManager.SetupAlienFX();
				return true;
			}
			return false;
		}

		// Token: 0x060014A6 RID: 5286 RVA: 0x0002A3B4 File Offset: 0x000287B4
		public static void SetLocKeys()
		{
			BaseStorage.LOCALCONTINUEWITHOUTSAVINGMESSAGE = (LocalizationManager.GetTranslation("SYSTEM/DATA/SAVE_CONTINUEWITHOUT", true, 0, true, false, null, null) ?? BaseStorage.LOCALCONTINUEWITHOUTSAVINGMESSAGE);
			BaseStorage.LOCALCANCELSAVEMESSAGE = (LocalizationManager.GetTranslation("SYSTEM/DATA/SAVE_CANCEL", true, 0, true, false, null, null) ?? BaseStorage.LOCALCANCELSAVEMESSAGE);
			BaseStorage.LOCALRETRYSAVEMESSAGE = (LocalizationManager.GetTranslation("SYSTEM/DATA/SAVE_RETRY", true, 0, true, false, null, null) ?? BaseStorage.LOCALRETRYSAVEMESSAGE);
			BaseStorage.LOCALRETRYLOADMESSAGE = (LocalizationManager.GetTranslation("SYSTEM/DATA/LOAD_RETRY", true, 0, true, false, null, null) ?? BaseStorage.LOCALRETRYLOADMESSAGE);
			BaseStorage.LOCALCONTINUEWITHOUTLOADINGMESSAGE = (LocalizationManager.GetTranslation("SYSTEM/DATA/LOAD_CONTINUEWITHOUT", true, 0, true, false, null, null) ?? BaseStorage.LOCALCONTINUEWITHOUTLOADINGMESSAGE);
		}

		// Token: 0x04000CD6 RID: 3286
		[CompilerGenerated]
		private static LocalizationManager.OnLocalizeCallback action;
	}
}
