using System;
using System.Collections.Generic;
using System.IO;
using CS.Platform.Utils;

namespace CS.Platform.Base.Client.Part
{
	// Token: 0x02000036 RID: 54
	public class SystemSaveLoad : BaseStorage
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000214 RID: 532 RVA: 0x0000A984 File Offset: 0x00008D84
		public override bool IsLoading
		{
			get
			{
				return this._loading;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000215 RID: 533 RVA: 0x0000A98C File Offset: 0x00008D8C
		public override bool IsSaving
		{
			get
			{
				return this._saving;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000216 RID: 534 RVA: 0x0000A994 File Offset: 0x00008D94
		public override bool IsBusy
		{
			get
			{
				return this._saveloadThread.Running;
			}
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000A9A1 File Offset: 0x00008DA1
		public void Awake()
		{
			this._manager = base.gameObject.GetComponent<BasePlatformManager>();
			this._saveloadThread = new ThreadHandler("BaseSaveLoad");
			this._saveloadThread.RunThreadOnce = true;
			this._saveloadThread.PauseThread = false;
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000A9DC File Offset: 0x00008DDC
		protected void OnDestroy()
		{
			this._saveloadThread.Abort(true);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000A9EC File Offset: 0x00008DEC
		protected override void SaveData()
		{
			if (!this.IsBusy)
			{
				this._failed = false;
				this._saving = true;
				PlatformEvents.SaveLocalStarted();
				this._saveloadThread.ClearComplete();
				this._saveloadThread.OnCompletion += this.SaveComplete;
				this._saveloadThread.ClearParts();
				this._saveloadThread.AddPart(new Action(this.SaveToFiles));
				this._saveloadThread.Start();
			}
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000AA68 File Offset: 0x00008E68
		private void SaveToFiles()
		{
			List<string>.Enumerator enumerator = this._dirtyFiles.GetEnumerator();
			FileStream fileStream = null;
			string str = Storage.StorageNoneDirectory();
			while (enumerator.MoveNext())
			{
				try
				{
					string text = str + enumerator.Current;
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
				catch
				{
					fileStream = null;
				}
				byte[] data = base.GetData(enumerator.Current);
				if (fileStream != null)
				{
					if (data != null && data.Length != 0)
					{
						fileStream.Flush();
						fileStream.Write(data, 0, data.Length);
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
				else if (data != null && data.Length != 0)
				{
					this._failed = true;
					break;
				}
			}
			enumerator.Dispose();
			base.RemoveUnused();
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000ABAC File Offset: 0x00008FAC
		protected override void LoadData()
		{
			if (!this.IsBusy)
			{
				this._failed = false;
				this._loading = true;
				PlatformEvents.LoadLocalStarted();
				this._saveloadThread.ClearComplete();
				this._saveloadThread.OnCompletion += this.LoadComplete;
				this._saveloadThread.ClearParts();
				this._saveloadThread.AddPart(new Action(this.LoadFromFiles));
				this._saveloadThread.Start();
			}
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000AC28 File Offset: 0x00009028
		private void LoadFromFiles()
		{
			FileStream fileStream = null;
			List<string> loadFiles = this._loadFiles;
			string str = Storage.StorageNoneDirectory();
			for (int i = 0; i < loadFiles.Count; i++)
			{
				try
				{
					fileStream = File.Open(str + loadFiles[i], FileMode.Open, FileAccess.Read);
				}
				catch
				{
					fileStream = null;
				}
				if (fileStream != null)
				{
					try
					{
						byte[] array = new byte[fileStream.Length];
						fileStream.Read(array, 0, (int)fileStream.Length);
						fileStream.Close();
						base.LogicAddData(loadFiles[i], array);
					}
					catch
					{
						this._failed = true;
						break;
					}
				}
			}
			base.RemoveUnused();
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000ACEC File Offset: 0x000090EC
		private void SaveComplete()
		{
			this._saving = false;
			if (this._failed)
			{
				base.FailData(true, false);
			}
			else
			{
				base.CompleteSaveData();
			}
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000AD13 File Offset: 0x00009113
		private void LoadComplete()
		{
			this._loading = false;
			if (this._failed)
			{
				base.FailData(false, false);
			}
			else
			{
				base.CompleteLoadData();
			}
		}

		// Token: 0x040000C2 RID: 194
		private BasePlatformManager _manager;

		// Token: 0x040000C3 RID: 195
		private bool _saving;

		// Token: 0x040000C4 RID: 196
		private bool _loading;

		// Token: 0x040000C5 RID: 197
		private bool _failed;

		// Token: 0x040000C6 RID: 198
		private ThreadHandler _saveloadThread;
	}
}
