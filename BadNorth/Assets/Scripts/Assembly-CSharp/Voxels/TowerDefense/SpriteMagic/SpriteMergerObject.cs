using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense.SpriteMagic
{
	// Token: 0x020007DB RID: 2011
	public class SpriteMergerObject : MonoBehaviour
	{
		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x0600341A RID: 13338 RVA: 0x000E17C1 File Offset: 0x000DFBC1
		private Mesh mesh
		{
			get
			{
				return this.spriteMesh.mesh;
			}
		}

		// Token: 0x0600341B RID: 13339 RVA: 0x000E17D0 File Offset: 0x000DFBD0
		public static SpriteMergerObject GetNew(MergeableSprite sr)
		{
			Material sharedMaterial = sr.spriteRenderer.sharedMaterial;
			GameObject gameObject = new GameObject("MergedSprites " + sharedMaterial.name + " " + sr.spriteRenderer.sprite.texture.name);
			SpriteMergerObject spriteMergerObject = gameObject.AddComponent<SpriteMergerObject>();
			spriteMergerObject.mf = gameObject.AddComponent<MeshFilter>();
			spriteMergerObject.spriteMesh = new SpriteMesh(0);
			spriteMergerObject.mf.sharedMesh = spriteMergerObject.spriteMesh.mesh;
			MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
			List<Material> list = new List<Material>();
			Material material = UnityEngine.Object.Instantiate<Material>(sharedMaterial);
			material.EnableKeyword("_STATIC_ON");
			material.mainTexture = sr.spriteRenderer.sprite.texture;
			list.Add(material);
			if (material.shader.name.ToLower().Contains("grass"))
			{
				string text = sharedMaterial.name.ToLower();
				if (!text.Contains("grass") && !text.Contains("flower"))
				{
					if (sharedMaterial.HasProperty("_Mirror"))
					{
						Material material2 = UnityEngine.Object.Instantiate<Material>(material);
						material2.EnableKeyword("_MIRROR_ON");
						list.Add(material2);
					}
					Material material3 = UnityEngine.Object.Instantiate<Material>(material);
					material3.EnableKeyword("_OUTLINE_OUTER");
					material.EnableKeyword("_OUTLINE_INNER");
					list.Add(material3);
				}
			}
			meshRenderer.sharedMaterials = list.ToArray();
			return spriteMergerObject;
		}

		// Token: 0x0600341C RID: 13340 RVA: 0x000E1945 File Offset: 0x000DFD45
		private void OnDestroy()
		{
			UnityEngine.Object.Destroy(this.mesh);
		}

		// Token: 0x0600341D RID: 13341 RVA: 0x000E1952 File Offset: 0x000DFD52
		public void Wipe()
		{
			this.mesh.Clear();
			this.spriteRenderes.Clear();
		}

		// Token: 0x0600341E RID: 13342 RVA: 0x000E196A File Offset: 0x000DFD6A
		public void Add(MergeableSprite spriteRenderer)
		{
			this.spriteRenderes.Add(spriteRenderer);
		}

		// Token: 0x0600341F RID: 13343 RVA: 0x000E1978 File Offset: 0x000DFD78
		public void Merge()
		{
			int count = this.spriteRenderes.Count;
			this.spriteMesh.Resize(count);
			for (int i = 0; i < this.spriteRenderes.Count; i++)
			{
				MergeableSprite mergeableSprite = this.spriteRenderes[i];
				SpriteStampDef stamp = new SpriteStampDef(mergeableSprite.spriteRenderer, mergeableSprite.navPos);
				stamp.matrix = base.transform.worldToLocalMatrix * stamp.matrix;
				this.spriteMesh.SetVerts(i, stamp);
			}
			this.spriteMesh.Apply();
		}

		// Token: 0x04002389 RID: 9097
		private SpriteMesh spriteMesh;

		// Token: 0x0400238A RID: 9098
		private MeshFilter mf;

		// Token: 0x0400238B RID: 9099
		private MeshRenderer mr;

		// Token: 0x0400238C RID: 9100
		private List<MergeableSprite> spriteRenderes = new List<MergeableSprite>();
	}
}
