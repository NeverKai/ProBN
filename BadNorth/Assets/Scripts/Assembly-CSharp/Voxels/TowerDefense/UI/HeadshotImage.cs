using System;
using UnityEngine;
using UnityEngine.UI;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x020008D5 RID: 2261
	public class HeadshotImage : MaskableGraphic
	{
		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x06003BCC RID: 15308 RVA: 0x00109556 File Offset: 0x00107956
		// (set) Token: 0x06003BCD RID: 15309 RVA: 0x0010955E File Offset: 0x0010795E
		public Sprite sprite
		{
			get
			{
				return this._sprite;
			}
			set
			{
				this._sprite = value;
				this.SetAllDirty();
			}
		}

		// Token: 0x06003BCE RID: 15310 RVA: 0x00109570 File Offset: 0x00107970
		protected override void OnPopulateMesh(VertexHelper vh)
		{
			vh.Clear();
			vh.AddTriangle(0, 2, 1);
			vh.AddTriangle(1, 2, 3);
			Rect rect = base.rectTransform.rect;
			if (!this._sprite)
			{
				return;
			}
			Rect textureRect = this._sprite.textureRect;
			textureRect = new Rect(textureRect.xMin / (float)this._sprite.texture.width, textureRect.yMin / (float)this._sprite.texture.height, textureRect.width / (float)this._sprite.texture.width, textureRect.height / (float)this._sprite.texture.height);
			Color32 color = Color.white;
			UIVertex uivertex = new UIVertex
			{
				color = color,
				position = new Vector2(rect.xMin, rect.yMin),
				uv0 = new Vector2((!this.flipX) ? textureRect.xMax : textureRect.xMin, textureRect.yMin),
				uv1 = new Vector2(0f, 0f)
			};
			UIVertex uivertex2 = uivertex;
			uivertex = new UIVertex
			{
				color = color,
				position = new Vector2(rect.xMax, rect.yMin),
				uv0 = new Vector2((!this.flipX) ? textureRect.xMin : textureRect.xMax, textureRect.yMin),
				uv1 = new Vector2(1f, 0f)
			};
			UIVertex vertex = uivertex;
			uivertex = new UIVertex
			{
				color = color,
				position = new Vector2(rect.xMin, rect.yMax),
				uv0 = new Vector2((!this.flipX) ? textureRect.xMax : textureRect.xMin, textureRect.yMax),
				uv1 = new Vector2(0f, 1f)
			};
			UIVertex vertex2 = uivertex;
			uivertex = new UIVertex
			{
				color = color,
				position = new Vector2(rect.xMax, rect.yMax),
				uv0 = new Vector2((!this.flipX) ? textureRect.xMin : textureRect.xMax, textureRect.yMax),
				uv1 = new Vector2(1f, 1f)
			};
			UIVertex vertex3 = uivertex;
			for (int i = 0; i < 4; i++)
			{
				vh.AddVert(uivertex2);
			}
			vh.SetUIVertex(uivertex2, 0);
			vh.SetUIVertex(vertex, 1);
			vh.SetUIVertex(vertex2, 2);
			vh.SetUIVertex(vertex3, 3);
		}

		// Token: 0x06003BCF RID: 15311 RVA: 0x0010985C File Offset: 0x00107C5C
		protected override void UpdateMaterial()
		{
			base.UpdateMaterial();
			base.canvasRenderer.SetTexture((!this._sprite) ? null : this._sprite.texture);
			base.canvasRenderer.SetColor(this.color);
		}

		// Token: 0x04002991 RID: 10641
		[SerializeField]
		private Sprite _sprite;

		// Token: 0x04002992 RID: 10642
		[SerializeField]
		private bool flipX;
	}
}
