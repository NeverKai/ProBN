using System;
using UnityEngine;

// Token: 0x02000512 RID: 1298
public static class ExtraMath
{
	// Token: 0x06002174 RID: 8564 RVA: 0x0005C414 File Offset: 0x0005A814
	public static bool IsNaN(Vector3 vector)
	{
		return float.IsNaN(vector.x) || float.IsNaN(vector.y) || float.IsNaN(vector.z);
	}

	// Token: 0x06002175 RID: 8565 RVA: 0x0005C448 File Offset: 0x0005A848
	public static Vector2 Rotate2D(Vector2 vector, float degrees)
	{
		float f = degrees * 0.017453292f;
		float num = Mathf.Cos(f);
		float num2 = Mathf.Sin(f);
		return new Vector2(num * vector.x - num2 * vector.y, num2 * vector.x + num * vector.y);
	}

	// Token: 0x06002176 RID: 8566 RVA: 0x0005C496 File Offset: 0x0005A896
	public static Vector2 Rotate2D90(Vector2 vector)
	{
		return new Vector2(vector.y, -vector.x);
	}

	// Token: 0x06002177 RID: 8567 RVA: 0x0005C4AC File Offset: 0x0005A8AC
	public static Rect Encapsulate(Rect rect0, Rect rect1)
	{
		Vector2 vector = Vector2.Min(rect0.min, rect1.min);
		Vector2 a = Vector2.Max(rect0.max, rect1.max);
		return new Rect(vector, a - vector);
	}

	// Token: 0x06002178 RID: 8568 RVA: 0x0005C4EE File Offset: 0x0005A8EE
	public static float Screen(float a, float b)
	{
		return 1f - (1f - a) * (1f - b);
	}

	// Token: 0x06002179 RID: 8569 RVA: 0x0005C505 File Offset: 0x0005A905
	public static Vector2 Screen(Vector2 a, Vector2 b)
	{
		return Vector2.one - Vector2.Scale(Vector2.one - a, Vector2.one - b);
	}

	// Token: 0x0600217A RID: 8570 RVA: 0x0005C52C File Offset: 0x0005A92C
	public static Vector3 Screen(Vector3 a, Vector3 b)
	{
		return Vector3.one - Vector3.Scale(Vector3.one - a, Vector3.one - b);
	}

	// Token: 0x0600217B RID: 8571 RVA: 0x0005C553 File Offset: 0x0005A953
	public static Vector4 Screen(Vector4 a, Vector4 b)
	{
		return Vector4.one - Vector4.Scale(Vector4.one - a, Vector4.one - b);
	}

	// Token: 0x0600217C RID: 8572 RVA: 0x0005C57C File Offset: 0x0005A97C
	public static bool Between(Vector3 min, Vector3 max, Vector3 value)
	{
		return value.x >= min.x && value.y >= min.y && value.z >= min.z && value.x <= max.x && value.y <= max.y && value.z <= max.z;
	}

	// Token: 0x0600217D RID: 8573 RVA: 0x0005C600 File Offset: 0x0005AA00
	public static bool Less(Vector3 value, Vector3 max)
	{
		return value.x < max.x && value.y < max.y && value.z < max.z;
	}

	// Token: 0x0600217E RID: 8574 RVA: 0x0005C63B File Offset: 0x0005AA3B
	public static Vector2 Floor(Vector2 vector)
	{
		vector.x = Mathf.Floor(vector.x);
		vector.y = Mathf.Floor(vector.y);
		return vector;
	}

	// Token: 0x0600217F RID: 8575 RVA: 0x0005C664 File Offset: 0x0005AA64
	public static Vector3 Floor(Vector3 vector)
	{
		vector.x = Mathf.Floor(vector.x);
		vector.y = Mathf.Floor(vector.y);
		vector.z = Mathf.Floor(vector.z);
		return vector;
	}

	// Token: 0x06002180 RID: 8576 RVA: 0x0005C6A0 File Offset: 0x0005AAA0
	public static Vector2Int FloorToInt(Vector2 vector)
	{
		return new Vector2Int(Mathf.FloorToInt(vector.x), Mathf.FloorToInt(vector.y));
	}

	// Token: 0x06002181 RID: 8577 RVA: 0x0005C6BF File Offset: 0x0005AABF
	public static Vector3Int FloorToInt(Vector3 vector)
	{
		return new Vector3Int(Mathf.FloorToInt(vector.x), Mathf.FloorToInt(vector.y), Mathf.FloorToInt(vector.z));
	}

	// Token: 0x06002182 RID: 8578 RVA: 0x0005C6EC File Offset: 0x0005AAEC
	public static Vector4 Divide(Vector4 vector, Vector4 denom)
	{
		vector.x /= denom.x;
		vector.y /= denom.y;
		vector.z /= denom.z;
		vector.w /= denom.w;
		return vector;
	}

	// Token: 0x06002183 RID: 8579 RVA: 0x0005C750 File Offset: 0x0005AB50
	public static Vector3 Divide(Vector3 vector, Vector3 denom)
	{
		vector.x /= denom.x;
		vector.y /= denom.y;
		vector.z /= denom.z;
		return vector;
	}

	// Token: 0x06002184 RID: 8580 RVA: 0x0005C79D File Offset: 0x0005AB9D
	public static Vector2 Divide(Vector2 vector, Vector2 denom)
	{
		vector.x /= denom.x;
		vector.y /= denom.y;
		return vector;
	}

	// Token: 0x06002185 RID: 8581 RVA: 0x0005C7CA File Offset: 0x0005ABCA
	public static Vector2 Round(Vector2 vector)
	{
		vector.x = Mathf.Round(vector.x);
		vector.y = Mathf.Round(vector.y);
		return vector;
	}

	// Token: 0x06002186 RID: 8582 RVA: 0x0005C7F3 File Offset: 0x0005ABF3
	public static Vector3 Round(Vector3 vector)
	{
		vector.x = Mathf.Round(vector.x);
		vector.y = Mathf.Round(vector.y);
		vector.z = Mathf.Round(vector.z);
		return vector;
	}

	// Token: 0x06002187 RID: 8583 RVA: 0x0005C82F File Offset: 0x0005AC2F
	public static Vector2Int RoundToInt(Vector2 vector)
	{
		return new Vector2Int(Mathf.RoundToInt(vector.x), Mathf.RoundToInt(vector.y));
	}

	// Token: 0x06002188 RID: 8584 RVA: 0x0005C84E File Offset: 0x0005AC4E
	public static Vector3Int RoundToInt(Vector3 vector)
	{
		return new Vector3Int(Mathf.RoundToInt(vector.x), Mathf.RoundToInt(vector.y), Mathf.RoundToInt(vector.z));
	}

	// Token: 0x06002189 RID: 8585 RVA: 0x0005C879 File Offset: 0x0005AC79
	public static Vector2 Ceil(Vector2 vector)
	{
		vector.x = Mathf.Ceil(vector.x);
		vector.y = Mathf.Ceil(vector.y);
		return vector;
	}

	// Token: 0x0600218A RID: 8586 RVA: 0x0005C8A2 File Offset: 0x0005ACA2
	public static Vector3 Ceil(Vector3 vector)
	{
		vector.x = Mathf.Ceil(vector.x);
		vector.y = Mathf.Ceil(vector.y);
		vector.z = Mathf.Ceil(vector.z);
		return vector;
	}

	// Token: 0x0600218B RID: 8587 RVA: 0x0005C8DE File Offset: 0x0005ACDE
	public static Vector2 Abs(Vector2 vector)
	{
		vector.x = Mathf.Abs(vector.x);
		vector.y = Mathf.Abs(vector.y);
		return vector;
	}

	// Token: 0x0600218C RID: 8588 RVA: 0x0005C907 File Offset: 0x0005AD07
	public static Vector3 Abs(Vector3 vector)
	{
		vector.x = Mathf.Abs(vector.x);
		vector.y = Mathf.Abs(vector.y);
		vector.z = Mathf.Abs(vector.z);
		return vector;
	}

	// Token: 0x0600218D RID: 8589 RVA: 0x0005C943 File Offset: 0x0005AD43
	public static Vector2 Sign(Vector2 vector)
	{
		vector.x = Mathf.Sign(vector.x);
		vector.y = Mathf.Sign(vector.y);
		return vector;
	}

	// Token: 0x0600218E RID: 8590 RVA: 0x0005C96C File Offset: 0x0005AD6C
	public static int GetManhattanMagnitude(this Vector3Int vector)
	{
		return Mathf.Max(new int[]
		{
			Mathf.Abs(vector.x),
			Mathf.Abs(vector.y),
			Mathf.Abs(vector.z)
		});
	}

	// Token: 0x0600218F RID: 8591 RVA: 0x0005C9A6 File Offset: 0x0005ADA6
	public static float GetManhattanMagnitude(this Vector3 vector)
	{
		return Mathf.Max(new float[]
		{
			Mathf.Abs(vector.x),
			Mathf.Abs(vector.y),
			Mathf.Abs(vector.z)
		});
	}

	// Token: 0x06002190 RID: 8592 RVA: 0x0005C9E0 File Offset: 0x0005ADE0
	public static float GetManhattanMagnitude(this Vector2 vector)
	{
		return Mathf.Max(Mathf.Abs(vector.x), Mathf.Abs(vector.y));
	}

	// Token: 0x06002191 RID: 8593 RVA: 0x0005C9FF File Offset: 0x0005ADFF
	public static float GetArea(this Vector2 vector)
	{
		return vector.x * vector.y;
	}

	// Token: 0x06002192 RID: 8594 RVA: 0x0005CA10 File Offset: 0x0005AE10
	public static bool CloseEnough(Vector3 a, Vector3 b, float threshold = 0.01f)
	{
		return Vector3.SqrMagnitude(a - b) < threshold * threshold;
	}

	// Token: 0x06002193 RID: 8595 RVA: 0x0005CA24 File Offset: 0x0005AE24
	public static bool GetIsFlipped(this Vector3 scale)
	{
		bool flag = false;
		if (scale.x < 0f)
		{
			flag = !flag;
		}
		if (scale.y < 0f)
		{
			flag = !flag;
		}
		if (scale.z < 0f)
		{
			flag = !flag;
		}
		return flag;
	}

	// Token: 0x06002194 RID: 8596 RVA: 0x0005CA78 File Offset: 0x0005AE78
	public static Vector2 Bezier(Vector2 p0, Vector2 p1, Vector2 p2, float t)
	{
		float num = 1f - t;
		return num * num * p0 + 2f * num * t * p1 + t * t * p2;
	}

	// Token: 0x06002195 RID: 8597 RVA: 0x0005CAB8 File Offset: 0x0005AEB8
	public static Vector3 Bezier(Vector3 p0, Vector3 p1, Vector3 p2, float t)
	{
		float num = 1f - t;
		return num * num * p0 + 2f * num * t * p1 + t * t * p2;
	}

	// Token: 0x06002196 RID: 8598 RVA: 0x0005CAF8 File Offset: 0x0005AEF8
	public static Vector2 BezierDerivate(Vector2 p0, Vector2 p1, Vector2 p2, float t)
	{
		float num = 1f - t;
		return 2f * num * (p1 - p0) + 2f * t * (p2 - p1);
	}

	// Token: 0x06002197 RID: 8599 RVA: 0x0005CB38 File Offset: 0x0005AF38
	public static Vector3 BezierDerivate(Vector3 p0, Vector3 p1, Vector3 p2, float t)
	{
		float num = 1f - t;
		return 2f * num * (p1 - p0) + 2f * t * (p2 - p1);
	}

	// Token: 0x06002198 RID: 8600 RVA: 0x0005CB78 File Offset: 0x0005AF78
	public static Vector2 Bezier(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, float t)
	{
		float d = 1f - t;
		return p0 * d * d * d + p1 * 3f * d * d * t + p2 * 3f * d * t * t + p3 * t * t * t;
	}

	// Token: 0x06002199 RID: 8601 RVA: 0x0005CC04 File Offset: 0x0005B004
	public static Vector3 Bezier(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
	{
		float d = 1f - t;
		return p0 * d * d * d + p1 * 3f * d * d * t + p2 * 3f * d * t * t + p3 * t * t * t;
	}

	// Token: 0x0600219A RID: 8602 RVA: 0x0005CC90 File Offset: 0x0005B090
	public static Vector2 BezierDerivate(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, float t)
	{
		float num = 1f - t;
		return 3f * num * num * (p1 - p0) + 6f * num * t * (p2 - p1) + 3f * t * t * (p3 - p2);
	}

	// Token: 0x0600219B RID: 8603 RVA: 0x0005CCF4 File Offset: 0x0005B0F4
	public static Vector3 BezierDerivate(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
	{
		float num = 1f - t;
		return 3f * num * num * (p1 - p0) + 6f * num * t * (p2 - p1) + 3f * t * t * (p3 - p2);
	}

	// Token: 0x0600219C RID: 8604 RVA: 0x0005CD58 File Offset: 0x0005B158
	public static Vector3 GetBarycentric(Vector3 point, Vector3 a, Vector3 b, Vector3 c)
	{
		Vector3 vector = b - a;
		Vector3 vector2 = c - a;
		Vector3 lhs = point - a;
		float num = Vector3.Dot(vector, vector);
		float num2 = Vector3.Dot(vector, vector2);
		float num3 = Vector3.Dot(vector2, vector2);
		float num4 = Vector3.Dot(lhs, vector);
		float num5 = Vector3.Dot(lhs, vector2);
		float num6 = num * num3 - num2 * num2;
		Vector3 vector3;
		vector3.y = (num3 * num4 - num2 * num5) / num6;
		vector3.z = (num * num5 - num2 * num4) / num6;
		vector3.x = 1f - vector3.y - vector3.z;
		vector3.x = Mathf.Clamp01(vector3.x);
		vector3.y = Mathf.Clamp01(vector3.y);
		vector3.z = Mathf.Clamp01(vector3.z);
		vector3 /= vector3.x + vector3.y + vector3.z;
		return vector3;
	}

	// Token: 0x0600219D RID: 8605 RVA: 0x0005CE58 File Offset: 0x0005B258
	public static Vector3 ClosestPointOnTriangle(Vector3 point, Vector3 a, Vector3 b, Vector3 c)
	{
		Vector3 barycentric = ExtraMath.GetBarycentric(point, a, b, c);
		return a * barycentric.x + b * barycentric.y + c * barycentric.z;
	}

	// Token: 0x0600219E RID: 8606 RVA: 0x0005CEA0 File Offset: 0x0005B2A0
	public static Vector3 ClosestPointOnLineSegment(Vector3 start, Vector3 end, Vector3 point)
	{
		if (start == end)
		{
			return start;
		}
		Vector3 a = end - start;
		float magnitude = a.magnitude;
		Vector3 vector = a / magnitude;
		Vector3 lhs = point - start;
		float d = Mathf.Clamp(Vector3.Dot(lhs, vector), 0f, magnitude);
		return start + vector * d;
	}

	// Token: 0x0600219F RID: 8607 RVA: 0x0005CF00 File Offset: 0x0005B300
	public static float ClosestFractionOnLineSegment(Vector2 start, Vector2 end, Vector2 point)
	{
		if (start == end)
		{
			return 0f;
		}
		Vector2 a = end - start;
		float magnitude = a.magnitude;
		Vector2 rhs = a / magnitude;
		Vector2 lhs = point - start;
		return Mathf.Clamp01(Vector2.Dot(lhs, rhs) / magnitude);
	}

	// Token: 0x060021A0 RID: 8608 RVA: 0x0005CF50 File Offset: 0x0005B350
	public static Vector2 ClosestPointOnLineSegment(Vector2 start, Vector2 end, Vector2 point)
	{
		if (start == end)
		{
			return start;
		}
		Vector2 a = end - start;
		float magnitude = a.magnitude;
		Vector2 vector = a / magnitude;
		Vector2 lhs = point - start;
		float d = Mathf.Clamp(Vector2.Dot(lhs, vector), 0f, magnitude);
		return start + vector * d;
	}

	// Token: 0x060021A1 RID: 8609 RVA: 0x0005CFAD File Offset: 0x0005B3AD
	public static int CoordinateToIndex(Vector3Int coordinate, Vector3Int size)
	{
		return coordinate.x + coordinate.y * size.x + coordinate.z * size.x * size.y;
	}

	// Token: 0x060021A2 RID: 8610 RVA: 0x0005CFDE File Offset: 0x0005B3DE
	public static int CoordinateToIndex(Vector3 coordinate, Vector3 size)
	{
		return (int)coordinate.x + (int)coordinate.y * (int)size.x + (int)coordinate.z * (int)size.x * (int)size.y;
	}

	// Token: 0x060021A3 RID: 8611 RVA: 0x0005D015 File Offset: 0x0005B415
	public static Vector3Int IndexToCoordinate(int index, Vector3 size)
	{
		return ExtraMath.IndexToCoordinate(index, ExtraMath.FloorToInt(size));
	}

	// Token: 0x060021A4 RID: 8612 RVA: 0x0005D024 File Offset: 0x0005B424
	public static Vector3Int IndexToCoordinate(int index, Vector3Int size)
	{
		return new Vector3Int
		{
			x = index % size.x,
			y = index / size.x % size.y,
			z = index / (size.x * size.y)
		};
	}

	// Token: 0x060021A5 RID: 8613 RVA: 0x0005D07C File Offset: 0x0005B47C
	public static void TestCoordinateConversions()
	{
		int max = 100;
		for (int i = 0; i < 100; i++)
		{
			Vector3 vector = ExtraMath.Round(new Vector3((float)UnityEngine.Random.Range(1, max), (float)UnityEngine.Random.Range(1, max), (float)UnityEngine.Random.Range(1, max)));
			Vector3 vector2 = ExtraMath.Floor(new Vector3(UnityEngine.Random.Range(0f, vector.x), UnityEngine.Random.Range(0f, vector.y), UnityEngine.Random.Range(0f, vector.z)));
			if (vector2 != ExtraMath.IndexToCoordinate(ExtraMath.CoordinateToIndex(vector2, vector), vector))
			{
				Debug.Log(string.Concat(new object[]
				{
					"doesnt work!",
					vector2,
					" ",
					vector,
					" ",
					ExtraMath.CoordinateToIndex(vector2, vector),
					" ",
					ExtraMath.IndexToCoordinate(ExtraMath.CoordinateToIndex(vector2, vector), vector)
				}));
				return;
			}
			int num = (int)UnityEngine.Random.Range(0f, vector.x * vector.z * vector.y);
			if (num != ExtraMath.CoordinateToIndex(ExtraMath.IndexToCoordinate(num, vector), vector))
			{
				Debug.Log(string.Concat(new object[]
				{
					"doesnt work! 2",
					num,
					" ",
					vector,
					" ",
					ExtraMath.IndexToCoordinate(num, vector)
				}));
				return;
			}
		}
		Debug.Log("works!");
	}

	// Token: 0x060021A6 RID: 8614 RVA: 0x0005D218 File Offset: 0x0005B618
	public static int GetVolume(Vector3 vector)
	{
		return (int)vector.x * (int)vector.y * (int)vector.z;
	}

	// Token: 0x060021A7 RID: 8615 RVA: 0x0005D234 File Offset: 0x0005B634
	public static Vector3 RemapValue(Vector3 value, Vector3 in0, Vector3 in1)
	{
		Vector3 result;
		result.x = ExtraMath.RemapValue(value.x, in0.x, in1.x);
		result.y = ExtraMath.RemapValue(value.y, in0.y, in1.y);
		result.z = ExtraMath.RemapValue(value.z, in0.z, in1.z);
		return result;
	}

	// Token: 0x060021A8 RID: 8616 RVA: 0x0005D2A8 File Offset: 0x0005B6A8
	public static Vector3 RemapValue(Vector3 value, Vector3 in0, Vector3 in1, Vector3 out0, Vector3 out1)
	{
		Vector3 result;
		result.x = ExtraMath.RemapValue(value.x, in0.x, in1.x, out0.x, out1.x);
		result.y = ExtraMath.RemapValue(value.y, in0.y, in1.y, out0.y, out1.y);
		result.z = ExtraMath.RemapValue(value.z, in0.z, in1.z, out0.z, out1.z);
		return result;
	}

	// Token: 0x060021A9 RID: 8617 RVA: 0x0005D344 File Offset: 0x0005B744
	public static Vector2 RemapValue(Vector2 value, Vector2 in0, Vector2 in1)
	{
		Vector2 result;
		result.x = ExtraMath.RemapValue(value.x, in0.x, in1.x);
		result.y = ExtraMath.RemapValue(value.y, in0.y, in1.y);
		return result;
	}

	// Token: 0x060021AA RID: 8618 RVA: 0x0005D394 File Offset: 0x0005B794
	public static Vector2 RemapValue(Vector2 value, Vector2 in0, Vector2 in1, Vector2 out0, Vector2 out1)
	{
		Vector2 result;
		result.x = ExtraMath.RemapValue(value.x, in0.x, in1.x, out0.x, out1.x);
		result.y = ExtraMath.RemapValue(value.y, in0.y, in1.y, out0.y, out1.y);
		return result;
	}

	// Token: 0x060021AB RID: 8619 RVA: 0x0005D400 File Offset: 0x0005B800
	public static float RemapValue(float value, float in0, float in1, float out0, float out1)
	{
		if (in1 > in0)
		{
			return Mathf.Lerp(out0, out1, Mathf.InverseLerp(in0, in1, value));
		}
		return Mathf.Lerp(out1, out0, Mathf.InverseLerp(in1, in0, value));
	}

	// Token: 0x060021AC RID: 8620 RVA: 0x0005D42C File Offset: 0x0005B82C
	public static float RemapValue(float value, float in0, float in1)
	{
		if (in1 == in0)
		{
			return 0f;
		}
		float value2 = (value - in0) / (in1 - in0);
		return Mathf.Clamp01(value2);
	}

	// Token: 0x060021AD RID: 8621 RVA: 0x0005D454 File Offset: 0x0005B854
	public static float RemapValueUnclamped(float value, float in0, float in1)
	{
		if (in1 == in0)
		{
			return 0f;
		}
		return (value - in0) / (in1 - in0);
	}

	// Token: 0x060021AE RID: 8622 RVA: 0x0005D46C File Offset: 0x0005B86C
	public static float RemapValueUnclamped(float value, float in0, float in1, float out0, float out1)
	{
		float num = in1 - in0;
		if (num == 0f)
		{
			return out0;
		}
		return out0 + (value - in0) * (out1 - out0) / num;
	}

	// Token: 0x060021AF RID: 8623 RVA: 0x0005D498 File Offset: 0x0005B898
	public static Vector2 RemapValueUnclamped(Vector2 value, Vector2 in0, Vector2 in1)
	{
		Vector2 result;
		result.x = ExtraMath.RemapValueUnclamped(value.x, in0.x, in1.x);
		result.y = ExtraMath.RemapValueUnclamped(value.y, in0.y, in1.y);
		return result;
	}

	// Token: 0x060021B0 RID: 8624 RVA: 0x0005D4E8 File Offset: 0x0005B8E8
	public static Vector3 RemapValueUnclamped(Vector3 value, Vector3 in0, Vector3 in1, Vector3 out0, Vector3 out1)
	{
		Vector3 result;
		result.x = ExtraMath.RemapValueUnclamped(value.x, in0.x, in1.x, out0.x, out1.x);
		result.y = ExtraMath.RemapValueUnclamped(value.y, in0.y, in1.y, out0.y, out1.y);
		result.z = ExtraMath.RemapValueUnclamped(value.z, in0.z, in1.z, out0.z, out1.z);
		return result;
	}

	// Token: 0x060021B1 RID: 8625 RVA: 0x0005D584 File Offset: 0x0005B984
	public static Vector2 RemapValueUnclamped(Vector2 value, Vector2 in0, Vector2 in1, Vector2 out0, Vector2 out1)
	{
		Vector2 result;
		result.x = ExtraMath.RemapValueUnclamped(value.x, in0.x, in1.x, out0.x, out1.x);
		result.y = ExtraMath.RemapValueUnclamped(value.y, in0.y, in1.y, out0.y, out1.y);
		return result;
	}

	// Token: 0x060021B2 RID: 8626 RVA: 0x0005D5F0 File Offset: 0x0005B9F0
	public static Vector3 ForceOrthagonal(Vector3 vector, Vector3 direction, float clampFactor = 1f)
	{
		direction.Normalize();
		float d = Vector3.Dot(vector, direction);
		vector -= direction * d * clampFactor;
		return vector;
	}

	// Token: 0x060021B3 RID: 8627 RVA: 0x0005D624 File Offset: 0x0005BA24
	public static Vector3 ClampVectorToDirection(Vector3 vector, Vector3 direction, float clampFactor = 1f)
	{
		float num = Vector3.Dot(vector, direction);
		if (num < 0f)
		{
			direction.Normalize();
			vector += direction * Vector3.Dot(vector, -direction) * clampFactor;
		}
		return vector;
	}

	// Token: 0x060021B4 RID: 8628 RVA: 0x0005D66C File Offset: 0x0005BA6C
	public static Vector2 ClampVectorToDirection(Vector2 vector, Vector2 direction, float clampFactor = 1f)
	{
		float num = Vector2.Dot(vector, direction);
		if (num < 0f)
		{
			direction.Normalize();
			vector += direction * Vector2.Dot(vector, -direction) * clampFactor;
		}
		return vector;
	}

	// Token: 0x060021B5 RID: 8629 RVA: 0x0005D6B4 File Offset: 0x0005BAB4
	public static Vector3 Lerp(Vector2 v0, Vector2 v1, Vector2 interpolator)
	{
		Vector2 v2;
		v2.x = Mathf.Lerp(v0.x, v1.x, interpolator.x);
		v2.y = Mathf.Lerp(v0.y, v1.y, interpolator.y);
		return v2;
	}

	// Token: 0x060021B6 RID: 8630 RVA: 0x0005D70C File Offset: 0x0005BB0C
	public static Vector3 Lerp(Vector3 v0, Vector3 v1, Vector3 interpolator)
	{
		Vector3 result;
		result.x = Mathf.Lerp(v0.x, v1.x, interpolator.x);
		result.y = Mathf.Lerp(v0.y, v1.y, interpolator.y);
		result.z = Mathf.Lerp(v0.z, v1.z, interpolator.z);
		return result;
	}

	// Token: 0x060021B7 RID: 8631 RVA: 0x0005D780 File Offset: 0x0005BB80
	public static Vector4 Lerp(Vector4 v0, Vector4 v1, Vector4 interpolator)
	{
		Vector4 result;
		result.x = Mathf.Lerp(v0.x, v1.x, interpolator.x);
		result.y = Mathf.Lerp(v0.y, v1.y, interpolator.y);
		result.z = Mathf.Lerp(v0.z, v1.z, interpolator.z);
		result.w = Mathf.Lerp(v0.w, v1.w, interpolator.w);
		return result;
	}

	// Token: 0x060021B8 RID: 8632 RVA: 0x0005D812 File Offset: 0x0005BC12
	public static Vector3 GetOneMinusAbsolute(this Vector3 vector)
	{
		return Vector3.one - ExtraMath.Abs(vector);
	}

	// Token: 0x060021B9 RID: 8633 RVA: 0x0005D824 File Offset: 0x0005BC24
	public static float GetExpLerpInterpolator(float exponent, float deltaTime)
	{
		return Mathf.Pow(Mathf.Clamp01(exponent), deltaTime);
	}

	// Token: 0x060021BA RID: 8634 RVA: 0x0005D832 File Offset: 0x0005BC32
	public static float ExpLerpTowards(float current, float target, float exponent, float deltaTime)
	{
		return Mathf.Lerp(target, current, ExtraMath.GetExpLerpInterpolator(exponent, deltaTime));
	}

	// Token: 0x060021BB RID: 8635 RVA: 0x0005D842 File Offset: 0x0005BC42
	public static Vector2 ExpLerpTowards(Vector2 current, Vector2 target, float exponent, float deltaTime)
	{
		return Vector2.Lerp(target, current, ExtraMath.GetExpLerpInterpolator(exponent, deltaTime));
	}

	// Token: 0x060021BC RID: 8636 RVA: 0x0005D852 File Offset: 0x0005BC52
	public static Vector3 ExpLerpTowards(Vector3 current, Vector3 target, float exponent, float deltaTime)
	{
		return Vector3.Lerp(target, current, ExtraMath.GetExpLerpInterpolator(exponent, deltaTime));
	}

	// Token: 0x060021BD RID: 8637 RVA: 0x0005D862 File Offset: 0x0005BC62
	public static float Atan2(Vector2 vector)
	{
		return Mathf.Atan2(vector.x, vector.y);
	}

	// Token: 0x060021BE RID: 8638 RVA: 0x0005D878 File Offset: 0x0005BC78
	public static bool TriangleInsideDoubleUnitBounds(Vector3 v0, Vector3 v1, Vector3 v2)
	{
		Vector3 lhs = v1 - v0;
		Vector3 rhs = v2 - v1;
		Vector3 vector = v0 - v2;
		Vector3 rhs2 = new Vector3(0f, -lhs.z, lhs.y);
		float a = Vector3.Dot(v0, rhs2);
		float b = Vector3.Dot(v1, rhs2);
		float c = Vector3.Dot(v2, rhs2);
		float num = Mathf.Abs(lhs.z) + Mathf.Abs(lhs.y);
		if (Mathf.Max(-ExtraMath.fmax(a, b, c), ExtraMath.fmin(a, b, c)) > num)
		{
			return false;
		}
		Vector3 rhs3 = new Vector3(0f, -rhs.z, rhs.y);
		a = Vector3.Dot(v0, rhs3);
		b = Vector3.Dot(v1, rhs3);
		c = Vector3.Dot(v2, rhs3);
		num = Mathf.Abs(rhs.z) + Mathf.Abs(rhs.y);
		if (Mathf.Max(-ExtraMath.fmax(a, b, c), ExtraMath.fmin(a, b, c)) > num)
		{
			return false;
		}
		Vector3 rhs4 = new Vector3(0f, -vector.z, vector.y);
		a = Vector3.Dot(v0, rhs4);
		b = Vector3.Dot(v1, rhs4);
		c = Vector3.Dot(v2, rhs4);
		num = Mathf.Abs(vector.z) + Mathf.Abs(vector.y);
		if (Mathf.Max(-ExtraMath.fmax(a, b, c), ExtraMath.fmin(a, b, c)) > num)
		{
			return false;
		}
		Vector3 rhs5 = new Vector3(lhs.z, 0f, -lhs.x);
		a = Vector3.Dot(v0, rhs5);
		b = Vector3.Dot(v1, rhs5);
		c = Vector3.Dot(v2, rhs5);
		num = Mathf.Abs(lhs.z) + Mathf.Abs(lhs.x);
		if (Mathf.Max(-ExtraMath.fmax(a, b, c), ExtraMath.fmin(a, b, c)) > num)
		{
			return false;
		}
		Vector3 rhs6 = new Vector3(rhs.z, 0f, -rhs.x);
		a = Vector3.Dot(v0, rhs6);
		b = Vector3.Dot(v1, rhs6);
		c = Vector3.Dot(v2, rhs6);
		num = Mathf.Abs(rhs.z) + Mathf.Abs(rhs.x);
		if (Mathf.Max(-ExtraMath.fmax(a, b, c), ExtraMath.fmin(a, b, c)) > num)
		{
			return false;
		}
		Vector3 rhs7 = new Vector3(vector.z, 0f, -vector.x);
		a = Vector3.Dot(v0, rhs7);
		b = Vector3.Dot(v1, rhs7);
		c = Vector3.Dot(v2, rhs7);
		num = Mathf.Abs(vector.z) + Mathf.Abs(vector.x);
		if (Mathf.Max(-ExtraMath.fmax(a, b, c), ExtraMath.fmin(a, b, c)) > num)
		{
			return false;
		}
		Vector3 rhs8 = new Vector3(-lhs.y, lhs.x, 0f);
		a = Vector3.Dot(v0, rhs8);
		b = Vector3.Dot(v1, rhs8);
		c = Vector3.Dot(v2, rhs8);
		num = Mathf.Abs(lhs.y) + Mathf.Abs(lhs.x);
		if (Mathf.Max(-ExtraMath.fmax(a, b, c), ExtraMath.fmin(a, b, c)) > num)
		{
			return false;
		}
		Vector3 rhs9 = new Vector3(-rhs.y, rhs.x, 0f);
		a = Vector3.Dot(v0, rhs9);
		b = Vector3.Dot(v1, rhs9);
		c = Vector3.Dot(v2, rhs9);
		num = Mathf.Abs(rhs.y) + Mathf.Abs(rhs.x);
		if (Mathf.Max(-ExtraMath.fmax(a, b, c), ExtraMath.fmin(a, b, c)) > num)
		{
			return false;
		}
		Vector3 rhs10 = new Vector3(-vector.y, vector.x, 0f);
		a = Vector3.Dot(v0, rhs10);
		b = Vector3.Dot(v1, rhs10);
		c = Vector3.Dot(v2, rhs10);
		num = Mathf.Abs(vector.y) + Mathf.Abs(vector.x);
		if (Mathf.Max(-ExtraMath.fmax(a, b, c), ExtraMath.fmin(a, b, c)) > num)
		{
			return false;
		}
		if (ExtraMath.fmax(v0.x, v1.x, v2.x) < -1f || ExtraMath.fmin(v0.x, v1.x, v2.x) > 1f)
		{
			return false;
		}
		if (ExtraMath.fmax(v0.y, v1.y, v2.y) < -1f || ExtraMath.fmin(v0.y, v1.y, v2.y) > 1f)
		{
			return false;
		}
		if (ExtraMath.fmax(v0.z, v1.z, v2.z) < -1f || ExtraMath.fmin(v0.z, v1.z, v2.z) > 1f)
		{
			return false;
		}
		Vector3 lhs2 = Vector3.Cross(lhs, rhs);
		float num2 = Vector3.Dot(lhs2, v0);
		num = Mathf.Abs(lhs2.x) + Mathf.Abs(lhs2.y) + Mathf.Abs(lhs2.z);
		return num2 <= num;
	}

	// Token: 0x060021BF RID: 8639 RVA: 0x0005DE00 File Offset: 0x0005C200
	public static bool BoundsTriangleIntersection(Bounds bounds, Vector3 a, Vector3 b, Vector3 c)
	{
		Vector3 center = bounds.center;
		Vector3 extents = bounds.extents;
		Vector3 vector = a - center;
		Vector3 vector2 = b - center;
		Vector3 vector3 = c - center;
		Vector3 lhs = vector2 - vector;
		Vector3 rhs = vector3 - vector2;
		Vector3 vector4 = vector - vector3;
		Vector3 rhs2 = new Vector3(0f, -lhs.z, lhs.y);
		float a2 = Vector3.Dot(vector, rhs2);
		float b2 = Vector3.Dot(vector2, rhs2);
		float c2 = Vector3.Dot(vector3, rhs2);
		float num = extents.y * Mathf.Abs(lhs.z) + extents.z * Mathf.Abs(lhs.y);
		if (Mathf.Max(-ExtraMath.fmax(a2, b2, c2), ExtraMath.fmin(a2, b2, c2)) > num)
		{
			return false;
		}
		Vector3 rhs3 = new Vector3(0f, -rhs.z, rhs.y);
		a2 = Vector3.Dot(vector, rhs3);
		b2 = Vector3.Dot(vector2, rhs3);
		c2 = Vector3.Dot(vector3, rhs3);
		num = extents.y * Mathf.Abs(rhs.z) + extents.z * Mathf.Abs(rhs.y);
		if (Mathf.Max(-ExtraMath.fmax(a2, b2, c2), ExtraMath.fmin(a2, b2, c2)) > num)
		{
			return false;
		}
		Vector3 rhs4 = new Vector3(0f, -vector4.z, vector4.y);
		a2 = Vector3.Dot(vector, rhs4);
		b2 = Vector3.Dot(vector2, rhs4);
		c2 = Vector3.Dot(vector3, rhs4);
		num = extents.y * Mathf.Abs(vector4.z) + extents.z * Mathf.Abs(vector4.y);
		if (Mathf.Max(-ExtraMath.fmax(a2, b2, c2), ExtraMath.fmin(a2, b2, c2)) > num)
		{
			return false;
		}
		Vector3 rhs5 = new Vector3(lhs.z, 0f, -lhs.x);
		a2 = Vector3.Dot(vector, rhs5);
		b2 = Vector3.Dot(vector2, rhs5);
		c2 = Vector3.Dot(vector3, rhs5);
		num = extents.x * Mathf.Abs(lhs.z) + extents.z * Mathf.Abs(lhs.x);
		if (Mathf.Max(-ExtraMath.fmax(a2, b2, c2), ExtraMath.fmin(a2, b2, c2)) > num)
		{
			return false;
		}
		Vector3 rhs6 = new Vector3(rhs.z, 0f, -rhs.x);
		a2 = Vector3.Dot(vector, rhs6);
		b2 = Vector3.Dot(vector2, rhs6);
		c2 = Vector3.Dot(vector3, rhs6);
		num = extents.x * Mathf.Abs(rhs.z) + extents.z * Mathf.Abs(rhs.x);
		if (Mathf.Max(-ExtraMath.fmax(a2, b2, c2), ExtraMath.fmin(a2, b2, c2)) > num)
		{
			return false;
		}
		Vector3 rhs7 = new Vector3(vector4.z, 0f, -vector4.x);
		a2 = Vector3.Dot(vector, rhs7);
		b2 = Vector3.Dot(vector2, rhs7);
		c2 = Vector3.Dot(vector3, rhs7);
		num = extents.x * Mathf.Abs(vector4.z) + extents.z * Mathf.Abs(vector4.x);
		if (Mathf.Max(-ExtraMath.fmax(a2, b2, c2), ExtraMath.fmin(a2, b2, c2)) > num)
		{
			return false;
		}
		Vector3 rhs8 = new Vector3(-lhs.y, lhs.x, 0f);
		a2 = Vector3.Dot(vector, rhs8);
		b2 = Vector3.Dot(vector2, rhs8);
		c2 = Vector3.Dot(vector3, rhs8);
		num = extents.x * Mathf.Abs(lhs.y) + extents.y * Mathf.Abs(lhs.x);
		if (Mathf.Max(-ExtraMath.fmax(a2, b2, c2), ExtraMath.fmin(a2, b2, c2)) > num)
		{
			return false;
		}
		Vector3 rhs9 = new Vector3(-rhs.y, rhs.x, 0f);
		a2 = Vector3.Dot(vector, rhs9);
		b2 = Vector3.Dot(vector2, rhs9);
		c2 = Vector3.Dot(vector3, rhs9);
		num = extents.x * Mathf.Abs(rhs.y) + extents.y * Mathf.Abs(rhs.x);
		if (Mathf.Max(-ExtraMath.fmax(a2, b2, c2), ExtraMath.fmin(a2, b2, c2)) > num)
		{
			return false;
		}
		Vector3 rhs10 = new Vector3(-vector4.y, vector4.x, 0f);
		a2 = Vector3.Dot(vector, rhs10);
		b2 = Vector3.Dot(vector2, rhs10);
		c2 = Vector3.Dot(vector3, rhs10);
		num = extents.x * Mathf.Abs(vector4.y) + extents.y * Mathf.Abs(vector4.x);
		if (Mathf.Max(-ExtraMath.fmax(a2, b2, c2), ExtraMath.fmin(a2, b2, c2)) > num)
		{
			return false;
		}
		if (ExtraMath.fmax(vector.x, vector2.x, vector3.x) < -extents.x || ExtraMath.fmin(vector.x, vector2.x, vector3.x) > extents.x)
		{
			return false;
		}
		if (ExtraMath.fmax(vector.y, vector2.y, vector3.y) < -extents.y || ExtraMath.fmin(vector.y, vector2.y, vector3.y) > extents.y)
		{
			return false;
		}
		if (ExtraMath.fmax(vector.z, vector2.z, vector3.z) < -extents.z || ExtraMath.fmin(vector.z, vector2.z, vector3.z) > extents.z)
		{
			return false;
		}
		Vector3 lhs2 = Vector3.Cross(lhs, rhs);
		float num2 = Vector3.Dot(lhs2, vector);
		num = extents.x * Mathf.Abs(lhs2.x) + extents.y * Mathf.Abs(lhs2.y) + extents.z * Mathf.Abs(lhs2.z);
		return num2 <= num;
	}

	// Token: 0x060021C0 RID: 8640 RVA: 0x0005E47B File Offset: 0x0005C87B
	private static float fmin(float a, float b, float c)
	{
		return Mathf.Min(a, Mathf.Min(b, c));
	}

	// Token: 0x060021C1 RID: 8641 RVA: 0x0005E48A File Offset: 0x0005C88A
	private static float fmax(float a, float b, float c)
	{
		return Mathf.Max(a, Mathf.Max(b, c));
	}

	// Token: 0x060021C2 RID: 8642 RVA: 0x0005E49C File Offset: 0x0005C89C
	public static bool LineIntersection(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4)
	{
		float num = p2.x - p1.x;
		float num2 = p3.x - p4.x;
		float x;
		float x2;
		if (num < 0f)
		{
			x = p2.x;
			x2 = p1.x;
		}
		else
		{
			x2 = p2.x;
			x = p1.x;
		}
		if (num2 > 0f)
		{
			if (x2 < p4.x || p3.x < x)
			{
				return false;
			}
		}
		else if (x2 < p3.x || p4.x < x)
		{
			return false;
		}
		float num3 = p2.y - p1.y;
		float num4 = p3.y - p4.y;
		float y;
		float y2;
		if (num3 < 0f)
		{
			y = p2.y;
			y2 = p1.y;
		}
		else
		{
			y2 = p2.y;
			y = p1.y;
		}
		if (num4 > 0f)
		{
			if (y2 < p4.y || p3.y < y)
			{
				return false;
			}
		}
		else if (y2 < p3.y || p4.y < y)
		{
			return false;
		}
		float num5 = p1.x - p3.x;
		float num6 = p1.y - p3.y;
		float num7 = num4 * num5 - num2 * num6;
		float num8 = num3 * num2 - num * num4;
		if (num8 > 0f)
		{
			if (num7 < 0f || num7 > num8)
			{
				return false;
			}
		}
		else if (num7 > 0f || num7 < num8)
		{
			return false;
		}
		float num9 = num * num6 - num3 * num5;
		if (num8 > 0f)
		{
			if (num9 < 0f || num9 > num8)
			{
				return false;
			}
		}
		else if (num9 > 0f || num9 < num8)
		{
			return false;
		}
		return num8 != 0f;
	}

	// Token: 0x060021C3 RID: 8643 RVA: 0x0005E6B8 File Offset: 0x0005CAB8
	public static bool LineIntersection(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, ref Vector2 intersection)
	{
		float num = p2.x - p1.x;
		float num2 = p3.x - p4.x;
		float x;
		float x2;
		if (num < 0f)
		{
			x = p2.x;
			x2 = p1.x;
		}
		else
		{
			x2 = p2.x;
			x = p1.x;
		}
		if (num2 > 0f)
		{
			if (x2 < p4.x || p3.x < x)
			{
				return false;
			}
		}
		else if (x2 < p3.x || p4.x < x)
		{
			return false;
		}
		float num3 = p2.y - p1.y;
		float num4 = p3.y - p4.y;
		float y;
		float y2;
		if (num3 < 0f)
		{
			y = p2.y;
			y2 = p1.y;
		}
		else
		{
			y2 = p2.y;
			y = p1.y;
		}
		if (num4 > 0f)
		{
			if (y2 < p4.y || p3.y < y)
			{
				return false;
			}
		}
		else if (y2 < p3.y || p4.y < y)
		{
			return false;
		}
		float num5 = p1.x - p3.x;
		float num6 = p1.y - p3.y;
		float num7 = num4 * num5 - num2 * num6;
		float num8 = num3 * num2 - num * num4;
		if (num8 > 0f)
		{
			if (num7 < 0f || num7 > num8)
			{
				return false;
			}
		}
		else if (num7 > 0f || num7 < num8)
		{
			return false;
		}
		float num9 = num * num6 - num3 * num5;
		if (num8 > 0f)
		{
			if (num9 < 0f || num9 > num8)
			{
				return false;
			}
		}
		else if (num9 > 0f || num9 < num8)
		{
			return false;
		}
		if (num8 == 0f)
		{
			return false;
		}
		float num10 = num7 * num;
		intersection.x = p1.x + num10 / num8;
		num10 = num7 * num3;
		intersection.y = p1.y + num10 / num8;
		return true;
	}

	// Token: 0x060021C4 RID: 8644 RVA: 0x0005E908 File Offset: 0x0005CD08
	public static float DistanceBetweenLineSegments(Vector3 a0, Vector3 a1, Vector3 b0, Vector3 b1)
	{
		Vector3 vector = a1 - a0;
		Vector3 vector2 = b1 - b0;
		Vector3 vector3 = a0 - b0;
		float num = Vector3.Dot(vector, vector);
		float num2 = Vector3.Dot(vector, vector2);
		float num3 = Vector3.Dot(vector2, vector2);
		float num4 = Vector3.Dot(vector, vector3);
		float num5 = Vector3.Dot(vector2, vector3);
		float num6 = num * num3 - num2 * num2;
		float num7 = num6;
		float num8 = num6;
		float num9;
		float num10;
		if ((double)num6 < 0.01)
		{
			num9 = 0f;
			num7 = 1f;
			num10 = num5;
			num8 = num3;
		}
		else
		{
			num9 = num2 * num5 - num3 * num4;
			num10 = num * num5 - num2 * num4;
			if (num9 < 0f)
			{
				num9 = 0f;
				num10 = num5;
				num8 = num3;
			}
			else if (num9 > num7)
			{
				num9 = num7;
				num10 = num5 + num2;
				num8 = num3;
			}
		}
		if (num10 < 0f)
		{
			num10 = 0f;
			if (-num4 < 0f)
			{
				num9 = 0f;
			}
			else if (-num4 > num)
			{
				num9 = num7;
			}
			else
			{
				num9 = -num4;
				num7 = num;
			}
		}
		else if (num10 > num8)
		{
			num10 = num8;
			if (-num4 + num2 < 0f)
			{
				num9 = 0f;
			}
			else if (-num4 + num2 > num)
			{
				num9 = num7;
			}
			else
			{
				num9 = -num4 + num2;
				num7 = num;
			}
		}
		float d;
		if ((double)Mathf.Abs(num9) < 0.01)
		{
			d = 0f;
		}
		else
		{
			d = num9 / num7;
		}
		float d2;
		if ((double)Mathf.Abs(num10) < 0.01)
		{
			d2 = 0f;
		}
		else
		{
			d2 = num10 / num8;
		}
		Vector3 vector4 = vector3 + d * vector - d2 * vector2;
		return Mathf.Sqrt(Vector3.Dot(vector4, vector4));
	}

	// Token: 0x060021C5 RID: 8645 RVA: 0x0005EB28 File Offset: 0x0005CF28
	public static float DistanceTolineSegment(Vector2 v, Vector2 w, Vector2 p)
	{
		float sqrMagnitude = (v - w).sqrMagnitude;
		if (sqrMagnitude == 0f)
		{
			return (p - v).magnitude;
		}
		float d = Mathf.Clamp01(Vector2.Dot(p - v, w - v) / sqrMagnitude);
		Vector2 b = v + d * (w - v);
		return (p - b).magnitude;
	}

	// Token: 0x060021C6 RID: 8646 RVA: 0x0005EBA4 File Offset: 0x0005CFA4
	public static int Pow(int val, int exponent)
	{
		int num = 1;
		for (int i = 0; i < exponent; i++)
		{
			num *= val;
		}
		return num;
	}

	// Token: 0x060021C7 RID: 8647 RVA: 0x0005EBCC File Offset: 0x0005CFCC
	public static float SignedAngle(Vector3 aNormal, Vector3 bNormal, Vector3 referenceNormal)
	{
		float num = Mathf.Acos(Vector3.Dot(aNormal, bNormal));
		Vector3 lhs = Vector3.Cross(aNormal, bNormal);
		return (Vector3.Dot(lhs, referenceNormal) >= 0f) ? (-num) : num;
	}

	// Token: 0x04001481 RID: 5249
	public static readonly Vector3 half = Vector3.one / 2f;
}
