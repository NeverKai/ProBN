using System;
using UnityEngine;

namespace I2.Loc
{
	// Token: 0x020003FC RID: 1020
	public class LocalizeTarget_UnityStandard_GUITexture : LocalizeTarget<GUITexture>
	{
		// Token: 0x06001798 RID: 6040 RVA: 0x0003BAC1 File Offset: 0x00039EC1
		static LocalizeTarget_UnityStandard_GUITexture()
		{
			LocalizeTarget_UnityStandard_GUITexture.AutoRegister();
		}

		// Token: 0x0600179A RID: 6042 RVA: 0x0003BAD0 File Offset: 0x00039ED0
		[RuntimeInitializeOnLoadMethod]
		private static void AutoRegister()
		{
			LocalizationManager.RegisterTarget(new LocalizeTargetDesc_Type<GUITexture, LocalizeTarget_UnityStandard_GUITexture>
			{
				Name = "GUITexture",
				Priority = 100
			});
		}

		// Token: 0x0600179B RID: 6043 RVA: 0x0003BAFC File Offset: 0x00039EFC
		public override eTermType GetPrimaryTermType(Localize cmp)
		{
			return eTermType.Texture;
		}

		// Token: 0x0600179C RID: 6044 RVA: 0x0003BAFF File Offset: 0x00039EFF
		public override eTermType GetSecondaryTermType(Localize cmp)
		{
			return eTermType.Text;
		}

		// Token: 0x0600179D RID: 6045 RVA: 0x0003BB02 File Offset: 0x00039F02
		public override bool CanUseSecondaryTerm()
		{
			return false;
		}

		// Token: 0x0600179E RID: 6046 RVA: 0x0003BB05 File Offset: 0x00039F05
		public override bool AllowMainTermToBeRTL()
		{
			return false;
		}

		// Token: 0x0600179F RID: 6047 RVA: 0x0003BB08 File Offset: 0x00039F08
		public override bool AllowSecondTermToBeRTL()
		{
			return false;
		}

		// Token: 0x060017A0 RID: 6048 RVA: 0x0003BB0B File Offset: 0x00039F0B
		public override void GetFinalTerms(Localize cmp, string Main, string Secondary, out string primaryTerm, out string secondaryTerm)
		{
			primaryTerm = ((!this.mTarget.texture) ? string.Empty : this.mTarget.texture.name);
			secondaryTerm = null;
		}

		// Token: 0x060017A1 RID: 6049 RVA: 0x0003BB44 File Offset: 0x00039F44
		public override void DoLocalize(Localize cmp, string mainTranslation, string secondaryTranslation)
		{
			Texture texture = this.mTarget.texture;
			if (texture == null || texture.name != mainTranslation)
			{
				this.mTarget.texture = cmp.FindTranslatedObject<Texture>(mainTranslation);
			}
		}
	}
}
