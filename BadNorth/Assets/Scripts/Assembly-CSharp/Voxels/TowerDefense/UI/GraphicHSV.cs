using System;
using UnityEngine;
using UnityEngine.UI;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x0200081E RID: 2078
	[RequireComponent(typeof(Graphic))]
	public class GraphicHSV : BaseMeshEffect
	{
		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x06003644 RID: 13892 RVA: 0x000E9F73 File Offset: 0x000E8373
		// (set) Token: 0x06003645 RID: 13893 RVA: 0x000E9F7B File Offset: 0x000E837B
		public float hue
		{
			get
			{
				return this._hue;
			}
			set
			{
				this._hue = value;
				this.SetDirty();
			}
		}

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x06003646 RID: 13894 RVA: 0x000E9F8A File Offset: 0x000E838A
		// (set) Token: 0x06003647 RID: 13895 RVA: 0x000E9F92 File Offset: 0x000E8392
		public float saturation
		{
			get
			{
				return this._saturation;
			}
			set
			{
				this._saturation = value;
				this.SetDirty();
			}
		}

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x06003648 RID: 13896 RVA: 0x000E9FA1 File Offset: 0x000E83A1
		// (set) Token: 0x06003649 RID: 13897 RVA: 0x000E9FA9 File Offset: 0x000E83A9
		public float value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
				this.SetDirty();
			}
		}

		// Token: 0x0600364A RID: 13898 RVA: 0x000E9FB8 File Offset: 0x000E83B8
		public override void ModifyMesh(VertexHelper vh)
		{
			UIVertex vertex = default(UIVertex);
			int i = 0;
			int currentVertCount = vh.currentVertCount;
			while (i < currentVertCount)
			{
				vh.PopulateUIVertex(ref vertex, i);
				float num;
				float num2;
				float num3;
				Color.RGBToHSV(vertex.color, out num, out num2, out num3);
				num = (1f + num + this._hue) % 1f;
				num2 *= this._saturation;
				num3 *= this._value;
				vertex.color = Color.HSVToRGB(num, num2, num3);
				vh.SetUIVertex(vertex, i);
				i++;
			}
		}

		// Token: 0x0600364B RID: 13899 RVA: 0x000EA051 File Offset: 0x000E8451
		private void SetDirty()
		{
			base.GetComponent<Graphic>().SetVerticesDirty();
		}

		// Token: 0x040024D8 RID: 9432
		[SerializeField]
		[Range(-1f, 1f)]
		private float _hue;

		// Token: 0x040024D9 RID: 9433
		[SerializeField]
		[Range(0f, 2f)]
		private float _saturation = 1f;

		// Token: 0x040024DA RID: 9434
		[SerializeField]
		[Range(0f, 2f)]
		private float _value = 1f;
	}
}
