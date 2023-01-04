using System;
using System.Diagnostics;
using UnityEngine;

// Token: 0x020005DD RID: 1501
public static class DebugHelpers
{
	// Token: 0x060026FB RID: 9979 RVA: 0x0007D61C File Offset: 0x0007BA1C
	[Conditional("UNITY_EDITOR")]
	[Conditional("DEVELOPMENT_BUILD")]
	public static void DebugDrawCircle(Vector3 center, float radius, Color color, float duration)
	{
		float num = 0f;
		float num2 = 0.19634955f;
		while (num < 6.2831855f)
		{
			float f = num + num2;
			Vector3 start = center + new Vector3(Mathf.Cos(num), 0f, Mathf.Sin(num)) * radius;
			Vector3 end = center + new Vector3(Mathf.Cos(f), 0f, Mathf.Sin(f)) * radius;
			UnityEngine.Debug.DrawLine(start, end, color, duration);
			num += num2;
		}
	}

	// Token: 0x060026FC RID: 9980 RVA: 0x0007D6A0 File Offset: 0x0007BAA0
	public static string GetPath(this Transform transform)
	{
		string str = transform.gameObject.scene.name + "/";
		string text = string.Empty;
		while (transform)
		{
			text = transform.name + "/" + text;
			transform = transform.parent;
		}
		return str + text;
	}

	// Token: 0x060026FD RID: 9981 RVA: 0x0007D702 File Offset: 0x0007BB02
	public static string GetPath(this GameObject gameObject)
	{
		return gameObject.transform.GetPath();
	}

	// Token: 0x060026FE RID: 9982 RVA: 0x0007D70F File Offset: 0x0007BB0F
	public static string GetPath(this Component component)
	{
		return component.transform.GetPath();
	}
}
