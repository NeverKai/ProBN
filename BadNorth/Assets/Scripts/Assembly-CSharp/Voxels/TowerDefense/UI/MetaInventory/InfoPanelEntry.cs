using RTM.Pools;
using UnityEngine;

namespace Voxels.TowerDefense.UI.MetaInventory
{
	// Token: 0x020008EC RID: 2284
	internal abstract class InfoPanelEntry<T> : MonoBehaviour, IPoolable where T : InfoPanelEntry<T>
	{
		// Token: 0x06003C91 RID: 15505 RVA: 0x0010E169 File Offset: 0x0010C569
		void IPoolable.SetPool<TPool>(LocalPool<TPool> pool)
		{
			this.pool = (pool as LocalPool<T>);
		}

		// Token: 0x06003C92 RID: 15506 RVA: 0x0010E17C File Offset: 0x0010C57C
		void IPoolable.OnRemoved()
		{
			base.gameObject.SetActive(true);
		}

		// Token: 0x06003C93 RID: 15507 RVA: 0x0010E18A File Offset: 0x0010C58A
		void IPoolable.OnReturned()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x04002A3D RID: 10813
		private LocalPool<T> pool;
	}
}
