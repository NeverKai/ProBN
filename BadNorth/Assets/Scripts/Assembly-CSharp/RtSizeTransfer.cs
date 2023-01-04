using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x0200092C RID: 2348
public class RtSizeTransfer : UIBehaviour
{
	// Token: 0x06003F10 RID: 16144 RVA: 0x0011C88C File Offset: 0x0011AC8C
	[ContextMenu("Transfer Size")]
	private void TransferSize()
	{
		RectTransform rectTransform = base.transform.parent as RectTransform;
		if (!rectTransform)
		{
			return;
		}
		Rect rect = rectTransform.rect;
		RectTransform rectTransform2 = base.transform as RectTransform;
		rectTransform2.anchorMax = new Vector2((!this.horizontal) ? rectTransform2.anchorMax.x : rectTransform2.anchorMin.x, (!this.vertical) ? rectTransform2.anchorMax.y : rectTransform2.anchorMin.y);
		if (this.horizontal)
		{
			rectTransform2.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rect.width);
		}
		if (this.vertical)
		{
			rectTransform2.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, rect.height);
		}
	}

	// Token: 0x06003F11 RID: 16145 RVA: 0x0011C961 File Offset: 0x0011AD61
	protected override void OnRectTransformDimensionsChange()
	{
		base.OnRectTransformDimensionsChange();
		this.TransferSize();
	}

	// Token: 0x06003F12 RID: 16146 RVA: 0x0011C96F File Offset: 0x0011AD6F
	protected override void OnEnable()
	{
		base.OnEnable();
		this.TransferSize();
	}

	// Token: 0x04002C1C RID: 11292
	[SerializeField]
	private bool horizontal;

	// Token: 0x04002C1D RID: 11293
	[SerializeField]
	private bool vertical;
}
