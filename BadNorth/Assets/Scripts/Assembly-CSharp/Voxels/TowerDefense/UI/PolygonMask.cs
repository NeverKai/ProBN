using System;
using UnityEngine;
using UnityEngine.UI;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x02000914 RID: 2324
	public class PolygonMask : MaskableGraphic
	{
		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x06003E52 RID: 15954 RVA: 0x00118988 File Offset: 0x00116D88
		// (set) Token: 0x06003E53 RID: 15955 RVA: 0x00118990 File Offset: 0x00116D90
		public UiPolygon polygon
		{
			get
			{
				return this._polygon;
			}
			set
			{
				this._polygon = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x06003E54 RID: 15956 RVA: 0x0011899F File Offset: 0x00116D9F
		// (set) Token: 0x06003E55 RID: 15957 RVA: 0x001189A7 File Offset: 0x00116DA7
		public Sprite sprite
		{
			get
			{
				return this._sprite;
			}
			set
			{
				this._sprite = value;
				this.DirtyVerts();
				this.SetMaterialDirty();
			}
		}

		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x06003E56 RID: 15958 RVA: 0x001189BC File Offset: 0x00116DBC
		// (set) Token: 0x06003E57 RID: 15959 RVA: 0x001189C4 File Offset: 0x00116DC4
		public PolygonStyle style
		{
			get
			{
				return this._style;
			}
			set
			{
				this._style = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x06003E58 RID: 15960 RVA: 0x001189D4 File Offset: 0x00116DD4
		protected override void OnPopulateMesh(VertexHelper vh)
		{
			if (this.polygon && this.style)
			{
				using ("PolygonMask.OnPopulateMesh")
				{
					using (VertexHelper vertexHelper = new VertexHelper())
					{
						vh.Clear();
						Rect rect = base.rectTransform.rect;
						this.polygon.Populate(vertexHelper, rect, this.radial);
						this.style.Populate(vh, vertexHelper, this.sprite, rect, this.color, base.transform.localToWorldMatrix.MultiplyVector(Vector2.one).normalized * Vector2.one.magnitude);
					}
				}
			}
		}

		// Token: 0x06003E59 RID: 15961 RVA: 0x00118AD0 File Offset: 0x00116ED0
		public void DirtyVerts()
		{
			this.SetVerticesDirty();
		}

		// Token: 0x06003E5A RID: 15962 RVA: 0x00118AD8 File Offset: 0x00116ED8
		protected override void UpdateMaterial()
		{
			base.UpdateMaterial();
			if (this.sprite)
			{
				base.canvasRenderer.SetTexture(this.sprite.texture);
			}
		}

		// Token: 0x04002B8F RID: 11151
		[SerializeField]
		private UiPolygon _polygon;

		// Token: 0x04002B90 RID: 11152
		[SerializeField]
		private Sprite _sprite;

		// Token: 0x04002B91 RID: 11153
		[SerializeField]
		private PolygonStyle _style;

		// Token: 0x04002B92 RID: 11154
		private bool radial;
	}
}
