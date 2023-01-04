using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020008A0 RID: 2208
	public class TargetMeshCopy : MonoBehaviour, TargetMesh.IMeshComponent
	{
		// Token: 0x060039C1 RID: 14785 RVA: 0x000FCAF7 File Offset: 0x000FAEF7
		void TargetMesh.IMeshComponent.SetMesh(Mesh mesh)
		{
			this.meshFilter.mesh = mesh;
		}

		// Token: 0x060039C2 RID: 14786 RVA: 0x000FCB05 File Offset: 0x000FAF05
		void TargetMesh.IMeshComponent.Init(TargetMesh owner)
		{
		}

		// Token: 0x060039C3 RID: 14787 RVA: 0x000FCB07 File Offset: 0x000FAF07
		private void Reset()
		{
			this.meshFilter = base.GetComponent<MeshFilter>();
		}

		// Token: 0x040027D5 RID: 10197
		[SerializeField]
		private MeshFilter meshFilter;
	}
}
