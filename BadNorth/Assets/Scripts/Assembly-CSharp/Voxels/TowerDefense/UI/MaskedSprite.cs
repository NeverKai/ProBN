using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Voxels.TowerDefense.SpriteMagic;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x020008E2 RID: 2274
	[RequireComponent(typeof(Image))]
	public class MaskedSprite : MonoBehaviour, IMeshModifier
	{
		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x06003C41 RID: 15425 RVA: 0x0010C47D File Offset: 0x0010A87D
		public Image image
		{
			get
			{
				if (!this._image)
				{
					this._image = base.GetComponent<Image>();
				}
				return this._image;
			}
		}

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x06003C42 RID: 15426 RVA: 0x0010C4A1 File Offset: 0x0010A8A1
		// (set) Token: 0x06003C43 RID: 15427 RVA: 0x0010C4AE File Offset: 0x0010A8AE
		public Sprite sprite
		{
			get
			{
				return this.image.sprite;
			}
			set
			{
				this.image.sprite = value;
			}
		}

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x06003C44 RID: 15428 RVA: 0x0010C4BC File Offset: 0x0010A8BC
		private RectTransform rt
		{
			get
			{
				if (!this._rt)
				{
					this._rt = base.GetComponent<RectTransform>();
				}
				return this._rt;
			}
		}

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x06003C45 RID: 15429 RVA: 0x0010C4E0 File Offset: 0x0010A8E0
		// (set) Token: 0x06003C46 RID: 15430 RVA: 0x0010C4E8 File Offset: 0x0010A8E8
		public Sprite mask
		{
			get
			{
				return this._mask;
			}
			set
			{
				this._mask = value;
				this.image.SetVerticesDirty();
			}
		}

		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x06003C47 RID: 15431 RVA: 0x0010C4FC File Offset: 0x0010A8FC
		// (set) Token: 0x06003C48 RID: 15432 RVA: 0x0010C504 File Offset: 0x0010A904
		public Sprite foreground
		{
			get
			{
				return this._foreground;
			}
			set
			{
				this._foreground = value;
				this.image.SetVerticesDirty();
			}
		}

		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x06003C49 RID: 15433 RVA: 0x0010C518 File Offset: 0x0010A918
		// (set) Token: 0x06003C4A RID: 15434 RVA: 0x0010C520 File Offset: 0x0010A920
		public Sprite background
		{
			get
			{
				return this._background;
			}
			set
			{
				this._background = value;
				this.image.SetVerticesDirty();
			}
		}

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x06003C4B RID: 15435 RVA: 0x0010C534 File Offset: 0x0010A934
		// (set) Token: 0x06003C4C RID: 15436 RVA: 0x0010C53C File Offset: 0x0010A93C
		public float radial
		{
			get
			{
				return this._radial;
			}
			set
			{
				if (value != this._radial)
				{
					this._radial = value;
					this.image.SetVerticesDirty();
				}
			}
		}

		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x06003C4D RID: 15437 RVA: 0x0010C55C File Offset: 0x0010A95C
		// (set) Token: 0x06003C4E RID: 15438 RVA: 0x0010C569 File Offset: 0x0010A969
		public float brightness
		{
			get
			{
				return this.shaderSettings.brightness;
			}
			set
			{
				this.shaderSettings.brightness = value;
				this.SetDirty();
			}
		}

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x06003C4F RID: 15439 RVA: 0x0010C57D File Offset: 0x0010A97D
		// (set) Token: 0x06003C50 RID: 15440 RVA: 0x0010C58A File Offset: 0x0010A98A
		public float saturation
		{
			get
			{
				return this.shaderSettings.saturation;
			}
			set
			{
				this.shaderSettings.saturation = value;
				this.SetDirty();
			}
		}

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x06003C51 RID: 15441 RVA: 0x0010C59E File Offset: 0x0010A99E
		private MaskedSprite.IMaskedSpriteModifier[] modifiers
		{
			get
			{
				if (!Application.isPlaying || this._modifiers == null)
				{
					this._modifiers = base.GetComponents<MaskedSprite.IMaskedSpriteModifier>();
				}
				return this._modifiers;
			}
		}

		// Token: 0x06003C52 RID: 15442 RVA: 0x0010C5C7 File Offset: 0x0010A9C7
		[ContextMenu("NameAfterSprite")]
		private void NameAfterSprite()
		{
			if (this.sprite)
			{
				base.name = this.sprite.name;
			}
		}

		// Token: 0x06003C53 RID: 15443 RVA: 0x0010C5EA File Offset: 0x0010A9EA
		public void Set(SerializableHeroUpgrade upgrade)
		{
			this.Set(upgrade.definition, upgrade.level);
		}

		// Token: 0x06003C54 RID: 15444 RVA: 0x0010C600 File Offset: 0x0010AA00
		public void Set(HeroUpgradeDefinition upgrade, int level)
		{
			this.sprite = upgrade.GetSprite();
			this.mask = upgrade.upgradeType.mask;
			this.foreground = null;
			this.spriteScale = this.maskScale * 0.75f;
			for (int i = 0; i < this.borders.Length; i++)
			{
				if (this.borders[i].sprite)
				{
					this.borders[i].color.a = (float)((level <= 1) ? 0 : 1);
					break;
				}
			}
			for (int j = this.borders.Length - 1; j >= 0; j--)
			{
				if (this.borders[j].sprite)
				{
					this.borders[j].color.a = (float)((level <= 0) ? 0 : 1);
					break;
				}
			}
			this.SetDirty();
		}

		// Token: 0x06003C55 RID: 15445 RVA: 0x0010C708 File Offset: 0x0010AB08
		public void Set(IHeroGraphics heroGraphics)
		{
			using (new ScopedProfiler("MaskedSprite.SetHerographics", null))
			{
				this.spriteScale = this.maskScale * 0.7083333f;
				this.mask = ScriptableObjectSingleton<MaskedSpriteConstants>.instance.heroMask;
				this.sprite = heroGraphics.iconBackground;
				this.foreground = heroGraphics.iconHead;
			}
		}

		// Token: 0x06003C56 RID: 15446 RVA: 0x0010C780 File Offset: 0x0010AB80
		public void Clear()
		{
			this.sprite = null;
			this.foreground = null;
		}

		// Token: 0x06003C57 RID: 15447 RVA: 0x0010C790 File Offset: 0x0010AB90
		public void SetDirty()
		{
			this.image.SetVerticesDirty();
		}

		// Token: 0x06003C58 RID: 15448 RVA: 0x0010C79D File Offset: 0x0010AB9D
		private void OnValidate()
		{
			this.SetDirty();
		}

		// Token: 0x06003C59 RID: 15449 RVA: 0x0010C7A8 File Offset: 0x0010ABA8
		void IMeshModifier.ModifyMesh(VertexHelper verts)
		{
			Sprite sprite = this.image.sprite;
			if (!this.mask)
			{
				return;
			}
			MaskedSprite.ShaderSettings shaderSettings = this.shaderSettings;
			MaskedSprite.borderList.Clear();
			MaskedSprite.borderList.AddRange(this.borders);
			for (int i = 0; i < this.modifiers.Length; i++)
			{
				this.modifiers[i].ModifyMaskedSprite(ref shaderSettings, MaskedSprite.borderList);
			}
			verts.Clear();
			Rect rect = this.rt.rect;
			Rect rect2 = (!sprite) ? default(Rect) : MaskedSprite.GetUvRect(sprite);
			Rect uvRect = MaskedSprite.GetUvRect(this.mask);
			float d = this.maskScale * shaderSettings.maskScale;
			float d2 = this.spriteScale * shaderSettings.spriteScale;
			for (int j = 0; j < 4; j++)
			{
				Vector2 a = MaskedSprite.ps[j];
				MaskedSprite.quad[j].position = rect.min + Vector2.Scale((a * d + Vector2.one) / 2f, rect.size);
				MaskedSprite.quad[j].uv1 = uvRect.min + Vector2.Scale((a + Vector2.one) / 2f, uvRect.size);
				MaskedSprite.quad[j].normal = new Vector3(a.x, a.y, 0f);
				MaskedSprite.quad[j].tangent.z = 1f;
			}
			float num = 0f;
			for (int k = 0; k < MaskedSprite.borderList.Count; k++)
			{
				if (!MaskedSprite.borderList[k].sprite)
				{
					num += MaskedSprite.borderList[k].width;
				}
			}
			if (this.shadowSettings.color.a > 0f)
			{
				Vector2 uv = new Vector2(-(num + 1f), -1f);
				Color c = this.shadowSettings.color / 2f;
				c.a *= this.image.color.a;
				Color32 color = c;
				for (int l = 0; l < 4; l++)
				{
					MaskedSprite.quad[l].color = color;
					UIVertex[] array = MaskedSprite.quad;
					int num2 = l;
					array[num2].position = array[num2].position + this.shadowSettings.offset;
					MaskedSprite.quad[l].uv0 = uv;
				}
				verts.AddUIVertexQuad(MaskedSprite.quad);
				for (int m = 0; m < 4; m++)
				{
					UIVertex[] array2 = MaskedSprite.quad;
					int num3 = m;
					array2[num3].position = array2[num3].position - this.shadowSettings.offset;
				}
			}
			Color color2 = new Color(shaderSettings.brightness / 2f, shaderSettings.saturation / 2f, shaderSettings.outlineWidth / 10f, this.image.color.a);
			for (int n = MaskedSprite.borderList.Count - 1; n >= 0; n--)
			{
				MaskedSprite.BorderSettings borderSettings = MaskedSprite.borderList[n];
				if (borderSettings.width > 0f && borderSettings.color.a > 0f)
				{
					Color c2 = borderSettings.color / 2f;
					Vector2 uv2 = new Vector2(-(num + 1f), borderSettings.outlineWidth);
					if (borderSettings.sprite)
					{
						Rect uv3 = MaskedSprite.pool.GetSpriteBounds(borderSettings.sprite).uv;
						Vector2 a2 = uv3.center;
						Vector2 size = uv3.size;
						size.y /= 2f;
						a2.x *= 2f;
						a2 += new Vector2(2f, 0f);
						Color c3 = color2.SetA(color2.a * borderSettings.color.a);
						Color32 color3 = c3;
						for (int num4 = 0; num4 < 4; num4++)
						{
							Vector2 a3 = MaskedSprite.ps[num4];
							MaskedSprite.quad[num4].uv0 = a2 + Vector2.Scale(a3, size);
							MaskedSprite.quad[num4].color = color3;
							MaskedSprite.quad[num4].tangent.w = 1f;
						}
					}
					else
					{
						Color32 color4 = c2;
						float w = (!borderSettings.maskedByRadial) ? 1f : this.radial;
						for (int num5 = 0; num5 < 4; num5++)
						{
							MaskedSprite.quad[num5].color = color4;
							MaskedSprite.quad[num5].uv0 = uv2;
							MaskedSprite.quad[num5].tangent.w = w;
						}
						num -= borderSettings.width;
					}
					verts.AddUIVertexQuad(MaskedSprite.quad);
				}
			}
			if (sprite)
			{
				Color32 color5 = color2;
				for (int num6 = 0; num6 < 4; num6++)
				{
					Vector2 a4 = MaskedSprite.ps[num6];
					Vector2 a5 = (a4 * d / d2 + Vector2.one) / 2f;
					MaskedSprite.quad[num6].uv0 = rect2.min + Vector2.Scale(a5, rect2.size);
					MaskedSprite.quad[num6].color = color5;
					MaskedSprite.quad[num6].tangent.w = this.radial;
				}
				verts.AddUIVertexQuad(MaskedSprite.quad);
				if (this.foreground)
				{
					SpriteBounds spriteBounds = MaskedSprite.pool.GetSpriteBounds(this.foreground);
					Vector2 size2 = rect2.size;
					size2.x *= (float)sprite.texture.width;
					size2.y *= (float)sprite.texture.height;
					Vector2 vector = ExtraMath.Divide(rect.size, size2);
					vector *= this.foreground.pixelsPerUnit;
					vector *= d2;
					color5 = color2;
					for (int num7 = 0; num7 < 4; num7++)
					{
						Vector2 a6 = MaskedSprite.ps[num7];
						Vector2 a7 = (a6 + Vector2.one) / 2f;
						UIVertex uivertex = MaskedSprite.quad[num7];
						uivertex.position = spriteBounds.v.min + Vector2.Scale(a7, spriteBounds.v.size);
						uivertex.position = Vector3.Scale(uivertex.position, vector);
						uivertex.normal = uivertex.position;
						uivertex.position += rect.center;
						uivertex.uv0 = spriteBounds.uv.min + Vector2.Scale(a7, spriteBounds.uv.size);
						uivertex.uv1 = Vector2.one * -2f;
						uivertex.color = color5;
						MaskedSprite.quad[num7] = uivertex;
					}
					verts.AddUIVertexQuad(MaskedSprite.quad);
				}
			}
			else
			{
				Color32 color6 = this.image.color / 2f;
				for (int num8 = 0; num8 < 4; num8++)
				{
					MaskedSprite.quad[num8].uv0 = new Vector2(-1f, 1f);
					MaskedSprite.quad[num8].color = color6;
					MaskedSprite.quad[num8].tangent.w = 1f;
				}
				verts.AddUIVertexQuad(MaskedSprite.quad);
			}
		}

		// Token: 0x06003C5A RID: 15450 RVA: 0x0010D094 File Offset: 0x0010B494
		private static Rect GetPixelRect(Sprite sprite)
		{
			SpriteBounds spriteBounds = MaskedSprite.pool.GetSpriteBounds(sprite);
			Vector2 a = new Vector2((float)sprite.texture.width, (float)sprite.texture.height);
			Vector2 b;
			b.x = ExtraMath.RemapValueUnclamped(0f, spriteBounds.v.min.x, spriteBounds.v.max.x, 0f, 1f);
			b.y = ExtraMath.RemapValueUnclamped(0f, spriteBounds.v.min.y, spriteBounds.v.max.y, 0f, 1f);
			Vector2 b2 = spriteBounds.uv.min + Vector2.Scale(spriteBounds.uv.size, b);
			b2 = Vector2.Scale(a, b2);
			Vector2 position = sprite.rect.min + b2 - (sprite.pivot + sprite.rect.min);
			return new Rect(position, sprite.rect.size);
		}

		// Token: 0x06003C5B RID: 15451 RVA: 0x0010D1D4 File Offset: 0x0010B5D4
		private static Rect GetUvRect(Sprite sprite)
		{
			Rect pixelRect = MaskedSprite.GetPixelRect(sprite);
			int width = sprite.texture.width;
			int height = sprite.texture.height;
			return new Rect(pixelRect.min.x / (float)width, pixelRect.min.y / (float)height, pixelRect.size.x / (float)width, pixelRect.size.y / (float)height);
		}

		// Token: 0x06003C5C RID: 15452 RVA: 0x0010D250 File Offset: 0x0010B650
		void IMeshModifier.ModifyMesh(Mesh mesh)
		{
		}

		// Token: 0x040029FB RID: 10747
		private Image _image;

		// Token: 0x040029FC RID: 10748
		private Sprite _sprite;

		// Token: 0x040029FD RID: 10749
		private RectTransform _rt;

		// Token: 0x040029FE RID: 10750
		private static SpriteBoundsPool pool = new SpriteBoundsPool();

		// Token: 0x040029FF RID: 10751
		[SerializeField]
		private Sprite _mask;

		// Token: 0x04002A00 RID: 10752
		[SerializeField]
		private Sprite _foreground;

		// Token: 0x04002A01 RID: 10753
		[SerializeField]
		private Sprite _background;

		// Token: 0x04002A02 RID: 10754
		public float spriteScale = 1f;

		// Token: 0x04002A03 RID: 10755
		public float maskScale = 1f;

		// Token: 0x04002A04 RID: 10756
		[SerializeField]
		[Range(0f, 1f)]
		private float _radial = 1f;

		// Token: 0x04002A05 RID: 10757
		[SerializeField]
		public MaskedSprite.ShaderSettings shaderSettings = new MaskedSprite.ShaderSettings(1f, 1f, 1f);

		// Token: 0x04002A06 RID: 10758
		[SerializeField]
		public MaskedSprite.BorderSettings[] borders;

		// Token: 0x04002A07 RID: 10759
		[SerializeField]
		private MaskedSprite.ShadowSettings shadowSettings = new MaskedSprite.ShadowSettings(Color.clear, new Vector2(2f, -6f));

		// Token: 0x04002A08 RID: 10760
		private MaskedSprite.IMaskedSpriteModifier[] _modifiers;

		// Token: 0x04002A09 RID: 10761
		private static List<MaskedSprite.BorderSettings> borderList = new List<MaskedSprite.BorderSettings>();

		// Token: 0x04002A0A RID: 10762
		private static Vector2[] ps = new Vector2[]
		{
			new Vector2(-1f, 1f),
			new Vector2(1f, 1f),
			new Vector2(1f, -1f),
			new Vector2(-1f, -1f)
		};

		// Token: 0x04002A0B RID: 10763
		private static UIVertex[] quad = new UIVertex[4];

		// Token: 0x020008E3 RID: 2275
		[Serializable]
		public struct ShaderSettings
		{
			// Token: 0x06003C5E RID: 15454 RVA: 0x0010D2F7 File Offset: 0x0010B6F7
			public ShaderSettings(float brightness, float saturation, float outlineWidth)
			{
				this.brightness = brightness;
				this.saturation = saturation;
				this.outlineWidth = outlineWidth;
				this.maskScale = 1f;
				this.spriteScale = 1f;
			}

			// Token: 0x17000875 RID: 2165
			// (get) Token: 0x06003C5F RID: 15455 RVA: 0x0010D324 File Offset: 0x0010B724
			public static MaskedSprite.ShaderSettings One
			{
				get
				{
					return new MaskedSprite.ShaderSettings(1f, 1f, 1f);
				}
			}

			// Token: 0x17000876 RID: 2166
			// (get) Token: 0x06003C60 RID: 15456 RVA: 0x0010D33A File Offset: 0x0010B73A
			public static MaskedSprite.ShaderSettings Zero
			{
				get
				{
					return new MaskedSprite.ShaderSettings(0f, 0f, 0f);
				}
			}

			// Token: 0x06003C61 RID: 15457 RVA: 0x0010D350 File Offset: 0x0010B750
			public static MaskedSprite.ShaderSettings Lerp(MaskedSprite.ShaderSettings a, MaskedSprite.ShaderSettings b, float t)
			{
				t = Mathf.Clamp01(t);
				return a * (1f - t) + b * t;
			}

			// Token: 0x06003C62 RID: 15458 RVA: 0x0010D373 File Offset: 0x0010B773
			public override bool Equals(object obj)
			{
				return obj is MaskedSprite.ShaderSettings && this == (MaskedSprite.ShaderSettings)obj;
			}

			// Token: 0x06003C63 RID: 15459 RVA: 0x0010D394 File Offset: 0x0010B794
			public override int GetHashCode()
			{
				return this.saturation.GetHashCode() ^ this.brightness.GetHashCode() ^ this.outlineWidth.GetHashCode() ^ this.maskScale.GetHashCode() ^ this.spriteScale.GetHashCode();
			}

			// Token: 0x06003C64 RID: 15460 RVA: 0x0010D3FC File Offset: 0x0010B7FC
			public static bool operator ==(MaskedSprite.ShaderSettings x, MaskedSprite.ShaderSettings y)
			{
				return x.saturation == y.saturation && x.brightness == y.brightness && x.outlineWidth == y.outlineWidth && x.maskScale == y.maskScale && x.spriteScale == y.spriteScale;
			}

			// Token: 0x06003C65 RID: 15461 RVA: 0x0010D468 File Offset: 0x0010B868
			public static bool operator !=(MaskedSprite.ShaderSettings x, MaskedSprite.ShaderSettings y)
			{
				return !(x == y);
			}

			// Token: 0x06003C66 RID: 15462 RVA: 0x0010D474 File Offset: 0x0010B874
			public static MaskedSprite.ShaderSettings operator *(MaskedSprite.ShaderSettings a, MaskedSprite.ShaderSettings b)
			{
				a.brightness *= b.brightness;
				a.saturation *= b.saturation;
				a.outlineWidth *= b.outlineWidth;
				a.maskScale *= b.maskScale;
				a.spriteScale *= b.spriteScale;
				return a;
			}

			// Token: 0x06003C67 RID: 15463 RVA: 0x0010D4EC File Offset: 0x0010B8EC
			public static MaskedSprite.ShaderSettings operator *(MaskedSprite.ShaderSettings a, float b)
			{
				a.brightness *= b;
				a.saturation *= b;
				a.outlineWidth *= b;
				a.maskScale *= b;
				a.spriteScale *= b;
				return a;
			}

			// Token: 0x06003C68 RID: 15464 RVA: 0x0010D548 File Offset: 0x0010B948
			public static MaskedSprite.ShaderSettings operator +(MaskedSprite.ShaderSettings a, MaskedSprite.ShaderSettings b)
			{
				a.brightness += b.brightness;
				a.saturation += b.saturation;
				a.outlineWidth += b.outlineWidth;
				a.maskScale += b.maskScale;
				a.spriteScale += b.spriteScale;
				return a;
			}

			// Token: 0x04002A0C RID: 10764
			[Range(0f, 2f)]
			public float saturation;

			// Token: 0x04002A0D RID: 10765
			[Range(0f, 2f)]
			public float brightness;

			// Token: 0x04002A0E RID: 10766
			public float outlineWidth;

			// Token: 0x04002A0F RID: 10767
			public float maskScale;

			// Token: 0x04002A10 RID: 10768
			public float spriteScale;
		}

		// Token: 0x020008E4 RID: 2276
		[Serializable]
		public struct BorderSettings
		{
			// Token: 0x06003C69 RID: 15465 RVA: 0x0010D5BF File Offset: 0x0010B9BF
			public BorderSettings(Color color, float width, float outlineWidth)
			{
				this.color = color;
				this.width = width;
				this.outlineWidth = outlineWidth;
				this.sprite = null;
				this.maskedByRadial = false;
			}

			// Token: 0x17000877 RID: 2167
			// (get) Token: 0x06003C6A RID: 15466 RVA: 0x0010D5E4 File Offset: 0x0010B9E4
			public static MaskedSprite.BorderSettings One
			{
				get
				{
					return new MaskedSprite.BorderSettings(Color.white, 1f, 1f);
				}
			}

			// Token: 0x17000878 RID: 2168
			// (get) Token: 0x06003C6B RID: 15467 RVA: 0x0010D5FA File Offset: 0x0010B9FA
			public static MaskedSprite.BorderSettings Zero
			{
				get
				{
					return new MaskedSprite.BorderSettings(Color.clear, 0f, 0f);
				}
			}

			// Token: 0x06003C6C RID: 15468 RVA: 0x0010D610 File Offset: 0x0010BA10
			public static MaskedSprite.BorderSettings Lerp(MaskedSprite.BorderSettings a, MaskedSprite.BorderSettings b, float t)
			{
				t = Mathf.Clamp01(t);
				return a * (1f - t) + b * t;
			}

			// Token: 0x06003C6D RID: 15469 RVA: 0x0010D633 File Offset: 0x0010BA33
			public override bool Equals(object obj)
			{
				return obj is MaskedSprite.BorderSettings && this == (MaskedSprite.BorderSettings)obj;
			}

			// Token: 0x06003C6E RID: 15470 RVA: 0x0010D654 File Offset: 0x0010BA54
			public override int GetHashCode()
			{
				return this.color.GetHashCode() ^ this.width.GetHashCode() ^ this.outlineWidth.GetHashCode() ^ this.maskedByRadial.GetHashCode();
			}

			// Token: 0x06003C6F RID: 15471 RVA: 0x0010D6A8 File Offset: 0x0010BAA8
			public static bool operator ==(MaskedSprite.BorderSettings x, MaskedSprite.BorderSettings y)
			{
				return x.color == y.color && x.width == y.width && x.outlineWidth == y.outlineWidth && x.maskedByRadial == y.maskedByRadial;
			}

			// Token: 0x06003C70 RID: 15472 RVA: 0x0010D706 File Offset: 0x0010BB06
			public static bool operator !=(MaskedSprite.BorderSettings x, MaskedSprite.BorderSettings y)
			{
				return !(x == y);
			}

			// Token: 0x06003C71 RID: 15473 RVA: 0x0010D712 File Offset: 0x0010BB12
			public static MaskedSprite.BorderSettings operator *(MaskedSprite.BorderSettings a, float b)
			{
				a.color *= b;
				a.width *= b;
				a.outlineWidth *= b;
				return a;
			}

			// Token: 0x06003C72 RID: 15474 RVA: 0x0010D748 File Offset: 0x0010BB48
			public static MaskedSprite.BorderSettings operator *(MaskedSprite.BorderSettings a, MaskedSprite.BorderSettings b)
			{
				a.color *= b.color;
				a.width *= b.width;
				a.outlineWidth *= b.outlineWidth;
				return a;
			}

			// Token: 0x06003C73 RID: 15475 RVA: 0x0010D79C File Offset: 0x0010BB9C
			public static MaskedSprite.BorderSettings operator +(MaskedSprite.BorderSettings a, MaskedSprite.BorderSettings b)
			{
				a.color += b.color;
				a.width += b.width;
				a.outlineWidth += b.outlineWidth;
				return a;
			}

			// Token: 0x06003C74 RID: 15476 RVA: 0x0010D7F0 File Offset: 0x0010BBF0
			public static MaskedSprite.BorderSettings operator -(MaskedSprite.BorderSettings a, MaskedSprite.BorderSettings b)
			{
				a.color -= b.color;
				a.width -= b.width;
				a.outlineWidth -= b.outlineWidth;
				return a;
			}

			// Token: 0x04002A11 RID: 10769
			public Color color;

			// Token: 0x04002A12 RID: 10770
			[Range(0f, 1f)]
			public float width;

			// Token: 0x04002A13 RID: 10771
			public float outlineWidth;

			// Token: 0x04002A14 RID: 10772
			public bool maskedByRadial;

			// Token: 0x04002A15 RID: 10773
			public Sprite sprite;
		}

		// Token: 0x020008E5 RID: 2277
		[Serializable]
		public struct ShadowSettings
		{
			// Token: 0x06003C75 RID: 15477 RVA: 0x0010D841 File Offset: 0x0010BC41
			public ShadowSettings(Color color, Vector2 offset)
			{
				this.color = color;
				this.offset = offset;
			}

			// Token: 0x04002A16 RID: 10774
			public Color color;

			// Token: 0x04002A17 RID: 10775
			public Vector2 offset;
		}

		// Token: 0x020008E6 RID: 2278
		public interface IMaskedSpriteModifier
		{
			// Token: 0x06003C76 RID: 15478
			void ModifyMaskedSprite(ref MaskedSprite.ShaderSettings shaderSettings, List<MaskedSprite.BorderSettings> borders);
		}
	}
}
