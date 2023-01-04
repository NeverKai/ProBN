using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200090E RID: 2318
public class ScaledScrollItem : MonoBehaviour
{
	// Token: 0x06003E22 RID: 15906 RVA: 0x00117220 File Offset: 0x00115620
	private void Awake()
	{
		this.scrollRect = base.GetComponentInParent<ScrollRect>();
		this.scrollRect.onValueChanged.AddListener(new UnityAction<Vector2>(this.OnScrollValueChanged));
		this.scrollRt = ((!this.scrollRect.viewport) ? this.scrollRect.GetComponent<RectTransform>() : this.scrollRect.viewport);
		this.rt = base.GetComponent<RectTransform>();
	}

	// Token: 0x06003E23 RID: 15907 RVA: 0x00117297 File Offset: 0x00115697
	private void Start()
	{
	}

	// Token: 0x06003E24 RID: 15908 RVA: 0x00117299 File Offset: 0x00115699
	private void OnDestroy()
	{
		if (this.scrollRect)
		{
			this.scrollRect.onValueChanged.RemoveListener(new UnityAction<Vector2>(this.OnScrollValueChanged));
		}
	}

	// Token: 0x06003E25 RID: 15909 RVA: 0x001172C8 File Offset: 0x001156C8
	private void OnScrollValueChanged(Vector2 v)
	{
		Rect rect = this.scrollRt.rect;
		Rect rect2 = this.rt.rect;
		Matrix4x4 matrix4x = this.scrollRt.worldToLocalMatrix * this.rt.localToWorldMatrix;
		Vector2 vector = matrix4x.MultiplyPoint(rect2.min);
		Vector2 a = matrix4x.MultiplyPoint(rect2.max);
		rect2 = new Rect(vector, a - vector);
		Vector2 center = rect2.center;
		Vector2 vector2 = rect2.size / 2f;
		Vector2 vector3;
		vector3.x = Mathf.Clamp(rect2.center.x, rect.min.x + vector2.x, rect.max.x - vector2.x);
		vector3.y = Mathf.Clamp(rect2.center.y, rect.min.y + vector2.y, rect.max.y - vector2.y);
		Vector2 lhs = rect2.center - vector3;
		Vector2 v2 = vector3;
		float d = 1f;
		if (lhs != Vector2.zero)
		{
			Vector2 b;
			b.x = Mathf.Clamp01(Mathf.Abs(lhs.x) / rect2.size.x);
			b.y = Mathf.Clamp01(Mathf.Abs(lhs.y) / rect2.size.y);
			Vector2 vector4 = Vector2.Lerp(Vector2.zero, b, this.scaleFactor);
			d = 1f - Mathf.Max(vector4.x, vector4.y);
			v2.x += Mathf.Sign(lhs.x) * vector4.x * vector2.x;
			v2.y += Mathf.Sign(lhs.y) * vector4.y * vector2.y;
		}
		this.target.transform.position = this.scrollRt.TransformPoint(v2);
		this.target.transform.localScale = Vector3.one * d;
	}

	// Token: 0x04002B6F RID: 11119
	private RectTransform rt;

	// Token: 0x04002B70 RID: 11120
	private RectTransform scrollRt;

	// Token: 0x04002B71 RID: 11121
	private ScrollRect scrollRect;

	// Token: 0x04002B72 RID: 11122
	[SerializeField]
	private float scaleFactor = 1f;

	// Token: 0x04002B73 RID: 11123
	[SerializeField]
	private RectTransform target;
}
