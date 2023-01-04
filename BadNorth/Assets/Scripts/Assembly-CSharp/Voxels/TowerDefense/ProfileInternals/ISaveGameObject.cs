using System;

namespace Voxels.TowerDefense.ProfileInternals
{
	// Token: 0x0200058B RID: 1419
	public interface ISaveGameObject
	{
		// Token: 0x060024D6 RID: 9430
		void PostCreate(string fileName);

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x060024D7 RID: 9431
		string fileName { get; }
	}
}
