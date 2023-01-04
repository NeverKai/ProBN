using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020005BC RID: 1468
public class Rotator : MonoBehaviour
{
	// Token: 0x0600267A RID: 9850 RVA: 0x00079E64 File Offset: 0x00078264
	private void Update()
	{
		base.transform.localRotation *= Quaternion.Euler(0f, 360f / (float)this.turns, 0f);
		this.iterator++;
		if (this.iterator == this.turns)
		{
			this.onRevolution.Invoke();
		}
	}

	// Token: 0x0400185E RID: 6238
	public int turns;

	// Token: 0x0400185F RID: 6239
	private int iterator;

	// Token: 0x04001860 RID: 6240
	public UnityEvent onRevolution;
}
