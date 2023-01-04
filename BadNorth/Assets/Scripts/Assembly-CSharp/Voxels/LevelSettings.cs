using System;
using UnityEngine;

namespace Voxels
{
	// Token: 0x0200061F RID: 1567
	[Serializable]
	public class LevelSettings
	{
		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06002855 RID: 10325 RVA: 0x00085628 File Offset: 0x00083A28
		public string key
		{
			get
			{
				return this.size.ToString("F0");
			}
		}

		// Token: 0x040019DA RID: 6618
		public Vector3 size = new Vector3(7f, 11f, 7f);
	}
}
