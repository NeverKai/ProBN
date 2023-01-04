using System;
using System.Collections.Generic;
using CS.Platform.Base.Client.Addon;
using CS.Platform.Base.Client.Part;
using CS.Platform.Utils;
using Steamworks;

namespace CS.Platform.Steam.Client.Part
{
	// Token: 0x02000049 RID: 73
	public class SteamStorage : BaseStorage
	{
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060002FC RID: 764 RVA: 0x0000F89F File Offset: 0x0000DC9F
		public override bool IsBusy
		{
			get
			{
				return this._saveloadThread.Running || this._saving || this._loading || this._systemData.IsBusy;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060002FD RID: 765 RVA: 0x0000F8D5 File Offset: 0x0000DCD5
		public override bool IsLoading
		{
			get
			{
				return this._loading || this._systemData.IsLoading;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060002FE RID: 766 RVA: 0x0000F8F0 File Offset: 0x0000DCF0
		public override bool IsSaving
		{
			get
			{
				return this._saving || this._systemData.IsSaving;
			}
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000F90C File Offset: 0x0000DD0C
		public void Awake()
		{
			this._manager = base.GetComponent<SteamManager>();
			this._systemData = new SystemSaveLoadAddon(base.GetComponent<BasePlatformManager>());
			this._systemData.OnLoadComplete += this.OnLoadComplete;
			this._systemData.OnSaveComplete += this.OnSaveComplete;
			this._systemData.GetFiles = new Func<List<string>.Enumerator>(base.GetFiles);
			this._systemData.GetLoadFiles = new Func<List<string>.Enumerator>(base.GetLoadFiles);
			this._systemData.GetData = new Func<string, byte[]>(base.GetData);
			this._systemData.AddData = new Action<string, byte[]>(base.LogicAddData);
			this._saveloadThread = new ThreadHandler("SteamSaveLoad");
			this._saveloadThread.RunThreadOnce = true;
			this._saveloadThread.PauseThread = false;
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000F9E8 File Offset: 0x0000DDE8
		private void OnDestroy()
		{
			this._saveloadThread.Abort(true);
			this._systemData.Abort(true);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000FA04 File Offset: 0x0000DE04
		private void OnLoadComplete()
		{
			if (!this.IsBusy)
			{
				base.RemoveUnused();
				if (this._failed || this._systemData.HasFailed)
				{
					base.FailData(false, false);
				}
				else
				{
					base.CompleteLoadData();
				}
			}
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000FA50 File Offset: 0x0000DE50
		private void OnSaveComplete()
		{
			if (!this.IsBusy)
			{
				base.RemoveUnused();
				if (this._failed || this._systemData.HasFailed)
				{
					base.FailData(true, false);
				}
				else
				{
					base.CompleteSaveData();
				}
			}
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000FA9C File Offset: 0x0000DE9C
		protected override void LoadData()
		{
			if (!this.IsBusy)
			{
				this._loading = true;
				this._failed = false;
				PlatformEvents.LoadLocalStarted();
				this._systemData.LoadData();
				this._saveloadThread.ClearComplete();
				this._saveloadThread.OnCompletion += this.OnSteamLoadComplete;
				this._saveloadThread.ClearParts();
				this._saveloadThread.AddPart(new Action(this.SteamLoadLogic));
				this._saveloadThread.Start();
			}
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000FB21 File Offset: 0x0000DF21
		private void OnSteamLoadComplete()
		{
			this._loading = false;
			this.OnLoadComplete();
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000FB30 File Offset: 0x0000DF30
		private void SteamLoadLogic()
		{
			int fileCount = SteamRemoteStorage.GetFileCount();
			for (int i = 0; i < fileCount; i++)
			{
				int num = 0;
				string fileNameAndSize = SteamRemoteStorage.GetFileNameAndSize(i, out num);
				if (this._loadFiles.Contains(fileNameAndSize))
				{
					if (!SteamRemoteStorage.FileExists(fileNameAndSize))
					{
						this._failed = true;
						break;
					}
					int fileSize = SteamRemoteStorage.GetFileSize(fileNameAndSize);
					if (fileSize != 0)
					{
						byte[] array = new byte[fileSize];
						int num2 = SteamRemoteStorage.FileRead(fileNameAndSize, array, fileSize);
						if (num2 != fileSize)
						{
							Debug.ThreadLogError("[SSTORAGE] SteamLoadLogic Failed: Reading | Read: {0} | After: {1}", new object[]
							{
								num2,
								fileSize
							});
							this._failed = true;
							break;
						}
						base.LogicAddData(fileNameAndSize, array);
					}
				}
			}
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000FC00 File Offset: 0x0000E000
		protected override void SaveData()
		{
			if (!this.IsBusy)
			{
				this._saving = true;
				this._failed = false;
				PlatformEvents.SaveLocalStarted();
				this._systemData.SaveData();
				this._saveloadThread.ClearComplete();
				this._saveloadThread.OnCompletion += this.OnSteamSaveComplete;
				this._saveloadThread.ClearParts();
				this._saveloadThread.AddPart(new Action(this.SteamSaveLogic));
				this._saveloadThread.Start();
			}
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000FC85 File Offset: 0x0000E085
		private void OnSteamSaveComplete()
		{
			this._saving = false;
			this.OnSaveComplete();
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000FC94 File Offset: 0x0000E094
		private void SteamSaveLogic()
		{
			List<string>.Enumerator enumerator = this._dirtyFiles.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (!Storage.StorageFilesNameExpectedCloud(enumerator.Current))
				{
					if (SteamRemoteStorage.FileExists(enumerator.Current))
					{
						SteamRemoteStorage.FileDelete(enumerator.Current);
					}
				}
				else
				{
					byte[] array = this._storedData[enumerator.Current];
					if (array != null)
					{
						SteamRemoteStorage.FileWrite(enumerator.Current, array, array.Length);
					}
					else if (SteamRemoteStorage.FileExists(enumerator.Current))
					{
						SteamRemoteStorage.FileDelete(enumerator.Current);
					}
				}
			}
			enumerator.Dispose();
		}

		// Token: 0x04000140 RID: 320
		private SteamManager _manager;

		// Token: 0x04000141 RID: 321
		private object locker = new object();

		// Token: 0x04000142 RID: 322
		private bool _saving;

		// Token: 0x04000143 RID: 323
		private bool _loading;

		// Token: 0x04000144 RID: 324
		private SystemSaveLoadAddon _systemData;

		// Token: 0x04000145 RID: 325
		private ThreadHandler _saveloadThread;

		// Token: 0x04000146 RID: 326
		private bool _failed;
	}
}
