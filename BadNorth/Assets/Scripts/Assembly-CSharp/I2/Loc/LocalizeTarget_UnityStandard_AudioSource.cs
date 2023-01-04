using System;
using UnityEngine;

namespace I2.Loc
{
	// Token: 0x020003F8 RID: 1016
	public class LocalizeTarget_UnityStandard_AudioSource : LocalizeTarget<AudioSource>
	{
		// Token: 0x06001777 RID: 6007 RVA: 0x0003B6D7 File Offset: 0x00039AD7
		static LocalizeTarget_UnityStandard_AudioSource()
		{
			LocalizeTarget_UnityStandard_AudioSource.AutoRegister();
		}

		// Token: 0x06001779 RID: 6009 RVA: 0x0003B6E8 File Offset: 0x00039AE8
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void AutoRegister()
		{
			LocalizationManager.RegisterTarget(new LocalizeTargetDesc_Type<AudioSource, LocalizeTarget_UnityStandard_AudioSource>
			{
				Name = "AudioSource",
				Priority = 100
			});
		}

		// Token: 0x0600177A RID: 6010 RVA: 0x0003B714 File Offset: 0x00039B14
		public override eTermType GetPrimaryTermType(Localize cmp)
		{
			return eTermType.AudioClip;
		}

		// Token: 0x0600177B RID: 6011 RVA: 0x0003B717 File Offset: 0x00039B17
		public override eTermType GetSecondaryTermType(Localize cmp)
		{
			return eTermType.Text;
		}

		// Token: 0x0600177C RID: 6012 RVA: 0x0003B71A File Offset: 0x00039B1A
		public override bool CanUseSecondaryTerm()
		{
			return false;
		}

		// Token: 0x0600177D RID: 6013 RVA: 0x0003B71D File Offset: 0x00039B1D
		public override bool AllowMainTermToBeRTL()
		{
			return false;
		}

		// Token: 0x0600177E RID: 6014 RVA: 0x0003B720 File Offset: 0x00039B20
		public override bool AllowSecondTermToBeRTL()
		{
			return false;
		}

		// Token: 0x0600177F RID: 6015 RVA: 0x0003B723 File Offset: 0x00039B23
		public override void GetFinalTerms(Localize cmp, string Main, string Secondary, out string primaryTerm, out string secondaryTerm)
		{
			primaryTerm = ((!this.mTarget.clip) ? string.Empty : this.mTarget.clip.name);
			secondaryTerm = null;
		}

		// Token: 0x06001780 RID: 6016 RVA: 0x0003B75C File Offset: 0x00039B5C
		public override void DoLocalize(Localize cmp, string mainTranslation, string secondaryTranslation)
		{
			bool flag = (this.mTarget.isPlaying || this.mTarget.loop) && Application.isPlaying;
			AudioClip clip = this.mTarget.clip;
			AudioClip audioClip = cmp.FindTranslatedObject<AudioClip>(mainTranslation);
			if (clip != audioClip)
			{
				this.mTarget.clip = audioClip;
			}
			if (flag && this.mTarget.clip)
			{
				this.mTarget.Play();
			}
		}
	}
}
