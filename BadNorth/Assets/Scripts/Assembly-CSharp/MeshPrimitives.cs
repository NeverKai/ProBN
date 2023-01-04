using System;
using UnityEngine;

// Token: 0x020005E9 RID: 1513
public static class MeshPrimitives
{
	// Token: 0x17000548 RID: 1352
	// (get) Token: 0x06002735 RID: 10037 RVA: 0x0007E740 File Offset: 0x0007CB40
	public static Mesh quad
	{
		get
		{
			if (!MeshPrimitives._quad)
			{
				MeshPrimitives._quad = new Mesh();
				MeshPrimitives._quad.vertices = new Vector3[]
				{
					new Vector3(-0.5f, -0.5f),
					new Vector3(0.5f, -0.5f),
					new Vector3(-0.5f, 0.5f),
					new Vector3(0.5f, 0.5f)
				};
				MeshPrimitives._quad.uv = new Vector2[]
				{
					new Vector2(0f, 0f),
					new Vector2(1f, 0f),
					new Vector2(0f, 1f),
					new Vector2(1f, 1f)
				};
				MeshPrimitives._quad.triangles = new int[]
				{
					0,
					1,
					2,
					2,
					1,
					3
				};
				MeshPrimitives._quad.name = "Quad";
				MeshPrimitives._quad.RecalculateBounds();
			}
			return MeshPrimitives._quad;
		}
	}

	// Token: 0x17000549 RID: 1353
	// (get) Token: 0x06002736 RID: 10038 RVA: 0x0007E898 File Offset: 0x0007CC98
	public static Mesh diamond
	{
		get
		{
			if (!MeshPrimitives._diamond)
			{
				MeshPrimitives._diamond = new Mesh();
				MeshPrimitives._diamond.vertices = new Vector3[]
				{
					new Vector3(0f, -0.5f),
					new Vector3(-0.5f, 0f),
					new Vector3(0f, 0.5f),
					new Vector3(0.5f, 0f)
				};
				MeshPrimitives._diamond.uv = new Vector2[]
				{
					new Vector2(0.5f, 0f),
					new Vector2(0f, 0.5f),
					new Vector2(0.5f, 1f),
					new Vector2(1f, 0.5f)
				};
				MeshPrimitives._diamond.triangles = new int[]
				{
					0,
					1,
					2,
					2,
					3,
					0
				};
				MeshPrimitives._diamond.name = "Diamond";
				MeshPrimitives._diamond.RecalculateBounds();
			}
			return MeshPrimitives._diamond;
		}
	}

	// Token: 0x0400191A RID: 6426
	private static Mesh _quad;

	// Token: 0x0400191B RID: 6427
	private static Mesh _diamond;
}
