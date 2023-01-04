using System;
using I2.Loc;
using RTM.Pools;
using RTM.UISystem;
using UnityEngine;
using UnityEngine.UI;
using Voxels.TowerDefense.UI;

// Token: 0x020008D0 RID: 2256
internal class PopupWidgetOption : MonoBehaviour, IPoolable
{
	// Token: 0x06003B9A RID: 15258 RVA: 0x0010919C File Offset: 0x0010759C
	public void Setup(PopupWidget_Popup owner, int i, string value, bool wantsLocalized)
	{
		if (wantsLocalized)
		{
			this.localize.Term = value;
		}
		else
		{
			this.text.text = value;
		}
		this.onClick = delegate()
		{
			owner.ClickHandler(i);
			this.SetSelected(true);
		};
	}

	// Token: 0x06003B9B RID: 15259 RVA: 0x001091FA File Offset: 0x001075FA
	public void ClickHandler()
	{
		this.onClick();
	}

	// Token: 0x06003B9C RID: 15260 RVA: 0x00109207 File Offset: 0x00107607
	void IPoolable.OnRemoved()
	{
		base.gameObject.SetActive(true);
		this.SetSelected(false);
	}

	// Token: 0x06003B9D RID: 15261 RVA: 0x0010921C File Offset: 0x0010761C
	void IPoolable.OnReturned()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x06003B9E RID: 15262 RVA: 0x0010922C File Offset: 0x0010762C
	void IPoolable.SetPool<T>(LocalPool<T> pool)
	{
		this.interactible = base.GetComponent<UIInteractable>();
		PolygonAnimator component = base.GetComponent<PolygonAnimator>();
		if (component)
		{
			component.MaybeInitialize();
		}
		else
		{
			DistanceFieldAnimator component2 = base.GetComponent<DistanceFieldAnimator>();
			if (component2)
			{
				component2.MaybeInitialize();
			}
		}
	}

	// Token: 0x06003B9F RID: 15263 RVA: 0x0010927A File Offset: 0x0010767A
	public void SetSelected(bool selected)
	{
		this.interactible.selected = selected;
	}

	// Token: 0x0400297C RID: 10620
	[SerializeField]
	public Text text;

	// Token: 0x0400297D RID: 10621
	[SerializeField]
	public Localize localize;

	// Token: 0x0400297E RID: 10622
	private Action onClick;

	// Token: 0x0400297F RID: 10623
	private UIInteractable interactible;
}
