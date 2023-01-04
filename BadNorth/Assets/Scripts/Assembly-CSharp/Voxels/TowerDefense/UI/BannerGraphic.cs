using System;
using UnityEngine;
using UnityEngine.UI;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x020008A4 RID: 2212
	public class BannerGraphic : MonoBehaviour
	{
		// Token: 0x060039D4 RID: 14804 RVA: 0x000FCDE0 File Offset: 0x000FB1E0
		public void Setup(HeroDefinition heroDef)
		{
			this.bannerStretch.gameObject.SetActive(true);
			Sprite flag = heroDef.graphics.flag;
			Texture2D texture = flag.texture;
			Rect textureRect = flag.textureRect;
			textureRect = new Rect(Vector2.Scale(textureRect.min, texture.texelSize), Vector2.Scale(textureRect.size, texture.texelSize));
			Rect uvRect = textureRect;
			uvRect.xMin = Mathf.Lerp(uvRect.xMin, uvRect.xMax, 0.02f);
			Rect uvRect2 = textureRect;
			uvRect2.xMin = uvRect.xMin;
			uvRect2.xMax = uvRect.xMin;
			this.bannerStretch.texture = texture;
			this.bannerTip.texture = texture;
			this.bannerTip.uvRect = uvRect;
			this.bannerStretch.uvRect = uvRect2;
			this.bannerTip.rectTransform.sizeDelta = this.bannerTip.rectTransform.sizeDelta.SetX(this.bannerTip.rectTransform.rect.height * (uvRect.width * (float)texture.width / (uvRect.height * (float)texture.height)));
			this.bannerStretch.color = heroDef.GetUIBannerColor();
			this.bannerTip.color = this.bannerStretch.color;
		}

		// Token: 0x040027E0 RID: 10208
		[SerializeField]
		private RawImage bannerStretch;

		// Token: 0x040027E1 RID: 10209
		[SerializeField]
		private RawImage bannerTip;
	}
}
