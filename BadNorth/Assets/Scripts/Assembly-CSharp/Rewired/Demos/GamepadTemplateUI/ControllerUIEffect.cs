using System;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.Demos.GamepadTemplateUI
{
	// Token: 0x0200047F RID: 1151
	[RequireComponent(typeof(Image))]
	public class ControllerUIEffect : MonoBehaviour
	{
		// Token: 0x06001A6D RID: 6765 RVA: 0x00047E19 File Offset: 0x00046219
		private void Awake()
		{
			this._image = base.GetComponent<Image>();
			this._origColor = this._image.color;
			this._color = this._origColor;
		}

		// Token: 0x06001A6E RID: 6766 RVA: 0x00047E44 File Offset: 0x00046244
		public void Activate(float amount)
		{
			amount = Mathf.Clamp01(amount);
			if (this._isActive && amount == this._highlightAmount)
			{
				return;
			}
			this._highlightAmount = amount;
			this._color = Color.Lerp(this._origColor, this._highlightColor, this._highlightAmount);
			this._isActive = true;
			this.RedrawImage();
		}

		// Token: 0x06001A6F RID: 6767 RVA: 0x00047EA2 File Offset: 0x000462A2
		public void Deactivate()
		{
			if (!this._isActive)
			{
				return;
			}
			this._color = this._origColor;
			this._highlightAmount = 0f;
			this._isActive = false;
			this.RedrawImage();
		}

		// Token: 0x06001A70 RID: 6768 RVA: 0x00047ED4 File Offset: 0x000462D4
		private void RedrawImage()
		{
			this._image.color = this._color;
			this._image.enabled = this._isActive;
		}

		// Token: 0x04001063 RID: 4195
		[SerializeField]
		private Color _highlightColor = Color.white;

		// Token: 0x04001064 RID: 4196
		private Image _image;

		// Token: 0x04001065 RID: 4197
		private Color _color;

		// Token: 0x04001066 RID: 4198
		private Color _origColor;

		// Token: 0x04001067 RID: 4199
		private bool _isActive;

		// Token: 0x04001068 RID: 4200
		private float _highlightAmount;
	}
}
