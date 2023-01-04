using System;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.Demos.GamepadTemplateUI
{
	// Token: 0x02000480 RID: 1152
	[RequireComponent(typeof(Image))]
	public class ControllerUIElement : MonoBehaviour
	{
		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06001A72 RID: 6770 RVA: 0x00047F17 File Offset: 0x00046317
		private bool hasEffects
		{
			get
			{
				return this._positiveUIEffect != null || this._negativeUIEffect != null;
			}
		}

		// Token: 0x06001A73 RID: 6771 RVA: 0x00047F39 File Offset: 0x00046339
		private void Awake()
		{
			this._image = base.GetComponent<Image>();
			this._origColor = this._image.color;
			this._color = this._origColor;
			this.ClearLabels();
		}

		// Token: 0x06001A74 RID: 6772 RVA: 0x00047F6C File Offset: 0x0004636C
		public void Activate(float amount)
		{
			amount = Mathf.Clamp(amount, -1f, 1f);
			if (this.hasEffects)
			{
				if (amount < 0f && this._negativeUIEffect != null)
				{
					this._negativeUIEffect.Activate(Mathf.Abs(amount));
				}
				if (amount > 0f && this._positiveUIEffect != null)
				{
					this._positiveUIEffect.Activate(Mathf.Abs(amount));
				}
			}
			else
			{
				if (this._isActive && amount == this._highlightAmount)
				{
					return;
				}
				this._highlightAmount = amount;
				this._color = Color.Lerp(this._origColor, this._highlightColor, this._highlightAmount);
			}
			this._isActive = true;
			this.RedrawImage();
			if (this._childElements.Length != 0)
			{
				for (int i = 0; i < this._childElements.Length; i++)
				{
					if (!(this._childElements[i] == null))
					{
						this._childElements[i].Activate(amount);
					}
				}
			}
		}

		// Token: 0x06001A75 RID: 6773 RVA: 0x0004808C File Offset: 0x0004648C
		public void Deactivate()
		{
			if (!this._isActive)
			{
				return;
			}
			this._color = this._origColor;
			this._highlightAmount = 0f;
			if (this._positiveUIEffect != null)
			{
				this._positiveUIEffect.Deactivate();
			}
			if (this._negativeUIEffect != null)
			{
				this._negativeUIEffect.Deactivate();
			}
			this._isActive = false;
			this.RedrawImage();
			if (this._childElements.Length != 0)
			{
				for (int i = 0; i < this._childElements.Length; i++)
				{
					if (!(this._childElements[i] == null))
					{
						this._childElements[i].Deactivate();
					}
				}
			}
		}

		// Token: 0x06001A76 RID: 6774 RVA: 0x0004814C File Offset: 0x0004654C
		public void SetLabel(string text, AxisRange labelType)
		{
			Text text2;
			switch (labelType)
			{
			case AxisRange.Full:
				text2 = this._label;
				break;
			case AxisRange.Positive:
				text2 = this._positiveLabel;
				break;
			case AxisRange.Negative:
				text2 = this._negativeLabel;
				break;
			default:
				text2 = null;
				break;
			}
			if (text2 != null)
			{
				text2.text = text;
			}
			if (this._childElements.Length != 0)
			{
				for (int i = 0; i < this._childElements.Length; i++)
				{
					if (!(this._childElements[i] == null))
					{
						this._childElements[i].SetLabel(text, labelType);
					}
				}
			}
		}

		// Token: 0x06001A77 RID: 6775 RVA: 0x000481FC File Offset: 0x000465FC
		public void ClearLabels()
		{
			if (this._label != null)
			{
				this._label.text = string.Empty;
			}
			if (this._positiveLabel != null)
			{
				this._positiveLabel.text = string.Empty;
			}
			if (this._negativeLabel != null)
			{
				this._negativeLabel.text = string.Empty;
			}
			if (this._childElements.Length != 0)
			{
				for (int i = 0; i < this._childElements.Length; i++)
				{
					if (!(this._childElements[i] == null))
					{
						this._childElements[i].ClearLabels();
					}
				}
			}
		}

		// Token: 0x06001A78 RID: 6776 RVA: 0x000482B7 File Offset: 0x000466B7
		private void RedrawImage()
		{
			this._image.color = this._color;
		}

		// Token: 0x04001069 RID: 4201
		[SerializeField]
		private Color _highlightColor = Color.white;

		// Token: 0x0400106A RID: 4202
		[SerializeField]
		private ControllerUIEffect _positiveUIEffect;

		// Token: 0x0400106B RID: 4203
		[SerializeField]
		private ControllerUIEffect _negativeUIEffect;

		// Token: 0x0400106C RID: 4204
		[SerializeField]
		private Text _label;

		// Token: 0x0400106D RID: 4205
		[SerializeField]
		private Text _positiveLabel;

		// Token: 0x0400106E RID: 4206
		[SerializeField]
		private Text _negativeLabel;

		// Token: 0x0400106F RID: 4207
		[SerializeField]
		private ControllerUIElement[] _childElements = new ControllerUIElement[0];

		// Token: 0x04001070 RID: 4208
		private Image _image;

		// Token: 0x04001071 RID: 4209
		private Color _color;

		// Token: 0x04001072 RID: 4210
		private Color _origColor;

		// Token: 0x04001073 RID: 4211
		private bool _isActive;

		// Token: 0x04001074 RID: 4212
		private float _highlightAmount;
	}
}
