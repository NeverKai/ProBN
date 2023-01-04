using System;
using UnityEngine;
using UnityEngine.UI;

namespace I2.Loc
{
	// Token: 0x02000402 RID: 1026
	public class LocalizeTarget_UnityUI_Image : LocalizeTarget<Image>
	{
		// Token: 0x060017CE RID: 6094 RVA: 0x0003C18A File Offset: 0x0003A58A
		static LocalizeTarget_UnityUI_Image()
		{
			LocalizeTarget_UnityUI_Image.AutoRegister();
		}

		// Token: 0x060017D0 RID: 6096 RVA: 0x0003C19C File Offset: 0x0003A59C
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void AutoRegister()
		{
			LocalizationManager.RegisterTarget(new LocalizeTargetDesc_Type<Image, LocalizeTarget_UnityUI_Image>
			{
				Name = "Image",
				Priority = 100
			});
		}

		// Token: 0x060017D1 RID: 6097 RVA: 0x0003C1C8 File Offset: 0x0003A5C8
		public override bool CanUseSecondaryTerm()
		{
			return false;
		}

		// Token: 0x060017D2 RID: 6098 RVA: 0x0003C1CB File Offset: 0x0003A5CB
		public override bool AllowMainTermToBeRTL()
		{
			return false;
		}

		// Token: 0x060017D3 RID: 6099 RVA: 0x0003C1CE File Offset: 0x0003A5CE
		public override bool AllowSecondTermToBeRTL()
		{
			return false;
		}

		// Token: 0x060017D4 RID: 6100 RVA: 0x0003C1D1 File Offset: 0x0003A5D1
		public override eTermType GetPrimaryTermType(Localize cmp)
		{
			return (!(this.mTarget.sprite == null)) ? eTermType.Sprite : eTermType.Texture;
		}

		// Token: 0x060017D5 RID: 6101 RVA: 0x0003C1F0 File Offset: 0x0003A5F0
		public override eTermType GetSecondaryTermType(Localize cmp)
		{
			return eTermType.Text;
		}

		// Token: 0x060017D6 RID: 6102 RVA: 0x0003C1F4 File Offset: 0x0003A5F4
		public override void GetFinalTerms(Localize cmp, string Main, string Secondary, out string primaryTerm, out string secondaryTerm)
		{
			primaryTerm = ((!this.mTarget.mainTexture) ? string.Empty : this.mTarget.mainTexture.name);
			if (this.mTarget.sprite != null && this.mTarget.sprite.name != primaryTerm)
			{
				primaryTerm = primaryTerm + "." + this.mTarget.sprite.name;
			}
			secondaryTerm = null;
		}

		// Token: 0x060017D7 RID: 6103 RVA: 0x0003C28C File Offset: 0x0003A68C
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
