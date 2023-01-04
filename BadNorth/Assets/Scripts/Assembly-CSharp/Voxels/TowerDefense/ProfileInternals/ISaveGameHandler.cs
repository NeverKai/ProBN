using System;
using System.Collections.Generic;
using System.IO;
using CS.VT;

namespace Voxels.TowerDefense.ProfileInternals
{
	// Token: 0x0200058A RID: 1418
	public interface ISaveGameHandler
	{
		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x060024CE RID: 9422
		bool Saving { get; }

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x060024CF RID: 9423
		bool Loading { get; }

		// Token: 0x060024D0 RID: 9424
		Callback<int> Save(string filename, MemoryStream data, Action<Callback<int>> completionCallback);

		// Token: 0x060024D1 RID: 9425
		Callback<int>[] Save(string[] filename, MemoryStream[] data, Action<Callback<int>>[] completionCallback);

		// Token: 0x060024D2 RID: 9426
		Callback<MemoryStream> Load(string filename, Action<Callback<MemoryStream>> completionCallback, Action<MemoryStream> offThreadCallback = null);

		// Token: 0x060024D3 RID: 9427
		Callback<MemoryStream>[] Load(string[] filename, Action<Callback<MemoryStream>>[] completionCallback, Action<MemoryStream>[] offThreadCallback = null);

		// Token: 0x060024D4 RID: 9428
		Callback<List<ISaveGameObject>> GetHeaders(int slots, Action<Callback<List<ISaveGameObject>>> completionCallback, Func<MemoryStream, ISaveGameObject> offThreadCallback);

		// Token: 0x060024D5 RID: 9429
		Callback<int> DeleteFile(string fileName);
	}
}
