using System;
using UnityEngine;
using Voxels.TowerDefense.UI;

namespace Voxels.TowerDefense
{
	// Token: 0x0200070C RID: 1804
	public class LevelNodeEnemy : ChildComponent<LevelNode>
	{
		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06002ED0 RID: 11984 RVA: 0x000B6D78 File Offset: 0x000B5178
		public float scale
		{
			get
			{
				return this.vikingReference.agent.transform.localScale.x;
			}
		}

		// Token: 0x06002ED1 RID: 11985 RVA: 0x000B6DA4 File Offset: 0x000B51A4
		public LevelNodeEnemy Setup(VikingReference vikingReference)
		{
			this.vikingReference = vikingReference;
			this.width = base.GetComponent<RectTransform>().sizeDelta.x * this.scale;
			return this;
		}

		// Token: 0x06002ED2 RID: 11986 RVA: 0x000B6DDC File Offset: 0x000B51DC
		public bool UpdateVisual()
		{
			if (!this.seen && this.vikingReference.seen)
			{
				HeadshotImage component = base.GetComponent<HeadshotImage>();
				component.sprite = this.vikingReference.sprite2;
				component.rectTransform.sizeDelta = component.rectTransform.sizeDelta * this.scale;
				this.seen = true;
				return true;
			}
			return false;
		}

		// Token: 0x04001EE5 RID: 7909
		private VikingReference vikingReference;

		// Token: 0x04001EE6 RID: 7910
		private bool seen;

		// Token: 0x04001EE7 RID: 7911
		[NonSerialized]
		public float width;
	}
}
