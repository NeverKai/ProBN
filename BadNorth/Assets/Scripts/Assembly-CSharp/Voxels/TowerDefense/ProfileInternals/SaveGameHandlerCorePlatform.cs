using System;
using System.Collections.Generic;
using System.IO;
using CS.Platform;
using CS.Platform.Utils;
using CS.VT;

namespace Voxels.TowerDefense.ProfileInternals
{
	// Token: 0x0200059B RID: 1435
	public class SaveGameHandlerCorePlatform : ISaveGameHandler
	{
		// Token: 0x0600255D RID: 9565 RVA: 0x00075E6C File Offset: 0x0007426C
		public SaveGameHandlerCorePlatform()
		{
			PlatformEvents.OnLoadLocalFilesCompleteEvent += this.PlatformEvents_OnLoadLocalFilesCompleteEvent;
			PlatformEvents.OnSaveLocalFilesCompleteEvent += this.PlatformEvents_OnSaveLocalFilesCompleteEvent;
			PlatformEvents.OnOffMainThreadDataLoaded += this.PlatformEvents_OnOffMainThreadDataLoaded;
			PlatformEvents.OnSaveLocalCompleteEvent += this.DoNextAction;
			PlatformEvents.OnLoadLocalCompleteEvent += this.DoNextAction;
		}

		// Token: 0x0600255E RID: 9566 RVA: 0x00075EF8 File Offset: 0x000742F8
		private void DoNextAction()
		{
			if (0 < this._todoQueue.Count)
			{
				bool save = this._todoQueue.Peek().save;
				if (save)
				{
					while (0 < this._todoQueue.Count)
					{
						SaveGameHandlerCorePlatform.WaitingAction waitingAction = this._todoQueue.Peek();
						if (save != waitingAction.save)
						{
							break;
						}
						this._todoQueue.Dequeue();
						BasePlatformManager.Instance.Save(waitingAction.file, waitingAction.data, 0, false, true);
						if (waitingAction.callback != null)
						{
							this.AddSaveCallback(waitingAction.file, (Callback<int>)waitingAction.callback);
						}
					}
					BasePlatformManager.Instance.SendSavedData();
				}
				else
				{
					List<string> list = new List<string>();
					while (0 < this._todoQueue.Count)
					{
						SaveGameHandlerCorePlatform.WaitingAction waitingAction = this._todoQueue.Peek();
						if (save != waitingAction.save)
						{
							break;
						}
						this._todoQueue.Dequeue();
						if (waitingAction.callback != null)
						{
							this.AddLoadCallback(waitingAction.file, (SaveGameHandlerCorePlatform.LoadCallback<MemoryStream>)waitingAction.callback);
						}
						if (!list.Contains(waitingAction.file))
						{
							list.Add(waitingAction.file);
						}
					}
					BasePlatformManager.Instance.LoadSavedData(list.ToArray());
				}
			}
		}

		// Token: 0x0600255F RID: 9567 RVA: 0x0007605C File Offset: 0x0007445C
		~SaveGameHandlerCorePlatform()
		{
			PlatformEvents.OnLoadLocalFilesCompleteEvent -= this.PlatformEvents_OnLoadLocalFilesCompleteEvent;
			PlatformEvents.OnSaveLocalFilesCompleteEvent -= this.PlatformEvents_OnSaveLocalFilesCompleteEvent;
			PlatformEvents.OnOffMainThreadDataLoaded -= this.PlatformEvents_OnOffMainThreadDataLoaded;
			PlatformEvents.OnSaveLocalCompleteEvent -= this.DoNextAction;
			PlatformEvents.OnLoadLocalCompleteEvent -= this.DoNextAction;
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06002560 RID: 9568 RVA: 0x000760DC File Offset: 0x000744DC
		public bool Saving
		{
			get
			{
				return BasePlatformManager.Instance && BasePlatformManager.Instance.IsLocalSaving;
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06002561 RID: 9569 RVA: 0x000760FA File Offset: 0x000744FA
		public bool Loading
		{
			get
			{
				return BasePlatformManager.Instance && BasePlatformManager.Instance.IsLocalLoading;
			}
		}

		// Token: 0x06002562 RID: 9570 RVA: 0x00076118 File Offset: 0x00074518
		private void PlatformEvents_OnOffMainThreadDataLoaded(string arg1, byte[] arg2)
		{
			if (this._waitingLoads.ContainsKey(arg1))
			{
				SaveGameHandlerCorePlatform.LoadCallback<MemoryStream> loadCallback = this._waitingLoads[arg1];
				if (loadCallback != null)
				{
					byte[] array = null;
					BasePlatformManager.Instance.Load(arg1, ref array, false, false);
					if (array != null)
					{
						loadCallback.Result = new MemoryStream(array);
					}
					if (loadCallback.OnComplete == new Action<Callback<MemoryStream>>(this.CompleteMetaLoad))
					{
						SaveGameHandlerCorePlatform.LoadMetaCallback loadMetaCallback = (SaveGameHandlerCorePlatform.LoadMetaCallback)loadCallback;
						if (loadMetaCallback != null)
						{
							if (loadMetaCallback.offthreadCallback != null)
							{
								try
								{
									loadMetaCallback.element = loadMetaCallback.offthreadCallback(loadMetaCallback.Result);
								}
								catch (Exception ex)
								{
									Debug.ThreadLogError("[SGH] offthreadCallback failed: {0}", new object[]
									{
										ex.Message
									});
								}
							}
							this._waitingMeta.Result[loadMetaCallback.slot] = loadMetaCallback.element;
						}
						else
						{
							Debug.ThreadLogError("[SGH] offthreadCallback failed: OnComplete set to CompleteMetaLoad when not a meta callback | File: {0}", new object[]
							{
								arg1
							});
						}
					}
					else
					{
						try
						{
							if (loadCallback.offthreadCallback != null)
							{
								loadCallback.offthreadCallback(loadCallback.Result);
							}
						}
						catch (Exception ex2)
						{
							Debug.ThreadLogError("[SGH] offthreadCallback failed | File: {0} | Message: {1}", new object[]
							{
								arg1,
								ex2.Message
							});
						}
					}
				}
			}
		}

		// Token: 0x06002563 RID: 9571 RVA: 0x00076278 File Offset: 0x00074678
		private void PlatformEvents_OnLoadLocalFilesCompleteEvent(string[] obj)
		{
			if (obj == null)
			{
				return;
			}
			for (int i = 0; i < obj.Length; i++)
			{
				if (this._waitingLoads.ContainsKey(obj[i]))
				{
					Callback<MemoryStream> callback = this._waitingLoads[obj[i]];
					if (callback != null)
					{
						callback.State = Callback<MemoryStream>.CallbackState.Complete;
					}
					this._waitingLoads.Remove(obj[i]);
				}
				BasePlatformManager.Instance.ForgetFile(obj[i]);
			}
		}

		// Token: 0x06002564 RID: 9572 RVA: 0x000762EC File Offset: 0x000746EC
		private void PlatformEvents_OnSaveLocalFilesCompleteEvent(string[] obj)
		{
			if (obj == null)
			{
				return;
			}
			for (int i = 0; i < obj.Length; i++)
			{
				if (this._waitingSaves.ContainsKey(obj[i]))
				{
					Callback<int> callback = this._waitingSaves[obj[i]];
					if (callback != null)
					{
						try
						{
							callback.Result = 0;
							callback.State = Callback<int>.CallbackState.Complete;
						}
						catch (Exception ex)
						{
							Debug.LogError("[SGH] OnSaveLocalFilesCompleteEvent CallbackComplete Failed | {0}", new object[]
							{
								ex.ToString()
							});
						}
					}
					this._waitingSaves.Remove(obj[i]);
				}
				BasePlatformManager.Instance.ForgetFile(obj[i]);
			}
		}

		// Token: 0x06002565 RID: 9573 RVA: 0x0007639C File Offset: 0x0007479C
		private void AddSaveCallback(string file, Callback<int> callback)
		{
			if (this._waitingSaves.ContainsKey(file))
			{
				this._waitingSaves[file].Result = -2;
				this._waitingSaves[file].State = Callback<int>.CallbackState.Failed;
				this._waitingSaves[file] = callback;
			}
			else
			{
				this._waitingSaves.Add(file, callback);
			}
		}

		// Token: 0x06002566 RID: 9574 RVA: 0x00076400 File Offset: 0x00074800
		private void AddLoadCallback(string file, SaveGameHandlerCorePlatform.LoadCallback<MemoryStream> callback)
		{
			if (this._waitingSaves.ContainsKey(file))
			{
				this._waitingLoads[file].Result = null;
				this._waitingLoads[file].State = Callback<MemoryStream>.CallbackState.Failed;
				this._waitingLoads[file] = callback;
			}
			else
			{
				this._waitingLoads.Add(file, callback);
			}
		}

		// Token: 0x06002567 RID: 9575 RVA: 0x00076464 File Offset: 0x00074864
		public Callback<int> Save(string filename, MemoryStream data, Action<Callback<int>> completionCallback)
		{
			if (BasePlatformManager.Instance == null)
			{
				return null;
			}
			Callback<int> callback = new Callback<int>();
			callback.Result = -1;
			callback.OnComplete = completionCallback;
			this._todoQueue.Enqueue(new SaveGameHandlerCorePlatform.WaitingAction(filename, (data == null) ? null : data.GetBuffer(), callback));
			if (!BasePlatformManager.Instance.IsLocalLoading && !BasePlatformManager.Instance.IsLocalSaving)
			{
				this.DoNextAction();
			}
			return callback;
		}

		// Token: 0x06002568 RID: 9576 RVA: 0x000764E0 File Offset: 0x000748E0
		public Callback<int>[] Save(string[] filename, MemoryStream[] data, Action<Callback<int>>[] completionCallback)
		{
			if (filename == null)
			{
				return null;
			}
			Callback<int>[] array = new Callback<int>[filename.Length];
			if (BasePlatformManager.Instance == null)
			{
				return null;
			}
			for (int i = 0; i < filename.Length; i++)
			{
				array[i] = new Callback<int>();
				array[i].Result = -1;
				if (completionCallback != null && i < completionCallback.Length)
				{
					array[i].OnComplete = completionCallback[i];
				}
				if (data != null && i < data.Length && data[i] != null)
				{
					this._todoQueue.Enqueue(new SaveGameHandlerCorePlatform.WaitingAction(filename[i], data[i].GetBuffer(), array[i]));
				}
				else
				{
					this._todoQueue.Enqueue(new SaveGameHandlerCorePlatform.WaitingAction(filename[i], null, array[i]));
				}
			}
			if (!BasePlatformManager.Instance.IsLocalLoading && !BasePlatformManager.Instance.IsLocalSaving)
			{
				this.DoNextAction();
			}
			return array;
		}

		// Token: 0x06002569 RID: 9577 RVA: 0x000765C8 File Offset: 0x000749C8
		public Callback<int> DeleteFile(string fileName)
		{
			if (BasePlatformManager.Instance == null)
			{
				return null;
			}
			Callback<int> callback = new Callback<int>();
			callback.Result = -1;
			this._todoQueue.Enqueue(new SaveGameHandlerCorePlatform.WaitingAction(fileName, null, callback));
			if (!BasePlatformManager.Instance.IsLocalLoading && !BasePlatformManager.Instance.IsLocalSaving)
			{
				this.DoNextAction();
			}
			return callback;
		}

		// Token: 0x0600256A RID: 9578 RVA: 0x0007662C File Offset: 0x00074A2C
		public Callback<MemoryStream> Load(string filename, Action<Callback<MemoryStream>> completionCallback, Action<MemoryStream> offthreadCallback)
		{
			if (BasePlatformManager.Instance == null)
			{
				return null;
			}
			SaveGameHandlerCorePlatform.LoadCallback<MemoryStream> loadCallback = new SaveGameHandlerCorePlatform.LoadCallback<MemoryStream>();
			loadCallback.Result = null;
			loadCallback.offthreadCallback = offthreadCallback;
			loadCallback.OnComplete = completionCallback;
			this._todoQueue.Enqueue(new SaveGameHandlerCorePlatform.WaitingAction(filename, loadCallback));
			if (!BasePlatformManager.Instance.IsLocalLoading && !BasePlatformManager.Instance.IsLocalSaving)
			{
				this.DoNextAction();
			}
			return loadCallback;
		}

		// Token: 0x0600256B RID: 9579 RVA: 0x000766A0 File Offset: 0x00074AA0
		public Callback<MemoryStream>[] Load(string[] filename, Action<Callback<MemoryStream>>[] completionCallback, Action<MemoryStream>[] offThreadCallback)
		{
			if (BasePlatformManager.Instance == null)
			{
				return null;
			}
			SaveGameHandlerCorePlatform.LoadCallback<MemoryStream>[] array = new SaveGameHandlerCorePlatform.LoadCallback<MemoryStream>[filename.Length];
			for (int i = 0; i < filename.Length; i++)
			{
				array[i] = new SaveGameHandlerCorePlatform.LoadCallback<MemoryStream>();
				array[i].Result = null;
				if (completionCallback != null && i < completionCallback.Length)
				{
					array[i].OnComplete = completionCallback[i];
				}
				if (offThreadCallback != null && i < offThreadCallback.Length)
				{
					array[i].offthreadCallback = offThreadCallback[i];
				}
				this._todoQueue.Enqueue(new SaveGameHandlerCorePlatform.WaitingAction(filename[i], array[i]));
			}
			if (!BasePlatformManager.Instance.IsLocalLoading && !BasePlatformManager.Instance.IsLocalSaving)
			{
				this.DoNextAction();
			}
			return array;
		}

		// Token: 0x0600256C RID: 9580 RVA: 0x0007675C File Offset: 0x00074B5C
		public Callback<List<ISaveGameObject>> GetHeaders(int slots, Action<Callback<List<ISaveGameObject>>> completionCallback, Func<MemoryStream, ISaveGameObject> offThreadCallback)
		{
			if (this._waitingMeta == null)
			{
				this._metaReturnCounter = 0;
				string[] array = new string[slots];
				List<ISaveGameObject> list = new List<ISaveGameObject>();
				for (int i = 0; i < slots; i++)
				{
					array[i] = CampaignSave.FileNameGen(i) + ".meta";
					SaveGameHandlerCorePlatform.LoadMetaCallback loadMetaCallback = new SaveGameHandlerCorePlatform.LoadMetaCallback();
					loadMetaCallback.Result = null;
					loadMetaCallback.OnComplete = new Action<Callback<MemoryStream>>(this.CompleteMetaLoad);
					loadMetaCallback.offthreadCallback = offThreadCallback;
					loadMetaCallback.slot = i;
					this._metaReturnCounter++;
					this.AddLoadCallback(array[i], loadMetaCallback);
					list.Add(null);
				}
				BasePlatformManager.Instance.LoadSavedData(array);
				this._waitingMeta = new Callback<List<ISaveGameObject>>();
				this._waitingMeta.Result = list;
				this._waitingMeta.OnComplete = completionCallback;
			}
			return this._waitingMeta;
		}

		// Token: 0x0600256D RID: 9581 RVA: 0x00076830 File Offset: 0x00074C30
		private void CompleteMetaLoad(Callback<MemoryStream> callback)
		{
			this._metaReturnCounter--;
			if (this._metaReturnCounter == 0)
			{
				Callback<List<ISaveGameObject>> waitingMeta = this._waitingMeta;
				this._waitingMeta = null;
				waitingMeta.State = Callback<List<ISaveGameObject>>.CallbackState.Complete;
			}
		}

		// Token: 0x040017BF RID: 6079
		private Queue<SaveGameHandlerCorePlatform.WaitingAction> _todoQueue = new Queue<SaveGameHandlerCorePlatform.WaitingAction>();

		// Token: 0x040017C0 RID: 6080
		private Dictionary<string, SaveGameHandlerCorePlatform.LoadCallback<MemoryStream>> _waitingLoads = new Dictionary<string, SaveGameHandlerCorePlatform.LoadCallback<MemoryStream>>();

		// Token: 0x040017C1 RID: 6081
		private Dictionary<string, Callback<int>> _waitingSaves = new Dictionary<string, Callback<int>>();

		// Token: 0x040017C2 RID: 6082
		private int _metaReturnCounter;

		// Token: 0x040017C3 RID: 6083
		private Func<MemoryStream, ISaveGameObject> _offthreadMetaConveter;

		// Token: 0x040017C4 RID: 6084
		private Callback<List<ISaveGameObject>> _waitingMeta;

		// Token: 0x0200059C RID: 1436
		private struct WaitingAction
		{
			// Token: 0x0600256E RID: 9582 RVA: 0x0007686B File Offset: 0x00074C6B
			public WaitingAction(string file, byte[] data, object callback)
			{
				this.save = true;
				this.file = file;
				this.data = data;
				this.callback = callback;
			}

			// Token: 0x0600256F RID: 9583 RVA: 0x00076889 File Offset: 0x00074C89
			public WaitingAction(string file, object callback)
			{
				this.save = false;
				this.file = file;
				this.data = null;
				this.callback = callback;
			}

			// Token: 0x040017C5 RID: 6085
			public bool save;

			// Token: 0x040017C6 RID: 6086
			public string file;

			// Token: 0x040017C7 RID: 6087
			public byte[] data;

			// Token: 0x040017C8 RID: 6088
			public object callback;
		}

		// Token: 0x0200059D RID: 1437
		private class LoadCallback<T> : Callback<T>
		{
			// Token: 0x040017C9 RID: 6089
			public Action<T> offthreadCallback;
		}

		// Token: 0x0200059E RID: 1438
		private class LoadMetaCallback : SaveGameHandlerCorePlatform.LoadCallback<MemoryStream>
		{
			// Token: 0x040017CA RID: 6090
			public int slot;

			// Token: 0x040017CB RID: 6091
			public new Func<MemoryStream, ISaveGameObject> offthreadCallback;

			// Token: 0x040017CC RID: 6092
			public ISaveGameObject element;
		}
	}
}
