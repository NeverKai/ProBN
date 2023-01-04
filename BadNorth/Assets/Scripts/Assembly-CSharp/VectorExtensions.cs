using System;
using UnityEngine;

// Token: 0x02000510 RID: 1296
public static class VectorExtensions
{
	// Token: 0x06002162 RID: 8546 RVA: 0x0005C10A File Offset: 0x0005A50A
	public static Vector3 GetZeroX(this Vector3 inVec)
	{
		inVec.x = 0f;
		return inVec;
	}

	// Token: 0x06002163 RID: 8547 RVA: 0x0005C119 File Offset: 0x0005A519
	public static Vector3 GetZeroY(this Vector3 inVec)
	{
		inVec.y = 0f;
		return inVec;
	}

	// Token: 0x06002164 RID: 8548 RVA: 0x0005C128 File Offset: 0x0005A528
	public static Vector3 GetZeroZ(this Vector3 inVec)
	{
		inVec.z = 0f;
		return inVec;
	}

	// Token: 0x06002165 RID: 8549 RVA: 0x0005C137 File Offset: 0x0005A537
	public static Vector2 GetXZ(this Vector3 inVec)
	{
		return new Vector2(inVec.x, inVec.z);
	}

	// Token: 0x06002166 RID: 8550 RVA: 0x0005C14C File Offset: 0x0005A54C
	public static Vector3 GetXZtoXYZ(this Vector2 inVec)
	{
		return new Vector3(inVec.x, 0f, inVec.y);
	}

	// Token: 0x06002167 RID: 8551 RVA: 0x0005C166 File Offset: 0x0005A566
	public static float GetHorizontalMagnitudeSq(this Vector3 inVec)
	{
		return inVec.x * inVec.x + inVec.z * inVec.z;
	}

	// Token: 0x06002168 RID: 8552 RVA: 0x0005C187 File Offset: 0x0005A587
	public static float GetHorizontalMagnitude(this Vector3 inVec)
	{
		return Mathf.Sqrt(inVec.GetHorizontalMagnitudeSq());
	}

	// Token: 0x06002169 RID: 8553 RVA: 0x0005C194 File Offset: 0x0005A594
	public static Vector3 GetPlaneRejection(this Vector3 vector, Vector3 planeNormal)
	{
		return vector - Vector3.ProjectOnPlane(vector, planeNormal);
	}

	// Token: 0x0600216A RID: 8554 RVA: 0x0005C1A4 File Offset: 0x0005A5A4
	public static Vector3 GetScaledOnNormal(this Vector3 vector, Vector3 normal, float scale)
	{
		Vector3 vector2 = Vector3.ProjectOnPlane(vector, normal);
		Vector3 a = vector - vector2;
		return vector2 + a * scale;
	}

	// Token: 0x0600216B RID: 8555 RVA: 0x0005C1CE File Offset: 0x0005A5CE
	public static bool HasNaN(this Vector3 vector)
	{
		return float.IsNaN(vector.x) || float.IsNaN(vector.y) || float.IsNaN(vector.z);
	}

	// Token: 0x0600216C RID: 8556 RVA: 0x0005C201 File Offset: 0x0005A601
	public static bool HasNaN(this Vector2 vector)
	{
		return float.IsNaN(vector.x) || float.IsNaN(vector.y);
	}
}
