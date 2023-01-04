using System;
using UnityEngine;

namespace Voxels.TowerDefense.SpriteMagic
{
	// Token: 0x020007D3 RID: 2003
	[RequireComponent(typeof(SpriteRenderer))]
	public class MergeableSprite : MonoBehaviour
	{
		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x060033ED RID: 13293 RVA: 0x000E001C File Offset: 0x000DE41C
		public SpriteRenderer spriteRenderer
		{
			get
			{
				if (!this._spriteRenderer)
				{
					this._spriteRenderer = base.GetComponent<SpriteRenderer>();
				}
				return this._spriteRenderer;
			}
		}

		// Token: 0x0400235F RID: 9055
		public NavPos navPos;

		// Token: 0x04002360 RID: 9056
		public bool randomFlip = true;

		// Token: 0x04002361 RID: 9057
		private SpriteRenderer _spriteRenderer;
	}
}
