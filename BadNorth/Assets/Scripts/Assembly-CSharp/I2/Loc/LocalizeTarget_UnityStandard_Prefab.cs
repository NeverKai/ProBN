using System;
using UnityEngine;

namespace I2.Loc
{
	// Token: 0x020003FF RID: 1023
	public class LocalizeTarget_UnityStandard_Prefab : LocalizeTarget<GameObject>
	{
		// Token: 0x060017AE RID: 6062 RVA: 0x0003BD15 File Offset: 0x0003A115
		static LocalizeTarget_UnityStandard_Prefab()
		{
			LocalizeTarget_UnityStandard_Prefab.AutoRegister();
		}

		// Token: 0x060017B0 RID: 6064 RVA: 0x0003BD24 File Offset: 0x0003A124
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void AutoRegister()
		{
			LocalizationManager.RegisterTarget(new LocalizeTargetDesc_Prefab
			{
				Name = "Prefab",
				Priority = 250
			});
		}

		// Token: 0x060017B1 RID: 6065 RVA: 0x0003BD53 File Offset: 0x0003A153
		public override bool IsValid(Localize cmp)
		{
			return true;
		}

		// Token: 0x060017B2 RID: 6066 RVA: 0x0003BD56 File Offset: 0x0003A156
		public override eTermType GetPrimaryTermType(Localize cmp)
		{
			return eTermType.GameObject;
		}

		// Token: 0x060017B3 RID: 6067 RVA: 0x0003BD59 File Offset: 0x0003A159
		public override eTermType GetSecondaryTermType(Localize cmp)
		{
			return eTermType.Text;
		}

		// Token: 0x060017B4 RID: 6068 RVA: 0x0003BD5C File Offset: 0x0003A15C
		public override bool CanUseSecondaryTerm()
		{
			return false;
		}

		// Token: 0x060017B5 RID: 6069 RVA: 0x0003BD5F File Offset: 0x0003A15F
		public override bool AllowMainTermToBeRTL()
		{
			return false;
		}

		// Token: 0x060017B6 RID: 6070 RVA: 0x0003BD62 File Offset: 0x0003A162
		public override bool AllowSecondTermToBeRTL()
		{
			return false;
		}

		// Token: 0x060017B7 RID: 6071 RVA: 0x0003BD65 File Offset: 0x0003A165
		public override void GetFinalTerms(Localize cmp, string Main, string Secondary, out string primaryTerm, out string secondaryTerm)
		{
			primaryTerm = cmp.name;
			secondaryTerm = null;
		}

		// Token: 0x060017B8 RID: 6072 RVA: 0x0003BD74 File Offset: 0x0003A174
		public override void DoLocalize(Localize cmp, string mainTranslation, string secondaryTranslation)
		{
			if (string.IsNullOrEmpty(mainTranslation))
			{
				return;
			}
			if (this.mTarget && this.mTarget.name == mainTranslation)
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
			Transform transform2 = this.InstantiateNewPrefab(cmp, mainTranslation);
			if (transform2 == null)
			{
				return;
			}
			transform2.name = text;
			for (int i = transform.childCount - 1; i >= 0; i--)
			{
				Transform child = transform.GetChild(i);
				if (child != transform2)
				{
					UnityEngine.Object.Destroy(child.gameObject);
				}
			}
		}

		// Token: 0x060017B9 RID: 6073 RVA: 0x0003BE38 File Offset: 0x0003A238
		private Transform InstantiateNewPrefab(Localize cmp, string mainTranslation)
		{
			GameObject gameObject = cmp.FindTranslatedObject<GameObject>(mainTranslation);
			if (gameObject == null)
			{
				return null;
			}
			GameObject mTarget = this.mTarget;
			this.mTarget = UnityEngine.Object.Instantiate<GameObject>(gameObject);
			if (this.mTarget == null)
			{
				return null;
			}
			Transform transform = cmp.transform;
			Transform transform2 = this.mTarget.transform;
			transform2.SetParent(transform);
			Transform transform3 = (!mTarget) ? transform : mTarget.transform;
			transform2.rotation = transform3.rotation;
			transform2.position = transform3.position;
			return transform2;
		}
	}
}
