using System;
using System.Linq;
using I2.Loc;
using RTM.Pools;
using RTM.UISystem;
using UnityEngine;
using UnityEngine.UI;
using Voxels.TowerDefense.UI;

// Token: 0x020008CF RID: 2255
internal class PopupWidget_Popup : UIMenu
{
	// Token: 0x1700085B RID: 2139
	// (get) Token: 0x06003B8F RID: 15247 RVA: 0x00108DF6 File Offset: 0x001071F6
	// (set) Token: 0x06003B90 RID: 15248 RVA: 0x00108DFE File Offset: 0x001071FE
	public LocalPool<PopupWidgetOption> options { get; private set; }

	// Token: 0x06003B91 RID: 15249 RVA: 0x00108E08 File Offset: 0x00107208
	public void Setup(PopupWidget widget)
	{
		this.widget = widget;
		this.visibility = base.GetComponent<IUIVisibility>();
		this.visibility.SetVisible(false, true);
		this.options = new LocalPool<PopupWidgetOption>(base.GetComponentsInChildren<PopupWidgetOption>(true), null);
		this.options.ExpandTo(widget.values.Count<string>());
		if (widget.localize.enabled)
		{
			this.titleLocalize.Term = widget.localize.Term;
		}
		else
		{
			this.titleText.text = widget.label.text;
		}
		this.scroller = base.GetComponentInChildren<UIMenuFocusScroller>(true);
		UiBehaviorDelegates component = base.GetComponent<UiBehaviorDelegates>();
		component.onRectTransformDimensionsChange += this.OnRectTransformDimensionsChange;
	}

	// Token: 0x06003B92 RID: 15250 RVA: 0x00108EC8 File Offset: 0x001072C8
	public override void OpenMenu()
	{
		this.options.ReturnAll();
		bool wantsLocalizedValues = this.widget.wantsLocalizedValues;
		int num = 0;
		int value = this.widget.GetValue();
		IUINavigable navigable = null;
		foreach (string value2 in this.widget.values)
		{
			PopupWidgetOption instance = this.options.GetInstance();
			instance.Setup(this, num, value2, wantsLocalizedValues);
			instance.transform.SetAsLastSibling();
			if (num == value)
			{
				instance.SetSelected(true);
				navigable = instance.GetComponent<IUINavigable>();
			}
			num++;
		}
		base.OpenMenu();
		this.visibility.SetVisible(true, false);
		base.transform.ForceChildLayoutUpdates(false);
		this.UpdateSizeAndPos();
		this.scroller.ScrollTo(navigable, true);
	}

	// Token: 0x06003B93 RID: 15251 RVA: 0x00108F9C File Offset: 0x0010739C
	public void ClickHandler(int i)
	{
		this.widget.SetValueHandler(i);
		foreach (PopupWidgetOption popupWidgetOption in this.options.inUse)
		{
			popupWidgetOption.SetSelected(false);
		}
		this.CloseMenu();
	}

	// Token: 0x06003B94 RID: 15252 RVA: 0x00109010 File Offset: 0x00107410
	public void HandleClickOff()
	{
		this.HandleBackButton();
	}

	// Token: 0x06003B95 RID: 15253 RVA: 0x00109019 File Offset: 0x00107419
	public override void CloseMenu()
	{
		this.visibility.SetVisible(false, false);
		base.CloseMenu();
	}

	// Token: 0x06003B96 RID: 15254 RVA: 0x00109030 File Offset: 0x00107430
	protected override IUINavigable GetDefaultNavigable()
	{
		IUINavigable lastNavigable = base.GetLastNavigable();
		if (lastNavigable != null)
		{
			return lastNavigable;
		}
		base.RefreshNavigables();
		int value = this.widget.GetValue();
		return (!this.options.inUse.IsValidIndex(value)) ? base.GetFirstNavigable() : this.options.inUse[value].GetComponent<UINavigable>();
	}

	// Token: 0x06003B97 RID: 15255 RVA: 0x00109098 File Offset: 0x00107498
	private void UpdateSizeAndPos()
	{
		if (!this.widget)
		{
			return;
		}
		Vector2 size = this.scrollRect.content.rect.size;
		Vector2 size2 = this.scrollRect.viewport.rect.size;
		Vector2 size3 = this.box.rect.size;
		Vector2 vector = size3 - size2;
		Vector2 size4 = this.maxSize.rect.size;
		float size5 = Mathf.Min(size.y + vector.y, size4.y);
		this.box.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size5);
		this.box.position = new Vector3(this.widget.transform.position.x, this.maxSize.position.y, 0f);
	}

	// Token: 0x06003B98 RID: 15256 RVA: 0x0010918B File Offset: 0x0010758B
	private void OnRectTransformDimensionsChange()
	{
		this.UpdateSizeAndPos();
	}

	// Token: 0x04002974 RID: 10612
	[Header("Title")]
	[SerializeField]
	private Text titleText;

	// Token: 0x04002975 RID: 10613
	[SerializeField]
	private Localize titleLocalize;

	// Token: 0x04002976 RID: 10614
	[Header("Sizing")]
	[SerializeField]
	private RectTransform maxSize;

	// Token: 0x04002977 RID: 10615
	[SerializeField]
	private RectTransform box;

	// Token: 0x04002978 RID: 10616
	private PopupWidget widget;

	// Token: 0x04002979 RID: 10617
	private IUIVisibility visibility;

	// Token: 0x0400297A RID: 10618
	private UIMenuFocusScroller scroller;
}
