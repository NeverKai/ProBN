using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using CS.Platform;
using I2.Loc;
using ReflexCLI.Attributes;
using UnityEngine;
using Voxels.TowerDefense.ProfileInternals;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x020008F5 RID: 2293
	[ConsoleCommandClassCustomizer("Profile")]
	public static class MetaMenuHelpers
	{
		// Token: 0x06003CE7 RID: 15591 RVA: 0x00110324 File Offset: 0x0010E724
		public static bool LoadGame(CampaignSaveMeta campaignMeta)
		{
			if (campaignMeta.loadToCheckpoint)
			{
				LoadingScreen.BeginLoadingPhase(ScriptLocalization.Get("CHECKPOINT/LOADING/LOAD_DESC", true, 0, true, false, null, null), null, new IEnumerator[]
				{
					Profile.LoadCampaign(campaignMeta, true)
				});
			}
			else if (campaignMeta.gameOver)
			{
				MetaMenuHelpers.CampaignLoadFailed(campaignMeta);
			}
			else
			{
				LoadingScreen.BeginLoadingPhase(ScriptLocalization.Get("LOAD_SCREEN/RESUME_CAMPAIGN", true, 0, true, false, null, null), null, new IEnumerator[]
				{
					Profile.LoadCampaign(campaignMeta, false)
				});
			}
			return true;
		}

		// Token: 0x06003CE8 RID: 15592 RVA: 0x001103A4 File Offset: 0x0010E7A4
		public static void CampaignLoadFailed(CampaignSaveMeta campaignMeta)
		{
			Singleton<Stack>.instance.stateMeta.SetActive(true);
			if (BasePlatformManager.Instance.MainUserSignedIn)
			{
				string text = campaignMeta.GetDisplayName();
				if (string.IsNullOrEmpty(text))
				{
					string translation = LocalizationManager.GetTranslation("UI/SAVE_LOAD/SLOT_DISPLAY_NAME", true, 0, true, false, null, null);
					text = translation.Replace("{[NUM]}", campaignMeta.campaignNumber.ToString());
				}
				ModalOverlay.GetInstance().InitializeNonLocalized(LocalizationManager.GetTranslation("UI/COMMON/ERROR", true, 0, true, false, null, null), string.Format(LocalizationManager.GetTranslation("SYSTEM/LOAD/WARNING_FAILED", true, 0, true, false, null, null) + " '{0}'.", text), false).AddOKButton(null);
			}
			else
			{
				ModalOverlay.GetInstance().InitializeNonLocalized(LocalizationManager.GetTranslation("UI/COMMON/ERROR", true, 0, true, false, null, null), LocalizationManager.GetTranslation("SYSTEM/USER/LOAD_SIGNIN_NEEDED", true, 0, true, true, null, null), false).AddOKButton(null);
			}
		}

		// Token: 0x06003CE9 RID: 15593 RVA: 0x00110489 File Offset: 0x0010E889
		[ConsoleCommand("")]
		public static void NewGame(int seed)
		{
			MetaMenuHelpers.NewGame(Profile.CreateNewCampaign(seed));
		}

		// Token: 0x06003CEA RID: 15594 RVA: 0x00110498 File Offset: 0x0010E898
		public static void NewGame(CampaignSave campaign)
		{
			try
			{
				string new_CAMPAIGN = ScriptLocalization.LOAD_SCREEN.NEW_CAMPAIGN;
				if (MetaMenuHelpers.action == null)
				{
					MetaMenuHelpers.action = new Action(MetaMenuHelpers.EnterNewCampaign);
				}
				LoadingScreen.BeginLoadingPhase(new_CAMPAIGN, MetaMenuHelpers.action, new IEnumerator[]
				{
					CampaignManager.GenerateCampaign(campaign, true)
				});
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
			}
		}

		// Token: 0x06003CEB RID: 15595 RVA: 0x00110500 File Offset: 0x0010E900
		private static void EnterNewCampaign()
		{
			Singleton<CampaignManager>.instance.campaign.startLevel.Play();
		}

		// Token: 0x06003CEC RID: 15596 RVA: 0x00110516 File Offset: 0x0010E916
		public static bool ExitToMainMenu()
		{
			LoadingScreen.BeginLoadingPhase(string.Empty, delegate
			{
				Singleton<Stack>.instance.stateMeta.SetActive(true);
			}, new IEnumerator[]
			{
				MetaMenuHelpers.ExitToMainMenuRoutine()
			});
			return true;
		}

		// Token: 0x06003CED RID: 15597 RVA: 0x00110550 File Offset: 0x0010E950
		public static IEnumerator ExitToMainMenuRoutine()
		{
			Singleton<CampaignManager>.instance.ClearCampaign();
			IEnumerator r = Singleton<CampaignManager>.instance.islandGenerator.ShutDownRoutine();
			while (r.MoveNext())
			{
				yield return null;
			}
			yield break;
		}

		// Token: 0x06003CEE RID: 15598 RVA: 0x00110564 File Offset: 0x0010E964
		public static bool LoadCheckpoint(CampaignSaveMeta campaignSaveMeta, CampaignSave newerCampaignSave = null)
		{
			LoadingScreen.BeginLoadingPhase(LocalizationManager.GetTranslation("CHECKPOINT/LOADING/RELOAD_DESC", true, 0, true, false, null, null), null, new IEnumerator[]
			{
				MetaMenuHelpers.LoadCheckpointRoutine(campaignSaveMeta, newerCampaignSave)
			});
			return true;
		}

		// Token: 0x06003CEF RID: 15599 RVA: 0x00110590 File Offset: 0x0010E990
		public static IEnumerator LoadCheckpointRoutine(CampaignSaveMeta campaignSaveMeta, CampaignSave newerCampaignSave)
		{
			Dictionary<int, int> knownSeeds = null;
			if (newerCampaignSave != null)
			{
				knownSeeds = new Dictionary<int, int>(newerCampaignSave.levelStates.Count);
				foreach (LevelState levelState in newerCampaignSave.levelStates)
				{
					if (levelState.goodSeed)
					{
						knownSeeds.Add((int)levelState.index, levelState.seed);
					}
				}
			}
			newerCampaignSave = null;
			CampaignManager campaignManager = Singleton<CampaignManager>.instance;
			if (campaignManager.campaign)
			{
				IEnumerator r = campaignManager.islandGenerator.ShutDownRoutine();
				while (r.MoveNext())
				{
					yield return null;
				}
				campaignManager.ClearCampaign();
			}
			Singleton<Stack>.instance.stateCampaign.SetActive(false);
			IEnumerator<object> r2 = Profile.LoadCampaignCheckpoint(campaignSaveMeta, knownSeeds);
			while (r2.MoveNext())
			{
				yield return null;
			}
			yield break;
		}

		// Token: 0x06003CF0 RID: 15600 RVA: 0x001105B2 File Offset: 0x0010E9B2
		public static bool Quit()
		{
			Application.Quit();
			return true;
		}

		// Token: 0x06003CF1 RID: 15601 RVA: 0x001105BC File Offset: 0x0010E9BC
		public static void DeleteSave(CampaignSaveMeta campaignMeta, Action afterMath)
		{
			if (Profile.campaign && Profile.activeCampaignMeta == campaignMeta)
			{
				Profile.activeCampaignMeta = null;
				Profile.campaign = null;
			}
			LoadingScreen.BeginLoadingPhase(string.Empty, afterMath, new IEnumerator[]
			{
				Profile.meta.DeleteSaveSlot(campaignMeta)
			});
		}

		// Token: 0x04002A83 RID: 10883
		[CompilerGenerated]
		private static Action action;
	}
}
