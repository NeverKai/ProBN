using System;
using RTM.Pools;
using UnityEngine;
using Voxels.TowerDefense.SpriteMagic;

namespace Voxels.TowerDefense.CoinDispensing
{
	// Token: 0x0200072E RID: 1838
	public class DispensedCoin : MonoBehaviour, IPoolable
	{
		// Token: 0x06002FB0 RID: 12208 RVA: 0x000C2F98 File Offset: 0x000C1398
		public void SetRed(bool red)
		{
			this.animator.SetBool(DispensedCoin.redId, red);
		}

		// Token: 0x06002FB1 RID: 12209 RVA: 0x000C2FB0 File Offset: 0x000C13B0
		public void Disappear()
		{
			this.animator.Play(DispensedCoin.disappearId);
		}

		// Token: 0x06002FB2 RID: 12210 RVA: 0x000C2FC7 File Offset: 0x000C13C7
		public void Reappear()
		{
			this.animator.Play(DispensedCoin.reappearId);
		}

		// Token: 0x06002FB3 RID: 12211 RVA: 0x000C2FE0 File Offset: 0x000C13E0
		void IPoolable.SetPool<T>(LocalPool<T> pool)
		{
			this.containerTransform = base.transform.parent;
			this.animator = base.GetComponent<Animator>();
			this.bs = base.GetComponentInChildren<BatchedSprite>(true);
			this.defaultScale = this.bs.transform.localScale;
		}

		// Token: 0x06002FB4 RID: 12212 RVA: 0x000C302D File Offset: 0x000C142D
		void IPoolable.OnRemoved()
		{
			base.gameObject.SetActive(true);
		}

		// Token: 0x06002FB5 RID: 12213 RVA: 0x000C303B File Offset: 0x000C143B
		void IPoolable.OnReturned()
		{
			base.transform.parent = this.containerTransform;
			base.gameObject.SetActive(false);
		}

		// Token: 0x04001FE0 RID: 8160
		private static AnimId disappearId = "Disappear";

		// Token: 0x04001FE1 RID: 8161
		private static AnimId reappearId = "Reappear";

		// Token: 0x04001FE2 RID: 8162
		private static AnimId redId = "Red";

		// Token: 0x04001FE3 RID: 8163
		private Transform containerTransform;

		// Token: 0x04001FE4 RID: 8164
		private Animator animator;

		// Token: 0x04001FE5 RID: 8165
		private BatchedSprite bs;

		// Token: 0x04001FE6 RID: 8166
		private Vector3 defaultScale;
	}
}
