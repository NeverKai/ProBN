using System;
using UnityEngine;

namespace Rewired.Demos
{
	// Token: 0x0200048C RID: 1164
	[AddComponentMenu("")]
	public class Bullet : MonoBehaviour
	{
		// Token: 0x06001AC2 RID: 6850 RVA: 0x00049CD8 File Offset: 0x000480D8
		private void Start()
		{
			if (this.lifeTime > 0f)
			{
				this.deathTime = Time.time + this.lifeTime;
				this.die = true;
			}
		}

		// Token: 0x06001AC3 RID: 6851 RVA: 0x00049D03 File Offset: 0x00048103
		private void Update()
		{
			if (this.die && Time.time >= this.deathTime)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}

		// Token: 0x040010C2 RID: 4290
		public float lifeTime = 3f;

		// Token: 0x040010C3 RID: 4291
		private bool die;

		// Token: 0x040010C4 RID: 4292
		private float deathTime;
	}
}
