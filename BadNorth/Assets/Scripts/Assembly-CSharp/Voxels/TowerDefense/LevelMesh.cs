using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x02000799 RID: 1945
	public class LevelMesh : MonoBehaviour, ILevelComponent
	{
		// Token: 0x06003224 RID: 12836 RVA: 0x000D49AC File Offset: 0x000D2DAC
		public void OnSetLevel(Agent agent, int level)
		{
			base.GetComponent<MeshFilter>().sharedMesh = this.meshes[Mathf.Max(0, level - 1)];
		}

		// Token: 0x04002211 RID: 8721
		public Mesh[] meshes;
	}
}
