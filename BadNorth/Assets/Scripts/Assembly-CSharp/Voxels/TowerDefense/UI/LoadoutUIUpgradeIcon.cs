using System;
using RTM.Pools;
using UnityEngine;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x020008DE RID: 2270
	public class LoadoutUIUpgradeIcon : MonoBehaviour, IPoolable
	{
		// Token: 0x06003C35 RID: 15413 RVA: 0x0010C280 File Offset: 0x0010A680
		public void Setup(HeroUpgradeDefinition definition, int level)
		{
			if (!this.maskedSprite)
			{
				this.maskedSprite = base.GetComponent<MaskedSprite>();
			}
			this.maskedSprite.Set(definition, level);
		}

		// Token: 0x06003C36 RID: 15414 RVA: 0x0010C2AB File Offset: 0x0010A6AB
		void IPoolable.OnRemoved()
		{
			base.gameObject.SetActive(true);
		}

		// Token: 0x06003C37 RID: 15415 RVA: 0x0010C2B9 File Offset: 0x0010A6B9
		void IPoolable.OnReturned()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x06003C38 RID: 15416 RVA: 0x0010C2C7 File Offset: 0x0010A6C7
		void IPoolable.SetPool<T>(LocalPool<T> pool)
		{
		}

		// Token: 0x040029F3 RID: 10739
		[SerializeField]
		private MaskedSprite maskedSprite;
	}
}
