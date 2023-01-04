using System;
using System.Collections.Generic;
using UnityEngine;

namespace ArcConsistency
{
	// Token: 0x02000009 RID: 9
	public abstract class Domain : MonoBehaviour
	{
		// Token: 0x0600001A RID: 26 RVA: 0x00003347 File Offset: 0x00001747
		public List<float> GetInitialValues()
		{
			return this.GenerateValues();
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00003350 File Offset: 0x00001750
		public List<float> GetValues(Domain variableOverride, float valueOverride)
		{
			if (variableOverride == this)
			{
				if (this.overrideValue.Count == 0)
				{
					this.overrideValue.Add(valueOverride);
				}
				else
				{
					this.overrideValue[0] = valueOverride;
				}
				return this.overrideValue;
			}
			return this.values;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000033A4 File Offset: 0x000017A4
		public void PrepareDomain()
		{
			this.values.Clear();
			this.values.AddRange(this.GetInitialValues());
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000033C4 File Offset: 0x000017C4
		private string GetDomainString()
		{
			if (this.values.Count > 0)
			{
				string text = this.values[0].ToString();
				for (int i = 1; i < this.values.Count; i++)
				{
					text = text + ", " + this.values[i];
				}
				return text;
			}
			return "EMPTY";
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000343D File Offset: 0x0000183D
		public override string ToString()
		{
			return base.name + " " + this.GetDomainString();
		}

		// Token: 0x0600001F RID: 31
		protected abstract List<float> GenerateValues();

		// Token: 0x04000016 RID: 22
		[HideInInspector]
		[SerializeField]
		public List<float> values;

		// Token: 0x04000017 RID: 23
		[NonSerialized]
		public List<Arc> arcs = new List<Arc>();

		// Token: 0x04000018 RID: 24
		private List<float> overrideValue = new List<float>(1);
	}
}
