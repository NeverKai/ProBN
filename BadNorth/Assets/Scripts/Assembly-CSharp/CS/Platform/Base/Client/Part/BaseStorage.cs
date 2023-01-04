using System;
using System.Collections.Generic;
using CS.Platform.Utils;
using UnityEngine;

namespace CS.Platform.Base.Client.Part
{
	// Token: 0x02000034 RID: 52
	public abstract class BaseStorage : MonoBehaviour
	{
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060001CA RID: 458
		public abstract bool IsBusy { get; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060001CB RID: 459 RVA: 0x000092F3 File Offset: 0x000076F3
		public bool IsReady
		{
			get
			{
				return this._hasCompleted;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060001CC RID: 460 RVA: 0x000092FB File Offset: 0x000076FB
		// (set) Token: 0x060001CD RID: 461 RVA: 0x00009303 File Offset: 0x00007703
		public virtual bool StorageDisabled
		{
			get
			{
				return this._storageDisabled;
			}
			set
			{
				this._storageDisabled = value;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060001CE RID: 462 RVA: 0x0000930C File Offset: 0x0000770C
		public virtual bool IsLoading
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001CF RID: 463 RVA: 0x0000930F File Offset: 0x0000770F
		public virtual bool IsSaving
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060001D0 RID: 464
		protected abstract void SaveData();

		// Token: 0x060001D1 RID: 465
		protected abstract void LoadData();

		// Token: 0x060001D2 RID: 466 RVA: 0x00009314 File Offset: 0x00007714
		public void StartSaveData()
		{
			if (this._hasCompleted && this._waitingAction == null)
			{
				this._hasCompleted = false;
				if (!this.StorageDisabled)
				{
					this.SaveData();
				}
				else
				{
					BasePlatformManager.Instance.AddToNextUpdate(new Action(this.CompleteSaveData));
				}
			}
			else
			{
				this._waitingAction = new Action(this.StartSaveData);
			}
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00009384 File Offset: 0x00007784
		public void StartLoadData(string[] files)
		{
			if (files == null)
			{
				this._loadFiles.Clear();
				this._loadFiles.AddRange(Storage.StorageFiles());
			}
			else
			{
				for (int i = 0; i < files.Length; i++)
				{
					if (!this._loadFiles.Contains(files[i]))
					{
						this._loadFiles.Add(files[i]);
					}
				}
			}
			this.BeginLoad();
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x000093F4 File Offset: 0x000077F4
		private void BeginLoad()
		{
			if (this._hasCompleted && this._waitingAction == null)
			{
				this._hasCompleted = false;
				if (!this.StorageDisabled)
				{
					this.LoadData();
				}
				else
				{
					BasePlatformManager.Instance.AddToNextUpdate(new Action(this.CompleteLoadData));
				}
			}
			else
			{
				this._waitingAction = new Action(this.BeginLoad);
			}
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00009464 File Offset: 0x00007864
		protected void CompleteSaveData()
		{
			string[] files = this._dirtyFiles.ToArray();
			this._dirtyFiles.Clear();
			PlatformEvents.SaveLocalFilesCompleteEvent(files);
			PlatformEvents.SaveLocalComplete();
			this._hasCompleted = true;
			if (this._waitingAction != null)
			{
				Action waitingAction = this._waitingAction;
				this._waitingAction = null;
				waitingAction();
			}
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x000094BC File Offset: 0x000078BC
		protected void CompleteLoadData()
		{
			string[] files = this._loadFiles.ToArray();
			this._loadFiles.Clear();
			PlatformEvents.LoadLocalFilesComplete(files);
			PlatformEvents.LoadLocalComplete();
			this._hasCompleted = true;
			if (this._waitingAction != null)
			{
				Action waitingAction = this._waitingAction;
				this._waitingAction = null;
				waitingAction();
			}
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00009514 File Offset: 0x00007914
		protected void FailData(bool save, bool handled)
		{
			if (handled)
			{
				PlatformEvents.SaveLoadFail(save);
				this._hasCompleted = true;
				if (this._waitingAction != null)
				{
					Action waitingAction = this._waitingAction;
					this._waitingAction = null;
					waitingAction();
				}
			}
			else
			{
				this.ShowMessageFailRetry(save);
			}
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00009560 File Offset: 0x00007960
		protected void RemoveUnused()
		{
			Dictionary<string, byte[]>.Enumerator enumerator = this._storedData.GetEnumerator();
			int num = 0;
			while (enumerator.MoveNext())
			{
				KeyValuePair<string, byte[]> keyValuePair = enumerator.Current;
				if (keyValuePair.Value == null)
				{
					KeyValuePair<string, byte[]> keyValuePair2 = enumerator.Current;
					string key = keyValuePair2.Key;
					this._storedData.Remove(key);
					enumerator = this._storedData.GetEnumerator();
					num--;
					for (int i = 0; i < num; i++)
					{
						enumerator.MoveNext();
					}
				}
				num++;
			}
			enumerator.Dispose();
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x000095F7 File Offset: 0x000079F7
		public void Forget(string file)
		{
			if (this._storedData.ContainsKey(file))
			{
				this._storedData.Remove(file);
			}
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00009618 File Offset: 0x00007A18
		public string[] LoadedFiles()
		{
			if (!this.IsBusy && 0 < this._storedData.Count)
			{
				List<string> list = new List<string>();
				Dictionary<string, byte[]>.Enumerator enumerator = this._storedData.GetEnumerator();
				while (enumerator.MoveNext())
				{
					KeyValuePair<string, byte[]> keyValuePair = enumerator.Current;
					if (keyValuePair.Value != null)
					{
						List<string> list2 = list;
						KeyValuePair<string, byte[]> keyValuePair2 = enumerator.Current;
						list2.Add(keyValuePair2.Key);
					}
				}
				enumerator.Dispose();
				return list.ToArray();
			}
			return null;
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000969C File Offset: 0x00007A9C
		public int HasData(string fileName)
		{
			if (!this.IsBusy && this._storedData.ContainsKey(fileName) && this._storedData[fileName] != null)
			{
				return this._storedData[fileName].Length;
			}
			return 0;
		}

		// Token: 0x060001DC RID: 476 RVA: 0x000096DC File Offset: 0x00007ADC
		public int GetLoadedData(string file, ref string data, bool safe = true)
		{
			if (!this.IsBusy || !safe)
			{
				byte[] array = null;
				if (this.GetLoadedData(file, ref array, false, safe) != 0)
				{
					char[] array2 = new char[array.Length / 2];
					Buffer.BlockCopy(array, 0, array2, 0, array.Length);
					data = new string(array2);
					return data.Length;
				}
			}
			return 0;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00009734 File Offset: 0x00007B34
		public int GetLoadedData(string fileName, ref byte[] data, bool copy, bool safe = true)
		{
			if ((!this.IsBusy || !safe) && this._storedData.ContainsKey(fileName) && this._storedData[fileName] != null)
			{
				if (copy)
				{
					data = new byte[this._storedData[fileName].Length];
					Buffer.BlockCopy(this._storedData[fileName], 0, data, 0, this._storedData[fileName].Length);
				}
				else
				{
					data = this._storedData[fileName];
				}
				return data.Length;
			}
			return 0;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x000097CC File Offset: 0x00007BCC
		public bool AddSaveData(string file, string data, bool safe = true)
		{
			if (this.IsBusy && safe)
			{
				return false;
			}
			if (string.IsNullOrEmpty(data))
			{
				return this.AddSaveData(file, null, 0, safe, true);
			}
			byte[] array = new byte[data.Length * 2];
			Buffer.BlockCopy(data.ToCharArray(), 0, array, 0, array.Length);
			return this.AddSaveData(file, array, array.Length, false, safe);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00009830 File Offset: 0x00007C30
		public bool AddSaveData(string fileName, byte[] data, int amount = 0, bool copy = false, bool safe = true)
		{
			if (amount == 0 && data != null)
			{
				amount = data.Length;
			}
			if (!this.IsBusy || !safe)
			{
				if (amount == 0)
				{
					this.LogicSaveData(fileName, null);
				}
				else
				{
					byte[] array = data;
					if (copy || data.Length != amount)
					{
						array = new byte[amount];
						Buffer.BlockCopy(data, 0, array, 0, amount);
					}
					this.LogicSaveData(fileName, array);
				}
				if (!this._dirtyFiles.Contains(fileName))
				{
					this._dirtyFiles.Add(fileName);
				}
				return true;
			}
			return false;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x000098C0 File Offset: 0x00007CC0
		protected void LogicSaveData(string file, byte[] data)
		{
			object locker = this._locker;
			lock (locker)
			{
				if (this._storedData.ContainsKey(file))
				{
					this._storedData[file] = data;
				}
				else
				{
					this._storedData.Add(file, data);
				}
			}
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00009928 File Offset: 0x00007D28
		protected void LogicAddData(string file, byte[] data)
		{
			object locker = this._locker;
			lock (locker)
			{
				if (this._storedData.ContainsKey(file))
				{
					this._storedData[file] = data;
				}
				else
				{
					this._storedData.Add(file, data);
				}
			}
			PlatformEvents.OffMainThreadDataLoaded(file, data);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00009998 File Offset: 0x00007D98
		protected void ShowMessageFailRetry(bool effect1)
		{
			if (effect1)
			{
				BasePlatformManager.Instance.ShowSystemMessageYESNO(BaseStorage.LOCALRETRYSAVEMESSAGE, new Action(this.SaveData), delegate
				{
					this.ShowMessageCancelSave();
				});
			}
			else
			{
				BasePlatformManager.Instance.ShowSystemMessageYESNO(BaseStorage.LOCALRETRYLOADMESSAGE, new Action(this.LoadData), delegate
				{
					this.ShowMessageContinueWithout(false, false);
				});
			}
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00009A02 File Offset: 0x00007E02
		private void ShowMessageCancelSave()
		{
			BasePlatformManager.Instance.ShowSystemMessageYESNO(BaseStorage.LOCALCANCELSAVEMESSAGE, delegate
			{
				this.FailData(true, true);
			}, delegate
			{
				this.ShowMessageFailRetry(true);
			});
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00009A2C File Offset: 0x00007E2C
		private void ShowMessageContinueWithout(bool save, bool loadcompleteEvent = false)
		{
			if (save)
			{
				BasePlatformManager.Instance.ShowSystemMessageYESNO(BaseStorage.LOCALCONTINUEWITHOUTSAVINGMESSAGE, delegate
				{
					this._storageDisabled = true;
					if (loadcompleteEvent)
					{
						this.CompleteLoadData();
					}
				}, delegate
				{
					this._storageDisabled = false;
					if (loadcompleteEvent)
					{
						this.CompleteLoadData();
					}
				});
			}
			else
			{
				BasePlatformManager.Instance.ShowSystemMessageYESNO(BaseStorage.LOCALCONTINUEWITHOUTLOADINGMESSAGE, delegate
				{
					this.ShowMessageContinueWithout(true, true);
				}, delegate
				{
					this.ShowMessageFailRetry(false);
				});
			}
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00009AA8 File Offset: 0x00007EA8
		protected List<string>.Enumerator GetFiles()
		{
			object locker = this._locker;
			List<string>.Enumerator enumerator;
			lock (locker)
			{
				enumerator = this._dirtyFiles.GetEnumerator();
			}
			return enumerator;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00009AEC File Offset: 0x00007EEC
		protected List<string>.Enumerator GetLoadFiles()
		{
			object locker = this._locker;
			List<string>.Enumerator enumerator;
			lock (locker)
			{
				enumerator = this._loadFiles.GetEnumerator();
			}
			return enumerator;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00009B30 File Offset: 0x00007F30
		protected byte[] GetData(string file)
		{
			object locker = this._locker;
			byte[] result;
			lock (locker)
			{
				if (this._storedData.ContainsKey(file))
				{
					result = this._storedData[file];
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x040000AA RID: 170
		protected bool _hasCompleted = true;

		// Token: 0x040000AB RID: 171
		protected bool _storageDisabled;

		// Token: 0x040000AC RID: 172
		protected Action _waitingAction;

		// Token: 0x040000AD RID: 173
		protected object _locker = new object();

		// Token: 0x040000AE RID: 174
		protected Dictionary<string, byte[]> _storedData = new Dictionary<string, byte[]>();

		// Token: 0x040000AF RID: 175
		protected List<string> _dirtyFiles = new List<string>();

		// Token: 0x040000B0 RID: 176
		protected List<string> _loadFiles = new List<string>();

		// Token: 0x040000B1 RID: 177
		public static string LOCALCONTINUEWITHOUTSAVINGMESSAGE = "CONTINUE WITHOUT SAVING?";

		// Token: 0x040000B2 RID: 178
		public static string LOCALCANCELSAVEMESSAGE = "CANCEL SAVING?";

		// Token: 0x040000B3 RID: 179
		public static string LOCALRETRYSAVEMESSAGE = "RETRY SAVING?";

		// Token: 0x040000B4 RID: 180
		public static string LOCALRETRYLOADMESSAGE = "RETRY LOADING?";

		// Token: 0x040000B5 RID: 181
		public static string LOCALCONTINUEWITHOUTLOADINGMESSAGE = "CONTINUE WITHOUT LOADING?";
	}
}
