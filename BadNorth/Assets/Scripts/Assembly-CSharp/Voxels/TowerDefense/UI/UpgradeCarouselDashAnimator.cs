using System;
using RTM.UISystem;
using UnityEngine;
using UnityEngine.UI;
using Voxels.TowerDefense.ScriptAnimations;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x02000900 RID: 2304
	internal class UpgradeCarouselDashAnimator : MonoBehaviour
	{
		// Token: 0x06003D8D RID: 15757 RVA: 0x001141E0 File Offset: 0x001125E0
		public void Init()
		{
			Image image = base.GetComponent<Image>();
			Color defaultColor = image.color;
			AnimatedState interacting = new AnimatedState("Interacting", this.stateRoot.rootState, false, false, UpgradeCarouselItemName.animFuncs);
			float flashAlpha = 0f;
			TargetAnimator flash = new TargetAnimator("Flash", () => flashAlpha, delegate(float a)
			{
				flashAlpha = a;
			}, this.stateRoot.rootState, UpgradeCarouselItemName.flashFuncs);
			Action updateInteracing = delegate()
			{
				UIInteractable.State interactableState = this.driver.interactableState;
				UIInteractable.State state = (!this.driver.selectedUpgrade && interactableState != UIInteractable.State.Focus) ? UIInteractable.State.None : interactableState;
				interacting.active = (state != UIInteractable.State.None);
			};
			this.driver.onInteractibleStateChanged += delegate(UIInteractable.State s)
			{
				updateInteracing();
			};
			this.driver.onSelectedUpgradeChanged += delegate(HeroUpgradeDefinition u)
			{
				updateInteracing();
			};
			this.driver.onFlash += delegate(float delay)
			{
				flash.SetTargetAndCurrent(1f);
				TargetAnimator<float> flash2 = flash;
				float target = 0f;
				flash2.SetTarget(target, null, null, null, delay, null);
			};
			Action<float> b = delegate(float a)
			{
				Color color = Color.Lerp(defaultColor, this.focusColor, interacting.anim.current);
				float num = flash.current * 2f - 1f;
				float t = 1f - num * num;
				color = Color.Lerp(color, Color.white, t);
				image.color = color;
			};
			TargetAnimator<float> anim = interacting.anim;
			anim.setFunc = (Action<float>)Delegate.Combine(anim.setFunc, b);
			TargetAnimator flash3 = flash;
			flash3.setFunc = (Action<float>)Delegate.Combine(flash3.setFunc, b);
			interacting.ForceToTarget();
		}

		// Token: 0x06003D8E RID: 15758 RVA: 0x00114328 File Offset: 0x00112728
		private void Update()
		{
			this.stateRoot.Update();
		}

		// Token: 0x04002AF9 RID: 11001
		[SerializeField]
		private UpgradeCarousel driver;

		// Token: 0x04002AFA RID: 11002
		[SerializeField]
		private Color focusColor = Color.black;

		// Token: 0x04002AFB RID: 11003
		[SerializeField]
		private AgentStateRoot stateRoot;
	}
}
