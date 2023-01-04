using System;
using RTM.UISystem;
using RTM.Utilities;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020008C9 RID: 2249
public class IntWidget_NumericDecorator : MonoBehaviour, Widget.IWidgetInit
{
	// Token: 0x17000859 RID: 2137
	// (get) Token: 0x06003B71 RID: 15217 RVA: 0x001082C1 File Offset: 0x001066C1
	private IntStringCache intStringCache
	{
		get
		{
			return (!this.widget.percentDisplay) ? IntStringCache.clean : IntStringCache.percent;
		}
	}

	// Token: 0x06003B72 RID: 15218 RVA: 0x001082E4 File Offset: 0x001066E4
	void Widget.IWidgetInit.OnWidgetInit(Widget x)
	{
		this.numberText.gameObject.SetActive(false);
		Transform parent = this.numberText.transform.parent;
		this.values = new Text[]
		{
			parent.gameObject.InstantiateChild(this.numberText, null),
			parent.gameObject.InstantiateChild(this.numberText, null)
		};
		this.widget.onValueChanged += this.Refresh;
		this.widget.onForceUpdate += this.Refresh;
		this.ForceUpdate(this.widget.GetValue());
	}

	// Token: 0x06003B73 RID: 15219 RVA: 0x00108388 File Offset: 0x00106788
	private void OnDestroy()
	{
		this.widget.onValueChanged -= this.Refresh;
		this.widget.onForceUpdate -= this.Refresh;
	}

	// Token: 0x06003B74 RID: 15220 RVA: 0x001083B8 File Offset: 0x001067B8
	private void Refresh(int newValue)
	{
		this.Updateclickables(newValue);
	}

	// Token: 0x06003B75 RID: 15221 RVA: 0x001083C1 File Offset: 0x001067C1
	private void ForceUpdate(int newValue)
	{
		this.targetIdx = 0;
		this.displayValue = newValue;
		this.previousValue = newValue;
		this.values[0].text = this.intStringCache.Get(newValue);
		this.Updateclickables(newValue);
		this.CompleteAnimation();
	}

	// Token: 0x06003B76 RID: 15222 RVA: 0x00108400 File Offset: 0x00106800
	private void Updateclickables(int value)
	{
		if (this.widget.incrementsCycle)
		{
			UIInteractable uiinteractable = this.decrementClick;
			bool disabled = false;
			this.incrementClick.disabled = disabled;
			uiinteractable.disabled = disabled;
		}
		else
		{
			this.decrementClick.disabled = (value == this.widget.min);
			this.incrementClick.disabled = (value == this.widget.max);
		}
	}

	// Token: 0x06003B77 RID: 15223 RVA: 0x00108470 File Offset: 0x00106870
	private void CompleteAnimation()
	{
		this.transitionAlpha = 1f;
		int num = (this.targetIdx + 1) % 2;
		this.values[this.targetIdx].gameObject.SetActive(true);
		this.values[num].gameObject.SetActive(false);
		this.values[this.targetIdx].rectTransform.pivot = new Vector2(0.5f, 0.5f);
	}

	// Token: 0x06003B78 RID: 15224 RVA: 0x001084E4 File Offset: 0x001068E4
	private void LateUpdate()
	{
		int value = this.widget.GetValue();
		if (this.transitionAlpha >= 1f && this.displayValue != value)
		{
			int num = this.targetIdx;
			this.transitionAlpha = 0f;
			this.previousValue = this.displayValue;
			this.displayValue = value;
			this.targetIdx = (this.targetIdx + 1) % 2;
			this.values[this.targetIdx].text = this.intStringCache.Get(value);
			this.values[this.targetIdx].gameObject.SetActive(true);
			this.values[num].gameObject.SetActive(true);
			this.values[this.targetIdx].rectTransform.pivot = new Vector2(0.5f, 0.5f);
			this.values[num].rectTransform.pivot = new Vector2(0.5f, 0.5f);
		}
		if (this.transitionAlpha < 1f)
		{
			this.transitionAlpha += Mathf.Min(Time.unscaledDeltaTime, 0.025f) * 10f;
			if (this.transitionAlpha >= 1f)
			{
				this.CompleteAnimation();
			}
			else
			{
				int num2 = (this.targetIdx + 1) % 2;
				int num3 = Mathf.Clamp(this.previousValue - this.displayValue, -1, 1);
				this.values[this.targetIdx].color = this.values[this.targetIdx].color.SetA(this.transitionAlpha);
				this.values[num2].color = this.values[num2].color.SetA(1f - this.transitionAlpha * 1.5f);
				if (this.horizontalMovement)
				{
					this.values[this.targetIdx].rectTransform.pivot = new Vector2(0.5f + (float)num3 * (1f - this.transitionAlpha) * 0.1f, 0.5f);
					this.values[num2].rectTransform.pivot = new Vector2(0.5f - (float)num3 * this.transitionAlpha * 0.05f, 0.5f);
				}
				else
				{
					this.values[this.targetIdx].rectTransform.pivot = new Vector2(0.5f, 0.5f + (float)num3 * (1f - this.transitionAlpha) * 0.4f);
					this.values[num2].rectTransform.pivot = new Vector2(0.5f, 0.5f - (float)num3 * this.transitionAlpha * 0.2f);
				}
			}
		}
	}

	// Token: 0x04002950 RID: 10576
	private const float maxDt = 0.025f;

	// Token: 0x04002951 RID: 10577
	[SerializeField]
	private IntWidget widget;

	// Token: 0x04002952 RID: 10578
	[SerializeField]
	private Text numberText;

	// Token: 0x04002953 RID: 10579
	[SerializeField]
	private UIClickable decrementClick;

	// Token: 0x04002954 RID: 10580
	[SerializeField]
	private UIClickable incrementClick;

	// Token: 0x04002955 RID: 10581
	[SerializeField]
	private bool horizontalMovement;

	// Token: 0x04002956 RID: 10582
	private Text[] values;

	// Token: 0x04002957 RID: 10583
	private int previousValue;

	// Token: 0x04002958 RID: 10584
	private int displayValue;

	// Token: 0x04002959 RID: 10585
	private int targetIdx;

	// Token: 0x0400295A RID: 10586
	private float transitionAlpha = 1f;
}
