using System;
using System.Diagnostics;
using RTM.UISystem;
using UnityEngine;

// Token: 0x020008D2 RID: 2258
public abstract class ValueWidget<T> : Widget
{
	// Token: 0x140000CA RID: 202
	// (add) Token: 0x06003BA9 RID: 15273 RVA: 0x00107740 File Offset: 0x00105B40
	// (remove) Token: 0x06003BAA RID: 15274 RVA: 0x00107778 File Offset: 0x00105B78
	
	public event Action<T> onValueChanged = delegate(T A_0)
	{
	};

	// Token: 0x140000CB RID: 203
	// (add) Token: 0x06003BAB RID: 15275 RVA: 0x001077B0 File Offset: 0x00105BB0
	// (remove) Token: 0x06003BAC RID: 15276 RVA: 0x001077E8 File Offset: 0x00105BE8
	
	public event Action<T> onForceUpdate = delegate(T A_0)
	{
	};

	// Token: 0x06003BAD RID: 15277 RVA: 0x00107820 File Offset: 0x00105C20
	public ValueWidget<T> Initialize(string locID, Func<T> getAction, Func<T, bool> setAction)
	{
		base.Initialize(locID);
		this.getAction = getAction;
		this.setAction = setAction;
		UINavigable component = base.GetComponent<UINavigable>();
		if (component)
		{
			component.onConsumedNavigation += this.Navigable_onConsumedNavigation;
		}
		return this;
	}

	// Token: 0x06003BAE RID: 15278 RVA: 0x00107868 File Offset: 0x00105C68
	private void Navigable_onConsumedNavigation(Vector2 dir)
	{
		if (dir.x < 0f)
		{
			this.DecrementHandler();
		}
		else if (dir.x > 0f)
		{
			this.IncrementHandler();
		}
	}

	// Token: 0x06003BAF RID: 15279 RVA: 0x001078A0 File Offset: 0x00105CA0
	public T GetValue()
	{
		return (this.getAction == null) ? default(T) : this.getAction();
	}

	// Token: 0x06003BB0 RID: 15280 RVA: 0x001078D4 File Offset: 0x00105CD4
	public void SetValueHandler(T newValue)
	{
		T value = this.GetValue();
		if (!newValue.Equals(value) && this.setAction != null && this.setAction(newValue))
		{
			FabricWrapper.PostEvent(base.successAudio);
			this.onValueChanged(newValue);
		}
		else
		{
			FabricWrapper.PostEvent(base.failAudio);
		}
	}

	// Token: 0x06003BB1 RID: 15281 RVA: 0x00107945 File Offset: 0x00105D45
	public override void ForceUpdate()
	{
		base.ForceUpdate();
		this.onForceUpdate(this.GetValue());
	}

	// Token: 0x06003BB2 RID: 15282 RVA: 0x0010795E File Offset: 0x00105D5E
	public virtual void DecrementHandler()
	{
		FabricWrapper.PostEvent(base.failAudio);
	}

	// Token: 0x06003BB3 RID: 15283 RVA: 0x0010796C File Offset: 0x00105D6C
	public virtual void IncrementHandler()
	{
		FabricWrapper.PostEvent(base.failAudio);
	}

	// Token: 0x04002982 RID: 10626
	private Func<T> getAction;

	// Token: 0x04002983 RID: 10627
	private Func<T, bool> setAction;
}
