using System;
using UnityEngine;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x0200090F RID: 2319
	public class SimpleBouncer : MonoBehaviour
	{
		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x06003E28 RID: 15912 RVA: 0x001175B1 File Offset: 0x001159B1
		// (set) Token: 0x06003E27 RID: 15911 RVA: 0x0011757C File Offset: 0x0011597C
		public bool bounce
		{
			get
			{
				return base.enabled;
			}
			set
			{
				base.enabled = value;
				if (!value && this.originalScale != Vector3.zero)
				{
					base.transform.localScale = this.originalScale;
				}
			}
		}

		// Token: 0x06003E29 RID: 15913 RVA: 0x001175B9 File Offset: 0x001159B9
		private void Awake()
		{
			this.originalScale = base.transform.localScale;
		}

		// Token: 0x06003E2A RID: 15914 RVA: 0x001175CC File Offset: 0x001159CC
		private void OnEnable()
		{
			this.startTime = Time.unscaledTime;
		}

		// Token: 0x06003E2B RID: 15915 RVA: 0x001175DC File Offset: 0x001159DC
		private void Update()
		{
			float num = Time.unscaledTime - this.startTime;
			float time = num / this.period % 1f;
			base.transform.localScale = this.originalScale * (1f + this.profile.Evaluate(time) * this.magnitude);
		}

		// Token: 0x04002B74 RID: 11124
		[SerializeField]
		private float period = 2f;

		// Token: 0x04002B75 RID: 11125
		[SerializeField]
		private float magnitude = 0.1f;

		// Token: 0x04002B76 RID: 11126
		[SerializeField]
		private AnimationCurve profile = AnimationCurve.Linear(0f, 0f, 1f, 0f);

		// Token: 0x04002B77 RID: 11127
		private Vector3 originalScale;

		// Token: 0x04002B78 RID: 11128
		private float startTime;
	}
}
