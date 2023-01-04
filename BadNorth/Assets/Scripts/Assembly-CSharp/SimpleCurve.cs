using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200092D RID: 2349
public class SimpleCurve : MaskableGraphic
{
	// Token: 0x06003F14 RID: 16148 RVA: 0x0011C9E4 File Offset: 0x0011ADE4
	private static Vector2 SampleBezierPoint(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, float t)
	{
		float num = 1f - t;
		return num * num * num * p0 + 3f * num * num * t * p1 + 3f * num * t * t * p2 + t * t * t * p3;
	}

	// Token: 0x06003F15 RID: 16149 RVA: 0x0011CA48 File Offset: 0x0011AE48
	private static Vector2 SampleBezierDerivate(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, float t)
	{
		float num = 1f - t;
		return 6f * num * (p2 - 2f * p1 + p0) + 6f * t * (p3 - 2f * p2 + p1);
	}

	// Token: 0x06003F16 RID: 16150 RVA: 0x0011CAAC File Offset: 0x0011AEAC
	private SimpleCurve.Bezier GetBezier(int p)
	{
		Vector2 vector;
		Vector2 b;
		this.points[p].Get(out vector, out b, base.rectTransform);
		Vector2 vector2;
		Vector2 b2;
		this.points[p + 1].Get(out vector2, out b2, base.rectTransform);
		return new SimpleCurve.Bezier(vector, vector + b, vector2 - b2, vector2);
	}

	// Token: 0x06003F17 RID: 16151 RVA: 0x0011CB10 File Offset: 0x0011AF10
	protected override void OnPopulateMesh(VertexHelper vh)
	{
		vh.Clear();
		float d = this.width * 0.5f;
		bool flag = this.sprite;
		Rect spriteUv = (!flag) ? new Rect(0f, 0f, 1f, 1f) : new Rect(this.sprite.textureRect.xMin / (float)this.sprite.texture.width, this.sprite.textureRect.yMin / (float)this.sprite.texture.height, this.sprite.textureRect.width / (float)this.sprite.texture.width, this.sprite.textureRect.height / (float)this.sprite.texture.height);
		float num = (!flag) ? 100f : this.sprite.rect.width;
		int num2 = Mathf.CeilToInt(num / 10f);
		float num3 = num / (float)num2;
		int num4 = 0;
		int num5 = 0;
		for (int i = 0; i < this.points.Count - 1; i++)
		{
			SimpleCurve.Bezier bezier = this.GetBezier(i);
			Vector2 b = bezier.p1 - bezier.p0;
			Vector2 vector = bezier.p0;
			Vector2 b2 = bezier.p0 - b;
			float num6 = 0f;
			Vector2 b3 = bezier.p0;
			for (float num7 = 0.2f; num7 < 1f; num7 += 0.2f)
			{
				Vector2 vector2 = bezier.SamplePoint(num7);
				num6 += (vector2 - b3).magnitude;
				b3 = vector2;
			}
			int num8 = Mathf.CeilToInt(num6 / num3);
			for (int j = 1; j <= num8; j++)
			{
				float t = (float)j / (float)num8;
				Vector2 vector3 = bezier.SamplePoint(t);
				Vector2 normalized = this.Rotate90(vector - b2).normalized;
				Vector2 normalized2 = this.Rotate90(vector3 - vector).normalized;
				Vector2 vector4 = normalized + normalized2;
				vector4 /= Vector2.Dot(vector4, normalized);
				vector4 *= d;
				this.AddSegment(vh, spriteUv, vector, vector4, num2, ref num5, ref num4);
				b2 = vector;
				vector = vector3;
			}
			if (i == this.points.Count - 2)
			{
				this.AddSegment(vh, spriteUv, vector, this.Rotate90(bezier.p3 - bezier.p2).normalized * d, num2, ref num5, ref num4);
			}
		}
		for (int k = 0; k < num4 * 2 - 2; k += 2)
		{
			vh.AddTriangle(k, k + 1, k + 3);
			vh.AddTriangle(k + 2, k, k + 3);
		}
	}

	// Token: 0x06003F18 RID: 16152 RVA: 0x0011CE28 File Offset: 0x0011B228
	private void AddSegment(VertexHelper vh, Rect spriteUv, Vector2 pos, Vector2 normal, int segmentsPerSprite, ref int spriteSegments, ref int segmentCount)
	{
		float x;
		if (++spriteSegments >= segmentsPerSprite)
		{
			vh.AddVert(pos - normal, Color.white, new Vector2(spriteUv.xMax, spriteUv.yMin));
			vh.AddVert(pos + normal, Color.white, new Vector2(spriteUv.xMax, spriteUv.yMax));
			spriteSegments = 0;
			x = spriteUv.xMin;
			segmentCount++;
		}
		else
		{
			x = spriteUv.xMin + spriteUv.width / (float)segmentsPerSprite * (float)spriteSegments;
		}
		vh.AddVert(pos - normal, Color.white, new Vector2(x, spriteUv.yMin));
		vh.AddVert(pos + normal, Color.white, new Vector2(x, spriteUv.yMax));
		segmentCount++;
	}

	// Token: 0x06003F19 RID: 16153 RVA: 0x0011CF35 File Offset: 0x0011B335
	private Vector2 Rotate90(Vector2 v)
	{
		return new Vector2(-v.y, v.x);
	}

	// Token: 0x06003F1A RID: 16154 RVA: 0x0011CF4C File Offset: 0x0011B34C
	protected override void UpdateMaterial()
	{
		base.UpdateMaterial();
		base.canvasRenderer.SetTexture((!this.sprite) ? null : this.sprite.texture);
		base.canvasRenderer.SetColor(this.color);
	}

	// Token: 0x06003F1B RID: 16155 RVA: 0x0011CF9C File Offset: 0x0011B39C
	[ContextMenu("SetDirty")]
	private void SetDirty()
	{
		this.SetAllDirty();
	}

	// Token: 0x04002C1E RID: 11294
	[SerializeField]
	private Sprite sprite;

	// Token: 0x04002C1F RID: 11295
	[SerializeField]
	private float width = 10f;

	// Token: 0x04002C20 RID: 11296
	[SerializeField]
	public List<SimpleCurve.Point> points = new List<SimpleCurve.Point>
	{
		new SimpleCurve.Point(Vector2.zero, Vector2.zero, Vector2.right, null),
		new SimpleCurve.Point(Vector2.one, Vector2.zero, Vector2.left, null)
	};

	// Token: 0x0200092E RID: 2350
	[Serializable]
	public struct Point
	{
		// Token: 0x06003F1C RID: 16156 RVA: 0x0011CFA4 File Offset: 0x0011B3A4
		public Point(Vector2 pivot, Vector2 offset, Vector2 tangent, RectTransform rt = null)
		{
			this.pivot = pivot;
			this.offset = offset;
			this.tangent = tangent;
			this.rt = rt;
		}

		// Token: 0x06003F1D RID: 16157 RVA: 0x0011CFC4 File Offset: 0x0011B3C4
		public void Get(out Vector2 pos, out Vector2 tan, RectTransform localTransform)
		{
			RectTransform rectTransform = (!this.rt) ? localTransform : this.rt;
			Rect rect = rectTransform.rect;
			pos.x = rect.xMin + rect.width * this.pivot.x + this.offset.x;
			pos.y = rect.yMin + rect.height * this.pivot.y + this.offset.y;
			tan = this.tangent;
			if (localTransform != rectTransform)
			{
				Matrix4x4 matrix4x = localTransform.worldToLocalMatrix * rectTransform.localToWorldMatrix;
				pos = matrix4x.MultiplyPoint(pos);
				tan = matrix4x.MultiplyVector(tan);
			}
		}

		// Token: 0x04002C21 RID: 11297
		public Vector2 pivot;

		// Token: 0x04002C22 RID: 11298
		public Vector2 offset;

		// Token: 0x04002C23 RID: 11299
		public Vector2 tangent;

		// Token: 0x04002C24 RID: 11300
		public RectTransform rt;
	}

	// Token: 0x0200092F RID: 2351
	private struct Bezier
	{
		// Token: 0x06003F1E RID: 16158 RVA: 0x0011D0B3 File Offset: 0x0011B4B3
		public Bezier(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
		{
			this.p0 = p0;
			this.p1 = p1;
			this.p2 = p2;
			this.p3 = p3;
		}

		// Token: 0x06003F1F RID: 16159 RVA: 0x0011D0D2 File Offset: 0x0011B4D2
		public Vector2 SamplePoint(float t)
		{
			return SimpleCurve.SampleBezierPoint(this.p0, this.p1, this.p2, this.p3, t);
		}

		// Token: 0x06003F20 RID: 16160 RVA: 0x0011D0F2 File Offset: 0x0011B4F2
		public Vector2 SampleDerivate(float t)
		{
			return SimpleCurve.SampleBezierPoint(this.p0, this.p1, this.p2, this.p3, t);
		}

		// Token: 0x04002C25 RID: 11301
		public Vector2 p0;

		// Token: 0x04002C26 RID: 11302
		public Vector2 p1;

		// Token: 0x04002C27 RID: 11303
		public Vector2 p2;

		// Token: 0x04002C28 RID: 11304
		public Vector2 p3;
	}
}
