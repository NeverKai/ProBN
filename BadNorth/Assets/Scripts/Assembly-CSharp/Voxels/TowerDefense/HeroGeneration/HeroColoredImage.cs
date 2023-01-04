using System;
using ControlledRandomness;
using Spriteshop;
using UnityEngine;

namespace Voxels.TowerDefense.HeroGeneration
{
	// Token: 0x02000768 RID: 1896
	public class HeroColoredImage : MonoBehaviour, MonoHero.IHeroSetup
	{
		// Token: 0x0600314D RID: 12621 RVA: 0x000CC021 File Offset: 0x000CA421
		bool MonoHero.IHeroSetup.HeroSetup(MonoHero monoHero, PropertyBank propertyBank)
		{
			this.monoHero = monoHero;
			monoHero.colorDict[this.key].Subscribe(new Action<Color>(this.SetColor));
			return true;
		}

		// Token: 0x0600314E RID: 12622 RVA: 0x000CC050 File Offset: 0x000CA450
		public void SetColor(Color color)
		{
			if (!this)
			{
				return;
			}
			PsdGroup component = base.GetComponent<PsdGroup>();
			if (component)
			{
				component.SetColor(color);
			}
			SpriteRenderer component2 = base.GetComponent<SpriteRenderer>();
			if (component2)
			{
				component2.color = color.SetA(component2.color.a);
			}
		}

		// Token: 0x0600314F RID: 12623 RVA: 0x000CC0AE File Offset: 0x000CA4AE
		private void OnDestroy()
		{
			if (this.monoHero)
			{
				this.monoHero.colorDict[this.key].Unsubscribe(new Action<Color>(this.SetColor));
			}
		}

		// Token: 0x0400210F RID: 8463
		public string key;

		// Token: 0x04002110 RID: 8464
		[SerializeField]
		private HeroColoredImage.Mode deadMode = HeroColoredImage.Mode.WhiteOnDead;

		// Token: 0x04002111 RID: 8465
		private Color defaultColor = Color.white;

		// Token: 0x04002112 RID: 8466
		private MonoHero monoHero;

		// Token: 0x02000769 RID: 1897
		private enum Mode
		{
			// Token: 0x04002114 RID: 8468
			None,
			// Token: 0x04002115 RID: 8469
			WhiteOnDead,
			// Token: 0x04002116 RID: 8470
			DimOnDead
		}
	}
}
