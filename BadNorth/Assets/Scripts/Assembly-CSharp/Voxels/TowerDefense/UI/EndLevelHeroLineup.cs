using System;
using System.Collections;
using RTM.Pools;
using UnityEngine;
using Voxels.TowerDefense.Upgrades;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x02000548 RID: 1352
	public class EndLevelHeroLineup : MonoBehaviour, IGameSetup
	{
		// Token: 0x06002329 RID: 9001 RVA: 0x00069FF4 File Offset: 0x000683F4
		void IGameSetup.OnGameAwake()
		{
			EndLevelHeroIcon[] componentsInChildren = base.GetComponentsInChildren<EndLevelHeroIcon>(true);
			this.heroIconPool = new LocalPool<EndLevelHeroIcon>(componentsInChildren, componentsInChildren[0].transform.parent);
			this.borderWidth = componentsInChildren[0].GetComponentInChildren<MaskedSprite>(true).borders[0].width;
			this.animator = base.GetComponent<Animator>();
		}

		// Token: 0x0600232A RID: 9002 RVA: 0x0006A050 File Offset: 0x00068450
		public void Prepare(LevelNode levelNode)
		{
			this.heroIconPool.ReturnAll();
			foreach (Squad squad in levelNode.island.english.allSquads)
			{
				EnglishSquad englishSquad = (EnglishSquad)squad;
				HeroDefinition hero = englishSquad.hero;
				EvacuateAbility upgrade = englishSquad.upgradeManager.GetUpgrade<EvacuateAbility>();
				bool active = upgrade && upgrade.state != EvacuateAbility.State.None;
				bool heroAlive = englishSquad.heroAlive;
				bool active2 = levelNode.heroDefinition == hero;
				EndLevelHeroIcon instance = this.heroIconPool.GetInstance();
				instance.maskedSprite.Set(hero.graphics);
				instance.nameText.Term = hero.nameTerm;
				instance.dead.gameObject.SetActive(!heroAlive);
				instance.blood.gameObject.SetActive(!heroAlive && Profile.userSettings.showBlood);
				instance.blood.transform.localRotation = Quaternion.Euler(0f, 0f, (float)UnityEngine.Random.Range(0, 360));
				instance.fled.gameObject.SetActive(active);
				instance.recruited.gameObject.SetActive(active2);
			}
		}

		// Token: 0x0600232B RID: 9003 RVA: 0x0006A1CC File Offset: 0x000685CC
		public IEnumerator AnimatedRoutine()
		{
			this.playing = true;
			FabricWrapper.PostEvent(this.showAudio);
			base.gameObject.SetActive(true);
			while (this.playing)
			{
				yield return null;
			}
			IEnumerator r = this.Routine();
			while (r.MoveNext())
			{
				yield return null;
			}
			for (float t = 0f; t < 0.6f; t += Time.unscaledDeltaTime)
			{
				yield return null;
			}
			this.animator.speed = 1f;
			while (this.animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
			{
				yield return null;
			}
			base.gameObject.SetActive(false);
			yield break;
		}

		// Token: 0x0600232C RID: 9004 RVA: 0x0006A1E8 File Offset: 0x000685E8
		public IEnumerator Routine()
		{
			for (float t = 0f; t < 0.6f; t += Time.unscaledDeltaTime)
			{
				yield return null;
			}
			for (int i = 0; i < this.heroIconPool.inUse.Count; i++)
			{
				EndLevelHeroIcon icon = this.heroIconPool.inUse[i];
				if (icon.dead.activeSelf)
				{
					icon.GetComponent<Animator>().Play("Dead");
					FabricWrapper.PostEvent(this.heroDeadAudio);
					for (float t2 = 0f; t2 < 0.6f; t2 += Time.unscaledDeltaTime)
					{
						yield return null;
					}
				}
			}
			for (float t3 = 0f; t3 < 0.6f; t3 += Time.unscaledDeltaTime)
			{
				yield return null;
			}
			yield break;
		}

		// Token: 0x0600232D RID: 9005 RVA: 0x0006A203 File Offset: 0x00068603
		public void DoLineup()
		{
			this.playing = false;
			this.animator.speed = 0f;
		}

		// Token: 0x040015B7 RID: 5559
		private LocalPool<EndLevelHeroIcon> heroIconPool;

		// Token: 0x040015B8 RID: 5560
		private Animator animator;

		// Token: 0x040015B9 RID: 5561
		private bool playing;

		// Token: 0x040015BA RID: 5562
		private float borderWidth;

		// Token: 0x040015BB RID: 5563
		private FabricEventReference showAudio = "UI/InGame/Heroes";

		// Token: 0x040015BC RID: 5564
		private FabricEventReference heroDeadAudio = "UI/InGame/HeroDie";
	}
}
