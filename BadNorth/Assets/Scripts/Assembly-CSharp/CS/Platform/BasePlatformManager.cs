using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using CS.Platform.Utils;
using CS.Platform.Utils.Data;
using UnityEngine;

namespace CS.Platform
{
	// Token: 0x0200002D RID: 45
	public abstract class BasePlatformManager : MonoBehaviour
	{
		// Token: 0x060000EE RID: 238 RVA: 0x00006416 File Offset: 0x00004816
		public BasePlatformManager()
		{
			BasePlatformManager.Instance = this;
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060000EF RID: 239 RVA: 0x0000643C File Offset: 0x0000483C
		// (remove) Token: 0x060000F0 RID: 240 RVA: 0x00006474 File Offset: 0x00004874
		
		private event Action _CallNextUpdate;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060000F1 RID: 241 RVA: 0x000064AC File Offset: 0x000048AC
		// (remove) Token: 0x060000F2 RID: 242 RVA: 0x000064E4 File Offset: 0x000048E4
		
		private event Action _CallThisUpdate;

		// Token: 0x060000F3 RID: 243 RVA: 0x0000651C File Offset: 0x0000491C
		public void AddToNextUpdate(Action doThisNextUpdate)
		{
			object lockSend = this._lockSend;
			lock (lockSend)
			{
				this._CallNextUpdate += doThisNextUpdate;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x0000655C File Offset: 0x0000495C
		public string Key
		{
			get
			{
				return this.GetKey();
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00006564 File Offset: 0x00004964
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x0000656B File Offset: 0x0000496B
		public static BasePlatformManager Instance
		{
			get
			{
				return BasePlatformManager._Instance;
			}
			protected set
			{
				if (BasePlatformManager._Instance != null)
				{
					CS.Platform.Utils.Debug.LogError("[CorePlatform] CorePlatform Has Aready Been Set!!!", new object[0]);
				}
				else
				{
					BasePlatformManager._Instance = value;
				}
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00006598 File Offset: 0x00004998
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x0000659F File Offset: 0x0000499F
		public static bool Initialized
		{
			get
			{
				return BasePlatformManager._Initialized;
			}
			protected set
			{
				BasePlatformManager._Initialized = value;
				if (BasePlatformManager._Initialized)
				{
					PlatformEvents.PlatformInitialized();
				}
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x000065B6 File Offset: 0x000049B6
		public static bool GameSetup
		{
			get
			{
				return BasePlatformManager._gameSetup;
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000065C0 File Offset: 0x000049C0
		public static void StartGame()
		{
			if (!BasePlatformManager._gameSetup)
			{
				BasePlatformManager._gameSetup = true;
				CS.Platform.Utils.Debug.LogInfo("[CorePlatform] Game Started | Platform ready: {0} | Entitlement: {1}", new object[]
				{
					BasePlatformManager._Instance != null,
					BasePlatformManager.HasEntitlement
				});
				if (BasePlatformManager._Instance != null && BasePlatformManager.HasEntitlement)
				{
					if (BasePlatformManager._Instance.OnPreGameStart != null)
					{
						BasePlatformManager._Instance.OnPreGameStart();
					}
					PlatformEvents.GameSetup();
				}
			}
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0000664C File Offset: 0x00004A4C
		public static void StopGame()
		{
			if (BasePlatformManager._gameSetup)
			{
				BasePlatformManager._gameSetup = false;
				CS.Platform.Utils.Debug.LogInfo("[CorePlatform] Game Stopped | Platform ready: {0} | Entitlement: {1}", new object[]
				{
					BasePlatformManager._Instance != null,
					BasePlatformManager.HasEntitlement
				});
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060000FC RID: 252 RVA: 0x0000669C File Offset: 0x00004A9C
		// (remove) Token: 0x060000FD RID: 253 RVA: 0x000066D4 File Offset: 0x00004AD4
		
		public event Action OnPreGameStart;

		// Token: 0x060000FE RID: 254 RVA: 0x0000670A File Offset: 0x00004B0A
		public void AddBlocker(string key)
		{
			if (!this._blockers.Contains(key))
			{
				this._blockers.Add(key);
			}
			this._blockerFreeCall = null;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00006730 File Offset: 0x00004B30
		public void RemoveBlocker(string key)
		{
			this._blockers.Remove(key);
			if (!this.IsLoadBlocked)
			{
				if (BasePlatformManager.action == null)
				{
					BasePlatformManager.action = new Action(PlatformEvents.PlatformLoadUnblocked);
				}
				this._blockerFreeCall = BasePlatformManager.action;
				this.AddToNextUpdate(new Action(this.RecheckBlockComplete));
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000100 RID: 256 RVA: 0x0000678A File Offset: 0x00004B8A
		public bool IsLoadBlocked
		{
			get
			{
				return this._blockers.Count != 0;
			}
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000067A0 File Offset: 0x00004BA0
		private void RecheckBlockComplete()
		{
			if (this._blockerFreeCall != null)
			{
				if (this.IsLoadBlocked)
				{
					this._blockerFreeCall = null;
				}
				else
				{
					Action blockerFreeCall = this._blockerFreeCall;
					this._blockerFreeCall = null;
					blockerFreeCall();
				}
			}
		}

		// Token: 0x06000102 RID: 258
		public abstract string GetKey();

		// Token: 0x06000103 RID: 259
		public abstract string GetServerPlatformKey();

		// Token: 0x06000104 RID: 260
		public abstract BaseUserInfo GetUserBaseInfo();

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000105 RID: 261 RVA: 0x000067E3 File Offset: 0x00004BE3
		public virtual int MainUserID
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000067E6 File Offset: 0x00004BE6
		public virtual void Shutdown()
		{
			BasePlatformManager._Instance = null;
			BasePlatformManager.Initialized = false;
			CS.Platform.Utils.Debug.LogWarning("[CorePlatform] Shutdown", new object[0]);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00006804 File Offset: 0x00004C04
		private void OnApplicationQuit()
		{
			this.Shutdown();
		}

		// Token: 0x06000108 RID: 264 RVA: 0x0000680C File Offset: 0x00004C0C
		private void OnDestroy()
		{
			this.Shutdown();
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00006814 File Offset: 0x00004C14
		public virtual void PlatformUpdate()
		{
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00006818 File Offset: 0x00004C18
		public void Update()
		{
			object lockSend = this._lockSend;
			lock (lockSend)
			{
				this.PlatformUpdate();
				this._CallThisUpdate = this._CallNextUpdate;
				this._CallNextUpdate = null;
			}
			if (this._CallThisUpdate != null)
			{
				try
				{
					this._CallThisUpdate();
				}
				catch (Exception ex)
				{
					CS.Platform.Utils.Debug.LogError("[BASE] Next Update Exception | Message: {0} | Site: {1}", new object[]
					{
						ex.ToString(),
						ex.TargetSite
					});
				}
				this._CallThisUpdate = null;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600010B RID: 267 RVA: 0x000068C0 File Offset: 0x00004CC0
		public static bool EntitlementChecked
		{
			get
			{
				object entitlementLock = BasePlatformManager._entitlementLock;
				bool entitlementChecked;
				lock (entitlementLock)
				{
					entitlementChecked = BasePlatformManager._entitlementChecked;
				}
				return entitlementChecked;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600010C RID: 268 RVA: 0x000068FC File Offset: 0x00004CFC
		public static bool HasEntitlement
		{
			get
			{
				object entitlementLock = BasePlatformManager._entitlementLock;
				bool userIsEntitled;
				lock (entitlementLock)
				{
					userIsEntitled = BasePlatformManager._userIsEntitled;
				}
				return userIsEntitled;
			}
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00006938 File Offset: 0x00004D38
		public void FailEntitlement()
		{
			object entitlementLock = BasePlatformManager._entitlementLock;
			lock (entitlementLock)
			{
				if (BasePlatformManager._userIsEntitled || !BasePlatformManager._entitlementChecked)
				{
					BasePlatformManager._entitlementChecked = true;
					BasePlatformManager._userIsEntitled = false;
					this.AddToNextUpdate(delegate
					{
						PlatformEvents.EntitlementChanged(false);
					});
					if (BasePlatformManager.action1 == null)
					{
						BasePlatformManager.action1 = new Action(Application.Quit);
					}
					this.AddToNextUpdate(BasePlatformManager.action1);
				}
			}
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000069D4 File Offset: 0x00004DD4
		public void PassEntitlement()
		{
			object entitlementLock = BasePlatformManager._entitlementLock;
			lock (entitlementLock)
			{
				if (!BasePlatformManager._userIsEntitled || !BasePlatformManager._entitlementChecked)
				{
					BasePlatformManager._entitlementChecked = true;
					BasePlatformManager._userIsEntitled = true;
					this.AddToNextUpdate(delegate
					{
						PlatformEvents.EntitlementChanged(true);
					});
					if (BasePlatformManager._gameSetup)
					{
						if (BasePlatformManager._Instance.OnPreGameStart != null)
						{
							BasePlatformManager._Instance.OnPreGameStart();
						}
						PlatformEvents.GameSetup();
					}
				}
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00006A7C File Offset: 0x00004E7C
		// (set) Token: 0x06000110 RID: 272 RVA: 0x00006A7F File Offset: 0x00004E7F
		public virtual bool StorageDisabled
		{
			get
			{
				return true;
			}
			set
			{
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00006A81 File Offset: 0x00004E81
		public virtual bool IsLocalSaving
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00006A84 File Offset: 0x00004E84
		public virtual bool IsLocalLoading
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00006A87 File Offset: 0x00004E87
		public virtual string[] Loaded()
		{
			return null;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00006A8A File Offset: 0x00004E8A
		public virtual int Loaded(string file)
		{
			return 0;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00006A8D File Offset: 0x00004E8D
		public virtual int Load(string file, ref byte[] data, bool copy = false, bool safe = true)
		{
			data = null;
			return 0;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00006A93 File Offset: 0x00004E93
		public virtual int Load(string file, ref string data, bool safe = true)
		{
			return 0;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00006A96 File Offset: 0x00004E96
		public virtual bool Save(string file, byte[] data, int size, bool copy = false, bool safe = true)
		{
			return true;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00006A99 File Offset: 0x00004E99
		public virtual bool Save(string file, string data, bool safe = true)
		{
			return true;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00006A9C File Offset: 0x00004E9C
		public virtual void SendSavedData()
		{
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00006A9E File Offset: 0x00004E9E
		public virtual void LoadSavedData(string[] files)
		{
			if (BasePlatformManager.action2 == null)
			{
				BasePlatformManager.action2 = new Action(PlatformEvents.LoadLocalComplete);
			}
			this.AddToNextUpdate(BasePlatformManager.action2);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00006AC3 File Offset: 0x00004EC3
		public virtual void ForgetFile(string file)
		{
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00006AC5 File Offset: 0x00004EC5
		public virtual bool HasDLC(string dlcKey)
		{
			return false;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00006AC8 File Offset: 0x00004EC8
		public bool HasDLC(string[] dlcKeys)
		{
			if (dlcKeys != null)
			{
				for (int i = 0; i < dlcKeys.Length; i++)
				{
					if (!string.IsNullOrEmpty(dlcKeys[i]) && !this.HasDLC(dlcKeys[i]))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00006B10 File Offset: 0x00004F10
		public bool HasDLC(List<string> dlcKeys)
		{
			if (dlcKeys != null)
			{
				for (int i = 0; i < dlcKeys.Count; i++)
				{
					if (!string.IsNullOrEmpty(dlcKeys[i]) && !this.HasDLC(dlcKeys[i]))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00006B60 File Offset: 0x00004F60
		public virtual bool CanShowDLCStore(string dlcKey)
		{
			return false;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00006B63 File Offset: 0x00004F63
		public virtual void ShowDLCStore(string dlcKey)
		{
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00006B65 File Offset: 0x00004F65
		public virtual ulong GetUserID
		{
			get
			{
				return 0UL;
			}
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00006B69 File Offset: 0x00004F69
		public virtual string GetUserName()
		{
			return string.Empty;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00006B70 File Offset: 0x00004F70
		public virtual string GetUserName(int controller)
		{
			return string.Empty;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00006B77 File Offset: 0x00004F77
		public virtual ulong GetUserOnlineID(int controller)
		{
			return this.GetUserID;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00006B7F File Offset: 0x00004F7F
		public virtual void OpenUserProfile(BaseUserInfo userInfo)
		{
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00006B81 File Offset: 0x00004F81
		public virtual bool DynamicUsers
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00006B84 File Offset: 0x00004F84
		public virtual bool UserJoined(int controller)
		{
			return true;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00006B87 File Offset: 0x00004F87
		public virtual void UserLeave(int controller)
		{
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00006B89 File Offset: 0x00004F89
		public virtual void ClearMainUser()
		{
			BasePlatformManager.StopGame();
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00006B90 File Offset: 0x00004F90
		public virtual void SetRichPresenceDetails(string presenceKey = null, string[] parameter = null)
		{
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00006B92 File Offset: 0x00004F92
		public virtual void SetRichPresenceStatus(string statusKey = null)
		{
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00006B94 File Offset: 0x00004F94
		public virtual void SetRichPresenceLargeImage(string imageKey = null)
		{
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00006B96 File Offset: 0x00004F96
		public virtual void SetRichPresenceSmallImage(string imageKey = null)
		{
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00006B98 File Offset: 0x00004F98
		public void ClearPresence()
		{
			this.SetRichPresenceDetails(null, null);
			this.SetRichPresenceStatus(null);
			this.SetRichPresenceLargeImage(null);
			this.SetRichPresenceSmallImage(null);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00006BB7 File Offset: 0x00004FB7
		public virtual void SendRichPresence()
		{
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00006BB9 File Offset: 0x00004FB9
		public virtual void UnlockAchievement(string achievementName)
		{
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00006BBB File Offset: 0x00004FBB
		public virtual bool IsAchievementUnlocked(string achievementName)
		{
			return false;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00006BBE File Offset: 0x00004FBE
		public virtual void ShowAchievementsUI()
		{
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00006BC0 File Offset: 0x00004FC0
		public bool IsAchievementUnlocked(string[] achievementNames)
		{
			if (achievementNames != null)
			{
				for (int i = 0; i < achievementNames.Length; i++)
				{
					if (!string.IsNullOrEmpty(achievementNames[i]) && !this.IsAchievementUnlocked(achievementNames[i]))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00006C08 File Offset: 0x00005008
		public bool IsAchievementUnlocked(List<string> achievementNames)
		{
			if (achievementNames != null)
			{
				for (int i = 0; i < achievementNames.Count; i++)
				{
					if (!string.IsNullOrEmpty(achievementNames[i]) && !this.IsAchievementUnlocked(achievementNames[i]))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00006C58 File Offset: 0x00005058
		public virtual void IncrementStatistic(string statisticName, float amount)
		{
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00006C5A File Offset: 0x0000505A
		public virtual void SetStatistic(string statisticName, float value)
		{
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00006C5C File Offset: 0x0000505C
		public virtual float GetStatistic(string statisticName)
		{
			return 0f;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00006C63 File Offset: 0x00005063
		public virtual void ShowWebsite(string address)
		{
			Application.OpenURL(address);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00006C6B File Offset: 0x0000506B
		public virtual void ShowGameStore()
		{
			Application.OpenURL("https://coatsink.com/");
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00006C77 File Offset: 0x00005077
		public virtual void ShowGameForum()
		{
			Application.OpenURL("https://coatsink.com/");
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00006C83 File Offset: 0x00005083
		public virtual void ShowGameSupport()
		{
			Application.OpenURL("https://coatsink.com/");
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00006C8F File Offset: 0x0000508F
		public virtual Guid CurrentLobbyID
		{
			get
			{
				return Guid.Empty;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00006C96 File Offset: 0x00005096
		public virtual bool IsInLobby
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00006C99 File Offset: 0x00005099
		public virtual bool IsLobbyHost
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00006C9C File Offset: 0x0000509C
		public virtual bool IsJoiningLobby
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00006C9F File Offset: 0x0000509F
		public virtual void CreateLobby(LOBBY_TYPE lobbyType, uint maxSlots)
		{
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00006CA1 File Offset: 0x000050A1
		public virtual void LeaveLobby()
		{
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00006CA3 File Offset: 0x000050A3
		public virtual void JoinLobby(Guid id)
		{
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00006CA5 File Offset: 0x000050A5
		public virtual bool GetLobbyData(string dataKey, out string dataOut)
		{
			dataOut = null;
			return false;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00006CAB File Offset: 0x000050AB
		public virtual bool SetLobbyData(string dataKey, string dataIn)
		{
			return false;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00006CAE File Offset: 0x000050AE
		public virtual void SendLobbyData()
		{
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00006CB0 File Offset: 0x000050B0
		public virtual BaseUserInfo LobbyHost()
		{
			return default(BaseUserInfo);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00006CC6 File Offset: 0x000050C6
		public virtual bool GetLobbyInfo(ref LobbyInformation lobbyInfo)
		{
			lobbyInfo.host.Blank();
			lobbyInfo.maxSlots = 0U;
			lobbyInfo.lobbyType = LOBBY_TYPE.PRIVATE;
			return false;
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00006CE2 File Offset: 0x000050E2
		public virtual int TotalInLobby
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00006CE5 File Offset: 0x000050E5
		public virtual bool GetLobbyUser(int index, ref BaseUserInfo userData)
		{
			return false;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00006CE8 File Offset: 0x000050E8
		public virtual bool LobbyKickUser(BaseUserInfo info)
		{
			return false;
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00006CEB File Offset: 0x000050EB
		public virtual bool HasLobbyInvite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00006CEE File Offset: 0x000050EE
		public virtual string LobbyInviteHostName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00006CF5 File Offset: 0x000050F5
		public virtual bool JoinInviteLobby()
		{
			return false;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00006CF8 File Offset: 0x000050F8
		public virtual bool ShowPlatformLobbyMenu()
		{
			return false;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00006CFB File Offset: 0x000050FB
		public virtual bool SendLobbyMessage(BaseUserInfo userInfo, PlatformMessageBase message)
		{
			return false;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00006CFE File Offset: 0x000050FE
		public virtual bool SendLobbyMessage(PlatformMessageBase message)
		{
			return false;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00006D01 File Offset: 0x00005101
		public virtual bool UserIsInLobby(BaseUserInfo user)
		{
			return false;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00006D04 File Offset: 0x00005104
		public virtual bool UserIsLobbyHost(BaseUserInfo user)
		{
			return false;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00006D07 File Offset: 0x00005107
		public virtual void GuestJoin(int localID)
		{
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00006D09 File Offset: 0x00005109
		public virtual void GuestLeave(int localID)
		{
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00006D0B File Offset: 0x0000510B
		public virtual bool InviteUserToLobby(BaseUserInfo user)
		{
			return false;
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00006D0E File Offset: 0x0000510E
		// (set) Token: 0x06000157 RID: 343 RVA: 0x00006D11 File Offset: 0x00005111
		public virtual bool VoiceEnabled
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00006D13 File Offset: 0x00005113
		// (set) Token: 0x06000159 RID: 345 RVA: 0x00006D1A File Offset: 0x0000511A
		public virtual float DefaultVoiceVolume
		{
			get
			{
				return 1f;
			}
			set
			{
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00006D1C File Offset: 0x0000511C
		// (set) Token: 0x0600015B RID: 347 RVA: 0x00006D1F File Offset: 0x0000511F
		public virtual bool DefaultVoiceMute
		{
			get
			{
				return true;
			}
			set
			{
			}
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00006D21 File Offset: 0x00005121
		public virtual void AddVoiceConnection(BaseUserInfo userInfo)
		{
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00006D23 File Offset: 0x00005123
		public virtual void RemoveVoiceConnection(BaseUserInfo userInfo)
		{
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00006D25 File Offset: 0x00005125
		public virtual void ClearVoiceConectionsClear()
		{
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00006D27 File Offset: 0x00005127
		public virtual bool VoiceActive
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00006D2A File Offset: 0x0000512A
		public virtual void StartVoice()
		{
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00006D2C File Offset: 0x0000512C
		public virtual void StopVoice()
		{
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00006D2E File Offset: 0x0000512E
		public virtual void SetVoiceMuted(BaseUserInfo userInfo, bool mute)
		{
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00006D30 File Offset: 0x00005130
		public virtual bool GetVoiceMuted(BaseUserInfo userInfo)
		{
			return false;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00006D33 File Offset: 0x00005133
		public virtual void SetVoiceMutes(bool mute)
		{
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00006D35 File Offset: 0x00005135
		public virtual void SetVoiceVolumes(float volume)
		{
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00006D37 File Offset: 0x00005137
		public virtual float GetVoiceVolume(BaseUserInfo userInfo)
		{
			return 1f;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00006D3E File Offset: 0x0000513E
		public virtual void SetVoiceVolume(BaseUserInfo userInfo, float volume)
		{
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00006D40 File Offset: 0x00005140
		public virtual bool MainUserSignedIn
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00006D43 File Offset: 0x00005143
		public virtual bool SetToLocalUserImage(int localID, ref Texture2D terxtureSetting)
		{
			return true;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00006D46 File Offset: 0x00005146
		public virtual bool SetToUserImage(BaseUserInfo userInfo, ref Texture2D terxtureSetting)
		{
			return true;
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00006D49 File Offset: 0x00005149
		public virtual void StartedPlayingWith(BaseUserInfo userInfo)
		{
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00006D4B File Offset: 0x0000514B
		public virtual void StoppedPlayingWith(BaseUserInfo userInfo)
		{
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00006D4D File Offset: 0x0000514D
		public virtual void JoinedServer(string IP, ushort port)
		{
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00006D4F File Offset: 0x0000514F
		public virtual void LeftServer()
		{
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00006D51 File Offset: 0x00005151
		public virtual bool SendMessage(BaseUserInfo userInfo, PlatformMessageBase message, bool reliable)
		{
			return false;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00006D54 File Offset: 0x00005154
		public virtual bool MessageConnectionReady(BaseUserInfo userInfo)
		{
			return false;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00006D57 File Offset: 0x00005157
		public virtual void MessageStartConnecting(BaseUserInfo userInfo)
		{
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00006D59 File Offset: 0x00005159
		public virtual void MessageDiconnect(BaseUserInfo userInfo)
		{
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00006D5B File Offset: 0x0000515B
		public virtual void MessageDiconnectAll()
		{
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00006D5D File Offset: 0x0000515D
		public virtual bool SetControllerColour(int controllerID, Color colour)
		{
			return false;
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00006D60 File Offset: 0x00005160
		public virtual int MaxLocalPlayers
		{
			get
			{
				return 8;
			}
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00006D63 File Offset: 0x00005163
		public virtual int UserLocalID(int controlller)
		{
			return controlller;
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00006D66 File Offset: 0x00005166
		public virtual bool GUIActive
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00006D69 File Offset: 0x00005169
		public virtual bool ShowSystemMessageYESNO(string body, Action yesAction, Action noAction)
		{
			return false;
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00006D6C File Offset: 0x0000516C
		public virtual bool ShowSystemMessageOK(string body, Action action)
		{
			return false;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00006D6F File Offset: 0x0000516F
		public virtual bool IsShowingMessage()
		{
			return false;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00006D72 File Offset: 0x00005172
		public virtual bool IsSelectInverted()
		{
			return false;
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00006D75 File Offset: 0x00005175
		public virtual bool IsOnlineAllowed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00006D78 File Offset: 0x00005178
		public virtual void UpdateOnlinePermistion(int controller = -1)
		{
			this.AddToNextUpdate(delegate
			{
				PlatformEvents.OnlineCheckComplete(controller, TryOnlineResult.PASS);
			});
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00006DA4 File Offset: 0x000051A4
		public virtual TryOnlineResult TryToGoOnline(bool showMessage)
		{
			return TryOnlineResult.PASS;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00006DA7 File Offset: 0x000051A7
		public virtual TryOnlineResult TryToGoOnline(int controller, bool showMessage)
		{
			return TryOnlineResult.PASS;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00006DAA File Offset: 0x000051AA
		public virtual bool ShowOnlineFailReason(int controller)
		{
			return false;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00006DAD File Offset: 0x000051AD
		public virtual void OnlineUpdate()
		{
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000182 RID: 386 RVA: 0x00006DAF File Offset: 0x000051AF
		public virtual int MainController
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00006DB2 File Offset: 0x000051B2
		public virtual bool UserActive(int controlller)
		{
			return true;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00006DB5 File Offset: 0x000051B5
		public virtual void SetOnlineState(int controller, bool active)
		{
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00006DB7 File Offset: 0x000051B7
		public virtual bool UpdateFriendsList()
		{
			this.AddToNextUpdate(delegate
			{
				PlatformEvents.FriendsListUpdated(null);
			});
			return true;
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00006DDD File Offset: 0x000051DD
		public virtual BaseUserInfo[] FriendsList
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04000077 RID: 119
		protected object _lockSend = new object();

		// Token: 0x0400007A RID: 122
		protected static BasePlatformManager _Instance = null;

		// Token: 0x0400007B RID: 123
		private static bool _Initialized = false;

		// Token: 0x0400007C RID: 124
		protected static bool _InitializedPlatformAPI = false;

		// Token: 0x0400007D RID: 125
		private static bool _gameSetup = false;

		// Token: 0x0400007F RID: 127
		private List<string> _blockers = new List<string>();

		// Token: 0x04000080 RID: 128
		private Action _blockerFreeCall;

		// Token: 0x04000081 RID: 129
		protected static object _entitlementLock = new object();

		// Token: 0x04000082 RID: 130
		protected static bool _entitlementChecked = false;

		// Token: 0x04000083 RID: 131
		protected static bool _userIsEntitled = false;

		// Token: 0x04000084 RID: 132
		[CompilerGenerated]
		private static Action action;

		// Token: 0x04000085 RID: 133
		[CompilerGenerated]
		private static Action action1;

		// Token: 0x04000088 RID: 136
		[CompilerGenerated]
		private static Action action2;
	}
}
