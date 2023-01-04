using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Voxels.TowerDefense.SpriteMagic;

namespace Voxels.TowerDefense
{
	// Token: 0x0200088A RID: 2186
	public class WindParticleSystem : MonoBehaviour
	{
		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x0600392A RID: 14634 RVA: 0x000F9A29 File Offset: 0x000F7E29
		private int width
		{
			get
			{
				return Mathf.ClosestPowerOfTwo((int)this.texSize.x);
			}
		}

		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x0600392B RID: 14635 RVA: 0x000F9A3C File Offset: 0x000F7E3C
		private int height
		{
			get
			{
				return Mathf.ClosestPowerOfTwo((int)this.texSize.y);
			}
		}

		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x0600392C RID: 14636 RVA: 0x000F9A4F File Offset: 0x000F7E4F
		private RenderTexture srcRenderTexture
		{
			get
			{
				return (!this.alternate) ? this.texture1 : this.texture0;
			}
		}

		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x0600392D RID: 14637 RVA: 0x000F9A6D File Offset: 0x000F7E6D
		private RenderTexture dstRenderTexture
		{
			get
			{
				return (!this.alternate) ? this.texture0 : this.texture1;
			}
		}

		// Token: 0x0600392E RID: 14638 RVA: 0x000F9A8C File Offset: 0x000F7E8C
		[ContextMenu("Randomize")]
		public void Randomize()
		{
			if (!this.randomTex)
			{
				this.randomTex = new Texture2D(this.width, this.height, TextureFormat.RGBA32, false, true);
				Color32[] pixels = this.randomTex.GetPixels32();
				for (int i = 0; i < pixels.Length; i++)
				{
					pixels[i] = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value, 1f);
				}
				this.randomTex.SetPixels32(pixels);
				this.randomTex.Apply();
			}
			Graphics.Blit(this.randomTex, this.texture0);
			Graphics.Blit(this.randomTex, this.texture1);
		}

		// Token: 0x0600392F RID: 14639 RVA: 0x000F9B46 File Offset: 0x000F7F46
		[ContextMenu("Warmup")]
		public void Warmup()
		{
			this.Warmup(100, 7f);
		}

		// Token: 0x06003930 RID: 14640 RVA: 0x000F9B58 File Offset: 0x000F7F58
		public void Warmup(int iterations, float timeScale)
		{
			if (!this.mesh)
			{
				this.Setup();
			}
			this.Randomize();
			this.moveMaterial.SetFloat(WindParticleSystem.amountId, this.amount);
			this.moveMaterial.SetFloat(WindParticleSystem.deltaId, this.timeStep * timeScale);
			for (int i = 0; i < iterations; i++)
			{
				Graphics.Blit(this.texture0, this.texture1, this.moveMaterial);
				Graphics.Blit(this.texture1, this.texture0, this.moveMaterial);
			}
			this.moveMaterial.SetFloat(WindParticleSystem.deltaId, this.timeStep);
		}

		// Token: 0x06003931 RID: 14641 RVA: 0x000F9C14 File Offset: 0x000F8014
		public void SpawnParticle(Vector3 pos)
		{
			int num = this.endIndex % this.count;
			if (this.pixels == null)
			{
				this.pixels = this.srcTex.GetPixels32();
			}
			this.pixels[num] = Wind.staticWorld2Wind.MultiplyPoint(pos).SetW(1f);
			Vector2 vector = new Vector2(Time.timeSinceLevelLoad, UnityEngine.Random.value);
			this.uv4s[num * 4] = vector;
			this.uv4s[num * 4 + 1] = vector;
			this.uv4s[num * 4 + 2] = vector;
			this.uv4s[num * 4 + 3] = vector;
			this.endIndex++;
		}

		// Token: 0x06003932 RID: 14642 RVA: 0x000F9CF0 File Offset: 0x000F80F0
		private void Setup()
		{
			this.mesh = new Mesh();
			this.mesh.MarkDynamic();
			if (!WindParticleSystem.alphaBlitMaterial)
			{
				WindParticleSystem.alphaBlitMaterial = new Material(this.alphaBlit);
			}
			List<SpriteBounds> list = this.sprites.ToList<Sprite>().ConvertAll<SpriteBounds>((Sprite x) => new SpriteBounds(x));
			this.count = this.width * this.height;
			int[] array = new int[this.count * 6];
			Vector3[] vertices = new Vector3[this.count * 4];
			Vector2[] array2 = new Vector2[this.count * 4];
			Vector2[] array3 = new Vector2[this.count * 4];
			Vector2[] array4 = new Vector2[this.count * 4];
			this.uv4s = new Vector2[this.count * 4];
			for (int i = 0; i < this.count; i++)
			{
				int num = i * 6;
				int num2 = i * 4;
				array[num] = num2;
				array[num + 1] = num2 + 1;
				array[num + 2] = num2 + 2;
				array[num + 3] = num2 + 2;
				array[num + 4] = num2 + 1;
				array[num + 5] = num2 + 3;
				SpriteBounds spriteBounds = list[i % this.sprites.Length];
				array2[i * 4] = spriteBounds.GetUvCorner(0);
				array2[i * 4 + 1] = spriteBounds.GetUvCorner(1);
				array2[i * 4 + 2] = spriteBounds.GetUvCorner(2);
				array2[i * 4 + 3] = spriteBounds.GetUvCorner(3);
				if (this.flipX && UnityEngine.Random.value > 0.5f)
				{
					Vector2 vector = array2[i * 4];
					array2[i * 4] = array2[i * 4 + 1];
					array2[i * 4 + 1] = vector;
					vector = array2[i * 4 + 2];
					array2[i * 4 + 2] = array2[i * 4 + 3];
					array2[i * 4 + 3] = vector;
				}
				if (this.flipY && UnityEngine.Random.value > 0.5f)
				{
					Vector2 vector2 = array2[i * 4];
					array2[i * 4] = array2[i * 4 + 2];
					array2[i * 4 + 2] = vector2;
					vector2 = array2[i * 4 + 2];
					array2[i * 4 + 2] = array2[i * 4 + 1];
					array2[i * 4 + 1] = vector2;
				}
				array3[i * 4] = spriteBounds.GetUv2Corner(0);
				array3[i * 4 + 1] = spriteBounds.GetUv2Corner(1);
				array3[i * 4 + 2] = spriteBounds.GetUv2Corner(2);
				array3[i * 4 + 3] = spriteBounds.GetUv2Corner(3);
				Vector2 vector3 = new Vector2((float)(i % this.width) / (float)this.width, (float)(i / this.width) / (float)this.height);
				array4[i * 4] = vector3;
				array4[i * 4 + 1] = vector3;
				array4[i * 4 + 2] = vector3;
				array4[i * 4 + 3] = vector3;
			}
			this.mesh.vertices = vertices;
			this.mesh.uv = array2;
			this.mesh.uv2 = array3;
			this.mesh.uv3 = array4;
			this.mesh.triangles = array;
			this.mesh.bounds = new Bounds(Vector3.zero, Vector3.one * 20f);
			MeshFilter orAddComponent = base.gameObject.GetOrAddComponent<MeshFilter>();
			orAddComponent.mesh = this.mesh;
			if (SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGBHalf))
			{
				this.texture0 = new RenderTexture(this.width, this.height, 24, RenderTextureFormat.ARGBHalf);
				this.texture1 = new RenderTexture(this.width, this.height, 24, RenderTextureFormat.ARGBHalf);
			}
			else
			{
				this.texture0 = new RenderTexture(this.width, this.height, 24, RenderTextureFormat.ARGB32);
				this.texture1 = new RenderTexture(this.width, this.height, 24, RenderTextureFormat.ARGB32);
			}
			this.texture0.filterMode = FilterMode.Point;
			this.texture1.filterMode = FilterMode.Point;
			this.texture0.Create();
			this.texture1.Create();
			this.srcTex = new Texture2D(this.width, this.height, TextureFormat.RGBA32, false, true);
			this.pixels = this.srcTex.GetPixels32();
			Color32 color = Color.clear;
			for (int j = 0; j < this.pixels.Length; j++)
			{
				this.pixels[j] = color;
			}
			this.noiseTex = new Texture2D(this.width, this.height, TextureFormat.RGBA32, false, true);
			Color32[] pixels = this.srcTex.GetPixels32();
			for (int k = 0; k < pixels.Length; k++)
			{
				pixels[k] = (UnityEngine.Random.insideUnitSphere + Vector3.one / 2f).SetW(UnityEngine.Random.value);
			}
			this.noiseTex.SetPixels32(pixels);
			this.noiseTex.Apply();
			this.mr = base.gameObject.GetOrAddComponent<MeshRenderer>();
			this.mr.sharedMaterial = this.drawMaterial;
			this.drawBlock = new MaterialPropertyBlock();
			this.drawBlock.SetTexture("_MainTex", this.sprites[0].texture);
			this.drawBlock.SetTexture("_PosTex0", this.texture0);
			this.drawBlock.SetTexture("_PosTex1", this.texture1);
			this.mr.SetPropertyBlock(this.drawBlock);
			this.moveMaterial = UnityEngine.Object.Instantiate<Material>(this.srcMoveMaterial);
			this.moveMaterial.SetFloat("_DeltaTime", this.timeStep);
			this.moveMaterial.SetTexture("_NoiseTex", this.noiseTex);
			if (this.randomizeStart)
			{
				this.Randomize();
				this.Warmup();
			}
		}

		// Token: 0x06003933 RID: 14643 RVA: 0x000FA3C7 File Offset: 0x000F87C7
		private void Awake()
		{
			this.Setup();
		}

		// Token: 0x06003934 RID: 14644 RVA: 0x000FA3D0 File Offset: 0x000F87D0
		private void ApplyNewParticles(RenderTexture targetTex)
		{
			if (this.startIndex != this.endIndex)
			{
				this.srcTex.SetPixels32(this.pixels);
				this.srcTex.Apply();
				Graphics.Blit(this.srcTex, targetTex, WindParticleSystem.alphaBlitMaterial);
				Color clear = Color.clear;
				for (int i = this.startIndex; i < this.endIndex; i++)
				{
					this.pixels[i % this.count] = clear;
				}
				this.mesh.uv4 = this.uv4s;
				this.startIndex = this.endIndex;
			}
		}

		// Token: 0x06003935 RID: 14645 RVA: 0x000FA478 File Offset: 0x000F8878
		public void Redraw(float dt)
		{
			this.ApplyNewParticles(this.srcRenderTexture);
			this.moveMaterial.SetFloat(WindParticleSystem.amountId, this.amount);
			this.moveMaterial.SetFloat(WindParticleSystem.deltaId, dt);
			this.moveMaterial.SetVector(WindParticleSystem.randomId, new Vector4(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value));
			Graphics.Blit(this.srcRenderTexture, this.dstRenderTexture, this.moveMaterial);
			this.alternate = !this.alternate;
		}

		// Token: 0x06003936 RID: 14646 RVA: 0x000FA516 File Offset: 0x000F8916
		public void SetInterpolate(float interpolate)
		{
			this.drawBlock.SetFloat(WindParticleSystem.interpolatorId, interpolate + (float)((!this.alternate) ? 0 : 1));
			this.mr.SetPropertyBlock(this.drawBlock);
		}

		// Token: 0x06003937 RID: 14647 RVA: 0x000FA554 File Offset: 0x000F8954
		private IEnumerator ParticleUpdate()
		{
			for (;;)
			{
				this.ApplyNewParticles(this.texture0);
				this.moveMaterial.SetFloat(WindParticleSystem.amountId, this.amount);
				this.moveMaterial.SetVector(WindParticleSystem.randomId, new Vector4(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value));
				Graphics.Blit(this.texture0, this.texture1, this.moveMaterial);
				while (this.interpolator < 1f)
				{
					this.drawBlock.SetFloat(WindParticleSystem.interpolatorId, this.interpolator);
					this.mr.SetPropertyBlock(this.drawBlock);
					this.interpolator += Time.deltaTime / this.timeStep;
					yield return null;
				}
				this.ApplyNewParticles(this.texture1);
				this.moveMaterial.SetFloat(WindParticleSystem.amountId, this.amount);
				this.moveMaterial.SetVector(WindParticleSystem.randomId, new Vector4(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value));
				Graphics.Blit(this.texture1, this.texture0, this.moveMaterial);
				while (this.interpolator < 2f)
				{
					this.drawBlock.SetFloat(WindParticleSystem.interpolatorId, this.interpolator);
					this.mr.SetPropertyBlock(this.drawBlock);
					this.interpolator += Time.deltaTime / this.timeStep;
					yield return null;
				}
				this.interpolator -= 2f;
			}
			yield break;
		}

		// Token: 0x04002727 RID: 10023
		[SerializeField]
		private Shader alphaBlit;

		// Token: 0x04002728 RID: 10024
		private static Material alphaBlitMaterial;

		// Token: 0x04002729 RID: 10025
		[SerializeField]
		private bool randomizeStart = true;

		// Token: 0x0400272A RID: 10026
		[Space]
		public float timeStep = 0.1f;

		// Token: 0x0400272B RID: 10027
		[SerializeField]
		private Vector2 texSize = new Vector2(64f, 64f);

		// Token: 0x0400272C RID: 10028
		[Space]
		[SpritePreview]
		public Sprite sprite;

		// Token: 0x0400272D RID: 10029
		public Sprite[] sprites;

		// Token: 0x0400272E RID: 10030
		public bool flipX;

		// Token: 0x0400272F RID: 10031
		public bool flipY;

		// Token: 0x04002730 RID: 10032
		[SerializeField]
		[FormerlySerializedAs("moveMaterial")]
		private Material srcMoveMaterial;

		// Token: 0x04002731 RID: 10033
		[SerializeField]
		[FormerlySerializedAs("srcDrawMaterial")]
		private Material drawMaterial;

		// Token: 0x04002732 RID: 10034
		private Material moveMaterial;

		// Token: 0x04002733 RID: 10035
		public float amount;

		// Token: 0x04002734 RID: 10036
		private static ShaderId amountId = "_Amount";

		// Token: 0x04002735 RID: 10037
		private static ShaderId interpolatorId = "_Interpolator";

		// Token: 0x04002736 RID: 10038
		private static ShaderId randomId = "_Random";

		// Token: 0x04002737 RID: 10039
		private static ShaderId deltaId = "_DeltaTime";

		// Token: 0x04002738 RID: 10040
		private Mesh mesh;

		// Token: 0x04002739 RID: 10041
		private IEnumerator particleUpdate;

		// Token: 0x0400273A RID: 10042
		private float interpolator;

		// Token: 0x0400273B RID: 10043
		private int count;

		// Token: 0x0400273C RID: 10044
		private int startIndex;

		// Token: 0x0400273D RID: 10045
		private int endIndex;

		// Token: 0x0400273E RID: 10046
		private Vector2[] uv4s;

		// Token: 0x0400273F RID: 10047
		private Color32[] pixels;

		// Token: 0x04002740 RID: 10048
		private bool alternate;

		// Token: 0x04002741 RID: 10049
		[SerializeField]
		private RenderTexture texture0;

		// Token: 0x04002742 RID: 10050
		[SerializeField]
		private RenderTexture texture1;

		// Token: 0x04002743 RID: 10051
		[SerializeField]
		private Texture2D randomTex;

		// Token: 0x04002744 RID: 10052
		[SerializeField]
		private Texture2D srcTex;

		// Token: 0x04002745 RID: 10053
		[SerializeField]
		private Texture2D noiseTex;

		// Token: 0x04002746 RID: 10054
		private MaterialPropertyBlock drawBlock;

		// Token: 0x04002747 RID: 10055
		private MeshRenderer mr;
	}
}
