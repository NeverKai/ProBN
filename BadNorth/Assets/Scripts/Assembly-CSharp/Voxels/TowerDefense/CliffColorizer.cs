using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x02000787 RID: 1927
	public class CliffColorizer : IslandComponent, IIslandFirstEnter, IIslandEnter, IIslandAwake, IIslandDestroyEntered
	{
		// Token: 0x060031D5 RID: 12757 RVA: 0x000D1064 File Offset: 0x000CF464
		void IIslandAwake.OnIslandAwake(Island island)
		{
			float value = (float)island.levelNode.index / (float)island.levelNode.campaign.levels.Count;
			this.darkProbability = ExtraMath.RemapValue(value, this.cliffRangeMin, this.cliffRangeMax);
		}

		// Token: 0x060031D6 RID: 12758 RVA: 0x000D10AD File Offset: 0x000CF4AD
		private void OnValidate()
		{
			if (this.cliffTex)
			{
				this.GenerateTexture();
				this.SetShaderVariables();
			}
		}

		// Token: 0x060031D7 RID: 12759 RVA: 0x000D10CC File Offset: 0x000CF4CC
		IEnumerator<GenInfo> IIslandFirstEnter.OnIslandFirstEnter(Island island)
		{
			this.cliffTex = island.texturePool.GetTexture(1, CliffColorizer.pixels.Length, true);
			this.cliffTex.wrapMode = TextureWrapMode.Clamp;
			this.GenerateTexture();
			yield return new GenInfo("Cliff Colorizer", GenInfo.Mode.interruptable);
			yield break;
		}

		// Token: 0x060031D8 RID: 12760 RVA: 0x000D10F0 File Offset: 0x000CF4F0
		private static Color32 GetRandomColor(float probability)
		{
			return new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue)
			{
				r = ((UnityEngine.Random.value >= probability) ? 0 : byte.MaxValue),
				g = (byte)(UnityEngine.Random.value * 255f),
				b = (byte)(255f * Mathf.Clamp01(probability * 2f + UnityEngine.Random.value - 1f))
			};
		}

		// Token: 0x060031D9 RID: 12761 RVA: 0x000D1170 File Offset: 0x000CF570
		private void SetShaderVariables()
		{
			Vector4 value = default(Vector4);
			float num = Mathf.Lerp(this.cliffColor0, this.cliffColor1, this.darkProbability);
			value.x = Mathf.Lerp(this.cliffColor0, num, 0.4f);
			value.y = Mathf.Lerp(this.cliffColor1, num, 0.4f);
			value.z = this.darkProbability;
			value.w = num;
			if (this.darkProbability > 0f && this.darkProbability < 1f)
			{
				Shader.EnableKeyword("_CLIFF_MIX");
			}
			else
			{
				Shader.DisableKeyword("_CLIFF_MIX");
			}
			Shader.SetGlobalVector(CliffColorizer.cliffParamsId, value);
			Shader.SetGlobalTexture(CliffColorizer.cliffTexId, this.cliffTex);
		}

		// Token: 0x060031DA RID: 12762 RVA: 0x000D1240 File Offset: 0x000CF640
		private void GenerateTexture()
		{
			Color32 randomColor = CliffColorizer.GetRandomColor(this.darkProbability);
			float num = 10f / (float)CliffColorizer.pixels.Length;
			for (int i = 0; i < CliffColorizer.pixels.Length; i++)
			{
				float num2 = (float)i * num;
				float num3 = Mathf.Abs(Mathf.Round(num2) - num2);
				if ((double)num3 > 0.2 && (double)UnityEngine.Random.value < 0.6)
				{
					randomColor = CliffColorizer.GetRandomColor(this.darkProbability);
				}
				CliffColorizer.pixels[i] = randomColor;
			}
			this.cliffTex.SetPixels32(CliffColorizer.pixels);
			this.cliffTex.Apply();
		}

		// Token: 0x060031DB RID: 12763 RVA: 0x000D12F4 File Offset: 0x000CF6F4
		IEnumerator<GenInfo> IIslandEnter.OnIslandEnter(Island island)
		{
			this.SetShaderVariables();
			yield return new GenInfo("CliffColorizer", GenInfo.Mode.interruptable);
			yield break;
		}

		// Token: 0x060031DC RID: 12764 RVA: 0x000D130F File Offset: 0x000CF70F
		void IIslandDestroyEntered.OnIslandDestroyEntered(Island island)
		{
			island.texturePool.ReturnTexture(ref this.cliffTex);
		}

		// Token: 0x040021A9 RID: 8617
		[SerializeField]
		private Texture2D cliffTex;

		// Token: 0x040021AA RID: 8618
		[SerializeField]
		[Range(0f, 1f)]
		private float darkProbability;

		// Token: 0x040021AB RID: 8619
		private const float range = 10f;

		// Token: 0x040021AC RID: 8620
		private static Color32[] pixels = new Color32[128];

		// Token: 0x040021AD RID: 8621
		private static ShaderId cliffTexId = "_CliffTex";

		// Token: 0x040021AE RID: 8622
		private static ShaderId cliffParamsId = "_CliffParams";

		// Token: 0x040021AF RID: 8623
		public float cliffColor0 = 0.7f;

		// Token: 0x040021B0 RID: 8624
		public float cliffColor1 = 0.3f;

		// Token: 0x040021B1 RID: 8625
		public float cliffRangeMin = 0.4f;

		// Token: 0x040021B2 RID: 8626
		public float cliffRangeMax = 0.6f;
	}
}
