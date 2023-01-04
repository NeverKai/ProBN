using System;
using UnityEngine;

namespace Voxels
{
	// Token: 0x02000880 RID: 2176
	public static class Constants
	{
		// Token: 0x040026EB RID: 9963
		public static Vector3Int[] directions = new Vector3Int[]
		{
			new Vector3Int(0, 0, 1),
			Vector3Int.right,
			Vector3Int.up,
			new Vector3Int(0, 0, -1),
			Vector3Int.left,
			Vector3Int.down
		};

		// Token: 0x040026EC RID: 9964
		public static Quaternion[] rotations = new Quaternion[]
		{
			Quaternion.Euler(0f, 0f, 0f),
			Quaternion.Euler(0f, 90f, 0f),
			Quaternion.Euler(0f, 180f, 0f),
			Quaternion.Euler(0f, 270f, 0f),
			Quaternion.Euler(270f, 0f, 0f),
			Quaternion.Euler(90f, 0f, 0f)
		};

		// Token: 0x040026ED RID: 9965
		public static int[] opposites = new int[]
		{
			3,
			4,
			5,
			0,
			1,
			2
		};

		// Token: 0x040026EE RID: 9966
		public static int[] absolutes = new int[]
		{
			0,
			1,
			2,
			0,
			1,
			2
		};

		// Token: 0x040026EF RID: 9967
		public static int[] components = new int[]
		{
			2,
			0,
			1,
			2,
			0,
			1
		};

		// Token: 0x040026F0 RID: 9968
		public static Vector3[] corners = new Vector3[]
		{
			new Vector3(-1f, -1f, -1f) / 2f,
			new Vector3(1f, -1f, -1f) / 2f,
			new Vector3(-1f, 1f, -1f) / 2f,
			new Vector3(1f, 1f, -1f) / 2f,
			new Vector3(-1f, -1f, 1f) / 2f,
			new Vector3(1f, -1f, 1f) / 2f,
			new Vector3(-1f, 1f, 1f) / 2f,
			new Vector3(1f, 1f, 1f) / 2f
		};

		// Token: 0x040026F1 RID: 9969
		public static Vector3Int[] cornersInt = new Vector3Int[]
		{
			new Vector3Int(0, 0, 0),
			new Vector3Int(1, 0, 0),
			new Vector3Int(0, 1, 0),
			new Vector3Int(1, 1, 0),
			new Vector3Int(0, 0, 1),
			new Vector3Int(1, 0, 1),
			new Vector3Int(0, 1, 1),
			new Vector3Int(1, 1, 1)
		};

		// Token: 0x040026F2 RID: 9970
		public static Vector3[] diagonals = new Vector3[]
		{
			new Vector3(-1f, 0f, -1f),
			new Vector3(1f, 0f, -1f),
			new Vector3(-1f, 0f, 1f),
			new Vector3(1f, 0f, 1f)
		};

		// Token: 0x040026F3 RID: 9971
		public const float waterLevel = -0.105f;

		// Token: 0x040026F4 RID: 9972
		public const float cameraAngle = 30f;

		// Token: 0x040026F5 RID: 9973
		public static float upScale = 1f / Mathf.Sin(1.0471976f);
	}
}
