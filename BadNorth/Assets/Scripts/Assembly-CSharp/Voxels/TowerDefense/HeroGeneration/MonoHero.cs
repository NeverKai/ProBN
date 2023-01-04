using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ControlledRandomness;
using ControlledRandomness.Swapping;
using SpriteComposing;
using Spriteshop;
using UnityEngine;
using Voxels.TowerDefense.CampaignGeneration;
using Voxels.TowerDefense.SpriteMagic;

namespace Voxels.TowerDefense.HeroGeneration
{
	// Token: 0x02000770 RID: 1904
	public class MonoHero : MonoBehaviour, IHeroGraphics
	{
		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x06003164 RID: 12644 RVA: 0x000CC640 File Offset: 0x000CAA40
		Sprite IHeroGraphics.flag
		{
			get
			{
				return this.bannerSpriteGen.sprite;
			}
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x06003165 RID: 12645 RVA: 0x000CC64D File Offset: 0x000CAA4D
		Sprite IHeroGraphics.agentSpriteMinion
		{
			get
			{
				this.MaybeUpdateHero();
				return this.agentSpriteMinionGen.sprite;
			}
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x06003166 RID: 12646 RVA: 0x000CC660 File Offset: 0x000CAA60
		Sprite IHeroGraphics.agentSpriteHero
		{
			get
			{
				this.MaybeUpdateHero();
				return this.agentSpriteHeroGen.sprite;
			}
		}

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x06003167 RID: 12647 RVA: 0x000CC673 File Offset: 0x000CAA73
		Sprite IHeroGraphics.iconHead
		{
			get
			{
				return this.iconHead.sprite;
			}
		}

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x06003168 RID: 12648 RVA: 0x000CC680 File Offset: 0x000CAA80
		Sprite IHeroGraphics.iconBackground
		{
			get
			{
				this.MaybeUpdateHero();
				return this.iconBackground.sprite;
			}
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x06003169 RID: 12649 RVA: 0x000CC693 File Offset: 0x000CAA93
		// (set) Token: 0x0600316A RID: 12650 RVA: 0x000CC69B File Offset: 0x000CAA9B
		public Agent minionPrefab { get; private set; }

		// Token: 0x140000A0 RID: 160
		// (add) Token: 0x0600316B RID: 12651 RVA: 0x000CC6A4 File Offset: 0x000CAAA4
		// (remove) Token: 0x0600316C RID: 12652 RVA: 0x000CC6DC File Offset: 0x000CAADC
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<Agent> onMinionPrefabChanged = delegate(Agent A_0)
		{
		};

		// Token: 0x0600316D RID: 12653 RVA: 0x000CC714 File Offset: 0x000CAB14
		public void UpdateLevel(int level)
		{
			this.onLevel(level);
			foreach (MonoHero.LevelColor levelColor in this.levelColors)
			{
				levelColor.SetLevel(level);
			}
			if (this.minionPrefab)
			{
				this.minionPrefab.SetLevel(this.heroDef.squadLevel);
			}
			this.minionPrefab.UpdateVisuals();
			this.agentSpriteMinionGen.Draw();
			this.agentSpriteHeroGen.Draw();
			this.DrawMinion();
			this.iconBackground.MaybeRedraw();
			this.iconHead.Draw();
		}

		// Token: 0x0600316E RID: 12654 RVA: 0x000CC7B8 File Offset: 0x000CABB8
		public void SetMinionPrefab(Agent agentPrefab)
		{
			if (this.minionPrefab)
			{
				UnityEngine.Object.Destroy(this.minionPrefab.gameObject);
			}
			this.agentSpriteMinionGen.SetDirty();
			this.agentSpriteHeroGen.SetDirty();
			this.minionPrefab = UnityEngine.Object.Instantiate<Agent>(agentPrefab, this.agentContainer, false);
			this.minionPrefab.transform.localPosition = Vector3.zero;
			this.minionPrefab.GetComponentInChildren<SpriteAnimator>(true).SetSprite2(this.agentSpriteMinionGen.sprite);
			this.minionPrefab.UpdateVisuals();
			this.DrawMinion();
			this.iconBackground.MaybeRedraw();
			this.onMinionPrefabChanged(this.minionPrefab);
		}

		// Token: 0x0600316F RID: 12655 RVA: 0x000CC86C File Offset: 0x000CAC6C
		private void DrawMinion()
		{
			Singleton<AgentTextureBaker>.instance.Draw(this.minionPrefab, 0f, 0.7f, this.agentTexture);
		}

		// Token: 0x06003170 RID: 12656 RVA: 0x000CC890 File Offset: 0x000CAC90
		private void MaybeInitialize()
		{
			if (this.Initialized)
			{
				return;
			}
			this.Initialized = true;
			foreach (MonoHero.LevelColor levelColor in this.levelColors)
			{
				levelColor.color = levelColor.Get(0);
			}
			foreach (MonoHero.KeyedColor keyedColor in this.colors)
			{
				this.colorDict.Add(keyedColor.key, keyedColor);
			}
			foreach (MonoHero.LevelColor levelColor2 in this.levelColors)
			{
				this.colorDict.Add(levelColor2.key, levelColor2);
			}
			foreach (MonoHero.SubscribableColor subscribableColor in this.simpleColors)
			{
				this.colorDict.Add(subscribableColor.key, subscribableColor);
			}
			this.headRect = this.iconHead.rect;
			this.colorDict.Add("Banner", new MonoHero.SubscribableColor
			{
				color = Color.white,
				key = "Banner"
			});
			this.colorDict.Add("DimPattern", new MonoHero.SubscribableColor
			{
				color = Color.white,
				key = "DimPattern"
			});
			this.agentContainer.gameObject.SetActive(false);
			this.agentTexture = new RenderTexture(256, 256, 24);
			this.agentBlock = new MaterialPropertyBlock();
			this.agentBlock.SetTexture(ShaderId.mainTexId, this.agentTexture);
			this.taggedObjects.Add("Alive", new List<GameObject>());
			this.taggedObjects.Add("Dead", new List<GameObject>());
			this.taggedObjects.Add("Blood", new List<GameObject>());
			this.ranks = base.GetComponentsInChildren<SpriteRenderReference>(true);
			this.SetMinionPrefab(this.defaultMinionPrefab);
		}

		// Token: 0x06003171 RID: 12657 RVA: 0x000CCAA6 File Offset: 0x000CAEA6
		public static void InitializePools()
		{
			ListPool<PsdGroup>.Initialize(32, 1);
			ComponentEnumerator.Initialize<SpriteRenderer>(32, 1);
			ComponentEnumerator.Initialize<HeroTag>(16, 1);
			ComponentEnumerator.Initialize<HeroLevelDependent>(16, 1);
			ComponentEnumerator.Initialize<MonoHero.IHeroSetup>(8, 1);
			ComponentEnumerator.Initialize<IOnGenerate>(8, 1);
		}

		// Token: 0x06003172 RID: 12658 RVA: 0x000CCAD6 File Offset: 0x000CAED6
		public void UpdateRankCount()
		{
			this.UpdateRankCount(this.heroDef.upgrades);
		}

		// Token: 0x06003173 RID: 12659 RVA: 0x000CCAEC File Offset: 0x000CAEEC
		private void UpdateRankCount(List<SerializableHeroUpgrade> upgrades)
		{
			int num = 0;
			foreach (SerializableHeroUpgrade serializableHeroUpgrade in upgrades)
			{
				if (serializableHeroUpgrade != null)
				{
					MonoHero.IRankModifier rankModifier = serializableHeroUpgrade.definition as MonoHero.IRankModifier;
					if (rankModifier != null)
					{
						rankModifier.ModifyRank(ref num, serializableHeroUpgrade);
					}
				}
			}
			if (this.ranks[0].extraCount != num)
			{
				foreach (SpriteRenderReference spriteRenderReference in this.ranks)
				{
					spriteRenderReference.extraCount = num;
				}
				this.iconBackground.MaybeRedraw();
			}
		}

		// Token: 0x06003174 RID: 12660 RVA: 0x000CCBB0 File Offset: 0x000CAFB0
		public void MakeHero(PropertyBank propertyBank, GameObject swapSource, List<SerializableHeroUpgrade> upgrades, bool crop)
		{
			this.MaybeInitialize();
			HeroUpgradeDefinition heroUpgradeDefinition = null;
			foreach (SerializableHeroUpgrade serializableHeroUpgrade in upgrades)
			{
				if (serializableHeroUpgrade != null && serializableHeroUpgrade.definition != null && serializableHeroUpgrade.definition.typeEnum == HeroUpgradeTypeEnum.Trait)
				{
					heroUpgradeDefinition = serializableHeroUpgrade.definition;
				}
			}
			if (heroUpgradeDefinition != null && heroUpgradeDefinition.uniqueId.Contains("ExtraArmor"))
			{
				propertyBank.AddTag(new Tag("Base", "Helmut"));
			}
			if (this.srcHead)
			{
				foreach (IOnReset onReset in base.gameObject.EnumerateComponentsInChildren(true, false))
				{
					onReset.OnReset();
				}
				this.srcHead.transform.localPosition = Vector3.zero;
				this.srcHead.transform.localScale = Vector3.one;
				this.srcHead.transform.localRotation = Quaternion.identity;
			}
			MonoHero.KeyedColor[] array = this.colors;
			for (int i = 0; i < array.Length; i++)
			{
				MonoHero.KeyedColor keyColor = array[i];
				Color color = propertyBank.PickColor(keyColor.key, () => keyColor.gradient.Evaluate(UnityEngine.Random.value));
				keyColor.defaultColor = color;
				keyColor.PushColor(color);
			}
			HeroGeneratorUI.instance.spriteSwapper.Swap(propertyBank, base.transform, this.seed);
			this.srcHead = this.iconBackground.GetComponentInChildren<Swappable>().gameObject;
			float num = 0f;
			float num2 = 0f;
			foreach (PsdGroup psdGroup in base.gameObject.EnumerateComponentsInChildren(false, true))
			{
				float num3 = psdGroup.innerRect.width * psdGroup.innerRect.height;
				num += (psdGroup.transform.lossyScale.x + psdGroup.transform.lossyScale.y) / 2f * num3;
				num2 += num3;
			}
			num = num2 / num;
			num = Mathf.Lerp(num, 1f, 0.4f);
			this.srcHead.transform.localScale = Vector3.Scale(this.srcHead.transform.localScale, new Vector3(num, num, 1f));
			string empty = string.Empty;
			if (propertyBank.TryGetValue("Gender", ref empty))
			{
				this.srcHead.transform.localPosition += this.srcHead.transform.InverseTransformDirection(Vector3.up * ((!(empty == "Male")) ? -0.1f : 0.05f));
			}
			if (heroUpgradeDefinition is HeroTraitGiant)
			{
				this.srcHead.transform.localPosition = new Vector3(0.26f, 0.7f);
				this.srcHead.transform.localScale = new Vector3(1.15f, 1.15f, this.srcHead.transform.localScale.z);
			}
			foreach (HeroTag heroTag in this.agentSpriteMinionGen.EnumerateComponentsInChildren(true, true))
			{
				if (heroTag.key == "Hero")
				{
					heroTag.transform.SetParent(null);
					UnityEngine.Object.Destroy(heroTag.gameObject);
				}
			}
			foreach (HeroLevelDependent heroLevelDependent in this.agentSpriteHeroGen.EnumerateComponentsInChildren(true, true))
			{
				if (heroLevelDependent.maxLevel == 0)
				{
					UnityEngine.Object.Destroy(heroLevelDependent.gameObject);
				}
				else
				{
					heroLevelDependent.minLevel--;
				}
			}
			this.RecursiveSetup(base.gameObject, propertyBank);
			if (crop)
			{
				this.iconHead.pivot = new Vector2(0.5f, (!(heroUpgradeDefinition is HeroTraitGiant)) ? 0.08f : 0.04f);
				List<Vector3> list = ListPool<Vector3>.GetList(512);
				Rect rect = this.iconBackground.rect;
				rect.xMin += 20f;
				rect.xMax -= 20f;
				rect.yMin += 20f;
				rect.yMax -= 40f;
				bool flag = false;
				Vector2 vector = Vector2.zero;
				Vector2 vector2 = Vector2.zero;
				foreach (SpriteRenderer spriteRenderer in base.gameObject.EnumerateComponentsInChildren(false, true))
				{
					if (spriteRenderer.sprite)
					{
						SpriteMeshDictionary.GetMesh(spriteRenderer.sprite).GetVertices(list);
						foreach (Vector3 position in list)
						{
							Vector3 vector3 = spriteRenderer.transform.TransformPoint(position);
							Vector3 vector4 = this.iconBackground.transform.InverseTransformPoint(vector3);
							vector4 *= this.iconBackground.pixelsPerUnit;
							if (!rect.Contains(vector4))
							{
								Vector3 vector5 = this.iconHead.transform.InverseTransformPoint(vector3);
								vector5 *= this.iconHead.pixelsPerUnit;
								if (this.headRect.Contains(vector5))
								{
									UnityEngine.Debug.DrawLine(this.iconHead.transform.position, vector3, Color.red, 1f);
									if (flag)
									{
										vector = Vector2.Min(vector, vector5);
										vector2 = Vector2.Max(vector2, vector5);
									}
									else
									{
										flag = true;
										vector = vector5;
										vector2 = vector5;
									}
								}
							}
						}
						list.Clear();
					}
				}
				vector.y = Mathf.Min(vector.y, this.iconHead.transform.InverseTransformPoint(this.iconBackground.transform.TransformPoint(rect.max)).y);
				if (flag && vector2.x > vector.x && vector2.y > vector.y)
				{
					vector -= Vector2.one * 4f;
					vector2 += Vector2.one * 4f;
					this.iconHead.rect = new Rect(vector, vector2 - vector);
				}
				else
				{
					this.iconHead.size = Vector2.one * 2f;
				}
				list.ReturnToListPool<Vector3>();
			}
			if (heroUpgradeDefinition != null)
			{
				this.traitSpriteIcon.sprite = heroUpgradeDefinition.infoSprite;
			}
			else
			{
				this.traitSpriteIcon.sprite = null;
			}
			List<PsdGroup> list2 = ListPool<PsdGroup>.GetList(32);
			this.bannerSpriteGen.GetComponentsInChildren<PsdGroup>(true, list2);
			PsdGroup psdGroup2 = null;
			foreach (PsdGroup psdGroup3 in list2)
			{
				if (psdGroup3.splitName.Contains("Banner2"))
				{
					psdGroup2 = psdGroup3;
					break;
				}
			}
			PsdGroup psdGroup4 = null;
			foreach (PsdGroup psdGroup5 in list2)
			{
				if (psdGroup5.splitName.Contains("IconSlot2"))
				{
					psdGroup4 = psdGroup5;
					break;
				}
			}
			if (heroUpgradeDefinition != null)
			{
				this.traitSpriteBanner.sprite = heroUpgradeDefinition.infoSprite;
				this.traitSpriteBanner.transform.localPosition -= this.traitSpriteBanner.transform.parent.InverseTransformPoint(this.traitSpriteBanner.bounds.center);
				Vector3 localScale = psdGroup2.transform.localScale;
				localScale.x *= this.traitSpriteBanner.bounds.size.x / (psdGroup4.innerRect.width * psdGroup4.transform.lossyScale.x);
				localScale.y *= this.traitSpriteBanner.bounds.size.y / (psdGroup4.innerRect.height * psdGroup4.transform.lossyScale.y);
				psdGroup2.transform.localScale = localScale;
				psdGroup4.gameObject.SetActive(false);
				this.traitInverter.gameObject.SetActive(propertyBank.PickBool("MetaTrait", () => false));
			}
			else
			{
				this.traitSpriteBanner.sprite = null;
				this.traitInverter.gameObject.SetActive(false);
			}
			psdGroup2.transform.localPosition -= this.bannerSpriteGen.transform.InverseTransformPoint(psdGroup4.transform.position);
			Bounds bounds = new Bounds(psdGroup2.transform.position, Vector2.zero);
			foreach (PsdGroup psdGroup6 in list2)
			{
				if (psdGroup6.gameObject.activeInHierarchy && !psdGroup6.splitName.Contains("Decal"))
				{
					bounds.Encapsulate(psdGroup6.transform.TransformPoint(psdGroup6.innerBounds.min));
					bounds.Encapsulate(psdGroup6.transform.TransformPoint(psdGroup6.innerBounds.max));
				}
			}
			Rect rect2 = new Rect(this.bannerSpriteGen.transform.InverseTransformPoint(bounds.min) * this.bannerSpriteGen.pixelsPerUnit, this.bannerSpriteGen.transform.InverseTransformVector(bounds.size) * this.bannerSpriteGen.pixelsPerUnit);
			Rect rect3 = rect2;
			rect3.xMin -= 8f;
			rect3.yMin -= 8f;
			rect3.xMax += 8f;
			rect3.yMax += 8f;
			if (crop)
			{
				this.bannerSpriteGen.rect = rect3;
			}
			else
			{
				rect3 = this.bannerSpriteGen.rect;
			}
			this.bannerSpriteGen.border.x = rect2.xMin - rect3.xMin;
			this.bannerSpriteGen.border.y = rect2.yMin - rect3.yMin;
			this.bannerSpriteGen.border.z = rect3.xMax - rect2.xMax;
			this.bannerSpriteGen.border.w = rect3.yMax - rect2.yMax;
			list2.ReturnToListPool<PsdGroup>();
			this.UpdateRankCount(upgrades);
			this.DrawAll();
		}

		// Token: 0x06003175 RID: 12661 RVA: 0x000CD934 File Offset: 0x000CBD34
		private void RecursiveSetup(GameObject go, PropertyBank propertyBank)
		{
			if (!go)
			{
				return;
			}
			foreach (IOnGenerate onGenerate in go.EnumerateComponents(true))
			{
				onGenerate.OnGenerate(propertyBank, this.seed);
			}
			if (!go)
			{
				return;
			}
			foreach (MonoHero.IHeroSetup heroSetup in go.EnumerateComponents(true))
			{
				if (!heroSetup.HeroSetup(this, propertyBank))
				{
					return;
				}
			}
			if (!go)
			{
				return;
			}
			int num = go.transform.childCount - 1;
			while (num >= 0 && go)
			{
				this.RecursiveSetup(go.transform.GetChild(num).gameObject, propertyBank);
				num--;
			}
		}

		// Token: 0x06003176 RID: 12662 RVA: 0x000CDA54 File Offset: 0x000CBE54
		public IEnumerable UpdateAliveSprite(bool alive, bool memorialIfDead)
		{
			MonoHero.LivingState livingState = (!alive) ? ((!memorialIfDead) ? MonoHero.LivingState.Killed : MonoHero.LivingState.Memorial) : MonoHero.LivingState.Alive;
			if (livingState == this.livingState)
			{
				yield break;
			}
			bool memorial = memorialIfDead && !alive;
			this.livingState = livingState;
			this.SetTaggedObjects("Alive", alive);
			yield return null;
			this.SetTaggedObjects("Dead", !alive);
			yield return null;
			this.onAlive(alive);
			yield return null;
			Color deadColor = new Color(0.9f, 0.9f, 0.9f, 1f);
			this.colorDict["DimPattern"].PushColor(new Color(1f, 1f, 1f, (!memorial) ? 1f : 0.5f));
			foreach (MonoHero.KeyedColor keyedColor in this.colors)
			{
				if (keyedColor.key == "Skin")
				{
					keyedColor.PushColor((!alive) ? deadColor : keyedColor.defaultColor);
				}
				else if (keyedColor.key != "Guilded")
				{
					keyedColor.PushColor(memorial ? deadColor : keyedColor.defaultColor);
				}
			}
			this.iconHead.MaybeRedraw();
			yield return null;
			this.iconBackground.MaybeRedraw();
			yield return null;
			this.bannerSpriteGen.MaybeRedraw();
			yield return null;
			yield break;
		}

		// Token: 0x06003177 RID: 12663 RVA: 0x000CDA88 File Offset: 0x000CBE88
		public void Setup(HeroDefinition heroDef, List<SerializableHeroUpgrade> upgrades, GameObject swapSource, int seed, bool crop = true)
		{
			this.heroDef = heroDef;
			this.seed = seed;
			this.MaybeInitialize();
			this.colorDict["Banner"].PushColor(heroDef.color);
			this.MakeHero(heroDef.propertyBank, swapSource, upgrades, crop);
			IEnumerator enumerator = this.UpdateAliveSprite(heroDef.alive, true).GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
		}

		// Token: 0x06003178 RID: 12664 RVA: 0x000CDB28 File Offset: 0x000CBF28
		public static IEnumerable BakeSprites(IEnumerable<MonoHero> monoHeros)
		{
			foreach (MonoHero hero in monoHeros)
			{
				hero.bannerSpriteGen.Draw();
				yield return null;
			}
			foreach (MonoHero hero2 in monoHeros)
			{
				hero2.agentSpriteHeroGen.Draw();
				yield return null;
			}
			foreach (MonoHero hero3 in monoHeros)
			{
				hero3.agentSpriteMinionGen.Draw();
				hero3.iconBackground.MaybeRedraw();
				yield return null;
			}
			foreach (MonoHero hero4 in monoHeros)
			{
				hero4.iconHead.Draw();
				yield return null;
			}
			foreach (MonoHero hero5 in monoHeros)
			{
				hero5.iconBackground.Draw();
				yield return null;
			}
			yield break;
		}

		// Token: 0x06003179 RID: 12665 RVA: 0x000CDB4C File Offset: 0x000CBF4C
		private void SetTaggedObjects(string tag, bool active)
		{
			List<GameObject> list;
			if (this.taggedObjects.TryGetValue(tag, out list))
			{
				foreach (GameObject gameObject in list)
				{
					if (gameObject && gameObject.gameObject)
					{
						gameObject.gameObject.SetActive(active);
					}
				}
			}
		}

		// Token: 0x0600317A RID: 12666 RVA: 0x000CDBD8 File Offset: 0x000CBFD8
		public void AddTaggedObject(string tag, GameObject gameObject)
		{
			List<GameObject> list;
			if (!this.taggedObjects.TryGetValue(tag, out list))
			{
				list = new List<GameObject>();
				this.taggedObjects.Add(tag, list);
			}
			list.Add(gameObject);
		}

		// Token: 0x0600317B RID: 12667 RVA: 0x000CDC14 File Offset: 0x000CC014
		private void MaybeUpdateHero()
		{
			int squadLevel = this.heroDef.squadLevel;
			if (this.lastLevel != squadLevel)
			{
				this.lastLevel = squadLevel;
				this.agentSpriteMinionGen.Draw();
				this.agentSpriteHeroGen.Draw();
				this.iconBackground.MaybeRedraw();
				this.iconHead.Draw();
			}
		}

		// Token: 0x0600317C RID: 12668 RVA: 0x000CDC6C File Offset: 0x000CC06C
		private void DrawAll()
		{
			this.bannerSpriteGen.Draw();
			this.agentSpriteMinionGen.Draw();
			this.agentSpriteHeroGen.Draw();
			this.DrawMinion();
			this.iconBackground.MaybeRedraw();
			this.iconHead.Draw();
			this.lastLevel = this.heroDef.squadLevel;
		}

		// Token: 0x0600317D RID: 12669 RVA: 0x000CDCC7 File Offset: 0x000CC0C7
		public void Destroy()
		{
			this.agentTexture.Release();
			UnityEngine.Object.Destroy(this.agentTexture);
		}

		// Token: 0x04002121 RID: 8481
		[SerializeField]
		private ComposedSprite bannerSpriteGen;

		// Token: 0x04002122 RID: 8482
		[SerializeField]
		private ComposedSprite agentSpriteMinionGen;

		// Token: 0x04002123 RID: 8483
		[SerializeField]
		private ComposedSprite agentSpriteHeroGen;

		// Token: 0x04002124 RID: 8484
		[SerializeField]
		private ComposedSprite iconHead;

		// Token: 0x04002125 RID: 8485
		[SerializeField]
		private ComposedSprite iconBackground;

		// Token: 0x04002126 RID: 8486
		[SerializeField]
		private SpriteRenderer traitSpriteBanner;

		// Token: 0x04002127 RID: 8487
		[SerializeField]
		private SpriteRenderer traitSpriteIcon;

		// Token: 0x04002128 RID: 8488
		[SerializeField]
		private SpriteRenderer traitInverter;

		// Token: 0x04002129 RID: 8489
		private Dictionary<string, List<GameObject>> taggedObjects = new Dictionary<string, List<GameObject>>();

		// Token: 0x0400212A RID: 8490
		public Dictionary<string, MonoHero.SubscribableColor> colorDict = new Dictionary<string, MonoHero.SubscribableColor>();

		// Token: 0x0400212B RID: 8491
		[Space]
		[SerializeField]
		private MonoHero.KeyedColor[] colors;

		// Token: 0x0400212C RID: 8492
		[SerializeField]
		private MonoHero.LevelColor[] levelColors;

		// Token: 0x0400212D RID: 8493
		[SerializeField]
		private MonoHero.SubscribableColor[] simpleColors;

		// Token: 0x0400212E RID: 8494
		private SpriteRenderReference[] ranks;

		// Token: 0x0400212F RID: 8495
		[NonSerialized]
		public HeroDefinition heroDef;

		// Token: 0x04002130 RID: 8496
		public int seed;

		// Token: 0x04002131 RID: 8497
		private GameObject srcHead;

		// Token: 0x04002132 RID: 8498
		[SerializeField]
		private Agent defaultMinionPrefab;

		// Token: 0x04002135 RID: 8501
		[SerializeField]
		private Transform agentContainer;

		// Token: 0x04002136 RID: 8502
		private int lastLevel = -1;

		// Token: 0x04002137 RID: 8503
		private RenderTexture agentTexture;

		// Token: 0x04002138 RID: 8504
		public MaterialPropertyBlock agentBlock;

		// Token: 0x04002139 RID: 8505
		public Action<bool> onAlive = delegate(bool A_0)
		{
		};

		// Token: 0x0400213A RID: 8506
		public Action<int> onLevel = delegate(int A_0)
		{
		};

		// Token: 0x0400213B RID: 8507
		public Action onChanged = delegate()
		{
		};

		// Token: 0x0400213C RID: 8508
		private Rect headRect;

		// Token: 0x0400213D RID: 8509
		private bool Initialized;

		// Token: 0x0400213E RID: 8510
		private MonoHero.LivingState livingState;

		// Token: 0x02000771 RID: 1905
		[Serializable]
		public class SubscribableColor
		{
			// Token: 0x06003184 RID: 12676 RVA: 0x000CDD15 File Offset: 0x000CC115
			public void Subscribe(Action<Color> f)
			{
				f(this.color);
				this.setFuncs = (Action<Color>)Delegate.Combine(this.setFuncs, f);
			}

			// Token: 0x06003185 RID: 12677 RVA: 0x000CDD3A File Offset: 0x000CC13A
			public void Unsubscribe(Action<Color> f)
			{
				this.setFuncs = (Action<Color>)Delegate.Remove(this.setFuncs, f);
			}

			// Token: 0x06003186 RID: 12678 RVA: 0x000CDD53 File Offset: 0x000CC153
			public void PushColor()
			{
				this.setFuncs(this.color);
			}

			// Token: 0x06003187 RID: 12679 RVA: 0x000CDD66 File Offset: 0x000CC166
			public void PushColor(Color color)
			{
				this.color = color;
				this.PushColor();
			}

			// Token: 0x04002144 RID: 8516
			[SerializeField]
			public string key;

			// Token: 0x04002145 RID: 8517
			[SerializeField]
			public Color color;

			// Token: 0x04002146 RID: 8518
			private Action<Color> setFuncs = delegate(Color A_0)
			{
			};
		}

		// Token: 0x02000772 RID: 1906
		[Serializable]
		public class KeyedColor : MonoHero.SubscribableColor
		{
			// Token: 0x0600318A RID: 12682 RVA: 0x000CDD7F File Offset: 0x000CC17F
			public void PickColor()
			{
				this.color = this.gradient.Evaluate(UnityEngine.Random.value);
				base.PushColor();
			}

			// Token: 0x04002148 RID: 8520
			[SerializeField]
			public Gradient gradient;

			// Token: 0x04002149 RID: 8521
			private Action<Color> setFuncs;

			// Token: 0x0400214A RID: 8522
			[NonSerialized]
			public Color defaultColor;
		}

		// Token: 0x02000773 RID: 1907
		[Serializable]
		private class LevelColor : MonoHero.SubscribableColor
		{
			// Token: 0x0600318B RID: 12683 RVA: 0x000CDDA0 File Offset: 0x000CC1A0
			public LevelColor()
			{
				this.color0 = Color.white;
				this.color1 = Color.white;
				this.color2 = Color.white;
				this.color3 = Color.white;
				this.multiplier = 1f;
			}

			// Token: 0x17000716 RID: 1814
			// (get) Token: 0x0600318C RID: 12684 RVA: 0x000CDE21 File Offset: 0x000CC221
			[SerializeField]
			private Color multiplyColor
			{
				get
				{
					return new Color(this.multiplier, this.multiplier, this.multiplier, 1f);
				}
			}

			// Token: 0x0600318D RID: 12685 RVA: 0x000CDE40 File Offset: 0x000CC240
			public Color Get(int level)
			{
				switch (level)
				{
				case 0:
					return this.color0 * this.multiplyColor;
				case 1:
					return this.color1 * this.multiplyColor;
				case 2:
					return this.color2 * this.multiplyColor;
				case 3:
					return this.color3 * this.multiplyColor;
				default:
					return Color.white * this.multiplyColor;
				}
			}

			// Token: 0x0600318E RID: 12686 RVA: 0x000CDEC0 File Offset: 0x000CC2C0
			public void SetLevel(int level)
			{
				base.PushColor(this.Get(level));
			}

			// Token: 0x0400214B RID: 8523
			[SerializeField]
			public Color color0 = Color.white;

			// Token: 0x0400214C RID: 8524
			[SerializeField]
			public Color color1 = Color.white;

			// Token: 0x0400214D RID: 8525
			[SerializeField]
			public Color color2 = Color.white;

			// Token: 0x0400214E RID: 8526
			[SerializeField]
			public Color color3 = Color.white;

			// Token: 0x0400214F RID: 8527
			[SerializeField]
			private float multiplier = 1f;
		}

		// Token: 0x02000774 RID: 1908
		public enum LivingState
		{
			// Token: 0x04002151 RID: 8529
			NotSet,
			// Token: 0x04002152 RID: 8530
			Alive,
			// Token: 0x04002153 RID: 8531
			Killed,
			// Token: 0x04002154 RID: 8532
			Memorial
		}

		// Token: 0x02000775 RID: 1909
		public interface IHeroSetup
		{
			// Token: 0x0600318F RID: 12687
			bool HeroSetup(MonoHero monoHero, PropertyBank propertyBank);
		}

		// Token: 0x02000776 RID: 1910
		public interface IRankModifier
		{
			// Token: 0x06003190 RID: 12688
			void ModifyRank(ref int rankCount, SerializableHeroUpgrade upgrade);
		}
	}
}
