using System;
using System.IO;
using System.Runtime.CompilerServices;
using CS.Platform.Steam.Client.Part;
using CS.Platform.Utils;
using CS.Platform.Utils.Data;
using Steamworks;
using UnityEngine;

namespace CS.Platform.Steam.Client
{
	// Token: 0x0200004D RID: 77
	public class SteamManager : BasePlatformManager
	{
		// Token: 0x06000333 RID: 819 RVA: 0x00010A0C File Offset: 0x0000EE0C
		public SteamManager()
		{
			if (!Packsize.Test())
			{
				CS.Platform.Utils.Debug.LogError("[Steamworks.NET] Packsize Test returned false, the wrong version of Steamworks.NET is being run in this platform.", new object[0]);
			}
			if (!DllCheck.Test())
			{
				CS.Platform.Utils.Debug.LogError("[Steamworks.NET] DllCheck Test returned false, One or more of the Steamworks binaries seems to be the wrong version.", new object[0]);
			}
			if (!this.CheckSteamAppId())
			{
				CS.Platform.Utils.Debug.LogError("[Steamworks] steam_appid.txt exists and could not be deleted", new object[0]);
				if (SteamManager.action == null)
				{
					SteamManager.action = new Action(Application.Quit);
				}
				base.AddToNextUpdate(SteamManager.action);
			}
			if (SteamAPI.RestartAppIfNecessary(SteamManager.DesiredGameID))
			{
				CS.Platform.Utils.Debug.LogError("[Steamworks] Restarting for steam client.", new object[0]);
				if (SteamManager.action1 == null)
				{
					SteamManager.action1 = new Action(Application.Quit);
				}
				base.AddToNextUpdate(SteamManager.action1);
			}
			if (!BasePlatformManager._InitializedPlatformAPI)
			{
				BasePlatformManager._InitializedPlatformAPI = SteamAPI.Init();
				if (!BasePlatformManager._InitializedPlatformAPI)
				{
					CS.Platform.Utils.Debug.LogError("[Steamworks] SteamAPI_Init() failed.", new object[0]);
					base.AddToNextUpdate(delegate
					{
						base.FailEntitlement();
					});
				}
				else
				{
					this._SteamAppID = SteamUtils.GetAppID();
					base.AddToNextUpdate(delegate
					{
						this.CheckEntitlement();
					});
				}
			}
			else
			{
				CS.Platform.Utils.Debug.LogError("[CorePlatform] We're trying to initialise the 'Steam' platform when a platform has already been initialised... we shouldnt be doing this!", new object[0]);
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000334 RID: 820 RVA: 0x00010B4A File Offset: 0x0000EF4A
		// (set) Token: 0x06000335 RID: 821 RVA: 0x00010B51 File Offset: 0x0000EF51
		public static AppId_t DesiredGameID
		{
			get
			{
				return SteamManager._DesiredGameID;
			}
			set
			{
				SteamManager._DesiredGameID = value;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000336 RID: 822 RVA: 0x00010B59 File Offset: 0x0000EF59
		public ulong AppID
		{
			get
			{
				return (ulong)this._SteamAppID.m_AppId;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000337 RID: 823 RVA: 0x00010B67 File Offset: 0x0000EF67
		// (set) Token: 0x06000338 RID: 824 RVA: 0x00010B6E File Offset: 0x0000EF6E
		public static long DiscordLinkID
		{
			get
			{
				return SteamManager._DiscordLinkID;
			}
			set
			{
				SteamManager._DiscordLinkID = value;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000339 RID: 825 RVA: 0x00010B76 File Offset: 0x0000EF76
		public SteamJoining Join
		{
			get
			{
				return this._JoinInfo;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600033A RID: 826 RVA: 0x00010B7E File Offset: 0x0000EF7E
		public SteamUserInfomation UserInfo
		{
			get
			{
				return this._UserInfo;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600033B RID: 827 RVA: 0x00010B86 File Offset: 0x0000EF86
		public SteamAchievementManager Achievement
		{
			get
			{
				return this._achievements;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600033C RID: 828 RVA: 0x00010B8E File Offset: 0x0000EF8E
		public SteamStatisticManager Statistics
		{
			get
			{
				return this._stats;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600033D RID: 829 RVA: 0x00010B96 File Offset: 0x0000EF96
		public SteamUtil Utilities
		{
			get
			{
				return this._Utilities;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600033E RID: 830 RVA: 0x00010B9E File Offset: 0x0000EF9E
		public SteamLobby Lobby
		{
			get
			{
				return this._Lobby;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600033F RID: 831 RVA: 0x00010BA6 File Offset: 0x0000EFA6
		public SteamPostBox PostBox
		{
			get
			{
				return this._PostBox;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000340 RID: 832 RVA: 0x00010BAE File Offset: 0x0000EFAE
		public SteamVoice VoiceBox
		{
			get
			{
				return this._VoiceBox;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000341 RID: 833 RVA: 0x00010BB6 File Offset: 0x0000EFB6
		public SteamAuthenticator Authenticator
		{
			get
			{
				return this._Authenticator;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000342 RID: 834 RVA: 0x00010BBE File Offset: 0x0000EFBE
		public SteamStorage Storage
		{
			get
			{
				return this._Storage;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000343 RID: 835 RVA: 0x00010BC6 File Offset: 0x0000EFC6
		public SteamDLC DLCManager
		{
			get
			{
				return this._DLCManager;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000344 RID: 836 RVA: 0x00010BCE File Offset: 0x0000EFCE
		public CS.Platform.Steam.Client.Part.SteamFriends Friends
		{
			get
			{
				return this._friendManager;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000345 RID: 837 RVA: 0x00010BD6 File Offset: 0x0000EFD6
		public PlatformSystemMessenger Dialog
		{
			get
			{
				return this._dialogUI;
			}
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00010BDE File Offset: 0x0000EFDE
		public override string GetKey()
		{
			return "steam";
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00010BE5 File Offset: 0x0000EFE5
		public override string GetServerPlatformKey()
		{
			return this.GetKey();
		}

		// Token: 0x06000348 RID: 840 RVA: 0x00010BED File Offset: 0x0000EFED
		public override BaseUserInfo GetUserBaseInfo()
		{
			return new BaseUserInfo((ulong)this.UserInfo.LoggedUserID, this.GetKey(), this.GetUserName());
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00010C10 File Offset: 0x0000F010
		private bool CheckSteamAppId()
		{
			if (File.Exists("steam_appid.txt"))
			{
				try
				{
					File.Delete("steam_appid.txt");
				}
				catch (Exception exception)
				{
					UnityEngine.Debug.LogException(exception);
				}
				return !File.Exists("steam_appid.txt");
			}
			return true;
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00010C68 File Offset: 0x0000F068
		private void Awake()
		{
			if (BasePlatformManager._InitializedPlatformAPI)
			{
				this._dialogUI = CS.Platform.Utils.Random.CreateGameBasedUI(base.gameObject);
				this._Utilities = base.gameObject.AddComponent<SteamUtil>();
				this._UserInfo = base.gameObject.AddComponent<SteamUserInfomation>();
				this._achievements = base.gameObject.AddComponent<SteamAchievementManager>();
				this._stats = base.gameObject.AddComponent<SteamStatisticManager>();
				this._JoinInfo = base.gameObject.AddComponent<SteamJoining>();
				this._Lobby = base.gameObject.AddComponent<SteamLobby>();
				this._PostBox = base.gameObject.AddComponent<SteamPostBox>();
				this._VoiceBox = base.gameObject.AddComponent<SteamVoice>();
				this._Authenticator = base.gameObject.AddComponent<SteamAuthenticator>();
				this._Storage = base.gameObject.AddComponent<SteamStorage>();
				this._DLCManager = base.gameObject.AddComponent<SteamDLC>();
				this._friendManager = base.gameObject.AddComponent<CS.Platform.Steam.Client.Part.SteamFriends>();
				this._PostBox.StopMessageThread();
				this._PostBox.WaitForMessageThreadEnd();
				this._PostBox.StartMessageThread();
				this._VoiceBox.StopVoiceThread();
				this._VoiceBox.WaitForVoiceThreadEnd();
				this._VoiceBox.StartVoiceThread();
				BasePlatformManager.Initialized = true;
			}
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00010DA4 File Offset: 0x0000F1A4
		public override void Shutdown()
		{
			if (BasePlatformManager._InitializedPlatformAPI)
			{
				this._Lobby.LeaveLobby();
				this._VoiceBox.ConnectionsEnd();
				this._VoiceBox.StopVoiceThread();
				this._VoiceBox.WaitForVoiceThreadEnd();
				this._PostBox.StopMessageThread();
				this._PostBox.WaitForMessageThreadEnd();
				this._Authenticator.FreeTicket();
				this._Authenticator.DisconnectServer();
				SteamAPI.Shutdown();
				BasePlatformManager._InitializedPlatformAPI = false;
			}
			base.Shutdown();
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00010E24 File Offset: 0x0000F224
		public override void PlatformUpdate()
		{
			object lockSend = this._lockSend;
			lock (lockSend)
			{
				if (BasePlatformManager.Initialized)
				{
					SteamAPI.RunCallbacks();
				}
			}
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00010E6C File Offset: 0x0000F26C
		private void CheckEntitlement()
		{
			object entitlementLock = BasePlatformManager._entitlementLock;
			lock (entitlementLock)
			{
				if (this._SteamAppID == SteamManager.DesiredGameID)
				{
					base.PassEntitlement();
				}
				else
				{
					base.FailEntitlement();
				}
				CS.Platform.Utils.Debug.LogWarning("[CP] FINALISING: " + ((!BasePlatformManager._entitlementChecked) ? "0" : "1") + ((!BasePlatformManager._userIsEntitled) ? "0" : "1") + ((!(this._SteamAppID == SteamManager.DesiredGameID)) ? "0" : "1"), new object[0]);
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600034E RID: 846 RVA: 0x00010F34 File Offset: 0x0000F334
		// (set) Token: 0x0600034F RID: 847 RVA: 0x00010F41 File Offset: 0x0000F341
		public override bool StorageDisabled
		{
			get
			{
				return this.Storage.StorageDisabled;
			}
			set
			{
				this.Storage.StorageDisabled = value;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000350 RID: 848 RVA: 0x00010F4F File Offset: 0x0000F34F
		public override bool IsLocalSaving
		{
			get
			{
				return this.Storage.IsSaving;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000351 RID: 849 RVA: 0x00010F5C File Offset: 0x0000F35C
		public override bool IsLocalLoading
		{
			get
			{
				return this.Storage.IsLoading;
			}
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00010F69 File Offset: 0x0000F369
		public override string[] Loaded()
		{
			return this.Storage.LoadedFiles();
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00010F76 File Offset: 0x0000F376
		public override int Loaded(string file)
		{
			return this.Storage.HasData(file);
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00010F84 File Offset: 0x0000F384
		public override int Load(string file, ref byte[] data, bool copy = true, bool safe = true)
		{
			if (BasePlatformManager.Initialized)
			{
				return this._Storage.GetLoadedData(file, ref data, copy, safe);
			}
			return 0;
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00010FA2 File Offset: 0x0000F3A2
		public override int Load(string file, ref string data, bool safe = true)
		{
			if (BasePlatformManager.Initialized)
			{
				return this._Storage.GetLoadedData(file, ref data, safe);
			}
			return 0;
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00010FBE File Offset: 0x0000F3BE
		public override bool Save(string file, byte[] data, int size, bool copy = false, bool safe = true)
		{
			return this.Storage.AddSaveData(file, data, size, copy, safe);
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00010FD2 File Offset: 0x0000F3D2
		public override bool Save(string file, string data, bool safe = true)
		{
			return this.Storage.AddSaveData(file, data, safe);
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00010FE2 File Offset: 0x0000F3E2
		public override void SendSavedData()
		{
			this._Storage.StartSaveData();
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00010FEF File Offset: 0x0000F3EF
		public override void LoadSavedData(string[] load)
		{
			this._Storage.StartLoadData(load);
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00010FFD File Offset: 0x0000F3FD
		public override void ForgetFile(string file)
		{
			this._Storage.Forget(file);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0001100B File Offset: 0x0000F40B
		public override bool HasDLC(string dlcKey)
		{
			return BasePlatformManager.HasEntitlement && this.DLCManager.HasDLC(dlcKey);
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00011025 File Offset: 0x0000F425
		public override bool CanShowDLCStore(string dlcKey)
		{
			return this.DLCManager.CanShowDLCStore(dlcKey);
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00011033 File Offset: 0x0000F433
		public override void ShowDLCStore(string dlcKey)
		{
			this.DLCManager.ShowDLCStore(dlcKey);
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600035E RID: 862 RVA: 0x00011042 File Offset: 0x0000F442
		public override ulong GetUserID
		{
			get
			{
				return (ulong)this._UserInfo.LoggedUserID;
			}
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00011054 File Offset: 0x0000F454
		public override string GetUserName()
		{
			return Steamworks.SteamFriends.GetPersonaName();
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0001105B File Offset: 0x0000F45B
		public override void OpenUserProfile(BaseUserInfo userInfo)
		{
			if (userInfo.platformKey == this.GetKey())
			{
				Steamworks.SteamFriends.ActivateGameOverlayToUser("steamid", (CSteamID)userInfo.userID);
			}
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0001108A File Offset: 0x0000F48A
		public override void SetRichPresenceDetails(string presenceKey = null, string[] parameter = null)
		{
			this.Utilities.SetRichPresenceDetails(presenceKey, parameter);
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00011099 File Offset: 0x0000F499
		public override void SetRichPresenceStatus(string statusKey = null)
		{
			this.Utilities.SetRichPresenceStatus(statusKey);
		}

		// Token: 0x06000363 RID: 867 RVA: 0x000110A7 File Offset: 0x0000F4A7
		public override void SendRichPresence()
		{
			this.Utilities.SendRichPresence();
		}

		// Token: 0x06000364 RID: 868 RVA: 0x000110B4 File Offset: 0x0000F4B4
		public override void SetRichPresenceLargeImage(string imageKey = null)
		{
			this.Utilities.SetRichPresenceLargeImate(imageKey);
		}

		// Token: 0x06000365 RID: 869 RVA: 0x000110C2 File Offset: 0x0000F4C2
		public override void SetRichPresenceSmallImage(string imageKey = null)
		{
			this.Utilities.SetRichPresenceSmallImage(imageKey);
		}

		// Token: 0x06000366 RID: 870 RVA: 0x000110D0 File Offset: 0x0000F4D0
		public override void UnlockAchievement(string achievementName)
		{
			this.Achievement.UnlockAchievement(achievementName);
		}

		// Token: 0x06000367 RID: 871 RVA: 0x000110DE File Offset: 0x0000F4DE
		public override bool IsAchievementUnlocked(string achievementName)
		{
			return this.Achievement.GetAchievement(achievementName);
		}

		// Token: 0x06000368 RID: 872 RVA: 0x000110EC File Offset: 0x0000F4EC
		public override void IncrementStatistic(string statisticName, float amount)
		{
			this.Statistics.ChangeStatistic(statisticName, amount);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x000110FB File Offset: 0x0000F4FB
		public override void SetStatistic(string statisticName, float value)
		{
			this.Statistics.SetStatistic(statisticName, value);
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0001110C File Offset: 0x0000F50C
		public override float GetStatistic(string statisticName)
		{
			float result = 0f;
			this.Statistics.GetStatistic(statisticName, out result);
			return result;
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0001112F File Offset: 0x0000F52F
		public override void ShowWebsite(string webURL)
		{
			if (BasePlatformManager.Initialized)
			{
				Steamworks.SteamFriends.ActivateGameOverlayToWebPage(webURL);
			}
			else
			{
				this.ShowWebsite(webURL);
			}
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0001114D File Offset: 0x0000F54D
		public override void ShowGameStore()
		{
			if (BasePlatformManager.Initialized)
			{
				Steamworks.SteamFriends.ActivateGameOverlayToStore(this._SteamAppID, EOverlayToStoreFlag.k_EOverlayToStoreFlag_None);
			}
			else
			{
				this.ShowWebsite("http://store.steampowered.com/app/285900/");
			}
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00011175 File Offset: 0x0000F575
		public override void ShowGameForum()
		{
			if (BasePlatformManager.Initialized)
			{
				Steamworks.SteamFriends.ActivateGameOverlayToWebPage("https://steamcommunity.com/app/285900/discussions/");
			}
			else
			{
				this.ShowWebsite("https://steamcommunity.com/app/285900/discussions/");
			}
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0001119B File Offset: 0x0000F59B
		public override void ShowGameSupport()
		{
			if (BasePlatformManager.Initialized)
			{
				Steamworks.SteamFriends.ActivateGameOverlayToWebPage("https://steamcommunity.com/app/285900/discussions/");
			}
			else
			{
				this.ShowWebsite("https://steamcommunity.com/app/285900/discussions/");
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600036F RID: 879 RVA: 0x000111C1 File Offset: 0x0000F5C1
		public override Guid CurrentLobbyID
		{
			get
			{
				return this._Lobby.CurrentLobbyID;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000370 RID: 880 RVA: 0x000111CE File Offset: 0x0000F5CE
		public override bool IsInLobby
		{
			get
			{
				return this._Lobby.InLobby;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000371 RID: 881 RVA: 0x000111DB File Offset: 0x0000F5DB
		public override bool IsLobbyHost
		{
			get
			{
				return this._Lobby.LobbyHost;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000372 RID: 882 RVA: 0x000111E8 File Offset: 0x0000F5E8
		public override bool IsJoiningLobby
		{
			get
			{
				return this._Lobby.JoiningLobby;
			}
		}

		// Token: 0x06000373 RID: 883 RVA: 0x000111F5 File Offset: 0x0000F5F5
		public override void CreateLobby(LOBBY_TYPE lobbyType, uint maxSlots)
		{
			this._Lobby.CreateLobby(lobbyType, maxSlots);
		}

		// Token: 0x06000374 RID: 884 RVA: 0x00011204 File Offset: 0x0000F604
		public override void LeaveLobby()
		{
			this._Lobby.LeaveLobby();
		}

		// Token: 0x06000375 RID: 885 RVA: 0x00011211 File Offset: 0x0000F611
		public override void JoinLobby(Guid id)
		{
			this._Lobby.JoinLobby(id);
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0001121F File Offset: 0x0000F61F
		public override bool GetLobbyData(string dataKey, out string dataOut)
		{
			return this._Lobby.GetLobbyData(dataKey, out dataOut);
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0001122E File Offset: 0x0000F62E
		public override bool SetLobbyData(string dataKey, string dataIn)
		{
			return this._Lobby.SetLobbyData(dataKey, dataIn);
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0001123D File Offset: 0x0000F63D
		public override void SendLobbyData()
		{
			this._Lobby.SendLobbyData();
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0001124A File Offset: 0x0000F64A
		public override BaseUserInfo LobbyHost()
		{
			return this._Lobby.GetLobbyHost();
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00011257 File Offset: 0x0000F657
		public override bool GetLobbyInfo(ref LobbyInformation lobbyInfo)
		{
			return this._Lobby.GetLobbyInfo(ref lobbyInfo);
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600037B RID: 891 RVA: 0x00011265 File Offset: 0x0000F665
		public override int TotalInLobby
		{
			get
			{
				return this._Lobby.TotalInLobby;
			}
		}

		// Token: 0x0600037C RID: 892 RVA: 0x00011272 File Offset: 0x0000F672
		public override bool GetLobbyUser(int index, ref BaseUserInfo userData)
		{
			return this._Lobby.GetLobbyUser(index, ref userData);
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00011281 File Offset: 0x0000F681
		public override bool LobbyKickUser(BaseUserInfo info)
		{
			return this._Lobby.KickUser(info);
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600037E RID: 894 RVA: 0x0001128F File Offset: 0x0000F68F
		public override bool HasLobbyInvite
		{
			get
			{
				return this._JoinInfo.HasLobby;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600037F RID: 895 RVA: 0x0001129C File Offset: 0x0000F69C
		public override string LobbyInviteHostName
		{
			get
			{
				return this._JoinInfo.InvitedHost;
			}
		}

		// Token: 0x06000380 RID: 896 RVA: 0x000112A9 File Offset: 0x0000F6A9
		public override bool JoinInviteLobby()
		{
			return this._JoinInfo.JoinInvitedLobby();
		}

		// Token: 0x06000381 RID: 897 RVA: 0x000112B6 File Offset: 0x0000F6B6
		public override bool ShowPlatformLobbyMenu()
		{
			return this._Lobby.ShowPlatformLobbyMenu();
		}

		// Token: 0x06000382 RID: 898 RVA: 0x000112C3 File Offset: 0x0000F6C3
		public override bool SendLobbyMessage(BaseUserInfo user, PlatformMessageBase message)
		{
			return this._Lobby.SendLobbyMessage(user, message);
		}

		// Token: 0x06000383 RID: 899 RVA: 0x000112D2 File Offset: 0x0000F6D2
		public override bool SendLobbyMessage(PlatformMessageBase message)
		{
			return this._Lobby.SendLobbyMessage(message);
		}

		// Token: 0x06000384 RID: 900 RVA: 0x000112E0 File Offset: 0x0000F6E0
		public override bool UserIsInLobby(BaseUserInfo user)
		{
			return this._Lobby.UserIsInLobby(user);
		}

		// Token: 0x06000385 RID: 901 RVA: 0x000112EE File Offset: 0x0000F6EE
		public override bool UserIsLobbyHost(BaseUserInfo user)
		{
			return this._Lobby.UserIsLobbyHost(user);
		}

		// Token: 0x06000386 RID: 902 RVA: 0x000112FC File Offset: 0x0000F6FC
		public override bool InviteUserToLobby(BaseUserInfo user)
		{
			return user.platformKey == base.Key && this.Lobby.InviteUserToParty(user.userID);
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000387 RID: 903 RVA: 0x00011329 File Offset: 0x0000F729
		// (set) Token: 0x06000388 RID: 904 RVA: 0x00011336 File Offset: 0x0000F736
		public override bool VoiceEnabled
		{
			get
			{
				return this.VoiceBox.VoipEnabled;
			}
			set
			{
				this.VoiceBox.VoipEnabled = value;
			}
		}

		// Token: 0x06000389 RID: 905 RVA: 0x00011344 File Offset: 0x0000F744
		public override void AddVoiceConnection(BaseUserInfo userInfo)
		{
			if (userInfo.platformKey == this.GetKey())
			{
				this.VoiceBox.ConnectionAdd(userInfo.userID);
			}
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0001136F File Offset: 0x0000F76F
		public override void RemoveVoiceConnection(BaseUserInfo userInfo)
		{
			if (userInfo.platformKey == this.GetKey())
			{
				this.VoiceBox.ConnectionAdd(userInfo.userID);
			}
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0001139A File Offset: 0x0000F79A
		public override void ClearVoiceConectionsClear()
		{
			this.VoiceBox.ConnectionsEnd();
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600038C RID: 908 RVA: 0x000113A7 File Offset: 0x0000F7A7
		public override bool VoiceActive
		{
			get
			{
				return this.VoiceBox.VoiceRunning;
			}
		}

		// Token: 0x0600038D RID: 909 RVA: 0x000113B4 File Offset: 0x0000F7B4
		public override void StartVoice()
		{
			this.VoiceBox.StartVoice();
		}

		// Token: 0x0600038E RID: 910 RVA: 0x000113C1 File Offset: 0x0000F7C1
		public override void StopVoice()
		{
			this.VoiceBox.StopVoice();
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600038F RID: 911 RVA: 0x000113CE File Offset: 0x0000F7CE
		// (set) Token: 0x06000390 RID: 912 RVA: 0x000113DB File Offset: 0x0000F7DB
		public override bool DefaultVoiceMute
		{
			get
			{
				return this.VoiceBox.DefaultMute;
			}
			set
			{
				this.VoiceBox.DefaultMute = value;
			}
		}

		// Token: 0x06000391 RID: 913 RVA: 0x000113E9 File Offset: 0x0000F7E9
		public override void SetVoiceMuted(BaseUserInfo userInfo, bool mute)
		{
			if (userInfo.platformKey == this.GetKey())
			{
				this.VoiceBox.ConnectionMute(userInfo.userID, mute);
				PlatformEvents.VoiceMuteStateChanged(userInfo, mute);
			}
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0001141C File Offset: 0x0000F81C
		public override bool GetVoiceMuted(BaseUserInfo userInfo)
		{
			return userInfo.platformKey == this.GetKey() && this.VoiceBox.ConnectionMuted(userInfo.userID);
		}

		// Token: 0x06000393 RID: 915 RVA: 0x00011449 File Offset: 0x0000F849
		public override void SetVoiceMutes(bool mute)
		{
			this.VoiceBox.SetVoiceMutes(mute);
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000394 RID: 916 RVA: 0x00011457 File Offset: 0x0000F857
		// (set) Token: 0x06000395 RID: 917 RVA: 0x00011464 File Offset: 0x0000F864
		public override float DefaultVoiceVolume
		{
			get
			{
				return this.VoiceBox.DefaultVolume;
			}
			set
			{
				this.VoiceBox.DefaultVolume = value;
			}
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00011472 File Offset: 0x0000F872
		public override void SetVoiceVolumes(float volume)
		{
			this.VoiceBox.SetVoiceVolumes(volume);
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00011480 File Offset: 0x0000F880
		public override float GetVoiceVolume(BaseUserInfo userInfo)
		{
			if (userInfo.platformKey == this.GetKey())
			{
				return this.VoiceBox.GetVoiceVolume(userInfo.userID);
			}
			return 1f;
		}

		// Token: 0x06000398 RID: 920 RVA: 0x000114B1 File Offset: 0x0000F8B1
		public override void SetVoiceVolume(BaseUserInfo userInfo, float volume)
		{
			if (userInfo.platformKey == this.GetKey())
			{
				this.VoiceBox.SetVoiceVolume(userInfo.userID, volume);
			}
		}

		// Token: 0x06000399 RID: 921 RVA: 0x000114DD File Offset: 0x0000F8DD
		public override bool SetToUserImage(BaseUserInfo userInfo, ref Texture2D terxtureSetting)
		{
			return !(userInfo.platformKey == this.GetKey()) || this.Utilities.LoadProfileImage((CSteamID)userInfo.userID, ref terxtureSetting);
		}

		// Token: 0x0600039A RID: 922 RVA: 0x00011510 File Offset: 0x0000F910
		public override void StartedPlayingWith(BaseUserInfo userInfo)
		{
			if (userInfo.platformKey == this.GetKey())
			{
				this.Utilities.AddPlayerToPlayedList((CSteamID)userInfo.userID);
				this.Utilities.WantsUserInfo((CSteamID)userInfo.userID, true);
			}
		}

		// Token: 0x0600039B RID: 923 RVA: 0x00011563 File Offset: 0x0000F963
		public override bool ShowSystemMessageYESNO(string body, Action yesAction, Action noAction)
		{
			if (this.Dialog != null)
			{
				this.Dialog.ShowYESNO(body, yesAction, noAction);
			}
			return this.Dialog != null;
		}

		// Token: 0x0600039C RID: 924 RVA: 0x00011590 File Offset: 0x0000F990
		public override bool ShowSystemMessageOK(string body, Action action)
		{
			if (this.Dialog != null)
			{
				this.Dialog.ShowOK(body, action);
			}
			return this.Dialog != null;
		}

		// Token: 0x0600039D RID: 925 RVA: 0x000115BC File Offset: 0x0000F9BC
		public override bool IsShowingMessage()
		{
			return this.Dialog != null && this.Dialog.ShowingMessage;
		}

		// Token: 0x0600039E RID: 926 RVA: 0x000115DC File Offset: 0x0000F9DC
		public override bool SendMessage(BaseUserInfo userInfo, PlatformMessageBase message, bool reliable)
		{
			if (userInfo.platformKey == this.GetKey())
			{
				this._PostBox.SendMessage(userInfo.userID, message, reliable);
				return true;
			}
			return false;
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0001160C File Offset: 0x0000FA0C
		public override bool MessageConnectionReady(BaseUserInfo userInfo)
		{
			return true;
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x0001160F File Offset: 0x0000FA0F
		public override int MaxLocalPlayers
		{
			get
			{
				return 8;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x00011612 File Offset: 0x0000FA12
		public override bool GUIActive
		{
			get
			{
				return this._Utilities.GUIActive;
			}
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0001161F File Offset: 0x0000FA1F
		public override bool UpdateFriendsList()
		{
			return this.Friends.UpdateList();
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x0001162C File Offset: 0x0000FA2C
		public override BaseUserInfo[] FriendsList
		{
			get
			{
				return this.Friends.Friends;
			}
		}

		// Token: 0x04000164 RID: 356
		private static AppId_t _DesiredGameID = AppId_t.Invalid;

		// Token: 0x04000165 RID: 357
		private static long _DiscordLinkID = -1L;

		// Token: 0x04000166 RID: 358
		private AppId_t _SteamAppID;

		// Token: 0x04000167 RID: 359
		private SteamJoining _JoinInfo;

		// Token: 0x04000168 RID: 360
		private SteamUserInfomation _UserInfo;

		// Token: 0x04000169 RID: 361
		private SteamAchievementManager _achievements;

		// Token: 0x0400016A RID: 362
		private SteamStatisticManager _stats;

		// Token: 0x0400016B RID: 363
		private SteamUtil _Utilities;

		// Token: 0x0400016C RID: 364
		private SteamLobby _Lobby;

		// Token: 0x0400016D RID: 365
		private SteamPostBox _PostBox;

		// Token: 0x0400016E RID: 366
		private SteamVoice _VoiceBox;

		// Token: 0x0400016F RID: 367
		private SteamAuthenticator _Authenticator;

		// Token: 0x04000170 RID: 368
		private SteamStorage _Storage;

		// Token: 0x04000171 RID: 369
		private SteamDLC _DLCManager;

		// Token: 0x04000172 RID: 370
		private CS.Platform.Steam.Client.Part.SteamFriends _friendManager;

		// Token: 0x04000173 RID: 371
		private PlatformSystemMessenger _dialogUI;

		// Token: 0x04000174 RID: 372
		[CompilerGenerated]
		private static Action action;

		// Token: 0x04000175 RID: 373
		[CompilerGenerated]
		private static Action action1;
	}
}
