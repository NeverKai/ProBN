using System;
using UnityEngine;

// Token: 0x020004EF RID: 1263
public class SaveIconRotate : MonoBehaviour
{
	// Token: 0x0600205B RID: 8283 RVA: 0x000571B7 File Offset: 0x000555B7
	private void OnEnable()
	{
		this.isTick = true;
		this.timeRemainingInPhase = this.tickDuration;
		base.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 30f));
	}

	// Token: 0x0600205C RID: 8284 RVA: 0x000571F0 File Offset: 0x000555F0
	private void Update()
	{
		if (this.timeRemainingInPhase <= 0f)
		{
			this.isTick = !this.isTick;
			this.timeRemainingInPhase = ((!this.isTick) ? this.tockDuration : this.tickDuration);
		}
		float num = Mathf.Min(Mathf.Min(Time.unscaledDeltaTime, 0.033333335f), this.timeRemainingInPhase);
		this.timeRemainingInPhase -= num;
		if (this.isTick)
		{
			Vector3 eulers = new Vector3(0f, 0f, -(30f * num / this.tickDuration));
			base.transform.Rotate(eulers);
		}
	}

	// Token: 0x04001415 RID: 5141
	public float tickDuration = 0.2f;

	// Token: 0x04001416 RID: 5142
	public float tockDuration = 0.8f;

	// Token: 0x04001417 RID: 5143
	private bool isTick;

	// Token: 0x04001418 RID: 5144
	private float timeRemainingInPhase;
}
