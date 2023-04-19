using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using CS.Platform;
using CS.Platform.Steam.Client;
using ReflexCLI.Attributes;
using Steamworks;
using UnityEngine;
using Voxels.TowerDefense.UI;

namespace TrialVersion
{
	// Token: 0x020005D5 RID: 1493
	[ConsoleCommandClassCustomizer("TrialVersion")]
	public static class Utils
	{
		// Token: 0x060026D6 RID: 9942 RVA: 0x0007C42F File Offset: 0x0007A82F
		static Utils()
		{
			if (Utils.action == null)
			{
				Utils.action = new Func<bool>(Utils.LaunchStore);
			}
			Utils.launchStore = Utils.action;
			Utils.launchStoreAndExitToMain = delegate()
			{
				bool flag = Utils.LaunchStore();
				if (flag)
				{
					MetaMenuHelpers.ExitToMainMenu();
				}
				return flag;
			};
		}

		// Token: 0x060026D7 RID: 9943 RVA: 0x0007C464 File Offset: 0x0007A864
		[Conditional("TRIAL_VERSION")]
		[Conditional("UPGRADEABLE_TRIAL")]
		public static void DoTrialVersionPopup(string title)
		{
			Func<bool> action = Utils.launchStoreAndExitToMain;
			ModalOverlay instance = ModalOverlayPool.GetInstance();
			instance.Initialize(title, "TRIAL/OVER/MESSAGE", false);
			GeneratedMenu generatedMenu = instance;
			string labelLocTerm = "TRIAL/UPGRADE/MAYBE_LATER";
			if (Utils.action1 == null)
			{
				Utils.action1 = new Func<bool>(MetaMenuHelpers.ExitToMainMenu);
			}
			generatedMenu.AddButton(labelLocTerm, Utils.action1, null, null);
			ButtonWidget defaultNavigable = instance.AddButton("TRIAL/UPGRADE/NOW", action, null, null);
			instance.SetDefaultNavigable(defaultNavigable);
			instance.SetOpenAudio("Mus/ThankYou");
		}

		// Token: 0x060026D8 RID: 9944 RVA: 0x0007C4E0 File Offset: 0x0007A8E0
		public static bool LaunchStore()
		{
			try
			{
				if (SteamUtils.IsOverlayEnabled())
				{
					SteamManager steamManager = BasePlatformManager.Instance as SteamManager;
					SteamFriends.ActivateGameOverlayToStore((AppId_t)688420U, EOverlayToStoreFlag.k_EOverlayToStoreFlag_None);
					return true;
				}
			}
			catch (Exception exception)
			{
				UnityEngine.Debug.LogException(exception);
			}
			string fileName = "https://store.steampowered.com/app/688420/";
			Process.Start(fileName);
			return true;
		}

		// Token: 0x060026D9 RID: 9945 RVA: 0x0007C54C File Offset: 0x0007A94C
		public static void RefreshEntitlement()
		{
		}

		// Token: 0x060026DA RID: 9946 RVA: 0x0007C54E File Offset: 0x0007A94E
		public static bool IsTrial()
		{
			return true;
		}

		// Token: 0x040018E3 RID: 6371
		public static Func<bool> launchStore;

		// Token: 0x040018E4 RID: 6372
		public static Func<bool> launchStoreAndExitToMain;

		// Token: 0x040018E5 RID: 6373
		[CompilerGenerated]
		private static Func<bool> action;

		// Token: 0x040018E6 RID: 6374
		[CompilerGenerated]
		private static Func<bool> action1;
	}
}
