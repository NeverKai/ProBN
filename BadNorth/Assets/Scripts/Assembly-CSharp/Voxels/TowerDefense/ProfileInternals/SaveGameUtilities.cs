using System;
using System.Collections.Generic;
using System.IO;
using CS.VT;
using UnityEngine;

namespace Voxels.TowerDefense.ProfileInternals
{
	// Token: 0x020005A0 RID: 1440
	public static class SaveGameUtilities
	{
		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x0600257D RID: 9597 RVA: 0x00076B17 File Offset: 0x00074F17
		public static ISaveGameHandler handler
		{
			get
			{
				return SaveGameUtilities._handler;
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x0600257E RID: 9598 RVA: 0x00076B1E File Offset: 0x00074F1E
		public static bool Saving
		{
			get
			{
				return SaveGameUtilities._handler.Saving;
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x0600257F RID: 9599 RVA: 0x00076B2A File Offset: 0x00074F2A
		public static bool Loading
		{
			get
			{
				return SaveGameUtilities._handler.Loading;
			}
		}

		// Token: 0x06002580 RID: 9600 RVA: 0x00076B38 File Offset: 0x00074F38
		public static Callback<int> Save(string file, MemoryStream data, Action<Callback<int>> completionCallback)
		{
			Callback<int> result;
			try
			{
				result = SaveGameUtilities._handler.Save(file, data, completionCallback);
			}
			catch (Exception e)
			{
				SaveGameUtilities.ShowErrorMessage(e);
				result = null;
			}
			return result;
		}

		// Token: 0x06002581 RID: 9601 RVA: 0x00076B78 File Offset: 0x00074F78
		public static Callback<int>[] Save(string[] file, MemoryStream[] data, Action<Callback<int>>[] completionCallback)
		{
			Callback<int>[] result;
			try
			{
				result = SaveGameUtilities._handler.Save(file, data, completionCallback);
			}
			catch (Exception e)
			{
				SaveGameUtilities.ShowErrorMessage(e);
				result = null;
			}
			return result;
		}

		// Token: 0x06002582 RID: 9602 RVA: 0x00076BB8 File Offset: 0x00074FB8
		public static Callback<MemoryStream> Load(string filename, Action<Callback<MemoryStream>> completionCallback, Action<MemoryStream> offthreadCallback)
		{
			Callback<MemoryStream> result;
			try
			{
				result = SaveGameUtilities._handler.Load(filename, completionCallback, offthreadCallback);
			}
			catch (Exception e)
			{
				SaveGameUtilities.ShowErrorMessage(e);
				result = new Callback<MemoryStream>
				{
					State = Callback<MemoryStream>.CallbackState.Failed
				};
			}
			return result;
		}

		// Token: 0x06002583 RID: 9603 RVA: 0x00076C04 File Offset: 0x00075004
		public static Callback<MemoryStream>[] Load(string[] filename, Action<Callback<MemoryStream>>[] completionCallback, Action<MemoryStream>[] offthreadCallback)
		{
			Callback<MemoryStream>[] result;
			try
			{
				result = SaveGameUtilities._handler.Load(filename, completionCallback, offthreadCallback);
			}
			catch (Exception e)
			{
				SaveGameUtilities.ShowErrorMessage(e);
				result = null;
			}
			return result;
		}

		// Token: 0x06002584 RID: 9604 RVA: 0x00076C44 File Offset: 0x00075044
		public static Callback<List<ISaveGameObject>> GetHeaders(int slots, Action<Callback<List<ISaveGameObject>>> completionCallback, Func<MemoryStream, ISaveGameObject> offThreadCallback)
		{
			Callback<List<ISaveGameObject>> result;
			try
			{
				result = SaveGameUtilities._handler.GetHeaders(slots, completionCallback, offThreadCallback);
			}
			catch (Exception e)
			{
				SaveGameUtilities.ShowErrorMessage(e);
				result = null;
			}
			return result;
		}

		// Token: 0x06002585 RID: 9605 RVA: 0x00076C84 File Offset: 0x00075084
		public static Callback<int> DeleteFile(string fileName)
		{
			Callback<int> result;
			try
			{
				result = SaveGameUtilities._handler.DeleteFile(fileName);
			}
			catch (Exception e)
			{
				SaveGameUtilities.ShowErrorMessage(e);
				result = null;
			}
			return result;
		}

		// Token: 0x06002586 RID: 9606 RVA: 0x00076CC4 File Offset: 0x000750C4
		private static void ShowErrorMessage(Exception e)
		{
			Debug.LogException(e);
		}

		// Token: 0x040017CE RID: 6094
		private static ISaveGameHandler _handler = new SaveGameHandlerCorePlatform();
	}
}
