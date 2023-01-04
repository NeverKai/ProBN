using System;
using RTM.UISystem;
using UnityEngine;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x020008E9 RID: 2281
	public class MaskedSpriteClickable : MonoBehaviour
	{
		// Token: 0x06003C80 RID: 15488 RVA: 0x0010DC50 File Offset: 0x0010C050
		private void Awake()
		{
			if (!this.clickable)
			{
				this.clickable = base.GetComponentInParent<UIClickable>();
			}
			if (!this.maskedSpriteAnimated)
			{
				this.maskedSpriteAnimated = base.GetComponentInChildren<MaskedSpriteAnimated>(true);
			}
			if (this.clickable && this.maskedSpriteAnimated)
			{
				this.clickable.onSelectedChanged += delegate(bool selected)
				{
					this.maskedSpriteAnimated.selected = selected;
				};
				this.clickable.onStateChanged += delegate(UIInteractable.State state)
				{
					this.maskedSpriteAnimated.state = state;
				};
				this.maskedSpriteAnimated.selected = this.clickable.selected;
				this.maskedSpriteAnimated.state = this.clickable.state;
			}
		}

		// Token: 0x04002A26 RID: 10790
		[SerializeField]
		private UIClickable clickable;

		// Token: 0x04002A27 RID: 10791
		[SerializeField]
		private MaskedSpriteAnimated maskedSpriteAnimated;
	}
}
