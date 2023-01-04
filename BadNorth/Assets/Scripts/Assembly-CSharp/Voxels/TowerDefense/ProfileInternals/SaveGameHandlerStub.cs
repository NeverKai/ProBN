using System;
using System.Collections.Generic;
using System.IO;
using CS.VT;
using UnityEngine;

namespace Voxels.TowerDefense.ProfileInternals
{
	// Token: 0x0200059F RID: 1439
	public class SaveGameHandlerStub : ISaveGameHandler
	{
		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06002573 RID: 9587 RVA: 0x000768BF File Offset: 0x00074CBF
		public bool Saving
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06002574 RID: 9588 RVA: 0x000768C2 File Offset: 0x00074CC2
		public bool Loading
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002575 RID: 9589 RVA: 0x000768C5 File Offset: 0x00074CC5
		private void LogFormat(string fmt, params object[] args)
		{
			if (this.verbose)
			{
				Debug.LogFormat(fmt, args);
			}
		}

		// Token: 0x06002576 RID: 9590 RVA: 0x000768DC File Offset: 0x00074CDC
		public Callback<int> Save(string filename, MemoryStream data, Action<Callback<int>> completionCallback)
		{
			Callback<int> callback = new Callback<int>();
			callback.OnComplete = completionCallback;
			this.LogFormat("SaveGameHandlerStub.Save(\"{0}\");", new object[]
			{
				filename
			});
			callback.Result = 0;
			callback.State = Callback<int>.CallbackState.Complete;
			return callback;
		}

		// Token: 0x06002577 RID: 9591 RVA: 0x0007691C File Offset: 0x00074D1C
		public Callback<int>[] Save(string[] filename, MemoryStream[] data, Action<Callback<int>>[] completionCallback)
		{
			if (filename == null)
			{
				return null;
			}
			Callback<int>[] array = new Callback<int>[filename.Length];
			for (int i = 0; i < filename.Length; i++)
			{
				array[i] = new Callback<int>();
				if (completionCallback != null && i < completionCallback.Length)
				{
					array[i].OnComplete = completionCallback[i];
				}
				this.LogFormat("SaveGameHandlerStub.Saveheader(\"{0}\");", filename);
				array[i].Result = 0;
				array[i].State = Callback<int>.CallbackState.Complete;
			}
			return array;
		}

		// Token: 0x06002578 RID: 9592 RVA: 0x00076990 File Offset: 0x00074D90
		public Callback<MemoryStream> Load(string filename, Action<Callback<MemoryStream>> completionCallback, Action<MemoryStream> offThreadCallback)
		{
			Callback<MemoryStream> callback = new Callback<MemoryStream>();
			callback.OnComplete = completionCallback;
			this.LogFormat("SaveGameHandlerStub.Load(\"{0}\");", new object[]
			{
				filename
			});
			callback.Result = null;
			callback.State = Callback<MemoryStream>.CallbackState.Complete;
			return callback;
		}

		// Token: 0x06002579 RID: 9593 RVA: 0x000769D0 File Offset: 0x00074DD0
		public Callback<MemoryStream>[] Load(string[] filename, Action<Callback<MemoryStream>>[] completionCallback, Action<MemoryStream>[] offThreadCallback)
		{
			if (filename == null)
			{
				return null;
			}
			Callback<MemoryStream>[] array = new Callback<MemoryStream>[filename.Length];
			if (offThreadCallback != null)
			{
				for (int i = 0; i < filename.Length; i++)
				{
					if (i < offThreadCallback.Length && offThreadCallback[i] != null)
					{
						offThreadCallback[i](null);
					}
				}
			}
			for (int j = 0; j < filename.Length; j++)
			{
				array[j] = new Callback<MemoryStream>();
				if (completionCallback != null && j < completionCallback.Length)
				{
					array[j].OnComplete = completionCallback[j];
				}
				this.LogFormat("SaveGameHandlerStub.Load(\"{0}\");", filename);
				array[j].Result = null;
				array[j].State = Callback<MemoryStream>.CallbackState.Complete;
			}
			return array;
		}

		// Token: 0x0600257A RID: 9594 RVA: 0x00076A78 File Offset: 0x00074E78
		public Callback<List<ISaveGameObject>> GetHeaders(int slots, Action<Callback<List<ISaveGameObject>>> completionCallback, Func<MemoryStream, ISaveGameObject> offThreadCallback)
		{
			Callback<List<ISaveGameObject>> callback = new Callback<List<ISaveGameObject>>();
			callback.OnComplete = completionCallback;
			this.LogFormat("SaveGameHandlerStub.GetHeaders();", new object[0]);
			callback.Result = new List<ISaveGameObject>();
			for (int i = 0; i < slots; i++)
			{
				callback.Result.Add(null);
			}
			callback.State = Callback<List<ISaveGameObject>>.CallbackState.Complete;
			return callback;
		}

		// Token: 0x0600257B RID: 9595 RVA: 0x00076AD4 File Offset: 0x00074ED4
		public Callback<int> DeleteFile(string fileName)
		{
			Callback<int> callback = new Callback<int>();
			this.LogFormat("SaveGameHandlerStub.DeleteFile(\"{0}\");", new object[]
			{
				fileName
			});
			callback.Result = 0;
			callback.State = Callback<int>.CallbackState.Complete;
			return callback;
		}

		// Token: 0x040017CD RID: 6093
		private readonly bool verbose;
	}
}
