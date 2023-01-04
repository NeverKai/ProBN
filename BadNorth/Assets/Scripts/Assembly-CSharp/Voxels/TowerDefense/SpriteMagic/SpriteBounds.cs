using System;
using UnityEngine;

namespace Voxels.TowerDefense.SpriteMagic
{
	// Token: 0x020007D7 RID: 2007
	public struct SpriteBounds
	{
		// Token: 0x06003409 RID: 13321 RVA: 0x000E0DC0 File Offset: 0x000DF1C0
		public SpriteBounds(Sprite sprite)
		{
			this.uv = SpriteBounds.GetUVRect(sprite);
			Vector2[] vertices = sprite.vertices;
			this.v = new Rect(vertices[0], Vector2.zero);
			for (int i = 1; i < vertices.Length; i++)
			{
				this.v.min = Vector2.Min(this.v.min, vertices[i]);
				this.v.max = Vector2.Max(this.v.max, vertices[i]);
			}
			this.hash = sprite.GetHashCode();
		}

		// Token: 0x0600340A RID: 13322 RVA: 0x000E0E6C File Offset: 0x000DF26C
		public Vector2[] GetUvCorners()
		{
			return new Vector2[]
			{
				new Vector2(this.uv.min.x, this.uv.min.y),
				new Vector2(this.uv.max.x, this.uv.min.y),
				new Vector2(this.uv.min.x, this.uv.max.y),
				new Vector2(this.uv.max.x, this.uv.max.y)
			};
		}

		// Token: 0x0600340B RID: 13323 RVA: 0x000E0F60 File Offset: 0x000DF360
		public Vector2[] GetUv2Corners()
		{
			return new Vector2[]
			{
				new Vector2(this.v.min.x, this.v.min.y),
				new Vector2(this.v.max.x, this.v.min.y),
				new Vector2(this.v.min.x, this.v.max.y),
				new Vector2(this.v.max.x, this.v.max.y)
			};
		}

		// Token: 0x0600340C RID: 13324 RVA: 0x000E1054 File Offset: 0x000DF454
		public Vector2 GetUvCorner(int index)
		{
			switch (index)
			{
			case 0:
				return new Vector2(this.uv.min.x, this.uv.min.y);
			case 1:
				return new Vector2(this.uv.max.x, this.uv.min.y);
			case 2:
				return new Vector2(this.uv.min.x, this.uv.max.y);
			default:
				return new Vector2(this.uv.max.x, this.uv.max.y);
			}
		}

		// Token: 0x0600340D RID: 13325 RVA: 0x000E112C File Offset: 0x000DF52C
		public Vector2 GetUv2Corner(int index)
		{
			switch (index)
			{
			case 0:
				return new Vector2(this.v.min.x, this.v.min.y);
			case 1:
				return new Vector2(this.v.max.x, this.v.min.y);
			case 2:
				return new Vector2(this.v.min.x, this.v.max.y);
			default:
				return new Vector2(this.v.max.x, this.v.max.y);
			}
		}

		// Token: 0x0600340E RID: 13326 RVA: 0x000E1204 File Offset: 0x000DF604
		public void SetUvCorner(Vector2[] array, int offset = 0)
		{
			array[offset].x = this.uv.min.x;
			array[offset].y = this.uv.min.y;
			array[1 + offset].x = this.uv.max.x;
			array[1 + offset].y = this.uv.min.y;
			array[2 + offset].x = this.uv.min.x;
			array[2 + offset].y = this.uv.max.y;
			array[3 + offset].x = this.uv.max.x;
			array[3 + offset].y = this.uv.max.y;
		}

		// Token: 0x0600340F RID: 13327 RVA: 0x000E131C File Offset: 0x000DF71C
		public void SetUv2Corner(Vector2[] array, int offset = 0)
		{
			array[offset].x = this.v.min.x;
			array[offset].y = this.v.min.y;
			array[1 + offset].x = this.v.max.x;
			array[1 + offset].y = this.v.min.y;
			array[2 + offset].x = this.v.min.x;
			array[2 + offset].y = this.v.max.y;
			array[3 + offset].x = this.v.max.x;
			array[3 + offset].y = this.v.max.y;
		}

		// Token: 0x06003410 RID: 13328 RVA: 0x000E1434 File Offset: 0x000DF834
		public static Rect GetUVRect(Sprite sprite)
		{
			Rect textureRect = sprite.textureRect;
			Vector2 texelSize = sprite.texture.texelSize;
			return new Rect(textureRect.xMin * texelSize.x, textureRect.yMin * texelSize.y, textureRect.width * texelSize.x, textureRect.height * texelSize.y);
		}

		// Token: 0x04002376 RID: 9078
		public Rect v;

		// Token: 0x04002377 RID: 9079
		public Rect uv;

		// Token: 0x04002378 RID: 9080
		public int hash;
	}
}
