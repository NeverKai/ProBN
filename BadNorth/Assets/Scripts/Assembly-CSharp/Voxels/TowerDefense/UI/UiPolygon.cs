using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x02000918 RID: 2328
	public class UiPolygon : ScriptableObject
	{
		// Token: 0x06003E60 RID: 15968 RVA: 0x001191A0 File Offset: 0x001175A0
		private Rect GetUvRect(Sprite sprite)
		{
			if (sprite)
			{
				Vector2 texelSize = sprite.texture.texelSize;
				Rect textureRect = sprite.textureRect;
				return new Rect(Vector2.Scale(textureRect.min, texelSize), Vector2.Scale(textureRect.max, texelSize));
			}
			return new Rect(Vector2.zero, Vector2.one);
		}

		// Token: 0x06003E61 RID: 15969 RVA: 0x001191FC File Offset: 0x001175FC
		public void PupulateRaw(VertexHelper vh, Rect rect)
		{
			UIVertex v = default(UIVertex);
			v.color = Color.white;
			for (int i = 0; i < this.points.Count; i++)
			{
				v.position = this.points[i].GetPosInRect(rect);
				vh.AddVert(v);
			}
		}

		// Token: 0x06003E62 RID: 15970 RVA: 0x00119268 File Offset: 0x00117668
		public void Populate(VertexHelper vh, Rect rect, bool radial = true)
		{
			if (this.points.Count * this.rotations == 0)
			{
				return;
			}
			UIVertex v = default(UIVertex);
			float num = 0f;
			Vector2 a = Vector2.up;
			Vector2 a2 = new Vector2(a.y, -a.x);
			float num2 = 360f / (float)this.rotations;
			float num3 = num2 * 0.017453292f;
			if (this.mirrorMode != UiPolygon.MirrorMode.None)
			{
				num3 *= 2f;
			}
			float num4 = Mathf.Cos(num3);
			float num5 = Mathf.Sin(num3);
			Vector2 vector = Vector2.zero;
			Vector2 vector2 = Vector2.zero;
			int num6 = this.rotations * this.points.Count + ((!radial) ? 1 : 5);
			Vector2 one = Vector2.one;
			Vector2 lhs = new Vector2(-1f, 0f);
			int num7 = 0;
			bool flag = false;
			bool flag2 = true;
			int num8 = 0;
			int num9 = 0;
			int num10 = 0;
			int num11 = 1;
			while (num10 < num6 && num8++ < 1000)
			{
				UiPolygon.Point point = this.points[num9];
				point.anchor = UiPolygon.half - point.anchor;
				point.anchor = point.anchor.x * a2 + point.anchor.y * a;
				point.anchor += UiPolygon.half;
				point.pos = -point.pos.x * a2 - point.pos.y * a;
				Vector2 vector3 = point.GetPosInRect(rect);
				num9 += num11;
				UiPolygon.MirrorMode mirrorMode = this.mirrorMode;
				if (mirrorMode != UiPolygon.MirrorMode.None)
				{
					if (mirrorMode != UiPolygon.MirrorMode.X)
					{
						if (mirrorMode == UiPolygon.MirrorMode.Y)
						{
							vector3.y = rect.center.y + rect.center.y - vector3.y;
							if (num9 == this.points.Count)
							{
								a = -a;
								num11 = -num11;
								num9 += num11;
							}
							else if (num9 < 0)
							{
								a2 = new Vector2(num4 * a2.x - num5 * a2.y, num5 * a2.x + num4 * a2.y);
								a = new Vector2(-a2.y, a2.x);
								num11 = -num11;
								num9 += num11;
							}
						}
					}
					else if (num9 == this.points.Count)
					{
						a2 = -a2;
						num11 = -num11;
						num9 += num11;
					}
					else if (num9 < 0)
					{
						a = new Vector2(num4 * a.x - num5 * a.y, num5 * a.x + num4 * a.y);
						a2 = new Vector2(a.y, -a.x);
						num11 = -num11;
						num9 += num11;
					}
				}
				else if (num9 == this.points.Count)
				{
					a = new Vector2(num4 * a.x - num5 * a.y, num5 * a.x + num4 * a.y);
					a2 = new Vector2(a.y, -a.x);
					num9 = 0;
				}
				if (radial)
				{
					Vector2 lhs2 = new Vector2(Mathf.Sign(rect.center.x - vector3.x), Mathf.Sign(rect.center.y - vector3.y));
					if (lhs2 == one)
					{
						one = new Vector2(-one.y, one.x);
						lhs = new Vector2(-lhs.y, lhs.x);
						num7++;
						if (num7 > 1)
						{
							Vector2 vector4 = vector3 - vector;
							vector3 -= vector4.normalized * Vector2.Dot(lhs, vector3 - rect.center);
							num9 -= num11;
							flag = true;
						}
					}
				}
				if (flag)
				{
					Vector2 vector5 = vector3 - vector;
					if (vector5 == Vector2.zero)
					{
						continue;
					}
					float magnitude = vector5.magnitude;
					Vector2 vector6 = vector5 / magnitude;
					Vector2 vector7 = new Vector2(vector6.y, -vector6.x);
					if (flag2)
					{
						flag2 = false;
					}
					else
					{
						Vector2 vector8 = (vector2 + vector7) * 0.5f;
						float a3 = Vector2.Dot(vector8, vector7);
						Vector2 v2 = vector8 / Mathf.Max(a3, 0.5f);
						v.uv0.x = num;
						num += magnitude;
						v.position = vector;
						v.normal = v2;
						v.tangent = vector2;
						vh.AddVert(v);
						num10++;
					}
					vector2 = vector7;
				}
				else if (!radial)
				{
					flag = true;
				}
				vector = vector3;
			}
		}

		// Token: 0x04002BA4 RID: 11172
		[SerializeField]
		[HideInInspector]
		public List<UiPolygon.Point> points = new List<UiPolygon.Point>();

		// Token: 0x04002BA5 RID: 11173
		[SerializeField]
		private UiPolygon.MirrorMode mirrorMode;

		// Token: 0x04002BA6 RID: 11174
		[SerializeField]
		private int rotations = 4;

		// Token: 0x04002BA7 RID: 11175
		private static Vector2 half = Vector2.one / 2f;

		// Token: 0x02000919 RID: 2329
		[Serializable]
		public struct Point
		{
			// Token: 0x06003E64 RID: 15972 RVA: 0x001197C5 File Offset: 0x00117BC5
			public Point(Vector2 pos, Vector2 anchor)
			{
				this.pos = pos;
				this.anchor = anchor;
			}

			// Token: 0x170008B6 RID: 2230
			// (get) Token: 0x06003E65 RID: 15973 RVA: 0x001197D5 File Offset: 0x00117BD5
			// (set) Token: 0x06003E66 RID: 15974 RVA: 0x00119808 File Offset: 0x00117C08
			public Vector4 vector4
			{
				get
				{
					return new Vector4(this.pos.x, this.pos.y, this.anchor.x, this.anchor.y);
				}
				set
				{
					this.pos.x = value.x;
					this.pos.y = value.y;
					this.anchor.x = value.z;
					this.anchor.y = value.w;
				}
			}

			// Token: 0x06003E67 RID: 15975 RVA: 0x00119860 File Offset: 0x00117C60
			public Vector2 GetPosInRect(Rect rect)
			{
				Vector2 a = this.pos + Vector2.Scale(rect.size, this.anchor);
				return a + rect.position;
			}

			// Token: 0x06003E68 RID: 15976 RVA: 0x0011989A File Offset: 0x00117C9A
			public static UiPolygon.Point Lerp(UiPolygon.Point p0, UiPolygon.Point p1, float t)
			{
				return new UiPolygon.Point(Vector2.Lerp(p0.pos, p1.pos, t), Vector2.Lerp(p0.anchor, p1.anchor, t));
			}

			// Token: 0x04002BA8 RID: 11176
			public Vector2 pos;

			// Token: 0x04002BA9 RID: 11177
			public Vector2 anchor;
		}

		// Token: 0x0200091A RID: 2330
		private enum MirrorMode
		{
			// Token: 0x04002BAB RID: 11179
			None,
			// Token: 0x04002BAC RID: 11180
			X,
			// Token: 0x04002BAD RID: 11181
			Y
		}
	}
}
