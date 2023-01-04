using System;
using System.Collections;
using RTM.OnScreenDebug;
using UnityEngine;
using UnityEngine.UI;

namespace RTM.UISystem
{
	// Token: 0x020004DF RID: 1247
	public class UITestVisuals : MonoBehaviour
	{
		// Token: 0x06002003 RID: 8195 RVA: 0x00056380 File Offset: 0x00054780
		private void Awake()
		{
			if (!this.targetGraphic)
			{
				this.targetGraphic = base.GetComponentInChildren<Graphic>();
			}
			this.clickable = base.GetComponent<UIClickable>();
			if (this.clickable)
			{
				this.clickable.onStateChanged += this.OnClickableStateChanged;
			}
		}

		// Token: 0x06002004 RID: 8196 RVA: 0x000563DC File Offset: 0x000547DC
		private void OnEnable()
		{
			if (this.clickable)
			{
				this.SetColor(this.clickable.state);
			}
		}

		// Token: 0x06002005 RID: 8197 RVA: 0x000563FF File Offset: 0x000547FF
		private void OnClickableStateChanged(UIInteractable.State state)
		{
			base.StopAllCoroutines();
			if (base.gameObject.activeInHierarchy)
			{
				this.DoFade(state);
			}
			else
			{
				this.SetColor(state);
			}
		}

		// Token: 0x06002006 RID: 8198 RVA: 0x0005642C File Offset: 0x0005482C
		private Color GetTargetColor(UIInteractable.State state)
		{
			switch (state)
			{
			case UIInteractable.State.None:
				return this.normalColor;
			case UIInteractable.State.Hover:
				return this.hoverColor;
			case UIInteractable.State.PointerButtonDown:
				return this.pointerDownColor;
			case UIInteractable.State.Focus:
				return this.focusColor;
			default:
				throw new NotImplementedException(string.Format("Unrecognised state ({0})", state));
			}
		}

		// Token: 0x06002007 RID: 8199 RVA: 0x00056488 File Offset: 0x00054888
		private float GetTransitionTime(UIInteractable.State state)
		{
			switch (state)
			{
			case UIInteractable.State.None:
				return 0.1f;
			case UIInteractable.State.Hover:
				return 0.2f;
			case UIInteractable.State.PointerButtonDown:
				return 0.05f;
			case UIInteractable.State.Focus:
				return 0.1f;
			default:
				throw new NotImplementedException(string.Format("Unrecognised state ({0})", state));
			}
		}

		// Token: 0x06002008 RID: 8200 RVA: 0x000564DD File Offset: 0x000548DD
		private void SetColor(UIInteractable.State state)
		{
			this.SetColor(this.GetTargetColor(state));
		}

		// Token: 0x06002009 RID: 8201 RVA: 0x000564EC File Offset: 0x000548EC
		private void SetColor(Color color)
		{
			this.targetGraphic.color = color;
		}

		// Token: 0x0600200A RID: 8202 RVA: 0x000564FA File Offset: 0x000548FA
		private void DoFade(UIInteractable.State state)
		{
			base.StartCoroutine(this.FadeTo(this.GetTargetColor(state), this.GetTransitionTime(state)));
		}

		// Token: 0x0600200B RID: 8203 RVA: 0x00056518 File Offset: 0x00054918
		private IEnumerator FadeTo(Color targetColor, float targetTime)
		{
			if (this.targetGraphic)
			{
				Color startColor = this.targetGraphic.color;
				for (float timer = Time.unscaledDeltaTime; timer < targetTime; timer += Time.unscaledDeltaTime)
				{
					this.SetColor(Color.Lerp(startColor, targetColor, timer / targetTime));
					yield return null;
				}
			}
			this.SetColor(targetColor);
			yield return null;
			yield break;
		}

		// Token: 0x040013DD RID: 5085
		private static DebugChannelGroup dbgGroup = new DebugChannelGroup("chn", EVerbosity.Quiet, 0);

		// Token: 0x040013DE RID: 5086
		[Header("Scene References")]
		public Graphic targetGraphic;

		// Token: 0x040013DF RID: 5087
		[Header("States")]
		[SerializeField]
		private Color normalColor = Color.white;

		// Token: 0x040013E0 RID: 5088
		[SerializeField]
		private Color hoverColor = new Color(0.9f, 0.9f, 0.9f);

		// Token: 0x040013E1 RID: 5089
		[SerializeField]
		private Color focusColor = new Color(0.7f, 0.7f, 0.7f);

		// Token: 0x040013E2 RID: 5090
		[SerializeField]
		private Color pointerDownColor = new Color(0.7f, 0.7f, 0.7f);

		// Token: 0x040013E3 RID: 5091
		private UIClickable clickable;
	}
}
