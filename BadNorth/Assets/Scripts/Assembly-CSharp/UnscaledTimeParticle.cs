using System;
using UnityEngine;

// Token: 0x02000605 RID: 1541
[RequireComponent(typeof(ParticleSystem))]
public class UnscaledTimeParticle : MonoBehaviour
{
	// Token: 0x060027AF RID: 10159 RVA: 0x00080651 File Offset: 0x0007EA51
	private void Awake()
	{
		this.ps = base.GetComponent<ParticleSystem>();
	}

	// Token: 0x060027B0 RID: 10160 RVA: 0x00080660 File Offset: 0x0007EA60
	private void Update()
	{
		float num = Time.unscaledDeltaTime - Time.deltaTime;
		if (num > 0f)
		{
			if (!this.ps)
			{
				this.ps = base.GetComponent<ParticleSystem>();
			}
			if (this.ps)
			{
				this.ps.Simulate(num, true, false);
			}
		}
	}

	// Token: 0x04001976 RID: 6518
	private ParticleSystem ps;
}
