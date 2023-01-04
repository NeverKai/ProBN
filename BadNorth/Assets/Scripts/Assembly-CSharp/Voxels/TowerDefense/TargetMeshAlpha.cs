using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x0200089F RID: 2207
	internal class TargetMeshAlpha : MonoBehaviour, TargetMesh.IMeshComponent
	{
		// Token: 0x060039BB RID: 14779 RVA: 0x000FC9E0 File Offset: 0x000FADE0
		void TargetMesh.IMeshComponent.Init(TargetMesh owner)
		{
			this.owner = owner;
			this.baseAlpha = this.alphaRenderer.sharedMaterial.GetColor(TargetMeshAlpha.colorId).a;
			this.block = new MaterialPropertyBlock();
		}

		// Token: 0x060039BC RID: 14780 RVA: 0x000FCA27 File Offset: 0x000FAE27
		void TargetMesh.IMeshComponent.SetMesh(Mesh mesh)
		{
		}

		// Token: 0x060039BD RID: 14781 RVA: 0x000FCA2C File Offset: 0x000FAE2C
		private void Update()
		{
			Color color = this.alphaRenderer.sharedMaterial.GetColor(TargetMeshAlpha.colorId);
			float num = Mathf.Lerp(1f, this.flashTargetAlpha, this.owner.flashFraction);
			float num2 = this.owner.visibilityFraction * num * Mathf.Lerp(1f, this.hoverTargetAlpha, this.owner.hoverFraction);
			color.a = num2 * this.baseAlpha;
			this.block.SetColor(TargetMeshAlpha.colorId, color);
			this.alphaRenderer.SetPropertyBlock(this.block);
		}

		// Token: 0x060039BE RID: 14782 RVA: 0x000FCAD0 File Offset: 0x000FAED0
		private void Reset()
		{
			this.alphaRenderer = base.GetComponent<MeshRenderer>();
		}

		// Token: 0x040027CE RID: 10190
		[Header("References")]
		[SerializeField]
		private MeshRenderer alphaRenderer;

		// Token: 0x040027CF RID: 10191
		[Header("Settings")]
		[SerializeField]
		private float flashTargetAlpha = 2.25f;

		// Token: 0x040027D0 RID: 10192
		[SerializeField]
		private float hoverTargetAlpha = 4f;

		// Token: 0x040027D1 RID: 10193
		private TargetMesh owner;

		// Token: 0x040027D2 RID: 10194
		private float baseAlpha = 1f;

		// Token: 0x040027D3 RID: 10195
		private static ShaderId colorId = "_Color";

		// Token: 0x040027D4 RID: 10196
		private MaterialPropertyBlock block;
	}
}
