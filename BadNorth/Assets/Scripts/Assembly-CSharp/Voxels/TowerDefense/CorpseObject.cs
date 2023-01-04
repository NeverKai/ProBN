using System;
using System.Collections.Generic;
using UnityEngine;
using Voxels.TowerDefense.SpriteMagic;

namespace Voxels.TowerDefense
{
	// Token: 0x02000744 RID: 1860
	public class CorpseObject : ChildComponent<CorpseManager>
	{
		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06003090 RID: 12432 RVA: 0x000C6638 File Offset: 0x000C4A38
		private Mesh[] srcMeshes
		{
			get
			{
				return ScriptableObjectSingleton<PrefabManager>.instance.corpseMeshes;
			}
		}

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x06003091 RID: 12433 RVA: 0x000C6644 File Offset: 0x000C4A44
		private int corpseCount
		{
			get
			{
				return base.manager.corpseCount;
			}
		}

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06003092 RID: 12434 RVA: 0x000C6651 File Offset: 0x000C4A51
		private bool island
		{
			get
			{
				return base.manager.island;
			}
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06003093 RID: 12435 RVA: 0x000C665E File Offset: 0x000C4A5E
		private Mesh mesh
		{
			get
			{
				return this.corpseMesh.mesh;
			}
		}

		// Token: 0x06003094 RID: 12436 RVA: 0x000C666C File Offset: 0x000C4A6C
		private static CorpseObject.CorpseMesh GetCorpseMesh(int corpseCount)
		{
			CorpseObject.CorpseMesh corpseMesh;
			if (!CorpseObject.initialCorpseMeshes.TryGetValue(corpseCount, out corpseMesh))
			{
				corpseMesh = new CorpseObject.CorpseMesh(corpseCount);
				CorpseObject.initialCorpseMeshes.Add(corpseCount, corpseMesh);
			}
			return new CorpseObject.CorpseMesh(corpseMesh);
		}

		// Token: 0x06003095 RID: 12437 RVA: 0x000C66A4 File Offset: 0x000C4AA4
		public void Setup(SpriteAnimator spriteAnimator, NavigationMesh navMesh)
		{
			this.corpseMesh = CorpseObject.GetCorpseMesh(this.corpseCount);
			this.corpseMesh.mesh.bounds = new Bounds(navMesh.bounds.center, navMesh.bounds.size + Vector3.one * 0.1f);
			Material material = ScriptableObjectSingleton<PrefabManager>.instance.corpseMaterial;
			if (this.island)
			{
				material = UnityEngine.Object.Instantiate<Material>(material);
				material.EnableKeyword("_ISLAND_ON");
			}
			base.gameObject.AddComponent<MeshFilter>().sharedMesh = this.mesh;
			MeshRenderer meshRenderer = base.gameObject.AddComponent<MeshRenderer>();
			meshRenderer.sharedMaterial = material;
			meshRenderer.SetPropertyBlock(spriteAnimator.GetMaterialPropertyBlock());
		}

		// Token: 0x06003096 RID: 12438 RVA: 0x000C675E File Offset: 0x000C4B5E
		private void OnDestroy()
		{
			if (this.corpseMesh != null)
			{
				UnityEngine.Object.Destroy(this.corpseMesh.mesh);
			}
		}

		// Token: 0x06003097 RID: 12439 RVA: 0x000C677B File Offset: 0x000C4B7B
		public void AddCorpse(Matrix4x4 matrix, SpriteAnimator spriteAnimator, NavPos navPos)
		{
			if (this.corpseMesh == null)
			{
				this.Setup(spriteAnimator, navPos.navigationMesh);
			}
			this.corpseMesh.AddCorpse(matrix, spriteAnimator, navPos);
		}

		// Token: 0x06003098 RID: 12440 RVA: 0x000C67A4 File Offset: 0x000C4BA4
		public void Wipe()
		{
			if (this.corpseMesh != null)
			{
				this.corpseMesh.Wipe();
			}
		}

		// Token: 0x06003099 RID: 12441 RVA: 0x000C67BC File Offset: 0x000C4BBC
		public void Precache()
		{
			CorpseObject.GetCorpseMesh(this.corpseCount);
		}

		// Token: 0x0400206A RID: 8298
		private CorpseObject.CorpseMesh corpseMesh;

		// Token: 0x0400206B RID: 8299
		private static Dictionary<int, CorpseObject.CorpseMesh> initialCorpseMeshes = new Dictionary<int, CorpseObject.CorpseMesh>();

		// Token: 0x0400206C RID: 8300
		private int index;

		// Token: 0x02000745 RID: 1861
		private class CorpseMesh
		{
			// Token: 0x0600309B RID: 12443 RVA: 0x000C67D8 File Offset: 0x000C4BD8
			public CorpseMesh(int corpseCount)
			{
				Mesh[] corpseMeshes = ScriptableObjectSingleton<PrefabManager>.instance.corpseMeshes;
				CombineInstance[] array = new CombineInstance[corpseCount];
				this.ms = new CorpseObject.CorpseMesh.M[corpseCount];
				this.srcVerts = new Vector3[corpseMeshes.Length][];
				for (int i = 0; i < this.srcVerts.Length; i++)
				{
					this.srcVerts[i] = corpseMeshes[i].vertices;
				}
				int num = 0;
				for (int j = 0; j < array.Length; j++)
				{
					this.ms[j] = default(CorpseObject.CorpseMesh.M);
					this.ms[j].meshIndex = UnityEngine.Random.Range(0, corpseMeshes.Length);
					this.ms[j].start = num;
					this.ms[j].count = this.srcVerts[this.ms[j].meshIndex].Length;
					num += this.ms[j].count;
					array[j] = default(CombineInstance);
					array[j].mesh = corpseMeshes[this.ms[j].meshIndex];
				}
				this.mesh = new Mesh();
				this.mesh.CombineMeshes(array);
				this.mVerts = new Vector3[this.mesh.vertexCount];
				this.mColor = new Color32[this.mVerts.Length];
			}

			// Token: 0x0600309C RID: 12444 RVA: 0x000C6960 File Offset: 0x000C4D60
			public CorpseMesh(CorpseObject.CorpseMesh original)
			{
				this.mesh = UnityEngine.Object.Instantiate<Mesh>(original.mesh);
				this.mVerts = new Vector3[original.mVerts.Length];
				this.mColor = new Color32[original.mColor.Length];
				original.mVerts.CopyTo(this.mVerts, 0);
				original.mColor.CopyTo(this.mColor, 0);
				this.ms = original.ms;
				this.srcVerts = original.srcVerts;
			}

			// Token: 0x0600309D RID: 12445 RVA: 0x000C69E8 File Offset: 0x000C4DE8
			public void AddCorpse(Matrix4x4 matrix, SpriteAnimator spriteAnimator, NavPos navPos)
			{
				CorpseObject.CorpseMesh.M m = this.ms[this.index];
				this.index = (this.index + 1) % this.ms.Length;
				bool flag = navPos.GetBorderness() > 0f;
				matrix = navPos.transform.worldToLocalMatrix * matrix;
				Bounds bounds = (!navPos.island) ? default(Bounds) : navPos.island.navSpotter.GetNavSpot(navPos.pos, true).meshBounds;
				bounds.max += new Vector3(0f, 0.2f, 0f);
				for (int i = 0; i < m.count; i++)
				{
					Vector3 vector = matrix.MultiplyPoint(this.srcVerts[m.meshIndex][i]);
					if (flag)
					{
						NavPos navPos2 = navPos;
						if (!navPos2.MoveTo(vector))
						{
							vector = (navPos2.pos + (vector - navPos2.pos).GetClampedMagnitude(0.08f)).SetY(vector.y);
						}
					}
					this.mVerts[i + m.start] = vector;
					this.mColor[i + m.start] = spriteAnimator.color.SetB((float)((!bounds.Contains(vector)) ? 0 : 1));
				}
				this.mesh.vertices = this.mVerts;
				this.mesh.colors32 = this.mColor;
			}

			// Token: 0x0600309E RID: 12446 RVA: 0x000C6BB0 File Offset: 0x000C4FB0
			public void Wipe()
			{
				for (int i = 0; i < this.mVerts.Length; i++)
				{
					this.mVerts[i] = Vector3.zero;
				}
				this.mesh.vertices = this.mVerts;
			}

			// Token: 0x0400206D RID: 8301
			private CorpseObject.CorpseMesh.M[] ms;

			// Token: 0x0400206E RID: 8302
			public Mesh mesh;

			// Token: 0x0400206F RID: 8303
			private Vector3[] mVerts;

			// Token: 0x04002070 RID: 8304
			private Color32[] mColor;

			// Token: 0x04002071 RID: 8305
			private Vector3[][] srcVerts;

			// Token: 0x04002072 RID: 8306
			private int index;

			// Token: 0x02000746 RID: 1862
			private struct M
			{
				// Token: 0x04002073 RID: 8307
				public int meshIndex;

				// Token: 0x04002074 RID: 8308
				public int start;

				// Token: 0x04002075 RID: 8309
				public int count;
			}
		}
	}
}
