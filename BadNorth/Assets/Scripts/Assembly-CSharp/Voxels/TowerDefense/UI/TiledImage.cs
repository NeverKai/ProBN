using System;
using UnityEngine;
using UnityEngine.UI;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x02000911 RID: 2321
	public class TiledImage : MaskableGraphic
	{
		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x06003E31 RID: 15921 RVA: 0x00117A98 File Offset: 0x00115E98
		// (set) Token: 0x06003E32 RID: 15922 RVA: 0x00117AA0 File Offset: 0x00115EA0
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

		// Token: 0x06003E33 RID: 15923 RVA: 0x00117AB0 File Offset: 0x00115EB0
		protected override void OnPopulateMesh(VertexHelper vh)
		{
			if (!this._sprite)
			{
				base.OnPopulateMesh(vh);
				return;
			}
			vh.Clear();
			Rect rect = base.rectTransform.rect;
			Rect textureRect = this.sprite.textureRect;
			Rect rect2 = new Rect(Vector2.Scale(textureRect.min, this.sprite.texture.texelSize), Vector2.Scale(textureRect.size, this.sprite.texture.texelSize));
			UIVertex v = new UIVertex
			{
				color = Color.white
			};
			float num = rect.width / 2f;
			float x = rect.center.x;
			float num2 = textureRect.width;
			num2 *= this.aspectRatio;
			if (this.keepAspectRatio)
			{
				num2 *= rect.height / textureRect.height;
			}
			int num3 = Mathf.CeilToInt(num / num2);
			for (int i = 0; i <= num3; i++)
			{
				float num4 = Mathf.Min(num, (float)i * num2);
				float num5 = num4 / num2;
				num5 = Mathf.Abs((num5 + 1f) % 2f - 1f);
				v.uv0.x = rect2.xMin + (float)(i % 2) * rect2.width;
				v.uv0.x = rect2.xMin + num5 * rect2.width;
				v.uv0.y = rect2.yMin;
				v.position.x = x + num4;
				v.position.y = rect.yMin;
				vh.AddVert(v);
				v.uv0.y = rect2.yMax;
				v.position.y = rect.yMax;
				vh.AddVert(v);
				v.position.x = x - num4;
				vh.AddVert(v);
				v.uv0.y = rect2.yMin;
				v.position.y = rect.yMin;
				vh.AddVert(v);
			}
			for (int j = 0; j < num3; j++)
			{
				int num6 = j * 4;
				vh.AddTriangle(num6 + 1, num6 + 4, num6);
				vh.AddTriangle(num6 + 1, num6 + 5, num6 + 4);
				vh.AddTriangle(num6 + 2, num6 + 3, num6 + 7);
				vh.AddTriangle(num6 + 2, num6 + 7, num6 + 6);
			}
		}

		// Token: 0x06003E34 RID: 15924 RVA: 0x00117D48 File Offset: 0x00116148
		protected override void UpdateMaterial()
		{
			base.UpdateMaterial();
			base.canvasRenderer.SetTexture((!this._sprite) ? null : this._sprite.texture);
			base.canvasRenderer.SetColor(this.color);
		}

		// Token: 0x04002B7B RID: 11131
		[SerializeField]
		private Sprite _sprite;

		// Token: 0x04002B7C RID: 11132
		[SerializeField]
		private float aspectRatio = 1f;

		// Token: 0x04002B7D RID: 11133
		[SerializeField]
		private bool keepAspectRatio = true;
	}
}
