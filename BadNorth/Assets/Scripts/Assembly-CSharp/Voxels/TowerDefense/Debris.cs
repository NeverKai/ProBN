using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x0200074E RID: 1870
	public class Debris : MonoBehaviour
	{
		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x060030BF RID: 12479 RVA: 0x000C7273 File Offset: 0x000C5673
		public Rigidbody rb
		{
			get
			{
				if (!this._rb)
				{
					this._rb = base.GetComponent<Rigidbody>();
				}
				return this._rb;
			}
		}

		// Token: 0x060030C0 RID: 12480 RVA: 0x000C7297 File Offset: 0x000C5697
		private void OnCollisionEnter(Collision collision)
		{
			Singleton<DustParticles>.instance.SpawnParticles(collision.contacts[0].point, collision.contacts[0].normal);
		}

		// Token: 0x060030C1 RID: 12481 RVA: 0x000C72C8 File Offset: 0x000C56C8
		private void OnCollisionStay(Collision collisionInfo)
		{
			if (this.rb.velocity.sqrMagnitude < this.mininumVelocity * this.mininumVelocity)
			{
				NavPos navPos = new NavPos(NavigationMesh.instance, base.transform.position, true, 1f);
				if ((double)Vector3.SqrMagnitude(navPos.wPos - base.transform.position) < 0.2)
				{
					this.rb.isKinematic = true;
					base.transform.position = navPos.wPos;
				}
			}
			if (this.rb.velocity.sqrMagnitude == 0f)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}

		// Token: 0x060030C2 RID: 12482 RVA: 0x000C7388 File Offset: 0x000C5788
		private void Update()
		{
			if (base.transform.position.y < -0.105f)
			{
				Vector3 position = base.transform.position;
				position.y = 0f;
				ScriptableObjectSingleton<PrefabManager>.instance.splash.PlayAt(position, Vector3.up);
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}

		// Token: 0x04002095 RID: 8341
		private Rigidbody _rb;

		// Token: 0x04002096 RID: 8342
		public Vector3 localVelocity;

		// Token: 0x04002097 RID: 8343
		private float mininumVelocity = 1f;
	}
}
