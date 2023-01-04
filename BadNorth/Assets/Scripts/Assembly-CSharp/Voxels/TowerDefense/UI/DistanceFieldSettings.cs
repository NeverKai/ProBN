using System;
using UnityEngine;
using UnityEngine.UI;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x0200090A RID: 2314
	public class DistanceFieldSettings : BaseMeshEffect
	{
		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x06003DF4 RID: 15860 RVA: 0x00116374 File Offset: 0x00114774
		// (set) Token: 0x06003DF5 RID: 15861 RVA: 0x0011637C File Offset: 0x0011477C
		public float fill
		{
			get
			{
				return this._fill;
			}
			set
			{
				this._fill = value;
				base.graphic.SetVerticesDirty();
			}
		}

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x06003DF6 RID: 15862 RVA: 0x00116390 File Offset: 0x00114790
		// (set) Token: 0x06003DF7 RID: 15863 RVA: 0x00116398 File Offset: 0x00114798
		public float fraction
		{
			get
			{
				return this._fraction;
			}
			set
			{
				this._fraction = value;
				base.graphic.SetVerticesDirty();
			}
		}

		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x06003DF8 RID: 15864 RVA: 0x001163AC File Offset: 0x001147AC
		// (set) Token: 0x06003DF9 RID: 15865 RVA: 0x001163B4 File Offset: 0x001147B4
		public float pixelWidth
		{
			get
			{
				return this._pixelWidth;
			}
			set
			{
				this._pixelWidth = value;
				base.graphic.SetVerticesDirty();
			}
		}

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x06003DFA RID: 15866 RVA: 0x001163C8 File Offset: 0x001147C8
		// (set) Token: 0x06003DFB RID: 15867 RVA: 0x001163D0 File Offset: 0x001147D0
		public float shadow
		{
			get
			{
				return this._shadow;
			}
			set
			{
				this._shadow = value;
				base.graphic.SetVerticesDirty();
			}
		}

		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x06003DFC RID: 15868 RVA: 0x001163E4 File Offset: 0x001147E4
		// (set) Token: 0x06003DFD RID: 15869 RVA: 0x001163EC File Offset: 0x001147EC
		public int bands
		{
			get
			{
				return this._bands;
			}
			set
			{
				this._bands = value;
				base.graphic.SetVerticesDirty();
			}
		}

		// Token: 0x06003DFE RID: 15870 RVA: 0x00116400 File Offset: 0x00114800
		public override void ModifyMesh(VertexHelper vh)
		{
			UIVertex vertex = default(UIVertex);
			DFSettings dfsettings = new DFSettings(this.fill, this.fraction, this.pixelWidth, this.shadow, this.bands);
			int i = 0;
			int currentVertCount = vh.currentVertCount;
			while (i < currentVertCount)
			{
				vh.PopulateUIVertex(ref vertex, i);
				dfsettings.SetVert(ref vertex);
				vh.SetUIVertex(vertex, i);
				i++;
			}
		}

		// Token: 0x04002B46 RID: 11078
		[SerializeField]
		[Range(0f, 1f)]
		private float _fill = 1f;

		// Token: 0x04002B47 RID: 11079
		[SerializeField]
		[Range(0f, 1f)]
		private float _fraction = 0.5f;

		// Token: 0x04002B48 RID: 11080
		[SerializeField]
		private float _pixelWidth = 1f;

		// Token: 0x04002B49 RID: 11081
		[SerializeField]
		private float _shadow;

		// Token: 0x04002B4A RID: 11082
		[SerializeField]
		private int _bands;
	}
}
