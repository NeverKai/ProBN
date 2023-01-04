using System;
using UnityEngine;

// Token: 0x020005FA RID: 1530
[Serializable]
public class SimpleLinearAccelerator
{
	// Token: 0x06002775 RID: 10101 RVA: 0x0007FB1C File Offset: 0x0007DF1C
	public bool Update(float deltaTime, out float moveDelta)
	{
		moveDelta = 0f;
		float num = this.maxSpeed * this.input;
		bool flag = this.input != 0f && Mathf.Abs(num) > Mathf.Abs(this.velocity) && this.velocity * num >= 0f;
		float num2 = (!flag) ? this.deceleration : this.acceleration;
		this.velocity = Mathf.MoveTowards(this.velocity, num, deltaTime * num2);
		bool flag2 = Mathf.Max(Mathf.Abs(this.velocity), Mathf.Abs(num)) > this.minSpeed;
		if (flag2)
		{
			moveDelta = this.velocity * deltaTime;
		}
		else
		{
			this.EndMovementImmediate();
		}
		return flag2;
	}

	// Token: 0x06002776 RID: 10102 RVA: 0x0007FBE2 File Offset: 0x0007DFE2
	public void EndMovementImmediate()
	{
		this.velocity = 0f;
		this.input = 0f;
	}

	// Token: 0x04001947 RID: 6471
	public float maxSpeed = 540f;

	// Token: 0x04001948 RID: 6472
	public float acceleration = 720f;

	// Token: 0x04001949 RID: 6473
	public float deceleration = 1440f;

	// Token: 0x0400194A RID: 6474
	public float minSpeed = 1f;

	// Token: 0x0400194B RID: 6475
	[HideInInspector]
	public float input;

	// Token: 0x0400194C RID: 6476
	[HideInInspector]
	public float velocity;
}
