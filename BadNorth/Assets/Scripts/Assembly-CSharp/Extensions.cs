using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000507 RID: 1287
public static class Extensions
{
	// Token: 0x060020EC RID: 8428 RVA: 0x00059E73 File Offset: 0x00058273
	public static Vector3 Floored(this Vector3 vector)
	{
		vector.x = Mathf.Floor(vector.x);
		vector.y = Mathf.Floor(vector.y);
		vector.z = Mathf.Floor(vector.z);
		return vector;
	}

	// Token: 0x060020ED RID: 8429 RVA: 0x00059EAF File Offset: 0x000582AF
	public static Vector3 Rounded(this Vector3 vector)
	{
		vector.x = Mathf.Round(vector.x);
		vector.y = Mathf.Round(vector.y);
		vector.z = Mathf.Round(vector.z);
		return vector;
	}

	// Token: 0x060020EE RID: 8430 RVA: 0x00059EEB File Offset: 0x000582EB
	public static Vector3 Ceiled(this Vector3 vector)
	{
		vector.x = Mathf.Ceil(vector.x);
		vector.y = Mathf.Ceil(vector.y);
		vector.z = Mathf.Ceil(vector.z);
		return vector;
	}

	// Token: 0x060020EF RID: 8431 RVA: 0x00059F28 File Offset: 0x00058328
	public static void Invert(this Mesh mesh)
	{
		List<int> list = ListPool<int>.GetList(mesh.vertexCount * 2);
		mesh.GetTriangles(list, 0);
		for (int i = 0; i < list.Count; i += 3)
		{
			int value = list[i];
			int value2 = list[i + 1];
			list[i] = value2;
			list[i + 1] = value;
		}
		mesh.SetTriangles(list, 0);
		list.ReturnToListPool<int>();
	}

	// Token: 0x060020F0 RID: 8432 RVA: 0x00059F94 File Offset: 0x00058394
	public static void SoftNormalsInTangents(this Mesh mesh)
	{
		IEnumerator enumerator = mesh.SoftNormalsInTangetsEnumerator();
		while (enumerator.MoveNext())
		{
		}
	}

	// Token: 0x060020F1 RID: 8433 RVA: 0x00059FB8 File Offset: 0x000583B8
	public static IEnumerator SoftNormalsInTangetsEnumerator(this Mesh mesh)
	{
		List<Vector3> mVerts = ListPool<Vector3>.GetList(mesh.vertexCount);
		List<Vector4> mTangents = ListPool<Vector4>.GetList(mesh.vertexCount);
		List<int> mTris = ListPool<int>.GetList((int)mesh.GetIndexCount(0));
		mesh.GetVertices(mVerts);
		mesh.GetTriangles(mTris, 0);
		Extensions.vertDict.Clear();
		for (int j = mTangents.Count; j < mVerts.Count; j++)
		{
			mTangents.Add(Vector4.zero);
		}
		for (int i = 0; i < mTris.Count; i += 3)
		{
			int i2 = mTris[i];
			int i3 = mTris[i + 1];
			int i4 = mTris[i + 2];
			Vector3 v0 = mVerts[i2];
			Vector3 v = mVerts[i3];
			Vector3 v2 = mVerts[i4];
			Vector3 dir = v - v0;
			Vector3 tan = v2 - v0;
			if (!(dir == Vector3.zero))
			{
				if (!(tan == Vector3.zero))
				{
					Vector3 normal = Vector3.Cross(tan, dir).normalized;
					float dirLength = dir.magnitude;
					float height = Vector3.Dot(tan, Vector3.Cross(dir / dirLength, normal));
					float area = dirLength * height / 2f;
					normal *= area;
					Vector4 tangent = normal.SetW(0f);
					for (int k = 0; k < 3; k++)
					{
						int num = mTris[i + k];
						Vector3 key = mVerts[num];
						Extensions.SoftNormal softNormal2;
						if (!Extensions.vertDict.TryGetValue(mVerts[mTris[i + k]], out softNormal2))
						{
							softNormal2 = new Extensions.SoftNormal();
							Extensions.vertDict.Add(key, softNormal2);
						}
						if (!softNormal2.indexes.Contains(num))
						{
							softNormal2.indexes.Add(num);
						}
						softNormal2.normal += tangent;
					}
					yield return null;
				}
			}
		}
		Dictionary<Vector3, Extensions.SoftNormal>.ValueCollection softNormals = Extensions.vertDict.Values;
		foreach (Extensions.SoftNormal softNormal in softNormals)
		{
			for (int l = 0; l < softNormal.indexes.Count; l++)
			{
				List<Vector4> list;
				int index;
				(list = mTangents)[index = softNormal.indexes[l]] = list[index] + softNormal.normal;
			}
			yield return null;
		}
		for (int m = 0; m < mTangents.Count; m++)
		{
			mTangents[m] = -mTangents[m].normalized;
		}
		yield return null;
		mesh.SetTangents(mTangents);
		mVerts.ReturnToListPool<Vector3>();
		mTangents.ReturnToListPool<Vector4>();
		mTris.ReturnToListPool<int>();
		Extensions.vertDict.Clear();
		yield break;
	}

	// Token: 0x060020F2 RID: 8434 RVA: 0x00059FD3 File Offset: 0x000583D3
	public static Bounds GetClamped(this Bounds bounds, Bounds clampBounds)
	{
		bounds.SetMinMax(Vector3.Max(bounds.min, clampBounds.min), Vector3.Min(bounds.max, clampBounds.max));
		return bounds;
	}

	// Token: 0x060020F3 RID: 8435 RVA: 0x0005A003 File Offset: 0x00058403
	public static bool Contains(this Bounds bounds, Bounds otherBounds)
	{
		return bounds.Contains(otherBounds.min) && bounds.Contains(otherBounds.max);
	}

	// Token: 0x060020F4 RID: 8436 RVA: 0x0005A029 File Offset: 0x00058429
	public static Matrix4x4 GetMoveMatrix(this Vector3 vector)
	{
		return Matrix4x4.TRS(vector, Quaternion.identity, Vector3.one);
	}

	// Token: 0x060020F5 RID: 8437 RVA: 0x0005A03B File Offset: 0x0005843B
	public static Matrix4x4 GetMoved(this Matrix4x4 matrix, Vector3 vector)
	{
		return matrix * Matrix4x4.TRS(vector, Quaternion.identity, Vector3.one);
	}

	// Token: 0x060020F6 RID: 8438 RVA: 0x0005A054 File Offset: 0x00058454
	public static Vector3 GetClamped(this Vector3 vector, Bounds bounds)
	{
		vector.x = Mathf.Clamp(vector.x, bounds.min.x, bounds.max.x);
		vector.y = Mathf.Clamp(vector.y, bounds.min.y, bounds.max.y);
		vector.z = Mathf.Clamp(vector.z, bounds.min.z, bounds.max.z);
		return vector;
	}

	// Token: 0x060020F7 RID: 8439 RVA: 0x0005A0F8 File Offset: 0x000584F8
	public static Vector2 GetClampedMagnitude(this Vector2 vector, float max)
	{
		float sqrMagnitude = vector.sqrMagnitude;
		if (sqrMagnitude > max * max)
		{
			vector = vector.normalized * max;
		}
		return vector;
	}

	// Token: 0x060020F8 RID: 8440 RVA: 0x0005A128 File Offset: 0x00058528
	public static Vector3 GetClampedMagnitude(this Vector3 vector, float max)
	{
		float sqrMagnitude = vector.sqrMagnitude;
		if (sqrMagnitude > max * max)
		{
			vector = vector.normalized * max;
		}
		return vector;
	}

	// Token: 0x060020F9 RID: 8441 RVA: 0x0005A158 File Offset: 0x00058558
	public static Vector3 GetMinMagnitude(this Vector3 vector, float min)
	{
		float sqrMagnitude = vector.sqrMagnitude;
		if (sqrMagnitude < min * min)
		{
			vector = vector.normalized * min;
		}
		return vector;
	}

	// Token: 0x060020FA RID: 8442 RVA: 0x0005A188 File Offset: 0x00058588
	public static bool IsRound(this Vector3 vector)
	{
		return vector.x % 1f == 0f && vector.y % 1f == 0f && vector.z % 1f == 0f;
	}

	// Token: 0x060020FB RID: 8443 RVA: 0x0005A1DA File Offset: 0x000585DA
	public static Color GetTransparent(this Color color, float alpha)
	{
		return color * new Color(1f, 1f, 1f, alpha);
	}

	// Token: 0x060020FC RID: 8444 RVA: 0x0005A1F7 File Offset: 0x000585F7
	public static Matrix4x4 GetMatrix(this Bounds bounds)
	{
		return Matrix4x4.TRS(bounds.center, Quaternion.identity, bounds.extents * 2f);
	}

	// Token: 0x060020FD RID: 8445 RVA: 0x0005A21C File Offset: 0x0005861C
	public static Vector4 GetAbs(this Vector4 vector)
	{
		vector.x = Mathf.Abs(vector.x);
		vector.y = Mathf.Abs(vector.y);
		vector.z = Mathf.Abs(vector.z);
		vector.w = Mathf.Abs(vector.w);
		return vector;
	}

	// Token: 0x060020FE RID: 8446 RVA: 0x0005A276 File Offset: 0x00058676
	public static float GetVolume(this Vector3 vector)
	{
		return vector.x * vector.y * vector.z;
	}

	// Token: 0x060020FF RID: 8447 RVA: 0x0005A28F File Offset: 0x0005868F
	public static Color SetR(this Color color, float value)
	{
		color.r = value;
		return color;
	}

	// Token: 0x06002100 RID: 8448 RVA: 0x0005A29A File Offset: 0x0005869A
	public static Color SetG(this Color color, float value)
	{
		color.g = value;
		return color;
	}

	// Token: 0x06002101 RID: 8449 RVA: 0x0005A2A5 File Offset: 0x000586A5
	public static Color SetB(this Color color, float value)
	{
		color.b = value;
		return color;
	}

	// Token: 0x06002102 RID: 8450 RVA: 0x0005A2B0 File Offset: 0x000586B0
	public static Color SetA(this Color color, float value)
	{
		color.a = value;
		return color;
	}

	// Token: 0x06002103 RID: 8451 RVA: 0x0005A2BC File Offset: 0x000586BC
	public static Color SetComponent(this Color color, int index, float value)
	{
		switch (index)
		{
		case 0:
			color.r = value;
			break;
		case 1:
			color.g = value;
			break;
		case 2:
			color.b = value;
			break;
		case 3:
			color.a = value;
			break;
		}
		return color;
	}

	// Token: 0x06002104 RID: 8452 RVA: 0x0005A31C File Offset: 0x0005871C
	public static float GetComponent(this Color color, int index)
	{
		switch (index)
		{
		case 0:
			return color.r;
		case 1:
			return color.g;
		case 2:
			return color.b;
		case 3:
			return color.a;
		default:
			return 0f;
		}
	}

	// Token: 0x06002105 RID: 8453 RVA: 0x0005A36C File Offset: 0x0005876C
	public static float GetComponent(this Vector4 vector, int index)
	{
		switch (index)
		{
		case 0:
			return vector.x;
		case 1:
			return vector.y;
		case 2:
			return vector.z;
		case 3:
			return vector.w;
		default:
			return 0f;
		}
	}

	// Token: 0x06002106 RID: 8454 RVA: 0x0005A3B9 File Offset: 0x000587B9
	public static float GetComponent(this Vector3 vector, int index)
	{
		switch (index)
		{
		case 0:
			return vector.x;
		case 1:
			return vector.y;
		case 2:
			return vector.z;
		default:
			return 0f;
		}
	}

	// Token: 0x06002107 RID: 8455 RVA: 0x0005A3EF File Offset: 0x000587EF
	public static float GetComponent(this Vector2 vector, int index)
	{
		if (index == 0)
		{
			return vector.x;
		}
		if (index != 1)
		{
			return 0f;
		}
		return vector.y;
	}

	// Token: 0x06002108 RID: 8456 RVA: 0x0005A418 File Offset: 0x00058818
	public static Vector4 SetComponent(this Vector4 vector, int index, float value)
	{
		switch (index)
		{
		case 0:
			vector.x = value;
			break;
		case 1:
			vector.y = value;
			break;
		case 2:
			vector.z = value;
			break;
		case 3:
			vector.w = value;
			break;
		}
		return vector;
	}

	// Token: 0x06002109 RID: 8457 RVA: 0x0005A475 File Offset: 0x00058875
	public static Vector3 SetZ(this Vector2 vector, float value)
	{
		return new Vector3(vector.x, vector.y, value);
	}

	// Token: 0x0600210A RID: 8458 RVA: 0x0005A48B File Offset: 0x0005888B
	public static Vector2 SetX(this Vector2 vector, float value)
	{
		vector.x = value;
		return vector;
	}

	// Token: 0x0600210B RID: 8459 RVA: 0x0005A496 File Offset: 0x00058896
	public static Vector2 SetY(this Vector2 vector, float value)
	{
		vector.y = value;
		return vector;
	}

	// Token: 0x0600210C RID: 8460 RVA: 0x0005A4A1 File Offset: 0x000588A1
	public static Vector3 SetX(this Vector3 vector, float value)
	{
		vector.x = value;
		return vector;
	}

	// Token: 0x0600210D RID: 8461 RVA: 0x0005A4AC File Offset: 0x000588AC
	public static Vector3 SetY(this Vector3 vector, float value)
	{
		vector.y = value;
		return vector;
	}

	// Token: 0x0600210E RID: 8462 RVA: 0x0005A4B7 File Offset: 0x000588B7
	public static Vector3 SetZ(this Vector3 vector, float value)
	{
		vector.z = value;
		return vector;
	}

	// Token: 0x0600210F RID: 8463 RVA: 0x0005A4C2 File Offset: 0x000588C2
	public static Vector4 SetW(this Vector3 vector, float value)
	{
		return new Vector4(vector.x, vector.y, vector.z, value);
	}

	// Token: 0x06002110 RID: 8464 RVA: 0x0005A4E0 File Offset: 0x000588E0
	public static Vector3 SetComponent(this Vector3 vector, int index, float value)
	{
		if (index != 0)
		{
			if (index != 1)
			{
				if (index == 2)
				{
					vector.z = value;
				}
			}
			else
			{
				vector.y = value;
			}
		}
		else
		{
			vector.x = value;
		}
		return vector;
	}

	// Token: 0x06002111 RID: 8465 RVA: 0x0005A530 File Offset: 0x00058930
	public static Vector3 GetFlatDir(this Camera camera)
	{
		Vector3 forward = camera.transform.forward;
		forward.y = 0f;
		return forward;
	}

	// Token: 0x06002112 RID: 8466 RVA: 0x0005A556 File Offset: 0x00058956
	public static void SetEnabled(this MonoBehaviour mono, bool enabled)
	{
		mono.enabled = enabled;
	}

	// Token: 0x06002113 RID: 8467 RVA: 0x0005A560 File Offset: 0x00058960
	public static T GetRandomByProbability<T>(this IEnumerable<T> enumerable, Func<T, float> probability, float nullProbability = 0f)
	{
		float num = (enumerable.Sum((T t) => Mathf.Max(0f, probability(t))) + nullProbability) * UnityEngine.Random.value;
		foreach (T t2 in enumerable)
		{
			num -= Mathf.Max(0f, probability(t2));
			if (num <= 0f)
			{
				return t2;
			}
		}
		return default(T);
	}

	// Token: 0x06002114 RID: 8468 RVA: 0x0005A610 File Offset: 0x00058A10
	public static V GetOrAdd<K, V>(this Dictionary<K, V> dictionary, K key, Func<V> newValue)
	{
		V v;
		if (!dictionary.TryGetValue(key, out v))
		{
			v = newValue();
			dictionary.Add(key, v);
		}
		return v;
	}

	// Token: 0x06002115 RID: 8469 RVA: 0x0005A63C File Offset: 0x00058A3C
	public static Mesh GetMesh(this Sprite sprite)
	{
		Mesh mesh = new Mesh();
		mesh.MarkDynamic();
		Vector2[] vertices = sprite.vertices;
		Vector3[] array = new Vector3[vertices.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = vertices[i];
		}
		mesh.vertices = array;
		ushort[] triangles = sprite.triangles;
		int[] array2 = new int[triangles.Length];
		for (int j = 0; j < array2.Length; j++)
		{
			array2[j] = (int)triangles[j];
		}
		mesh.triangles = array2;
		mesh.uv = sprite.uv;
		return mesh;
	}

	// Token: 0x0400147D RID: 5245
	private static Dictionary<Vector3, Extensions.SoftNormal> vertDict = new Dictionary<Vector3, Extensions.SoftNormal>();

	// Token: 0x02000508 RID: 1288
	private class SoftNormal
	{
		// Token: 0x0400147E RID: 5246
		public List<int> indexes = new List<int>();

		// Token: 0x0400147F RID: 5247
		public Vector4 normal;
	}
}
