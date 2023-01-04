using System;
using UnityEngine;

namespace I2.Loc
{
	// Token: 0x02000400 RID: 1024
	public class LocalizeTarget_UnityStandard_SpriteRenderer : LocalizeTarget<SpriteRenderer>
	{
		// Token: 0x060017BA RID: 6074 RVA: 0x0003BECF File Offset: 0x0003A2CF
		static LocalizeTarget_UnityStandard_SpriteRenderer()
		{
			LocalizeTarget_UnityStandard_SpriteRenderer.AutoRegister();
		}

		// Token: 0x060017BC RID: 6076 RVA: 0x0003BEE0 File Offset: 0x0003A2E0
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void AutoRegister()
		{
			LocalizationManager.RegisterTarget(new LocalizeTargetDesc_Type<SpriteRenderer, LocalizeTarget_UnityStandard_SpriteRenderer>
			{
				Name = "SpriteRenderer",
				Priority = 100
			});
		}

		// Token: 0x060017BD RID: 6077 RVA: 0x0003BF0C File Offset: 0x0003A30C
		public override eTermType GetPrimaryTermType(Localize cmp)
		{
			return eTermType.Sprite;
		}

		// Token: 0x060017BE RID: 6078 RVA: 0x0003BF0F File Offset: 0x0003A30F
		public override eTermType GetSecondaryTermType(Localize cmp)
		{
			return eTermType.Text;
		}

		// Token: 0x060017BF RID: 6079 RVA: 0x0003BF12 File Offset: 0x0003A312
		public override bool CanUseSecondaryTerm()
		{
			return false;
		}

		// Token: 0x060017C0 RID: 6080 RVA: 0x0003BF15 File Offset: 0x0003A315
		public override bool AllowMainTermToBeRTL()
		{
			return false;
		}

		// Token: 0x060017C1 RID: 6081 RVA: 0x0003BF18 File Offset: 0x0003A318
		public override bool AllowSecondTermToBeRTL()
		{
			return false;
		}

		// Token: 0x060017C2 RID: 6082 RVA: 0x0003BF1B File Offset: 0x0003A31B
		public override void GetFinalTerms(Localize cmp, string Main, string Secondary, out string primaryTerm, out string secondaryTerm)
		{
			primaryTerm = ((!(this.mTarget.sprite != null)) ? string.Empty : this.mTarget.sprite.name);
			secondaryTerm = null;
		}

		// Token: 0x060017C3 RID: 6083 RVA: 0x0003BF54 File Offset: 0x0003A354
		public override void DoLocalize(Localize cmp, string mainTranslation, string secondaryTranslation)
		{
			Sprite sprite = this.mTarget.sprite;
			if (sprite == null || sprite.name != mainTranslation)
			{
				this.mTarget.sprite = cmp.FindTranslatedObject<Sprite>(mainTranslation);
			}
		}
	}
}
