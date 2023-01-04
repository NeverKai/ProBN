using System;
using System.Collections.Generic;
using System.Linq;
using ControlledRandomness;
using ControlledRandomness.Swapping;
using CS.Platform;
using SpriteComposing;
using UnityEngine;
using Voxels.TowerDefense.HeroGeneration;

namespace Voxels.TowerDefense.CampaignGeneration
{
	// Token: 0x020006F1 RID: 1777
	public class HeroGeneratorUI : MonoBehaviour, IGameSetup
	{
		// Token: 0x06002E06 RID: 11782 RVA: 0x000B341C File Offset: 0x000B181C
		void IGameSetup.OnGameAwake()
		{
			this.swapSource = this.componentMap.GetMappedCopy(base.transform);
			foreach (SeededComponent seededComponent in this.swapSource.EnumerateComponentsInChildren(false, true))
			{
				seededComponent.SetSeed(seededComponent.name.GetHashCode() + seededComponent.GetInstanceID());
			}
			base.gameObject.GetOrAddComponent<SpriteComposer>();
			this.spriteSwapper = base.gameObject.GetOrAddComponent<SpriteSwapper>();
			this.spriteSwapper.Setup(this.swapSource.transform);
			HeroGeneratorUI.instance = this;
			MonoHero.InitializePools();
		}

		// Token: 0x06002E07 RID: 11783 RVA: 0x000B34E4 File Offset: 0x000B18E4
		public void RandomizeStartingHero(HeroDefinition heroDef, List<SerializableHeroUpgrade> upgrades, bool hasDeluxe, ref int seed, int ds, HeroGeneratorUI.ChangeFlags changeFlags, List<HeroDefinition> otherHeroes)
		{
			bool flag = BasePlatformManager.Instance.HasDLC("DELUXE_ED_CONTENT");
			bool flag2 = 0 != (byte)(changeFlags & HeroGeneratorUI.ChangeFlags.Portrait);
			bool flag3 = 0 != (byte)(changeFlags & HeroGeneratorUI.ChangeFlags.Banner);
			hasDeluxe = (hasDeluxe && flag);
			heroDef.propertyBank.Clear();
			UnityEngine.Random.State state = UnityEngine.Random.state;
			bool flag4;
			float value;
			bool flag5;
			do
			{
				seed += ds;
				ds = ((ds >= 0) ? 1 : -1);
				UnityEngine.Random.InitState(seed);
				flag4 = (UnityEngine.Random.value < 0.7f);
				string[] array = (!flag4) ? HeroNameTerms.female : HeroNameTerms.male;
				heroDef.nameTerm = array[UnityEngine.Random.Range(0, array.Length)];
				value = UnityEngine.Random.value;
				flag5 = true;
				int num = 0;
				while (num < otherHeroes.Count && flag5)
				{
					if (otherHeroes[num] != heroDef)
					{
						if (heroDef.nameTerm == otherHeroes[num].nameTerm)
						{
							flag5 = false;
						}
						if (Mathf.Min(Mathf.Abs(value - otherHeroes[num].hue), Mathf.Abs(value - otherHeroes[num].hue + 1f)) < 0.2f)
						{
							flag5 = false;
						}
					}
					num++;
				}
			}
			while (!flag5);
			heroDef.color = this.gradient.Evaluate(value);
			heroDef.hue = value;
			HeroVoice[] array2 = (!flag4) ? this.femaleVoices : this.maleVoices;
			heroDef.voice = array2[UnityEngine.Random.Range(0, array2.Length)];
			heroDef.propertyBank.AddTag("Gender", (!flag4) ? "Female" : "Male");
			heroDef.propertyBank.AddTag("Deluxe", (!hasDeluxe) ? "false" : "true");
			heroDef.propertyBank.SetBool("MetaTrait", true);
			heroDef.monoHero.Setup(heroDef, upgrades, this.swapSource, seed, false);
			UnityEngine.Random.state = state;
		}

		// Token: 0x06002E08 RID: 11784 RVA: 0x000B3700 File Offset: 0x000B1B00
		public MonoHero GetStartingMonoHero()
		{
			MonoHero monoHero = UnityEngine.Object.Instantiate<MonoHero>(this.monoHeroPrefab, base.transform);
			monoHero.transform.position += Vector3.down * (float)base.transform.childCount * 6f;
			return monoHero;
		}

		// Token: 0x06002E09 RID: 11785 RVA: 0x000B3758 File Offset: 0x000B1B58
		public IEnumerable<object> RandomizeOtherHeroes(List<HeroDefinition> heroes)
		{
			List<float> hues = ListPool<float>.GetList(4);
			HeroUpgradeDefinition[] classes = (from u in ResourceList<HeroUpgradeDefinition>.list
			where u.typeEnum == HeroUpgradeTypeEnum.Class
			select u).ToArray<HeroUpgradeDefinition>();
			foreach (HeroDefinition heroDefinition in heroes)
			{
				hues.Add(heroDefinition.hue);
				hues.Add(heroDefinition.hue + 1f);
			}
			yield return null;
			for (int i = heroes.Count; i < 10; i++)
			{
				HeroDefinition heroDef = new HeroDefinition(i);
				heroes.Add(heroDef);
				heroDef.propertyBank.Clear();
				bool male = UnityEngine.Random.value < 0.7f;
				heroDef.propertyBank.AddTag("Gender", (!male) ? "Female" : "Male");
				string[] array = (!male) ? HeroNameTerms.female : HeroNameTerms.male;
				float num = float.MaxValue;
				foreach (string text in array)
				{
					float num2 = UnityEngine.Random.value;
					foreach (HeroDefinition heroDefinition2 in heroes)
					{
						if (heroDefinition2 != heroDef && text == heroDefinition2.nameTerm)
						{
							num2 += 1f;
						}
					}
					if (num2 < num)
					{
						heroDef.nameTerm = text;
						num = num2;
					}
				}
				yield return null;
				HeroVoice[] array2 = (!male) ? this.femaleVoices : this.maleVoices;
				float num3 = float.MaxValue;
				foreach (HeroVoice heroVoice in array2)
				{
					float num4 = UnityEngine.Random.value;
					foreach (HeroDefinition heroDefinition3 in heroes)
					{
						if (heroDefinition3 != heroDef && heroVoice == heroDefinition3.voice)
						{
							num4 += 1f;
						}
					}
					if (num4 < num3)
					{
						heroDef.voice = heroVoice;
						num3 = num4;
					}
				}
				yield return null;
				hues.Sort();
				int num5 = 0;
				for (int l = 0; l < hues.Count - 1; l++)
				{
					if (hues[l + 1] - hues[l] > hues[num5 + 1] - hues[num5])
					{
						num5 = l;
					}
				}
				heroDef.hue = Mathf.Lerp(hues[num5], hues[num5 + 1], UnityEngine.Random.Range(0.4f, 0.6f)) % 1f;
				hues.Add(heroDef.hue);
				hues.Add(heroDef.hue + 1f);
				heroDef.color = this.gradient.Evaluate(heroDef.hue);
				yield return null;
				if (i >= 6)
				{
					float num6 = float.MaxValue;
					HeroUpgradeDefinition def = classes[0];
					foreach (HeroUpgradeDefinition heroUpgradeDefinition in classes)
					{
						float num7 = UnityEngine.Random.value;
						foreach (HeroDefinition heroDefinition4 in heroes)
						{
							if (heroDefinition4 != heroDef && heroUpgradeDefinition == heroDefinition4.classUpgrade)
							{
								num7 += 1f;
							}
						}
						if (num7 < num6)
						{
							def = heroUpgradeDefinition;
							num6 = num7;
						}
					}
					int level = (i != 9) ? ((i < 7) ? 0 : 1) : 2;
					heroDef.SetupHeroState(false, null, new HeroUpgradeDefintionWithLevel(def, level), null);
				}
				yield return null;
			}
			hues.ReturnToListPool<float>();
			yield break;
		}

		// Token: 0x04001E7D RID: 7805
		[SerializeField]
		public MonoHero monoHeroPrefab;

		// Token: 0x04001E7E RID: 7806
		[SerializeField]
		private ComponentMap componentMap;

		// Token: 0x04001E7F RID: 7807
		public Gradient gradient;

		// Token: 0x04001E80 RID: 7808
		[Header("Voices")]
		public HeroVoice[] maleVoices;

		// Token: 0x04001E81 RID: 7809
		public HeroVoice[] femaleVoices;

		// Token: 0x04001E82 RID: 7810
		public GameObject swapSource;

		// Token: 0x04001E83 RID: 7811
		public SpriteSwapper spriteSwapper;

		// Token: 0x04001E84 RID: 7812
		public static HeroGeneratorUI instance;

		// Token: 0x04001E85 RID: 7813
		public const int numHeroes = 10;

		// Token: 0x04001E86 RID: 7814
		public const int numStartingHeroes = 2;

		// Token: 0x04001E87 RID: 7815
		private const int numClassedHeroes = 4;

		// Token: 0x04001E88 RID: 7816
		private const int numVeteranHeroes = 3;

		// Token: 0x020006F2 RID: 1778
		[Flags]
		public enum ChangeFlags : byte
		{
			// Token: 0x04001E8A RID: 7818
			None = 0,
			// Token: 0x04001E8B RID: 7819
			Banner = 1,
			// Token: 0x04001E8C RID: 7820
			Portrait = 2,
			// Token: 0x04001E8D RID: 7821
			All = 255
		}
	}
}
