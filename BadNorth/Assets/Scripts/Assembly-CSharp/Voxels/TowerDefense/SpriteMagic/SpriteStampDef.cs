using System;
using UnityEngine;

namespace Voxels.TowerDefense.SpriteMagic
{
	// Token: 0x020007D8 RID: 2008
	public struct SpriteStampDef
	{
		// Token: 0x06003411 RID: 13329 RVA: 0x000E1498 File Offset: 0x000DF898
		public SpriteStampDef(SpriteRenderer sr, NavPos navPos)
		{
			this.matrix = sr.localToWorldMatrix;
			this.color = sr.color;
			this.material = sr.sharedMaterial;
			this.sprite = sr.sprite;
			this.layer = sr.gameObject.layer;
			this.flipX = sr.flipX;
			this.flipY = sr.flipY;
			this.navPos = navPos;
		}

		// Token: 0x06003412 RID: 13330 RVA: 0x000E1505 File Offset: 0x000DF905
		public SpriteStampDef(SpriteRenderer sr)
		{
			this = new SpriteStampDef(sr, NavPos.empty);
		}

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x06003413 RID: 13331 RVA: 0x000E1513 File Offset: 0x000DF913
		public Texture texture
		{
			get
			{
				return (!this.sprite) ? null : this.sprite.texture;
			}
		}

		// Token: 0x06003414 RID: 13332 RVA: 0x000E1536 File Offset: 0x000DF936
		public int GetKey()
		{
			return this.material.GetHashCode() + this.texture.GetHashCode() + this.layer;
		}

		// Token: 0x04002379 RID: 9081
		public Matrix4x4 matrix;

		// Token: 0x0400237A RID: 9082
		public Color color;

		// Token: 0x0400237B RID: 9083
		public NavPos navPos;

		// Token: 0x0400237C RID: 9084
		public Material material;

		// Token: 0x0400237D RID: 9085
		public Sprite sprite;

		// Token: 0x0400237E RID: 9086
		public int layer;

		// Token: 0x0400237F RID: 9087
		public bool flipX;

		// Token: 0x04002380 RID: 9088
		public bool flipY;
	}
}
