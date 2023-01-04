using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense.SpriteMagic
{
	// Token: 0x020007DD RID: 2013
	public class SpriteStamper : MonoBehaviour
	{
		// Token: 0x06003425 RID: 13349 RVA: 0x000E1C1C File Offset: 0x000E001C
		private void SetupBase(int maxCount)
		{
			this.maxCount = maxCount;
			this.mr = base.gameObject.AddComponent<MeshRenderer>();
			this.mf = base.gameObject.AddComponent<MeshFilter>();
			this.spriteMesh = new SpriteMesh(maxCount);
			this.mf.sharedMesh = this.spriteMesh.mesh;
		}

		// Token: 0x06003426 RID: 13350 RVA: 0x000E1C74 File Offset: 0x000E0074
		private void Setup(Material material, Texture texture, int maxCount = 128)
		{
			this.maxCount = maxCount;
			if (this.mr == null)
			{
				this.mr = base.gameObject.AddComponent<MeshRenderer>();
			}
			if (this.mf == null)
			{
				this.mf = base.gameObject.AddComponent<MeshFilter>();
			}
			this.srcMat = material;
			bool flag = this.srcMat.HasProperty(SpriteStamper.mirrorId);
			Dictionary<Material, Material[]> dictionary = (!this.loose) ? ((!flag) ? SpriteStamper.staticMaterials : SpriteStamper.mirrorMaterials) : SpriteStamper.looseMaterials;
			Material[] array;
			if (!dictionary.TryGetValue(this.srcMat, out array))
			{
				if (this.loose)
				{
					array = new Material[]
					{
						UnityEngine.Object.Instantiate<Material>(this.srcMat)
					};
					array[0].EnableKeyword("_LOOSE_ON");
				}
				else if (flag)
				{
					array = new Material[]
					{
						UnityEngine.Object.Instantiate<Material>(this.srcMat),
						UnityEngine.Object.Instantiate<Material>(this.srcMat)
					};
					array[0].EnableKeyword("_STATIC_ON");
					array[1].EnableKeyword("_STATIC_ON");
					array[1].EnableKeyword("_MIRROR_ON");
				}
				else
				{
					array = new Material[]
					{
						UnityEngine.Object.Instantiate<Material>(this.srcMat)
					};
					array[0].EnableKeyword("_LOOSE_ON");
				}
			}
			this.mr.sharedMaterials = array;
			if (this.spriteMesh == null)
			{
				this.spriteMesh = new SpriteMesh(maxCount);
				this.mf.sharedMesh = this.spriteMesh.mesh;
			}
			if (SpriteStamper.block == null)
			{
				SpriteStamper.block = new MaterialPropertyBlock();
			}
			SpriteStamper.block.SetTexture(ShaderId.mainTexId, texture);
			this.mr.SetPropertyBlock(SpriteStamper.block);
		}

		// Token: 0x06003427 RID: 13351 RVA: 0x000E1E44 File Offset: 0x000E0244
		public static SpriteStamper NewSpriteStamper(Transform parent, bool loose, SpriteStampDef stamp, int key, int maxCount = 128)
		{
			SpriteStamper spriteStamper = new GameObject("SpriteStamper").AddComponent<SpriteStamper>();
			spriteStamper.SetupBase(maxCount);
			spriteStamper.gameObject.SetActive(true);
			spriteStamper.name = "SpriteStamper " + stamp.material.name;
			spriteStamper.transform.SetParent(parent, false);
			spriteStamper.gameObject.layer = stamp.layer;
			spriteStamper.loose = loose;
			spriteStamper.Setup(stamp.material, stamp.texture, maxCount);
			spriteStamper.key = key;
			return spriteStamper;
		}

		// Token: 0x06003428 RID: 13352 RVA: 0x000E1ED5 File Offset: 0x000E02D5
		public void Add(SpriteStampDef stamp)
		{
			stamp.matrix = base.transform.worldToLocalMatrix * stamp.matrix;
			this.spriteMesh.SetVerts(this.index, stamp);
			this.ApplyAndIterate();
		}

		// Token: 0x06003429 RID: 13353 RVA: 0x000E1F0D File Offset: 0x000E030D
		private void ApplyAndIterate()
		{
			this.spriteMesh.Apply();
			this.count = Mathf.Max(this.count, this.index + 1);
			this.index = (this.index + 1) % this.maxCount;
		}

		// Token: 0x0600342A RID: 13354 RVA: 0x000E1F48 File Offset: 0x000E0348
		private void OnDestroy()
		{
			UnityEngine.Object.Destroy(this.spriteMesh.mesh);
		}

		// Token: 0x0600342B RID: 13355 RVA: 0x000E1F5A File Offset: 0x000E035A
		public void Clear()
		{
			this.spriteMesh.ClearVerts();
		}

		// Token: 0x04002393 RID: 9107
		private SpriteMesh spriteMesh;

		// Token: 0x04002394 RID: 9108
		private MeshRenderer mr;

		// Token: 0x04002395 RID: 9109
		private MeshFilter mf;

		// Token: 0x04002396 RID: 9110
		public int count;

		// Token: 0x04002397 RID: 9111
		private int index;

		// Token: 0x04002398 RID: 9112
		private int maxCount = 128;

		// Token: 0x04002399 RID: 9113
		private int key;

		// Token: 0x0400239A RID: 9114
		private bool loose;

		// Token: 0x0400239B RID: 9115
		private Material srcMat;

		// Token: 0x0400239C RID: 9116
		private static Dictionary<Material, Material[]> looseMaterials = new Dictionary<Material, Material[]>();

		// Token: 0x0400239D RID: 9117
		private static Dictionary<Material, Material[]> mirrorMaterials = new Dictionary<Material, Material[]>();

		// Token: 0x0400239E RID: 9118
		private static Dictionary<Material, Material[]> staticMaterials = new Dictionary<Material, Material[]>();

		// Token: 0x0400239F RID: 9119
		private static ShaderId mirrorId = "_Mirror";

		// Token: 0x040023A0 RID: 9120
		private static MaterialPropertyBlock block;
	}
}
