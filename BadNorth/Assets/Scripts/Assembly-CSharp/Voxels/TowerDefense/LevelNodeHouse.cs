using System;
using RTM.Pools;
using UnityEngine;
using UnityEngine.UI;
using Voxels.TowerDefense.UI;

namespace Voxels.TowerDefense
{
	// Token: 0x0200070D RID: 1805
	public class LevelNodeHouse : ChildComponent<LevelNode>, IPoolable
	{
		// Token: 0x06002ED4 RID: 11988 RVA: 0x000B6E50 File Offset: 0x000B5250
		public LevelNodeHouse Setup(LevelNode levelNode, HouseState houseState)
		{
			RectTransform rectTransform = base.transform as RectTransform;
			rectTransform.sizeDelta = (houseState.rect.size * 1.5f + Vector2.one * 0.6f) * 10f;
			base.transform.localPosition = (houseState.rect.center * 10f).SetZ(base.transform.localPosition.z);
			base.gameObject.SetActive(true);
			return this;
		}

		// Token: 0x06002ED5 RID: 11989 RVA: 0x000B6EF0 File Offset: 0x000B52F0
		public void SetColor(Color color, bool shadow, float fill)
		{
			Image component = base.GetComponent<Image>();
			component.color = color.SetA(1f);
			base.GetComponent<Image>().color = color;
			base.GetComponent<DistanceFieldSettings>().fill = fill;
			base.GetComponent<Shadow>().SetEnabled(shadow);
		}

		// Token: 0x06002ED6 RID: 11990 RVA: 0x000B6F39 File Offset: 0x000B5339
		void IPoolable.SetPool<T>(LocalPool<T> pool)
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x06002ED7 RID: 11991 RVA: 0x000B6F47 File Offset: 0x000B5347
		void IPoolable.OnRemoved()
		{
			base.gameObject.SetActive(true);
		}

		// Token: 0x06002ED8 RID: 11992 RVA: 0x000B6F55 File Offset: 0x000B5355
		void IPoolable.OnReturned()
		{
			base.gameObject.SetActive(false);
		}
	}
}
