using System;
using I2.Loc;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Voxels.TowerDefense.UI.MetaInventory
{
	// Token: 0x020008ED RID: 2285
	[AddComponentMenu("Meta Inventory - Inventory Group")]
	internal class InventoryGroup : UIBehaviour
	{
		// Token: 0x06003C95 RID: 15509 RVA: 0x0010E1AB File Offset: 0x0010C5AB
		public void Init()
		{
			this.titleLocalize = base.GetComponentInChildren<Localize>();
		}

		// Token: 0x06003C96 RID: 15510 RVA: 0x0010E1B9 File Offset: 0x0010C5B9
		protected override void OnDisable()
		{
			base.OnDisable();
		}

		// Token: 0x06003C97 RID: 15511 RVA: 0x0010E1C4 File Offset: 0x0010C5C4
		protected override void OnRectTransformDimensionsChange()
		{
			base.OnRectTransformDimensionsChange();
			float width = ((RectTransform)base.transform).rect.width;
			float num = (float)(this.gridLayout.padding.left + this.gridLayout.padding.right);
			float x = this.gridLayout.cellSize.x;
			int num2 = Mathf.FloorToInt((width - num + this.minHorizontalSpacing) / (x + this.minHorizontalSpacing));
			float value = Mathf.Floor((width - num - (float)num2 * x) / (float)(num2 - 1));
			this.gridLayout.spacing = this.gridLayout.spacing.SetX(value);
		}

		// Token: 0x04002A3E RID: 10814
		public GridLayoutGroup gridLayout;

		// Token: 0x04002A3F RID: 10815
		public Localize titleLocalize;

		// Token: 0x04002A40 RID: 10816
		public float minHorizontalSpacing = 10f;
	}
}
