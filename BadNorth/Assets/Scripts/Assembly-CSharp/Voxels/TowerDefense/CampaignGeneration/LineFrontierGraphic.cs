using System;
using UnityEngine;
using UnityEngine.UI;
using Voxels.TowerDefense.SpriteMagic;

namespace Voxels.TowerDefense.CampaignGeneration
{
	// Token: 0x02000716 RID: 1814
	public class LineFrontierGraphic : MaskableGraphic
	{
		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x06002F0E RID: 12046 RVA: 0x000BB17F File Offset: 0x000B957F
		// (set) Token: 0x06002F0F RID: 12047 RVA: 0x000BB187 File Offset: 0x000B9587
		public float frontierDepth
		{
			get
			{
				return this._frontierDepth;
			}
			set
			{
				this._frontierDepth = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06002F10 RID: 12048 RVA: 0x000BB196 File Offset: 0x000B9596
		// (set) Token: 0x06002F11 RID: 12049 RVA: 0x000BB19E File Offset: 0x000B959E
		public float width
		{
			get
			{
				return this._width;
			}
			set
			{
				this._width = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06002F12 RID: 12050 RVA: 0x000BB1AD File Offset: 0x000B95AD
		// (set) Token: 0x06002F13 RID: 12051 RVA: 0x000BB1B5 File Offset: 0x000B95B5
		public float pixelWidth
		{
			get
			{
				return this._pixelWidth;
			}
			set
			{
				this._pixelWidth = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x06002F14 RID: 12052 RVA: 0x000BB1C4 File Offset: 0x000B95C4
		public LineFrontierGraphic Set(LineFrontier lineFrontier, float frontierDepth)
		{
			this.lineFrontier = lineFrontier;
			this.frontierDepth = frontierDepth;
			this.SetVerticesDirty();
			return this;
		}

		// Token: 0x06002F15 RID: 12053 RVA: 0x000BB1DC File Offset: 0x000B95DC
		protected override void UpdateMaterial()
		{
			base.UpdateMaterial();
			if (this.sprite && this.sprite.texture)
			{
				base.canvasRenderer.SetTexture(this.sprite.texture);
			}
			base.canvasRenderer.SetColor(this.color);
		}

		// Token: 0x06002F16 RID: 12054 RVA: 0x000BB23C File Offset: 0x000B963C
		protected override void OnPopulateMesh(VertexHelper vh)
		{
			vh.Clear();
			if (this.lineFrontier)
			{
				Rect rect = (!this.sprite) ? new Rect(0f, 0f, 1f, 1f) : SpriteBounds.GetUVRect(this.sprite);
				LineFrontier.Point[,] points = this.lineFrontier.points;
				int sizeY = this.lineFrontier.sizeY;
				float d = this.width / 2f;
				UIVertex uivertex = new UIVertex
				{
					color = Color.white,
					normal = Vector3.forward,
					uv1 = new Vector2(0f, this._pixelWidth - 1f),
					uv2 = new Vector2(-1f, -1f)
				};
				float num = this.frontierDepth + 4f;
				int num2 = Mathf.FloorToInt(num);
				int num3 = Mathf.CeilToInt(num);
				float num4 = num - (float)num2;
				float b = 1f - num4;
				int currentVertCount = vh.currentVertCount;
				for (int i = 0; i < sizeY; i++)
				{
					LineFrontier.Point point = points[num2, i] * b + points[num3, i] * num4;
					if (this.spriteRange > 0)
					{
						float num5 = (float)i / (float)this.spriteRange;
						num5 %= 2f;
						num5 = Mathf.Abs(num5 - 1f);
						uivertex.uv0.x = Mathf.Lerp(rect.xMin, rect.xMax, num5);
					}
					else
					{
						uivertex.uv0.x = (rect.xMin + rect.xMax) / 2f;
					}
					uivertex.position = point.pos - point.tangent * d;
					uivertex.uv0.y = rect.yMin;
					vh.AddVert(uivertex);
					vh.SetUIVertex(uivertex, i * 2);
					uivertex.position = point.pos + point.tangent * d;
					uivertex.uv0.y = rect.yMax;
					vh.AddVert(uivertex);
					vh.SetUIVertex(uivertex, i * 2 + 1);
				}
				for (int j = 0; j < sizeY - 1; j++)
				{
					int num6 = currentVertCount + j * 2;
					vh.AddTriangle(num6, num6 + 3, num6 + 1);
					vh.AddTriangle(num6 + 3, num6, num6 + 2);
				}
			}
		}

		// Token: 0x04001F42 RID: 8002
		[SerializeField]
		private float _frontierDepth;

		// Token: 0x04001F43 RID: 8003
		[SerializeField]
		private float _width = 1f;

		// Token: 0x04001F44 RID: 8004
		[SerializeField]
		private float _pixelWidth = 1f;

		// Token: 0x04001F45 RID: 8005
		[SerializeField]
		private int spriteRange = 1;

		// Token: 0x04001F46 RID: 8006
		[SerializeField]
		private Vector2 alphaRange = new Vector2(20f, 10f);

		// Token: 0x04001F47 RID: 8007
		[SerializeField]
		private Sprite sprite;

		// Token: 0x04001F48 RID: 8008
		private LineFrontier lineFrontier;
	}
}
