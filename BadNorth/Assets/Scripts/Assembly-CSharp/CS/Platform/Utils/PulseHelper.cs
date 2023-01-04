using System;
using System.Collections;
using UnityEngine;

namespace CS.Platform.Utils
{
	// Token: 0x0200007D RID: 125
	public class PulseHelper : MonoBehaviour
	{
		// Token: 0x0600057F RID: 1407 RVA: 0x00016350 File Offset: 0x00014750
		public void StartHelper(int burstLimit, int sustainLimit)
		{
			this._pulser = new WaitForSecondsRealtime(Mathf.Max(15f / (float)burstLimit, 300f / (float)sustainLimit) + 0.05f);
			if (this._tracker == null)
			{
				this._tracker = base.StartCoroutine(this.LogicKeeper());
			}
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x000163A0 File Offset: 0x000147A0
		private void OnEnable()
		{
			if (this._pulser != null && this._tracker == null)
			{
				this._tracker = base.StartCoroutine(this.LogicKeeper());
			}
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x000163CA File Offset: 0x000147CA
		private void OnDisable()
		{
			this._tracker = null;
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x000163D4 File Offset: 0x000147D4
		private IEnumerator LogicKeeper()
		{
			bool free = false;
			while (this._pulser != null)
			{
				this._pulser.Reset();
				yield return this._pulser;
				free = true;
				while (free)
				{
					if (this.PulseLogic != null)
					{
						free = !this.PulseLogic();
					}
					if (free)
					{
						yield return null;
					}
				}
			}
			this._tracker = null;
			yield break;
		}

		// Token: 0x04000232 RID: 562
		public Func<bool> PulseLogic;

		// Token: 0x04000233 RID: 563
		private Coroutine _tracker;

		// Token: 0x04000234 RID: 564
		private WaitForSecondsRealtime _pulser;
	}
}
