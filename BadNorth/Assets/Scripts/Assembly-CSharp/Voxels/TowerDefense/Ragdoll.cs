using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020007C9 RID: 1993
	public class Ragdoll : SelfPoolingPrefab
	{
		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x060033B2 RID: 13234 RVA: 0x000DEFDB File Offset: 0x000DD3DB
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

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x060033B3 RID: 13235 RVA: 0x000DEFFF File Offset: 0x000DD3FF
		public SphereCollider col
		{
			get
			{
				if (this._col == null)
				{
					this._col = base.GetComponent<SphereCollider>();
				}
				return this._col;
			}
		}

		// Token: 0x060033B4 RID: 13236 RVA: 0x000DF024 File Offset: 0x000DD424
		private void OnCollisionEnter(Collision collision)
		{
			if (this.ragdoller)
			{
				this.ragdoller.OnColEnter(collision);
			}
		}

		// Token: 0x060033B5 RID: 13237 RVA: 0x000DF042 File Offset: 0x000DD442
		private void OnCollisionStay(Collision collision)
		{
			if (this.ragdoller)
			{
				this.ragdoller.OnColStay(collision);
			}
		}

		// Token: 0x060033B6 RID: 13238 RVA: 0x000DF060 File Offset: 0x000DD460
		private void FixedUpdate()
		{
			this.rb.velocity *= 0.95f;
			this.rb.velocity += Vector3.down * 0.4f;
		}

		// Token: 0x04002335 RID: 9013
		private Rigidbody _rb;

		// Token: 0x04002336 RID: 9014
		private SphereCollider _col;

		// Token: 0x04002337 RID: 9015
		public Ragdoller ragdoller;
	}
}
