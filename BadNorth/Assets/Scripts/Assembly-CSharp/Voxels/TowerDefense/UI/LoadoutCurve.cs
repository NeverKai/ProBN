using System;
using RTM.Pools;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x020008D9 RID: 2265
	public class LoadoutCurve : UIBehaviour, IPoolable
	{
		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x06003BDF RID: 15327 RVA: 0x0010A472 File Offset: 0x00108872
		// (set) Token: 0x06003BE0 RID: 15328 RVA: 0x0010A47A File Offset: 0x0010887A
		public LoadoutUIListBanner rootBanner { get; private set; }

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x06003BE1 RID: 15329 RVA: 0x0010A483 File Offset: 0x00108883
		// (set) Token: 0x06003BE2 RID: 15330 RVA: 0x0010A48B File Offset: 0x0010888B
		public LoadoutUIListBanner targetBanner { get; private set; }

		// Token: 0x06003BE3 RID: 15331 RVA: 0x0010A494 File Offset: 0x00108894
		public LoadoutCurve Setup(LoadoutUIListBanner rootBanner, LoadoutUIListBanner targetBanner)
		{
			this.rootBanner = rootBanner;
			this.targetBanner = targetBanner;
			this.SetDirty();
			this.SetAlpha(0f);
			return this;
		}

		// Token: 0x06003BE4 RID: 15332 RVA: 0x0010A4B6 File Offset: 0x001088B6
		private void OnTokensReturn()
		{
			this.pool.ReturnToPool(this);
		}

		// Token: 0x06003BE5 RID: 15333 RVA: 0x0010A4C4 File Offset: 0x001088C4
		public void SetDirty()
		{
			Vector3 position = this.rootBanner.curveConnectorRight.position;
			Vector3 a = this.targetBanner.curveConnectorLeft.TransformPoint(this.targetBanner.curveConnectorLeft.rect.center);
			Vector3 vector = position.SetX((position.x + a.x) / 2f);
			Vector2 v = vector - position;
			Vector2 v2 = a - vector;
			this.diffBefore = v;
			this.rootPos = position;
			this.targetPos = a;
			v = this.rootBanner.transform.InverseTransformVector(v);
			v2 = this.rootBanner.transform.InverseTransformVector(v2);
			this.diffAfter = v2;
			this.lossyScale = this.rootBanner.transform.lossyScale;
			float num = v2.y * 0.04f;
			Vector2 tangent = new Vector2(v2.x * 0.3f, 0f);
			this.simpleCurve.points.Clear();
			this.simpleCurve.points.Add(new SimpleCurve.Point(new Vector2(0.5f, 0.5f), new Vector2(0f, num), tangent, this.rootBanner.curveConnectorRight));
			this.simpleCurve.points.Add(new SimpleCurve.Point(new Vector2(0.5f, 0.5f), new Vector2(v.x, num), tangent, this.rootBanner.curveConnectorRight));
			this.simpleCurve.points.Add(new SimpleCurve.Point(new Vector2(0.2f, 0.5f), new Vector2(0f, -num), tangent, this.targetBanner.curveConnectorLeft));
			this.simpleCurve.SetVerticesDirty();
		}

		// Token: 0x06003BE6 RID: 15334 RVA: 0x0010A6BF File Offset: 0x00108ABF
		private void SetAlpha(float x = 0f)
		{
			this.simpleCurve.color = this.simpleCurve.color.SetA(1f);
		}

		// Token: 0x06003BE7 RID: 15335 RVA: 0x0010A6E4 File Offset: 0x00108AE4
		public void UpdateAlpha(Rect containerRect)
		{
			Rect worldSpaceRect = this.targetBanner.curveConnectorLeft.GetWorldSpaceRect();
			float num = (Mathf.Max(containerRect.yMin - worldSpaceRect.yMin, 0f) + Mathf.Max(worldSpaceRect.yMax - containerRect.yMax, 0f)) / worldSpaceRect.height;
			num *= 1.15f;
			this.simpleCurve.color = this.simpleCurve.color.SetA(1f - num);
		}

		// Token: 0x06003BE8 RID: 15336 RVA: 0x0010A768 File Offset: 0x00108B68
		void IPoolable.SetPool<T>(LocalPool<T> pool)
		{
			this.pool = (pool as LocalPool<LoadoutCurve>);
			this.simpleCurve = base.GetComponent<SimpleCurve>();
		}

		// Token: 0x06003BE9 RID: 15337 RVA: 0x0010A787 File Offset: 0x00108B87
		void IPoolable.OnRemoved()
		{
			base.gameObject.SetActive(true);
		}

		// Token: 0x06003BEA RID: 15338 RVA: 0x0010A795 File Offset: 0x00108B95
		void IPoolable.OnReturned()
		{
			this.rootBanner = null;
			this.targetBanner = null;
			base.gameObject.SetActive(false);
		}

		// Token: 0x040029AC RID: 10668
		[SerializeField]
		private SimpleCurve simpleCurve;

		// Token: 0x040029AD RID: 10669
		private LocalPool<LoadoutCurve> pool;

		// Token: 0x040029AE RID: 10670
		[SerializeField]
		private Vector3 diffBefore;

		// Token: 0x040029AF RID: 10671
		[SerializeField]
		private Vector3 diffAfter;

		// Token: 0x040029B0 RID: 10672
		[SerializeField]
		private Vector3 lossyScale;

		// Token: 0x040029B1 RID: 10673
		[SerializeField]
		private Vector3 rootPos;

		// Token: 0x040029B2 RID: 10674
		[SerializeField]
		private Vector3 targetPos;
	}
}
