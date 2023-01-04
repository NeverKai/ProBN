using System;
using UnityEngine;

namespace Voxels.TowerDefense.Forestry
{
	// Token: 0x02000762 RID: 1890
	public class TreeAo : MonoBehaviour, ITreePlanter
	{
		// Token: 0x0600313B RID: 12603 RVA: 0x000CBD68 File Offset: 0x000CA168
		private Bounds GetBounds(Tree tree)
		{
			Vector3 a = new Vector3(this.radMul, this.heightMul - this.bottomMul, this.radMul) * tree.radius;
			a = Vector3.Scale(a, tree.transform.lossyScale);
			return new Bounds(tree.transform.position + Vector3.up * (a.y + this.bottomMul * tree.radius * 2f), a * 2f);
		}

		// Token: 0x0600313C RID: 12604 RVA: 0x000CBDF8 File Offset: 0x000CA1F8
		public void OnTreePlanted(Tree tree, Shoot shoot, Vector3 normal)
		{
			Bounds bounds = this.GetBounds(tree);
			shoot.navPos.navigationMesh.island.voxelSpace.AddCoverage(bounds, this.factor);
		}

		// Token: 0x0600313D RID: 12605 RVA: 0x000CBE30 File Offset: 0x000CA230
		public void OnDrawGizmos()
		{
			Tree component = base.GetComponent<Tree>();
			if (component)
			{
				Bounds bounds = this.GetBounds(component);
				Gizmos.DrawWireCube(bounds.center, bounds.size);
			}
		}

		// Token: 0x04002108 RID: 8456
		public float factor = 0.1f;

		// Token: 0x04002109 RID: 8457
		public float bottomMul;

		// Token: 0x0400210A RID: 8458
		public float heightMul = 1f;

		// Token: 0x0400210B RID: 8459
		public float radMul = 1f;
	}
}
