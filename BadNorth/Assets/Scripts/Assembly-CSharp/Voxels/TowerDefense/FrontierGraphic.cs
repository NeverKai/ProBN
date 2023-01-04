using System;
using UnityEngine;
using UnityEngine.UI;

namespace Voxels.TowerDefense
{
	// Token: 0x020006E8 RID: 1768
	public class FrontierGraphic : Graphic
	{
		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06002D9C RID: 11676 RVA: 0x000AE0DD File Offset: 0x000AC4DD
		// (set) Token: 0x06002D9D RID: 11677 RVA: 0x000AE0E5 File Offset: 0x000AC4E5
		public Rect uvRect
		{
			get
			{
				return this._uvRect;
			}
			set
			{
				this._uvRect = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06002D9E RID: 11678 RVA: 0x000AE0F4 File Offset: 0x000AC4F4
		// (set) Token: 0x06002D9F RID: 11679 RVA: 0x000AE0FC File Offset: 0x000AC4FC
		public Rect drawRect
		{
			get
			{
				return this._drawRect;
			}
			set
			{
				this._drawRect = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x06002DA0 RID: 11680 RVA: 0x000AE10B File Offset: 0x000AC50B
		// (set) Token: 0x06002DA1 RID: 11681 RVA: 0x000AE113 File Offset: 0x000AC513
		public Vector4 frontierDepthVector
		{
			get
			{
				return this._frontierDepthVector;
			}
			set
			{
				this._frontierDepthVector = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06002DA2 RID: 11682 RVA: 0x000AE122 File Offset: 0x000AC522
		// (set) Token: 0x06002DA3 RID: 11683 RVA: 0x000AE12A File Offset: 0x000AC52A
		public Texture mainTex
		{
			get
			{
				return this._mainTex;
			}
			set
			{
				this._mainTex = value;
				this.SetMaterialDirty();
			}
		}

		// Token: 0x06002DA4 RID: 11684 RVA: 0x000AE139 File Offset: 0x000AC539
		protected override void UpdateMaterial()
		{
			base.UpdateMaterial();
			if (this.mainTex)
			{
				base.canvasRenderer.SetTexture(this.mainTex);
			}
		}

		// Token: 0x06002DA5 RID: 11685 RVA: 0x000AE164 File Offset: 0x000AC564
		protected override void OnPopulateMesh(VertexHelper vh)
		{
			base.OnPopulateMesh(vh);
			vh.Clear();
			UIVertex v = default(UIVertex);
			v.color = Color.white;
			v.tangent = this.frontierDepthVector;
			Vector2 vector = Vector2.one / 2f;
			Vector2 a = vector;
			for (int i = 0; i < 4; i++)
			{
				v.position = ExtraMath.Lerp(this.drawRect.min, this.drawRect.max, a + vector);
				v.uv0 = ExtraMath.Lerp(this.uvRect.min, this.uvRect.max, a + vector);
				vh.AddVert(v);
				a = new Vector2(a.y, -a.x);
			}
			vh.AddTriangle(0, 1, 2);
			vh.AddTriangle(2, 3, 0);
		}

		// Token: 0x04001E3D RID: 7741
		[SerializeField]
		private Rect _uvRect = new Rect(0f, 0f, 1f, 1f);

		// Token: 0x04001E3E RID: 7742
		[SerializeField]
		private Rect _drawRect = new Rect(-100f, -100f, 100f, 100f);

		// Token: 0x04001E3F RID: 7743
		private Vector4 _frontierDepthVector;

		// Token: 0x04001E40 RID: 7744
		private Texture _mainTex;

		// Token: 0x04001E41 RID: 7745
		private ShaderId frontierDepthId = "_FrontierDepth";
	}
}
