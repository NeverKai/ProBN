using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000930 RID: 2352
public class SimplerCurve : MaskableGraphic
{
	// Token: 0x06003F22 RID: 16162 RVA: 0x0011D174 File Offset: 0x0011B574
	protected override void OnPopulateMesh(VertexHelper vh)
	{
		base.OnPopulateMesh(vh);
		if (!this.connection0.transform || !this.connection1.transform)
		{
			return;
		}
		vh.Clear();
		Vector2 vector = base.transform.InverseTransformPoint(this.connection0.GetPos());
		Vector2 vector2 = base.transform.InverseTransformPoint(this.connection1.GetPos());
		float d = this.width / 2f;
		Vector2 p = vector;
		Vector2 zero = Vector2.zero;
		Vector2 p2 = vector2;
		float num = p.magnitude + p2.magnitude;
		int num2 = Mathf.Max(2, Mathf.RoundToInt(num / 10f));
		Vector2 vector3;
		Vector2 vector4;
		if (this.sprite)
		{
			Rect textureRect = this.sprite.textureRect;
			vector3 = new Vector2(textureRect.center.x, textureRect.yMin);
			vector4 = new Vector2(textureRect.center.x, textureRect.yMax);
			vector3 = Vector2.Scale(vector3, this.sprite.texture.texelSize);
			vector4 = Vector2.Scale(vector4, this.sprite.texture.texelSize);
		}
		else
		{
			vector3 = new Vector2(0.5f, 0f);
			vector4 = new Vector2(0.5f, 1f);
		}
		for (int i = 0; i < num2; i++)
		{
			float t = (float)i / ((float)num2 - 1f);
			Vector2 a = ExtraMath.Bezier(p, zero, p2, t);
			Vector2 normalized = ExtraMath.Rotate2D90(ExtraMath.BezierDerivate(p, zero, p2, t)).normalized;
			UIVertex v = new UIVertex
			{
				position = a + normalized * d,
				color = Color.white,
				uv0 = vector3
			};
			UIVertex v2 = new UIVertex
			{
				position = a - normalized * d,
				color = Color.white,
				uv0 = vector4
			};
			vh.AddVert(v);
			vh.AddVert(v2);
		}
		int j = 0;
		int num3 = vh.currentVertCount - 2;
		while (j < num3)
		{
			vh.AddTriangle(j, j + 1, j + 2);
			vh.AddTriangle(j + 1, j + 3, j + 2);
			j += 2;
		}
	}

	// Token: 0x06003F23 RID: 16163 RVA: 0x0011D411 File Offset: 0x0011B811
	private void AddSegment(SimplerCurve.Segment segment0, SimplerCurve.Segment segment1)
	{
	}

	// Token: 0x06003F24 RID: 16164 RVA: 0x0011D413 File Offset: 0x0011B813
	protected override void UpdateMaterial()
	{
		base.UpdateMaterial();
		if (this.sprite)
		{
			base.canvasRenderer.SetTexture(this.sprite.texture);
		}
		base.canvasRenderer.SetColor(this.color);
	}

	// Token: 0x06003F25 RID: 16165 RVA: 0x0011D452 File Offset: 0x0011B852
	[ContextMenu("SetDirty")]
	private void SetDirty()
	{
		this.SetAllDirty();
	}

	// Token: 0x06003F26 RID: 16166 RVA: 0x0011D45A File Offset: 0x0011B85A
	private void OnDrawGizmos()
	{
		this.SetVerticesDirty();
		Gizmos.color = this.color * 2f;
		Gizmos.DrawSphere(base.transform.position, 5f);
	}

	// Token: 0x04002C29 RID: 11305
	[SerializeField]
	private SimplerCurve.Connection connection0 = new SimplerCurve.Connection(null, Vector2.one / 2f, Vector2.zero);

	// Token: 0x04002C2A RID: 11306
	[SerializeField]
	private SimplerCurve.Connection connection1 = new SimplerCurve.Connection(null, Vector2.one / 2f, Vector2.zero);

	// Token: 0x04002C2B RID: 11307
	[SerializeField]
	private Sprite sprite;

	// Token: 0x04002C2C RID: 11308
	[SerializeField]
	private float width = 10f;

	// Token: 0x04002C2D RID: 11309
	private const int spacing = 10;

	// Token: 0x02000931 RID: 2353
	[Serializable]
	private struct Connection
	{
		// Token: 0x06003F27 RID: 16167 RVA: 0x0011D48C File Offset: 0x0011B88C
		public Connection(RectTransform transform, Vector2 anchor, Vector2 offset)
		{
			this.transform = transform;
			this.anchor = anchor;
			this.offset = offset;
		}

		// Token: 0x06003F28 RID: 16168 RVA: 0x0011D4A4 File Offset: 0x0011B8A4
		public Vector2 GetPos()
		{
			Rect rect = this.transform.rect;
			return this.transform.TransformPoint(rect.min + Vector2.Scale(rect.size, this.anchor));
		}

		// Token: 0x04002C2E RID: 11310
		public RectTransform transform;

		// Token: 0x04002C2F RID: 11311
		public Vector2 anchor;

		// Token: 0x04002C30 RID: 11312
		public Vector2 offset;
	}

	// Token: 0x02000932 RID: 2354
	private struct Segment
	{
		// Token: 0x04002C31 RID: 11313
		public Vector2 pos;

		// Token: 0x04002C32 RID: 11314
		public Vector2 dir;

		// Token: 0x04002C33 RID: 11315
		private float t;
	}
}
