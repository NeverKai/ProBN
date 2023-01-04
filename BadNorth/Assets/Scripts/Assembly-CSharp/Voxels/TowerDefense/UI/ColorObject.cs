using System;
using UnityEngine;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x02000819 RID: 2073
	public class ColorObject : ScriptableObject, ColorIconPreview.IIconColors
	{
		// Token: 0x06003636 RID: 13878 RVA: 0x000E9CAC File Offset: 0x000E80AC
		Color[] ColorIconPreview.IIconColors.CetIconColors()
		{
			return new Color[]
			{
				this.color
			};
		}

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x06003637 RID: 13879 RVA: 0x000E9CC6 File Offset: 0x000E80C6
		public Color color
		{
			get
			{
				return this._color;
			}
		}

		// Token: 0x040024CC RID: 9420
		[SerializeField]
		private Color _color = Color.white;
	}
}
