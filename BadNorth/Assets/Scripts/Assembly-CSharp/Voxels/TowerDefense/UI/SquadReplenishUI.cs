using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x020007EF RID: 2031
	[SelectionBase]
	public class SquadReplenishUI : MonoBehaviour, IIslandEnter, IIslandWipe
	{
		// Token: 0x0600351D RID: 13597 RVA: 0x000E48BC File Offset: 0x000E2CBC
		private void Awake()
		{
			this.visibility.gameObject.SetActive(false);
			this.location.onAgentEntered += this.Location_onAgentEntered;
			this.location.onAgentHealed += this.Location_onAgentHealed;
			this.location.onSquadBeginExit += this.Location_onSquadBeginExit;
		}

		// Token: 0x0600351E RID: 13598 RVA: 0x000E491F File Offset: 0x000E2D1F
		private void Update()
		{
			if (this.visibility.gameObject.activeInHierarchy)
			{
				this.visibility.rotation = Quaternion.LookRotation(Singleton<LevelCamera>.instance.cameraRef.transform.forward, Vector3.up);
			}
		}

		// Token: 0x0600351F RID: 13599 RVA: 0x000E4960 File Offset: 0x000E2D60
		private void Location_onAgentEntered()
		{
			if (!this.inUse)
			{
				this.inUse = true;
				this.visibility.gameObject.SetActive(true);
				base.StopAllCoroutines();
				base.StartCoroutine(SquadReplenishUI.AnimateExpScaleTo(this.scaler, 1f, 15f, null));
				this.startUnitsProgress.ResetRatio(0f);
				this.healedUnitsProgress.ResetRatio(0f);
				Color color = this.location.currentSquad.hero.color;
				this.startUnitsProgress.image.color = color * new Color(0.75f, 0.75f, 0.75f, 1f);
				this.healedUnitsProgress.image.color = color * 1.2f;
			}
			this.UpdateAgentDisplay();
		}

		// Token: 0x06003520 RID: 13600 RVA: 0x000E4A39 File Offset: 0x000E2E39
		private void Location_onAgentHealed(int totalHealed)
		{
			this.healedUnits = totalHealed;
			this.UpdateAgentDisplay();
		}

		// Token: 0x06003521 RID: 13601 RVA: 0x000E4A48 File Offset: 0x000E2E48
		private void Location_onSquadBeginExit()
		{
			this.inUse = false;
			this.healedUnits = 0;
			base.StopAllCoroutines();
			base.StartCoroutine(SquadReplenishUI.AnimateExpScaleTo(this.scaler, 0f, 7.5f, delegate(Transform trans)
			{
				this.visibility.gameObject.SetActive(false);
			}));
		}

		// Token: 0x06003522 RID: 13602 RVA: 0x000E4A88 File Offset: 0x000E2E88
		private void UpdateAgentDisplay()
		{
			this.startUnitsProgress.ratio = (float)this.location.numContainedAgents / (float)this.location.currentSquad.maxCount;
			this.healedUnitsProgress.ratio = (float)this.healedUnits / (float)this.location.currentSquad.maxCount;
		}

		// Token: 0x06003523 RID: 13603 RVA: 0x000E4AE4 File Offset: 0x000E2EE4
		private static IEnumerator AnimateExpScaleTo(Transform transform, float target, float exponent, Action<Transform> completeCallback = null)
		{
			float scale = transform.localScale.y;
			float sign = Mathf.Sign(target - scale);
			float threashold = target - 0.001f * sign;
			while (scale * sign < threashold * sign)
			{
				scale = Mathf.Lerp(scale, target, Time.unscaledDeltaTime * exponent);
				transform.SetLocalScale(scale);
				yield return null;
			}
			transform.SetLocalScale(target);
			if (completeCallback != null)
			{
				completeCallback(transform);
			}
			yield break;
		}

		// Token: 0x06003524 RID: 13604 RVA: 0x000E4B14 File Offset: 0x000E2F14
		IEnumerator<GenInfo> IIslandEnter.OnIslandEnter(Island island)
		{
			this.ResetDisplay();
			yield return new GenInfo("replenishUI", GenInfo.Mode.interruptable);
			yield break;
		}

		// Token: 0x06003525 RID: 13605 RVA: 0x000E4B30 File Offset: 0x000E2F30
		IEnumerator<GenInfo> IIslandWipe.OnIslandWipe(Island island)
		{
			this.ResetDisplay();
			yield return new GenInfo("replenishUI", GenInfo.Mode.interruptable);
			yield break;
		}

		// Token: 0x06003526 RID: 13606 RVA: 0x000E4B4B File Offset: 0x000E2F4B
		private void ResetDisplay()
		{
			base.transform.CorrectNegativeScale();
			this.scaler.SetLocalScale(0f);
			this.visibility.gameObject.SetActive(false);
			this.inUse = false;
			this.healedUnits = 0;
		}

		// Token: 0x04002417 RID: 9239
		[SerializeField]
		private SquadReplenishLocation location;

		// Token: 0x04002418 RID: 9240
		[SerializeField]
		private Transform scaler;

		// Token: 0x04002419 RID: 9241
		[SerializeField]
		private Transform visibility;

		// Token: 0x0400241A RID: 9242
		[SerializeField]
		private ProgressBar startUnitsProgress;

		// Token: 0x0400241B RID: 9243
		[SerializeField]
		private ProgressBar healedUnitsProgress;

		// Token: 0x0400241C RID: 9244
		private bool inUse;

		// Token: 0x0400241D RID: 9245
		private int healedUnits;
	}
}
