using System;
using UnityEngine;
using UnityEngine.UI;

namespace I2.Loc
{
	// Token: 0x02000403 RID: 1027
	public class LocalizeTarget_UnityUI_RawImage : LocalizeTarget<RawImage>
	{
		// Token: 0x060017D8 RID: 6104 RVA: 0x0003C2D4 File Offset: 0x0003A6D4
		static LocalizeTarget_UnityUI_RawImage()
		{
			LocalizeTarget_UnityUI_RawImage.AutoRegister();
		}

		// Token: 0x060017DA RID: 6106 RVA: 0x0003C2E4 File Offset: 0x0003A6E4
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void AutoRegister()
		{
			LocalizationManager.RegisterTarget(new LocalizeTargetDesc_Type<RawImage, LocalizeTarget_UnityUI_RawImage>
			{
				Name = "RawImage",
				Priority = 100
			});
		}

		// Token: 0x060017DB RID: 6107 RVA: 0x0003C310 File Offset: 0x0003A710
		public override eTermType GetPrimaryTermType(Localize cmp)
		{
			return eTermType.Texture;
		}

		// Token: 0x060017DC RID: 6108 RVA: 0x0003C313 File Offset: 0x0003A713
		public override eTermType GetSecondaryTermType(Localize cmp)
		{
			return eTermType.Text;
		}

		// Token: 0x060017DD RID: 6109 RVA: 0x0003C316 File Offset: 0x0003A716
		public override bool CanUseSecondaryTerm()
		{
			return false;
		}

		// Token: 0x060017DE RID: 6110 RVA: 0x0003C319 File Offset: 0x0003A719
		public override bool AllowMainTermToBeRTL()
		{
			return false;
		}

		// Token: 0x060017DF RID: 6111 RVA: 0x0003C31C File Offset: 0x0003A71C
		public override bool AllowSecondTermToBeRTL()
		{
			return false;
		}

		// Token: 0x060017E0 RID: 6112 RVA: 0x0003C31F File Offset: 0x0003A71F
		public override void GetFinalTerms(Localize cmp, string Main, string Secondary, out string primaryTerm, out string secondaryTerm)
		{
			primaryTerm = ((!this.mTarget.mainTexture) ? string.Empty : this.mTarget.mainTexture.name);
			secondaryTerm = null;
		}

		// Token: 0x060017E1 RID: 6113 RVA: 0x0003C358 File Offset: 0x0003A758
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
