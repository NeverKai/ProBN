using System;
using UnityEngine;
using UnityEngine.UI;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x02000905 RID: 2309
	public class ColorModifier : BaseMeshEffect
	{
		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x06003DB9 RID: 15801 RVA: 0x001152F6 File Offset: 0x001136F6
		// (set) Token: 0x06003DBA RID: 15802 RVA: 0x001152FE File Offset: 0x001136FE
		public Color color
		{
			get
			{
				return this._color;
			}
			set
			{
				this._color = value;
				base.graphic.SetVerticesDirty();
			}
		}

		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x06003DBB RID: 15803 RVA: 0x00115312 File Offset: 0x00113712
		// (set) Token: 0x06003DBC RID: 15804 RVA: 0x0011531A File Offset: 0x0011371A
		public float alpha
		{
			get
			{
				return this._alpha;
			}
			set
			{
				this._alpha = value;
				base.graphic.SetVerticesDirty();
			}
		}

		// Token: 0x06003DBD RID: 15805 RVA: 0x00115330 File Offset: 0x00113730
		public override void ModifyMesh(VertexHelper vh)
		{
			UIVertex vertex = default(UIVertex);
			switch (this.mode)
			{
			case ColorModifier.Mode.Blend:
			{
				int i = 0;
				int currentVertCount = vh.currentVertCount;
				while (i < currentVertCount)
				{
					vh.PopulateUIVertex(ref vertex, i);
					vertex.color = Color.Lerp(vertex.color, this.color, this._alpha);
					vh.SetUIVertex(vertex, i);
					i++;
				}
				break;
			}
			case ColorModifier.Mode.Multiply:
			{
				int j = 0;
				int currentVertCount2 = vh.currentVertCount;
				while (j < currentVertCount2)
				{
					vh.PopulateUIVertex(ref vertex, j);
					vertex.color = Color.Lerp(vertex.color, vertex.color * this.color, this._alpha);
					vh.SetUIVertex(vertex, j);
					j++;
				}
				break;
			}
			case ColorModifier.Mode.Add:
			{
				int k = 0;
				int currentVertCount3 = vh.currentVertCount;
				while (k < currentVertCount3)
				{
					vh.PopulateUIVertex(ref vertex, k);
					vertex.color = Color.Lerp(vertex.color, vertex.color + this.color, this._alpha);
					vh.SetUIVertex(vertex, k);
					k++;
				}
				break;
			}
			}
		}

		// Token: 0x04002B1B RID: 11035
		[SerializeField]
		private ColorModifier.Mode mode;

		// Token: 0x04002B1C RID: 11036
		[SerializeField]
		private Color _color = Color.white;

		// Token: 0x04002B1D RID: 11037
		[SerializeField]
		[Range(0f, 1f)]
		private float _alpha = 1f;

		// Token: 0x02000906 RID: 2310
		private enum Mode
		{
			// Token: 0x04002B1F RID: 11039
			Blend,
			// Token: 0x04002B20 RID: 11040
			Multiply,
			// Token: 0x04002B21 RID: 11041
			Add
		}
	}
}
