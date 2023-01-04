using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020006C3 RID: 1731
	public static class LevelCameraExtensions
	{
		// Token: 0x06002CDA RID: 11482 RVA: 0x000A6FF8 File Offset: 0x000A53F8
		public static float GetDisplayRatio(this ILevelCamera cam)
		{
			Camera cameraRef = cam.cameraRef;
			return (float)cameraRef.pixelWidth / (float)cameraRef.pixelHeight;
		}

		// Token: 0x06002CDB RID: 11483 RVA: 0x000A701D File Offset: 0x000A541D
		public static float GetOrthoHeight(this ILevelCamera cam)
		{
			return cam.orthoHeight;
		}

		// Token: 0x06002CDC RID: 11484 RVA: 0x000A7025 File Offset: 0x000A5425
		public static float GetOrthoWidth(this ILevelCamera cam)
		{
			return cam.GetOrthoHeight() * cam.GetDisplayRatio();
		}

		// Token: 0x06002CDD RID: 11485 RVA: 0x000A7034 File Offset: 0x000A5434
		public static void SetOrthoWidth(this ILevelCamera cam, float width)
		{
			cam.orthoHeight = width / cam.GetDisplayRatio();
		}

		// Token: 0x06002CDE RID: 11486 RVA: 0x000A7044 File Offset: 0x000A5444
		public static float GetPitch(this ILevelCamera cam)
		{
			return cam.cameraRef.transform.rotation.eulerAngles.x;
		}

		// Token: 0x06002CDF RID: 11487 RVA: 0x000A7074 File Offset: 0x000A5474
		public static Vector3 GetRotatedVectorHorizontal(this ILevelCamera cam, Vector3 vector)
		{
			Quaternion rotation = Quaternion.LookRotation(cam.cameraRef.transform.forward.GetZeroY());
			return rotation * vector;
		}

		// Token: 0x06002CE0 RID: 11488 RVA: 0x000A70A3 File Offset: 0x000A54A3
		public static void MovePositionBy(this ILevelCamera cam, Vector3 delta)
		{
			cam.position += delta;
		}

		// Token: 0x06002CE1 RID: 11489 RVA: 0x000A70B7 File Offset: 0x000A54B7
		public static void RotateBy(this ILevelCamera cam, float degrees)
		{
			cam.yaw += degrees;
		}
	}
}
