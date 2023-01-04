using System;
using UnityEngine;

namespace I2.Loc
{
	// Token: 0x020003FD RID: 1021
	public class LocalizeTarget_UnityStandard_MeshRenderer : LocalizeTarget<MeshRenderer>
	{
		// Token: 0x060017A2 RID: 6050 RVA: 0x0003BB8C File Offset: 0x00039F8C
		static LocalizeTarget_UnityStandard_MeshRenderer()
		{
			LocalizeTarget_UnityStandard_MeshRenderer.AutoRegister();
		}

		// Token: 0x060017A4 RID: 6052 RVA: 0x0003BB9C File Offset: 0x00039F9C
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void AutoRegister()
		{
			LocalizationManager.RegisterTarget(new LocalizeTargetDesc_Type<MeshRenderer, LocalizeTarget_UnityStandard_MeshRenderer>
			{
				Name = "MeshRenderer",
				Priority = 800
			});
		}

		// Token: 0x060017A5 RID: 6053 RVA: 0x0003BBCB File Offset: 0x00039FCB
		public override eTermType GetPrimaryTermType(Localize cmp)
		{
			return eTermType.Mesh;
		}

		// Token: 0x060017A6 RID: 6054 RVA: 0x0003BBCE File Offset: 0x00039FCE
		public override eTermType GetSecondaryTermType(Localize cmp)
		{
			return eTermType.Material;
		}

		// Token: 0x060017A7 RID: 6055 RVA: 0x0003BBD1 File Offset: 0x00039FD1
		public override bool CanUseSecondaryTerm()
		{
			return true;
		}

		// Token: 0x060017A8 RID: 6056 RVA: 0x0003BBD4 File Offset: 0x00039FD4
		public override bool AllowMainTermToBeRTL()
		{
			return false;
		}

		// Token: 0x060017A9 RID: 6057 RVA: 0x0003BBD7 File Offset: 0x00039FD7
		public override bool AllowSecondTermToBeRTL()
		{
			return false;
		}

		// Token: 0x060017AA RID: 6058 RVA: 0x0003BBDC File Offset: 0x00039FDC
		public override void GetFinalTerms(Localize cmp, string Main, string Secondary, out string primaryTerm, out string secondaryTerm)
		{
			if (this.mTarget == null)
			{
				string text;
				secondaryTerm = (text = null);
				primaryTerm = text;
			}
			else
			{
				MeshFilter component = this.mTarget.GetComponent<MeshFilter>();
				if (component == null || component.sharedMesh == null)
				{
					primaryTerm = null;
				}
				else
				{
					primaryTerm = component.sharedMesh.name;
				}
			}
			if (this.mTarget == null || this.mTarget.sharedMaterial == null)
			{
				secondaryTerm = null;
			}
			else
			{
				secondaryTerm = this.mTarget.sharedMaterial.name;
			}
		}

		// Token: 0x060017AB RID: 6059 RVA: 0x0003BC8C File Offset: 0x0003A08C
		public override void DoLocalize(Localize cmp, string mainTranslation, string secondaryTranslation)
		{
			Material secondaryTranslatedObj = cmp.GetSecondaryTranslatedObj<Material>(ref mainTranslation, ref secondaryTranslation);
			if (secondaryTranslatedObj != null && this.mTarget.sharedMaterial != secondaryTranslatedObj)
			{
				this.mTarget.material = secondaryTranslatedObj;
			}
			Mesh mesh = cmp.FindTranslatedObject<Mesh>(mainTranslation);
			MeshFilter component = this.mTarget.GetComponent<MeshFilter>();
			if (mesh != null && component.sharedMesh != mesh)
			{
				component.mesh = mesh;
			}
		}
	}
}
