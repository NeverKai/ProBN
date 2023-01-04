using System;
using UnityEngine;
using UnityEngine.UI;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x02000910 RID: 2320
	public class SmallBannerGraphic : Graphic
	{
		// Token: 0x06003E2D RID: 15917 RVA: 0x0011763C File Offset: 0x00115A3C
		public void Setup(HeroDefinition heroDef)
		{
			this.sprite = heroDef.graphics.flag;
			this.color = heroDef.color.SetA(this.color.a);
			this.SetAllDirty();
		}

		// Token: 0x06003E2E RID: 15918 RVA: 0x00117680 File Offset: 0x00115A80
		protected override void OnPopulateMesh(VertexHelper vh)
		{
			using (VertexHelper vertexHelper = new VertexHelper())
			{
				vh.Clear();
				Rect textureRect = this.sprite.textureRect;
				Rect rect = (!this.sprite) ? new Rect(0f, 0f, 1f, 1f) : new Rect(Vector2.Scale(textureRect.min, this.sprite.texture.texelSize), Vector2.Scale(textureRect.size, this.sprite.texture.texelSize));
				Rect rect2 = base.rectTransform.rect;
				this.polygon.PupulateRaw(vertexHelper, rect2);
				UIVertex uivertex = default(UIVertex);
				vertexHelper.PopulateUIVertex(ref uivertex, 0);
				float num = 0f;
				int num2 = 0;
				float num3 = float.MaxValue;
				float num4 = float.MinValue;
				for (int i = 0; i < vertexHelper.currentVertCount; i++)
				{
					UIVertex uivertex2 = default(UIVertex);
					vertexHelper.PopulateUIVertex(ref uivertex2, i);
					if (uivertex2.position.y < num3)
					{
						num2 = i;
					}
					num3 = Mathf.Min(num3, uivertex2.position.y);
					num4 = Mathf.Max(num4, uivertex2.position.y);
					if (i > 0)
					{
						Vector3 a = uivertex2.position - uivertex.position;
						a.y *= 0.5f;
						if (a.x < 0f)
						{
							a *= 0.5f;
						}
						float magnitude = a.magnitude;
						num += magnitude;
					}
					uivertex2.uv0.x = num;
					vertexHelper.SetUIVertex(uivertex2, i);
					uivertex = uivertex2;
				}
				float num5 = rect2.height / num / (textureRect.height / textureRect.width);
				for (int j = 0; j < vertexHelper.currentVertCount; j++)
				{
					UIVertex uivertex3 = default(UIVertex);
					vertexHelper.PopulateUIVertex(ref uivertex3, j);
					uivertex3.color = Color.Lerp(this.color * this.color, this.color, ExtraMath.RemapValue(uivertex3.position.y, num3, num4));
					uivertex3.position.x = uivertex3.position.x * num5;
					uivertex3.position.y = rect2.center.y * 2f - uivertex3.position.y;
					uivertex3.uv0.x = ExtraMath.RemapValue(uivertex3.uv0.x, 0f, num, rect.xMin, rect.xMax);
					UIVertex v = uivertex3;
					UIVertex v2 = uivertex3;
					v.uv0.y = rect.yMax;
					v2.uv0.y = rect.yMin;
					v2.position.y = v2.position.y - rect2.height;
					vh.AddVert(v);
					vh.AddVert(v2);
				}
				for (int k = vh.currentVertCount - 4; k >= num2; k -= 2)
				{
					int num6 = k;
					int num7 = k + 1;
					int idx = num6 + 2;
					int idx2 = num7 + 2;
					vh.AddTriangle(num6, idx, idx2);
					vh.AddTriangle(num7, num6, idx2);
				}
				for (int l = 0; l < num2; l += 2)
				{
					int num8 = l;
					int num9 = l + 1;
					int idx3 = num8 + 2;
					int idx4 = num9 + 2;
					vh.AddTriangle(num8, idx3, idx4);
					vh.AddTriangle(num9, num8, idx4);
				}
			}
		}

		// Token: 0x06003E2F RID: 15919 RVA: 0x00117A50 File Offset: 0x00115E50
		protected override void UpdateMaterial()
		{
			base.UpdateMaterial();
			if (this.sprite)
			{
				base.canvasRenderer.SetTexture(this.sprite.texture);
			}
		}

		// Token: 0x04002B79 RID: 11129
		[SerializeField]
		private Sprite sprite;

		// Token: 0x04002B7A RID: 11130
		[SerializeField]
		private UiPolygon polygon;
	}
}
