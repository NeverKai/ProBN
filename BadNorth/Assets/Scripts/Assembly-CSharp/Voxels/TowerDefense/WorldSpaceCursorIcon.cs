using System;
using RTM.Pools;
using RTM.Utilities;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x0200093A RID: 2362
	public class WorldSpaceCursorIcon : MonoBehaviour, IPoolable
	{
		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x06003FD3 RID: 16339 RVA: 0x00121E22 File Offset: 0x00120222
		// (set) Token: 0x06003FD4 RID: 16340 RVA: 0x00121E2C File Offset: 0x0012022C
		private float alpha
		{
			get
			{
				return this._alpha;
			}
			set
			{
				this._alpha = value;
				this.shadowRenderer.color = this.shadowRenderer.color.SetA(value * 0.25f);
				this.baseScale = Mathf.Lerp(0f, 1f, this.alpha);
				this.shadow.localScale = this.shadowScale * this.baseScale;
			}
		}

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x06003FD5 RID: 16341 RVA: 0x00121E99 File Offset: 0x00120299
		public bool isNew
		{
			get
			{
				return this.spawnTime.isThisFrame;
			}
		}

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x06003FD6 RID: 16342 RVA: 0x00121EA6 File Offset: 0x001202A6
		public bool isJustDeactivated
		{
			get
			{
				return this.deactivateTime.isThisFrame;
			}
		}

		// Token: 0x06003FD7 RID: 16343 RVA: 0x00121EB3 File Offset: 0x001202B3
		public void UpdateFloorPos(Vector3 floorPos, bool snap = false)
		{
			this.SetTargetPos(floorPos + Vector3.up * 0.85f, false);
		}

		// Token: 0x06003FD8 RID: 16344 RVA: 0x00121ED1 File Offset: 0x001202D1
		public void SetTargetPos(Vector3 position, bool snap = false)
		{
			this.targetPos = position;
			if (snap)
			{
				this.SetCurrentPos(this.targetPos);
			}
		}

		// Token: 0x06003FD9 RID: 16345 RVA: 0x00121EEC File Offset: 0x001202EC
		public void SetCurrentPos(Vector3 position)
		{
			this.lazyPos = position;
			this.lazyY1 = position.y;
			this.lazyY0 = position.y;
		}

		// Token: 0x06003FDA RID: 16346 RVA: 0x00121F0F File Offset: 0x0012030F
		public void Deactivate()
		{
			this.visible = false;
			this.deactivateTime = FrameTimeStamp.now;
		}

		// Token: 0x06003FDB RID: 16347 RVA: 0x00121F23 File Offset: 0x00120323
		public void Reactivate()
		{
			this.deactivateTime = this.spawnTime;
			this.visible = true;
		}

		// Token: 0x06003FDC RID: 16348 RVA: 0x00121F38 File Offset: 0x00120338
		private void LateUpdate()
		{
			this.UpdateAlpha();
			this.UpdatePosition();
			this.UpdateShadow();
		}

		// Token: 0x06003FDD RID: 16349 RVA: 0x00121F4C File Offset: 0x0012034C
		private void UpdatePosition()
		{
			float num = this.targetPos.y - base.transform.position.y;
			float t = num / 1.5f;
			float num2 = Mathf.Lerp(18f, 3f, t);
			float num3 = Mathf.Lerp(16f, 22f, t);
			this.lazyPos = Vector3.Lerp(this.lazyPos, this.targetPos, Time.unscaledDeltaTime * num2);
			this.lazyY0 = Mathf.Lerp(this.lazyY0, this.targetPos.y, Time.unscaledDeltaTime * num3);
			this.lazyY1 = Mathf.Lerp(this.lazyY1, this.lazyY0, Time.unscaledDeltaTime * num3);
			Vector3 position = this.lazyPos;
			position.y = (this.lazyY0 + this.lazyY1) / 2f;
			base.transform.position = position;
			Vector3 vector = Vector3.one * this.baseScale;
			float num4 = Mathf.Abs(this.lazyY0 - this.lazyY1);
			float num5 = 1f / (1f + Mathf.Min(num4, 0.1f));
			vector *= 1f + Mathf.Sin(Time.unscaledTime * 3.1415927f * 4f) * 0.05f;
			vector.y += num4;
			vector.x *= num5;
			vector.z *= num5;
			this.icon.transform.localScale = vector;
		}

		// Token: 0x06003FDE RID: 16350 RVA: 0x001220E4 File Offset: 0x001204E4
		private void UpdateShadow()
		{
			Ray ray = new Ray(base.transform.position, Vector3.down);
			Vector3 position = base.transform.position;
			RaycastHit raycastHit;
			if (Physics.SphereCast(ray, 0.075f, out raycastHit, 10f, LayerMaster.cursorShadowMask))
			{
				position.y -= Mathf.Min(raycastHit.distance, position.y);
			}
			else
			{
				position.y = 0f;
			}
			this.shadow.position = position;
		}

		// Token: 0x06003FDF RID: 16351 RVA: 0x00122174 File Offset: 0x00120574
		private void UpdateAlpha()
		{
			float target = (!this.visible) ? 0f : 1f;
			float num = (!this.visible) ? 9.5f : 5f;
			this.alpha = Mathf.MoveTowards(this.alpha, target, Time.unscaledDeltaTime * num);
			if (this.alpha == 0f)
			{
				this.pool.ReturnToPool(this);
			}
		}

		// Token: 0x06003FE0 RID: 16352 RVA: 0x001221EC File Offset: 0x001205EC
		void IPoolable.SetPool<T>(LocalPool<T> pool)
		{
			this.pool = (pool as LocalPool<WorldSpaceCursorIcon>);
			this.shadowRenderer = this.shadow.GetComponent<SpriteRenderer>();
			this.shadowScale = this.shadow.localScale;
			this.alpha = 0f;
			this.visible = false;
			base.gameObject.SetActive(false);
		}

		// Token: 0x06003FE1 RID: 16353 RVA: 0x0012224A File Offset: 0x0012064A
		void IPoolable.OnRemoved()
		{
			base.gameObject.SetActive(true);
			this.alpha = 0f;
			this.visible = true;
			this.spawnTime = FrameTimeStamp.now;
		}

		// Token: 0x06003FE2 RID: 16354 RVA: 0x00122275 File Offset: 0x00120675
		void IPoolable.OnReturned()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x06003FE3 RID: 16355 RVA: 0x00122283 File Offset: 0x00120683
		private void OnDisable()
		{
			if (this.pool != null && this.pool.inUse.Contains(this))
			{
				Debug.LogError("disabling icon without returning to pool!");
			}
		}

		// Token: 0x04002CC1 RID: 11457
		[SerializeField]
		private Transform shadow;

		// Token: 0x04002CC2 RID: 11458
		[SerializeField]
		private Transform icon;

		// Token: 0x04002CC3 RID: 11459
		[SerializeField]
		private SpriteRenderer spriteRenderer;

		// Token: 0x04002CC4 RID: 11460
		private SpriteRenderer shadowRenderer;

		// Token: 0x04002CC5 RID: 11461
		private LocalPool<WorldSpaceCursorIcon> pool;

		// Token: 0x04002CC6 RID: 11462
		private const float hoverHeight = 0.85f;

		// Token: 0x04002CC7 RID: 11463
		private const float shadowHover = 0f;

		// Token: 0x04002CC8 RID: 11464
		private Vector3 targetPos = Vector3.zero;

		// Token: 0x04002CC9 RID: 11465
		private Vector3 lazyPos;

		// Token: 0x04002CCA RID: 11466
		private float lazyY0;

		// Token: 0x04002CCB RID: 11467
		private float lazyY1;

		// Token: 0x04002CCC RID: 11468
		private bool visible;

		// Token: 0x04002CCD RID: 11469
		private float baseScale = 1f;

		// Token: 0x04002CCE RID: 11470
		private FrameTimeStamp spawnTime;

		// Token: 0x04002CCF RID: 11471
		private FrameTimeStamp deactivateTime;

		// Token: 0x04002CD0 RID: 11472
		private Vector3 shadowScale = Vector3.one;

		// Token: 0x04002CD1 RID: 11473
		private float _alpha;
	}
}
