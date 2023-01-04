using System;
using System.Collections.Generic;
using I2.Loc;
using RTM.OnScreenDebug;
using RTM.UISystem;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020008CB RID: 2251
public class MultiSelectWidget_Decorator : MonoBehaviour, Widget.IWidgetInit
{
	// Token: 0x06003B7F RID: 15231 RVA: 0x00108848 File Offset: 0x00106C48
	void Widget.IWidgetInit.OnWidgetInit(Widget baseWidget)
	{
		int num = this.widget.values.Length;
		this.instances = new List<MultiSelectWidget_Decorator.ValueInstance>(this.widget.values.Length);
		Transform parent = this.valueReference.parent;
		for (int i = 0; i < num; i++)
		{
			MultiSelectWidget_Decorator.ValueInstance item = new MultiSelectWidget_Decorator.ValueInstance(parent.gameObject.InstantiateChild(this.valueReference, null));
			item.gameObject.SetActive(false);
			if (this.widget.wantsLocalizedValues)
			{
				item.localize.Term = this.widget.values[i];
			}
			else
			{
				item.text.text = this.widget.values[i];
			}
			this.instances.Add(item);
		}
		this.valueReference.gameObject.SetActive(false);
		this.defaultColor = this.valueReference.GetComponent<Text>().color;
		this.widget.onValueChanged += this.SetTarget;
		this.widget.onForceUpdate += this.SetTarget;
		this.widget.onForceUpdate += this.SnapToTarget;
	}

	// Token: 0x06003B80 RID: 15232 RVA: 0x00108980 File Offset: 0x00106D80
	private void OnDestroy()
	{
		this.widget.onValueChanged -= this.SetTarget;
		this.widget.onForceUpdate -= this.SetTarget;
		this.widget.onForceUpdate -= this.SnapToTarget;
	}

	// Token: 0x06003B81 RID: 15233 RVA: 0x001089D2 File Offset: 0x00106DD2
	private void SnapToTarget(int value)
	{
		this.SetTarget(value);
		this.currentValue = this.targetValue;
		this.UpdateVisibility();
	}

	// Token: 0x06003B82 RID: 15234 RVA: 0x001089F0 File Offset: 0x00106DF0
	private void SetTarget(int idx)
	{
		float num = (float)this.widget.values.Length;
		this.targetValue = (float)idx;
		this.currentValue = (this.currentValue + num) % num;
		if (this.widget.incrementsCycle)
		{
			UIInteractable uiinteractable = this.decrementClick;
			bool disabled = false;
			this.incrementClick.disabled = disabled;
			uiinteractable.disabled = disabled;
			float f = this.targetValue - this.currentValue;
			float f2 = this.targetValue - Mathf.Sign(f) * num - this.currentValue;
			bool flag = Mathf.Abs(f2) < Mathf.Abs(f);
			if (flag)
			{
				this.targetValue -= Mathf.Sign(f) * (float)this.widget.values.Length;
			}
		}
		else
		{
			this.decrementClick.disabled = (idx == this.widget.min);
			this.incrementClick.disabled = (idx == this.widget.max);
		}
	}

	// Token: 0x06003B83 RID: 15235 RVA: 0x00108AE4 File Offset: 0x00106EE4
	private void LateUpdate()
	{
		if (this.currentValue != this.targetValue)
		{
			float num = Mathf.Min(Time.unscaledDeltaTime, 0.025f);
			this.currentValue = Mathf.Lerp(this.currentValue, this.targetValue, num * 11f);
			this.currentValue = Mathf.MoveTowards(this.currentValue, this.targetValue, num * 6f);
			this.UpdateVisibility();
		}
	}

	// Token: 0x06003B84 RID: 15236 RVA: 0x00108B58 File Offset: 0x00106F58
	private void UpdateVisibility()
	{
		int num = this.widget.values.Length;
		float num2 = (this.currentValue + (float)num) % (float)num;
		int num3 = Mathf.FloorToInt(num2) % num;
		int num4 = Mathf.CeilToInt(num2) % num;
		int i = 0;
		int count = this.instances.Count;
		while (i < count)
		{
			MultiSelectWidget_Decorator.ValueInstance valueInstance = this.instances[i];
			if (i == num4 || i == num3)
			{
				int num5 = (i != num3) ? 1 : 0;
				valueInstance.gameObject.SetActive(true);
				float num6 = num2 % 1f - (float)num5;
				valueInstance.rectTransform.pivot = valueInstance.rectTransform.pivot.SetX(num6 * 0.3f + 0.5f);
				valueInstance.text.color = this.defaultColor.SetA(this.defaultColor.a * (1f - num6 * num6));
			}
			else
			{
				valueInstance.gameObject.SetActive(false);
			}
			i++;
		}
	}

	// Token: 0x0400295F RID: 10591
	private const float maxDeltaTime = 0.025f;

	// Token: 0x04002960 RID: 10592
	private DebugChannelGroup dbgGroup = new DebugChannelGroup("MultiSelectWidget_Decorator", EVerbosity.Quiet, 100);

	// Token: 0x04002961 RID: 10593
	[SerializeField]
	private MultiSelectWidget widget;

	// Token: 0x04002962 RID: 10594
	[SerializeField]
	private RectTransform valueReference;

	// Token: 0x04002963 RID: 10595
	private List<MultiSelectWidget_Decorator.ValueInstance> instances;

	// Token: 0x04002964 RID: 10596
	private float targetValue;

	// Token: 0x04002965 RID: 10597
	private float currentValue;

	// Token: 0x04002966 RID: 10598
	private Color defaultColor = Color.black;

	// Token: 0x04002967 RID: 10599
	[SerializeField]
	private UIClickable decrementClick;

	// Token: 0x04002968 RID: 10600
	[SerializeField]
	private UIClickable incrementClick;

	// Token: 0x020008CC RID: 2252
	private struct ValueInstance
	{
		// Token: 0x06003B85 RID: 15237 RVA: 0x00108C6B File Offset: 0x0010706B
		public ValueInstance(RectTransform rectTransform)
		{
			this.rectTransform = rectTransform;
			this.gameObject = rectTransform.gameObject;
			this.text = rectTransform.GetComponent<Text>();
			this.localize = rectTransform.GetComponent<Localize>();
		}

		// Token: 0x04002969 RID: 10601
		public RectTransform rectTransform;

		// Token: 0x0400296A RID: 10602
		public GameObject gameObject;

		// Token: 0x0400296B RID: 10603
		public Text text;

		// Token: 0x0400296C RID: 10604
		public Localize localize;
	}
}
