using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x02000590 RID: 1424
	[ObjectDumper.LeafAttribute]
	[Serializable]
	public struct LevelObjectReference
	{
		// Token: 0x060024F2 RID: 9458 RVA: 0x000740AB File Offset: 0x000724AB
		public LevelObjectReference(LevelObjectReference.Key key, string name)
		{
			this.key = key;
			this.name = name;
		}

		// Token: 0x060024F3 RID: 9459 RVA: 0x000740BB File Offset: 0x000724BB
		public LevelObjectReference(LevelObjectReference.Key key, UnityEngine.Object unityObject)
		{
			this.key = key;
			this.name = unityObject.name;
		}

		// Token: 0x060024F4 RID: 9460 RVA: 0x000740D0 File Offset: 0x000724D0
		public override string ToString()
		{
			return string.Format("{0} - {1}", this.name, this.key);
		}

		// Token: 0x04001796 RID: 6038
		[SerializeField]
		public string name;

		// Token: 0x04001797 RID: 6039
		[SerializeField]
		public LevelObjectReference.Key key;

		// Token: 0x02000591 RID: 1425
		public enum Key
		{
			// Token: 0x04001799 RID: 6041
			Misc,
			// Token: 0x0400179A RID: 6042
			Enemy,
			// Token: 0x0400179B RID: 6043
			Forest,
			// Token: 0x0400179C RID: 6044
			Grass,
			// Token: 0x0400179D RID: 6045
			ShipList,
			// Token: 0x0400179E RID: 6046
			Tiles,
			// Token: 0x0400179F RID: 6047
			IntroMusic
		}
	}
}
