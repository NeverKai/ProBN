using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ReflexCLI.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x0200074C RID: 1868
	public class CreditsUI : MonoBehaviour, IGameSetup, IScenePostprocessor
	{
		// Token: 0x1400009D RID: 157
		// (add) Token: 0x060030AC RID: 12460 RVA: 0x000C6DE8 File Offset: 0x000C51E8
		// (remove) Token: 0x060030AD RID: 12461 RVA: 0x000C6E1C File Offset: 0x000C521C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action onCreditsFillScreen;

		// Token: 0x1400009E RID: 158
		// (add) Token: 0x060030AE RID: 12462 RVA: 0x000C6E50 File Offset: 0x000C5250
		// (remove) Token: 0x060030AF RID: 12463 RVA: 0x000C6E84 File Offset: 0x000C5284
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action onCreditsComplete;

		// Token: 0x060030B0 RID: 12464 RVA: 0x000C6EB8 File Offset: 0x000C52B8
		void IGameSetup.OnGameAwake()
		{
			CreditsUI.instance = this;
			base.gameObject.SetActive(true);
			this.childBlocks = base.transform.GetComponentsInChildren<RectTransform>().ToList<RectTransform>();
			for (int i = this.childBlocks.Count - 1; i >= 0; i--)
			{
				RectTransform rectTransform = this.childBlocks[i];
				if (rectTransform == this.headerTransform || rectTransform.IsDescendentOf(this.headerTransform))
				{
					this.childBlocks.RemoveAt(i);
				}
				else
				{
					rectTransform.gameObject.SetActive(true);
					rectTransform.gameObject.SetActive(false);
				}
			}
			this.visibility = base.GetComponent<IUIVisibility>();
			this.visibility.SetVisible(false, true);
			base.gameObject.SetActive(false);
		}

		// Token: 0x060030B1 RID: 12465 RVA: 0x000C6F88 File Offset: 0x000C5388
		[ConsoleCommand("")]
		public static void Roll(TextAlignment alignment, Color color)
		{
			CreditsUI.instance.RollInternal(alignment, color);
		}

		// Token: 0x060030B2 RID: 12466 RVA: 0x000C6F96 File Offset: 0x000C5396
		[ConsoleCommand("")]
		public static void Cancel()
		{
			CreditsUI.instance.CancelInternal();
		}

		// Token: 0x060030B3 RID: 12467 RVA: 0x000C6FA2 File Offset: 0x000C53A2
		private void RollInternal(TextAlignment alignment, Color color)
		{
			base.transform.ForceChildLayoutUpdates(false);
			this.ResetRoll(alignment);
			this.color = color;
			this.visibility.SetVisible(true, false);
		}

		// Token: 0x060030B4 RID: 12468 RVA: 0x000C6FCB File Offset: 0x000C53CB
		private void CancelInternal()
		{
			this.visibility.SetVisible(false, false);
		}

		// Token: 0x060030B5 RID: 12469 RVA: 0x000C6FDC File Offset: 0x000C53DC
		private void ResetRoll(TextAlignment textAlignment)
		{
			float value = 0f;
			if (textAlignment != TextAlignment.Left)
			{
				if (textAlignment != TextAlignment.Center)
				{
					if (textAlignment == TextAlignment.Right)
					{
						value = 1f;
						this.textAnchor = TextAnchor.UpperRight;
					}
				}
				else
				{
					value = 0.5f;
					this.textAnchor = TextAnchor.UpperCenter;
				}
			}
			else
			{
				value = 0f;
				this.textAnchor = TextAnchor.UpperLeft;
			}
			this.visibility.SetVisible(false, true);
			this.currentChildBlock = 0;
			this.filledScreen = false;
			this.position = 0f;
			this.positionTransform.SetLocalY(0f);
			this.positionTransform.anchorMin = this.positionTransform.anchorMin.SetX(value);
			this.positionTransform.anchorMax = this.positionTransform.anchorMax.SetX(value);
			this.positionTransform.pivot = this.positionTransform.pivot.SetX(value);
		}

		// Token: 0x060030B6 RID: 12470 RVA: 0x000C70CC File Offset: 0x000C54CC
		private void Update()
		{
			this.position += CreditsUI.scrollSpeed * Time.deltaTime;
			this.positionTransform.SetLocalY(this.position);
			Rect worldSpaceRect = this.positionTransform.GetWorldSpaceRect();
			RectTransform rt = base.transform as RectTransform;
			float yMax = rt.GetWorldSpaceRect().yMax;
			if (this.visibility.visible)
			{
				if (this.childBlocks.IsValidIndex(this.currentChildBlock))
				{
					RectTransform rectTransform = this.childBlocks[this.currentChildBlock];
					if (rectTransform.GetWorldSpaceRect().yMax > -20f)
					{
						rectTransform.gameObject.SetActive(true);
						Text component = rectTransform.GetComponent<Text>();
						if (component)
						{
							component.color = this.color;
							component.alignment = this.textAnchor;
						}
						this.currentChildBlock++;
					}
				}
				if (!this.filledScreen && worldSpaceRect.yMax > yMax * 0.4f)
				{
					CreditsUI.onCreditsFillScreen();
					this.filledScreen = true;
				}
				if (worldSpaceRect.yMin > yMax)
				{
					CreditsUI.onCreditsComplete();
					this.visibility.SetVisible(false, true);
				}
			}
		}

		// Token: 0x060030B7 RID: 12471 RVA: 0x000C721A File Offset: 0x000C561A
		void IScenePostprocessor.OnPostprocessScene()
		{
		}

		// Token: 0x060030B8 RID: 12472 RVA: 0x000C721C File Offset: 0x000C561C
		// Note: this type is marked as 'beforefieldinit'.
		static CreditsUI()
		{
			CreditsUI.onCreditsFillScreen = delegate()
			{
			};
			CreditsUI.onCreditsComplete = delegate()
			{
			};
		}

		// Token: 0x04002085 RID: 8325
		private static CreditsUI instance = null;

		// Token: 0x04002086 RID: 8326
		[ConsoleCommand("")]
		private static float scrollSpeed = 45f;

		// Token: 0x04002087 RID: 8327
		[SerializeField]
		private RectTransform parentTransform;

		// Token: 0x04002088 RID: 8328
		[SerializeField]
		private RectTransform positionTransform;

		// Token: 0x04002089 RID: 8329
		[SerializeField]
		private RectTransform headerTransform;

		// Token: 0x0400208A RID: 8330
		[SerializeField]
		private CreditsDef def;

		// Token: 0x0400208B RID: 8331
		[SerializeField]
		private CreditsGroupUI[] groupPrefabs;

		// Token: 0x0400208C RID: 8332
		private IUIVisibility visibility;

		// Token: 0x0400208D RID: 8333
		private List<RectTransform> childBlocks;

		// Token: 0x0400208E RID: 8334
		private int currentChildBlock = 1;

		// Token: 0x0400208F RID: 8335
		private bool filledScreen;

		// Token: 0x04002090 RID: 8336
		private float position;

		// Token: 0x04002093 RID: 8339
		private TextAnchor textAnchor = TextAnchor.UpperCenter;

		// Token: 0x04002094 RID: 8340
		private Color color;
	}
}
