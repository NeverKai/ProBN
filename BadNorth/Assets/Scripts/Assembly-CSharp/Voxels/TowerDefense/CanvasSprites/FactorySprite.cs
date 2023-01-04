using System;
using UnityEngine;

namespace Voxels.TowerDefense.CanvasSprites
{
	// Token: 0x02000727 RID: 1831
	[RequireComponent(typeof(RectTransform))]
	public class FactorySprite : MonoBehaviour
	{
		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x06002F8B RID: 12171 RVA: 0x000C0DCB File Offset: 0x000BF1CB
		public RectTransform rectTransform
		{
			get
			{
				if (!this._rectTransform)
				{
					this._rectTransform = base.GetComponent<RectTransform>();
				}
				return this._rectTransform;
			}
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x06002F8C RID: 12172 RVA: 0x000C0DEF File Offset: 0x000BF1EF
		public SpriteFactory spriteFactory
		{
			get
			{
				if (!this._spriteFactory)
				{
					this._spriteFactory = base.GetComponentInParent<SpriteFactory>();
				}
				return this._spriteFactory;
			}
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x06002F8D RID: 12173 RVA: 0x000C0E14 File Offset: 0x000BF214
		public Rect spriteRect
		{
			get
			{
				Canvas canvas = this.spriteFactory.canvas;
				Rect rect = this.rectTransform.rect;
				Vector2 vector = canvas.transform.InverseTransformPoint(base.transform.TransformPoint(rect.min));
				Vector2 a = canvas.transform.InverseTransformPoint(base.transform.TransformPoint(rect.max));
				return new Rect(vector + canvas.pixelRect.size / 2f, a - vector);
			}
		}

		// Token: 0x06002F8E RID: 12174 RVA: 0x000C0EB4 File Offset: 0x000BF2B4
		public void Redraw()
		{
			this.spriteFactory.Render(new FactorySprite[]
			{
				this
			});
		}

		// Token: 0x06002F8F RID: 12175 RVA: 0x000C0ECB File Offset: 0x000BF2CB
		public void UpdateSprite()
		{
			this.sprite = Sprite.Create(this.spriteFactory.tex, this.spriteRect, this.rectTransform.pivot, this.pixelsPerUnit);
		}

		// Token: 0x06002F90 RID: 12176 RVA: 0x000C0EFC File Offset: 0x000BF2FC
		private void OnDrawGizmos()
		{
			Rect rect = this.rectTransform.rect;
			Vector3 vector = base.transform.TransformPoint(rect.min);
			Vector3 vector2 = base.transform.TransformPoint(rect.max);
			Gizmos.DrawWireCube((vector + vector2) / 2f, vector2 - vector);
		}

		// Token: 0x04001FB7 RID: 8119
		private RectTransform _rectTransform;

		// Token: 0x04001FB8 RID: 8120
		private SpriteFactory _spriteFactory;

		// Token: 0x04001FB9 RID: 8121
		[SpritePreview]
		public Sprite sprite;

		// Token: 0x04001FBA RID: 8122
		[SerializeField]
		private float pixelsPerUnit = 100f;
	}
}
