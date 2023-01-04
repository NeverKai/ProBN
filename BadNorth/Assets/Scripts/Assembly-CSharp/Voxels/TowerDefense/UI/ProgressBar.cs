using System;
using UnityEngine;
using UnityEngine.UI;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x0200079B RID: 1947
	public class ProgressBar : MonoBehaviour
	{
		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x06003228 RID: 12840 RVA: 0x000D49F6 File Offset: 0x000D2DF6
		public Image image
		{
			get
			{
				return this.progressImage;
			}
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06003229 RID: 12841 RVA: 0x000D49FE File Offset: 0x000D2DFE
		// (set) Token: 0x0600322A RID: 12842 RVA: 0x000D4A06 File Offset: 0x000D2E06
		public float ratio
		{
			get
			{
				return this._ratio;
			}
			set
			{
				this._ratio = value;
				if (!base.enabled)
				{
					this.progressImage.fillAmount = value;
				}
			}
		}

		// Token: 0x0600322B RID: 12843 RVA: 0x000D4A28 File Offset: 0x000D2E28
		public void ResetRatio(float resetRatio = 0f)
		{
			Image image = this.progressImage;
			this._ratio = resetRatio;
			image.fillAmount = resetRatio;
		}

		// Token: 0x0600322C RID: 12844 RVA: 0x000D4A4C File Offset: 0x000D2E4C
		private void Init()
		{
			if (!this.progressImage)
			{
				return;
			}
			if (!this.progressImage.sprite)
			{
				this.progressImage.sprite = this.GetOrCreateProcSprite();
				this.progressImage.type = Image.Type.Filled;
				this.progressImage.fillMethod = Image.FillMethod.Horizontal;
			}
			this.ratio = this._ratio;
			base.enabled = (this.exponent > 0f);
		}

		// Token: 0x0600322D RID: 12845 RVA: 0x000D4AC7 File Offset: 0x000D2EC7
		private void Awake()
		{
			this.Init();
		}

		// Token: 0x0600322E RID: 12846 RVA: 0x000D4ACF File Offset: 0x000D2ECF
		private void Update()
		{
			this.progressImage.fillAmount = Mathf.Lerp(this.progressImage.fillAmount, this.ratio, Time.unscaledDeltaTime * this.exponent);
		}

		// Token: 0x0600322F RID: 12847 RVA: 0x000D4B00 File Offset: 0x000D2F00
		private Sprite GetOrCreateProcSprite()
		{
			if (!ProgressBar.procSprite)
			{
				Texture2D texture2D = new Texture2D(1, 1, TextureFormat.RGBA32, false);
				texture2D.SetPixel(0, 0, Color.white);
				texture2D.Apply();
				ProgressBar.procSprite = Sprite.Create(texture2D, new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), new Vector2(0.5f, 0.5f), 100f);
			}
			return ProgressBar.procSprite;
		}

		// Token: 0x06003230 RID: 12848 RVA: 0x000D4B7A File Offset: 0x000D2F7A
		private void OnValidate()
		{
			this.Init();
		}

		// Token: 0x04002213 RID: 8723
		private static Sprite procSprite;

		// Token: 0x04002214 RID: 8724
		[SerializeField]
		private Image progressImage;

		// Token: 0x04002215 RID: 8725
		[SerializeField]
		[Range(0f, 1f)]
		private float _ratio;

		// Token: 0x04002216 RID: 8726
		[SerializeField]
		private float exponent;
	}
}
