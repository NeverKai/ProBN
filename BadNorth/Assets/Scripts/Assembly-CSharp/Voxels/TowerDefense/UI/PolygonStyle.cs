using System;
using UnityEngine;
using UnityEngine.UI;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x02000915 RID: 2325
	public class PolygonStyle : ScriptableObject, ColorIconPreview.IIconColors
	{
		// Token: 0x06003E5C RID: 15964 RVA: 0x00118C0C File Offset: 0x0011700C
		public void Populate(VertexHelper vh, VertexHelper srcVh, Sprite sprite, Rect rect, Color baseColor, Vector2 scale)
		{
			if (this.segments.Length == 0)
			{
				return;
			}
			baseColor *= this.color;
			Vector2 vector = Vector2.Scale(scale, this.offset);
			bool flag = this.segments[0].scale == 0f;
			int currentVertCount = srcVh.currentVertCount;
			int num = this.segments.Length;
			int num2 = (!flag) ? num : (num - 1);
			Rect rect2 = (!sprite) ? new Rect(0f, 0f, 1f, 1f) : new Rect(Vector2.Scale(sprite.textureRect.min, sprite.texture.texelSize), Vector2.Scale(sprite.textureRect.size, sprite.texture.texelSize));
			float num3 = 0f;
			if (this.borderMode != PolygonStyle.UvMode.Fill)
			{
				UIVertex uivertex = default(UIVertex);
				srcVh.PopulateUIVertex(ref uivertex, currentVertCount - 1);
				num3 = 1f / uivertex.uv0.x;
				switch (this.borderMode)
				{
				case PolygonStyle.UvMode.BorderFull:
					num3 = 1f;
					break;
				case PolygonStyle.UvMode.BorderHalf:
					num3 *= 2f;
					break;
				case PolygonStyle.UvMode.BorderQuarters:
					num3 *= 4f;
					break;
				case PolygonStyle.UvMode.BorderTiled:
					num3 = uivertex.uv0.x / rect2.width;
					break;
				}
			}
			int currentVertCount2 = vh.currentVertCount;
			if (flag)
			{
				UIVertex v = default(UIVertex);
				PolygonStyle.Segment segment = this.segments[0];
				Color c = segment.color * Color.Lerp(Color.white, baseColor, segment.useBaseColor);
				c.a = segment.color.a * baseColor.a;
				v.normal = Vector3.one;
				v.color = c;
				v.uv0 = rect2.center;
				v.position = rect.center + vector;
				vh.AddVert(v);
			}
			for (int i = 0; i < currentVertCount; i++)
			{
				UIVertex uivertex2 = default(UIVertex);
				srcVh.PopulateUIVertex(ref uivertex2, i);
				uivertex2.normal.z = 1f;
				uivertex2.position += vector;
				uivertex2.color = Color.white;
				if (sprite)
				{
					if (this.borderMode == PolygonStyle.UvMode.Fill)
					{
						uivertex2.uv0 = ExtraMath.RemapValue(uivertex2.position, rect.min, rect.max, rect2.min, rect2.max);
					}
					else
					{
						float num4 = Mathf.Abs(uivertex2.uv0.x * num3 * 2f % 2f - 1f);
						uivertex2.uv0.x = num4 * rect2.width + rect2.xMin;
						uivertex2.uv0.y = rect2.yMin;
					}
				}
				bool flag2 = true;
				for (int j = (!flag) ? 0 : 1; j < num; j++)
				{
					PolygonStyle.Segment segment2 = this.segments[j];
					UIVertex v2 = uivertex2;
					Color c2 = segment2.color * Color.Lerp(Color.white, baseColor, segment2.useBaseColor);
					c2.a = segment2.color.a * baseColor.a;
					v2.color = c2;
					v2.position = rect.center * (1f - segment2.scale) + uivertex2.position * segment2.scale;
					v2.position += uivertex2.normal * segment2.worldOffset;
					v2.uv1.x = segment2.shaderOffset;
					v2.uv1.y = segment2.lineWidthOffset;
					v2.uv0.y = v2.uv0.y + rect2.y * segment2.v;
					vh.AddVert(v2);
					if (i > 0)
					{
						int num5 = vh.currentVertCount - 1;
						if (flag2)
						{
							flag2 = false;
							if (flag)
							{
								int idx = num5;
								int idx2 = num5 - num2;
								vh.AddTriangle(currentVertCount2, idx, idx2);
							}
						}
						else
						{
							int num6 = num5;
							int num7 = num5 - 1;
							int idx3 = num6 - num2;
							int idx4 = num7 - num2;
							vh.AddTriangle(num6, idx3, idx4);
							vh.AddTriangle(num7, num6, idx4);
						}
					}
				}
			}
		}

		// Token: 0x06003E5D RID: 15965 RVA: 0x001190F4 File Offset: 0x001174F4
		Color[] ColorIconPreview.IIconColors.CetIconColors()
		{
			Color[] array = new Color[this.segments.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.segments[i].color * this.color;
			}
			return array;
		}

		// Token: 0x04002B93 RID: 11155
		[SerializeField]
		private PolygonStyle.UvMode borderMode;

		// Token: 0x04002B94 RID: 11156
		[SerializeField]
		private Color color = Color.white;

		// Token: 0x04002B95 RID: 11157
		[SerializeField]
		private PolygonStyle.Segment[] segments = new PolygonStyle.Segment[]
		{
			new PolygonStyle.Segment(0f, 0f, 0f, Color.white, 0f, 1f, 0f),
			new PolygonStyle.Segment(0f, 0f, 1f, Color.white, 0f, 1f, 0f),
			new PolygonStyle.Segment(0f, 1f, 1f, Color.black, 0f, 1f, 0f),
			new PolygonStyle.Segment(0f, 2f, 1f, Color.black.SetA(0f), 0f, 1f, 0f)
		};

		// Token: 0x04002B96 RID: 11158
		[SerializeField]
		private Vector2 offset;

		// Token: 0x02000916 RID: 2326
		[Serializable]
		private struct Segment
		{
			// Token: 0x06003E5E RID: 15966 RVA: 0x0011914C File Offset: 0x0011754C
			public Segment(float worldOffset, float shaderOffset, float scale, Color color, float v = 0f, float useBaseColor = 1f, float useLineWidth = 0f)
			{
				this.worldOffset = worldOffset;
				this.shaderOffset = shaderOffset;
				this.scale = scale;
				this.v = v;
				this.color = color;
				this.useBaseColor = useBaseColor;
				this.lineWidthOffset = useLineWidth;
			}

			// Token: 0x04002B97 RID: 11159
			public float worldOffset;

			// Token: 0x04002B98 RID: 11160
			public float shaderOffset;

			// Token: 0x04002B99 RID: 11161
			public float lineWidthOffset;

			// Token: 0x04002B9A RID: 11162
			public float scale;

			// Token: 0x04002B9B RID: 11163
			public float v;

			// Token: 0x04002B9C RID: 11164
			public float useBaseColor;

			// Token: 0x04002B9D RID: 11165
			public Color color;
		}

		// Token: 0x02000917 RID: 2327
		private enum UvMode
		{
			// Token: 0x04002B9F RID: 11167
			Fill,
			// Token: 0x04002BA0 RID: 11168
			BorderFull,
			// Token: 0x04002BA1 RID: 11169
			BorderHalf,
			// Token: 0x04002BA2 RID: 11170
			BorderQuarters,
			// Token: 0x04002BA3 RID: 11171
			BorderTiled
		}
	}
}
