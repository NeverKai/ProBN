using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace SpriteComposing
{
	// Token: 0x020005CE RID: 1486
	[RequireComponent(typeof(SpriteRenderer))]
	public class SpriteRenderProxy : MonoBehaviour, ISpritePart
	{
		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x060026B8 RID: 9912 RVA: 0x0007B487 File Offset: 0x00079887
		private Material material
		{
			get
			{
				return this.sr.sharedMaterial;
			}
		}

		// Token: 0x060026B9 RID: 9913 RVA: 0x0007B494 File Offset: 0x00079894
		void ISpritePart.Draw(CommandBuffer buffer, Matrix4x4 matrix)
		{
			if (!this.sr)
			{
				this.sr = base.gameObject.GetComponent<SpriteRenderer>();
			}
			if (!this.sr.enabled)
			{
				return;
			}
			if (SpriteRenderProxy.block == null)
			{
				SpriteRenderProxy.block = new MaterialPropertyBlock();
			}
			if (this.sr && this.sr.sprite)
			{
				SpriteRenderProxy.block.SetColor(ShaderId.colorId, this.sr.color);
				SpriteRenderProxy.block.SetTexture(ShaderId.mainTexId, this.sr.sprite.texture);
				Mesh mesh = SpriteMeshDictionary.GetMesh(this.sr.sprite);
				buffer.DrawMesh(mesh, matrix * this.sr.transform.localToWorldMatrix, this.sr.sharedMaterial, 0, 0, SpriteRenderProxy.block);
			}
		}

		// Token: 0x060026BA RID: 9914 RVA: 0x0007B58F File Offset: 0x0007998F
		private void OnDestroy()
		{
		}

		// Token: 0x040018B0 RID: 6320
		private SpriteRenderer sr;

		// Token: 0x040018B1 RID: 6321
		private static MaterialPropertyBlock block;
	}
}
