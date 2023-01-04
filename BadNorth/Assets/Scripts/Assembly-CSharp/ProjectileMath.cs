using System;
using UnityEngine;

// Token: 0x020007BB RID: 1979
public static class ProjectileMath
{
	// Token: 0x06003333 RID: 13107 RVA: 0x000DC0D0 File Offset: 0x000DA4D0
	public static Vector2 Get2DVec(Vector3 vector)
	{
		Vector3 vector2 = vector;
		float x = Mathf.Sqrt(vector2.x * vector2.x + vector2.z * vector2.z);
		return new Vector2(x, vector2.y);
	}

	// Token: 0x06003334 RID: 13108 RVA: 0x000DC114 File Offset: 0x000DA514
	public static Vector3 Get3DVec(Vector2 vector, Vector3 targetDisplacement)
	{
		Vector3 normalized = targetDisplacement.GetZeroY().normalized;
		return new Vector3(vector.x * normalized.x, vector.y, vector.x * normalized.z);
	}

	// Token: 0x06003335 RID: 13109 RVA: 0x000DC15A File Offset: 0x000DA55A
	public static Vector2 GetLaunchVelocity(float speed, float angleRadians)
	{
		return new Vector2(Mathf.Cos(angleRadians), Mathf.Sin(angleRadians)) * speed;
	}

	// Token: 0x06003336 RID: 13110 RVA: 0x000DC174 File Offset: 0x000DA574
	public static Vector3 GetLaunchVelocity(Vector3 targetDisplacement, float angleRadians, float speed)
	{
		Vector3 normalized = targetDisplacement.GetZeroY().normalized;
		float num = Mathf.Cos(angleRadians);
		float y = Mathf.Sin(angleRadians);
		return new Vector3(normalized.x * num, y, normalized.z * num) * speed;
	}

	// Token: 0x06003337 RID: 13111 RVA: 0x000DC1BC File Offset: 0x000DA5BC
	public static void BreakLaunchVectorToAngleAndSpeed(Vector2 launchVelocity, out float elevationRadians, out float speedSq)
	{
		elevationRadians = Mathf.Atan2(launchVelocity.y, launchVelocity.x);
		speedSq = Vector2.SqrMagnitude(launchVelocity);
	}

	// Token: 0x06003338 RID: 13112 RVA: 0x000DC1DC File Offset: 0x000DA5DC
	public static void BreakLaunchVectorToAngleAndSpeed(Vector3 launchVelocity, out float elevationRadians, out float speedSq)
	{
		float x = Mathf.Sqrt(launchVelocity.x * launchVelocity.x + launchVelocity.z * launchVelocity.z);
		ProjectileMath.BreakLaunchVectorToAngleAndSpeed(new Vector2(x, launchVelocity.y), out elevationRadians, out speedSq);
	}

	// Token: 0x06003339 RID: 13113 RVA: 0x000DC224 File Offset: 0x000DA624
	public static Vector3 GetDisplacementAtTime(Vector3 launchVelocity, float gravity, float time)
	{
		Vector3 result = launchVelocity * time;
		result.y += 0.5f * gravity * time * time;
		return result;
	}

	// Token: 0x0600333A RID: 13114 RVA: 0x000DC254 File Offset: 0x000DA654
	public static Vector2 GetDisplacementAtTime(Vector2 launchVelocity, float gravity, float time)
	{
		Vector2 result = launchVelocity * time;
		result.y += 0.5f * gravity * time * time;
		return result;
	}

	// Token: 0x0600333B RID: 13115 RVA: 0x000DC283 File Offset: 0x000DA683
	public static float GetPeakTime(float launchVelocityVertical, float gravity)
	{
		return -launchVelocityVertical / gravity;
	}

	// Token: 0x0600333C RID: 13116 RVA: 0x000DC289 File Offset: 0x000DA689
	public static float GetTimeToHorizontal(float launchSpeedHorizontal, float distanceHorizontal)
	{
		return distanceHorizontal / launchSpeedHorizontal;
	}

	// Token: 0x0600333D RID: 13117 RVA: 0x000DC28E File Offset: 0x000DA68E
	public static bool ComputeProjectileSpeedSq(Vector3 displacement, float elevationRadians, float gravity, out float speedSq)
	{
		return ProjectileMath.ComputeProjectileSpeedSq(ProjectileMath.Get2DVec(displacement), elevationRadians, gravity, out speedSq);
	}

	// Token: 0x0600333E RID: 13118 RVA: 0x000DC2A0 File Offset: 0x000DA6A0
	public static bool ComputeProjectileSpeedSq(Vector2 displacement, float elevationRadians, float gravity, out float speedSq)
	{
		speedSq = 0f;
		Vector2 vector = displacement;
		if (elevationRadians >= 3.1415927f || elevationRadians <= -3.1415927f)
		{
			return false;
		}
		float num = Mathf.Tan(elevationRadians);
		float num2 = Mathf.Cos(elevationRadians);
		if (vector.x * num <= vector.y)
		{
			return false;
		}
		speedSq = gravity * vector.x * vector.x / (2f * num2 * num2) / (vector.y - vector.x * num);
		return true;
	}

	// Token: 0x0600333F RID: 13119 RVA: 0x000DC325 File Offset: 0x000DA725
	public static bool ComputeProjectileMinEffort(Vector3 displacement, float minElevationRads, float maxElevationRads, float searchStep, float gravity, out float outRadians, out float speedSq)
	{
		return ProjectileMath.ComputeProjectileMinEffort(ProjectileMath.Get2DVec(displacement), minElevationRads, maxElevationRads, searchStep, gravity, out outRadians, out speedSq);
	}

	// Token: 0x06003340 RID: 13120 RVA: 0x000DC33C File Offset: 0x000DA73C
	public static bool ComputeProjectileMinEffort(Vector2 displacement, float minElevationRads, float maxElevationRads, float searchStep, float gravity, out float outRadians, out float speedSq)
	{
		float b = Mathf.Atan2(displacement.y, displacement.x);
		minElevationRads = Mathf.Max(minElevationRads, b);
		maxElevationRads = Mathf.Min(maxElevationRads, 1.5707964f);
		speedSq = float.MaxValue;
		outRadians = minElevationRads;
		for (float num = minElevationRads + searchStep; num < maxElevationRads; num += searchStep)
		{
			float num2 = 0f;
			if (ProjectileMath.ComputeProjectileSpeedSq(displacement, num, gravity, out num2) && num2 < speedSq)
			{
				speedSq = num2;
				outRadians = num;
			}
			else if (outRadians < 3.4028235E+38f)
			{
				return true;
			}
		}
		return outRadians < float.MaxValue;
	}

	// Token: 0x06003341 RID: 13121 RVA: 0x000DC3D8 File Offset: 0x000DA7D8
	public static bool ComputeProjectileWithPeakHeight(Vector2 displacement, float peakHeight, float gravity, out Vector2 launchVelocity, out float peakTime, out float targetTime)
	{
		launchVelocity = Vector2.zero;
		launchVelocity.y = Mathf.Sqrt(-2f * gravity * peakHeight);
		float num = displacement.y - peakHeight;
		float num2 = 1f / gravity;
		float num3 = -launchVelocity.y * num2;
		float num4 = Mathf.Sqrt(2f * num * num2);
		float num5 = num3 + num4;
		launchVelocity.x = displacement.x / num5;
		peakTime = num3;
		targetTime = num5;
		return true;
	}

	// Token: 0x06003342 RID: 13122 RVA: 0x000DC44E File Offset: 0x000DA84E
	public static bool ComputeProjectileWithFixedHorizonalSpeed(Vector3 displacement, float horizontalSpeed, float gravity, out Vector2 launchVelocity)
	{
		return ProjectileMath.ComputeProjectileWithFixedHorizonalSpeed(ProjectileMath.Get2DVec(displacement), horizontalSpeed, gravity, out launchVelocity);
	}

	// Token: 0x06003343 RID: 13123 RVA: 0x000DC460 File Offset: 0x000DA860
	public static bool ComputeProjectileWithFixedHorizonalSpeed(Vector2 displacement, float horizontalSpeed, float gravity, out Vector2 launchVelocity)
	{
		float num = displacement.x / horizontalSpeed;
		launchVelocity.x = horizontalSpeed;
		launchVelocity.y = displacement.y / num - 0.5f * gravity * num;
		return true;
	}

	// Token: 0x06003344 RID: 13124 RVA: 0x000DC498 File Offset: 0x000DA898
	public static bool ComputeProjectileWithFixedLinearSpeed(Vector3 displacement, float linearSpeed, float gravity, out Vector2 launchVelocity)
	{
		return ProjectileMath.ComputeProjectileWithFixedLinearSpeed(ProjectileMath.Get2DVec(displacement), linearSpeed, gravity, out launchVelocity);
	}

	// Token: 0x06003345 RID: 13125 RVA: 0x000DC4A8 File Offset: 0x000DA8A8
	public static bool ComputeProjectileWithFixedLinearSpeed(Vector2 displacement, float linearSpeed, float gravity, out Vector2 launchVelocity)
	{
		float horizontalSpeed = linearSpeed * displacement.x / displacement.magnitude;
		return ProjectileMath.ComputeProjectileWithFixedHorizonalSpeed(displacement, horizontalSpeed, gravity, out launchVelocity);
	}

	// Token: 0x06003346 RID: 13126 RVA: 0x000DC4D0 File Offset: 0x000DA8D0
	public static bool IsUnderParabola(Vector2 targetDisplacement, Vector2 launchVelocity, float gravity)
	{
		bool result;
		using (new ScopedProfiler("IsUnderParabola", null))
		{
			float num = targetDisplacement.x / launchVelocity.x;
			float num2 = launchVelocity.y * num + 0.5f * gravity * num * num;
			result = (targetDisplacement.y < num2);
		}
		return result;
	}

	// Token: 0x06003347 RID: 13127 RVA: 0x000DC540 File Offset: 0x000DA940
	public static bool IsUnderParabola(Vector3 targetDisplacement, Vector2 launchVelocity, float gravity)
	{
		return ProjectileMath.IsUnderParabola(ProjectileMath.Get2DVec(targetDisplacement), launchVelocity, gravity);
	}
}
