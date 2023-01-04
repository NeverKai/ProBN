using System;
using I2.Loc;
using RTM.UISystem;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020008D3 RID: 2259
public abstract class Widget : MonoBehaviour
{
	// Token: 0x1700085D RID: 2141
	// (get) Token: 0x06003BB7 RID: 15287 RVA: 0x0010753F File Offset: 0x0010593F
	// (set) Token: 0x06003BB8 RID: 15288 RVA: 0x00107547 File Offset: 0x00105947
	public IUINavigable navigable { get; private set; }

	// Token: 0x1700085E RID: 2142
	// (get) Token: 0x06003BB9 RID: 15289 RVA: 0x00107550 File Offset: 0x00105950
	// (set) Token: 0x06003BBA RID: 15290 RVA: 0x0010756D File Offset: 0x0010596D
	public FabricEventReference successAudio
	{
		get
		{
			return (this._successAudio != null) ? this._successAudio : FabricID.uiButtonClick;
		}
		set
		{
			this._successAudio = value;
		}
	}

	// Token: 0x1700085F RID: 2143
	// (get) Token: 0x06003BBB RID: 15291 RVA: 0x00107576 File Offset: 0x00105976
	// (set) Token: 0x06003BBC RID: 15292 RVA: 0x00107593 File Offset: 0x00105993
	public FabricEventReference failAudio
	{
		get
		{
			return (this._failAudio != null) ? this._failAudio : FabricID.uiError;
		}
		set
		{
			this._failAudio = value;
		}
	}

	// Token: 0x17000860 RID: 2144
	// (get) Token: 0x06003BBD RID: 15293 RVA: 0x0010759C File Offset: 0x0010599C
	// (set) Token: 0x06003BBE RID: 15294 RVA: 0x001075A4 File Offset: 0x001059A4
	public string dbgName { get; private set; }

	// Token: 0x06003BBF RID: 15295 RVA: 0x001075B0 File Offset: 0x001059B0
	protected Widget Initialize(string locID)
	{
		this.SetLabel(locID);
		this.navigable = base.GetComponentInChildren<IUINavigable>(true);
		foreach (Widget.IWidgetInit widgetInit in base.GetComponentsInChildren<Widget.IWidgetInit>(true))
		{
			widgetInit.OnWidgetInit(this);
		}
		return this;
	}

	// Token: 0x06003BC0 RID: 15296 RVA: 0x001075FA File Offset: 0x001059FA
	public Widget SetLabel(string labelLocTerm)
	{
		if (this.localize)
		{
			this.localize.Term = labelLocTerm;
			this.localize.enabled = true;
		}
		return this;
	}

	// Token: 0x06003BC1 RID: 15297 RVA: 0x00107625 File Offset: 0x00105A25
	public Widget SetNonLocalizedLabel(string labelText)
	{
		if (this.localize)
		{
			this.localize.enabled = false;
			this.label.text = labelText;
		}
		return this;
	}

	// Token: 0x06003BC2 RID: 15298 RVA: 0x00107650 File Offset: 0x00105A50
	public Widget SetFailAudio(FabricEventReference audioId)
	{
		this.failAudio = audioId;
		return this;
	}

	// Token: 0x06003BC3 RID: 15299 RVA: 0x0010765A File Offset: 0x00105A5A
	public Widget SetSuccessAudio(FabricEventReference audioId)
	{
		this.successAudio = audioId;
		return this;
	}

	// Token: 0x06003BC4 RID: 15300 RVA: 0x00107664 File Offset: 0x00105A64
	public Widget SetHoverAudio(FabricEventReference audioId)
	{
		UIInteractable component = base.GetComponent<UIInteractable>();
		if (component)
		{
			component.SetHoverAudio(audioId);
		}
		return this;
	}

	// Token: 0x06003BC5 RID: 15301 RVA: 0x0010768B File Offset: 0x00105A8B
	public Widget SetVisibilityCallback(Func<bool> visibilityCallback)
	{
		this.visibilityCallback = visibilityCallback;
		return this;
	}

	// Token: 0x06003BC6 RID: 15302 RVA: 0x00107695 File Offset: 0x00105A95
	public Widget SetUpdateAction(Action updateAction)
	{
		this.updateAction = updateAction;
		return this;
	}

	// Token: 0x06003BC7 RID: 15303 RVA: 0x0010769F File Offset: 0x00105A9F
	public virtual void SlaveUpdate()
	{
		if (this.visibilityCallback != null)
		{
			base.gameObject.SetActive(this.visibilityCallback());
		}
		if (this.updateAction != null)
		{
			this.updateAction();
		}
	}

	// Token: 0x06003BC8 RID: 15304 RVA: 0x001076D8 File Offset: 0x00105AD8
	public virtual void ForceUpdate()
	{
	}

	// Token: 0x06003BC9 RID: 15305 RVA: 0x001076DA File Offset: 0x00105ADA
	protected virtual void OnEnable()
	{
		this.ForceUpdate();
	}

	// Token: 0x04002988 RID: 10632
	[Header("Widget")]
	public Text label;

	// Token: 0x04002989 RID: 10633
	public Localize localize;

	// Token: 0x0400298B RID: 10635
	private FabricEventReference _successAudio;

	// Token: 0x0400298C RID: 10636
	private FabricEventReference _failAudio;

	// Token: 0x0400298D RID: 10637
	private Func<bool> visibilityCallback;

	// Token: 0x0400298E RID: 10638
	private Action enableAction;

	// Token: 0x0400298F RID: 10639
	private Action updateAction;

	// Token: 0x020008D4 RID: 2260
	public interface IWidgetInit
	{
		// Token: 0x06003BCA RID: 15306
		void OnWidgetInit(Widget widget);
	}
}
