using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Voxels.TowerDefense.HeroGeneration;

namespace Voxels.TowerDefense.CampaignGeneration
{
	// Token: 0x020006F0 RID: 1776
	public class HeroGenerator : MonoBehaviour, Campaign.ICampaignCreator, Campaign.ICampaignGenerator, Campaign.ICampaignDestroy
	{
		// Token: 0x06002E01 RID: 11777 RVA: 0x000B2C48 File Offset: 0x000B1048
		IEnumerator Campaign.ICampaignCreator.OnCampaigCreation(Campaign campaign, ProtoCampaign protoCampaign)
		{
			UnityEngine.Random.InitState(Profile.campaign.seed);
			foreach (object h in HeroGeneratorUI.instance.RandomizeOtherHeroes(campaign.campaignSave.heroes))
			{
				yield return null;
			}
			yield break;
		}

		// Token: 0x06002E02 RID: 11778 RVA: 0x000B2C64 File Offset: 0x000B1064
		IEnumerator<GenInfo> Campaign.ICampaignGenerator.OnCampaignGeneration(Campaign campaign)
		{
			GenInfo info = new GenInfo("CampaignBackground", GenInfo.Mode.interruptable);
			UnityEngine.Random.InitState(campaign.seed);
			List<HeroDefinition> heroes = campaign.campaignSave.heroes;
			Transform heroContainer = base.gameObject.AddEmptyChild("Hero Container").transform;
			heroContainer.transform.localPosition = new Vector2(-40f, -60f);
			yield return new GenInfo("HeroGenerator", GenInfo.Mode.interruptable);
			int i = 0;
			foreach (HeroDefinition heroDef in heroes)
			{
				heroDef.propertyBank.PickString("Deluxe", () => "false");
				MonoHero monoHero = UnityEngine.Object.Instantiate<MonoHero>(HeroGeneratorUI.instance.monoHeroPrefab, heroContainer);
				Transform transform = monoHero.transform;
				Vector3 position = transform.position;
				Vector3 a = Vector3.down * 4f;
				int num;
				i = (num = i) + 1;
				transform.position = position + a * (float)num;
				monoHero.Setup(heroDef, heroDef.upgrades, HeroGeneratorUI.instance.swapSource, heroDef.id + campaign.seed, true);
				heroDef.monoHero = monoHero;
				foreach (SerializableHeroUpgrade serializableHeroUpgrade in heroDef.upgrades)
				{
					serializableHeroUpgrade.definition.OnAttachedToMonoHero(monoHero, serializableHeroUpgrade.level);
				}
				yield return new GenInfo("HeroGenerator", GenInfo.Mode.forceInterrupt);
			}
			IEnumerator enumerator3 = MonoHero.BakeSprites(from x in heroes
			select x.monoHero).GetEnumerator();
			try
			{
				while (enumerator3.MoveNext())
				{
					object b = enumerator3.Current;
					yield return info;
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator3 as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
			yield return info;
			yield break;
		}

		// Token: 0x06002E03 RID: 11779 RVA: 0x000B2C88 File Offset: 0x000B1088
		private float GetDistinguisedFloat(IEnumerable<float> existingFloats)
		{
			List<float> floatList = existingFloats.ToList<float>();
			int count = floatList.Count;
			if (count == 0)
			{
				return UnityEngine.Random.value;
			}
			if (count == 1)
			{
				return (existingFloats.First<float>() + 0.5f) % 1f;
			}
			floatList.Sort();
			floatList.Add(floatList.First<float>() + 1f);
			int num = (from i in Enumerable.Range(0, count)
			orderby floatList[i + 1] - floatList[i] descending
			select i).First<int>();
			return Mathf.Lerp(floatList[num], floatList[num + 1], UnityEngine.Random.Range(0.4f, 0.6f)) % 1f;
		}

		// Token: 0x06002E04 RID: 11780 RVA: 0x000B2D54 File Offset: 0x000B1154
		void Campaign.ICampaignDestroy.OnCampaignDestroy(Campaign campaign)
		{
			foreach (HeroDefinition heroDefinition in campaign.campaignSave.heroes)
			{
				heroDefinition.monoHero.Destroy();
			}
		}

		// Token: 0x04001E77 RID: 7799
		public const int numHeroes = 10;

		// Token: 0x04001E78 RID: 7800
		private const int numClassedHeroes = 5;

		// Token: 0x04001E79 RID: 7801
		public const int numStartingHeroes = 2;

		// Token: 0x04001E7A RID: 7802
		public Gradient gradient;

		// Token: 0x04001E7B RID: 7803
		[Header("Voices")]
		public HeroVoice[] maleVoices;

		// Token: 0x04001E7C RID: 7804
		public HeroVoice[] femaleVoices;
	}
}
