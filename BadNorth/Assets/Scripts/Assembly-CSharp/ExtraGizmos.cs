using System;
using UnityEngine;

// Token: 0x02000511 RID: 1297
public static class ExtraGizmos
{
	// Token: 0x0600216D RID: 8557 RVA: 0x0005C224 File Offset: 0x0005A624
	public static void DrawCircleAxis(Vector3 position, Vector3 axisX, Vector3 axisY, int increments)
	{
		Vector2 right = Vector2.right;
		Vector3 from = position + right.x * axisX + right.y * axisY;
		float f = 6.2831855f / (float)increments;
		float num = Mathf.Cos(f);
		float num2 = Mathf.Sin(f);
		for (int i = 0; i < increments; i++)
		{
			right = new Vector2(num * right.x - num2 * right.y, num2 * right.x + num * right.y);
			Vector3 vector = position + right.x * axisX + right.y * axisY;
			Gizmos.DrawLine(from, vector);
			from = vector;
		}
	}

	// Token: 0x0600216E RID: 8558 RVA: 0x0005C2EC File Offset: 0x0005A6EC
	public static void DrawCircle(Vector3 position, float radius, int increments = 8)
	{
		ExtraGizmos.DrawCircleY(position, radius, increments);
	}

	// Token: 0x0600216F RID: 8559 RVA: 0x0005C2F6 File Offset: 0x0005A6F6
	public static void DrawCircleX(Vector3 position, float radius, int increments = 8)
	{
		ExtraGizmos.DrawCircleAxis(position, Vector3.up * radius, Vector3.forward * radius, increments);
	}

	// Token: 0x06002170 RID: 8560 RVA: 0x0005C315 File Offset: 0x0005A715
	public static void DrawCircleY(Vector3 position, float radius, int increments = 8)
	{
		ExtraGizmos.DrawCircleAxis(position, Vector3.right * radius, Vector3.forward * radius, increments);
	}

	// Token: 0x06002171 RID: 8561 RVA: 0x0005C334 File Offset: 0x0005A734
	public static void DrawCircleZ(Vector3 position, float radius, int increments = 8)
	{
		ExtraGizmos.DrawCircleAxis(position, Vector3.right * radius, Vector3.up * radius, increments);
	}

	// Token: 0x06002172 RID: 8562 RVA: 0x0005C354 File Offset: 0x0005A754
	public static void DrawCircle(Vector3 position, float radius, Vector3 normal, int increments = 8)
	{
		Quaternion rotation = Quaternion.LookRotation(normal);
		Vector3 a = rotation * Vector3.right;
		Vector3 a2 = rotation * Vector3.up;
		ExtraGizmos.DrawCircleAxis(position, a * radius, a2 * radius, increments);
	}

	// Token: 0x06002173 RID: 8563 RVA: 0x0005C398 File Offset: 0x0005A798
	public static Matrix4x4 CloserToCameraMatrix()
	{
		Camera current = Camera.current;
		if (current.orthographic)
		{
			return (current.transform.forward * -0.2f).GetMoveMatrix();
		}
		Matrix4x4 moveMatrix = current.transform.position.GetMoveMatrix();
		Matrix4x4 lhs = Matrix4x4.TRS(current.transform.position, Quaternion.identity, Vector3.one * 0.85f);
		return lhs * moveMatrix.inverse;
	}
}
