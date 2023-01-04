using System;
using System.Collections;
using UnityEngine;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x02000546 RID: 1350
	public class EndLevelCard : MonoBehaviour
	{
		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06002319 RID: 8985 RVA: 0x00069DA0 File Offset: 0x000681A0
		public bool playing
		{
			get
			{
				return this.animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f;
			}
		}

		// Token: 0x0600231A RID: 8986 RVA: 0x00069DC8 File Offset: 0x000681C8
		public void StartKillStats()
		{
			base.StartCoroutine(this.PauseRoutine(this.enemyLineup.EndOfLevelRoutine()));
		}

		// Token: 0x0600231B RID: 8987 RVA: 0x00069DE2 File Offset: 0x000681E2
		public void StartHeroLineupStats()
		{
			base.StartCoroutine(this.PauseRoutine(this.heroLineup.Routine()));
		}

		// Token: 0x0600231C RID: 8988 RVA: 0x00069DFC File Offset: 0x000681FC
		private IEnumerator PauseRoutine(IEnumerator r)
		{
			this.animator.speed = 0f;
			while (r.MoveNext())
			{
				yield return null;
			}
			this.animator.speed = 1f;
			yield break;
		}

		// Token: 0x0600231D RID: 8989 RVA: 0x00069E20 File Offset: 0x00068220
		public void Prepare(LevelNode levelNode)
		{
			if (!this.animator)
			{
				this.enemyLineup = base.GetComponentInChildren<EnemyLineup>(true);
				this.heroLineup = base.GetComponentInChildren<EndLevelHeroLineup>(true);
				this.animator = base.GetComponent<Animator>();
			}
			if (this.heroLineup)
			{
				this.heroLineup.Prepare(levelNode);
			}
			base.gameObject.SetActive(true);
			this.enemyLineup.Prepare();
		}

		// Token: 0x0600231E RID: 8990 RVA: 0x00069E96 File Offset: 0x00068296
		public void VictorySplash()
		{
			FabricWrapper.PostEvent(this.victoryAppearAudio);
		}

		// Token: 0x0600231F RID: 8991 RVA: 0x00069EA4 File Offset: 0x000682A4
		public void EnemyLineupStart()
		{
			FabricWrapper.PostEvent(EndLevelCard.showEnemyLineupAudio);
		}

		// Token: 0x06002320 RID: 8992 RVA: 0x00069EB1 File Offset: 0x000682B1
		public void DisappearStart()
		{
			FabricWrapper.PostEvent(this.disappearAudio);
		}

		// Token: 0x06002321 RID: 8993 RVA: 0x00069EBF File Offset: 0x000682BF
		public void PlayCointSound()
		{
			FabricWrapper.PostEvent("UI/InGame/VictoryCoin");
		}

		// Token: 0x040015A9 RID: 5545
		[SerializeField]
		public GameObject coin;

		// Token: 0x040015AA RID: 5546
		[SerializeField]
		public GameObject coinFlair;

		// Token: 0x040015AB RID: 5547
		private Animator animator;

		// Token: 0x040015AC RID: 5548
		private EnemyLineup enemyLineup;

		// Token: 0x040015AD RID: 5549
		private EndLevelHeroLineup heroLineup;

		// Token: 0x040015AE RID: 5550
		[SerializeField]
		private FabricEventReference victoryAppearAudio = "UI/InGame/VictorySplash";

		// Token: 0x040015AF RID: 5551
		private static FabricEventReference showEnemyLineupAudio = "UI/InGame/EnemyFaceCard";

		// Token: 0x040015B0 RID: 5552
		[SerializeField]
		private FabricEventReference disappearAudio = "UI/InGame/VictoryDespawn";
	}
}
