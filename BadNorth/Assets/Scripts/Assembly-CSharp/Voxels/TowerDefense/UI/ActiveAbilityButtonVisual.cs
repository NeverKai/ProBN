using System;
using I2.Loc;
using RTM.UISystem;
using RTM.Utilities;
using UnityEngine;
using UnityEngine.UI;
using Voxels.TowerDefense.Upgrades;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x02000841 RID: 2113
	public class ActiveAbilityButtonVisual : MonoBehaviour
	{
		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x06003734 RID: 14132 RVA: 0x000ECC03 File Offset: 0x000EB003
		private ActiveAbility activeAbility
		{
			get
			{
				return this.activeAbilityButton.activeAbility;
			}
		}

		// Token: 0x06003735 RID: 14133 RVA: 0x000ECC10 File Offset: 0x000EB010
		private void LateUpdate()
		{
			this.AnimateButton();
		}

		// Token: 0x06003736 RID: 14134 RVA: 0x000ECC18 File Offset: 0x000EB018
		private void UpdateMaskedSprite()
		{
			UIInteractable.State state = UIInteractable.State.None;
			if (this.pointer.state == UIPointerReceiver.State.Hover)
			{
				state = UIInteractable.State.Hover;
			}
			else if (this.pointer.state == UIPointerReceiver.State.ButtonDown)
			{
				state = UIInteractable.State.PointerButtonDown;
			}
			this.maskedSpriteAnimated.avaliable = this.activeAbilityButton.activeAbility.IsAvailable();
			this.maskedSpriteAnimated.state = state;
			this.maskedSpriteAnimated.selected = this.activeAbilityButton.hasFocus;
			this.maskedSpriteAnimated.maskedSprite.radial = ((!this.activeAbility.isCoolingDown) ? 1f : (1f - this.activeAbility.cooldownRatioRemaining));
		}

		// Token: 0x06003737 RID: 14135 RVA: 0x000ECCCC File Offset: 0x000EB0CC
		private void AnimateButton()
		{
			if (this.activeAbility.hasLimitedCharges && this.chargesRemain != this.activeAbility.chargesRemaining)
			{
				this.chargesRemain = this.activeAbility.chargesRemaining;
				this.chargesRemainText.text = ((this.chargesRemain <= 0) ? "-" : ActiveAbilityButtonVisual.chargesFmt.Get(this.chargesRemain));
			}
			bool visible = this.toolTipVisibility.visible;
			this.toolTipVisibility.SetVisible(this.ShouldShowToolTip(), false);
			if (this.toolTipVisibility.visible && !visible)
			{
				this.toolTip.transform.ForceChildLayoutUpdates(false);
			}
			this.UpdateMaskedSprite();
		}

		// Token: 0x06003738 RID: 14136 RVA: 0x000ECD8C File Offset: 0x000EB18C
		private bool ShouldShowToolTip()
		{
			return this.activeAbilityButton.hasFocus || this.maskedSpriteAnimated.state > UIInteractable.State.None;
		}

		// Token: 0x06003739 RID: 14137 RVA: 0x000ECDAF File Offset: 0x000EB1AF
		private void OnEnable()
		{
			this.toolTipVisibility.SetVisible(false, true);
			this.bannedIndicator.SetActive(this.activeAbility.isBanned);
		}

		// Token: 0x0600373A RID: 14138 RVA: 0x000ECDD4 File Offset: 0x000EB1D4
		public void Init(ActiveAbilityButton activeAbilityButton)
		{
			this.activeAbilityButton = activeAbilityButton;
			this.pointer = base.GetComponent<UIPointerReceiver>();
			this.toolTipVisibility = this.toolTip.GetComponent<IUIVisibility>();
		}

		// Token: 0x0600373B RID: 14139 RVA: 0x000ECDFC File Offset: 0x000EB1FC
		public void Setup()
		{
			using ("ActiveAbilityButtonVisual.Setup")
			{
				if (this.activeAbility.iconSprite)
				{
					this.iconImage.sprite = this.activeAbility.iconSprite;
				}
				this.tooltipTitle.Term = this.activeAbility.abilityNameTerm;
				this.chargesRemainBubble.gameObject.SetActive(this.activeAbility.hasLimitedCharges);
				this.toolTipVisibility.SetVisible(false, true);
				this.bannedIndicator.SetActive(this.activeAbility.isBanned);
			}
		}

		// Token: 0x04002573 RID: 9587
		private static IntStringCache chargesFmt = new IntStringCache(0, 5, "X{0}");

		// Token: 0x04002574 RID: 9588
		[SerializeField]
		private MaskedSpriteAnimated maskedSpriteAnimated;

		// Token: 0x04002575 RID: 9589
		private ActiveAbilityButton activeAbilityButton;

		// Token: 0x04002576 RID: 9590
		private UIPointerReceiver pointer;

		// Token: 0x04002577 RID: 9591
		[Header("Tooltip")]
		[SerializeField]
		private GameObject toolTip;

		// Token: 0x04002578 RID: 9592
		private IUIVisibility toolTipVisibility;

		// Token: 0x04002579 RID: 9593
		[SerializeField]
		private Localize tooltipTitle;

		// Token: 0x0400257A RID: 9594
		[Header("Icon")]
		[SerializeField]
		private Image iconImage;

		// Token: 0x0400257B RID: 9595
		[SerializeField]
		private GameObject bannedIndicator;

		// Token: 0x0400257C RID: 9596
		[Header("Charges etc.")]
		[SerializeField]
		private RectTransform chargesRemainBubble;

		// Token: 0x0400257D RID: 9597
		[SerializeField]
		private Text chargesRemainText;

		// Token: 0x0400257E RID: 9598
		private int chargesRemain = -2147483647;
	}
}
