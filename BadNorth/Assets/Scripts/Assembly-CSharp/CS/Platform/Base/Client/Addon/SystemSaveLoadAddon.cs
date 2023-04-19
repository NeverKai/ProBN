using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using CS.Platform.Utils;

namespace CS.Platform.Base.Client.Addon
{
	// Token: 0x02000037 RID: 55
	internal class SystemSaveLoadAddon
	{
		// Token: 0x0600021F RID: 543 RVA: 0x0000AD3A File Offset: 0x0000913A
		public SystemSaveLoadAddon(BasePlatformManager manager)
		{
			this._manager = manager;
			this._saveloadThread = new ThreadHandler("SystemSaveLoadAddon");
			this._saveloadThread.RunThreadOnce = true;
			this._saveloadThread.PauseThread = false;
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000220 RID: 544 RVA: 0x0000AD74 File Offset: 0x00009174
		// (remove) Token: 0x06000221 RID: 545 RVA: 0x0000ADAC File Offset: 0x000091AC
		
		public event Action OnSaveComplete;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000222 RID: 546 RVA: 0x0000ADE4 File Offset: 0x000091E4
		// (remove) Token: 0x06000223 RID: 547 RVA: 0x0000AE1C File Offset: 0x0000921C
		
		public event Action OnLoadComplete;

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000224 RID: 548 RVA: 0x0000AE52 File Offset: 0x00009252
		public bool IsLoading
		{
			get
			{
				return this._loading;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000225 RID: 549 RVA: 0x0000AE5A File Offset: 0x0000925A
		public bool IsSaving
		{
			get
			{
				return this._saving;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000226 RID: 550 RVA: 0x0000AE62 File Offset: 0x00009262
		public bool HasFailed
		{
			get
			{
				return this._failed;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000227 RID: 551 RVA: 0x0000AE6A File Offset: 0x0000926A
		public bool IsBusy
		{
			get
			{
				return this._saveloadThread.Running || this._saving || this._loading;
			}
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000AE90 File Offset: 0x00009290
		~SystemSaveLoadAddon()
		{
			this.Abort(true);
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000AEC0 File Offset: 0x000092C0
		public void Abort(bool block)
		{
			this._saveloadThread.Abort(block);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000AED0 File Offset: 0x000092D0
		public void SaveData()
		{
			if (!this.IsBusy)
			{
				this._failed = false;
				this._saving = true;
				this._saveloadThread.ClearComplete();
				this._saveloadThread.OnCompletion += this.SaveComplete;
				this._saveloadThread.ClearParts();
				this._saveloadThread.AddPart(new Action(this.SaveToFiles));
				this._saveloadThread.Start();
			}
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000AF48 File Offset: 0x00009348
		private void SaveToFiles()
		{
			if (this.GetFiles == null || this.GetData == null)
			{
				return;
			}
			List<string>.Enumerator enumerator = this.GetFiles();
			FileStream fileStream = null;
			string str = Storage.StorageNoneDirectory();
			while (enumerator.MoveNext())
			{
				if (!Storage.StorageFilesNameExpectedCloud(enumerator.Current))
				{
					string text = str + enumerator.Current;
					try
					{
						int num = text.LastIndexOf('/');
						if (num < 0)
						{
							num = text.LastIndexOf('\\');
						}
						string path = text.Substring(0, num);
						Directory.CreateDirectory(path);
						fileStream = File.Open(text, FileMode.Create, FileAccess.Write);
						fileStream.SetLength(0L);
					}
					catch (Exception)
					{
						fileStream = null;
					}
					byte[] array = this.GetData(enumerator.Current);
					if (fileStream != null)
					{
						if (array != null && array.Length != 0)
						{
							fileStream.Flush();
							fileStream.Write(array, 0, array.Length);
							fileStream.Close();
						}
						else
						{
							try
							{
								fileStream.Close();
								File.Delete(str + enumerator.Current);
							}
							catch
							{
								this._failed = true;
								break;
							}
						}
					}
					else if (array != null && array.Length != 0)
					{
						this._failed = true;
						break;
					}
				}
			}
			enumerator.Dispose();
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000B0B4 File Offset: 0x000094B4
		public void LoadData()
		{
			if (!this.IsBusy)
			{
				this._failed = false;
				this._loading = true;
				this._saveloadThread.ClearComplete();
				this._saveloadThread.OnCompletion += this.LoadComplete;
				this._saveloadThread.ClearParts();
				this._saveloadThread.AddPart(new Action(this.LoadFromFiles));
				this._saveloadThread.Start();
			}
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000B12C File Offset: 0x0000952C
		private void LoadFromFiles()
		{
			if (this.GetLoadFiles == null)
			{
				return;
			}
			FileStream fileStream = null;
			List<string>.Enumerator enumerator = this.GetLoadFiles();
			string str = Storage.StorageNoneDirectory();
			while (enumerator.MoveNext())
			{
				if (!Storage.StorageFilesNameExpectedCloud(enumerator.Current))
				{
					try
					{
						fileStream = File.Open(str + enumerator.Current, FileMode.Open, FileAccess.Read);
					}
					catch (Exception)
					{
						fileStream = null;
					}
					if (fileStream != null)
					{
						byte[] array = new byte[fileStream.Length];
						fileStream.Read(array, 0, (int)fileStream.Length);
						fileStream.Close();
						this.AddData(enumerator.Current, array);
					}
				}
			}
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000B1F0 File Offset: 0x000095F0
		private void SaveComplete()
		{
			this._saving = false;
			if (this.OnSaveComplete != null)
			{
				this.OnSaveComplete();
			}
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000B20F File Offset: 0x0000960F
		private void LoadComplete()
		{
			this._loading = false;
			if (this.OnLoadComplete != null)
			{
				this.OnLoadComplete();
			}
		}

		// Token: 0x040000C7 RID: 199
		private BasePlatformManager _manager;

		// Token: 0x040000C8 RID: 200
		public Func<List<string>.Enumerator> GetFiles;

		// Token: 0x040000C9 RID: 201
		public Func<List<string>.Enumerator> GetLoadFiles;

		// Token: 0x040000CA RID: 202
		public Func<string, byte[]> GetData;

		// Token: 0x040000CB RID: 203
		public Action<string, byte[]> AddData;

		// Token: 0x040000CE RID: 206
		private bool _saving;

		// Token: 0x040000CF RID: 207
		private bool _loading;

		// Token: 0x040000D0 RID: 208
		private bool _failed;

		// Token: 0x040000D1 RID: 209
		private ThreadHandler _saveloadThread;
	}
}
