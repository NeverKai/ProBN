using System;
using UnityEngine;

// Token: 0x020005E6 RID: 1510
public static class Intersections
{
	// Token: 0x0600272A RID: 10026 RVA: 0x0007E0A6 File Offset: 0x0007C4A6
	public static bool OrientedBoxBox(Matrix4x4 box0, Matrix4x4 box1)
	{
		return Intersections.OrientedBoxBox(new Intersections.OBB(box0), new Intersections.OBB(box1));
	}

	// Token: 0x0600272B RID: 10027 RVA: 0x0007E0BC File Offset: 0x0007C4BC
	public static bool OrientedBoxBox(Intersections.OBB obb0, Intersections.OBB obb1)
	{
		Vector3[] array = new Vector3[]
		{
			obb0.forward,
			obb0.right,
			obb0.up,
			obb1.forward,
			obb1.right,
			obb1.up,
			Vector3.Cross(obb0.forward, obb1.forward),
			Vector3.Cross(obb0.forward, obb1.right),
			Vector3.Cross(obb0.forward, obb1.up),
			Vector3.Cross(obb0.right, obb1.forward),
			Vector3.Cross(obb0.right, obb1.right),
			Vector3.Cross(obb0.right, obb1.up),
			Vector3.Cross(obb0.up, obb1.forward),
			Vector3.Cross(obb0.up, obb1.right),
			Vector3.Cross(obb0.up, obb1.up)
		};
		for (int i = 0; i < Intersections.corners.Length; i++)
		{
			Vector3 vector = Intersections.corners[i];
			Vector3 vector2 = obb0.pos;
			vector2 += vector.x * 0.5f * obb0.right;
			vector2 += vector.y * 0.5f * obb0.up;
			vector2 += vector.z * 0.5f * obb0.forward;
			Intersections.corners0[i] = vector2;
		}
		for (int j = 0; j < Intersections.corners.Length; j++)
		{
			Vector3 vector3 = Intersections.corners[j];
			Vector3 vector4 = obb1.pos;
			vector4 += vector3.x * 0.5f * obb1.right;
			vector4 += vector3.y * 0.5f * obb1.up;
			vector4 += vector3.z * 0.5f * obb1.forward;
			Intersections.corners1[j] = vector4;
		}
		for (int k = 0; k < 15; k++)
		{
			Vector3 vector5 = array[k];
			if (!(vector5 == Vector3.zero))
			{
				float num = float.MaxValue;
				float num2 = float.MinValue;
				float num3 = float.MaxValue;
				float num4 = float.MinValue;
				for (int l = 0; l < 8; l++)
				{
					Vector3 lhs = Intersections.corners0[l];
					float b = Vector3.Dot(lhs, vector5);
					num = Mathf.Min(num, b);
					num2 = Mathf.Max(num2, b);
				}
				for (int m = 0; m < 8; m++)
				{
					Vector3 lhs2 = Intersections.corners1[m];
					float b2 = Vector3.Dot(lhs2, vector5);
					num3 = Mathf.Min(num3, b2);
					num4 = Mathf.Max(num4, b2);
				}
				bool flag = num2 < num3 || num4 < num;
				if (flag)
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x0400190F RID: 6415
	private static readonly Vector3[] corners = new Vector3[]
	{
		new Vector3(1f, 1f, 1f),
		new Vector3(1f, 1f, -1f),
		new Vector3(1f, -1f, 1f),
		new Vector3(1f, -1f, -1f),
		new Vector3(-1f, 1f, 1f),
		new Vector3(-1f, 1f, -1f),
		new Vector3(-1f, -1f, 1f),
		new Vector3(-1f, -1f, -1f)
	};

	// Token: 0x04001910 RID: 6416
	private static Vector3[] corners0 = new Vector3[8];

	// Token: 0x04001911 RID: 6417
	private static Vector3[] corners1 = new Vector3[8];

	// Token: 0x020005E7 RID: 1511
	public struct OBB
	{
		// Token: 0x0600272D RID: 10029 RVA: 0x0007E5F0 File Offset: 0x0007C9F0
		public OBB(Matrix4x4 mat)
		{
			this.right = mat.GetColumn(0);
			this.up = mat.GetColumn(1);
			this.forward = mat.GetColumn(2);
			this.pos = mat.GetColumn(3);
		}

		// Token: 0x04001912 RID: 6418
		public readonly Vector3 pos;

		// Token: 0x04001913 RID: 6419
		public readonly Vector3 forward;

		// Token: 0x04001914 RID: 6420
		public readonly Vector3 right;

		// Token: 0x04001915 RID: 6421
		public readonly Vector3 up;
	}
}
