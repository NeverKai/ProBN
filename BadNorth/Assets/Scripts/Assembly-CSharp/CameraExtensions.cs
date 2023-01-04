using System;
using UnityEngine;

// Token: 0x02000505 RID: 1285
public static class CameraExtensions
{
	// Token: 0x060020E7 RID: 8423 RVA: 0x00059DCC File Offset: 0x000581CC
	public static Vector3 GetTopDownInputVector(this Camera camera, Vector2 inputVector)
	{
		Vector3 right = camera.transform.right;
		Vector3 a = new Vector3(-right.z, 0f, right.x);
		return right * inputVector.x + a * inputVector.y;
	}

	// Token: 0x060020E8 RID: 8424 RVA: 0x00059E1F File Offset: 0x0005821F
	public static float GetOrthoHalfHeight(this Camera camera)
	{
		return camera.orthographicSize;
	}

	// Token: 0x060020E9 RID: 8425 RVA: 0x00059E27 File Offset: 0x00058227
	public static float GetOrthoHalfWidth(this Camera camera)
	{
		return camera.GetOrthoHalfHeight() * (float)camera.pixelWidth / (float)camera.pixelHeight;
	}

	// Token: 0x060020EA RID: 8426 RVA: 0x00059E3F File Offset: 0x0005823F
	public static Vector2 GetOrthoHalfSize(this Camera camera)
	{
		return new Vector2(camera.GetOrthoHalfWidth(), camera.GetOrthoHalfHeight());
	}
}
