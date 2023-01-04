using System;
using System.Collections.Generic;
using RTM.Pools;
using UnityEngine;

namespace Voxels.TowerDefense.CampaignGeneration.Triangulation
{
	// Token: 0x0200071A RID: 1818
	public class PoolableListGrpahic : MonoBehaviour, IPoolable
	{
		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06002F2E RID: 12078 RVA: 0x000BD004 File Offset: 0x000BB404
		// (set) Token: 0x06002F2F RID: 12079 RVA: 0x000BD00C File Offset: 0x000BB40C
		public bool on
		{
			get
			{
				return this._on;
			}
			set
			{
				if (this._on == value)
				{
					return;
				}
				this._on = value;
				base.enabled = true;
			}
		}

		// Token: 0x06002F30 RID: 12080 RVA: 0x000BD02C File Offset: 0x000BB42C
		private void Update()
		{
			float t = Mathf.Min(Time.deltaTime * 20f, 0.3f);
			if (this._on)
			{
				this.canvasGroup.alpha = Mathf.Lerp(this.canvasGroup.alpha, 1f, t);
				if (this.canvasGroup.alpha > 0.99f)
				{
					this.canvasGroup.alpha = 1f;
					base.enabled = false;
				}
			}
			else
			{
				this.canvasGroup.alpha = Mathf.Lerp(this.canvasGroup.alpha, 0f, t);
				if (this.canvasGroup.alpha < 0.01f)
				{
					this.canvasGroup.alpha = 0f;
					this.pool.ReturnToPool(this);
				}
			}
		}

		// Token: 0x06002F31 RID: 12081 RVA: 0x000BD100 File Offset: 0x000BB500
		public static PoolableListGrpahic Create(Transform parent)
		{
			PoolableListGrpahic poolableListGrpahic = parent.gameObject.AddEmptyChild("PoolableListGraphic").AddComponent<PoolableListGrpahic>();
			poolableListGrpahic.canvasGroup = poolableListGrpahic.gameObject.AddComponent<CanvasGroup>();
			return poolableListGrpahic;
		}

		// Token: 0x06002F32 RID: 12082 RVA: 0x000BD135 File Offset: 0x000BB535
		void IPoolable.OnRemoved()
		{
			base.gameObject.SetActive(true);
		}

		// Token: 0x06002F33 RID: 12083 RVA: 0x000BD143 File Offset: 0x000BB543
		void IPoolable.OnReturned()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x06002F34 RID: 12084 RVA: 0x000BD151 File Offset: 0x000BB551
		void IPoolable.SetPool<T>(LocalPool<T> pool)
		{
			this.pool = (pool as LocalPool<PoolableListGrpahic>);
		}

		// Token: 0x04001F64 RID: 8036
		public CanvasGroup canvasGroup;

		// Token: 0x04001F65 RID: 8037
		public Dictionary<Material, UIVertexListGraphic> graphics = new Dictionary<Material, UIVertexListGraphic>();

		// Token: 0x04001F66 RID: 8038
		private LocalPool<PoolableListGrpahic> pool;

		// Token: 0x04001F67 RID: 8039
		private bool _on = true;
	}
}
