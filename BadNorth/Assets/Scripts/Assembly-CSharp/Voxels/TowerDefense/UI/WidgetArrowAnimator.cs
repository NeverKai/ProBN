using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x0200090B RID: 2315
	public class WidgetArrowAnimator : UIBehaviour
	{
		// Token: 0x06003E00 RID: 15872 RVA: 0x00116478 File Offset: 0x00114878
		public void MaybeInitialize()
		{
			if (this.initialized)
			{
				return;
			}
			this.initialized = true;
			DistanceFieldAnimator componentInParentIncludingInactive = base.gameObject.GetComponentInParentIncludingInactive<DistanceFieldAnimator>();
			componentInParentIncludingInactive.MaybeInitialize();
			Graphic componentInChildren = base.GetComponentInChildren<Graphic>(true);
			ColorModifier modifier = componentInChildren.gameObject.GetOrAddComponent<ColorModifier>();
			CanvasGroup canvasGroup = componentInChildren.gameObject.GetOrAddComponent<CanvasGroup>();
			componentInParentIncludingInactive.disabled.anim.Subscribe(delegate(float x)
			{
				modifier.alpha = Mathf.Lerp(0.8f, 0f, x);
				canvasGroup.alpha = Mathf.Lerp(1f, 0.4f, x);
			});
		}

		// Token: 0x06003E01 RID: 15873 RVA: 0x001164F6 File Offset: 0x001148F6
		protected override void Awake()
		{
			base.Awake();
			this.MaybeInitialize();
		}

		// Token: 0x04002B4B RID: 11083
		private bool initialized;
	}
}
