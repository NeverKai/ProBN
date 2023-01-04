using System;
using UnityEngine;

// Token: 0x0200050D RID: 1293
public static class RectTransformExtensions
{
	// Token: 0x06002138 RID: 8504 RVA: 0x0005B700 File Offset: 0x00059B00
	public static Rect GetHirearchyRect(this RectTransform rt)
	{
		RectTransform[] componentsInChildren = rt.GetComponentsInChildren<RectTransform>();
		Rect rect = componentsInChildren[0].GetWorldSpaceRect();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			rect = ExtraMath.Encapsulate(rect, componentsInChildren[i].GetWorldSpaceRect());
		}
		return rect;
	}

	// Token: 0x06002139 RID: 8505 RVA: 0x0005B744 File Offset: 0x00059B44
	public static Rect GetWorldSpaceRect(this RectTransform rt)
	{
		Rect rect = rt.rect;
		Vector2 vector = rt.TransformPoint(rect.min);
		Vector2 a = rt.TransformPoint(rect.max);
		return new Rect(vector, a - vector);
	}

	// Token: 0x0600213A RID: 8506 RVA: 0x0005B798 File Offset: 0x00059B98
	public static Rect GetScreenSpaceNormalizedRect(this RectTransform rt, Camera camera)
	{
		Vector2 vector = rt.TransformPoint(rt.rect.min);
		Vector2 a = rt.TransformPoint(rt.rect.max);
		vector.x /= (float)Screen.width;
		a.x /= (float)Screen.width;
		vector.y /= (float)Screen.height;
		a.y /= (float)Screen.height;
		return new Rect(vector, a - vector);
	}
}
