using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020006A2 RID: 1698
	public class RandomMesh : AgentComponent
	{
		// Token: 0x06002BE3 RID: 11235 RVA: 0x000A1F1C File Offset: 0x000A031C
		public override void Setup()
		{
			base.Setup();
			base.GetComponent<MeshFilter>().sharedMesh = this.meshes[UnityEngine.Random.Range(0, this.meshes.Length)];
		}

		// Token: 0x04001CA2 RID: 7330
		[SerializeField]
		private Mesh[] meshes;
	}
}
