using System;
using I2.Loc;
using RTM.Pools;
using UnityEngine;
using Voxels.TowerDefense.CoinDispensing;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x02000549 RID: 1353
	public class EndLevelHeroPerformanceUI : MonoBehaviour, IPoolable
	{
		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x0600232F RID: 9007 RVA: 0x0006A5D8 File Offset: 0x000689D8
		public bool canReceiveCoins
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002330 RID: 9008 RVA: 0x0006A5DC File Offset: 0x000689DC
		public void Setup(CoinDispenser coinDispenser, HeroDefinition heroDef, bool dead, bool evac, bool recruited)
		{
			using ("EndLevelHeroPerformanceUI.Setup()")
			{
				this.heroDef = heroDef;
				this.heroPortraitImage.Set(heroDef.graphics);
				this.heroNameText.Term = heroDef.nameTerm;
				this.bannerPolgon.Setup(heroDef, false);
				string text = null;
				if (dead || evac)
				{
					this.deadOrFled.gameObject.SetActive(true);
					if (dead)
					{
						text = "UI/GAMEPLAY/RESULTS/DEAD";
					}
					else if (evac)
					{
						text = "UI/GAMEPLAY/RESULTS/FLED";
					}
				}
				else
				{
					this.deadOrFled.gameObject.SetActive(false);
				}
				this._canReceiveCoins = (!dead && !evac);
				this.extraInfoText.Term = text;
				this.extraInfoText.gameObject.SetActive(!string.IsNullOrEmpty(text));
				base.gameObject.SetActive(true);
			}
		}

		// Token: 0x06002331 RID: 9009 RVA: 0x0006A6E8 File Offset: 0x00068AE8
		void IPoolable.SetPool<T>(LocalPool<T> pool)
		{
		}

		// Token: 0x06002332 RID: 9010 RVA: 0x0006A6EA File Offset: 0x00068AEA
		void IPoolable.OnRemoved()
		{
			base.transform.SetAsLastSibling();
		}

		// Token: 0x06002333 RID: 9011 RVA: 0x0006A6F7 File Offset: 0x00068AF7
		void IPoolable.OnReturned()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x040015BD RID: 5565
		[SerializeField]
		private MaskedSprite heroPortraitImage;

		// Token: 0x040015BE RID: 5566
		[SerializeField]
		private Localize heroNameText;

		// Token: 0x040015BF RID: 5567
		[SerializeField]
		private Localize extraInfoText;

		// Token: 0x040015C0 RID: 5568
		[SerializeField]
		private BannerPolygon bannerPolgon;

		// Token: 0x040015C1 RID: 5569
		[SerializeField]
		private GameObject deadOrFled;

		// Token: 0x040015C2 RID: 5570
		private HeroDefinition heroDef;

		// Token: 0x040015C3 RID: 5571
		private bool _canReceiveCoins;
	}
}
