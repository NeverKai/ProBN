using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using CS.VT;
using I2.Loc;
using ReflexCLI.Attributes;
using RTM.Input;
using RTM.OnScreenDebug;
using UnityEngine;
using Voxels.TowerDefense.ProfileInternals;
using Voxels.TowerDefense.Reflex;
using Voxels.TowerDefense.UI;

namespace Voxels.TowerDefense
{
	// Token: 0x02000595 RID: 1429
	[ConsoleCommandClassCustomizer("Profile")]
	public static class Profile
	{
		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x0600250A RID: 9482 RVA: 0x0007460B File Offset: 0x00072A0B
		public static bool Loading
		{
			get
			{
				return Profile._loading != 0;
			}
		}

		// Token: 0x14000085 RID: 133
		// (add) Token: 0x0600250B RID: 9483 RVA: 0x00074618 File Offset: 0x00072A18
		// (remove) Token: 0x0600250C RID: 9484 RVA: 0x0007464C File Offset: 0x00072A4C
		
		public static event Action<Profile.UpdateType> OnProfileUpdated;

		// Token: 0x14000086 RID: 134
		// (add) Token: 0x0600250D RID: 9485 RVA: 0x00074680 File Offset: 0x00072A80
		// (remove) Token: 0x0600250E RID: 9486 RVA: 0x000746B4 File Offset: 0x00072AB4
		
		public static event Action OnSettingsLoaded;

		// Token: 0x0600250F RID: 9487 RVA: 0x000746E8 File Offset: 0x00072AE8
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void GameInit()
		{
			Profile.meta = new MetaSave();
			Profile.ResetUserSettings(false);
		}

		// Token: 0x06002510 RID: 9488 RVA: 0x000746FC File Offset: 0x00072AFC
		public static void ReloadProfile()
		{
			Profile._loading = 1;
			Profile.meta = new MetaSave();
			int playerId = 0;
			string[] filename = new string[]
			{
				"user",
				"settings",
				Profile.GetUserControlMappingFilename(playerId)
			};
			Action<Callback<MemoryStream>>[] array = new Action<Callback<MemoryStream>>[3];
			int num = 0;
			if (Profile.action == null)
			{
				Profile.action = new Action<Callback<MemoryStream>>(Profile.OnUserSaveLoaded);
			}
			array[num] = Profile.action;
			int num2 = 1;
			if (Profile.action1 == null)
			{
				Profile.action1 = new Action<Callback<MemoryStream>>(Profile.OnUserSettingLoaded);
			}
			array[num2] = Profile.action1;
			array[2] = delegate(Callback<MemoryStream> d)
			{
				Profile.OnUserInputMappingsLoaded(playerId, d);
			};
			SaveGameUtilities.Load(filename, array, null);
		}

		// Token: 0x06002511 RID: 9489 RVA: 0x000747A8 File Offset: 0x00072BA8
		private static void OnUserSettingLoaded(Callback<MemoryStream> obj)
		{
			Profile._loading++;
			try
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				if (obj.Result != null)
				{
					Profile.userSettings = (UserSettings)binaryFormatter.Deserialize(obj.Result);
				}
				else
				{
					Profile.userSettings = new UserSettings();
				}
				Profile.userSettings.ProcessUpdate(false);
			}
			catch (Exception exception)
			{
				UnityEngine.Debug.LogException(exception);
				Profile.userSettings = new UserSettings();
				Profile.userSettings.ProcessUpdate(false);
				Profile.DoFailedToLoadModalOverlay("settings");
			}
			if (Profile.OnSettingsLoaded != null)
			{
				Profile.OnSettingsLoaded();
			}
			int maxCampaignSlots = Profile.meta.maxCampaignSlots;
			if (Profile.action3 == null)
			{
				Profile.action3 = new Action<Callback<List<ISaveGameObject>>>(Profile.OnCampaignLoaded);
			}
			Action<Callback<List<ISaveGameObject>>> completionCallback = Profile.action3;
			if (Profile.action4 == null)
			{
				Profile.action4 = new Func<MemoryStream, ISaveGameObject>(Profile.OnCampaignMetaDeserialise);
			}
			SaveGameUtilities.GetHeaders(maxCampaignSlots, completionCallback, Profile.action4);
			Profile._loading--;
		}

		// Token: 0x06002512 RID: 9490 RVA: 0x000748AC File Offset: 0x00072CAC
		private static void OnUserSaveLoaded(Callback<MemoryStream> obj)
		{
			Profile._loading++;
			try
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				if (obj.Result != null)
				{
					Profile.userSave = (UserSave)binaryFormatter.Deserialize(obj.Result);
				}
				else
				{
					Profile.userSave = new UserSave();
				}
				Profile.userSave.FixUpMaxDifficulty();
			}
			catch (Exception exception)
			{
				UnityEngine.Debug.LogException(exception);
				Profile.userSave = new UserSave();
				Profile.DoFailedToLoadModalOverlay("user");
			}
			Profile._loading--;
		}

		// Token: 0x06002513 RID: 9491 RVA: 0x00074948 File Offset: 0x00072D48
		private static ISaveGameObject OnCampaignMetaDeserialise(MemoryStream arg1)
		{
			try
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				if (arg1 != null)
				{
					return (CampaignSaveMeta)binaryFormatter.Deserialize(arg1);
				}
			}
			catch
			{
			}
			return null;
		}

		// Token: 0x06002514 RID: 9492 RVA: 0x00074994 File Offset: 0x00072D94
		private static void OnCampaignLoaded(Callback<List<ISaveGameObject>> obj)
		{
			Profile.meta.campaigns.Clear();
			for (int i = 0; i < obj.Result.Count; i++)
			{
				CampaignSaveMeta campaignSaveMeta = (CampaignSaveMeta)obj.Result[i];
				if (campaignSaveMeta != null)
				{
					campaignSaveMeta.SetSaveSlot(i);
				}
				Profile.meta.campaigns.Add(campaignSaveMeta);
			}
			Profile._loading--;
		}

		// Token: 0x06002515 RID: 9493 RVA: 0x00074A08 File Offset: 0x00072E08
		public static void ResetUserSettings(bool dirty)
		{
			UserSettings.GamepadLayout gamepadLayout = (!Profile.userSettings) ? UserSettings.GamepadLayout.Classic : Profile.userSettings.gamepadLayout;
			Profile.userSettings = new UserSettings();
			Profile.userSettings.gamepadLayout = gamepadLayout;
			Profile.userSettings.ProcessUpdate(dirty);
		}

		// Token: 0x06002516 RID: 9494 RVA: 0x00074A58 File Offset: 0x00072E58
		public static CampaignSave CreateNewCampaign(int seed)
		{
			Profile.campaign = new CampaignSave();
			Profile.campaign.seed = seed;
			Profile.campaign.loadTime = Time.unscaledTime;
			Profile.campaign.prefs = Profile.userSave.campaignPrefs;
			Profile.activeCampaignMeta = Profile.campaign;
			Profile.activeCampaignMeta.SetSaveSlot(Profile.meta.FirstFreeSlot);
			Profile.userSave.campaignCount++;
			Profile.activeCampaignMeta.campaignNumber = Profile.userSave.campaignCount;
			return Profile.campaign;
		}

		// Token: 0x06002517 RID: 9495 RVA: 0x00074AEC File Offset: 0x00072EEC
		public static IEnumerator<object> LoadCampaign(CampaignSaveMeta campaignSaveMeta, bool loadCheckpoint)
		{
			string saveFileName = (!loadCheckpoint) ? campaignSaveMeta.targetFileName : campaignSaveMeta.checkpointFileName;
			if (loadCheckpoint)
			{
				campaignSaveMeta.checkpointReloads++;
			}
			IEnumerator<object> r = Profile.Load(campaignSaveMeta, saveFileName);
			while (r.MoveNext())
			{
				object obj = r.Current;
				yield return obj;
			}
			if (Profile.campaign == null)
			{
				MetaMenuHelpers.CampaignLoadFailed(campaignSaveMeta);
			}
			else if (Profile.campaign.gameOver)
			{
				MetaMenuHelpers.CampaignLoadFailed(campaignSaveMeta);
			}
			else
			{
				IEnumerator r2 = CampaignManager.GenerateCampaign(Profile.campaign, false);
				while (r2.MoveNext())
				{
					yield return null;
				}
				Singleton<Stack>.instance.stateCampaign.SetActive(true);
			}
			yield break;
		}

		// Token: 0x06002518 RID: 9496 RVA: 0x00074B10 File Offset: 0x00072F10
		public static IEnumerator<object> LoadCampaignCheckpoint(CampaignSaveMeta campaignSaveMeta, Dictionary<int, int> knownSeeds)
		{
			yield return null;
			IEnumerator<object> r = Profile.Load(campaignSaveMeta, campaignSaveMeta.checkpointFileName);
			while (r.MoveNext())
			{
				object obj = r.Current;
				yield return obj;
			}
			if (Profile.campaign == null)
			{
				MetaMenuHelpers.CampaignLoadFailed(campaignSaveMeta);
			}
			else
			{
				try
				{
					if (knownSeeds != null)
					{
						Profile.campaign.ApplyLevelStatesToCheckpoint(knownSeeds);
					}
				}
				catch (Exception exception)
				{
					UnityEngine.Debug.LogException(exception);
				}
				campaignSaveMeta.checkpointReloads++;
				Profile.SaveCampaign(false);
				IEnumerator r2 = CampaignManager.GenerateCampaign(Profile.campaign, false);
				while (r2.MoveNext())
				{
					yield return null;
				}
				Singleton<Stack>.instance.stateCampaign.SetActive(true);
			}
			yield break;
		}

		// Token: 0x06002519 RID: 9497 RVA: 0x00074B34 File Offset: 0x00072F34
		private static IEnumerator<object> Load(CampaignSaveMeta campaignSave, string targetFile)
		{
			float t = Time.unscaledTime + 0.3f;
			while (t > Time.unscaledTime)
			{
				yield return null;
			}
			Profile.campaign = null;
			Profile.activeCampaignMeta = campaignSave;
			if (Profile.action5 == null)
			{
				Profile.action5 = new Action<Callback<MemoryStream>>(Profile.OnCampaignDeserialise);
			}
			Callback<MemoryStream> campaignLoader = SaveGameUtilities.Load(targetFile, Profile.action5, null);
			while (campaignLoader.MoveNext())
			{
				object obj = campaignLoader.Current;
				yield return obj;
			}
			yield break;
		}

		// Token: 0x0600251A RID: 9498 RVA: 0x00074B58 File Offset: 0x00072F58
		private static void OnCampaignDeserialise(Callback<MemoryStream> obj)
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			try
			{
				if (obj.Result != null)
				{
					Profile.campaign = (CampaignSave)binaryFormatter.Deserialize(obj.Result);
				}
				else
				{
					Profile.campaign = null;
				}
			}
			catch (Exception exception)
			{
				UnityEngine.Debug.LogException(exception);
				Profile.campaign = null;
			}
		}

		// Token: 0x0600251B RID: 9499 RVA: 0x00074BC0 File Offset: 0x00072FC0
		[ConsoleCommand("Save.Campaign")]
		public static void SaveCampaign(bool saveCheckpoint = false)
		{
			if (Profile.campaign.gameOver && !Profile.campaign.hasCheckpoint)
			{
				Profile.DeleteCurrentCampaign();
				return;
			}
			Profile.OnProfileUpdated(Profile.UpdateType.Save);
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (MemoryStream memoryStream2 = new MemoryStream())
				{
					using (MemoryStream memoryStream3 = new MemoryStream())
					{
						BinaryFormatter binaryFormatter = new BinaryFormatter();
						Profile.campaign.hasCheckpoint = saveCheckpoint;
						Profile.activeCampaignMeta.Refresh(Profile.campaign, saveCheckpoint);
						binaryFormatter.Serialize(memoryStream2, Profile.campaign);
						binaryFormatter.Serialize(memoryStream, Profile.activeCampaignMeta);
						binaryFormatter.Serialize(memoryStream3, Profile.userSave);
						CampaignSaveMeta campaignSaveMeta = Profile.activeCampaignMeta;
						if (saveCheckpoint)
						{
							SaveGameUtilities.Save(new string[]
							{
								campaignSaveMeta.targetFileName,
								campaignSaveMeta.checkpointFileName,
								campaignSaveMeta.metaFileName,
								"user"
							}, new MemoryStream[]
							{
								memoryStream2,
								memoryStream2,
								memoryStream,
								memoryStream3
							}, null);
						}
						else
						{
							SaveGameUtilities.Save(new string[]
							{
								campaignSaveMeta.targetFileName,
								campaignSaveMeta.metaFileName,
								"user"
							}, new MemoryStream[]
							{
								memoryStream2,
								memoryStream,
								memoryStream3
							}, null);
						}
					}
				}
			}
			int saveSlot = Profile.activeCampaignMeta.saveSlot;
			Profile.meta.campaigns[Profile.activeCampaignMeta.saveSlot] = Profile.activeCampaignMeta;
		}

		// Token: 0x0600251C RID: 9500 RVA: 0x00074D9C File Offset: 0x0007319C
		[ConsoleCommand("Save.Settings")]
		public static void SaveSettings()
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				binaryFormatter.Serialize(memoryStream, Profile.userSettings);
				SaveGameUtilities.Save("settings", memoryStream, null);
			}
		}

		// Token: 0x0600251D RID: 9501 RVA: 0x00074DF0 File Offset: 0x000731F0
		[ConsoleCommand("Save.UserSave")]
		public static void SaveUserSave()
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				binaryFormatter.Serialize(memoryStream, Profile.userSave);
				SaveGameUtilities.Save("user", memoryStream, null);
			}
		}

		// Token: 0x0600251E RID: 9502 RVA: 0x00074E44 File Offset: 0x00073244
		[ConsoleCommand("")]
		private static void DeleteCurrentCampaign()
		{
			if (Profile.activeCampaignMeta != null)
			{
				Profile.meta.DeleteSaveSlot(Profile.activeCampaignMeta);
			}
			Profile.SaveUserSave();
		}

		// Token: 0x0600251F RID: 9503 RVA: 0x00074E65 File Offset: 0x00073265
		private static string GetUserControlMappingFilename(int playerId)
		{
			return string.Format("control map - {0}", playerId);
		}

		// Token: 0x06002520 RID: 9504 RVA: 0x00074E78 File Offset: 0x00073278
		public static void SaveUserControlMapping(int playerId)
		{
			RewiredStorageUtils.PlayerData playerData = RewiredStorageUtils.ExportPlayerData(playerId);
			if (playerData != null)
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					binaryFormatter.Serialize(memoryStream, playerData);
					SaveGameUtilities.Save(Profile.GetUserControlMappingFilename(playerId), memoryStream, null);
				}
			}
		}

		// Token: 0x06002521 RID: 9505 RVA: 0x00074ED8 File Offset: 0x000732D8
		public static void LoadUserControlMapping(int playerId)
		{
			SaveGameUtilities.Load(Profile.GetUserControlMappingFilename(playerId), delegate(Callback<MemoryStream> d)
			{
				Profile.OnUserInputMappingsLoaded(0, d);
			}, null);
		}

		// Token: 0x06002522 RID: 9506 RVA: 0x00074F04 File Offset: 0x00073304
		public static void OnUserInputMappingsLoaded(int playerId, Callback<MemoryStream> obj)
		{
			try
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				if (obj.Result != null)
				{
					RewiredStorageUtils.PlayerData playerData = (RewiredStorageUtils.PlayerData)binaryFormatter.Deserialize(obj.Result);
					RewiredStorageUtils.ApplyPlayerData(playerId, playerData);
				}
			}
			catch (Exception exception)
			{
				UnityEngine.Debug.LogException(exception);
				Profile.userSave = new UserSave();
				Profile.DoFailedToLoadModalOverlay(Profile.GetUserControlMappingFilename(playerId));
			}
		}

		// Token: 0x06002523 RID: 9507 RVA: 0x00074F74 File Offset: 0x00073374
		public static void DeletedUserControlMappings(int playerId)
		{
			SaveGameUtilities.DeleteFile(Profile.GetUserControlMappingFilename(playerId));
		}

		// Token: 0x06002524 RID: 9508 RVA: 0x00074F84 File Offset: 0x00073384
		private static void DoFailedToLoadModalOverlay(string filename)
		{
			ModalOverlayPool.GetInstance().InitializeNonLocalized(LocalizationManager.GetTranslation("UI/COMMON/ERROR", true, 0, true, false, null, null), string.Format(LocalizationManager.GetTranslation("SYSTEM/LOAD/WARNING_FAILED", true, 0, true, false, null, null) + " '{0}'.", filename), false).AddOKButton(null);
		}

		// Token: 0x06002525 RID: 9509 RVA: 0x00074FD4 File Offset: 0x000733D4
		[ConsoleCommand("Dump.CampaignSave")]
		private static string DumpCampaign()
		{
			ObjectDumper objectDumper = new ObjectDumper();
			return objectDumper.Dump(Profile.campaign);
		}

		// Token: 0x06002526 RID: 9510 RVA: 0x00074FF4 File Offset: 0x000733F4
		[ConsoleCommand("Dump.HeroState")]
		private static string DumpHeroState(HeroDefinition hero)
		{
			ObjectDumper objectDumper = new ObjectDumper();
			return objectDumper.Dump(hero);
		}

		// Token: 0x06002527 RID: 9511 RVA: 0x00075010 File Offset: 0x00073410
		[ConsoleCommand("Dump.HeroSummaries")]
		private static string DumpHeroSummaries()
		{
			ObjectDumper objectDumper = new ObjectDumper();
			return objectDumper.Dump(Profile.campaign.heroes);
		}

		// Token: 0x06002528 RID: 9512 RVA: 0x00075034 File Offset: 0x00073434
		[ConsoleCommand("Dump.CampaignMeta")]
		private static string DumpCampaignMeta()
		{
			ObjectDumper objectDumper = new ObjectDumper();
			return objectDumper.Dump(Profile.activeCampaignMeta);
		}

		// Token: 0x06002529 RID: 9513 RVA: 0x00075054 File Offset: 0x00073454
		[ConsoleCommand("Dump.UserSettings")]
		private static string DumpUserSettings()
		{
			ObjectDumper objectDumper = new ObjectDumper();
			return objectDumper.Dump(Profile.userSettings);
		}

		// Token: 0x0600252A RID: 9514 RVA: 0x00075074 File Offset: 0x00073474
		[ConsoleCommand("Dump.CampaignStats")]
		private static string DumpCampaignStats()
		{
			ObjectDumper objectDumper = new ObjectDumper();
			return objectDumper.Dump(Profile.campaign.stats);
		}

		// Token: 0x0600252B RID: 9515 RVA: 0x00075098 File Offset: 0x00073498
		[ConsoleCommand("Dump.UserSave")]
		private static string DumpUserSave()
		{
			ObjectDumper objectDumper = new ObjectDumper();
			return objectDumper.Dump(Profile.userSave);
		}

		// Token: 0x0600252C RID: 9516 RVA: 0x000750B8 File Offset: 0x000734B8
		[ConsoleCommand("Dump.LevelState")]
		private static string DumpLevelState(LevelState level)
		{
			ObjectDumper objectDumper = new ObjectDumper();
			return objectDumper.Dump(level);
		}

		// Token: 0x0600252D RID: 9517 RVA: 0x000750D2 File Offset: 0x000734D2
		[ConsoleCommand("Dump.LevelSummary")]
		private static string DumpLevelSummary(LevelState level)
		{
			return level.ToString();
		}

		// Token: 0x0600252E RID: 9518 RVA: 0x000750DC File Offset: 0x000734DC
		[ConsoleCommand("Dump.LevelSummaries")]
		private static string DumpLevelSummaries()
		{
			ObjectDumper objectDumper = new ObjectDumper();
			return objectDumper.Dump(Profile.campaign.levelStates);
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x0600252F RID: 9519 RVA: 0x000750FF File Offset: 0x000734FF
		// (set) Token: 0x06002530 RID: 9520 RVA: 0x0007510B File Offset: 0x0007350B
		[ConsoleCommand("")]
		private static int vikingFrontier
		{
			get
			{
				return Profile.campaign.vikingFrontierPosition;
			}
			set
			{
				Profile.campaign.vikingFrontierPosition = value;
				Profile.OnProfileUpdated(Profile.UpdateType.Save);
			}
		}

		// Token: 0x06002531 RID: 9521 RVA: 0x00075124 File Offset: 0x00073524
		[ConsoleCommand("Inventory.AddAll")]
		[DebugSetting("Add All Items to Inventory", "给库存添加所有道具", DebugSettingLocation.Campaign)]
		private static void AddAllToInventory()
		{
			foreach (HeroUpgradeDefinition heroUpgradeDefinition in ResourceList<HeroUpgradeDefinition>.list)
			{
				if (heroUpgradeDefinition.typeEnum == HeroUpgradeTypeEnum.Item || heroUpgradeDefinition.typeEnum == HeroUpgradeTypeEnum.Consumable)
				{
					Profile.campaign.inventory.Add(heroUpgradeDefinition);
					Profile.userSave.inventory.Add(heroUpgradeDefinition, 0, false);
				}
			}
		}

		// Token: 0x06002532 RID: 9522 RVA: 0x000751B8 File Offset: 0x000735B8
		[ConsoleCommand("Inventory.Add")]
		private static void AddToInventory([UpgradeType(HeroUpgradeTypeEnum.Item)] [UpgradeType(HeroUpgradeTypeEnum.Consumable)] HeroUpgradeDefinition upgrade)
		{
			Profile.campaign.inventory.Add(upgrade);
			Profile.userSave.inventory.Add(upgrade, 0, false);
		}

		// Token: 0x06002533 RID: 9523 RVA: 0x000751E1 File Offset: 0x000735E1
		[ConsoleCommand("Inventory.Clear")]
		[DebugSetting("Clear Inventory", DebugSettingLocation.Campaign)]
		private static void ClearInventory()
		{
			Profile.campaign.inventory.Clear();
		}

		// Token: 0x06002534 RID: 9524 RVA: 0x000751F4 File Offset: 0x000735F4
		[DebugSetting("5 Rings to Inventory", "给库存添加5个戒指", DebugSettingLocation.Campaign)]
		private static void Add5RingsInventory()
		{
			HeroUpgradeDefinition heroUpgradeDefinition = ResourceList<HeroUpgradeDefinition>.Get("Hero_Upgrade_Size");
			for (int i = 0; i < 5; i++)
			{
				Profile.campaign.inventory.Add(heroUpgradeDefinition);
			}
			Profile.userSave.inventory.Add(heroUpgradeDefinition, 0, false);
		}

		// Token: 0x06002535 RID: 9525 RVA: 0x00075245 File Offset: 0x00073645
		[ConsoleCommand("")]
		[DebugSetting("Add 100 Coins", "添加100个金币", DebugSettingLocation.Campaign)]
		private static void Add100Coins()
		{
			Profile.campaign.coinBank += 100;
		}

		// Token: 0x06002536 RID: 9526 RVA: 0x0007525A File Offset: 0x0007365A
		[ConsoleCommand("")]
		private static void AddCoins(int coins)
		{
			Profile.campaign.coinBank += coins;
		}

		// Token: 0x06002537 RID: 9527 RVA: 0x00075270 File Offset: 0x00073670
		[DebugSetting("User Stats", DebugSettingLocation.All)]
		private static void DisplayUserStats()
		{
			string text = new ObjectDumper().Dump(Profile.userSave.stats);
		}

		// Token: 0x06002538 RID: 9528 RVA: 0x00075292 File Offset: 0x00073692
		[ConsoleCommand("Save.Checkpoint")]
		[DebugSetting("Save Checkpoint", DebugSettingLocation.Campaign)]
		private static void SaveCheckpoint()
		{
			Profile.SaveCampaign(true);
		}

		// Token: 0x06002539 RID: 9529 RVA: 0x0007529A File Offset: 0x0007369A
		[ConsoleCommand("MetaInventory.Add")]
		public static void AddToMetaInventory(HeroUpgradeDefinition upgrade, int level, bool isStarting)
		{
			Profile.userSave.inventory.Add(upgrade, level, isStarting);
		}

		// Token: 0x0600253A RID: 9530 RVA: 0x000752AE File Offset: 0x000736AE
		[ConsoleCommand("MetaInventory.Clear")]
		[DebugSetting("Meta Inventory : Clear", DebugSettingLocation.All)]
		public static void ClearMetaInventory()
		{
			Profile.userSave.inventory = new MetaInventory();
		}

		// Token: 0x0600253B RID: 9531 RVA: 0x000752C0 File Offset: 0x000736C0
		[ConsoleCommand("MetaInventory.Fill")]
		public static void DebugFillMetaInventory(float ratioSeen = 0.66f)
		{
			List<HeroUpgradeTypeEnum> list = new List<HeroUpgradeTypeEnum>
			{
				HeroUpgradeTypeEnum.Item,
				HeroUpgradeTypeEnum.Trait,
				HeroUpgradeTypeEnum.Consumable
			};
			foreach (HeroUpgradeDefinition heroUpgradeDefinition in ResourceList<HeroUpgradeDefinition>.list)
			{
				if (UnityEngine.Random.value <= ratioSeen)
				{
					Profile.userSave.inventory.Add(heroUpgradeDefinition, UnityEngine.Random.Range(0, heroUpgradeDefinition.numLevels), list.Contains(heroUpgradeDefinition.typeEnum) && UnityEngine.Random.value > 0.5f);
				}
			}
		}

		// Token: 0x0600253C RID: 9532 RVA: 0x00075380 File Offset: 0x00073780
		[ConsoleCommand("MetaInventory.MaxOut")]
		public static void DebugMaxMetaInventory()
		{
			List<HeroUpgradeTypeEnum> list = new List<HeroUpgradeTypeEnum>
			{
				HeroUpgradeTypeEnum.Item,
				HeroUpgradeTypeEnum.Trait,
				HeroUpgradeTypeEnum.Consumable
			};
			foreach (HeroUpgradeDefinition heroUpgradeDefinition in ResourceList<HeroUpgradeDefinition>.list)
			{
				Profile.userSave.inventory.Add(heroUpgradeDefinition, heroUpgradeDefinition.numLevels - 1, list.Contains(heroUpgradeDefinition.typeEnum));
			}
			Profile.SaveUserSave();
		}

		// Token: 0x0600253D RID: 9533 RVA: 0x0007541C File Offset: 0x0007381C
		[ConsoleCommand("MetaInventory.SetNew")]
		public static void SetNew(HeroUpgradeDefinition upgrade, bool isNew = false)
		{
			Profile.userSave.inventory.SetNew(upgrade, isNew);
		}

		// Token: 0x0600253E RID: 9534 RVA: 0x0007542F File Offset: 0x0007382F
		[ConsoleCommand("MetaInventory.UnmarkAllAsNew")]
		public static void MetaInventoryUnmarkAllAsNew()
		{
			Profile.userSave.inventory.UnmarkAllAsNew();
		}

		// Token: 0x0600253F RID: 9535 RVA: 0x00075440 File Offset: 0x00073840
		[ConsoleCommand("MetaInventory.Dump")]
		public static string DumpMetaInventory()
		{
			return new ObjectDumper().Dump(Profile.userSave.inventory);
		}

		// Token: 0x06002540 RID: 9536 RVA: 0x00075456 File Offset: 0x00073856
		[ConsoleCommand("Dump.MetaInventory")]
		public static string DumpMetaInventory2()
		{
			return Profile.DumpMetaInventory();
		}

		// Token: 0x06002541 RID: 9537 RVA: 0x0007545D File Offset: 0x0007385D
		[DebugSetting("Meta Inventory : Populate", DebugSettingLocation.All)]
		public static void DebugFillMetaInventoryDS()
		{
			Profile.DebugFillMetaInventory(0.66f);
		}

		// Token: 0x06002542 RID: 9538 RVA: 0x00075469 File Offset: 0x00073869
		[DebugSetting("Meta Inventory : Print", DebugSettingLocation.All)]
		public static void DumpMetaInventoryDS()
		{
		}

		// Token: 0x06002543 RID: 9539 RVA: 0x0007546C File Offset: 0x0007386C
		[ConsoleCommand("CampaignCompletion.GetCount")]
		public static int GetCompletionCount(Difficulty difficulty)
		{
			int result;
			Profile.userSave.completionCounts.TryGetValue(difficulty, out result);
			return result;
		}

		// Token: 0x06002544 RID: 9540 RVA: 0x00075490 File Offset: 0x00073890
		[ConsoleCommand("CampaignCompletion.SetCount")]
		public static int SetCompletionCount(Difficulty difficulty, int count)
		{
			int num;
			if (Profile.userSave.completionCounts.TryGetValue(difficulty, out num))
			{
				Profile.userSave.completionCounts[difficulty] = count;
			}
			else
			{
				Profile.userSave.completionCounts.Add(difficulty, count);
			}
			return Profile.GetCompletionCount(difficulty);
		}

		// Token: 0x06002545 RID: 9541 RVA: 0x000754E1 File Offset: 0x000738E1
		[ConsoleCommand("CampaignCompletion.ResetAll")]
		private static void ClearCompletionCounts()
		{
			Profile.userSave.completionCounts = new Dictionary<Difficulty, int>();
		}

		// Token: 0x06002546 RID: 9542 RVA: 0x000754F2 File Offset: 0x000738F2
		[DebugSetting("Unlock Very Hard", DebugSettingLocation.All)]
		public static void UnlockVeryHard()
		{
			Profile.userSave.maxDifficulty = Difficulty.VeryHard;
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06002547 RID: 9543 RVA: 0x000754FF File Offset: 0x000738FF
		// (set) Token: 0x06002548 RID: 9544 RVA: 0x0007550E File Offset: 0x0007390E
		[ConsoleCommand("CampaignCompletion.allowVeryHard")]
		private static bool allowVeryHard
		{
			get
			{
				return Profile.userSave.maxDifficulty == Difficulty.VeryHard;
			}
			set
			{
				Profile.userSave.maxDifficulty = ((!value) ? Difficulty.Hard : Difficulty.VeryHard);
			}
		}

		// Token: 0x06002549 RID: 9545 RVA: 0x00075527 File Offset: 0x00073927
		[ConsoleCommand("CampaignCompletion.forceMaxDifficulty")]
		private static void ForceMaxDifficulty(Difficulty difficulty)
		{
			Profile.userSave.maxDifficulty = difficulty;
		}

		// Token: 0x0600254A RID: 9546 RVA: 0x00075534 File Offset: 0x00073934
		// Note: this type is marked as 'beforefieldinit'.
		static Profile()
		{
			Profile.OnProfileUpdated = delegate(Profile.UpdateType A_0)
			{
			};
			Profile.OnSettingsLoaded = null;
		}

		// Token: 0x040017A9 RID: 6057
		private static DebugChannelGroup dbgGroup = new DebugChannelGroup("Profile", EVerbosity.Quiet, 0);

		// Token: 0x040017AA RID: 6058
		private static int _loading = 0;

		// Token: 0x040017AB RID: 6059
		public static UserSave userSave = null;

		// Token: 0x040017AC RID: 6060
		public static MetaSave meta = null;

		// Token: 0x040017AD RID: 6061
		public static CampaignSaveMeta activeCampaignMeta = null;

		// Token: 0x040017AE RID: 6062
		public static CampaignSave campaign = null;

		// Token: 0x040017AF RID: 6063
		public static UserSettings userSettings = null;

		// Token: 0x040017B2 RID: 6066
		[CompilerGenerated]
		private static Action<Callback<MemoryStream>> action;

		// Token: 0x040017B3 RID: 6067
		[CompilerGenerated]
		private static Action<Callback<MemoryStream>> action1;

		// Token: 0x040017B4 RID: 6068
		[CompilerGenerated]
		private static Action<Callback<List<ISaveGameObject>>> action3;

		// Token: 0x040017B5 RID: 6069
		[CompilerGenerated]
		private static Func<MemoryStream, ISaveGameObject> action4;

		// Token: 0x040017B6 RID: 6070
		[CompilerGenerated]
		private static Action<Callback<MemoryStream>> action5;

		// Token: 0x02000596 RID: 1430
		public enum UpdateType
		{
			// Token: 0x040017B9 RID: 6073
			Load,
			// Token: 0x040017BA RID: 6074
			Save
		}
	}
}
