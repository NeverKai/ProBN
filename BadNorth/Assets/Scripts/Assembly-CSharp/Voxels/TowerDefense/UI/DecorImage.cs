using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x020008B6 RID: 2230
	public class DecorImage : MaskableGraphic
	{
		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x06003A86 RID: 14982 RVA: 0x00104534 File Offset: 0x00102934
		// (set) Token: 0x06003A87 RID: 14983 RVA: 0x0010453C File Offset: 0x0010293C
		public Texture tex
		{
			get
			{
				return this._tex;
			}
			set
			{
				this._tex = value;
				this.SetAllDirty();
			}
		}

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x06003A88 RID: 14984 RVA: 0x0010454B File Offset: 0x0010294B
		// (set) Token: 0x06003A89 RID: 14985 RVA: 0x00104553 File Offset: 0x00102953
		public float tiling
		{
			get
			{
				return this._tiling;
			}
			set
			{
				this._tiling = value;
				this.SetAllDirty();
			}
		}

		// Token: 0x06003A8A RID: 14986 RVA: 0x00104564 File Offset: 0x00102964
		protected override void OnPopulateMesh(VertexHelper vh)
		{
			base.OnPopulateMesh(vh);
			vh.Clear();
			bool flag = this.orientation % 2 == 0;
			Rect rect = base.rectTransform.rect;
			Vector2 a = (!this._tex) ? (Vector2.one / 256f) : this.tex.texelSize;
			a = Vector2.Scale(a, rect.size) * 0.5f * this.tiling;
			Vector2 uv;
			uv.x = (float)(this.orientation / 2 % 2);
			uv.y = (float)(this.orientation / 4 % 2);
			Vector2 uv2;
			uv2.x = uv.x + (1f - uv.x * 2f) * a.x;
			uv2.y = uv.y + (1f - uv.y * 2f) * a.y;
			Vector2 uv3 = new Vector2(uv2.x, uv.y);
			Vector2 uv4 = new Vector2(uv.x, uv2.y);
			if (flag)
			{
				uv2 = new Vector2(uv2.y, uv2.x);
				uv = new Vector2(uv.y, uv.x);
				uv3 = new Vector2(uv3.y, uv3.x);
				uv4 = new Vector2(uv4.y, uv4.x);
			}
			Vector2 min = rect.min;
			Vector2 vector = (rect.min + rect.max) / 2f;
			Vector2 max = rect.max;
			DecorImage.verts[0] = new UIVertex
			{
				position = new Vector2(min.x, min.y),
				uv0 = uv,
				color = Color.white
			};
			DecorImage.verts[1] = new UIVertex
			{
				position = new Vector2(vector.x, min.y),
				uv0 = uv3,
				color = Color.white
			};
			DecorImage.verts[2] = new UIVertex
			{
				position = new Vector2(max.x, min.y),
				uv0 = uv,
				color = Color.white
			};
			DecorImage.verts[3] = new UIVertex
			{
				position = new Vector2(min.x, vector.y),
				uv0 = uv4,
				color = Color.white
			};
			DecorImage.verts[4] = new UIVertex
			{
				position = new Vector2(vector.x, vector.y),
				uv0 = uv2,
				color = Color.white
			};
			DecorImage.verts[5] = new UIVertex
			{
				position = new Vector2(max.x, vector.y),
				uv0 = uv4,
				color = Color.white
			};
			DecorImage.verts[6] = new UIVertex
			{
				position = new Vector2(min.x, max.y),
				uv0 = uv,
				color = Color.white
			};
			DecorImage.verts[7] = new UIVertex
			{
				position = new Vector2(vector.x, max.y),
				uv0 = uv3,
				color = Color.white
			};
			DecorImage.verts[8] = new UIVertex
			{
				position = new Vector2(max.x, max.y),
				uv0 = uv,
				color = Color.white
			};
			vh.AddUIVertexStream(DecorImage.verts, DecorImage.allTris);
		}

		// Token: 0x06003A8B RID: 14987 RVA: 0x001049E0 File Offset: 0x00102DE0
		protected override void UpdateMaterial()
		{
			base.UpdateMaterial();
			base.canvasRenderer.SetTexture(this._tex);
			base.canvasRenderer.SetColor(this.color);
		}

		// Token: 0x06003A8C RID: 14988 RVA: 0x00104A0A File Offset: 0x00102E0A
		[ContextMenu("Iterate Orientation")]
		private void IterateOrientation()
		{
			this.orientation = (this.orientation + 1) % 8;
			this.SetAllDirty();
		}

		// Token: 0x06003A8D RID: 14989 RVA: 0x00104A22 File Offset: 0x00102E22
		[ContextMenu("Double Tiling")]
		private void DoubleTiling()
		{
			this.tiling *= 2f;
			this.SetAllDirty();
		}

		// Token: 0x06003A8E RID: 14990 RVA: 0x00104A3C File Offset: 0x00102E3C
		[ContextMenu("Halve Tiling")]
		private void HalveTiling()
		{
			this.tiling /= 2f;
			this.SetAllDirty();
		}

		// Token: 0x0400289C RID: 10396
		[SerializeField]
		private Texture _tex;

		// Token: 0x0400289D RID: 10397
		private const int maxOrientation = 8;

		// Token: 0x0400289E RID: 10398
		[SerializeField]
		[Range(0f, 7f)]
		private int orientation;

		// Token: 0x0400289F RID: 10399
		[SerializeField]
		private float _tiling = 2f;

		// Token: 0x040028A0 RID: 10400
		private static readonly List<UIVertex> verts = new List<UIVertex>(9)
		{
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

		// Token: 0x040028A1 RID: 10401
		private static readonly List<int> allTris = new List<int>
		{
			4,
			1,
			0,
			4,
			2,
			1,
			4,
			5,
			2,
			4,
			8,
			5,
			4,
			7,
			8,
			4,
			6,
			7,
			4,
			3,
			6,
			4,
			0,
			3
		};
	}
}
