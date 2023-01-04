using System;
using UnityEngine;

namespace I2.Loc
{
	// Token: 0x020003FA RID: 1018
	public class LocalizeTarget_UnityStandard_Child : LocalizeTarget<GameObject>
	{
		// Token: 0x06001783 RID: 6019 RVA: 0x0003B7FC File Offset: 0x00039BFC
		static LocalizeTarget_UnityStandard_Child()
		{
			LocalizeTarget_UnityStandard_Child.AutoRegister();
		}

		// Token: 0x06001785 RID: 6021 RVA: 0x0003B80C File Offset: 0x00039C0C
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void AutoRegister()
		{
			LocalizationManager.RegisterTarget(new LocalizeTargetDesc_Child
			{
				Name = "Child",
				Priority = 200
			});
		}

		// Token: 0x06001786 RID: 6022 RVA: 0x0003B83B File Offset: 0x00039C3B
		public override bool IsValid(Localize cmp)
		{
			return cmp.transform.childCount > 1;
		}

		// Token: 0x06001787 RID: 6023 RVA: 0x0003B84B File Offset: 0x00039C4B
		public override eTermType GetPrimaryTermType(Localize cmp)
		{
			return eTermType.GameObject;
		}

		// Token: 0x06001788 RID: 6024 RVA: 0x0003B84E File Offset: 0x00039C4E
		public override eTermType GetSecondaryTermType(Localize cmp)
		{
			return eTermType.Text;
		}

		// Token: 0x06001789 RID: 6025 RVA: 0x0003B851 File Offset: 0x00039C51
		public override bool CanUseSecondaryTerm()
		{
			return false;
		}

		// Token: 0x0600178A RID: 6026 RVA: 0x0003B854 File Offset: 0x00039C54
		public override bool AllowMainTermToBeRTL()
		{
			return false;
		}

		// Token: 0x0600178B RID: 6027 RVA: 0x0003B857 File Offset: 0x00039C57
		public override bool AllowSecondTermToBeRTL()
		{
			return false;
		}

		// Token: 0x0600178C RID: 6028 RVA: 0x0003B85A File Offset: 0x00039C5A
		public override void GetFinalTerms(Localize cmp, string Main, string Secondary, out string primaryTerm, out string secondaryTerm)
		{
			primaryTerm = cmp.name;
			secondaryTerm = null;
		}

		// Token: 0x0600178D RID: 6029 RVA: 0x0003B86C File Offset: 0x00039C6C
		public override void DoLocalize(Localize cmp, string mainTranslation, string secondaryTranslation)
		{
			if (string.IsNullOrEmpty(mainTranslation))
			{
				return;
			}
			Transform transform = cmp.transform;
			string text = mainTranslation;
			int num = mainTranslation.LastIndexOfAny(LanguageSourceData.CategorySeparators);
			if (num >= 0)
			{
				text = text.Substring(num + 1);
			}
			for (int i = 0; i < transform.childCount; i++)
			{
				Transform child = transform.GetChild(i);
				child.gameObject.SetActive(child.name == text);
			}
		}
	}
}
