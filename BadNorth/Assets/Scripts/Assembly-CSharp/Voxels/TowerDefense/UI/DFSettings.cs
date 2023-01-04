using System;
using UnityEngine;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x02000909 RID: 2313
	[Serializable]
	public struct DFSettings
	{
		// Token: 0x06003DEE RID: 15854 RVA: 0x0011629D File Offset: 0x0011469D
		public DFSettings(float fill, float fraction, float pixelWidth, float shadow, int bands)
		{
			this.fill = fill;
			this.fraction = fraction;
			this.pixelWidth = pixelWidth;
			this.shadow = shadow;
			this.bands = bands;
		}

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x06003DEF RID: 15855 RVA: 0x001162C4 File Offset: 0x001146C4
		public Vector2 uv1
		{
			get
			{
				return new Vector2(this.fraction - 0.5f, this.pixelWidth - 1f);
			}
		}

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x06003DF0 RID: 15856 RVA: 0x001162E3 File Offset: 0x001146E3
		public Vector2 uv2
		{
			get
			{
				return new Vector2(this.fill - 1f, this.shadow * 0.99f + (float)this.bands);
			}
		}

		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x06003DF1 RID: 15857 RVA: 0x0011630A File Offset: 0x0011470A
		public static DFSettings standard
		{
			get
			{
				return new DFSettings(1f, 0.5f, 1f, 0f, 0);
			}
		}

		// Token: 0x06003DF2 RID: 15858 RVA: 0x00116326 File Offset: 0x00114726
		public void SetVert(ref UIVertex vert)
		{
			vert.uv1 = this.uv1;
			vert.uv2 = this.uv2;
			vert.normal = Vector3.forward;
		}

		// Token: 0x04002B41 RID: 11073
		[Range(0f, 1f)]
		public float fill;

		// Token: 0x04002B42 RID: 11074
		[Range(0f, 1f)]
		public float fraction;

		// Token: 0x04002B43 RID: 11075
		public float pixelWidth;

		// Token: 0x04002B44 RID: 11076
		public float shadow;

		// Token: 0x04002B45 RID: 11077
		public int bands;
	}
}
