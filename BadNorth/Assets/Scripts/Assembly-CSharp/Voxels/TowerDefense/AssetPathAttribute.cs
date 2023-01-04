using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020005F8 RID: 1528
	[AttributeUsage(AttributeTargets.Class)]
	public class AssetPathAttribute : PropertyAttribute
	{
		// Token: 0x0600276C RID: 10092 RVA: 0x0007FA4C File Offset: 0x0007DE4C
		public AssetPathAttribute(string path)
		{
			this.Path = path;
		}

		// Token: 0x04001941 RID: 6465
		public string Path;
	}
}
