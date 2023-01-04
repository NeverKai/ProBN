using System;
using System.Collections.Generic;
using UnityEngine;

namespace I2.Loc
{
	// Token: 0x02000407 RID: 1031
	[Serializable]
	public class TermData
	{
		// Token: 0x060017EE RID: 6126 RVA: 0x0003C6C4 File Offset: 0x0003AAC4
		public string GetTranslation(int idx, string specialization = null, bool editMode = false)
		{
			string text = this.Languages[idx];
			if (text != null)
			{
				text = SpecializationManager.GetSpecializedText(text, specialization);
				if (!editMode)
				{
					text = text.Replace("[i2nt]", string.Empty).Replace("[/i2nt]", string.Empty);
				}
			}
			return text;
		}

		// Token: 0x060017EF RID: 6127 RVA: 0x0003C70F File Offset: 0x0003AB0F
		public void SetTranslation(int idx, string translation, string specialization = null)
		{
			this.Languages[idx] = SpecializationManager.SetSpecializedText(this.Languages[idx], translation, specialization);
		}

		// Token: 0x060017F0 RID: 6128 RVA: 0x0003C728 File Offset: 0x0003AB28
		public void RemoveSpecialization(string specialization)
		{
			for (int i = 0; i < this.Languages.Length; i++)
			{
				this.RemoveSpecialization(i, specialization);
			}
		}

		// Token: 0x060017F1 RID: 6129 RVA: 0x0003C758 File Offset: 0x0003AB58
		public void RemoveSpecialization(int idx, string specialization)
		{
			string text = this.Languages[idx];
			if (specialization == "Any" || !text.Contains("[i2s_" + specialization + "]"))
			{
				return;
			}
			Dictionary<string, string> specializations = SpecializationManager.GetSpecializations(text, null);
			specializations.Remove(specialization);
			this.Languages[idx] = SpecializationManager.SetSpecializedText(specializations);
		}

		// Token: 0x060017F2 RID: 6130 RVA: 0x0003C7B8 File Offset: 0x0003ABB8
		public bool IsAutoTranslated(int idx, bool IsTouch)
		{
			return (this.Flags[idx] & 2) > 0;
		}

		// Token: 0x060017F3 RID: 6131 RVA: 0x0003C7C8 File Offset: 0x0003ABC8
		public void Validate()
		{
			int num = Mathf.Max(this.Languages.Length, this.Flags.Length);
			if (this.Languages.Length != num)
			{
				Array.Resize<string>(ref this.Languages, num);
			}
			if (this.Flags.Length != num)
			{
				Array.Resize<byte>(ref this.Flags, num);
			}
			if (this.Languages_Touch != null)
			{
				for (int i = 0; i < Mathf.Min(this.Languages_Touch.Length, num); i++)
				{
					if (string.IsNullOrEmpty(this.Languages[i]) && !string.IsNullOrEmpty(this.Languages_Touch[i]))
					{
						this.Languages[i] = this.Languages_Touch[i];
						this.Languages_Touch[i] = null;
					}
				}
				this.Languages_Touch = null;
			}
		}

		// Token: 0x060017F4 RID: 6132 RVA: 0x0003C88D File Offset: 0x0003AC8D
		public bool IsTerm(string name, bool allowCategoryMistmatch)
		{
			if (!allowCategoryMistmatch)
			{
				return name == this.Term;
			}
			return name == LanguageSourceData.GetKeyFromFullTerm(this.Term, false);
		}

		// Token: 0x060017F5 RID: 6133 RVA: 0x0003C8B4 File Offset: 0x0003ACB4
		public bool HasSpecializations()
		{
			for (int i = 0; i < this.Languages.Length; i++)
			{
				if (!string.IsNullOrEmpty(this.Languages[i]) && this.Languages[i].Contains("[i2s_"))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060017F6 RID: 6134 RVA: 0x0003C908 File Offset: 0x0003AD08
		public List<string> GetAllSpecializations()
		{
			List<string> list = new List<string>();
			for (int i = 0; i < this.Languages.Length; i++)
			{
				SpecializationManager.AppendSpecializations(this.Languages[i], list);
			}
			return list;
		}

		// Token: 0x04000EA6 RID: 3750
		public string Term = string.Empty;

		// Token: 0x04000EA7 RID: 3751
		public eTermType TermType;

		// Token: 0x04000EA8 RID: 3752
		public string Description;

		// Token: 0x04000EA9 RID: 3753
		public string[] Languages = new string[0];

		// Token: 0x04000EAA RID: 3754
		public byte[] Flags = new byte[0];

		// Token: 0x04000EAB RID: 3755
		[SerializeField]
		private string[] Languages_Touch;
	}
}
