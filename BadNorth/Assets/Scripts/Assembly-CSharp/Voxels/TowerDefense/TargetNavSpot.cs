using System;
using RTM.Pools;
using RTM.Utilities;

namespace Voxels.TowerDefense
{
	// Token: 0x020008A1 RID: 2209
	public class TargetNavSpot : TargetMesh
	{
		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x060039C5 RID: 14789 RVA: 0x000FCB29 File Offset: 0x000FAF29
		public NavSpot navSpot
		{
			get
			{
				return this._navSpot;
			}
		}

		// Token: 0x060039C6 RID: 14790 RVA: 0x000FCB36 File Offset: 0x000FAF36
		protected override void Init()
		{
			base.Init();
			this.navSpotComponents = base.GetComponentsInChildren<TargetNavSpot.INavSpotComponent>(true);
		}

		// Token: 0x060039C7 RID: 14791 RVA: 0x000FCB4B File Offset: 0x000FAF4B
		protected override void SetPool_Internal<T>(LocalPool<T> pool)
		{
			this.pool = (pool as LocalPool<TargetNavSpot>);
		}

		// Token: 0x060039C8 RID: 14792 RVA: 0x000FCB5E File Offset: 0x000FAF5E
		protected override void ReturnToPool_Internal()
		{
			this.pool.ReturnToPool(this);
		}

		// Token: 0x060039C9 RID: 14793 RVA: 0x000FCB6C File Offset: 0x000FAF6C
		public void Setup(NavSpot referenceNavSpot)
		{
			if (referenceNavSpot)
			{
				base.Setup(referenceNavSpot.mesh, referenceNavSpot.transform);
			}
			else
			{
				base.Setup(null, null);
			}
			using (new ScopedProfiler("SetupTargetNavSpot", null))
			{
				this._navSpot.Target = referenceNavSpot;
				foreach (TargetNavSpot.INavSpotComponent navSpotComponent in this.navSpotComponents)
				{
					navSpotComponent.SetNavSpot(this._navSpot);
				}
			}
		}

		// Token: 0x040027D6 RID: 10198
		private TargetNavSpot.INavSpotComponent[] navSpotComponents;

		// Token: 0x040027D7 RID: 10199
		private LocalPool<TargetNavSpot> pool;

		// Token: 0x040027D8 RID: 10200
		private WeakReference<NavSpot> _navSpot = new WeakReference<NavSpot>(null);

		// Token: 0x020008A2 RID: 2210
		public interface INavSpotComponent : TargetMesh.IMeshComponent
		{
			// Token: 0x060039CA RID: 14794
			void SetNavSpot(NavSpot newNavSpot);
		}
	}
}
