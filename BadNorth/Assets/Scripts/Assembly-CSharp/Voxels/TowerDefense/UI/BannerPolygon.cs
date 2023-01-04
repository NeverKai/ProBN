using System;
using UnityEngine;
using UnityEngine.UI;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x020008A5 RID: 2213
	public class BannerPolygon : MaskableGraphic
	{
		// Token: 0x060039D6 RID: 14806 RVA: 0x000FCF48 File Offset: 0x000FB348
		public void Setup(HeroDefinition heroDef, bool faded = false)
		{
			this.sprite = heroDef.graphics.flag;
			this.color = heroDef.color;
			float h;
			float num;
			float num2;
			Color.RGBToHSV(this.color, out h, out num, out num2);
			num *= ((!faded) ? 0.6f : 0.3f);
			num2 *= ((!faded) ? 0.9f : 0.55f);
			this.color = Color.HSVToRGB(h, num, num2);
			this.SetVerticesDirty();
		}

		// Token: 0x060039D7 RID: 14807 RVA: 0x000FCFC8 File Offset: 0x000FB3C8
		protected override void OnPopulateMesh(VertexHelper vh)
		{
			if (!this.polygon)
			{
				return;
			}
			using (VertexHelper vertexHelper = new VertexHelper())
			{
				vh.Clear();
				Rect rect = default(Rect);
				Rect rect2 = base.rectTransform.rect;
				float num = 0f;
				float num2 = 0f;
				float num3 = 0f;
				float num4 = 1f;
				if (this.sprite)
				{
					Rect textureRect = this.sprite.textureRect;
					rect.min = Vector2.Scale(textureRect.min + new Vector2(this.sprite.border.x + 2f, 0f), this.sprite.texture.texelSize);
					rect.max = Vector2.Scale(textureRect.max, this.sprite.texture.texelSize);
					float num5 = textureRect.height - this.sprite.border.y - this.sprite.border.w;
					num = -(this.sprite.border.y / num5) * rect2.height;
					num2 = this.sprite.border.w / num5 * rect2.height;
					num3 = rect2.height + num2 - num;
					num4 = rect.height / num3;
					num4 *= this.sprite.texture.texelSize.x / this.sprite.texture.texelSize.y;
				}
				else
				{
					new Rect(0f, 0f, 1f, 1f);
				}
				Rect rect3 = rect2;
				rect3.xMin = rect3.xMax - rect3.height;
				this.polygon.PupulateRaw(vertexHelper, rect3);
				UIVertex uivertex = default(UIVertex);
				uivertex.color = this.color;
				uivertex.uv0.x = rect.xMin;
				UIVertex v = uivertex;
				UIVertex v2 = uivertex;
				v.uv0.y = rect.yMax;
				v2.uv0.y = rect.yMin;
				v.position = new Vector2(rect2.xMin, rect2.yMax + num2);
				v2.position = new Vector2(rect2.xMin, rect2.yMin + num);
				vh.AddVert(v);
				vh.AddVert(v2);
				float num6 = 0f;
				UIVertex uivertex2 = default(UIVertex);
				for (int i = 0; i < vertexHelper.currentVertCount; i++)
				{
					UIVertex uivertex3 = default(UIVertex);
					vertexHelper.PopulateUIVertex(ref uivertex3, i);
					uivertex3.position.y = rect2.center.y * 2f - uivertex3.position.y + num2;
					uivertex3.color = Color.Lerp(this.color, this.color * this.color, ExtraMath.RemapValue(uivertex3.position.y, rect2.yMax, rect2.yMin));
					uivertex3.uv0.x = rect.xMin;
					if (i > 0)
					{
						float magnitude = (uivertex3.position - uivertex2.position).magnitude;
						num6 += magnitude;
						uivertex3.uv0.x = uivertex3.uv0.x + num6 * num4;
					}
					if (uivertex3.uv0.x > rect.xMax)
					{
						i = vertexHelper.currentVertCount;
						uivertex3.uv0.x = rect.xMax;
					}
					uivertex2 = uivertex3;
					UIVertex v3 = uivertex3;
					UIVertex v4 = uivertex3;
					v3.uv0.y = rect.yMax;
					v4.uv0.y = rect.yMin;
					v4.position.y = v4.position.y - num3;
					vh.AddVert(v3);
					vh.AddVert(v4);
				}
				if (this.addEnd)
				{
					UIVertex uivertex4 = uivertex2;
					uivertex4.color = this.color;
					uivertex4.uv0.x = rect.xMax;
					uivertex4.position.x = uivertex4.position.x + (uivertex4.uv0.x - uivertex2.uv0.x) / num4;
					UIVertex v5 = uivertex4;
					UIVertex v6 = uivertex4;
					v5.uv0.y = rect.yMax;
					v6.uv0.y = rect.yMin;
					v6.position.y = v6.position.y - num3;
					vh.AddVert(v5);
					vh.AddVert(v6);
				}
				for (int j = vh.currentVertCount - 4; j >= 0; j -= 2)
				{
					int num7 = j;
					int num8 = j + 1;
					int idx = num7 + 2;
					int idx2 = num8 + 2;
					vh.AddTriangle(num7, idx, idx2);
					vh.AddTriangle(num8, num7, idx2);
				}
			}
		}

		// Token: 0x060039D8 RID: 14808 RVA: 0x000FD530 File Offset: 0x000FB930
		protected override void UpdateMaterial()
		{
			base.UpdateMaterial();
			if (this.sprite)
			{
				base.canvasRenderer.SetTexture(this.sprite.texture);
			}
		}

		// Token: 0x040027E2 RID: 10210
		[SerializeField]
		private Sprite sprite;

		// Token: 0x040027E3 RID: 10211
		[SerializeField]
		private UiPolygon polygon;

		// Token: 0x040027E4 RID: 10212
		[SerializeField]
		private bool addEnd = true;
	}
}
