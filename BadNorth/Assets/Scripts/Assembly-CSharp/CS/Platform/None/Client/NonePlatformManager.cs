using System;
using CS.Platform.Base.Client.Part;
using CS.Platform.None.Client.Part;
using CS.Platform.Utils;
using UnityEngine;

namespace CS.Platform.None.Client
{
	// Token: 0x0200003E RID: 62
	public class NonePlatformManager : BasePlatformManager
	{
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000264 RID: 612 RVA: 0x0000C033 File Offset: 0x0000A433
		public SystemSaveLoad Storage
		{
			get
			{
				return this._storage;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000265 RID: 613 RVA: 0x0000C03B File Offset: 0x0000A43B
		public PlatformSystemMessenger Dialog
		{
			get
			{
				return this._dialogUI;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000266 RID: 614 RVA: 0x0000C043 File Offset: 0x0000A443
		public NoneUtils Utils
		{
			get
			{
				return this._utils;
			}
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000C04C File Offset: 0x0000A44C
		public void Awake()
		{
			if (!BasePlatformManager._InitializedPlatformAPI)
			{
				if (!BasePlatformManager.Initialized)
				{
					this._dialogUI = CS.Platform.Utils.Random.CreateGameBasedUI(base.gameObject);
					this._utils = base.gameObject.AddComponent<NoneUtils>();
					this._storage = base.gameObject.AddComponent<SystemSaveLoad>();
					BasePlatformManager._InitializedPlatformAPI = true;
					BasePlatformManager.Initialized = true;
					base.PassEntitlement();
				}
				if (!BasePlatformManager.Initialized)
				{
					CS.Platform.Utils.Debug.LogError("[NonePlatform] Initialize() failed", new object[0]);
				}
			}
			else
			{
				CS.Platform.Utils.Debug.LogError("[CorePlatform] We're trying to initialise the 'None' platform when a platform has already been initialised... we shouldnt be doing this!", new object[0]);
			}
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000C0E1 File Offset: 0x0000A4E1
		public override string GetKey()
		{
			return "no platform";
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000C0E8 File Offset: 0x0000A4E8
		public override string GetServerPlatformKey()
		{
			return this.GetKey();
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000C0F0 File Offset: 0x0000A4F0
		public override BaseUserInfo GetUserBaseInfo()
		{
			return new BaseUserInfo(0UL, this.GetKey(), "PLAYER");
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600026B RID: 619 RVA: 0x0000C104 File Offset: 0x0000A504
		// (set) Token: 0x0600026C RID: 620 RVA: 0x0000C111 File Offset: 0x0000A511
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

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600026D RID: 621 RVA: 0x0000C11F File Offset: 0x0000A51F
		public override bool IsLocalSaving
		{
			get
			{
				return this.Storage.IsSaving;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600026E RID: 622 RVA: 0x0000C12C File Offset: 0x0000A52C
		public override bool IsLocalLoading
		{
			get
			{
				return this.Storage.IsLoading;
			}
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000C139 File Offset: 0x0000A539
		public override string[] Loaded()
		{
			return this.Storage.LoadedFiles();
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000C146 File Offset: 0x0000A546
		public override int Loaded(string file)
		{
			return this.Storage.HasData(file);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000C154 File Offset: 0x0000A554
		public override int Load(string file, ref byte[] data, bool copy = true, bool safe = true)
		{
			return this.Storage.GetLoadedData(file, ref data, copy, safe);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000C166 File Offset: 0x0000A566
		public override int Load(string file, ref string data, bool safe = true)
		{
			return this.Storage.GetLoadedData(file, ref data, safe);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000C176 File Offset: 0x0000A576
		public override bool Save(string file, byte[] data, int size, bool copy = false, bool safe = true)
		{
			return this.Storage.AddSaveData(file, data, size, copy, safe);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000C18A File Offset: 0x0000A58A
		public override bool Save(string file, string data, bool safe = true)
		{
			return this.Storage.AddSaveData(file, data, safe);
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000C19A File Offset: 0x0000A59A
		public override void SendSavedData()
		{
			this.Storage.StartSaveData();
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000C1A7 File Offset: 0x0000A5A7
		public override void LoadSavedData(string[] load)
		{
			this.Storage.StartLoadData(load);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000C1B5 File Offset: 0x0000A5B5
		public override void ForgetFile(string file)
		{
			this.Storage.Forget(file);
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000278 RID: 632 RVA: 0x0000C1C3 File Offset: 0x0000A5C3
		public override bool GUIActive
		{
			get
			{
				return this.Utils.GUIActive;
			}
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000C1D0 File Offset: 0x0000A5D0
		public override bool SetToUserImage(BaseUserInfo userInfo, ref Texture2D terxtureSetting)
		{
			return false;
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600027A RID: 634 RVA: 0x0000C1D3 File Offset: 0x0000A5D3
		public override int MaxLocalPlayers
		{
			get
			{
				return 8;
			}
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000C1D6 File Offset: 0x0000A5D6
		public override bool ShowSystemMessageYESNO(string body, Action yesAction, Action noAction)
		{
			if (this.Dialog != null)
			{
				this.Dialog.ShowYESNO(body, yesAction, noAction);
			}
			return this.Dialog != null;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000C203 File Offset: 0x0000A603
		public override bool ShowSystemMessageOK(string body, Action action)
		{
			if (this.Dialog != null)
			{
				this.Dialog.ShowOK(body, action);
			}
			return this.Dialog != null;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000C22F File Offset: 0x0000A62F
		public override bool IsShowingMessage()
		{
			return this.Dialog != null && this.Dialog.ShowingMessage;
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600027E RID: 638 RVA: 0x0000C24F File Offset: 0x0000A64F
		public override bool IsOnlineAllowed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000C254 File Offset: 0x0000A654
		public override void UpdateOnlinePermistion(int controller = -1)
		{
			base.AddToNextUpdate(delegate
			{
				PlatformEvents.OnlineCheckComplete(controller, TryOnlineResult.FAIL);
			});
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000C280 File Offset: 0x0000A680
		public override TryOnlineResult TryToGoOnline(bool showMessage)
		{
			return TryOnlineResult.FAIL;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000C283 File Offset: 0x0000A683
		public override TryOnlineResult TryToGoOnline(int controller, bool showMessage)
		{
			return TryOnlineResult.FAIL;
		}

		// Token: 0x040000F6 RID: 246
		private SystemSaveLoad _storage;

		// Token: 0x040000F7 RID: 247
		private PlatformSystemMessenger _dialogUI;

		// Token: 0x040000F8 RID: 248
		private NoneUtils _utils;
	}
}
