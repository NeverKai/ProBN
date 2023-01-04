using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Voxels.TowerDefense.UI.GridScrolling
{
	// Token: 0x02000923 RID: 2339
	public class ScrollGridPopulator : UIBehaviour
	{
		// Token: 0x06003EF3 RID: 16115 RVA: 0x0011C024 File Offset: 0x0011A424
		protected override void Start()
		{
			base.Start();
			ScrollGridItem componentInChildren = base.GetComponentInChildren<ScrollGridItem>();
			ScrollGrid[] array = UnityEngine.Object.FindObjectsOfType<ScrollGrid>();
			foreach (ScrollGrid scrollGrid in array)
			{
				int j = 0;
				int num = UnityEngine.Random.Range(2, 12);
				while (j < num)
				{
					ScrollGridItem scrollGridItem = UnityEngine.Object.Instantiate<ScrollGridItem>(componentInChildren, componentInChildren.transform.parent);
					Image componentInChildren2 = scrollGridItem.GetComponentInChildren<Image>();
					componentInChildren2.color = Color.HSVToRGB((float)j / 10f, 0.5f, 1f);
					scrollGrid.AddItem(scrollGridItem);
					j++;
				}
			}
			componentInChildren.gameObject.SetActive(false);
		}
	}
}
