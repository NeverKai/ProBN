using System;
using I2.Loc;
using RTM.Pools;
using UnityEngine;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x02000547 RID: 1351
	public class EndLevelHeroIcon : MonoBehaviour, IPoolable
	{
		// Token: 0x06002324 RID: 8996 RVA: 0x00069FA9 File Offset: 0x000683A9
		void IPoolable.OnRemoved()
		{
			base.gameObject.SetActive(true);
		}

		// Token: 0x06002325 RID: 8997 RVA: 0x00069FB7 File Offset: 0x000683B7
		void IPoolable.OnReturned()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x06002326 RID: 8998 RVA: 0x00069FC5 File Offset: 0x000683C5
		void IPoolable.SetPool<T>(LocalPool<T> pool)
		{
		}

		// Token: 0x06002327 RID: 8999 RVA: 0x00069FC7 File Offset: 0x000683C7
		public void RemoveBorder()
		{
		}

		// Token: 0x040015B1 RID: 5553
		[SerializeField]
		public MaskedSprite maskedSprite;

		// Token: 0x040015B2 RID: 5554
		[SerializeField]
		public Localize nameText;

		// Token: 0x040015B3 RID: 5555
		[SerializeField]
		public GameObject dead;

		// Token: 0x040015B4 RID: 5556
		[SerializeField]
		public GameObject fled;

		// Token: 0x040015B5 RID: 5557
		[SerializeField]
		public GameObject recruited;

		// Token: 0x040015B6 RID: 5558
		[SerializeField]
		public GameObject blood;
	}
}
