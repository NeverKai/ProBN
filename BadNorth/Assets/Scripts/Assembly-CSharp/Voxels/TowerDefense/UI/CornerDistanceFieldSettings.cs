using System;
using UnityEngine;
using UnityEngine.UI;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x02000907 RID: 2311
	public class CornerDistanceFieldSettings : BaseMeshEffect
	{
		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x06003DBF RID: 15807 RVA: 0x001154AA File Offset: 0x001138AA
		// (set) Token: 0x06003DC0 RID: 15808 RVA: 0x001154B2 File Offset: 0x001138B2
		public float all
		{
			get
			{
				return this._all;
			}
			set
			{
				this._all = value;
				base.graphic.SetVerticesDirty();
			}
		}

		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x06003DC1 RID: 15809 RVA: 0x001154C6 File Offset: 0x001138C6
		// (set) Token: 0x06003DC2 RID: 15810 RVA: 0x001154CE File Offset: 0x001138CE
		public float center
		{
			get
			{
				return this._center;
			}
			set
			{
				this._center = value;
				base.graphic.SetVerticesDirty();
			}
		}

		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x06003DC3 RID: 15811 RVA: 0x001154E2 File Offset: 0x001138E2
		// (set) Token: 0x06003DC4 RID: 15812 RVA: 0x001154EA File Offset: 0x001138EA
		public float border
		{
			get
			{
				return this._border;
			}
			set
			{
				this._border = value;
				base.graphic.SetVerticesDirty();
			}
		}

		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x06003DC5 RID: 15813 RVA: 0x001154FE File Offset: 0x001138FE
		// (set) Token: 0x06003DC6 RID: 15814 RVA: 0x00115506 File Offset: 0x00113906
		public float centerX
		{
			get
			{
				return this._centerX;
			}
			set
			{
				this._centerX = value;
				base.graphic.SetVerticesDirty();
			}
		}

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x06003DC7 RID: 15815 RVA: 0x0011551A File Offset: 0x0011391A
		// (set) Token: 0x06003DC8 RID: 15816 RVA: 0x00115522 File Offset: 0x00113922
		public float centerY
		{
			get
			{
				return this._centerY;
			}
			set
			{
				this._centerY = value;
				base.graphic.SetVerticesDirty();
			}
		}

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x06003DC9 RID: 15817 RVA: 0x00115536 File Offset: 0x00113936
		// (set) Token: 0x06003DCA RID: 15818 RVA: 0x0011553E File Offset: 0x0011393E
		public float sidesX
		{
			get
			{
				return this._sidesX;
			}
			set
			{
				this._sidesX = value;
				base.graphic.SetVerticesDirty();
			}
		}

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x06003DCB RID: 15819 RVA: 0x00115552 File Offset: 0x00113952
		// (set) Token: 0x06003DCC RID: 15820 RVA: 0x0011555A File Offset: 0x0011395A
		public float sidesY
		{
			get
			{
				return this._sidesY;
			}
			set
			{
				this._sidesY = value;
				base.graphic.SetVerticesDirty();
			}
		}

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x06003DCD RID: 15821 RVA: 0x0011556E File Offset: 0x0011396E
		// (set) Token: 0x06003DCE RID: 15822 RVA: 0x00115576 File Offset: 0x00113976
		public float corners
		{
			get
			{
				return this._corners;
			}
			set
			{
				this._corners = value;
				base.graphic.SetVerticesDirty();
			}
		}

		// Token: 0x06003DCF RID: 15823 RVA: 0x0011558C File Offset: 0x0011398C
		public override void ModifyMesh(VertexHelper vh)
		{
			if (vh.currentVertCount != 9)
			{
				return;
			}
			if (this._all != 0f)
			{
				int i = 0;
				int currentVertCount = vh.currentVertCount;
				while (i < currentVertCount)
				{
					this.Add(vh, this._all, i);
					i++;
				}
			}
			if (this._center != 0f)
			{
				this.Add(vh, this._center, 4);
			}
			if (this._border != 0f)
			{
				int j = 0;
				int currentVertCount2 = vh.currentVertCount;
				while (j < currentVertCount2)
				{
					if (j != 4)
					{
						this.Add(vh, this._border, j);
					}
					j++;
				}
			}
			if (this._centerY != 0f)
			{
				this.Add(vh, this._centerY, 3);
				this.Add(vh, this._centerY, 4);
				this.Add(vh, this._centerY, 5);
			}
			if (this._centerX != 0f)
			{
				this.Add(vh, this._centerX, 1);
				this.Add(vh, this._centerX, 4);
				this.Add(vh, this._centerX, 7);
			}
			if (this._sidesX != 0f)
			{
				this.Add(vh, this._sidesX, 0);
				this.Add(vh, this._sidesX, 3);
				this.Add(vh, this._sidesX, 6);
				this.Add(vh, this._sidesX, 2);
				this.Add(vh, this._sidesX, 5);
				this.Add(vh, this._sidesX, 8);
			}
			if (this._sidesY != 0f)
			{
				this.Add(vh, this._sidesY, 0);
				this.Add(vh, this._sidesY, 1);
				this.Add(vh, this._sidesY, 2);
				this.Add(vh, this._sidesY, 6);
				this.Add(vh, this._sidesY, 7);
				this.Add(vh, this._sidesY, 8);
			}
			if (this._corners != 0f)
			{
				this.Add(vh, this._corners, 0);
				this.Add(vh, this._corners, 2);
				this.Add(vh, this._corners, 6);
				this.Add(vh, this._corners, 8);
			}
		}

		// Token: 0x06003DD0 RID: 15824 RVA: 0x001157BE File Offset: 0x00113BBE
		private void Add(VertexHelper vh, float offset, int index)
		{
			vh.PopulateUIVertex(ref CornerDistanceFieldSettings.vert, index);
			CornerDistanceFieldSettings.vert.uv1.x = CornerDistanceFieldSettings.vert.uv1.x + offset;
			vh.SetUIVertex(CornerDistanceFieldSettings.vert, index);
		}

		// Token: 0x04002B22 RID: 11042
		[SerializeField]
		[Range(-1f, 1f)]
		private float _all;

		// Token: 0x04002B23 RID: 11043
		[SerializeField]
		[Range(-1f, 1f)]
		private float _center;

		// Token: 0x04002B24 RID: 11044
		[SerializeField]
		[Range(-1f, 1f)]
		private float _border;

		// Token: 0x04002B25 RID: 11045
		[SerializeField]
		[Range(-1f, 1f)]
		private float _centerX;

		// Token: 0x04002B26 RID: 11046
		[SerializeField]
		[Range(-1f, 1f)]
		private float _centerY;

		// Token: 0x04002B27 RID: 11047
		[SerializeField]
		[Range(-1f, 1f)]
		private float _sidesX;

		// Token: 0x04002B28 RID: 11048
		[SerializeField]
		[Range(-1f, 1f)]
		private float _sidesY;

		// Token: 0x04002B29 RID: 11049
		[SerializeField]
		[Range(-1f, 1f)]
		private float _corners;

		// Token: 0x04002B2A RID: 11050
		private static UIVertex vert;
	}
}
