using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense.SpriteMagic
{
	// Token: 0x020007D1 RID: 2001
	public class BatchedSprite : MonoBehaviour
	{
		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x060033D5 RID: 13269 RVA: 0x000DF69C File Offset: 0x000DDA9C
		private SpriteRenderer spriteRenderer
		{
			get
			{
				if (this._spriteRenderer == null)
				{
					this._spriteRenderer = base.GetComponent<SpriteRenderer>();
				}
				return this._spriteRenderer;
			}
		}

		// Token: 0x060033D6 RID: 13270 RVA: 0x000DF6C4 File Offset: 0x000DDAC4
		private static Mesh GetMesh()
		{
			if (BatchedSprite.meshPool.Count > 0)
			{
				return BatchedSprite.meshPool.Pop();
			}
			if (!BatchedSprite.srcMesh)
			{
				BatchedSprite.srcMesh = new Mesh();
				BatchedSprite.srcMesh.MarkDynamic();
				for (int i = 0; i < 4; i++)
				{
					BatchedSprite.vector3s[i] = Vector3.zero;
				}
				BatchedSprite.srcMesh.vertices = BatchedSprite.vector3s;
				for (int j = 0; j < 4; j++)
				{
					BatchedSprite.vector3s[j] = Vector3.right;
				}
				BatchedSprite.srcMesh.normals = BatchedSprite.vector3s;
				for (int k = 0; k < 4; k++)
				{
					BatchedSprite.vector4s[k] = Vector4.zero;
				}
				BatchedSprite.srcMesh.tangents = BatchedSprite.vector4s;
				for (int l = 0; l < 4; l++)
				{
					BatchedSprite.vector2s[l] = Vector2.zero;
				}
				BatchedSprite.srcMesh.uv = BatchedSprite.vector2s;
				for (int m = 0; m < 4; m++)
				{
					BatchedSprite.vector2s[m] = Vector2.zero;
				}
				BatchedSprite.srcMesh.uv2 = BatchedSprite.vector2s;
				for (int n = 0; n < 4; n++)
				{
					BatchedSprite.colors[n] = Color.white;
				}
				BatchedSprite.srcMesh.colors32 = BatchedSprite.colors;
				BatchedSprite.srcMesh.triangles = BatchedSprite.tris;
			}
			Mesh mesh = UnityEngine.Object.Instantiate<Mesh>(BatchedSprite.srcMesh);
			mesh.MarkDynamic();
			return mesh;
		}

		// Token: 0x060033D7 RID: 13271 RVA: 0x000DF886 File Offset: 0x000DDC86
		[ContextMenu("UpdateScale")]
		public void UpdateScale()
		{
			if (this.mesh)
			{
				this.SetScaledUV2();
			}
		}

		// Token: 0x060033D8 RID: 13272 RVA: 0x000DF8A0 File Offset: 0x000DDCA0
		[ContextMenu("UpdateScale")]
		public void UpdateColor()
		{
			if (this.mesh)
			{
				Color32 color = this.spriteRenderer.color;
				for (int i = 0; i < 4; i++)
				{
					BatchedSprite.colors[i] = color;
				}
				this.mesh.colors32 = BatchedSprite.colors;
			}
		}

		// Token: 0x060033D9 RID: 13273 RVA: 0x000DF904 File Offset: 0x000DDD04
		private void SetScaledUV2()
		{
			Vector3 lossyScale = base.transform.lossyScale;
			Vector2 b = lossyScale;
			this.spriteBounds.SetUv2Corner(BatchedSprite.vector2s, 0);
			float num = Mathf.Max(this.spriteBounds.v.width * 0.5f, -this.spriteBounds.v.yMin) + this.tangentW;
			Vector4 vector = new Vector4(0f, 1f, 0f, num * lossyScale.z);
			for (int i = 0; i < 4; i++)
			{
				BatchedSprite.vector2s[i] = Vector2.Scale(BatchedSprite.vector2s[i], b);
				BatchedSprite.vector4s[i] = vector;
			}
			this.mesh.uv2 = BatchedSprite.vector2s;
			this.mesh.tangents = BatchedSprite.vector4s;
		}

		// Token: 0x060033DA RID: 13274 RVA: 0x000DF9F8 File Offset: 0x000DDDF8
		protected virtual void SetMeshToSprite(Mesh mesh, Sprite sprite)
		{
			if (!sprite)
			{
				for (int i = 0; i < 8; i++)
				{
					BatchedSprite.vector2s[i] = Vector2.zero;
				}
				mesh.uv2 = BatchedSprite.vector2s;
				return;
			}
			this.spriteBounds = BatchedSprite.spriteBoundsPool.GetSpriteBounds(sprite);
			this.spriteBounds.SetUvCorner(BatchedSprite.vector2s, 0);
			mesh.uv = BatchedSprite.vector2s;
			this.SetScaledUV2();
			mesh.bounds = new Bounds(Vector3.zero, Vector3.one * 100f);
		}

		// Token: 0x060033DB RID: 13275 RVA: 0x000DFA98 File Offset: 0x000DDE98
		private static Material GetReplacementMaterial(Material srcMaterial, string keyword)
		{
			int key = srcMaterial.GetHashCode() + 119 * keyword.GetHashCode();
			Material material;
			if (!BatchedSprite.replacementMaterials.TryGetValue(key, out material))
			{
				material = UnityEngine.Object.Instantiate<Material>(srcMaterial);
				material.name = srcMaterial.name + " " + keyword;
				material.EnableKeyword(keyword);
				BatchedSprite.replacementMaterials.Add(key, material);
			}
			return material;
		}

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x060033DC RID: 13276 RVA: 0x000DFAFA File Offset: 0x000DDEFA
		// (set) Token: 0x060033DD RID: 13277 RVA: 0x000DFB08 File Offset: 0x000DDF08
		public Sprite bSprite
		{
			get
			{
				return this.spriteRenderer.sprite;
			}
			set
			{
				if (value == this.spriteRenderer.sprite)
				{
					return;
				}
				this.spriteRenderer.sprite = value;
				if (this.mesh)
				{
					this.SetMeshToSprite(this.mesh, value);
					this.block.SetTexture(ShaderId.mainTexId, (!value) ? null : value.texture);
					this.ComittBlock();
				}
			}
		}

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x060033DE RID: 13278 RVA: 0x000DFB87 File Offset: 0x000DDF87
		// (set) Token: 0x060033DF RID: 13279 RVA: 0x000DFB94 File Offset: 0x000DDF94
		public Color color
		{
			get
			{
				return this.spriteRenderer.color;
			}
			set
			{
				if (value == this.spriteRenderer.color)
				{
					return;
				}
				this.spriteRenderer.color = value;
				if (this.mesh)
				{
					Color32 color = value;
					for (int i = 0; i < 4; i++)
					{
						BatchedSprite.colors[i] = color;
					}
					this.mesh.colors32 = BatchedSprite.colors;
				}
			}
		}

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x060033E0 RID: 13280 RVA: 0x000DFC10 File Offset: 0x000DE010
		public MaterialPropertyBlock block
		{
			get
			{
				if (this._block == null)
				{
					this._block = new MaterialPropertyBlock();
					this._block.SetTexture(ShaderId.mainTexId, (!this.bSprite) ? null : this.bSprite.texture);
				}
				return this._block;
			}
		}

		// Token: 0x060033E1 RID: 13281 RVA: 0x000DFC70 File Offset: 0x000DE070
		public void Awake()
		{
			if (!this.mesh)
			{
				this.mesh = BatchedSprite.GetMesh();
				this.SetMeshToSprite(this.mesh, this.bSprite);
				this.block.SetTexture(ShaderId.mainTexId, (!this.bSprite) ? null : this.bSprite.texture);
				for (int i = 0; i < 4; i++)
				{
					BatchedSprite.colors[i] = this.spriteRenderer.color;
				}
				this.mesh.colors32 = BatchedSprite.colors;
				if (this.spriteRenderer.sharedMaterial.HasProperty(BatchedSprite.mirrorId))
				{
					this.rends = new BatchedSprite.Rend[]
					{
						new BatchedSprite.Rend(this.spriteRenderer, this.mesh, string.Empty),
						new BatchedSprite.Rend(this.spriteRenderer, this.mesh, "_MIRROR_ON")
					};
				}
				else
				{
					this.rends = new BatchedSprite.Rend[]
					{
						new BatchedSprite.Rend(this.spriteRenderer, this.mesh, string.Empty)
					};
				}
				this._spriteRenderer.enabled = false;
				this.ComittBlock();
			}
		}

		// Token: 0x060033E2 RID: 13282 RVA: 0x000DFDBB File Offset: 0x000DE1BB
		private void OnDestroy()
		{
			BatchedSprite.meshPool.Push(this.mesh);
		}

		// Token: 0x060033E3 RID: 13283 RVA: 0x000DFDD0 File Offset: 0x000DE1D0
		public void ComittBlock()
		{
			if (this.mesh)
			{
				for (int i = 0; i < this.rends.Length; i++)
				{
					this.rends[i].mr.SetPropertyBlock(this.block);
				}
			}
		}

		// Token: 0x060033E4 RID: 13284 RVA: 0x000DFE1E File Offset: 0x000DE21E
		public virtual MaterialPropertyBlock GetMaterialPropertyBlock()
		{
			return this.block;
		}

		// Token: 0x060033E5 RID: 13285 RVA: 0x000DFE26 File Offset: 0x000DE226
		public virtual int GetMaterialKey()
		{
			return this.spriteRenderer.sharedMaterial.GetHashCode() * 197 + this.spriteRenderer.sprite.texture.GetHashCode() * 17;
		}

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x060033E6 RID: 13286 RVA: 0x000DFE57 File Offset: 0x000DE257
		// (set) Token: 0x060033E7 RID: 13287 RVA: 0x000DFE64 File Offset: 0x000DE264
		public Material sharedMaterial
		{
			get
			{
				return this.spriteRenderer.sharedMaterial;
			}
			set
			{
				if (value != this.spriteRenderer.sharedMaterial)
				{
					this.spriteRenderer.sharedMaterial = value;
					for (int i = 0; i < this.rends.Length; i++)
					{
						this.rends[i].sharedMaterial = value;
					}
				}
			}
		}

		// Token: 0x0400234B RID: 9035
		[SerializeField]
		private SpriteRenderer _spriteRenderer;

		// Token: 0x0400234C RID: 9036
		[SerializeField]
		private float tangentW;

		// Token: 0x0400234D RID: 9037
		private BatchedSprite.Rend[] rends;

		// Token: 0x0400234E RID: 9038
		private static ShaderId mirrorId = "_Mirror";

		// Token: 0x0400234F RID: 9039
		private static int[] tris = new int[]
		{
			0,
			1,
			2,
			2,
			1,
			3
		};

		// Token: 0x04002350 RID: 9040
		private static Vector2[] vector2s = new Vector2[4];

		// Token: 0x04002351 RID: 9041
		private static Vector3[] vector3s = new Vector3[4];

		// Token: 0x04002352 RID: 9042
		private static Vector4[] vector4s = new Vector4[4];

		// Token: 0x04002353 RID: 9043
		private static Color32[] colors = new Color32[4];

		// Token: 0x04002354 RID: 9044
		private static SpriteBoundsPool spriteBoundsPool = new SpriteBoundsPool();

		// Token: 0x04002355 RID: 9045
		private static Stack<Mesh> meshPool = new Stack<Mesh>(128);

		// Token: 0x04002356 RID: 9046
		private static Mesh srcMesh;

		// Token: 0x04002357 RID: 9047
		private static Dictionary<int, Material> replacementMaterials = new Dictionary<int, Material>();

		// Token: 0x04002358 RID: 9048
		private Mesh mesh;

		// Token: 0x04002359 RID: 9049
		private SpriteBounds spriteBounds;

		// Token: 0x0400235A RID: 9050
		private MaterialPropertyBlock _block;

		// Token: 0x020007D2 RID: 2002
		private class Rend
		{
			// Token: 0x060033E9 RID: 13289 RVA: 0x000DFF40 File Offset: 0x000DE340
			public Rend(SpriteRenderer sr, Mesh mesh, string keyword)
			{
				this.keyword = keyword;
				this.hasKeyword = (keyword != string.Empty);
				this.mr = sr.gameObject.AddEmptyChild((!this.hasKeyword) ? sr.name : (sr.name + " " + keyword)).AddComponent<MeshRenderer>();
				this.mf = this.mr.gameObject.AddComponent<MeshFilter>();
				this.mf.sharedMesh = mesh;
				this.sharedMaterial = sr.sharedMaterial;
			}

			// Token: 0x17000775 RID: 1909
			// (get) Token: 0x060033EA RID: 13290 RVA: 0x000DFFD6 File Offset: 0x000DE3D6
			// (set) Token: 0x060033EB RID: 13291 RVA: 0x000DFFE3 File Offset: 0x000DE3E3
			public Material sharedMaterial
			{
				get
				{
					return this.mr.sharedMaterial;
				}
				set
				{
					this.mr.sharedMaterial = ((!this.hasKeyword) ? value : BatchedSprite.GetReplacementMaterial(value, this.keyword));
				}
			}

			// Token: 0x0400235B RID: 9051
			public MeshRenderer mr;

			// Token: 0x0400235C RID: 9052
			private MeshFilter mf;

			// Token: 0x0400235D RID: 9053
			private string keyword;

			// Token: 0x0400235E RID: 9054
			private bool hasKeyword;
		}
	}
}
