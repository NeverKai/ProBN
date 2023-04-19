using System;
using RTM.UISystem;
using UnityEngine;
using UnityEngine.UI;
using Voxels.TowerDefense.ScriptAnimations;

namespace Voxels.TowerDefense.UI.MetaInventory
{
	// Token: 0x020008F2 RID: 2290
	[AddComponentMenu("Meta Inventory - Upgrade Token")]
	internal class UpgradeToken : MonoBehaviour
	{
		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x06003CB3 RID: 15539 RVA: 0x0010EF2A File Offset: 0x0010D32A
		// (set) Token: 0x06003CB4 RID: 15540 RVA: 0x0010EF32 File Offset: 0x0010D332
		public HeroUpgradeDefinition upgradeDef { get; private set; }

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x06003CB5 RID: 15541 RVA: 0x0010EF3B File Offset: 0x0010D33B
		// (set) Token: 0x06003CB6 RID: 15542 RVA: 0x0010EF43 File Offset: 0x0010D343
		public UIClickable clickable { get; private set; }

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x06003CB7 RID: 15543 RVA: 0x0010EF4C File Offset: 0x0010D34C
		// (set) Token: 0x06003CB8 RID: 15544 RVA: 0x0010EF54 File Offset: 0x0010D354
		public bool isStarting { get; private set; }

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x06003CB9 RID: 15545 RVA: 0x0010EF5D File Offset: 0x0010D35D
		// (set) Token: 0x06003CBA RID: 15546 RVA: 0x0010EF65 File Offset: 0x0010D365
		public InventoryGroup group { get; private set; }

		// Token: 0x06003CBB RID: 15547 RVA: 0x0010EF70 File Offset: 0x0010D370
		public void Setup(MetaInventoryMenu menu, InventoryGroup group, HeroUpgradeDefinition upgradeDef, int index, int maxCount)
		{
			this.menu = menu;
			this.group = group;
			this.upgradeDef = upgradeDef;
			this.clickable = base.GetComponent<UIClickable>();
			this.clickable.onStateChanged += this.OnStateChanged;
			this.maskedSprite = base.GetComponentInChildren<MaskedSprite>(true);
			foreach (Image image in this.icons)
			{
				image.sprite = upgradeDef.infoSprite;
			}
			if (this.rainbows.Length > 0)
			{
				Color color = Color.HSVToRGB(Mathf.InverseLerp(0f, (float)maxCount, (float)index), 0.55f, 0.75f);
				foreach (Image image2 in this.rainbows)
				{
					image2.color = color;
				}
			}
			AnimatedState focusState = new AnimatedState("Focus", this.root.rootState, false, false);
			AnimatedState hoverState = new AnimatedState("Hover", this.root.rootState, false, false);
			DistanceFieldSettings[] array3 = this.focuses;
			for (int k = 0; k < array3.Length; k++)
			{
				// UpgradeToken.<Setup>c__AnonStorey0 <Setup>c__AnonStorey2 = new UpgradeToken.<Setup>c__AnonStorey0();
				var focus = array3[k];
				float fraction = focus.fraction;
				Graphic graphic = focus.GetComponent<Graphic>();
				focusState.Subscribe(delegate(bool x)
				{
					focus.gameObject.SetActive(x);
				}, delegate(float x)
				{
					focus.fraction = Mathf.Lerp(fraction * 0.1f, fraction, x);
					graphic.color = graphic.color.SetA(x);
				});
			}
			hoverState.anim.Subscribe(delegate(float x)
			{
				this.transform.localScale = this.transform.localScale.SetZ(Mathf.Lerp(1f, 1.2f, x));
			});
			if (this.maskedSprite)
			{
				float width = this.maskedSprite.borders[3].width;
				focusState.anim.Subscribe(delegate(float x)
				{
					MaskedSprite.BorderSettings borderSettings = this.maskedSprite.borders[3];
					borderSettings.width = Mathf.Lerp(0f, width, x);
					this.maskedSprite.borders[3] = borderSettings;
					this.maskedSprite.SetDirty();
				});
			}
			TargetAnimator<Vector2> moveAnim = new TargetAnimator<Vector2>(() => this.moveOffset.transform.localPosition, delegate(Vector2 x)
			{
				this.moveOffset.transform.localPosition = x;
			}, this.root.rootState, LerpTowards2.standard);
			this.clickable.onSelectedChanged += delegate(bool x)
			{
				focusState.SetActive(x);
			};
			this.clickable.onStateChanged += delegate(UIInteractable.State x)
			{
				hoverState.SetActive(x == UIInteractable.State.Hover);
				if (x != UIInteractable.State.Hover)
				{
					if (x != UIInteractable.State.PointerButtonDown)
					{
						moveAnim.SetTarget(Vector2.zero, null, null, null, 0f, null);
					}
					else
					{
						moveAnim.SetTarget(Vector2.down * 2f, null, null, null, 0f, null);
					}
				}
				else
				{
					moveAnim.SetTarget(Vector2.up * 2f, null, null, null, 0f, null);
				}
			};
		}

		// Token: 0x06003CBC RID: 15548 RVA: 0x0010F1FC File Offset: 0x0010D5FC
		public void Refresh()
		{
			int num;
			bool flag2;
			bool flag3;
			bool flag = Profile.userSave.inventory.Get(this.upgradeDef, out num, out flag2, out flag3);
			this.isStarting = flag3;
			foreach (GameObject gameObject in this.knowns)
			{
				gameObject.SetActive(flag);
			}
			foreach (GameObject gameObject2 in this.unknowns)
			{
				gameObject2.SetActive(!flag);
			}
			foreach (GameObject gameObject3 in this.starters)
			{
				gameObject3.SetActive(flag3);
			}
			foreach (GameObject gameObject4 in this.nonStarters)
			{
				gameObject4.SetActive(!flag3);
			}
			if (this.maskedSprite)
			{
				this.maskedSprite.Set(this.upgradeDef, 0);
				if (flag)
				{
					this.maskedSprite.gameObject.SetActive(true);
					MaskedSprite.BorderSettings borderSettings = this.maskedSprite.borders[2];
					borderSettings.width = ((!flag3) ? 0f : 0.5f);
					this.maskedSprite.borders[2] = borderSettings;
				}
				else
				{
					this.maskedSprite.gameObject.SetActive(false);
				}
			}
		}

		// Token: 0x06003CBD RID: 15549 RVA: 0x0010F38D File Offset: 0x0010D78D
		private void Update()
		{
			this.root.Update();
		}

		// Token: 0x06003CBE RID: 15550 RVA: 0x0010F39A File Offset: 0x0010D79A
		public void HandleClick()
		{
			this.menu.HandleClick(this);
		}

		// Token: 0x06003CBF RID: 15551 RVA: 0x0010F3A9 File Offset: 0x0010D7A9
		private void OnStateChanged(UIInteractable.State state)
		{
			if (state == UIInteractable.State.Focus)
			{
				this.menu.HandleFocusChange(this);
			}
		}

		// Token: 0x04002A60 RID: 10848
		[SerializeField]
		private Image[] icons;

		// Token: 0x04002A61 RID: 10849
		[SerializeField]
		public Image[] rainbows;

		// Token: 0x04002A62 RID: 10850
		[SerializeField]
		private GameObject[] knowns;

		// Token: 0x04002A63 RID: 10851
		[SerializeField]
		private GameObject[] unknowns;

		// Token: 0x04002A64 RID: 10852
		[SerializeField]
		private GameObject[] starters;

		// Token: 0x04002A65 RID: 10853
		[SerializeField]
		private GameObject[] nonStarters;

		// Token: 0x04002A66 RID: 10854
		[SerializeField]
		private Transform moveOffset;

		// Token: 0x04002A67 RID: 10855
		[SerializeField]
		private DistanceFieldSettings[] focuses;

		// Token: 0x04002A68 RID: 10856
		private MetaInventoryMenu menu;

		// Token: 0x04002A6B RID: 10859
		private MaskedSprite maskedSprite;

		// Token: 0x04002A6E RID: 10862
		[SerializeField]
		private AgentStateRoot root = new AgentStateRoot(4);
	}
}
