using System;
using UnityEngine;

// Token: 0x020005FB RID: 1531
[Serializable]
public class SimpleLinearSeeker
{
	// Token: 0x06002778 RID: 10104 RVA: 0x0007FC39 File Offset: 0x0007E039
	private float MoveTowards(float current, float target, float maxDelta)
	{
		return (this.mode != SimpleLinearSeeker.EMode.angle) ? Mathf.MoveTowards(current, target, maxDelta) : Mathf.MoveTowardsAngle(current, target, maxDelta);
	}

	// Token: 0x06002779 RID: 10105 RVA: 0x0007FC5C File Offset: 0x0007E05C
	private float GetDiff(float current, float target)
	{
		return (this.mode != SimpleLinearSeeker.EMode.angle) ? (target - current) : Mathf.DeltaAngle(current, target);
	}

	// Token: 0x0600277A RID: 10106 RVA: 0x0007FC79 File Offset: 0x0007E079
	public void EndSeek()
	{
		this.speed = 0f;
		this.target = this.position;
	}

	// Token: 0x0600277B RID: 10107 RVA: 0x0007FC92 File Offset: 0x0007E092
	public void EndIfOpposed(float direction)
	{
		if (!this.IsAtTarget() && this.GetDiff(this.position, this.target) * direction < 0f)
		{
			this.EndSeek();
		}
	}

	// Token: 0x0600277C RID: 10108 RVA: 0x0007FCC4 File Offset: 0x0007E0C4
	public bool Update(float deltaTime, out float newPosition)
	{
		newPosition = this.position;
		float num = Mathf.Abs(this.GetDiff(this.position, this.target));
		if (num < this.maxError)
		{
			this.EndSeek();
			return false;
		}
		this.speed = Mathf.MoveTowards(this.speed, this.maxSeekVelocity, deltaTime * this.acceleration);
		float b = Mathf.Lerp(this.minSeekVelocity, this.maxSeekVelocity, num / this.decelerationDist);
		this.speed = Mathf.Min(this.speed, b);
		this.speed = Mathf.Max(this.speed, this.minSeekVelocity);
		newPosition = (this.position = this.MoveTowards(this.position, this.target, this.speed * deltaTime));
		return true;
	}

	// Token: 0x0600277D RID: 10109 RVA: 0x0007FD8D File Offset: 0x0007E18D
	public bool IsAtTarget()
	{
		return Mathf.Abs(this.GetDiff(this.position, this.target)) < this.maxError;
	}

	// Token: 0x0400194D RID: 6477
	public SimpleLinearSeeker.EMode mode;

	// Token: 0x0400194E RID: 6478
	public float maxSeekVelocity = 720f;

	// Token: 0x0400194F RID: 6479
	public float minSeekVelocity = 5f;

	// Token: 0x04001950 RID: 6480
	public float acceleration = 720f;

	// Token: 0x04001951 RID: 6481
	public float decelerationDist = 15f;

	// Token: 0x04001952 RID: 6482
	public float maxError = 0.5f;

	// Token: 0x04001953 RID: 6483
	[HideInInspector]
	public float target;

	// Token: 0x04001954 RID: 6484
	[HideInInspector]
	public float position;

	// Token: 0x04001955 RID: 6485
	[HideInInspector]
	public float speed;

	// Token: 0x020005FC RID: 1532
	public enum EMode
	{
		// Token: 0x04001957 RID: 6487
		continuous,
		// Token: 0x04001958 RID: 6488
		angle
	}
}
