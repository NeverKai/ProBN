using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x020008B4 RID: 2228
	public class CornerImage : MaskableGraphic
	{
		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x06003A7E RID: 14974 RVA: 0x001033DB File Offset: 0x001017DB
		// (set) Token: 0x06003A7F RID: 14975 RVA: 0x001033E3 File Offset: 0x001017E3
		public Sprite sprite
		{
			get
			{
				return this._sprite;
			}
			set
			{
				this._sprite = value;
				this.SetAllDirty();
			}
		}

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x06003A80 RID: 14976 RVA: 0x001033F2 File Offset: 0x001017F2
		// (set) Token: 0x06003A81 RID: 14977 RVA: 0x001033FA File Offset: 0x001017FA
		public override Color color
		{
			get
			{
				return base.color;
			}
			set
			{
				base.color = value;
				this.SetMaterialDirty();
			}
		}

		// Token: 0x06003A82 RID: 14978 RVA: 0x0010340C File Offset: 0x0010180C
		protected override void OnPopulateMesh(VertexHelper vh)
		{
			base.OnPopulateMesh(vh);
			Rect rect = base.rectTransform.rect;
			float num = this.innerSize;
			float num2 = this.innerSize;
			float num3 = this.outerSize;
			float num4 = this.outerSize;
			float num5;
			float num6;
			Rect rect2;
			if (this._sprite)
			{
				Rect textureRect = this._sprite.textureRect;
				textureRect.xMin += this._sprite.border.x;
				textureRect.yMin += this._sprite.border.y;
				textureRect.xMax -= this._sprite.border.z;
				textureRect.yMax -= this._sprite.border.w;
				if (this.borders)
				{
					if (this.flipX)
					{
						textureRect.width -= this._sprite.border.x;
					}
					else
					{
						textureRect.xMin += this._sprite.border.z;
					}
					if (this.flipY)
					{
						textureRect.height -= this._sprite.border.w;
					}
					else
					{
						textureRect.yMin += this._sprite.border.y;
					}
				}
				CornerImage.Alignment alignment = this.alignment;
				if (alignment != CornerImage.Alignment.Horizontal)
				{
					if (alignment == CornerImage.Alignment.Vertical)
					{
						num = num2 / textureRect.height * textureRect.width;
					}
				}
				else
				{
					num2 = num / textureRect.width * textureRect.height;
				}
				num5 = Mathf.Min(num, rect.width / 2f);
				num6 = Mathf.Min(num2, rect.height / 2f);
				Texture2D texture = this._sprite.texture;
				float num7 = textureRect.xMax / (float)texture.width;
				float num8 = textureRect.yMax / (float)texture.height;
				float num9 = textureRect.width / (float)texture.width;
				float num10 = textureRect.height / (float)texture.height;
				if (this.flipX)
				{
					num7 -= num9;
					num9 = -num9;
				}
				if (this.flipY)
				{
					num8 -= num10;
					num10 = -num10;
				}
				float num11 = (num3 + num5) / (num3 + num);
				float num12 = (num4 + num6) / (num4 + num2);
				num9 *= num11;
				num10 *= num12;
				rect2 = new Rect(num7 - num9, num8 - num10, num9, num10);
			}
			else
			{
				rect2 = new Rect(0f, 0f, 1f, 1f);
				num5 = Mathf.Min(this.innerSize, rect.width / 2f);
				num6 = Mathf.Min(this.innerSize, rect.height / 2f);
			}
			Vector3 forward = Vector3.forward;
			Vector2 uv = new Vector2(0.5f, 1f);
			CornerImage.verts[0] = new UIVertex
			{
				position = new Vector2(rect.xMin - num3, rect.yMin - num4),
				color = this.cornerColor * this.color,
				uv0 = new Vector2(rect2.xMax, rect2.yMax),
				normal = forward,
				uv2 = uv
			};
			CornerImage.verts[1] = new UIVertex
			{
				position = new Vector2(rect.xMin + num5, rect.yMin - num4),
				color = this.sideColor * this.color,
				uv0 = new Vector2(rect2.xMin, rect2.yMax),
				normal = forward,
				uv2 = uv
			};
			CornerImage.verts[2] = new UIVertex
			{
				position = new Vector2(rect.xMax - num5, rect.yMin - num4),
				color = this.sideColor * this.color,
				uv0 = new Vector2(rect2.xMin, rect2.yMax),
				normal = forward,
				uv2 = uv
			};
			CornerImage.verts[3] = new UIVertex
			{
				position = new Vector2(rect.xMax + num3, rect.yMin - num4),
				color = this.cornerColor * this.color,
				uv0 = new Vector2(rect2.xMax, rect2.yMax),
				normal = forward,
				uv2 = uv
			};
			CornerImage.verts[4] = new UIVertex
			{
				position = new Vector2(rect.xMin - num3, rect.yMin + num6),
				color = this.sideColor * this.color,
				uv0 = new Vector2(rect2.xMax, rect2.yMin),
				normal = forward,
				uv2 = uv
			};
			CornerImage.verts[5] = new UIVertex
			{
				position = new Vector2(rect.xMin + num5, rect.yMin + num6),
				color = this.centerColor * this.color,
				uv0 = new Vector2(rect2.xMin, rect2.yMin),
				normal = forward,
				uv2 = uv
			};
			CornerImage.verts[6] = new UIVertex
			{
				position = new Vector2(rect.xMax - num5, rect.yMin + num6),
				color = this.centerColor * this.color,
				uv0 = new Vector2(rect2.xMin, rect2.yMin),
				normal = forward,
				uv2 = uv
			};
			CornerImage.verts[7] = new UIVertex
			{
				position = new Vector2(rect.xMax + num3, rect.yMin + num6),
				color = this.sideColor * this.color,
				uv0 = new Vector2(rect2.xMax, rect2.yMin),
				normal = forward,
				uv2 = uv
			};
			CornerImage.verts[8] = new UIVertex
			{
				position = new Vector2(rect.xMin - num3, rect.yMax - num6),
				color = this.sideColor * this.color,
				uv0 = new Vector2(rect2.xMax, rect2.yMin),
				normal = forward,
				uv2 = uv
			};
			CornerImage.verts[9] = new UIVertex
			{
				position = new Vector2(rect.xMin + num5, rect.yMax - num6),
				color = this.centerColor * this.color,
				uv0 = new Vector2(rect2.xMin, rect2.yMin),
				normal = forward,
				uv2 = uv
			};
			CornerImage.verts[10] = new UIVertex
			{
				position = new Vector2(rect.xMax - num5, rect.yMax - num6),
				color = this.centerColor * this.color,
				uv0 = new Vector2(rect2.xMin, rect2.yMin),
				normal = forward,
				uv2 = uv
			};
			CornerImage.verts[11] = new UIVertex
			{
				position = new Vector2(rect.xMax + num3, rect.yMax - num6),
				color = this.sideColor * this.color,
				uv0 = new Vector2(rect2.xMax, rect2.yMin),
				normal = forward,
				uv2 = uv
			};
			CornerImage.verts[12] = new UIVertex
			{
				position = new Vector2(rect.xMin - num3, rect.yMax + num3),
				color = this.cornerColor * this.color,
				uv0 = new Vector2(rect2.xMax, rect2.yMax),
				normal = forward,
				uv2 = uv
			};
			CornerImage.verts[13] = new UIVertex
			{
				position = new Vector2(rect.xMin + num5, rect.yMax + num3),
				color = this.sideColor * this.color,
				uv0 = new Vector2(rect2.xMin, rect2.yMax),
				normal = forward,
				uv2 = uv
			};
			CornerImage.verts[14] = new UIVertex
			{
				position = new Vector2(rect.xMax - num5, rect.yMax + num3),
				color = this.sideColor * this.color,
				uv0 = new Vector2(rect2.xMin, rect2.yMax),
				normal = forward,
				uv2 = uv
			};
			CornerImage.verts[15] = new UIVertex
			{
				position = new Vector2(rect.xMax + num3, rect.yMax + num3),
				color = this.cornerColor * this.color,
				uv0 = new Vector2(rect2.xMax, rect2.yMax),
				normal = forward,
				uv2 = uv
			};
			vh.Clear();
			CornerImage.tempTriList.Clear();
			CornerImage.tempTriList.AddRange(CornerImage.cornerTris);
			if (this.leftAndRight)
			{
				CornerImage.tempTriList.AddRange(CornerImage.leftAndRightTris);
			}
			if (this.topAndBottom)
			{
				CornerImage.tempTriList.AddRange(CornerImage.topAndBottomTris);
			}
			if (this.center)
			{
				CornerImage.tempTriList.AddRange(CornerImage.centerTris);
			}
			vh.AddUIVertexStream(CornerImage.verts, CornerImage.tempTriList);
		}

		// Token: 0x06003A83 RID: 14979 RVA: 0x00103FE5 File Offset: 0x001023E5
		protected override void UpdateMaterial()
		{
			base.UpdateMaterial();
			base.canvasRenderer.SetTexture((!this._sprite) ? null : this._sprite.texture);
		}

		// Token: 0x04002884 RID: 10372
		[SerializeField]
		private Sprite _sprite;

		// Token: 0x04002885 RID: 10373
		[SerializeField]
		private float innerSize = 10f;

		// Token: 0x04002886 RID: 10374
		[SerializeField]
		private float outerSize = 10f;

		// Token: 0x04002887 RID: 10375
		[SerializeField]
		private CornerImage.Alignment alignment;

		// Token: 0x04002888 RID: 10376
		[SerializeField]
		[Header("UVs")]
		private bool flipX;

		// Token: 0x04002889 RID: 10377
		[SerializeField]
		private bool flipY;

		// Token: 0x0400288A RID: 10378
		[SerializeField]
		private bool borders;

		// Token: 0x0400288B RID: 10379
		[SerializeField]
		[Header("Fill")]
		private bool leftAndRight = true;

		// Token: 0x0400288C RID: 10380
		[SerializeField]
		private bool topAndBottom = true;

		// Token: 0x0400288D RID: 10381
		[SerializeField]
		private bool center = true;

		// Token: 0x0400288E RID: 10382
		[SerializeField]
		[Header("Vertex Colors")]
		private Color centerColor = Color.white;

		// Token: 0x0400288F RID: 10383
		[SerializeField]
		private Color sideColor = Color.white;

		// Token: 0x04002890 RID: 10384
		[SerializeField]
		private Color cornerColor = Color.white;

		// Token: 0x04002891 RID: 10385
		private static readonly List<UIVertex> verts = new List<UIVertex>(16)
		{
			default(UIVertex),
			default(UIVertex),
			default(UIVertex),
			default(UIVertex),
			default(UIVertex),
			default(UIVertex),
			default(UIVertex),
			default(UIVertex),
			default(UIVertex),
			default(UIVertex),
			default(UIVertex),
			default(UIVertex),
			default(UIVertex),
			default(UIVertex),
			default(UIVertex),
			default(UIVertex)
		};

		// Token: 0x04002892 RID: 10386
		private static readonly List<int> tempTriList = new List<int>(54);

		// Token: 0x04002893 RID: 10387
		private static readonly List<int> allTris = new List<int>
		{
			0,
			4,
			1,
			1,
			4,
			5,
			1,
			5,
			2,
			2,
			5,
			6,
			2,
			6,
			3,
			3,
			6,
			7,
			4,
			8,
			5,
			5,
			8,
			9,
			5,
			9,
			6,
			6,
			9,
			10,
			6,
			10,
			7,
			7,
			10,
			11,
			8,
			12,
			9,
			9,
			12,
			13,
			9,
			13,
			10,
			10,
			13,
			14,
			10,
			14,
			11,
			11,
			14,
			15
		};

		// Token: 0x04002894 RID: 10388
		private static readonly List<int> cornerTris = new List<int>
		{
			0,
			5,
			1,
			0,
			4,
			5,
			2,
			6,
			3,
			3,
			6,
			7,
			8,
			12,
			9,
			9,
			12,
			13,
			15,
			11,
			10,
			14,
			15,
			10
		};

		// Token: 0x04002895 RID: 10389
		private static readonly List<int> leftAndRightTris = new List<int>
		{
			4,
			8,
			5,
			5,
			8,
			9,
			6,
			10,
			7,
			7,
			10,
			11
		};

		// Token: 0x04002896 RID: 10390
		private static readonly List<int> topAndBottomTris = new List<int>
		{
			1,
			5,
			2,
			2,
			5,
			6,
			9,
			13,
			10,
			10,
			13,
			14
		};

		// Token: 0x04002897 RID: 10391
		private static readonly List<int> centerTris = new List<int>
		{
			5,
			9,
			6,
			6,
			9,
			10
		};

		// Token: 0x04002898 RID: 10392
		private static List<IMeshModifier> meshModifiers = new List<IMeshModifier>(4);

		// Token: 0x020008B5 RID: 2229
		private enum Alignment
		{
			// Token: 0x0400289A RID: 10394
			Horizontal,
			// Token: 0x0400289B RID: 10395
			Vertical
		}
	}
}
