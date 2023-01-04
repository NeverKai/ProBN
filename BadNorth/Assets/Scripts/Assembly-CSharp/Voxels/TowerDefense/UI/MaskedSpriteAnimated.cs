using System;
using System.Collections.Generic;
using RTM.UISystem;
using UnityEngine;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x020008E7 RID: 2279
	public class MaskedSpriteAnimated : MonoBehaviour, MaskedSprite.IMaskedSpriteModifier
	{
		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x06003C78 RID: 15480 RVA: 0x0010D876 File Offset: 0x0010BC76
		public MaskedSprite maskedSprite
		{
			get
			{
				if (!this._maskedSprite)
				{
					this._maskedSprite = base.GetComponent<MaskedSprite>();
				}
				return this._maskedSprite;
			}
		}

		// Token: 0x06003C79 RID: 15481 RVA: 0x0010D89A File Offset: 0x0010BC9A
		private void OnValidate()
		{
			this.UpdateSettings(1f, true);
			this.maskedSprite.image.SetVerticesDirty();
		}

		// Token: 0x06003C7A RID: 15482 RVA: 0x0010D8B8 File Offset: 0x0010BCB8
		private void OnEnable()
		{
			this.UpdateSettings(1f, true);
		}

		// Token: 0x06003C7B RID: 15483 RVA: 0x0010D8C8 File Offset: 0x0010BCC8
		private void UpdateSettings(float dt, bool canSetDirty = true)
		{
			this.innerBorderTarget = this.settings.innerBorder;
			MaskedSprite.BorderSettings borderSettings = new MaskedSprite.BorderSettings(ScriptableObjectSingleton<MaskedSpriteConstants>.instance.SelectionBorderColor, 0f, 0.5f);
			this.shaderTarget = this.maskedSprite.shaderSettings;
			if (this.selected || this.state == UIInteractable.State.Focus)
			{
				this.shaderTarget.brightness = this.shaderTarget.brightness + 0.1f;
				this.innerBorderTarget.width = this.innerBorderTarget.width * 0.8f;
				borderSettings.width += this.outerBorderExpand;
				this.shaderTarget.spriteScale = this.shaderTarget.spriteScale * 1.01f;
				this.shaderTarget.maskScale = this.shaderTarget.maskScale * 1.02f;
				this.innerBorderTarget.color = this.innerBorderTarget.color + new Color(0.1f, 0.1f, 0.1f, 0f);
				this.shaderTarget.outlineWidth = this.shaderTarget.outlineWidth + 1f;
			}
			switch (this.state)
			{
			case UIInteractable.State.Hover:
				this.shaderTarget.brightness = this.shaderTarget.brightness + 0.1f;
				this.shaderTarget.outlineWidth = this.shaderTarget.outlineWidth + 0.4f;
				this.innerBorderTarget.width = this.innerBorderTarget.width + 0.1f;
				this.innerBorderTarget.color = this.innerBorderTarget.color + new Color(0.1f, 0.1f, 0.1f, 0f);
				break;
			case UIInteractable.State.PointerButtonDown:
				this.shaderTarget.brightness = this.shaderTarget.brightness + 0.2f;
				this.shaderTarget.maskScale = this.shaderTarget.maskScale * 0.98f;
				this.innerBorderTarget.width = this.innerBorderTarget.width + 0.03f;
				break;
			}
			if (!this.avaliable)
			{
				this.shaderTarget.saturation = 0f;
				this.shaderTarget.spriteScale = this.shaderTarget.spriteScale * 0.98f;
			}
			if (canSetDirty && (this.innerBorderCurrent != this.innerBorderTarget || this.outerBorderCurrent != borderSettings || this.shaderCurrent != this.shaderTarget))
			{
				this.maskedSprite.image.SetVerticesDirty();
			}
			this.innerBorderCurrent = MaskedSprite.BorderSettings.Lerp(this.innerBorderCurrent, this.innerBorderTarget, dt);
			this.outerBorderCurrent = MaskedSprite.BorderSettings.Lerp(this.outerBorderCurrent, borderSettings, dt);
			this.shaderCurrent = MaskedSprite.ShaderSettings.Lerp(this.shaderCurrent, this.shaderTarget, dt);
		}

		// Token: 0x06003C7C RID: 15484 RVA: 0x0010DB9C File Offset: 0x0010BF9C
		private void LateUpdate()
		{
			float dt = (Time.unscaledTime - this.lastTime) * 20f;
			this.lastTime = Time.unscaledTime;
			this.UpdateSettings(dt, true);
		}

		// Token: 0x06003C7D RID: 15485 RVA: 0x0010DBD0 File Offset: 0x0010BFD0
		void MaskedSprite.IMaskedSpriteModifier.ModifyMaskedSprite(ref MaskedSprite.ShaderSettings shaderSettings, List<MaskedSprite.BorderSettings> borders)
		{
			if (!base.enabled)
			{
				return;
			}
			if (!Application.isPlaying)
			{
				this.UpdateSettings(1f, false);
			}
			borders.Add(this.innerBorderCurrent);
			borders.Add(this.outerBorderCurrent);
			shaderSettings = this.shaderCurrent;
		}

		// Token: 0x04002A18 RID: 10776
		[SerializeField]
		public MaskedSpriteAnimated.MaskedSpriteClickableSettings settings;

		// Token: 0x04002A19 RID: 10777
		private MaskedSprite.ShaderSettings shaderCurrent;

		// Token: 0x04002A1A RID: 10778
		private MaskedSprite.ShaderSettings shaderTarget;

		// Token: 0x04002A1B RID: 10779
		private MaskedSprite.BorderSettings innerBorderCurrent;

		// Token: 0x04002A1C RID: 10780
		private MaskedSprite.BorderSettings innerBorderTarget;

		// Token: 0x04002A1D RID: 10781
		private MaskedSprite.BorderSettings outerBorderCurrent;

		// Token: 0x04002A1E RID: 10782
		private MaskedSprite.BorderSettings outerBorderTarget;

		// Token: 0x04002A1F RID: 10783
		public UIInteractable.State state;

		// Token: 0x04002A20 RID: 10784
		public bool selected;

		// Token: 0x04002A21 RID: 10785
		public bool avaliable = true;

		// Token: 0x04002A22 RID: 10786
		[SerializeField]
		private float outerBorderExpand = 0.5f;

		// Token: 0x04002A23 RID: 10787
		private float lastTime = float.MinValue;

		// Token: 0x04002A24 RID: 10788
		private MaskedSprite _maskedSprite;

		// Token: 0x020008E8 RID: 2280
		[Serializable]
		public class MaskedSpriteClickableSettings
		{
			// Token: 0x04002A25 RID: 10789
			public MaskedSprite.BorderSettings innerBorder = new MaskedSprite.BorderSettings(Color.white, 0.2f, 2f);
		}
	}
}
