using System;
using ReflexCLI.Attributes;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x02000870 RID: 2160
	public class IslandStyle2 : MonoBehaviour
	{
		// Token: 0x0600389D RID: 14493 RVA: 0x000F4A57 File Offset: 0x000F2E57
		private void OnValidate()
		{
			this.Apply();
		}

		// Token: 0x0600389E RID: 14494 RVA: 0x000F4A5F File Offset: 0x000F2E5F
		[ContextMenu("Apply")]
		private void Apply()
		{
			if (Application.isPlaying)
			{
				this.MaybeCreateTexture();
				this.GenerateTexture();
				this.ApplyShaderConstants(this.year);
				Shader.SetGlobalFloat("_Year", this.year);
			}
		}

		// Token: 0x0600389F RID: 14495 RVA: 0x000F4A93 File Offset: 0x000F2E93
		private void MaybeCreateTexture()
		{
			if (!this.tex)
			{
				this.tex = new Texture2D(64, 2, TextureFormat.ARGB32, false);
				this.GenerateTexture();
			}
		}

		// Token: 0x060038A0 RID: 14496 RVA: 0x000F4ABC File Offset: 0x000F2EBC
		private void GenerateTexture()
		{
			for (int i = 0; i < this.tex.width; i++)
			{
				float time = (float)i / (float)this.tex.width;
				Color color = this.gradient.Evaluate(time);
				this.tex.SetPixel(i, 0, color);
				this.tex.SetPixel(i, 1, color);
			}
			this.tex.Apply();
		}

		// Token: 0x060038A1 RID: 14497 RVA: 0x000F4B29 File Offset: 0x000F2F29
		[ContextMenu("Apply")]
		[ConsoleCommand("")]
		public void ApplyShaderConstants(float year)
		{
			this.MaybeCreateTexture();
			Shader.SetGlobalVector(this.colorId, this.gradient.Evaluate(year));
			Shader.SetGlobalTexture(this.texId, this.tex);
		}

		// Token: 0x04002684 RID: 9860
		[SerializeField]
		private string texId;

		// Token: 0x04002685 RID: 9861
		[SerializeField]
		private string colorId;

		// Token: 0x04002686 RID: 9862
		[SerializeField]
		private Gradient gradient = new Gradient();

		// Token: 0x04002687 RID: 9863
		[SerializeField]
		private Texture2D tex;

		// Token: 0x04002688 RID: 9864
		[SerializeField]
		[Range(0f, 1f)]
		private float year = 0.5f;
	}
}
