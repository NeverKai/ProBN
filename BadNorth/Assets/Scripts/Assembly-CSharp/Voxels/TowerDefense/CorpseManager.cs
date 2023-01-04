using System;
using System.Collections.Generic;
using UnityEngine;
using Voxels.TowerDefense.SpriteMagic;

namespace Voxels.TowerDefense
{
	// Token: 0x02000742 RID: 1858
	public class CorpseManager : MonoBehaviour, IIslandWipe
	{
		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06003089 RID: 12425 RVA: 0x000C634C File Offset: 0x000C474C
		private Mesh[] srcMeshes
		{
			get
			{
				return ScriptableObjectSingleton<PrefabManager>.instance.corpseMeshes;
			}
		}

		// Token: 0x0600308A RID: 12426 RVA: 0x000C6358 File Offset: 0x000C4758
		public void AddCorpse(Matrix4x4 matrix, SpriteAnimator spriteAnimator, NavPos navPos)
		{
			int materialKey = spriteAnimator.GetMaterialKey();
			CorpseObject corpseObject;
			if (!this.corpseObjects.TryGetValue(materialKey, out corpseObject))
			{
				this.PrecacheIfNeeded(true);
				corpseObject = this.precachedCorpseObjects.Dequeue();
				corpseObject.name = "CorpseObject " + materialKey;
				corpseObject.gameObject.SetActive(true);
				this.corpseObjects.Add(materialKey, corpseObject);
				corpseObject.gameObject.layer = LayerMaster.blood.id;
			}
			corpseObject.AddCorpse(matrix, spriteAnimator, navPos);
		}

		// Token: 0x0600308B RID: 12427 RVA: 0x000C63E0 File Offset: 0x000C47E0
		public void RegisterCorpse(SpriteAnimator spriteAnimator)
		{
			int materialKey = spriteAnimator.GetMaterialKey();
			CorpseObject corpseObject;
			if (!this.corpseObjects.TryGetValue(materialKey, out corpseObject))
			{
				this.PrecacheIfNeeded(true);
				corpseObject = this.precachedCorpseObjects.Dequeue();
				corpseObject.name = "CorpseObject " + materialKey;
				corpseObject.gameObject.SetActive(true);
				this.corpseObjects.Add(materialKey, corpseObject);
				corpseObject.gameObject.layer = LayerMaster.blood.id;
			}
		}

		// Token: 0x0600308C RID: 12428 RVA: 0x000C6460 File Offset: 0x000C4860
		IEnumerator<GenInfo> IIslandWipe.OnIslandWipe(Island island)
		{
			foreach (CorpseObject corpseObject in this.corpseObjects.Values)
			{
				corpseObject.Wipe();
			}
			while (this.precachedCorpseObjects.Count > 0)
			{
				UnityEngine.Object.Destroy(this.precachedCorpseObjects.Dequeue().gameObject);
			}
			yield return new GenInfo("Cleaning corpses", GenInfo.Mode.interruptable);
			yield break;
		}

		// Token: 0x0600308D RID: 12429 RVA: 0x000C647C File Offset: 0x000C487C
		public void Precache()
		{
			for (int i = 0; i < 10; i++)
			{
				this.PrecacheIfNeeded(false);
			}
		}

		// Token: 0x0600308E RID: 12430 RVA: 0x000C64A4 File Offset: 0x000C48A4
		private void PrecacheIfNeeded(bool checkEmpty = true)
		{
			if (checkEmpty && this.precachedCorpseObjects.Count > 0)
			{
				return;
			}
			CorpseObject corpseObject = base.gameObject.AddEmptyChild("PrecachedCorpseObject").AddComponent<CorpseObject>();
			corpseObject.Precache();
			corpseObject.gameObject.SetActive(false);
			this.precachedCorpseObjects.Enqueue(corpseObject);
		}

		// Token: 0x04002063 RID: 8291
		public int corpseCount = 128;

		// Token: 0x04002064 RID: 8292
		public bool island;

		// Token: 0x04002065 RID: 8293
		private Dictionary<int, CorpseObject> corpseObjects = new Dictionary<int, CorpseObject>();

		// Token: 0x04002066 RID: 8294
		private Queue<CorpseObject> precachedCorpseObjects = new Queue<CorpseObject>();

		// Token: 0x02000743 RID: 1859
		private struct M
		{
			// Token: 0x04002067 RID: 8295
			public int meshIndex;

			// Token: 0x04002068 RID: 8296
			public int start;

			// Token: 0x04002069 RID: 8297
			public int count;
		}
	}
}
